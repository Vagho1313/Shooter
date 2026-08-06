namespace FXs.Shooter
{
    public class ProjectilePhysics : IGameEntity
    {
        private UIController uIController;

        public void Init()
        {

        }

        public void Setup(GameContext context)
        {
            if (context.TryGetEntity(out UIController uIController))
            {
                this.uIController = uIController;
            }
        }

        public void StartGame()
        {
            PowerChanged(uIController.ShotPower);
            uIController.OnPowerChanged += PowerChanged;
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
        }

        private void PowerChanged(int power)
        {
            UnityEngine.Debug.Log($"PowerChanged: {power}");
        }
    }
}
