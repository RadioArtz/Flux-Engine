using Flux.Constants;
using Flux.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Core.Rendering
{
    public class Material
    {
        public virtual string getVertShaderPath()
        {
            return vertShaderPath;
        }
        public virtual string getFragShaderPath()
        {
            return fragShaderpath;
        }
        public string vertShaderPath = RenderingConfig.SHADER_FALLBACK_VERT;
        public string fragShaderpath = RenderingConfig.SHADER_FALLBACK_FRAG;
        public Shader shader;

        public void Compile()
        {
            shader = RenderManager.CompileAndRegisterShader(getFragShaderPath(), getVertShaderPath());
        }

        public virtual void Render() 
        {
            if (RenderManager.CheckIfShaderCompiled(getFragShaderPath(), getVertShaderPath()))
                shader.Use();
            else
                Compile();
        }
    }
}