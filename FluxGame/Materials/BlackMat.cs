using Flux.Core.Rendering;
using Flux.Types;

namespace FluxGame.Materials
{
    public class BlackMat : Material
    {
        public BlackMat() { Compile(); }
        public override string GetVertShaderPath()
        {
            return Path.Combine("Shaders", "main.vert");
        }
        public override string GetFragShaderPath()
        {
            return Path.Combine("Shaders", "main.frag");
        }
        public override void Render(TransformComponent inTransform)
        {
            _shader.SetVector4("color", new OpenTK.Mathematics.Vector4(0));
            base.Render(inTransform);
        }
    }
}
