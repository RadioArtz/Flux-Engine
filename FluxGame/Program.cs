using Flux;
using System.Diagnostics;

namespace FluxGame
{
    public static class Game
    {
        public static void Main()
        {
            Flux.Core.Debug.Log("Waiting for window creation");
            Engine.Main(null, () => GameStart()); ;
        }
        public static void GameStart()
        {
            Engine.window.SetActiveScene(new TestScene());
        }

    }
}
