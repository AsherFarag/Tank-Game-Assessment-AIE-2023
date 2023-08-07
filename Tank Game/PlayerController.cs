using Raylib_cs;
using MathClasses;
using Tank_Game.Particles;

namespace Tank_Game
{
    internal class PlayerController : Component
    {
        #region Variables
        private TankBody AttachedTank; // The Tank this Component is Attached to
        private Transform TankTransform; // The Transform of the Tank
        private TankTurret Turret; // The Turret that is Attached to the Tank
        private Transform TurretTransform; // The Transform of the Turret
        private Vector3 MovementSpeed;
        private float TankRotationSpeed;
        private float TurretRotationSpeed;

        // === Particle ===
        private float m_TimeSinceLastParticleSpawn = 0.0f;
        #endregion

        public PlayerController(TankBody Tank)
        {
            // === Tank Body ===
            AttachedObject    = Tank;
            AttachedTank      = Tank;
            TankTransform     = AttachedTank.Transform;
            MovementSpeed     = AttachedTank.MovementSpeed;
            TankRotationSpeed = AttachedTank.RotationSpeed;
            AttachedObject.AddComponent(this);


            // === Turret ===
            Turret = AttachedTank.Turret;
            TurretTransform = Turret.Transform;
            TurretRotationSpeed = Turret.RotationSpeed;
        }

        #region Particle Handling
        private void SpawnParticle()
        {
            if (m_TimeSinceLastParticleSpawn >= 0.625f)
            {
                new TankTread_Particle(ref AttachedObject.Transform);
                m_TimeSinceLastParticleSpawn = 0;
            }
        }
        #endregion

        #region Overrides
        public override void FixedUpdate(ref float DeltaTime)
        {
            // === Particle Handling ===
            m_TimeSinceLastParticleSpawn += DeltaTime;

            #region Input Handling

            #region 1D Movement
            // === Sounds ===
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_S)) // Sound Start
            {
                EngineAPI.Instance.AudioManagerInstance.PlaySound(false, 1);
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_W) || Raylib.IsKeyReleased(KeyboardKey.KEY_S)) // Sound Stop
            {
                EngineAPI.Instance.AudioManagerInstance.PlaySound(true, 1);
            }

            // === Movement ===
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) // Move Tank Forwards
            {
                SpawnParticle();
                
                Matrix3 Rotation = Matrix3.CreateRotationZ(AttachedObject.Transform.LocalRotation);
                TankTransform.LocalPosition += Rotation * MovementSpeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) // Move Tank Backwards
            {
                SpawnParticle();
                Matrix3 Rotation = Matrix3.CreateRotationZ(AttachedObject.Transform.LocalRotation);
                TankTransform.LocalPosition -= Rotation * MovementSpeed;
            }


            // === Rotation ===
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) // Rotate Tank Counter-ClockWise
            {
                SpawnParticle();
                TankTransform.LocalRotation += TankRotationSpeed * DeltaTime;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) // Rotate Tank ClockWise
            {
                SpawnParticle();
                TankTransform.LocalRotation -= TankRotationSpeed * DeltaTime;
            }
            #endregion

            #region Turret
            // === Sounds ===
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_Q) || Raylib.IsKeyPressed(KeyboardKey.KEY_E)) // Sound Start
            {
                EngineAPI.Instance.AudioManagerInstance.PlaySound(false, 3);
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_Q) || Raylib.IsKeyReleased(KeyboardKey.KEY_E)) // Sound Stop
            {
                EngineAPI.Instance.AudioManagerInstance.PlaySound(true, 3);
            }

            // === Turret Rotation ===
            if (Raylib.IsKeyDown(KeyboardKey.KEY_Q)) // Rotate Turret Counter-ClockWise
            {
                TurretTransform.LocalRotation += TurretRotationSpeed * DeltaTime;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_E)) // Rotate Turret ClockWise 
            {
                TurretTransform.LocalRotation -= TurretRotationSpeed * DeltaTime;
            }

            // === Turret Firing ===

            if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE)) // Rotate Turret ClockWise 
            {
                EngineAPI.Instance.AudioManagerInstance.PlaySound(false, 2);
                Turret.Fire();
            }
            #endregion
            #endregion
        }
        #endregion
    }
}
