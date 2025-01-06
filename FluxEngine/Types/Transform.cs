using OpenTK.Mathematics;

namespace Flux.Types
{
    public struct Transform
    {
        public Vector3 Location;
        public Vector3 Rotation;
        public Vector3 Scale;
        public Transform(Vector3 _Location, Vector3 _Rotation, Vector3 _Scale)
        {
            Location = _Location;
            Rotation = _Rotation;
            Scale = _Scale;
        }
    }
}