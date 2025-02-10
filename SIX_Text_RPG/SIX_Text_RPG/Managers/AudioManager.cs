using NAudio.Wave;

namespace SIX_Text_RPG
{
    internal class AudioManager
    {
        public static AudioManager Instance { get; private set; } = new();

        private readonly WaveOutEvent audioSource = new();

        private AudioFileReader? audioFile;
        private float volume;

        public void PlayMusic(string fileName, float volume = 0.2f)
        {
            audioSource.Stop();

            string filePath = $"Audio/{fileName}.wav";
            audioFile = new AudioFileReader(filePath);
            audioSource.Init(audioFile);

            audioSource.PlaybackStopped += PlaybackStoppedHandler;

            audioSource.Volume = volume;
            audioSource.Play();

            this.volume = volume;
        }

        public void PlayOneShot()
        {

        }

        private void PlaybackStoppedHandler(object? sender, StoppedEventArgs? e)
        {
            if (audioFile == null)
            {
                return;
            }

            audioFile.Position = 0;
            audioSource.Volume = volume;
            audioSource.Play();
        }

        private void Stop()
        {
            audioSource.PlaybackStopped -= PlaybackStoppedHandler;
            audioSource.Stop();
        }
    }
}