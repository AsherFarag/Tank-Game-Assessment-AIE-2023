using Raylib_cs;
using System.Numerics;

namespace Tank_Game
{
    internal class SpriteRenderer : Component
    {
        #region Variables
        private Texture2D Sprite = new Texture2D();
        public Color Tint { get; private set; } = Color.WHITE; // The Colour the Sprite will be Rendered
        public float PivotSpawnX, PivotSpawnY; // On Load, these Numbers are used to Spawn the Pivot at a Point Set by the Attached Object
        private Vector2 m_Pivot; // Where the Sprite is Connected to and Rotates Around
        public float Width { get => Sprite.width; }
        public float Height { get => Sprite.height; }

        // === Animation ===
        private Texture2D[] AnimationFrames;
        private int m_FrameIndex;
        private float m_FrameTime = 0f;
        public float AnimationFrameRate;
        #endregion

        public SpriteRenderer(GameObject Object) 
        {
            // === Attached Object Handling ===
            AttachedObject = Object; // Initialises the Object as the Attached Object
            AttachedObject.AddComponent(this);
        }

        #region Texture Handling
        public void Load(string SpriteAddress) // Loads sprite onto VRam      // Should only be called on the Initialisation or if there is a Texture Change  
        {
            Image Img = Raylib.LoadImage(SpriteAddress); // Loads Image from the address
            Sprite = Raylib.LoadTextureFromImage(Img);   // Converts the Image to Texture2D
            m_Pivot = new Vector2(Sprite.width * PivotSpawnX, Sprite.height * PivotSpawnY); // Pivot Spawns are Set by the Attached Object
        }
        public void LoadAnimation(string[] SpriteAddresses) // Loads sprite onto VRam      // Should only be called on the Initialisation or if there is a Texture Change  
        {
            AnimationFrames = new Texture2D[SpriteAddresses.Length];

            for (int i = 0;  i < AnimationFrames.Length; i++) 
            {
                Image Img = Raylib.LoadImage(SpriteAddresses[i]); // Loads Image from the address
                AnimationFrames[i] = Raylib.LoadTextureFromImage(Img);   // Converts the Image to Texture2D
                m_Pivot = new Vector2(Sprite.width * PivotSpawnX, Sprite.height * PivotSpawnY); // Pivot Spawns are Set by the Attached Object
            }
        }
        public void UnLoad() // Unloads the Sprite from VRam  
        {
            Raylib.UnloadTexture(Sprite);
        }
        #endregion

        #region Draw Functions
        public void Draw() // Draws the sprite onto the screen using the attached objects transform
        {
            MathClasses.Vector3 Position = AttachedObject.Transform.GlobalPosition; // Uses the Global Position of the Attached Objects Transform
            float Rotation = AttachedObject.Transform.GlobalRotation * (float)(180.0f / Math.PI); // Global Rotation of the Z-Axis on the Attached Objects Transform     // Converted to Degrees

            Raylib.DrawTexturePro(
                Sprite, // The Texture that is being Rendered
                new Rectangle(0, 0, Sprite.width, Sprite.height), // Rectangle set to the Size of the Sprite 
                new Rectangle(Position.x, EngineAPI.Instance.WindowHeight - Position.y, Sprite.width, Sprite.height), // Where the Sprite is Drawn in the World
                m_Pivot, // A point where the Sprite is Drawn and Rotated around
                Rotation, // The Global Rotation of the Sprite
                Tint); // the Colour of the Sprite
        }

        public void DrawAnimation()
        {
            Draw();
            m_FrameTime += Raylib.GetFrameTime();
            if (m_FrameTime > AnimationFrameRate && m_FrameIndex < AnimationFrames.Length)
            {
                Sprite = AnimationFrames[m_FrameIndex];
                m_FrameIndex++;
                m_FrameTime = 0;
            }
        }
        #endregion

        #region Overrides
        public override void Update()
        {
            // Update Component
        }
        public override void Destroy()
        {
            base.Destroy();
            UnLoad();
        }
        #endregion
    }
}
