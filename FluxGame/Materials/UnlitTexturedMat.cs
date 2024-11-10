using Flux.Core.Rendering;
using Flux.Types;

namespace FluxGame.Materials
{
    public class UnlitTexturedMat : Material
    {
        Texture myTex;
        public UnlitTexturedMat() 
        {
            myTex = Texture.LoadFromFile("F:\\THE STICK\\enemy.png");
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
            base.Render(inTransform);
        }
    }
}