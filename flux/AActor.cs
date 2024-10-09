using Flux.Core;

namespace Flux.Types
{
    public abstract class AActor
    {
        public string ObjectName = "Unnamed";
        public bool canEverTick = true;
        private List<BaseComponent> _childComponents = new List<BaseComponent>();
        public List<BaseComponent> ChildComponents { get => _childComponents; }

        private TransformComponent _transformComponent = new TransformComponent();
        public TransformComponent TransformComponent { get => _transformComponent; }
        public AActor() { Engine.window.GetActiveScene().RegisterActor(this); }
        public abstract void Constructor();
        public abstract void BeginPlay();
        public abstract void OnTick(float delta);
        public void TickComponents(float delta)
        {
            foreach (BaseComponent comp in ChildComponents)
            {
                if(comp.canEverTick)
                    comp.OnTick(delta);
                else
                    Debug.LogError("Suboptimal implementation of BaseComponents canEverTick! unneseccary querrying of bool. switch for some other cached solution!");
            }
        }
        public void AddComponent(BaseComponent Component)
        {
            _childComponents.Add(Component);
            Component.OnComponentAttached(this);
            Component.SetParent(this);
        }
        public void RemoveComponent(BaseComponent Component)
        {
            _childComponents.Remove(Component);
        }
        public void RemoveComponent(int Index)
        {
            _childComponents.RemoveAt(Index);
        }
    }
}