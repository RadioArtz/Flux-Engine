using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Flux.Core.Rendering;
using OpenTK.Mathematics;
using WinRT;
using System.Diagnostics;

namespace Flux.Core
{
    public class EngineWindow : GameWindow
    {
        public TestScene tstScn;
        private Stopwatch _deltaCalc = new Stopwatch();
        private float _deltatime = 0.0f;
        public EngineWindow(NativeWindowSettings windowSettingsNative, GameWindowSettings windowSettingsGame) 
               :base(windowSettingsGame, windowSettingsNative)
        {
            Debug.LogEngine("Engine window constructed...");
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            tstScn.OnTick(_deltatime);
            RenderManager.Render();
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            _deltatime = (float)_deltaCalc.Elapsed.TotalSeconds;
            _deltaCalc.Restart();
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if(KeyboardState.IsKeyPressed(Keys.Space))
            {
                AudioSystemTMP.PlaySound((IntPtr)0);
            }
        }
        public void SetShowMouseCursor(CursorState showCursor)
        {
            CursorState = showCursor;
        }
        public void SetCursorGrabbed(CursorState grabCursor)
        {
            CursorState = grabCursor;
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Multisample);

            _deltaCalc.Start();
            tstScn = new TestScene();
            tstScn.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            Debug.LogEngine("Engine initialized");
         }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            RenderManager._projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), (float)e.Width / (float)e.Height, 0.1f, 2048);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}