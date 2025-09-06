using Flux.Core.Rendering;
using Flux.Types;

namespace FluxGame.Materials
{
    public class TexturedMat : Material
    {
        Texture myTex;
        float _tiling = 1;

        public TexturedMat(string TexturePath) 
        {
            myTex = Texture.LoadFromFile(TexturePath);
            Compile();
        }
        public TexturedMat(string TexturePath, float tiling)
        {
            _tiling = tiling;
            myTex = Texture.LoadFromFile(TexturePath);
            Compile();
        }
        public override string GetVertShaderPath()
        {
            return Path.Combine("Shaders", "unlit_textured.vert");
        }
        public override string GetFragShaderPath()
        {
            return Path.Combine("Shaders", "unlit_textured.frag");
        }
        public override void Render(TransformComponent inTransform)
        {
            myTex.Use(OpenTK.Graphics.OpenGL4.TextureUnit.Texture0);
            _shader.SetFloat("tiling", _tiling);
            base.Render(inTransform);
        }
    }
}