using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Constants
{
    public static class Rendering
    {
        public static int RES_X { get; private set; } = 1280;
        public static int RES_Y { get; private set; } = 720;
        /// <summary>
        /// Maximum Rate at which the game may render.
        /// </summary>
        public static int FPS_MAX { get; private set; } = 60;
        /// <summary>
        /// Maximum Rate at which the game may update logic and phyics.
        /// </summary>
        public static int UPDATE_MAX { get; private set; } = 30;
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
        public static int MSAA_SAMPLES { get; private set; } = 16;
        public static bool USE_MULTITHREADDING { get; private set; } = true;
    }
}
