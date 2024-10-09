using Flux.Constants;
using Flux.Types;

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
        public Shader _shader;

        public void Compile()=> _shader = RenderManager.CompileAndRegisterShader(getFragShaderPath(), getVertShaderPath());

        public virtual void Render(TransformComponent inTransform) 
        {
            if (RenderManager.CheckIfShaderCompiled(getFragShaderPath(), getVertShaderPath()))
            {
                RenderManager.activeCamera.UpdateViewMatrix();

                _shader.SetMatrix4("model", inTransform.GetModelMatrix());
                _shader.SetMatrix4("view", RenderManager.activeCamera.view);
                _shader.SetMatrix4("projection", RenderManager._projection);

                _shader.Use();
            }
            else
                Compile();
        }
    }
}