using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Flux.Constants;
using OpenTK.Windowing.Common;
using Flux.Core;
using FluxEngine.Types;

namespace Flux
{
    public static class Engine
    {
        private static NativeWindowSettings nativeSettings = NativeWindowSettings.Default;
        private static GameWindowSettings gameSettings = GameWindowSettings.Default;
        public static EngineWindow? window { get; private set; }
        public static AudioListenerComponent? activeAudioListener;

        public static void Main(string[] args, Action onInitialized)
        {
            InitEngine(onInitialized);
        }

        static void InitEngine(Action onInitialized)
        {
            nativeSettings = NativeWindowSettings.Default;
            nativeSettings.ClientSize = new Vector2i(RenderingConfig.RES_X, RenderingConfig.RES_Y);
            nativeSettings.Vsync = (VSyncMode)RenderingConfig.VSYNC_MODE;
            nativeSettings.WindowState = (WindowState)RenderingConfig.FULLSCREEN_MODE;
            nativeSettings.NumberOfSamples = RenderingConfig.MSAA_SAMPLES;
            nativeSettings.Title = "Flux Engine";
            gameSettings.UpdateFrequency = RenderingConfig.UPDATE_MAX;

            window = new EngineWindow(nativeSettings, gameSettings);

            window.OnInitializedCallback = onInitialized;

            window.Run();
            Debug.Log("Test");
        }
    }
}
