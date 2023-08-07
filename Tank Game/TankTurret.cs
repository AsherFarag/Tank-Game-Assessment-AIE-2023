using System;
using System.Net.Http.Headers;
using MathClasses;
using Raylib_cs;
using Tank_Game.Particles;

namespace Tank_Game
{
    internal class TankTurret : GameObject
    {
        #region Variables
        public GameObject FirePoint;
        private SpriteRenderer Sprite;
        private string SpriteAddress = @"../../Sprites/Tank/barrelBlue_outline.png";

        public float RotationSpeed = 1; // The Speed the Turret will Rotate at
        #endregion

        public TankTurret(GameObject ParentObject)
        {
            // === Parent Handling
            Parent = ParentObject;
            Parent.AddChild(this);

            // === Fire Point Handling
            FirePoint = new GameObject();
            AddChild(FirePoint);
            FirePoint.Parent = this;
            FirePoint.Transform.LocalPosition += new Vector3(10, 40, 0); // Sets the Firepoint to the End of the Turret

            // === Sprite Loading ===
            Sprite = new SpriteRenderer(this); // Sets the Sprite Renderer
            Sprite.PivotSpawnX = 0.8f; Sprite.PivotSpawnY = 0.5f;  // Sets where the Pivot will Spawn 
            Sprite.Load(SpriteAddress); // Loads Sprite for the Tank Turret from SpriteAddress
        }

        public void Fire()
        {
            new MuzzleFlash_Particle( ref FirePoint.Transform);
            new Projectile(this, ref FirePoint.Transform);
        }

        #region Overrides
        public override void Draw()
        {
            Sprite.Draw();
        }
        public override void Tick(ref float DeltaTime)
        {
            base.Tick(ref DeltaTime);
        }
        #endregion
    }
}
