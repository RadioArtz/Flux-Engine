using Assimp;
using Flux.Types;

namespace Flux.Core.AssetManagement
{
    internal static class MeshLoader
    {
        static List<MeshRef> _meshRefs = new List<MeshRef>();
        static List <StaticMeshAsset> _meshes = new List<StaticMeshAsset>();

        public static MeshRef LoadMeshFromFile(string filePath) 
        {
            int loadedCheck = CheckMeshLoaded(filePath);
            if (loadedCheck != -1)
            {
                return _meshRefs[loadedCheck];
            }

            var assimpContext = new AssimpContext();
            var assimpScene = assimpContext.ImportFile(filePath, PostProcessSteps.GenerateNormals | PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate | PostProcessSteps.FindInvalidData | PostProcessSteps.OptimizeMeshes | PostProcessSteps.ImproveCacheLocality | PostProcessSteps.JoinIdenticalVertices);
            
            float[] tmpVerts = ConvertVertecies(assimpScene.Meshes[0], false, false, 0);
            float[] tmpUvs = ConvertUVCoords(assimpScene.Meshes[0],0);
            float[] tmpNormals = ConvertNormals(assimpScene.Meshes[0]);
            uint[] tmpIndecies = ConvertIndecies(assimpScene.Meshes[0]);

            StaticMeshAsset tmpAsset = new StaticMeshAsset(tmpVerts, tmpNormals, tmpUvs, tmpIndecies, filePath);
            _meshes.Add(tmpAsset);
            MeshRef _tmpRef = RegisterMeshAsset(filePath, _meshes.Count - 1);
            return _tmpRef;
        }

        static MeshRef RegisterMeshAsset(string filePath, int assetIndex)
        {
            MeshRef tmpMeshRef = new MeshRef();
            tmpMeshRef._meshIndex = assetIndex;
            tmpMeshRef._guid = filePath.GetHashCode();
            return tmpMeshRef;
        }

        public static int CheckMeshLoaded(string filePath)
        {
            int tmpGuid = filePath.GetHashCode() ;
            foreach (MeshRef mRef in _meshRefs)
            {
                if(mRef._guid == tmpGuid) return mRef._meshIndex;
            }
            return -1;
        }

        public static int CheckMeshLoaded(string filePath, out MeshRef loadedMesh)
        {
            int tmpGuid = filePath.GetHashCode();
            foreach (MeshRef mRef in _meshRefs)
            {
                if (mRef._guid == tmpGuid)
                {
                    loadedMesh = mRef;
                    return mRef._meshIndex;
                }
            }
            loadedMesh = new MeshRef(-1,-1);
            return -1;
        }
        #region converters
        public static float[] ConvertVertecies(Mesh inAssimpMesh, bool b_IncludeTexCoords, bool b_IncludeNormals, int uvChannel)
        {
            Debug.Log("Converting Vertecies", ConsoleColor.DarkYellow);
            int index = 0;
            var tmplist = new List<float>();
            foreach (Vector3D v3d in inAssimpMesh.Vertices)
            {
                tmplist.Add(v3d.X);
                tmplist.Add(v3d.Y);
                tmplist.Add(v3d.Z);

                if (b_IncludeTexCoords)
                {
                    tmplist.Add((inAssimpMesh.TextureCoordinateChannels[uvChannel])[index].X);
                    tmplist.Add((inAssimpMesh.TextureCoordinateChannels[uvChannel])[index].Y);
                }
                if (b_IncludeNormals)
                {
                    tmplist.Add(inAssimpMesh.Normals[index].X);
                    tmplist.Add(inAssimpMesh.Normals[index].Y);
                    tmplist.Add(inAssimpMesh.Normals[index].Z);
                }
                index++;
            }
            return tmplist.ToArray();
        }

        public static float[] ConvertUVCoords(Mesh inAssimpMesh, int uvChannel)
        {
            Debug.Log("Converting UVs", ConsoleColor.DarkYellow);
            var tmplist = new List<float>();
            foreach (Vector3D v3d in inAssimpMesh.TextureCoordinateChannels[uvChannel])
            {
                tmplist.Add(v3d.X);
                tmplist.Add(v3d.Y);
            }
            return tmplist.ToArray();
        }

        public static uint[] ConvertIndecies(Mesh inAssimpMesh)
        {
            Debug.Log("Converting Indecies", ConsoleColor.DarkYellow);
            var tmplist = new List<uint>();
            foreach (Face face in inAssimpMesh.Faces)
            {
                foreach (uint index in face.Indices)
                {
                    tmplist.Add(index);
                }
            }
            return tmplist.ToArray();
        }

        public static float[] ConvertNormals(Mesh inAssimpMesh)
        {
            Debug.Log("Converting Normals", ConsoleColor.DarkYellow);
            var tmplist = new List<float>();
            foreach (Vector3D v3d in inAssimpMesh.Normals)
            {
                tmplist.Add(v3d.X);
                tmplist.Add(v3d.Y);
                tmplist.Add(v3d.Z);
            }
            return tmplist.ToArray();
        }
        #endregion
    }
}