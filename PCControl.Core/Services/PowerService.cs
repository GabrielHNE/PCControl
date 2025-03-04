using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NAudio.Wave;
using PCControl.Core.Interfaces;

namespace PCControl.Core.Services
{
    public class PowerService : IPowerService
    {
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        public void Shutdown(int delayInSeconds = 0) => Process.Start("shutdown", $"/s /t {delayInSeconds}");

        public void Restart(int delayInSeconds = 0) => Process.Start("shutdown", $"/r /t {delayInSeconds}");
        
        public void Sleep() => SetSuspendState(false, false, false);
        
        public void Hibernate() => SetSuspendState(true, false, false);

        public void AbortShutdown() => Process.Start("shutdown", "/a");
    }
}