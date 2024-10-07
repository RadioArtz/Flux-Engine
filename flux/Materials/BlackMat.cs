using Flux.Core.Rendering;

namespace Flux.Materials
{
    public class BlackMat : Material
    {
        public BlackMat() { Compile(); }
        public override string getVertShaderPath()
        {
            return "Shaders\\main.vert";
        }
        public override string getFragShaderPath()
        {
            return "Shaders\\main.frag";
        }
        public override void Render()
        {
            _shader.SetVector4("color", new OpenTK.Mathematics.Vector4(0));
            base.Render();
        }
    }
}
