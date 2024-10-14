
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
    public class MeshData
    {
        public MeshData(float[] verts, float[] normals, float[] uvs, uint[] indices)
        {
            _vertices = verts;
            _indices = indices;
            _normals = normals;
            _uvCoords = uvs;
        }

        float[] _vertices = { };
        float[] _uvCoords = { };
        float[] _normals = { };
        uint[] _indices;

        public float[] Vertices { get => _vertices; }
        public uint[] Indices { get => _indices; }
        public float[] UVCoords { get => _uvCoords; }
        public float[] Normals { get => _normals; }
    }
    public struct StaticMeshAsset
    {
        public StaticMeshAsset(MeshData[] meshes, string path)
        {
            _path = path;
            _meshes = meshes;
        }

        string _path;
        MeshData[] _meshes = { };

        public string FilePath { get => _path; }
        public MeshData[] Meshes { get => _meshes; }
    }
}