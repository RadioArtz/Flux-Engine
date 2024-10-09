using Flux.Core.Rendering;
using Flux.Types;

namespace FluxGame.Materials
{
    public class UnlitTexturedMat : Material
    {
        public UnlitTexturedMat() { Compile(); }
        public override string getVertShaderPath()
        {
            return "Shaders\\unlit_textured.vert";
        }
        public override string getFragShaderPath()
        {
            return "Shaders\\unlit_textured.frag";
        }
        public override void Render(TransformComponent inTransform)
        {
            base.Render(inTransform);
        }
    }
}
