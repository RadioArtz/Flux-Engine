using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Flux.Constants;
using OpenTK.Windowing.Common;
using Flux.Core;

namespace Flux
{
    public static class Program
    {
        private static NativeWindowSettings nativeSettings = NativeWindowSettings.Default;
        private static GameWindowSettings gameSettings = GameWindowSettings.Default;
        public static EngineWindow? window { get; private set; }
        static void Main(string[] args)
        {
            InitEngine();   
        }
        static void InitEngine()
        {
            nativeSettings = NativeWindowSettings.Default;
            nativeSettings.ClientSize = new Vector2i(RenderingConfig.RES_X, RenderingConfig.RES_Y);
            nativeSettings.Vsync = (VSyncMode)RenderingConfig.VSYNC_MODE;
            nativeSettings.WindowState = (WindowState)RenderingConfig.FULLSCREEN_MODE;
            nativeSettings.NumberOfSamples = RenderingConfig.MSAA_SAMPLES;
            nativeSettings.Title = "Flux2 Engine";
            gameSettings.UpdateFrequency = RenderingConfig.UPDATE_MAX;

            window = new EngineWindow(nativeSettings, gameSettings);
            window.Run();
        }   
    }
}