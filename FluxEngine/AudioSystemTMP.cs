using Un4seen.Bass;

namespace Flux.Core
{
    public static class AudioSystemTMP
    {
        static string audioPath = @"C:\Users\mathi\AppData\Local\AxiomGame\Songs\kaufland - kaufland/audio.mp3";
        static int handle;

        static AudioSystemTMP()
        {
            Bass.BASS_Init(-1,44100,0,(IntPtr)0);   
        }
        public static void PlaySound(IntPtr devicePtr)
        {
            handle = Bass.BASS_StreamCreateFile(audioPath, 0, 0, 0);
            Bass.BASS_ChannelPlay(handle, false);
        }
    }
}