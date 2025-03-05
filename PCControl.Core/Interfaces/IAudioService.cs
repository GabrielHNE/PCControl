namespace PCControl.Core.Interfaces
{
    public interface IAudioService
    {
        float GetMasterVolume();
        void SetMasterVolume(float volume);
        bool IsMuted();
        void SetMuted(bool mute);
        string[] GetPlaybackDevices();
        void SetPlaybackDevices(string deviceId);
    }
}