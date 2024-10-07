using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Types
{
    public struct MeshRef
    {
        public int _meshIndex;
        public int _guid;
        public MeshRef(int index, int guid)
        {
            _guid = guid;
            _meshIndex = index;
        }
    }
    public struct StaticMeshAsset
    {
        public StaticMeshAsset(float[] verts, float[] normals, float[] uvs, uint[] indices, string path)
        {
            _path = path;   
            _vertices = verts;
            _indices = indices;
            _normals = normals;
            _uvCoords = uvs;
        }

        string _path;
        float[] _vertices = {};
        float[] _uvCoords = {};
        float[] _normals = {};
        uint[] _indices;

        public float[] Vertices { get => _vertices; }
        public uint[] Indices { get => _indices; } 
        public float[] UVCoords { get => _uvCoords; }
        public float[] Normals { get => _normals; }
    }
}