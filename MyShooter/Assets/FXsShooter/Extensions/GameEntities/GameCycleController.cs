namespace FXs
{
    public class GameCycleController
    {
        private readonly GameContext gameContext;

        private readonly IGameEntity[] entities;

        public GameCycleController(GameContext gameContext, Container[] containers, IGameEntity[] entities)
        {
            this.entities = entities;

            foreach (var container in containers)
            {
                container.Init();
            }

            foreach (var gameEntity in entities)
            {
                gameEntity.Init();
            }

            this.gameContext = gameContext;
        }

        public void Setup()
        {
            foreach (var gameEntity in entities)
            {
                gameEntity.Setup(gameContext);
            }
        }

        public void StartGame()
        {
            foreach (var gameEntity in entities)
            {
                gameEntity.StartGame();
            }
        }

        public void UpdateGame()
        {
            foreach (var gameEntity in entities)
            {
                gameEntity.UpdateGame();
            }
        }

        public void FixedUpdateGame()
        {
            foreach (var gameEntity in entities)
            {
                gameEntity.FixedUpdateGame();
            }
        }

        public void EndGame()
        {
            foreach (var gameEntity in entities)
            {
                gameEntity.EndGame();
            }
        }
    }
}
