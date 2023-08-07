namespace Tank_Game
{
    internal class Particle : GameObject
    {
        public SpriteRenderer Sprite;
        private float TimeAlive = 0f;  // Counts how long it has been Alive for Using FrameTime
        public float MaxLifeTime;

        public Particle()
        { 
            // === Initialising Order ===
            Sprite = new SpriteRenderer(this);                     // Sets the Sprite Renderer
            Sprite.PivotSpawnX = 0.5f; Sprite.PivotSpawnY = 0.5f;  // Sets where the Pivot will Spawn 
        }

        public override void Tick(ref float DeltaTime)
        {
            base.Tick(ref DeltaTime);
        }
        public override void FixedUpdate(ref float DeltaTime)
        {
            base.FixedUpdate(ref DeltaTime);
            TimeAlive += DeltaTime;
            if (TimeAlive > MaxLifeTime) // If the bullet has been Alive for longer than the MaxLifeTime,
                DestroyAndAllComponents();
        }
        public override void Draw()
        {
            Sprite.Draw();
        }

        #region Particle Handling
        public void ParticleSetTransform(ref Transform Transform)
        {
            this.Transform.LocalPosition = Transform.GlobalPosition;
            this.Transform.LocalRotation = -Transform.GlobalRotation;
            this.Transform.LocalScale    = Transform.GlobalScale;
        }
        #endregion
    }
}
