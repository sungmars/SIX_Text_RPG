using NAudio.Wave;

namespace SIX_Text_RPG
{
    public enum Clip
    {
        Music_Title,
        Music_Lobby,
        Music_Battle,
        Count
    }

    internal class AudioManager
    {
        public AudioManager()
        {
            var fileName = Enum.GetNames(typeof(Clip));
            for (int i = 0; i < fileName.Length - 1; i++)
            {
                audioSource[i] = new();

                string filePath = $"Audio/{fileName[i]}.wav";
                audioClips[i] = new(filePath);
            }
        }

        public static AudioManager Instance { get; private set; } = new();

        private readonly WaveOutEvent[] audioSource = new WaveOutEvent[(int)Clip.Count];
        private readonly AudioFileReader[] audioClips = new AudioFileReader[(int)Clip.Count];

        private Clip clip = Clip.Count;

        //public void PlayMusic(Clip audioClip, float volume = 0.2f)
        //{
        //    if (clip == audioClip)
        //    {
        //        return;
        //    }



        //    audioSource.Stop();
        //    audioSource.Init(musicClip);
        //    audioSource.Volume = volume;
        //    audioSource.Play();

        //    clip = audioClip;

        //    //Task.Run(FadeOut);
        //    //Task.Run(() => FadeIn(fileName));
        //}

        //public void PlayOneShot(string fileName, float volume = 0.4f)
        //{

        //}

        //private async Task FadeOutIn(string key)
        //{
        //    await FadeOut();

        //    await FadeIn();

        //    audioSource.Volume = volume;
        //    audioSource.PlaybackStopped += PlaybackStoppedHandler;

        //    this.key = key;
        //    this.volume = volume;
        //}

        //private async Task FadeIn()
        //{
        //    audioSource.Volume = 0.0f;
        //    audioSource.Play();

        //    while (audioSource.Volume < VOLUMES[])
        //    {
        //        await Task.Delay(10);

        //        volume += volume * 0.05f;
        //        audioSource.Volume = volume;
        //    }
        //}

        //private async Task FadeOut()
        //{
        //    while (musicFXSource.Volume > 0)
        //    {
        //        await Task.Delay(10);
        //        musicFXSource.Volume -= 0.5f;
        //    }

        //    musicFXSource.Stop();
        //}

        //private void PlaybackStoppedHandler(object? sender, StoppedEventArgs? e)
        //{
        //    if (musicClip == null)
        //    {
        //        return;
        //    }

        //    musicClip.Position = 0;
        //    audioSource.Play();
        //}
    }
}