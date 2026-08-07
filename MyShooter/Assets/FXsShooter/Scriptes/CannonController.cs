using UnityEngine;

namespace FXs.Shooter
{
    public class CannonController : MonoGameEntity
    {
        [SerializeField] private Transform pivot;

        private int maxHorizontalAngle;
        private int maxVerticalAngle;
        private float horizontalSpeed;
        private float verticalSpeed;

        private float hPosition;
        private float vPosition;

        private InputController inputController;
        private bool gameIsStarted;

        public override void Init()
        {

        }

        public override void Setup(GameContext context)
        {
            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                maxHorizontalAngle = settings.MaxHorizontalAngle;
                maxVerticalAngle = settings.MaxVerticalAngle;
                horizontalSpeed = settings.HorizontalSpeed;
                verticalSpeed = settings.VerticalSpeed;
            }

            if(context.TryGetEntity(out InputController inputController))
            {
                this.inputController = inputController;
            }
        }

        public override void StartGame()
        {
            gameIsStarted = true;
           
            inputController.OnAim += Aim;
        }

        public override void UpdateGame()
        {
            if(!gameIsStarted)
            {
                return;
            }
        }

        public override void FixedUpdateGame()
        {

        }

        public override void EndGame()
        {
            gameIsStarted = false;
           
            inputController.OnAim -= Aim;
        }

        private void Aim(float timeStep, Vector2 value)
        {
            hPosition += horizontalSpeed * value.x * timeStep;
            vPosition += verticalSpeed * value.y * timeStep;

            hPosition = Mathf.Clamp(hPosition, -maxHorizontalAngle, maxHorizontalAngle);
            vPosition = Mathf.Clamp(vPosition, 0f, maxVerticalAngle);

            transform.localEulerAngles = new Vector3(0f, hPosition, 0f);
            pivot.localEulerAngles = new Vector3(-vPosition, 0f, 0f);
        }
    }
}
