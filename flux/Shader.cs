using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Security.Cryptography;

namespace Flux.Types
{
    public struct ShaderRef
    {
        public Shader _shader;
        public int _guid;
    }
    public class Shader
    {
        private int _handle;
        private readonly Dictionary<string, int> uniformLocations;
        public Shader(string vertexPath, string fragmentPath)
        {
            Core.Debug.LogEngine("Compiling Vertex Shader..." + vertexPath);
            Core.Debug.LogEngine("Compiling Fragment Shader..." + fragmentPath);
            int VertexShader;
            int FragmentShader;

            string VertexShaderSource = File.ReadAllText(vertexPath);
            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }

            _handle = GL.CreateProgram();

            GL.AttachShader(_handle, VertexShader);
            GL.AttachShader(_handle, FragmentShader);

            GL.LinkProgram(_handle);

            GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(_handle);
                Console.WriteLine(infoLog);
            }

            GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            // Next, allocate the dictionary to hold the locations.
            uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                // get the name of this uniform,
                var key = GL.GetActiveUniform(_handle, i, out _, out _);

                // get the location,
                var location = GL.GetUniformLocation(_handle, key);

                // and then add it to the dictionary.
                uniformLocations.Add(key, location);
                //Core.Debug.Log((key, location));
            }
        }
        public void Use()
        {
            GL.UseProgram(_handle);
        }
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(_handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region setting utils
        // Uniform setters
        // Uniforms are variables that can be set by user code, instead of reading them from the VBO.
        // You use VBOs for vertex-related data, and uniforms for almost everything else.

        // Setting a uniform is almost always the exact same, so I'll explain it here once, instead of in every method:
        //     1. Bind the program you want to set the uniform on
        //     2. Get a handle to the location of the uniform with GL.GetUniformLocation.
        //     3. Use the appropriate GL.Uniform* function to set the uniform.

        /// <summary>
        /// Set a uniform int on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetInt(string name, int data)
        {
            if (!uniformLocations.ContainsKey(name))
                return;
            GL.UseProgram(_handle);
            GL.Uniform1(uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform float on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetFloat(string name, float data)
        {
            if (!uniformLocations.ContainsKey(name))
                return;
            GL.UseProgram(_handle);
            GL.Uniform1(uniformLocations[name], data);
        }

        /// <summary>
        /// Set a uniform Matrix4 on this shader
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        /// <remarks>
        ///   <para>
        ///   The matrix is transposed before being sent to the shader.
        ///   </para>
        /// </remarks>
        public void SetMatrix4(string name, Matrix4 data)
        {
            if (!uniformLocations.ContainsKey(name))
                return;
            GL.UseProgram(_handle);
            GL.UniformMatrix4(uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// Set a uniform Vector3 on this shader.
        /// </summary>
        /// <param name="name">The name of the uniform</param>
        /// <param name="data">The data to set</param>
        public void SetVector3(string name, Vector3 data)
        {
            if (!uniformLocations.ContainsKey(name))
                return;
            GL.UseProgram(_handle);
            GL.Uniform3(uniformLocations[name], data);
        }

        public void SetVector4(string name, Vector4 data)
        {
            if (!uniformLocations.ContainsKey(name))
                return;
            GL.UseProgram(_handle);
            GL.Uniform4(uniformLocations[name], data);
        }
        #endregion
    }
}
