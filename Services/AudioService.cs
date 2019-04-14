using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AudioService : IAudioService
    {
        public int GetValueVolume()
        {
            int volume = 0;
            NAudio.CoreAudioApi.MMDeviceEnumerator MMDE = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            NAudio.CoreAudioApi.MMDeviceCollection DevCol = MMDE.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.All);
            foreach (NAudio.CoreAudioApi.MMDevice dev in DevCol)
            {
                try
                {
                    if (dev.State == NAudio.CoreAudioApi.DeviceState.Active)
                    {
                        volume = Convert.ToInt32(dev.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
                    }
                }
                catch (Exception) { }
            }
            return volume;
        }
        public void ChangeVolume(int level)
        {
            NAudio.CoreAudioApi.MMDeviceEnumerator MMDE = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            NAudio.CoreAudioApi.MMDeviceCollection DevCol = MMDE.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.All);
            foreach (NAudio.CoreAudioApi.MMDevice dev in DevCol)
            {
                try
                {
                    if (dev.State == NAudio.CoreAudioApi.DeviceState.Active)
                    {
                        var newVolume = (float)Math.Max(Math.Min(level, 100), 0) / (float)100;
                        dev.AudioEndpointVolume.MasterVolumeLevelScalar = newVolume;
                        dev.AudioEndpointVolume.Mute = level == 0;
                    }
                }
                catch (Exception){}
            }
        }
    }
}
