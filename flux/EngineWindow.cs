using Microsoft.VisualBasic;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using Flux.Types;
using OpenTK.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Un4seen.Bass;
using Flux.Core.Rendering;

namespace Flux.Core
{
    public class EngineWindow : GameWindow
    {
        public TestScene tstScn;

        public EngineWindow(NativeWindowSettings windowSettingsNative, GameWindowSettings windowSettingsGame) 
               :base(windowSettingsGame, windowSettingsNative)
        {
            Debug.LogEngine("Engine window constructed...");
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            RenderManager.Render();
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if(KeyboardState.IsKeyPressed(Keys.Space))
            {
                AudioSystemTMP.PlaySound((IntPtr)0);
            }
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            tstScn = new TestScene();
            tstScn.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            Debug.LogEngine("Engine initialized");
         }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}