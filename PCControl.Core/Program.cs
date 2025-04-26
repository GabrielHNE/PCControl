// See https://aka.ms/new-console-template for more information
using PCControl.Core.Services;

Console.WriteLine("Hello, World!");


var audio = new AudioService();

audio.SetMuted(true);