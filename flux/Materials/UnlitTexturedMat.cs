using Flux.Core.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Materials
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
        public override void Render()
        {
            base.Render();
        }
    }
}
