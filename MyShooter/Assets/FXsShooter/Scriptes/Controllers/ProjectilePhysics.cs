using System;
using System.Collections.Generic;
using UnityEngine;

namespace FXs.Shooter
{
    // Mor multiplayer need to use fixed-point physics like my Calculable Mechanics v2
    // https://assetstore.unity.com/packages/tools/physics/angry-balls-multiplayer-with-fixed-point-physics-calculable-mech-278732
    public class ProjectilePhysics : IProjectilePhysics
    {
        private IUIController uIController;

        private IInputController inputController;
        private ICannonController cannonController;
        private IProjectileFactory projectileFactory;
        private ShooterGameSettings settings;
        private ILevelController levelController;
        private int power;
        private List<Projectile> removedProjectiles;

        public event Action<int, Vector3, Vector3> OnShot;
        public event Action<Vector3, Vector3, Vector2, Transform> OnProjectileHit;
        public event Action<Projectile> OnProjectileDestroyed;

        public void Init()
        {

        }

        public void Setup(GameContext context)
        {
            if (context.TryGetEntity(out IUIController uIController))
            {
                this.uIController = uIController;
            }

            if (context.TryGetEntity(out IInputController inputController))
            {
                this.inputController = inputController;
            }

            if (context.TryGetEntity(out ICannonController cannonController))
            {
                this.cannonController = cannonController;
            }

            if (context.TryGetEntity(out IProjectileFactory projectileFactory))
            {
                this.projectileFactory = projectileFactory;
            }

            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                this.settings = settings;
            }

            if (context.TryGetEntity(out ILevelController levelController))
            {
                this.levelController = levelController;
            }            
        }

        public void StartGame()
        {
            removedProjectiles = new List<Projectile>();
            PowerChanged(uIController.ShotPower);
            uIController.OnPowerChanged += PowerChanged;
            inputController.OnShoot += ShotInput;
        }

        public void UpdateGame()
        {
            
        }

        public void FixedUpdateGame()
        {
            if(projectileFactory == null)
            {
                return;
            }
            float timeStep = Time.fixedDeltaTime;

            foreach (var projectile in projectileFactory.Projectiles)
            {
                projectile.velocity += timeStep * settings.Gravity;
                projectile.transform.position += timeStep * projectile.velocity;
                projectile.transform.LookAt(projectile.transform.position + projectile.velocity, Vector3.up);

                if (projectile.transform.position.y < 0f ||
                    projectile.velocity.sqrMagnitude < settings.MinSpeed * settings.MinSpeed)
                {
                    removedProjectiles.Add(projectile);
                    continue;
                }

                foreach (Plane plane in levelController.CurrentLevel.Planes)
                {
                    if(CalculateHit(projectile, plane))
                    {
                        break;
                    }
                }
            }

            foreach (var projectile in removedProjectiles)
            {
                OnProjectileDestroyed?.Invoke(projectile);
            }

            removedProjectiles.Clear();
        }

        public void EndGame()
        {
            uIController.OnPowerChanged -= PowerChanged;
            inputController.OnShoot -= ShotInput;
        }

        private void ShotInput()
        {
            OnShot?.Invoke(power, cannonController.Position, cannonController.Direction);
        }

        private void PowerChanged(int power)
        {
            this.power = power;
        }

        private bool CalculateHit(Projectile projectile, Plane plane)
        {
            Vector3 planeNormal = plane.Normal;

            if (Vector3.Dot(projectile.velocity, planeNormal) > 0f)
            {
                return false;
            }

            float projectileRadius = projectile.radius;
            Vector3 projectilePosition = projectile.transform.position;
            Vector3 projectOnPlane = Vector3.ProjectOnPlane(projectilePosition - plane.Position, planeNormal);
            Vector3 pointOnPlane = plane.Position + projectOnPlane;

            if ((projectilePosition - pointOnPlane).sqrMagnitude > projectileRadius * projectileRadius)
            {
                return false;
            }

            if (
                Mathf.Abs(Vector3.Dot(projectOnPlane, plane.transform.right)) - projectileRadius >= 0.5f * plane.Size.x ||
                Mathf.Abs(Vector3.Dot(projectOnPlane, plane.transform.up)) - projectileRadius >= 0.5f * plane.Size.y)
            {
                return false;
            }

            float hitBounciness = projectile.bounciness * plane.bounciness;

            projectile.velocity *= hitBounciness;

            if (projectile.velocity.sqrMagnitude < settings.MinSpeed * settings.MinSpeed)
            {
                if (plane.hasEffect)
                {
                    OnProjectileHit?.Invoke(projectilePosition, planeNormal, plane.Size, plane.transform);
                }
                removedProjectiles.Add(projectile);
                return true;
            }

            Vector3 normalVelocity = Vector3.Project(projectile.velocity, planeNormal);
            projectile.velocity -= 2f * normalVelocity;
            if (plane.hasEffect)
            {
                OnProjectileHit?.Invoke(projectilePosition, planeNormal, plane.Size, plane.transform);
            }
            return true;
        }
    }
}
