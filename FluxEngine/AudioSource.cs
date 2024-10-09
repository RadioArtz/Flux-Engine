using Un4seen.Bass;
using Flux.Core;
using OpenTK.Mathematics;

namespace Flux.Types
{
    public class AudioSource : BaseComponent
    {
        private int handle = -1;
        private int channel = -1;

        public void Init()
        {
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                Console.WriteLine("BASS initialization failed with error: " + Bass.BASS_ErrorGetCode());
                return;
            }

            handle = Bass.BASS_SampleLoad("A:/dealermono.wav", 0, 0, 1, BASSFlag.BASS_SAMPLE_3D);
            if (handle == 0)
            {
                Console.WriteLine("Sample load failed with error: " + Bass.BASS_ErrorGetCode());
                return;
            }

            channel = Bass.BASS_SampleGetChannel(handle, BASSFlag.BASS_DEFAULT);
            if (channel == 0)
            {
                Console.WriteLine("Failed to get channel with error: " + Bass.BASS_ErrorGetCode());
                return;
            }

            Bass.BASS_ChannelSet3DAttributes(channel, BASS3DMode.BASS_3DMODE_NORMAL, -1, -1, -1, -1, -1);

            if (!Bass.BASS_ChannelPlay(channel, false))
            {
                Console.WriteLine("Channel play failed with error: " + Bass.BASS_ErrorGetCode());
            }

            Bass.BASS_Set3DFactors(75, 0.75f, 3);
        }

        public void Update(Transform listenerTransform,Vector3 velocity)
        {
            BASS_3DVECTOR listenerPosition = new BASS_3DVECTOR(listenerTransform.Location.X, listenerTransform.Location.Y, listenerTransform.Location.Z);
            BASS_3DVECTOR listenerFront = new BASS_3DVECTOR(
                listenerTransform.Rotation.GetForwardVector().X * -1,
                listenerTransform.Rotation.GetForwardVector().Y * -1,
                listenerTransform.Rotation.GetForwardVector().Z * -1);
            BASS_3DVECTOR listenerTop = new BASS_3DVECTOR(
                listenerTransform.Rotation.GetUpVector().X,
                listenerTransform.Rotation.GetUpVector().Y,
                listenerTransform.Rotation.GetUpVector().Z);
            BASS_3DVECTOR listenerVelo = new BASS_3DVECTOR(
                velocity.X,
                velocity.Y,
                velocity.Z);
            Bass.BASS_Set3DPosition(listenerPosition, listenerVelo, listenerFront, listenerTop);

            Vector3 objPos = ParentObject.TransformComponent.transform.Location;
            BASS_3DVECTOR soundPosition = new BASS_3DVECTOR(objPos.X, objPos.Y, objPos.Z);
            //Implement velocity for obj here!!
            Bass.BASS_ChannelSet3DPosition(channel, soundPosition, null, null);
            
            Bass.BASS_Apply3D();
        }
    }
}
