using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PCControl.Core.Interfaces
{
    public interface IApplicationService
    {
        void LauchApplication(string appName);
        void LauchApplicationWithArgs(string appName, string args);
        bool IsApplicationRunning(string appName);
        void CloseApplication(string appName);
        string[] GetInstalledApplications();
        string[] GetFavoriteApplications();
        void AddToFavorites(string appName);
        void RemoveFromfavorites(string appName);
    }
}