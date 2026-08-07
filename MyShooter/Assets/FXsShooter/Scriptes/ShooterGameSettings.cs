using System;
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

        [Space(10), Header("Camera Control")]
        [SerializeField] private float shakeAmplitude;
        [SerializeField] private float shakeAttenuation;
        [SerializeField] private AnimationCurve shakeCurve;

        public int MinPower => minPower;
        public int MaxPower => maxPower;

        public int MaxHorizontalAngle => maxHorizontalAngle;
        public int MaxVerticalAngle => maxVerticalAngle;
        public float HorizontalSpeed => horizontalSpeed;
        public float VerticalSpeed => verticalSpeed;

        public float ShakeAmplitude => shakeAmplitude;
        public float ShakeAttenuation => shakeAttenuation;
        public Func<float, float> ShakeCurve => (float power) => shakeCurve.Evaluate(power);

        public override void Init()
        {
            
        }
    }
}
