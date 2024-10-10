using Flux;
using Flux.Types;
using OpenTK.Mathematics;

namespace Flux.Types
{
    public class AudioListenerComponent : BaseComponent
    {
        public AudioListenerComponent()
        {
            SetAsActiveListener();
        }
        public void SetAsActiveListener()
        {
            Engine.activeAudioListener = this;
        }
        public Vector3 GetVelocity() {  return ParentObject.TransformComponent.GetVelocity(); }
        public Transform GetTransform() { return ParentObject.TransformComponent.transform; }
    }
}
