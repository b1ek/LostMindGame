using LostMind.Engine.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Sound
{
    public class MusicNotFoundException : Exception { }
    public static class SoundController
    {
        public enum Music {
            Chill,
            Trauma,
            Hell,
            Horror,
            MainMenu,
            Halloween
        }
        public static readonly string musicFolderPath = Environment.CurrentDirectory + @"\Resources\Music";
        public static void playMusic(Music type) {
            if (type == Music.MainMenu) {
                //try {
                    var files = Directory.GetFiles(musicFolderPath, "*.*.mp3"); Console.Write(files[0]);
                //} catch (Exception) { }
            }
        }
        public static void playMusic(Music type, int num) {

        }
    }
}
