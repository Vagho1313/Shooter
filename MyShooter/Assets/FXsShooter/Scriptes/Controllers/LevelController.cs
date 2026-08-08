using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FXs.Shooter
{
    public class LevelController : ILevelController
    {
        public Level CurrentLevel { get; set; }

        public void Init()
        {
            
        }

        public void Setup(GameContext context)
        {
            //Get level index from saved data controller
        }

        public void StartGame()
        {
            CurrentLevel = Object.Instantiate(Resources.Load<Level>("Levels/Level_" + 1));
        }

        public void UpdateGame()
        {
            
        }

        public void FixedUpdateGame()
        {

        }

        public void EndGame()
        {
            Object.Destroy(CurrentLevel.gameObject);
            Resources.UnloadUnusedAssets();
        }
    }
}
