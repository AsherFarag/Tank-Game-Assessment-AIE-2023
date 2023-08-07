using MathClasses;

namespace Tank_Game
{
    internal class TankBody : GameObject
    {
        #region Variables
        // === Components ===
        public SpriteRenderer Sprite;
        public PlayerController PlayerController;

        private string SpriteAddress = @"../../Sprites/Tank/tankBlue_outline.png"; // Address for where the Tank Body Sprite is Stored

        public TankTurret Turret;

        // === Stats ===
        public float Health { get; private set; } = 5;
        public Vector3 MovementSpeed { get; private set; } = new Vector3(0, 3, 0);
        public float RotationSpeed { get; private set; } = 1;
        #endregion

        public TankBody()
        {
            // === Initialising Order ===
            Sprite = new SpriteRenderer(this);                     // Sets the Sprite Renderer
            Sprite.PivotSpawnX = 0.5f; Sprite.PivotSpawnY = 0.5f;  // Sets where the Pivot will Spawn 
            Sprite.Load(SpriteAddress);                            // Loads Sprite for the Tank Body from SpriteAddress
            Turret = new TankTurret(this);                         // Constructs the Turret with the Tank as the Parent
            PlayerController = new PlayerController(this);         // Intialises the Player Controller

            Transform.LocalPosition = new Vector3(0.5f * EngineAPI.Instance.WindowWidth, 0.5f * EngineAPI.Instance.WindowHeight, 0); // Spawns the Tank at the Center of the Screen
        }

        #region Overrides
        public override void LateDraw()
        {
            Sprite.Draw();
            Turret.Draw();
        }
        public override void Tick(ref float DeltaTime)
        {
            base.Tick(ref DeltaTime);
        }
        #endregion
    }
}
