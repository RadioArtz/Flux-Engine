namespace Flux.Types
{
    public abstract class Actor
    {
        public string ObjectName = "NAN (DAS IST NOT A NAME NICHT NOT A NUMBER DU HURENSOHN)";
        private List<BaseComponent> _childComponents = new List<BaseComponent>();
        public List<BaseComponent> ChildComponents { get => _childComponents; }
        public abstract void Constructor();
        public abstract void BeginPlay();
        public void AddComponent(BaseComponent Component)
        {
            _childComponents.Add(Component);
            Component.OnComponentAttached(this);
            Component._parentObject = this;
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