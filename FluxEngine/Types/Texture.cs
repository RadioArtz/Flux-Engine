using Flux.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using StbImageSharp;
using System.IO;
using System.Reflection.Metadata;

namespace Flux.Types
{
    public class Texture
    {
        private int _handle;
        public static Texture LoadFromFile(string path)
        {
            int handle = GL.GenTexture();
            Texture t = new Texture(handle);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.LinearSharpenSgis);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropy, 16);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

            if (image != null)
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else
            {
                Console.WriteLine("Failed to load texture from path: " + path);
            }

            return t;
        }

        public Texture(int glHandle)
        {
            _handle = glHandle;
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, _handle);
        }
    }
}
