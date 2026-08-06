using UnityEngine;

namespace FXs.Shooter
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "FXs/GameSettings")]
    public class ShooterGameSettings : Container
    {
        //GameSettings need to be separated but right now it's ok :)

        [Space(10), Header("Shot Power")]
        [SerializeField] private int minPower;
        [SerializeField] private int maxPower;

        public int MinPower => minPower;
        public int MaxPower => maxPower;

        public override void Init()
        {
            
        }
    }
}
