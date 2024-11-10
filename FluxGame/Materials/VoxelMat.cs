using Flux;
using Flux.Core.Rendering;
using Flux.Types;

namespace FluxGame.Materials
{
    public class Voxelmat : Material
    {
        public Voxelmat() { Compile(); }
        public override string GetVertShaderPath()
        {
            return Path.Combine("Shaders", "main.vert");
        }
        public override string GetFragShaderPath()
        {
            return Path.Combine("Shaders", "voxel.frag");
        }
        public override void Render(TransformComponent inTransform)
        {
            _shader!.SetVector4("color", new OpenTK.Mathematics.Vector4(1));
            _shader.SetFloat("yScale", 1);
            base.Render(inTransform);
        }
    }
}