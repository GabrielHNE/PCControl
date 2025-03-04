using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCControl.Core.Interfaces
{
    public interface IPowerService
    {
        void Shutdown(int delayInSeconds = 0);
        void Restart(int delayInSeconds = 0);
        void Sleep();
        void Hibernate();
        void AbortShutdown();
    }
}