using Flux.Core;
using Flux.Core.AssetManagement;
using Flux.Core.Rendering;
using Flux.Types;
using Flux;
using FluxGame.Materials;

namespace FluxGame
{
    public class TestScene : FScene
    {
        AActor RenderTesterActor;
        MeshRef myAwesomeMesh;
        MeshRef myAwesomeMesh2;
        AActor myAwesomeCamera;

        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeCamera = new QuadActor();
            myAwesomeCamera.AddComponent(new CameraComponent());
            RenderManager.activeCamera = (CameraComponent)myAwesomeCamera.ChildComponents[0];
            myAwesomeCamera.AddComponent(new EditorCamera(Engine.window));
            Debug.Log("Loading Mesh!");
            myAwesomeMesh = MeshLoader.LoadMeshFromFile(@"A:\_Terrain.obj");
            myAwesomeMesh2 = MeshLoader.LoadMeshFromFile(@"A:\_Terrain2.obj");
            RenderTesterActor = new QuadActor();
            RenderTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(myAwesomeMesh),new WhiteMat()));
            RenderTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(myAwesomeMesh2), new WhiteMat()));
        }
    }
}