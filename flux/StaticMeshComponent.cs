using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Flux.Types
{
    public class StaticMeshComponent : BaseComponent
    {
        public StaticMeshAsset StaticMesh;

        //TODO switch for Materials later on
        public Shader MeshShader;

        private int VertexBufferObject;
        private int VertexArrayObject;
        private int ElementBufferObject;

        public StaticMeshComponent(StaticMeshAsset staticMesh)
        {
            StaticMesh = staticMesh;
            MeshShader = new Shader(@"A:\Projects\flux2\Flux\Shaders\main.vert", @"A:\Projects\flux2\Flux\Shaders\main.frag");
            VertexArrayObject = GL.GenVertexArray();
            VertexBufferObject = GL.GenBuffer();
            ElementBufferObject = GL.GenBuffer();
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, StaticMesh.Indices.Length * sizeof(uint), StaticMesh.Indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, StaticMesh.Vertices.Length * sizeof(float), StaticMesh.Vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
            
            RenderManager.RegisterStaticMeshComponent(this);
        }
        public void Render()
        {

            MeshShader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, StaticMesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}