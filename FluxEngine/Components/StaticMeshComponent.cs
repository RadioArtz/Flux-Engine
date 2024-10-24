﻿using OpenTK.Graphics.OpenGL4;
using Flux.Core.Rendering;
using Flux.Core.AssetManagement;

namespace Flux.Types
{
    public class StaticMeshComponent : BaseComponent
    {
        MeshRef _internalMeshRef;
        public SubMesh[] subMeshes;
        private Material _material;
        public float cullingDistance = -1;
        public bool isVisible = true;
        public StaticMeshComponent(MeshRef inMeshRef, Material inMaterial)
        {
            _material = inMaterial;
            _internalMeshRef = inMeshRef;
            GenerateSubMeshes(MeshLoader.GetMeshAssetFromRef(inMeshRef));
            RenderManager.RegisterStaticMeshComponent(this);
        }
        
        /// <summary>
        /// Initialize the StaticMesh with custom mesh data.
        /// </summary>
        /// <param name="inMeshData"></param>
        /// <param name="inMaterial"></param>
        public StaticMeshComponent(MeshData inMeshData, Material inMaterial)
        {
            _material = inMaterial;
            MeshData[] tmpMeshData = new MeshData[1];
            tmpMeshData[0] = inMeshData;
            StaticMeshAsset tmpAsset = new StaticMeshAsset(tmpMeshData, "internal");
            GenerateSubMeshes(tmpAsset);
            RenderManager.RegisterStaticMeshComponent(this);
        }
        
        public void GenerateSubMeshes(StaticMeshAsset meshAsset)
        {
            subMeshes = new SubMesh[meshAsset.Meshes.Length];
            for(int i = 0; i < meshAsset.Meshes.Length; i++)
            {
                subMeshes[i] = new SubMesh(meshAsset.Meshes[i], _material, this);
                //Flux.Core.Debug.LogEngine("submeshAdded");
            }
        }
        
        public bool Render()
        {
            if (!isVisible)
                return false;
            if (cullingDistance != -1)
            {
                if (RenderManager.activeCamera.ParentObject.TransformComponent.FastDistanceTo(ParentObject) > cullingDistance)
                    return false;
            }
            foreach (SubMesh sub in subMeshes)
            {
                sub.SubmeshRender();
            }
            return true;
        }

        public class SubMesh
        {
            public Material _material;

            private int VertexBufferObject;
            private int VertexArrayObject;
            private int ElementBufferObject;
            private int _indicesCount;
            private StaticMeshComponent _smc;

            public SubMesh(MeshData meshData, Material mat, StaticMeshComponent staticMeshCompRef)
            {
                _smc = staticMeshCompRef;
                _material = mat;
                _indicesCount = meshData.Indices.Length;

                float[] mergedMeshData = MergedMeshArray(meshData.Vertices, meshData.UVCoords, meshData.Normals);

                VertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, mergedMeshData.Length * sizeof(float), mergedMeshData, BufferUsageHint.StaticDraw);

                VertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(VertexArrayObject);

                int vertPosLocation = _material._shader.GetAttribLocation("aPosition");
                GL.VertexAttribPointer(vertPosLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
                GL.EnableVertexAttribArray(vertPosLocation);

                int texCoordLocation = _material._shader.GetAttribLocation("aTextureCoords");
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(texCoordLocation);

                int normalLocation = _material._shader.GetAttribLocation("aNormal");
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
                GL.EnableVertexAttribArray(normalLocation);

                ElementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indicesCount * sizeof(uint), meshData.Indices, BufferUsageHint.StaticDraw);

                _material._shader.Use();
            }

            public void SubmeshRender()
            {
                GL.BindVertexArray(VertexArrayObject);
                _material.Render(_smc.ParentObject.TransformComponent);
                GL.DrawElements(PrimitiveType.Triangles, _indicesCount, DrawElementsType.UnsignedInt, 0);
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
}