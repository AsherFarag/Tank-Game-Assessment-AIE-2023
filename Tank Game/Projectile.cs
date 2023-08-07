namespace Tank_Game
{
    internal class Projectile : GameObject
    {
        private TankTurret Owner; // The Player that Fired the Bullet

        private RigidBody2D RigidBody2D;

        // === Sprite Renderer ===
        private string SpriteAddress = @"../../Sprites/Bullets/bulletBeigeSilver_outline.png";
        private SpriteRenderer Sprite;

        private float Damage = 5f;
        private float Speed = -800;
        private float TimeAlive = 0f;
        private float MaxLifeTime = 15f; // How many seconds the bullet will live for until despawning

        public Projectile(GameObject Owner, ref Transform FirePoint)
        {
            // === Transform Handling ===
            SetTransform(ref FirePoint);

            // === RigidBody2D Handling ===
            RigidBody2D = new RigidBody2D(this);
            RigidBody2D.AddSpeed(Speed);

            // === Sprite Handling ===
            Sprite = new SpriteRenderer(this);
            Sprite.Load(SpriteAddress);


        }
        
        private void SetTransform(ref Transform FirePoint)
        {
            Transform.LocalPosition = FirePoint.GlobalPosition;
            Transform.LocalRotation = -FirePoint.GlobalRotation;
            Transform.LocalScale    = FirePoint.GlobalScale;
        }
        public override void Tick(ref float DeltaTime)
        {
            base.Tick(ref DeltaTime);

            TimeAlive += DeltaTime; // Counts how long it has been Alive for Using FrameTime
            if (TimeAlive > MaxLifeTime || OutsideOfBounds()) // If the bullet has been Alive for longer than the MaxLifeTime Or is Outside of Bounds
                DestroyAndAllComponents();

        }
        public override void Draw()
        {
            // Draw Bullet
            Sprite.Draw();
        }

        private bool OutsideOfBounds()
        {
            float WindowWidth  = EngineAPI.Instance.WindowWidth;
            float WindowHeight = EngineAPI.Instance.WindowHeight;
            // Returns true if the Projectile is Outside of the Screen Space
            return Transform.GlobalPosition.x <= 0 || Transform.GlobalPosition.x >= WindowWidth || Transform.GlobalPosition.y <= 0 || Transform.GlobalPosition.y >= WindowHeight; 
        }
    }
}
