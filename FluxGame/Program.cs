using Flux;
using Flux.Types;

namespace FluxGame
{
    public static class Game
    {
        public static void Main(string[] args)
        {
            Engine.Main(args, () => GameStart()); ;
        }

        public static void GameStart()
        {
            if (Engine.startArgs == null)
                Engine.window.SetActiveScene(new ManiaTestScene());
            else if (Engine.startArgs[0] == "voxel")
                Engine.window.SetActiveScene(new VoxelTestScene());
            else if(Engine.startArgs[0] == "mania")
                Engine.window.SetActiveScene(new ManiaTestScene());
        }
    }
}