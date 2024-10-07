using Flux.Constants;
using Flux.Types;

namespace Flux.Core.Rendering
{
    internal static class RenderManager
    {
        private static Shader _fallbackshader;
        private static List<ShaderRef> _shaders = new List<ShaderRef>();
        private static List<StaticMeshComponent> _staticMeshComponents = new List<StaticMeshComponent>();
        public static List<StaticMeshComponent> StaticMeshComponents => _staticMeshComponents;
        
        static RenderManager()
        {
            Debug.Log("Preparing Fallback shaders...", ConsoleColor.Cyan, ConsoleColor.DarkGray);
            _fallbackshader = new Shader(RenderingConfig.SHADER_FALLBACK_VERT, RenderingConfig.SHADER_FALLBACK_FRAG);
            Debug.Log("Finished preparing Fallback shaders!", ConsoleColor.Cyan, ConsoleColor.DarkGray);
        }
        public static bool RegisterStaticMeshComponent(StaticMeshComponent Component)
        {
            _staticMeshComponents.Add(Component);
            return true;
        }
        public static bool Render()
        {   
            foreach (StaticMeshComponent smc in _staticMeshComponents)
            {
                smc.Render();
            }
            return true;
        }
        public static Shader CompileAndRegisterShader(string fragShader, string vertShader)
        {
            Shader shader;
            if(CheckShaderCompiled(fragShader, vertShader, out shader))
                return(shader);
            else
            {
                string tmpString = fragShader + vertShader;
                int tmpGUID = tmpString.GetHashCode();
                shader = new Shader(vertShader, fragShader);
                ShaderRef tmpShaderRef = new ShaderRef();
                tmpShaderRef._shader = shader;
                tmpShaderRef._guid = tmpGUID;
                _shaders.Add(tmpShaderRef);
                return shader;
            }
        }

        public static bool CheckShaderCompiled(string fragShader, string vertShader, out Shader outShader)
        {
            foreach (ShaderRef current in _shaders)
            {
                string tmpString = fragShader + vertShader;
                int tmpGUID = tmpString.GetHashCode();
                if (tmpGUID == current._guid)
                {
                    outShader = current._shader;
                    return true;
                }
                else
                {
                    outShader = _fallbackshader;
                    return true;
                }
            }
            outShader = _fallbackshader;
            return false;
        }
        public static bool CheckIfShaderCompiled(string fragShader, string vertShader)
        {
            foreach (ShaderRef current in _shaders)
            {
                string tmpString = fragShader + vertShader;
                int tmpGUID = tmpString.GetHashCode();
                if (tmpGUID == current._guid)
                    return true;
                else
                    return true;
            }
            return false;
        }
    }
}