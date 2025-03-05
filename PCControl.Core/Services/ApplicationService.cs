using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using PCControl.Core.Interfaces;

namespace PCControl.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly string _favoritesFilePath;
        private HashSet<string> _favorites;
        public ApplicationService()
        {
            _favoritesFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PCControl",
                "favorites.txt");

            Directory.CreateDirectory(Path.GetDirectoryName(_favoritesFilePath));
            LoadFavorites();
        }
        
        private void LoadFavorites()
        {
            _favorites = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if(File.Exists(_favoritesFilePath))
            {
                var lines = File.ReadAllLines(_favoritesFilePath);
                foreach(var line in lines)
                {
                    _favorites.Add(line.Trim());
                }
            }
        }

        private void SaveFavorites(){
            File.WriteAllLines(_favoritesFilePath, _favorites);
        }

        public void LauchApplication(string appName)
        {
            Process.Start(appName);
        }

        public void LauchApplicationWithArgs(string appName, string args)
        {
            Process.Start(appName, args);
        }

        public bool IsApplicationRunning(string appName)
        {
            var process = Process.GetProcessesByName(
                Path.GetFileNameWithoutExtension(appName));
            
            return process.Length > 0;
        }

        public void CloseApplication(string appName)
        {
            var processes = Process.GetProcessesByName(
                Path.GetFileNameWithoutExtension(appName));
            
            foreach(var process in processes)
            {
                try {
                    process.CloseMainWindow();

                    if(!process.WaitForExit(3000))
                    {
                        process.Kill();
                    }
                }
                catch(Exception ex)
                {
                    
                }
                finally
                {
                    process.Dispose();
                }
            }
        }

        public string[] GetInstalledApplications()
        {
            var apps = new List<string>();

            string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            string programsPath = Path.Combine(startMenuPath, "Programs");

            if(Directory.Exists(programsPath))
            {
                foreach(var file in Directory.GetFiles(programsPath, "*.link", SearchOption.AllDirectories))
                {
                    apps.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

            using(var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths"))
            {
                if(key != null)
                {
                    foreach(var subkeyName in key.GetSubKeyNames())
                    {
                        apps.Add(Path.GetFileNameWithoutExtension(subkeyName));
                    }
                }
            }

            return apps.Distinct().OrderBy(c => c).ToArray();
        }

        public string[] GetFavoriteApplications()
        {
            return _favorites.ToArray();
        }

        public void AddToFavorites(string appName)
        {
            _favorites.Add(appName);
            SaveFavorites();
        }            

        public void RemoveFromfavorites(string appName)
        {
            _favorites.Remove(appName);
            SaveFavorites();
        }
    }
}