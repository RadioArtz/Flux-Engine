using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Flux.Core.Rendering;
using OpenTK.Mathematics;
using System.Diagnostics;
using Flux.Types;

namespace Flux.Core
{
    public class EngineWindow : GameWindow
    {
        private FScene? _activeScene;
        private Stopwatch _deltaCalc = new Stopwatch();
        private float _deltatime = 0.0f;
        public Action? OnInitializedCallback;

        public EngineWindow(NativeWindowSettings windowSettingsNative, GameWindowSettings windowSettingsGame) 
               :base(windowSettingsGame, windowSettingsNative)
        {
            Debug.LogEngine("Engine window constructed...");
        }

        public FScene? GetActiveScene() { return _activeScene; }

        public void SetActiveScene(FScene scene)
        {
            _activeScene = scene;
            _activeScene.OnLoad();
            return;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            _activeScene?.OnTick(_deltatime);
            _activeScene?.TickActors(_deltatime);
            RenderManager.Render();
            SwapBuffers();
            _deltatime = (float)_deltaCalc.Elapsed.TotalSeconds;
            _deltaCalc.Restart();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
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
            _deltaCalc.Start();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            Debug.LogEngine("Engine initialized");
            OnInitializedCallback!.Invoke();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            RenderManager._projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(80.0f), (float)e.Width / (float)e.Height, 0.1f, 4096);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}