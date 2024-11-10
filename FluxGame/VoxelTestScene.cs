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
        AActor? RenderTesterActor;
        AActor? AudioTesterActor;
        MeshRef terrainMesh;
        MeshRef cubeMesh;
        AActor? myAwesomeCamera;
        double audioTestTime;
        float totalTime;
        public float scaley;
        bool ready;
        Material voxelmat = new UnlitTexturedMat();

        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeCamera = new BasicActor();
            myAwesomeCamera.AddComponent(new CameraComponent());
            RenderManager.activeCamera = (CameraComponent)myAwesomeCamera.ChildComponents[0];
            myAwesomeCamera.AddComponent(new EditorCamera(Engine.window!));
            myAwesomeCamera.AddComponent(new AudioListenerComponent());

            Debug.Log("Generating Terrain!");
            RenderTesterActor = new BasicActor();



            Debug.Log("GENERATING... 01|09");
            VoxelTerrain.GenerateTerrain(out float[] _vertices, out float[] _normals, out float[] _uvCoords, out uint[] _indices, 0, 0);
            MeshData voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
            RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));;
            int idx = 0;
            int size = 16;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!(x == y && y == 0))
                    {
                        
                        Debug.Log("Generating: " + idx + "/" + size * size);
                        VoxelTerrain.GenerateTerrain(out _vertices, out _normals, out _uvCoords, out _indices, x, y);
                        voxelData = new MeshData(_vertices, _normals, _uvCoords, _indices);
                        RenderTesterActor.AddComponent(new StaticMeshComponent(voxelData, voxelmat));
                    }
                    idx++;
                }
            }

            GC.Collect();

        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);
        }
    }
}