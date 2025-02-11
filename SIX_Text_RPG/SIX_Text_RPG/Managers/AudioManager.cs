using NAudio.Wave;

namespace SIX_Text_RPG
{
    public enum AudioClip
    {
        Music_Title,
        Music_Battle,

        SoundFX_Click,
        SoundFX_ClockTicking,
        SoundFX_Damage1,
        SoundFX_Damage2,
        SoundFX_Damage3,
        SoundFX_Damage4,
        SoundFX_DrawLine,
        SoundFX_DrawMenu,
        SoundFX_Hit1,
        SoundFX_Hit2,
        SoundFX_Hit3,
        SoundFX_TaskDone,
        SoundFX_WriteAnim,

        Count
    }

    internal class AudioManager
    {
        private readonly float VOLUME = 0.5f;

        public AudioManager()
        {
            var fileName = Enum.GetNames(typeof(AudioClip));
            for (int i = 0; i < fileName.Length - 1; i++)
            {
                WaveOutEvent audioSource = new();
                audioSources[i] = audioSource;

                string filePath = $"Audio/{fileName[i]}.wav";
                AudioFileReader audioClip = new(filePath); ;
                audioSource.Init(audioClip);
                if (fileName[i].Contains("Music"))
                {
                    audioSource.PlaybackStopped += PlaybackStoppedHandler;
                }

                audioClips[i] = audioClip;
            }
        }

        public static AudioManager Instance { get; private set; } = new();

        private readonly WaveOutEvent[] audioSources = new WaveOutEvent[(int)AudioClip.Count];
        private readonly AudioFileReader[] audioClips = new AudioFileReader[(int)AudioClip.Count];

        private AudioClip musicClip = AudioClip.Count;

        public void Play(AudioClip audioClip)
        {
            if (musicClip == audioClip)
            {
                return;
            }

            int index = (int)audioClip;
            audioClips[index].Position = 0;

            if (audioClip.ToString().Contains("SoundFX"))
            {
                audioSources[index].Play();
                return;
            }

            musicClip = audioClip;
            Task.Run(FadeIn);
        }

        public void Stop(AudioClip audioClip)
        {
            if (audioClip.ToString().Contains("SoundFX"))
            {
                audioSources[(int)audioClip].Stop();
                return;
            }

            Task.Run(FadeOut);
        }

        private async Task FadeIn()
        {
            var audioSource = audioSources[(int)musicClip];
            audioSource.Volume = 0.0f;
            audioSource.Play();

            while (audioSource.Volume < VOLUME)
            {
                await Task.Delay(10);
                audioSource.Volume += VOLUME * 0.01f;
            }

            audioSource.Volume = VOLUME;
        }

        private async Task FadeOut()
        {
            int index = (int)musicClip;
            var audioSource = audioSources[index];
            while (audioSource.Volume > 0)
            {
                await Task.Delay(10);
                audioSource.Volume -= VOLUME * 0.01f;
            }

            audioSource.Stop();

            audioClips[index].Position = 0;
            musicClip = AudioClip.Count;
        }

        private void PlaybackStoppedHandler(object? sender, StoppedEventArgs? e)
        {
            audioClips[(int)musicClip].Position = 0;
            audioSources[(int)musicClip].Play();
        }
    }
}