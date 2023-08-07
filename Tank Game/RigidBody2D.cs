using MathClasses;

namespace Tank_Game
{
    internal class RigidBody2D : Component
    {
        #region Variables
        public Transform AttachedObjectTransform;
        public float Mass { get; private set; }
        public float Speed { get; private set; }
        public Vector3 Gravity { get; private set; } = new Vector3(0, -9.81f, 0);
        public Vector3 Velocity { get; private set; } = new Vector3(); // The Change of Position Per Second 
        public Vector3 Acceleration { get; private set; } // The Change of Velocity Per Second 
        public Vector3 Drag { get; private set; } // A constant DeAcceleration
        #endregion

        public RigidBody2D (GameObject Object)
        {
            // === Attached Object Handling ===
            AttachedObject = Object;
            AttachedObject.AddComponent(this);
            AttachedObjectTransform = AttachedObject.Transform;
        }

        #region Velocity Handling
        public void AddSpeed(float Speed)
        {
            this.Speed += Speed;
        }
        public void AddVelocity(Vector3 Velocity) // Adds a Velocity to the Current Velocity
        {
            this.Velocity += Velocity;
        }
        public void SetVelocity(Vector3 Velocity) // Sets a Velocity to the Current Velocity
        {
            this.Velocity = Velocity;
        }
        #endregion

        #region Overrides
        public override void Update()
        {
            // Update Component

        }
        public override void FixedUpdate(ref float DeltaTime)
        {
            // === Normal Handling ===
            Matrix3 Rotation = Matrix3.CreateRotationZ(-AttachedObject.Transform.GlobalRotation + (float)(Math.PI / 2f));
            Vector3 Normal = new Vector3(Rotation.m00, Rotation.m01, 0);
            Normal.Normalize();

            Velocity += Acceleration * DeltaTime;

            AttachedObjectTransform.LocalPosition += Normal * Speed * DeltaTime;
        }
        #endregion
    }
}
