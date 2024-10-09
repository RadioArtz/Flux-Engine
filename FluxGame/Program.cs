using Flux;

public static class FluxGame
{
   public static void Main()
    {
        Engine.Main(null);
        Engine.window.SetActiveScene(new TestScene());
    }
}