using Microsoft.VisualBasic;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using Flux.Types;
using OpenTK.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Flux
{
    public class EngineWindow : GameWindow
    {
        #region penis
        float[] vertices = {
     0.5f,  0.5f, 0.0f,  // top right
     0.5f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left
};
        uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
};
        #endregion

        StaticMeshAsset triangle;
        Actor TriangleActor;

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
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            triangle = new StaticMeshAsset(vertices, indices);
            TriangleActor = new QuadActor();
            TriangleActor.AddComponent(new StaticMeshComponent(triangle));
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            Debug.LogEngine("schenschin loaded");
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