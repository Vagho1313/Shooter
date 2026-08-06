using System;
using UnityEngine;

namespace FXs.Shooter
{
    public class UIController : MonoGameEntity
    {
        [SerializeField] private PowerUI powerUI;

        public event Action<int> OnPowerChanged;

        public int ShotPower => powerUI.Value;

        public override void Init()
        {
            powerUI.Init();
        }

        public override void Setup(GameContext context)
        {
            if(context.TryGetContainer(out ShooterGameSettings settings))
            {
                powerUI.Setup(settings.MinPower, settings.MinPower, settings.MaxPower);
            }
        }

        public override void StartGame()
        {
            powerUI.OnSliderChanged += PowerChanged;
        }

        public override void UpdateGame()
        {

        }

        public override void FixedUpdateGame()
        {

        }

        public override void EndGame()
        {
            powerUI.OnSliderChanged -= PowerChanged;
        }

        private void PowerChanged(int power)
        {
            OnPowerChanged?.Invoke(power);
        }
    }
}
