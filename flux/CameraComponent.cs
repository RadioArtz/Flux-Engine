using OpenTK.Mathematics;
using Flux.Core;

namespace Flux.Types
{
    public class CameraComponent : BaseComponent
    {
        public Matrix4 view { get; private set; }
        public CameraComponent()
        {
        }
        
        public void UpdateViewMatrix()
        {
            Transform trans = ParentObject.TransformComponent.transform;
            Vector3 rotDegrees = trans.Rotation;
            view = Matrix4.LookAt(trans.Location, trans.Location + rotDegrees.GetForwardVector(), MathExt.GetUpVector(rotDegrees));
        }
    }
}