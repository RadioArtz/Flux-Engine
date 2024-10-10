using Flux.Core;

namespace Flux.Types
{
    public abstract class FScene
    {
        public FScene() { }
        private List<AActor> _Actors = new List<AActor>();
        public AActor[] GetActors() { return _Actors.ToArray(); }

        public virtual void OnLoad() { }
        public virtual void OnTick(float delta) { }
        public void TickActors(float delta) 
        { 
            foreach (AActor actor in _Actors)
            {
                if (actor.canEverTick)
                    actor.OnTick(delta);
                else
                    Debug.LogError("Suboptimal implementation of AActors canEverTick! unneseccary querrying of bool. switch for some other cached solution!");
                actor.TickComponents(delta);
            }
        }
        public void RegisterActor(AActor actor)
        {
            _Actors.Add(actor);
        }
        public void RemoveActor(AActor actor)
        {
            _Actors.Remove(actor);
        }
    }
}