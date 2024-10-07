
namespace Flux.Types
{
    public class BaseComponent
    {
        private AActor _parentObject;
        public AActor ParentObject => _parentObject;
        public BaseComponent()
        {
        }
        public virtual void OnComponentAttached(AActor parentObject)
        {
        }
        public virtual void OnComponentDestroyed()
        {
        }
        public void SetParent(AActor newParent)
        {
            _parentObject = newParent;
        }
    }
}
