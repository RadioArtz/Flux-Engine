using Flux.Types;
using FluxGame.OsuManiaParser;

namespace FluxGame
{
    public class maniaMover : BaseComponent
    {
        public double time;
        public ManiaKey key;
        public double currentTime;
        public maniaMover(double inTime, ManiaKey inkey)
        {
            time = inTime;
            key = inkey;
        }

        public override void OnTick(float delta)
        {
            base.OnTick(delta);
            ParentObject.TransformComponent.transform.Location.X = ManiaBeatmapParser.ParseXFromkey(key) * 0.01f;
            ParentObject.TransformComponent.transform.Location.Z = (float)(time - currentTime) * 0.05f;
            if (currentTime > time)
            {
                ParentObject.GetComponent<StaticMeshComponent>().isVisible = false;
            }
            else
            {
                ParentObject.GetComponent<StaticMeshComponent>().isVisible = true;
            }
        }
    }
}
