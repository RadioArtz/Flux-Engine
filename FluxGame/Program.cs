using Flux;

namespace FluxGame
{
    public static class Game
    {
        public static void Main()
        {
            Engine.Main(null, () => GameStart()); ;
        }
        public static void GameStart()
        {
            Engine.window.SetActiveScene(new VoxelTestScene());
        }
    }
}
