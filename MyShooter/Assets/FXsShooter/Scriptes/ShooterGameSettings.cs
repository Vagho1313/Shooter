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

        [SerializeField] private float cannonShakeAmplitude;
        [SerializeField] private float cannonShakeAttenuation;
        [SerializeField] private AnimationCurve cannonShakeCurve;

        [Space(10), Header("Camera Control")]
        [SerializeField] private float cameraShakeAmplitude;
        [SerializeField] private float cameraShakeAttenuation;
        [SerializeField] private AnimationCurve cameraShakeCurve;

        [Space(10), Header("Physics")]
        [SerializeField] private float minSpeed;
        [SerializeField] private Vector3 gravity;
        [SerializeField] private Projectile projectilePrefab;

        public int MinPower => minPower;
        public int MaxPower => maxPower;

        public int MaxHorizontalAngle => maxHorizontalAngle;
        public int MaxVerticalAngle => maxVerticalAngle;
        public float HorizontalSpeed => horizontalSpeed;
        public float VerticalSpeed => verticalSpeed;

        public float CannonShakeAmplitude => cannonShakeAmplitude;
        public float CannonShakeAttenuation => cannonShakeAttenuation;
        public Func<float, float> CannonShakeCurve => (float power) => cannonShakeCurve.Evaluate(power);

        public float CameraShakeAmplitude => cameraShakeAmplitude;
        public float CameraShakeAttenuation => cameraShakeAttenuation;
        public Func<float, float> CameraShakeCurve => (float power) => cameraShakeCurve.Evaluate(power);

        public float MinSpeed => minSpeed;
        public Vector3 Gravity => gravity;

        public ObjectPool<Projectile> CreateProjectilePool()
        {
            return new ObjectPool<Projectile>(projectilePrefab);
        }

        public override void Init()
        {
            
        }
    }
}
