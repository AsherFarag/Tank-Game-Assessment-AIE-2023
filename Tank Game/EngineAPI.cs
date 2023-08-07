using MathClasses;
using Raylib_cs;

namespace Tank_Game
{
    internal class EngineAPI
    {
        public static EngineAPI Instance;
        public AudioManager AudioManagerInstance = new AudioManager();

        public string ApplicationName { get; private set; } = "Tank Game";
        public int TargetFrameRate { get; private set; } = 60;
        public int WindowWidth { get; private set; } = 800;
        public int WindowHeight { get; private set; } = 600;
        public Color ClearFlag { get; private set; } = Color.WHITE;
        public float FixedDeltaTime { get; private set; }
        public float DeltaTime { get; private set; }
        public float CurrentFixedCount { get; private set; } = 0.0f;

        #region Game States
        public EngineAPI ()
        {
            Instance = this;
        }
        public void Start()
        {
            Raylib.SetTargetFPS(TargetFrameRate);
            Raylib.InitWindow(WindowWidth, WindowHeight, ApplicationName);
        }
        public void Start(int WindowWidth, int WindowHeight, Color ClearFlag)
        {
            this.WindowWidth = WindowWidth;
            this.WindowHeight = WindowHeight;
            this.ClearFlag = ClearFlag;
            Start();
            new TankBody();
        }

        public void Tick()
        {
            while (!Raylib.WindowShouldClose())
            {
                // === Delta Time ===
                float DeltaTime = Raylib.GetFrameTime();

                UpdateGameObjects(ref DeltaTime);
                Raylib.BeginDrawing();
                Raylib.ClearBackground(ClearFlag);
 
                DrawGameObjects();
                LateDrawGameObjects();
                
                Raylib.EndDrawing();
            }
        }

        public void End()
        {
            Raylib.CloseWindow();
        }
        #endregion

        #region GameObject Handling

        private List<GameObject> GameObjects = new List<GameObject>();
        public void RegisterGameObject(GameObject GameObj)
        {
            GameObjects.Add(GameObj);
        }
        public void UnregisterGameObject(GameObject GameObj)
        {
            GameObjects.Remove(GameObj);
        }
        public void DrawGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].Parent == null)
                {
                    GameObjects[i].Draw();
                }
            }
        }
        public void LateDrawGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].Parent == null)
                {
                    GameObjects[i].LateDraw();
                }
            }
        }
        public void UpdateGameObjects(ref float DeltaTime)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Tick(ref DeltaTime);
            }
        }
        #endregion
        #region Physics Handling
        private List<Component> PhysicsComponents = new List<Component>(); // List of All Physics-Related Components that are Updated in FixedUpdate
       
        //public void FixedUpdate(float DeltaTime)
        //{
        //    foreach (Component PhysicsComponent in PhysicsComponents)
        //        PhysicsComponent.FixedUpdate(DeltaTime);
        //}
        //public void AddPhysicsComponent(Component PhysicsComponent) { PhysicsComponents.Add(PhysicsComponent); }
        //public void RemovePhysicsComponent(Component PhysicsComponent) { PhysicsComponents.Remove(PhysicsComponent); }
        #endregion
    }
}
