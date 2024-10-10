using OpenTK.Graphics.OpenGL4;
using Flux.Core.Rendering;

namespace Flux.Types
{
    public class StaticMeshComponent : BaseComponent
    {
        public Material _material;

        private int VertexBufferObject;
        private int VertexArrayObject;
        private int ElementBufferObject;

        private int indicesCount;

        public StaticMeshComponent(StaticMeshAsset staticMesh, Material mat)
        {
            _material = mat;
            indicesCount = staticMesh.Indices.Length;

            float[] mergedMeshData = MergedMeshArray(staticMesh.Vertices,staticMesh.UVCoords,staticMesh.Normals);

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
            GL.BufferData(BufferTarget.ElementArrayBuffer, indicesCount * sizeof(uint), staticMesh.Indices, BufferUsageHint.StaticDraw);

            _material._shader.Use();

            RenderManager.RegisterStaticMeshComponent(this);
        }
        public void Render()
        {
            GL.BindVertexArray(VertexArrayObject);
            _material.Render(ParentObject.TransformComponent);
            GL.DrawElements(PrimitiveType.Triangles, indicesCount, DrawElementsType.UnsignedInt, 0);
        }

        private float[] MergedMeshArray(float[] verts, float[] uvs, float[] normals)
        {
            var tmplist = new List<float>();
            int vertexCount = verts.Length / 3;

            for (int i = 0; i < vertexCount; i++)
            {
                tmplist.Add(verts[i * 3]);
                tmplist.Add(verts[i * 3 + 1]);
                tmplist.Add(verts[i * 3 + 2]);

                tmplist.Add(uvs[i * 2]);
                tmplist.Add(uvs[i * 2 + 1]);

                tmplist.Add(normals[i * 3]);
                tmplist.Add(normals[i * 3 + 1]);
                tmplist.Add(normals[i * 3 + 2]);
            }
            return tmplist.ToArray();
        }
    }
}