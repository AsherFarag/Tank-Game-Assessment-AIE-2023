namespace Tank_Game.Particles
{
    internal class TankTread_Particle : Particle
    {

        public TankTread_Particle(ref Transform SpawnPoint)
        {
            MaxLifeTime = 10f;
            Sprite.Load(@"../../Sprites/Tank/tracksLarge.png"); // Loads Sprite for the Tank Body from SpriteAddress
            ParticleSetTransform(ref SpawnPoint);
        }

        public override void FixedUpdate(ref float DeltaTime)
        {
            base.FixedUpdate(ref DeltaTime);
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
