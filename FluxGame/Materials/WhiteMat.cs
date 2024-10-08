﻿using Flux;
using Flux.Core.Rendering;
using Flux.Types;

namespace FluxGame.Materials
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
        public override void Render(TransformComponent inTransform)
        {
            _shader.SetVector4("color", new OpenTK.Mathematics.Vector4(1));
            _shader.SetFloat("yScale", 1);
            base.Render(inTransform);
        }
    }
}