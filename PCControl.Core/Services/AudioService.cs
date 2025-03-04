using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using PCControl.Core.Interfaces;

namespace PCControl.Core.Services
{
    public class AudioService : IAudioService
    {
        private MMDeviceEnumerator _deviceEnumerator;
        public AudioService()
        {
            _deviceEnumerator = new MMDeviceEnumerator();
        }

        public float GetMasterVolume()
        {
            using(var device = GetDefaultDevice()){
                return device.AudioEndpointVolume.MasterVolumeLevelScalar;
            }
        }

        public void SetMasterVolume(float volume)
        {
            volume = Math.Max(0, Math.Min(1, volume));

            using( var device = GetDefaultDevice())
            {
                device.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
            }
        }

        public bool IsMuted()
        {
            using(var device = GetDefaultDevice())
            {
                return device.AudioEndpointVolume.Mute;
            }
        }

        public void SetMuted(bool mute)
        {
            using(var device = GetDefaultDevice())
            {
                device.AudioEndpointVolume.Mute = mute;
            }
        }

        public string[] GetPlaybackDevices()
        {
            var devices = new List<string>();

            var collection = _deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            foreach(var device in collection)
            {
                devices.Add($"{device.ID}|{device.FriendlyName}");
            }

            return devices.ToArray();
        }

        public void SetPlaybackDevices(string deviceId)
        {
            throw new NotImplementedException(
                "Setting default audio device requires Windows API P/Invoke. " +
                "Consider using NirCmd or another utility for this functionality.");
        }

        private MMDevice GetDefaultDevice()
        {
            return _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public void Dispose()
        {
            _deviceEnumerator?.Dispose();
        }
    }
}