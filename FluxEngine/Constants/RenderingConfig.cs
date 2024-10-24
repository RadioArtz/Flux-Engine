
namespace Flux.Constants
{
    public struct RenderingConfig
    {
        public static int RES_X { get; private set; } = 640;
        public static int RES_Y { get; private set; } = 360;
        /// <summary>
        /// Maximum Rate at which the game may update logic and phyics.
        /// </summary>
        public static int UPDATE_MAX { get; private set; } = 0;
        /// <summary>
        /// 0 = off
        /// 1 = on
        /// 2 = Adaptive
        /// </summary>
        public static int VSYNC_MODE { get; private set; } = 2;
        /// <summary>
        /// 0 = Normal; 1 = Minimized; 2 = Maximized; 3 = Fullscreen;
        /// </summary>
        public static int FULLSCREEN_MODE { get; private set; } = 0;
        public static int MSAA_SAMPLES { get; private set; } = 2;

        public static string SHADER_FALLBACK_FRAG = Path.Combine("Shaders", "fallback.frag");
        public static string SHADER_FALLBACK_VERT = Path.Combine("Shaders", "fallback.vert");
    }
}   