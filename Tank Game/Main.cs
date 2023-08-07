using Raylib_cs;

namespace Tank_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EngineAPI Engine = new EngineAPI();
            Engine.Start(800, 600, Color.DARKGREEN);
            Engine.Tick();
            Engine.End();
        }
    }
}
