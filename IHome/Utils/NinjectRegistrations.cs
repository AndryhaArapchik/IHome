using Ninject.Modules;
using Services;
using Services.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RGB.Utils
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogService>().To<LogService>()
                .WithConstructorArgument("DataStorage", Enum.Parse(typeof(LogDataStorage), Properties.Resources.LogDataStorageType))
                .WithConstructorArgument("Connection", Properties.Resources.LogConnection);
            Bind<ILedControlService>().To<LedControlService>().WithConstructorArgument("port", Properties.Resources.COMPort);
            Bind<IAudioService>().To<AudioService>();
        }
    }
}