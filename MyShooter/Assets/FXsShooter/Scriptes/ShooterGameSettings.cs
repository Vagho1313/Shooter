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

        [Space(10), Header("Cannon Control")]
        [SerializeField] private int maxHorizontalAngle;
        [SerializeField] private int maxVerticalAngle;
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private float verticalSpeed;

        public int MinPower => minPower;
        public int MaxPower => maxPower;

        public int MaxHorizontalAngle => maxHorizontalAngle;
        public int MaxVerticalAngle => maxVerticalAngle;
        public float HorizontalSpeed => horizontalSpeed;
        public float VerticalSpeed => verticalSpeed;

        public override void Init()
        {
            
        }
    }
}
