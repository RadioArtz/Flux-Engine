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
        AActor AudioTesterActor;
        MeshRef myAwesomeMesh;
        MeshRef CubeMesh;
        AActor myAwesomeCamera;

        float totalTime;
        public float scaley;
        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeCamera = new QuadActor();
            myAwesomeCamera.AddComponent(new CameraComponent());
            RenderManager.activeCamera = (CameraComponent)myAwesomeCamera.ChildComponents[0];
            myAwesomeCamera.AddComponent(new EditorCamera(Engine.window));
            Debug.Log("Loading Mesh!");
            //myAwesomeMesh = MeshLoader.LoadMeshFromFile(@"A:\_Terrain.obj");
            //RenderTesterActor = new QuadActor();
            //RenderTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(myAwesomeMesh),new WhiteMat()));

            AudioTesterActor = new QuadActor();
            CubeMesh = MeshLoader.LoadMeshFromFile("A:/Goober.obj");
            AudioTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(CubeMesh), new WhiteMat()));
            AudioTesterActor.AddComponent(new AudioSource());
            ((AudioSource)AudioTesterActor.ChildComponents[AudioTesterActor.ChildComponents.Count - 1]).Init();
        }
        public override void OnTick(float delta)
        {
            base.OnTick(delta);
            totalTime += delta;
            float tmpCalcThing = 1/(60f / 135f);
            float sineThing = (MathF.Sin(2*MathF.PI*tmpCalcThing*totalTime)+1)/2;
            scaley = MathExt.Lerp(0.75f, 1.25f, sineThing);
            ((AudioSource)AudioTesterActor.ChildComponents[AudioTesterActor.ChildComponents.Count - 1]).Update(myAwesomeCamera.TransformComponent.transform, myAwesomeCamera.TransformComponent.GetVelocity());
        }
    }
}