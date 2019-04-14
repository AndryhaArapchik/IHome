using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAudioService
    {
        int GetValueVolume();
        void ChangeVolume(int volume);
    }
}
