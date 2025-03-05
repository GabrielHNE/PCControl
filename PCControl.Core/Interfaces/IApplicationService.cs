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