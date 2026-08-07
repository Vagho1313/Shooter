using System;

namespace FXs.Shooter
{
    public class ProjectilePhysics : IGameEntity
    {
        private UIController uIController;

        private InputController inputController;
        private int power;

        public event Action<int> OnShot;


        public void Init()
        {

        }

        public void Setup(GameContext context)
        {
            if (context.TryGetEntity(out UIController uIController))
            {
                this.uIController = uIController;
            }

            if (context.TryGetEntity(out InputController inputController))
            {
                this.inputController = inputController;
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
            
        }

        public void EndGame()
        {
            uIController.OnPowerChanged -= PowerChanged;
            inputController.OnShoot -= ShotInput;
        }

        private void ShotInput()
        {
            UnityEngine.Debug.Log($"ShotInput: {power}");
            OnShot?.Invoke(power);
        }

        private void PowerChanged(int power)
        {
            this.power = power;
            UnityEngine.Debug.Log($"PowerChanged: {power}");
        }
    }
}
