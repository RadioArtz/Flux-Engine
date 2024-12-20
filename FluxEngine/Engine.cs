﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Flux.Constants;
using OpenTK.Windowing.Common;
using Flux.Core;
using Flux.Types;
using Un4seen.Bass;

namespace Flux
{
    public static class Engine
    {
        public static string[] startArgs = Array.Empty<string>();
        private static NativeWindowSettings nativeSettings = NativeWindowSettings.Default;
        private static GameWindowSettings gameSettings = GameWindowSettings.Default;
        public static EngineWindow? window { get; private set; }
        public static AudioListenerComponent? activeAudioListener;

        public static void Main(string[] args, Action onInitialized)
        {
            startArgs = args;
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
            // shaders use gl 4.1 core, so force a minimum of 4.1
            // this is also the last version that will run on MacOS
            nativeSettings.APIVersion = new Version(4, 1);
            // GLES is nice, but this engine is core gl only right now
            nativeSettings.API = ContextAPI.OpenGL;
            // Force OpenGL Core
            nativeSettings.Profile = ContextProfile.Core;
            gameSettings.UpdateFrequency = RenderingConfig.UPDATE_MAX;
            InitBass();
            window = new EngineWindow(nativeSettings, gameSettings);

            window.OnInitializedCallback = onInitialized;

            window.Run();
            Debug.Log("Test");
        }

        static void InitBass()
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                Debug.LogError("BASS initialization failed with error: " + Bass.BASS_ErrorGetCode());
                return;
            }
        }
    }
}
