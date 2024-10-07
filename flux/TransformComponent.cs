using Flux.Core;
using OpenTK.Mathematics;

namespace Flux.Types
{
    public class TransformComponent : BaseComponent
    {
        public Transform transform;
        public EMobilityType mobilityType = EMobilityType.EMovable;
        Matrix4 ModelMatrixCache;
        public TransformComponent()
        {
            transform.Scale = new Vector3(1, 1, 1);
            if (mobilityType == EMobilityType.EStatic)
            {
                GetModelMatrix(true);
                Debug.Log("Creating static model matrix...", ConsoleColor.DarkCyan);
            }
        }
        public TransformComponent(Transform initTransform)
        {
            transform = initTransform;
            if (mobilityType == EMobilityType.EStatic)
            {
                GetModelMatrix(true);
                Debug.Log("Creating static model matrix...", ConsoleColor.DarkCyan);
            }
        }
        public Matrix4 GetModelMatrix(bool overrideStatic = false)
        {
            if (mobilityType == EMobilityType.EStatic && !overrideStatic)
            {/*
                if (ModelMatrixCache == null)
                {
                    GetModelMatrix(true);
                    Debug.Log("Creating static model matrix...", ConsoleColor.DarkCyan);
                } */
                return ModelMatrixCache;
            }

            Matrix4 model;
            model = Matrix4.Identity;
            var xRot = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(transform.Rotation.X));
            var yRot = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(transform.Rotation.Y));
            var zRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(transform.Rotation.Z));

            model *= xRot;
            model *= yRot;
            model *= zRot;
            model *= Matrix4.CreateScale(transform.Scale);
            model *= Matrix4.CreateTranslation(transform.Location);
            ModelMatrixCache = model;
            
            return model;
        }
        
    }
    public enum EMobilityType
    {
        EStatic,
        EMovable
    }
}