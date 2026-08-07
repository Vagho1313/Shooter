using System.Collections.Generic;
using UnityEngine;

namespace FXs.Shooter
{
    public class TrajectoryRenderer : MonoGameEntity, ITrajectoryRenderer
    {
        [SerializeField] private LineRenderer trajectoryLineRenderer;

        private ICannonController cannonController;
        private ShooterGameSettings settings;
        private IUIController uIController;

        private bool started;
        private List<Vector3> points;

        public override void Init()
        {

        }

        public override void Setup(GameContext context)
        {
            if (context.TryGetEntity(out ICannonController cannonController))
            {
                this.cannonController = cannonController;
            }

            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                this.settings = settings;
            }

            if (context.TryGetEntity(out IUIController uIController))
            {
                this.uIController = uIController;
            }
        }

        public override void StartGame()
        {
            points = new List<Vector3>();
            started = true;
        }

        public override void UpdateGame()
        {

        }

        public override void FixedUpdateGame()
        {
            if (!started)
            {
                return;
            }
            CalculateTrajectory(Time.fixedDeltaTime, cannonController.Position, uIController.ShotPower * cannonController.Direction);
            trajectoryLineRenderer.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                trajectoryLineRenderer.SetPosition(i, points[i]);
            }
        }

        public override void EndGame()
        {
            started = false;
        }

        private void CalculateTrajectory(float step,
            Vector3 startPosition,
            Vector3 startVelocity)
        {
            points.Clear();

            for (float t = 0f; ; t += step)
            {
                Vector3 position = startPosition + startVelocity * t;
                position.y += 0.5f * settings.Gravity.y * t * t;

                if (position.y <= 0f)
                {
                    position.y = 0f;
                    points.Add(position);
                    break;
                }

                points.Add(position);
            }
        }
    }
}
