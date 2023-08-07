using MathClasses;

namespace Tank_Game.Particles
{
    internal class MuzzleFlash_Particle : Particle
    {
        private string[] AnimationFrameAddresses = new string[] { @"../../Sprites/Smoke/smokeGrey0Resized.png", @"../../Sprites/Smoke/smokeGrey1Resized.png" };
        public MuzzleFlash_Particle(ref Transform SpawnPoint)
        {
            Sprite.PivotSpawnX = 0.3f; Sprite.PivotSpawnY = 0.1f;  // Sets where the Pivot will Spawn 
            MaxLifeTime = 0.3f;
            // === Sprites ===
            Sprite.Load(@"../../Sprites/Smoke/smokeGrey0Resized.png"); // Loads Sprite for the Tank Body from SpriteAddress

            // Animation
            Sprite.AnimationFrameRate = 0.1f;
            Sprite.LoadAnimation(AnimationFrameAddresses);
            ParticleSetTransform(ref SpawnPoint);
        }

        #region Overrides
        public override void FixedUpdate(ref float DeltaTime)
        {
            base.FixedUpdate(ref DeltaTime);
        }
        public override void LateDraw()
        {
            Sprite.DrawAnimation();
        }
        #endregion
    }
}
