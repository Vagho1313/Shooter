using System;
using UnityEngine;

namespace FXs.Shooter
{
    public class InputController : MonoGameEntity
    {
        public event Action<float, Vector2> OnAim;
        public event Action OnShoot;

        private bool isActive;

        public override void Init()
        {

        }

        public override void Setup(GameContext context)
        {

        }

        public override void StartGame()
        {
            isActive = true;
        }

        public override void UpdateGame()
        {
            if(!isActive)
            {
                return;
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 aim = new Vector2(horizontal, vertical);
            
            if (aim != Vector2.zero)
            {
                OnAim?.Invoke(Time.deltaTime, aim);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShoot?.Invoke();
            }
        }

        public override void FixedUpdateGame()
        {

        }

        public override void EndGame()
        {
            isActive = false;
        }  
    }
}
