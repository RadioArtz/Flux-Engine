
namespace Flux.Types
{
    public class BaseComponent
    {
        private AActor _parentObject;
        public AActor ParentObject => _parentObject;
        public bool canEverTick = true;
        public BaseComponent()
        {
        }
        public virtual void OnComponentAttached(AActor parentObject)
        {
        }
        public virtual void OnComponentDestroyed()
        {
        }
        public virtual void OnTick(float delta) 
        { 
        }
        public void SetParent(AActor newParent)
        {
            _parentObject = newParent;
        }
    }
}
