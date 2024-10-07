using Flux.Core.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            shader.SetVector4("color", new OpenTK.Mathematics.Vector4(0));
            base.Render();
        }
    }
}
