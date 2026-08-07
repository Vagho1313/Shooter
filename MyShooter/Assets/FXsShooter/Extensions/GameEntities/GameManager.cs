using System.Collections.Generic;
using UnityEngine;

namespace FXs
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Object containerObject;

        private GameCycleController cycleController;

        private void Awake()
        {
            if (containerObject is not IGameContainer gameContainer)
            {
                Debug.LogWarning("Container object must implement IGameContainer");
                enabled = false;
                return;
            }

            Container[] containers = gameContainer.GetContainers();
            IGameEntity[] gameEntities = gameContainer.GetGameEntities();

            GameContext context = new GameContext(containers, gameEntities);

            cycleController = new GameCycleController(context, containers, gameEntities);
            cycleController.Setup();
        }

        private void Start()
        {
            cycleController.StartGame();
        }

        private void Update()
        {
            cycleController.UpdateGame();
        }

        private void FixedUpdate()
        {
            cycleController.FixedUpdateGame();
        }

        private void OnDisable()
        {
            //cycleController.EndGame();
        }
    }

    public interface IGameContainer
    {
        Container[] GetContainers();
        IGameEntity[] GetGameEntities();
        List<IGameEntity> AddGameEntities();
    }

    public interface IGameEntity
    {
        void Init();

        void Setup(GameContext context);

        void StartGame();

        void UpdateGame();

        void FixedUpdateGame();

        void EndGame();
    }
}
