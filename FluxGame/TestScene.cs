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
        MeshRef terrainMesh;
        MeshRef cubeMesh;
        AActor myAwesomeCamera;

        float totalTime;
        public float scaley;
        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeCamera = new BasicActor();
            myAwesomeCamera.AddComponent(new CameraComponent());
            RenderManager.activeCamera = (CameraComponent)myAwesomeCamera.ChildComponents[0];
            myAwesomeCamera.AddComponent(new EditorCamera(Engine.window));
            myAwesomeCamera.AddComponent(new AudioListenerComponent());
           
            Debug.Log("Loading Terrain!");
            terrainMesh = MeshLoader.LoadMeshFromFile(@"A:\_Terrain.obj");
            RenderTesterActor = new BasicActor();
            RenderTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(terrainMesh),new WhiteMat()));

            AudioTesterActor = new BasicActor();
            cubeMesh = MeshLoader.LoadMeshFromFile("A:/Goober.obj");
            AudioTesterActor.AddComponent(new StaticMeshComponent(MeshLoader.GetMeshAssetFromRef(cubeMesh), new WhiteMat()));
            AudioTesterActor.AddComponent(new AudioSourceComponent("A:/Big Fat wip.wav", true,128,1,10,EAudioMode.Audio3D,true));
        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);
            totalTime += delta;
            float tmpCalcThing = 1/(60f / 135f);
            float sineThing = (MathF.Sin(2*MathF.PI*tmpCalcThing*totalTime/2f));
            scaley = MathExt.Lerp(0.75f, 1.25f, MathF.Abs(sineThing));
            AudioTesterActor.TransformComponent.transform.Scale = new OpenTK.Mathematics.Vector3(1+(1-scaley), scaley, 1 + (1 - scaley));
        }
    }
}