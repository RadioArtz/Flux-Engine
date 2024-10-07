using Flux.Core;
using Flux.Core.AssetManagement;
using Flux.Types;


namespace Flux
{
    public class TestScene : FScene
    {
        AActor RenderTesterActor;
        MeshRef myAwesomeMesh;
        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeMesh = MeshLoader.LoadMeshFromFile(@"A:\GinV2.fbx");
            RenderTesterActor = new QuadActor();
            RenderTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(myAwesomeMesh),false));
        }
    }
}