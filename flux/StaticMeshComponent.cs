
using OpenTK.Graphics.OpenGL4;
using Flux.Core.Rendering;
using Flux.Materials;
using System.Threading.Channels;

namespace Flux.Types
{
    public class StaticMeshComponent : BaseComponent
    {
        public StaticMeshAsset _staticMesh;

        //TODO switch for Materials later on
        //public Shader MeshShader;
        public Material _material;

        private int VertexBufferObject;
        private int VertexArrayObject;
        private int ElementBufferObject;

        public StaticMeshComponent(StaticMeshAsset staticMesh, bool yee)
        {
            _staticMesh = staticMesh;
            if (yee)
            {
                _material = new BlackMat();
            }
            else
            {
                _material = new WhiteMat();
            }
            float[] mergedMeshData = MergedMeshArray();
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mergedMeshData.Length * sizeof(float), mergedMeshData, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            int vertPosLocation = _material._shader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertPosLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertPosLocation);
            
            int texCoordLocation = _material._shader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texCoordLocation);

            int normalLocation = _material._shader.GetAttribLocation("aNormal");
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(normalLocation);
            

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _staticMesh.Indices.Length * sizeof(uint), _staticMesh.Indices, BufferUsageHint.StaticDraw);

            _material._shader.Use();

            RenderManager.RegisterStaticMeshComponent(this);
        }
        public void Render()
        {
            GL.BindVertexArray(VertexArrayObject);
            _material.Render();
            GL.DrawElements(PrimitiveType.Triangles, _staticMesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        private float[] MergedMeshArray()
        {
            var tmplist = new List<float>();
            int vertexCount = _staticMesh.Vertices.Length / 3;

            for (int i = 0; i < vertexCount; i++)
            {
                tmplist.Add(_staticMesh.Vertices[i * 3]);
                tmplist.Add(_staticMesh.Vertices[i * 3 + 1]);
                tmplist.Add(_staticMesh.Vertices[i * 3 + 2]);

                tmplist.Add(_staticMesh.UVCoords[i * 2]);
                tmplist.Add(_staticMesh.UVCoords[i * 2 + 1]);

                tmplist.Add(_staticMesh.Normals[i * 3]);
                tmplist.Add(_staticMesh.Normals[i * 3 + 1]);
                tmplist.Add(_staticMesh.Normals[i * 3 + 2]);
            }
            return tmplist.ToArray();
        }
    }
}