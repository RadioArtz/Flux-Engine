using Flux.Core;
using Flux.Core.AssetManagement;
using Flux.Core.Rendering;
using Flux.Types;

namespace Flux
{
    public class TestScene : FScene
    {
        AActor RenderTesterActor;
        MeshRef myAwesomeMesh;
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
            RenderTesterActor = new QuadActor();
            RenderTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(myAwesomeMesh),false));
        }
        public override void OnTick(float delta)
        {
            base.OnTick(delta);
            ((EditorCamera)myAwesomeCamera.ChildComponents[1]).PreRender(delta);
        }
    }
}