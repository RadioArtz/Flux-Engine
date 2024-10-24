using Flux.Core;
using Flux.Core.AssetManagement;
using Flux.Core.Rendering;
using Flux.Types;
using Flux;
using FluxGame.Materials;
using OpenTK.Mathematics;
using Flux.Components;
using FluxGame.OsuManiaParser;

namespace FluxGame
{
    public class VoxelTestScene : FScene
    {
        AActor RenderTesterActor;
        AActor AudioTesterActor;
        MeshRef terrainMesh;
        MeshRef cubeMesh;
        AActor myAwesomeCamera;
        double audioTestTime;
        float totalTime;
        public float scaley;
        bool ready;
        Material voxelmat = new Voxelmat();

        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeCamera = new BasicActor();
            myAwesomeCamera.AddComponent(new CameraComponent());
            RenderManager.activeCamera = (CameraComponent)myAwesomeCamera.ChildComponents[0];
            myAwesomeCamera.AddComponent(new EditorCamera(Engine.window));
            myAwesomeCamera.AddComponent(new AudioListenerComponent());

            Debug.Log("Generating Terrain!");
            RenderTesterActor = new BasicActor();
            float[] _vertices;
            float[] _uvCoords;
            float[] _normals;
            uint[] _indices;


            Debug.Log("GENERATING... 01|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 0, 0);
            MeshData voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 02|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 1, 0);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 03|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 2, 0);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 04|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 0, 1);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 05|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 0, 2);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 06|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 1, 1);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 07|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 2, 1);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 08|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 1, 2);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));

            Debug.Log("GENERATING... 09|09");
            VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, 2, 2);
            voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));
        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);
        }
    }
}