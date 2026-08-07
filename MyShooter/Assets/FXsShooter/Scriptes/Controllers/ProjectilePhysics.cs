using System;
using UnityEngine;

namespace FXs.Shooter
{
    public class ProjectilePhysics : IProjectilePhysics
    {
        private IUIController uIController;

        private IInputController inputController;
        private ICannonController cannonController;
        private IProjectileFactory projectileFactory;
        private ShooterGameSettings settings;
        private int power;

        public event Action<int, Vector3, Vector3> OnShot;
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
        }

        public void StartGame()
        {
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
            }
        }

        public void EndGame()
        {
            uIController.OnPowerChanged -= PowerChanged;
            inputController.OnShoot -= ShotInput;
        }

        private void ShotInput()
        {
            Debug.Log($"ShotInput: {power}");
            OnShot?.Invoke(power, cannonController.Position, cannonController.Direction);
        }

        private void PowerChanged(int power)
        {
            this.power = power;
            Debug.Log($"PowerChanged: {power}");
        }
    }
}
