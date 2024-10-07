using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.Types
{
    public struct StaticMeshAsset
    {
        public struct MeshRef
        {
            public StaticMeshAsset _mesh;
            public int _guid;
        }
        public StaticMeshAsset(float[] verts, uint[] indices)
        {
            _vertices = verts;
            _indices = indices;
        }
        float[] _vertices = {};
        uint[] _indices;
        public float[] Vertices { get => _vertices; }
        public uint[] Indices { get => _indices; } 
    }
}