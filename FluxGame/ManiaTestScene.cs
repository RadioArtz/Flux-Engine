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
    public class ManiaTestScene : FScene
    {
        AActor RenderTesterActor;
        AActor AudioTesterActor;
        MeshRef cubeMesh;
        AActor myAwesomeCamera;
        double audioTestTime;
        float totalTime;
        public float scaley;
        bool ready;
        ManiaBeatmapParser parser;
        public BasicActor[] beatobjects;

        public override void OnLoad()
        {
            base.OnLoad();
            myAwesomeCamera = new BasicActor();
            myAwesomeCamera.AddComponent(new CameraComponent());
            RenderManager.activeCamera = (CameraComponent)myAwesomeCamera.ChildComponents[0];
            myAwesomeCamera.AddComponent(new EditorCamera(Engine.window));
            myAwesomeCamera.AddComponent(new AudioListenerComponent());


            RenderTesterActor = new BasicActor();
            AudioTesterActor = new BasicActor();
            AudioTesterActor.AddComponent(new AudioSourceComponent(@"C:\\575053 Camellia - Exit This Earth's Atomosphere/audio.mp3", true, 0, 0, 0, EAudioMode.Audio2D, true));

            parser = new ManiaBeatmapParser();
            parser.Parse(@"C:\\575053 Camellia - Exit This Earth's Atomosphere/Camellia - Exit This Earth's Atomosphere (Protastic101) [7.667 kms].osu");
            List<BasicActor> shitlist = new List<BasicActor>();
            MeshRef hitobjectMesh = MeshLoader.LoadMeshFromFile(@"D:\Games\UE5_5_4\UE_5.4\Templates\TemplateResources\Standard/1M_Cube.FBX");
            Material cubeMat = new WhiteMat();
            foreach (ManiaHitObject obj in parser.HitObjects)
            {
                BasicActor act = new BasicActor();
                act.AddComponent(new StaticMeshComponent(hitobjectMesh, cubeMat));
                act.TransformComponent.transform.Scale = new Vector3(0.01f);
                act.AddComponent(new maniaMover(obj.TimeMs, obj.Key));
                act.AddComponent(new AudioSourceComponent(@"D:\\Users\\Mathias\\Desktop\\DesktopDecember2020\\Files\\bruh/hitsound.wav", false,0,0,0,EAudioMode.Audio2D,false));
                act.GetComponent<StaticMeshComponent>().cullingDistance = 500;
                shitlist.Add(act);
            }
            beatobjects = shitlist.ToArray();
            Debug.LogError(shitlist.Count);


            ready = true;
        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);
            audioTestTime = AudioTesterActor.GetComponent<AudioSourceComponent>().GetCurrentPositionSeconds() * 1000;
            PrintNotesHit();
            if (ready)
            {
                foreach (BasicActor actor in beatobjects)
                {
                    actor.GetComponent<maniaMover>().currentTime = audioTestTime;
                }
            }
        }

        public void PrintNotesHit()
        {
            if (!ready)
                return;
            foreach (var hitObject in parser.HitObjects)
            {
                if (audioTestTime >= hitObject.TimeMs && !parser.hitNotes.Contains(hitObject))
                {
                    // Print the note information
                    Console.WriteLine($"Hit! Key: {hitObject.Key}, Time: {hitObject.TimeMs}ms");
                    foreach(AActor act in beatobjects)
                    {
                        if(act.GetComponent<maniaMover>().time == hitObject.TimeMs)
                        {
                            act.GetComponent<AudioSourceComponent>().Play();
                        }
                    }
                    parser.hitNotes.Add(hitObject);
                }
            }
        }
    }
}