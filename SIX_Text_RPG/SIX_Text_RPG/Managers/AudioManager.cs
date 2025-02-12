using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SIX_Text_RPG
{
    public enum AudioClip
    {
        Music_Title,
        Music_Battle,
        Music_LevelUp,
        Music_BossLobby,
        Music_BossBattle,
        Music_Manager,

        SoundFX_Appear,
        SoundFX_Avoid,
        SoundFX_Click,
        SoundFX_ClockTicking,
        SoundFX_Confirm,
        SoundFX_Damage1,
        SoundFX_Damage2,
        SoundFX_Damage3,
        SoundFX_Damage4,
        SoundFX_DrawLine,
        SoundFX_DrawMenu,
        SoundFX_Error,
        SoundFX_Hit1,
        SoundFX_Hit2,
        SoundFX_Hit3,
        SoundFX_Landed,
        SoundFX_Potion,
        SoundFX_TaskDone,
        SoundFX_WriteAnim,
        SoundFX_Equip,
        SoundFX_Cashier,

        Count
    }

    internal class AudioManager
    {
        public AudioManager()
        {
            var fileName = Enum.GetNames(typeof(AudioClip));
            for (int i = 0; i < fileName.Length - 1; i++)
            {
                WaveOutEvent audioSource = new();
                audioSources[i] = audioSource;

                string filePath = $"Audio/{fileName[i]}.wav";
                WaveFileReader audioClip = new(filePath); ;
                audioSource.Init(audioClip);
                audioClips[i] = audioClip;
            }
        }

        public static AudioManager Instance { get; private set; } = new();

        private readonly WaveOutEvent[] audioSources = new WaveOutEvent[(int)AudioClip.Count];
        private readonly WaveFileReader[] audioClips = new WaveFileReader[(int)AudioClip.Count];

        private AudioClip audioClip = AudioClip.Count;
        private FadeInOutSampleProvider? sampleProvider;

        public void Play(AudioClip audioClip)
        {
            if (audioClip == this.audioClip)
            {
                return;
            }

            int index = (int)audioClip;
            var clip = audioClips[index];
            clip.Position = 0;

            var audioSource = audioSources[index];
            if (audioClip.ToString().Contains("SoundFX"))
            {
                audioSource.Play();
                return;
            }

            if (this.audioClip != AudioClip.Count)
            {
                Stop(this.audioClip);
            }

            var source = clip.ToSampleProvider();
            sampleProvider = new FadeInOutSampleProvider(source);
            sampleProvider.BeginFadeIn(2000);

            audioSource.Stop();
            audioSource.Init(sampleProvider);
            audioSource.Play();
            audioSource.PlaybackStopped += PlaybackStoppedHandler;
            audioSource.Volume = 0.2f; // TODO: TEST CODE

            this.audioClip = audioClip;
        }

        public void Stop(AudioClip audioClip)
        {
            var audioSource = audioSources[(int)audioClip];
            if (audioClip.ToString().Contains("SoundFX"))
            {
                audioSource.Stop();
                return;
            }

            if (sampleProvider == null)
            {
                return;
            }

            sampleProvider.BeginFadeOut(1000);
            audioSource.PlaybackStopped -= PlaybackStoppedHandler;
        }

        private void PlaybackStoppedHandler(object? sender, StoppedEventArgs? e)
        {
            int index = (int)audioClip;
            audioClips[index].Position = 0;
            audioSources[index].Play();
        }
    }
}