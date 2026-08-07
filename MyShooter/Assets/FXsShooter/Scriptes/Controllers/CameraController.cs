using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FXs.Shooter
{
    public class CameraController : MonoGameEntity, ICameraController
    {
        private IProjectilePhysics projectilePhysics;
        private ShooterGameSettings settings;
        private float amplitude;
        private bool shaking;

        public override void Init()
        {

        }

        public override void Setup(GameContext context)
        {
            if (context.TryGetEntity(out IProjectilePhysics projectilePhysics))
            {
                this.projectilePhysics = projectilePhysics;
            }

            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                this.settings = settings;
            }
        }

        public override void StartGame()
        {
            projectilePhysics.OnShot += ProjectileShot;
        }

        public override void UpdateGame()
        {
            if(shaking)
            {
                float deltaTime = Time.deltaTime;
                transform.localPosition = new Vector3(amplitude * Mathf.Sin(2f * Mathf.PI * settings.CameraShakeCurve(amplitude / settings.CameraShakeAmplitude)), 0f, 0f);
                amplitude -= settings.CameraShakeAttenuation * deltaTime;
                if (amplitude <= 0f)
                {
                    shaking = false;
                    amplitude = 0f;
                }
            }
        }

        public override void FixedUpdateGame()
        {

        }

        public override void EndGame()
        {
            projectilePhysics.OnShot -= ProjectileShot;
        }

        private void ProjectileShot(int power, Vector3 position, Vector3 direction)
        {
            amplitude = power * settings.CameraShakeAmplitude / settings.MaxPower;
            shaking = true;
        }
    }
}
