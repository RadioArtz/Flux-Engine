using Flux.Core;
using Flux.Core.AssetManagement;
using Flux.Types;


namespace Flux
{
    public class TestScene : FScene
    {
        #region tmpMesh
        float[] vertices = {
     0.5f,  0.5f, 0.0f,  // top right
     0.5f, -0.5f, 0.0f,  // bottom right
     0.0f, -0.5f, 0.0f,  // bottom left
     0.0f,  0.5f, 0.0f   // top left
};
        uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
};
        #endregion
        #region tmpMesh2
        float[] vertices2 = {
     0.0f,  0.5f, 0.0f,  // top right
     0.0f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left
};
        uint[] indices2 = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
};
        #endregion

        StaticMeshAsset mymesh;
        StaticMeshAsset mymesh2;
        Actor RenderTesterActor;
        Actor RenderTesterActor2;
        MeshRef myAwesomeMesh;
        MeshRef myAwesomeMesh2;
        public override void OnLoad()
        {
            base.OnLoad();
            mymesh = new StaticMeshAsset(vertices, null, null, indices, null);
            mymesh2 = new StaticMeshAsset(vertices2, null, null, indices2, null);
            RenderTesterActor = new QuadActor();
            RenderTesterActor.AddComponent(new StaticMeshComponent(mymesh,false));
            RenderTesterActor2 = new QuadActor();
            RenderTesterActor2.AddComponent(new StaticMeshComponent(mymesh2,true));

            myAwesomeMesh = MeshLoader.LoadMeshFromFile(@"A:\GinV2.fbx");
            myAwesomeMesh2 = MeshLoader.LoadMeshFromFile(@"A:\GinV2.fbx");
        }
    }
}
