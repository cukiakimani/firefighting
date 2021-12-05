using UnityEngine;

namespace SweetLibs
{
    [System.Serializable]
    public class Spring
    {
        public float PercentageDecay = 0.4f;
        public float Frequency = 5f;
        public float TimeDecay = 3f;

        [HideInInspector] public float Position;
        [HideInInspector] public float Velocity;
        [HideInInspector] public float TargetPosition;
        
        public void StartPosition(float position)
        {
            TargetPosition = Position = position;
        }
        
        public void Update(float dt)
        {
            float omega = 2 * Mathf.PI * Frequency;
            float zeta = Mathf.Log(PercentageDecay) / (-omega * TimeDecay);
            Position = Springer.FloatSpring(Position, ref Velocity, TargetPosition, zeta, omega, dt);
        }
    }

    [System.Serializable]
    public class VectorSpring
    {
        public float PercentageDecay = 0.4f;
        public float Frequency = 5f;
        public float TimeDecay = 3f;

        [HideInInspector] public Vector3 Position;
        [HideInInspector] public Vector3 Velocity;
        [HideInInspector] public Vector3 TargetPosition;
        
        public void StartPosition(Vector3 position)
        {
            TargetPosition = Position = position;
        }
        
        public void Update(float dt)
        {
            float omega = 2 * Mathf.PI * Frequency;
            float zeta = Mathf.Log(PercentageDecay) / (-omega * TimeDecay);
            Position = Springer.Vector3Spring(Position, ref Velocity, TargetPosition, zeta, omega, dt);
        }
    }
}