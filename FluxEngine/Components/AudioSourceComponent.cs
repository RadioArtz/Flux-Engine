using Un4seen.Bass;
using Flux.Core;
using OpenTK.Mathematics;

namespace Flux.Types
{
    public class AudioSourceComponent : BaseComponent
    {
        private int handle = -1;
        private int channel = -1;
        private bool _initialized = false;

        #region settings
        public bool _autoplay = true;
        public string _filePath = "A:/dealermono.wav";
        public EAudioMode _audioMode = EAudioMode.Audio2D;
        public float _maxDistance = 128;
        /// <summary>
        /// Strength of audio falloff. Higher values correspond to faster falloff.
        /// 1 = Real world falloff
        /// 2 = 2x real world falloff
        /// Max value: 10
        /// </summary>
        public float _falloff = 1;
        /// <summary>
        /// Strength of the doppler effect. 
        /// Max value: 10
        /// </summary>
        public float _dopplerLevel = 1;
        public bool _shouldLoop = false;
        #endregion

        public AudioSourceComponent(string filePath, bool autoplay = true, EAudioMode audioMode = EAudioMode.Audio2D) 
        {
            _filePath = filePath;
            _audioMode = audioMode;
            _autoplay = autoplay;
            Init(); 
        }
       
        public AudioSourceComponent(string filePath, bool autoplay=true, float maxDistance = 128, float falloff = 1, float dopplerLevel = 1, EAudioMode audioMode = EAudioMode.Audio3D, bool shouldLoop = false) 
        { 
            _filePath = filePath;
            _autoplay = autoplay;
            _maxDistance = maxDistance;
            _falloff = falloff;
            _dopplerLevel = dopplerLevel;
            _audioMode = audioMode;
            _shouldLoop = shouldLoop;
            Init();
        }

        public void Init()
        {
            if (Engine.activeAudioListener == null)
            {
                Debug.LogError("NO ACTIVE AUDIO LISTENER! 3D Audio will NOT work! Forcing source to be 2D...");
                _audioMode = EAudioMode.Audio2D;
            }

            BASSFlag sampleFlag;
            if (_audioMode == EAudioMode.Audio3D)
            {
                sampleFlag = BASSFlag.BASS_SAMPLE_3D | BASSFlag.BASS_SAMPLE_MONO;
            }
            else
            {
                sampleFlag = BASSFlag.BASS_DEFAULT;
            }

            handle = Bass.BASS_SampleLoad(_filePath, 0, 0, 1, sampleFlag);
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
            {
                Bass.BASS_ChannelSet3DAttributes(channel, BASS3DMode.BASS_3DMODE_NORMAL, -1, -1, -1, -1, -1);
            }
            else
            {
                Bass.BASS_ChannelSet3DAttributes(channel, BASS3DMode.BASS_3DMODE_OFF, -1, -1, -1, -1, -1);
            }

            Bass.BASS_Set3DFactors(_maxDistance, _falloff, _dopplerLevel);

            if (_shouldLoop)
            {
                Bass.BASS_ChannelFlags(channel, BASSFlag.BASS_SAMPLE_LOOP, BASSFlag.BASS_SAMPLE_LOOP);
            }

            _initialized = true;

            if (_autoplay)
            {
                Play();
            }
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
            if (!_initialized || _audioMode == EAudioMode.Audio2D)
                return;

            Transform listenerTransform = Engine.activeAudioListener.GetTransform();
            Vector3 listenerVelocity = Engine.activeAudioListener.GetVelocity();

            BASS_3DVECTOR listenerPosition = ToBassVector(listenerTransform.Location);
            BASS_3DVECTOR listenerForward = ToBassVector(listenerTransform.Rotation.GetForwardVector() * -1);
            BASS_3DVECTOR listenerUp = ToBassVector(listenerTransform.Rotation.GetUpVector());
            BASS_3DVECTOR listenerVelocityVec = ToBassVector(listenerVelocity);

            Bass.BASS_Set3DPosition(listenerPosition, listenerVelocityVec, listenerForward, listenerUp);

            Vector3 objPosition = ParentObject.TransformComponent.transform.Location;
            Vector3 objVelocity = ParentObject.TransformComponent.GetVelocity();

            BASS_3DVECTOR soundPosition = ToBassVector(objPosition);
            BASS_3DVECTOR soundVelocity = ToBassVector(objVelocity);

            Bass.BASS_ChannelSet3DPosition(channel, soundPosition, null, soundVelocity);

            Bass.BASS_Set3DFactors(_maxDistance, _falloff, _dopplerLevel);
            Bass.BASS_Apply3D();
        }

        /// <summary>
        /// Convert OpenTK.Mathematics.Vector3 to BASS_3DVECTOR
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        private BASS_3DVECTOR ToBassVector(Vector3 vec)
        {
            return new BASS_3DVECTOR(vec.X, vec.Y, vec.Z);
        }

        public double GetCurrentPositionSeconds()
        {
            if (channel == 0)
            {
                Debug.LogError("Channel is not initialized.");
                return -1;
            }

            long positionBytes = Bass.BASS_ChannelGetPosition(channel);
            double positionSeconds = Bass.BASS_ChannelBytes2Seconds(channel, positionBytes);

            return positionSeconds;
        }
    }
    public enum EAudioMode
    {
        Audio2D,
        Audio3D
    }
}
