using UnityEngine;

namespace FXs.Shooter
{
    public class CannonController : MonoGameEntity, ICannonController
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private Transform point;
        [SerializeField] private Transform cannon;

        private float hPosition;
        private float vPosition;
        private bool gameIsStarted;
        private float amplitude;
        private bool shaking;
        private bool canShot;

        private IInputController inputController;
        private ShooterGameSettings settings;
        private IProjectilePhysics projectilePhysics;

        public Vector3 Direction => point.forward;
        public Vector3 Position => point.position;
        public bool CanShot => canShot;

        public override void Init()
        {

        }

        public override void Setup(GameContext context)
        {
            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                this.settings = settings;
            }

            if(context.TryGetEntity(out IInputController inputController))
            {
                this.inputController = inputController;
            }

            if (context.TryGetEntity(out IProjectilePhysics projectilePhysics))
            {
                this.projectilePhysics = projectilePhysics;
            }
        }

        public override void StartGame()
        {
            gameIsStarted = true;
            canShot = true;

            inputController.OnAim += Aim;
            projectilePhysics.OnShot += ProjectileShot;
        }

        public override void UpdateGame()
        {
            if(!gameIsStarted)
            {
                return;
            }

            if (shaking)
            {
                canShot = false;

                float deltaTime = Time.deltaTime;
                cannon.localPosition = new Vector3(0f, 0f, amplitude * Mathf.Sin(2f * Mathf.PI * settings.CannonShakeCurve(amplitude / settings.CannonShakeAmplitude)));
                amplitude -= settings.CannonShakeAttenuation * deltaTime;
                if (amplitude <= 0f)
                {
                    shaking = false;
                    canShot = true;
                    amplitude = 0f;
                }
            }
        }

        public override void FixedUpdateGame()
        {

        }

        public override void EndGame()
        {
            gameIsStarted = false;
           
            inputController.OnAim -= Aim;
            projectilePhysics.OnShot -= ProjectileShot;
        }

        private void Aim(float timeStep, Vector2 value)
        {
            hPosition += settings.HorizontalSpeed * value.x * timeStep;
            vPosition += settings.VerticalSpeed * value.y * timeStep;

            hPosition = Mathf.Clamp(hPosition, -settings.MaxHorizontalAngle, settings.MaxHorizontalAngle);
            vPosition = Mathf.Clamp(vPosition, 0f, settings.MaxVerticalAngle);

            transform.localEulerAngles = new Vector3(0f, hPosition, 0f);
            pivot.localEulerAngles = new Vector3(-vPosition, 0f, 0f);
        }

        private void ProjectileShot(int power, Vector3 position, Vector3 direction)
        {
            amplitude = power * settings.CannonShakeAmplitude / settings.MaxPower;
            shaking = true;
        }
    }
}
