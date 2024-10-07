﻿using Flux.Core.Rendering;

namespace Flux.Materials
{
    public class WhiteMat : Material
    {
        public WhiteMat() { Compile(); }
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
            shader.SetVector4("color", new OpenTK.Mathematics.Vector4(1,1,1,1));
            base.Render();
        }
    }
}