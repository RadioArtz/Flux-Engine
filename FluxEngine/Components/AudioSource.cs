using Un4seen.Bass;
using Flux.Core;
using OpenTK.Mathematics;

namespace Flux.Types
{
    public class AudioSource : BaseComponent
    {
        private int handle = -1;
        private int channel = -1;
        private bool initialized = false;

        #region settings
        public bool _autoplay = true;
        public string _filePath = "A:/dealermono.wav";
        public EAudioMode _audioMode = EAudioMode.Audio2D;
        public float _maxDistance = 128;
        public float _falloff = 1;
        public float _dopplerLevel = 1;
        #endregion

        public AudioSource(string filePath) 
        {
            _filePath = filePath;
            Init(); 
        }
        public AudioSource(string filePath, EAudioMode audioMode = EAudioMode.Audio2D) 
        {
            _filePath = filePath;
            _audioMode = audioMode; 
            Init(); 
        }
        public AudioSource(string filePath, bool autoplay=true, float maxDistance = 128, float falloff = 1, float dopplerLevel = 1, EAudioMode audioMode = EAudioMode.Audio3D) 
        { 
            _filePath = filePath;
            _autoplay = autoplay;
            _maxDistance = maxDistance;
            _falloff = falloff;
            _dopplerLevel = dopplerLevel;
            _audioMode = audioMode;
            Init();
        }

        public void Init()
        {
            if(Engine.activeAudioListener == null)
            {
                Debug.LogError("NO ACTIVE AUDIO LISTENER! 3D Audio will NOT work! Forcing source to be 2D...");
                _audioMode = EAudioMode.Audio2D;
            }

            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                Debug.LogError("BASS initialization failed with error: " + Bass.BASS_ErrorGetCode());
                return;
            }

            if(_audioMode== EAudioMode.Audio3D)
                handle = Bass.BASS_SampleLoad(_filePath, 0, 0, 1, BASSFlag.BASS_SAMPLE_3D);
            else
                handle = Bass.BASS_SampleLoad(_filePath, 0, 0, 1, BASSFlag.BASS_DEFAULT);

            if (handle == 0)
            {
                Debug.LogError("Sample load failed with error: " + Bass.BASS_ErrorGetCode());
                return;
            }

            channel = Bass.BASS_SampleGetChannel(handle, BASSFlag.BASS_DEFAULT);
            if (channel == 0)
            {
                Debug.LogError("Failed to get channel with error: " + Bass.BASS_ErrorGetCode());
                return;
            }

            if (_audioMode == EAudioMode.Audio3D)
                Bass.BASS_ChannelSet3DAttributes(channel, BASS3DMode.BASS_3DMODE_NORMAL, -1, -1, -1, -1, -1);
            else
                Bass.BASS_ChannelSet3DAttributes(channel, BASS3DMode.BASS_3DMODE_OFF, -1, -1, -1, -1, -1);

            Bass.BASS_Set3DFactors(_maxDistance, _falloff, _dopplerLevel);

            if (_autoplay)
                Play();
        }
        public void Play()
        {
            if (!Bass.BASS_ChannelPlay(channel, false))
            {
                Debug.LogError("Channel play failed with error: " + Bass.BASS_ErrorGetCode());
            }
        }
        public override void OnTick(float delta)
        {
            if (!initialized)
                return;
            Transform listenerTransform = Engine.activeAudioListener.GetTransform();
            Vector3 velocity = Engine.activeAudioListener.GetVelocity();

            if (_audioMode == EAudioMode.Audio2D) 
                return;
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
    public enum EAudioMode
    {
        Audio2D,
        Audio3D
    }
}
