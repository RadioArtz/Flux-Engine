using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using Flux.Constants;
using OpenTK.Windowing.Common;
using Flux.Types;

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
            nativeSettings.Size = new Vector2i(Rendering.RES_X, Rendering.RES_Y);
            nativeSettings.Vsync = (VSyncMode)Rendering.VSYNC_MODE;
            nativeSettings.WindowState = (WindowState)Rendering.FULLSCREEN_MODE;
            nativeSettings.NumberOfSamples = Rendering.MSAA_SAMPLES;
            nativeSettings.Title = "Flux2 Engine";
            gameSettings.UpdateFrequency = Rendering.UPDATE_MAX;

            window = new EngineWindow(nativeSettings, gameSettings);
            window.Run();
        }   
    }
}