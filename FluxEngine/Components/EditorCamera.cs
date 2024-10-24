using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using Flux.Core;

namespace Flux.Types
{
    public class EditorCamera : BaseComponent
    {
        private MouseState? Mouse;
        private KeyboardState? Keyboard;
        private EngineWindow _window;
        public Vector2 lastPos { get; private set; }

        public EditorCamera(EngineWindow window)
        {
            _window = window;
        }
        public override void OnTick(float delta)
        {
            Mouse = _window.MouseState;
            Keyboard = _window.KeyboardState;
            TransformComponent trans = ParentObject.TransformComponent;
            float movespeed = delta*24;
            float sensitivity = .1f;

            float deltaX = Mouse.X - lastPos.X;
            float deltaY = Mouse.Y - lastPos.Y;
            lastPos = new Vector2(Mouse.X, Mouse.Y);
            if (!Mouse.IsButtonDown(MouseButton.Right))
            {
                _window.SetCursorGrabbed(CursorState.Normal);
                return;
            }
            _window.SetCursorGrabbed(CursorState.Grabbed);
            trans.transform.Rotation += new Vector3(-deltaY, deltaX, 0)*sensitivity;
            trans.transform.Rotation.X = MathHelper.Clamp(trans.transform.Rotation.X,-89.9f , 89.9f);

            if (Keyboard.IsKeyDown(Keys.LeftShift))
            {
                movespeed = delta * 128;
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                trans.transform.Location += MathExt.GetRightVector(trans.transform.Rotation) * movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                trans.transform.Location += MathExt.GetRightVector(trans.transform.Rotation) * -movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                trans.transform.Location += MathExt.GetForwardVector(trans.transform.Rotation)*-movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.W))
            {
                trans.transform.Location += MathExt.GetForwardVector(trans.transform.Rotation) * movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                trans.transform.Location += MathExt.GetUpVector(trans.transform.Rotation) * movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                trans.transform.Location += MathExt.GetUpVector(trans.transform.Rotation) * -movespeed;
            }
        }
    }
}