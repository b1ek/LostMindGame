using LostMind.Classes.UI;
using LostMind.Classes.User;
using LostMind.Classes.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Threading;

namespace LostMind.Classes.GameController {
    public struct GameControllerSettings {
        public int SceneViewportWidth;
        public int SceneViewportHeight;
        public int SceneViewportXPlace;
        public int SceneViewportYPlace;

        public int TitleViewportWidth;
        public int TitleViewportHeight;
        public int TitleViewportXPlace;
        public int TitleViewportYPlace;

        public string TitleText;
        public string TItleSep;
    }
    public struct GameControllerData {
        public List<Game.GamePlace> Places;

    }
    public class GameController {

        public GameController(GameControllerSettings controllerSettings, GameControllerData controllerData) {
            settings = controllerSettings;
            data = controllerData;
        }

        #region Settings/Data
        GameControllerSettings settings;
        GameControllerData data;
        public void Configure(GameControllerData gameControllerData) => data = gameControllerData;
        public void Configure(GameControllerSettings gameControllerSettings) => settings = gameControllerSettings;
        public GameControllerData Data => data;
        public GameControllerSettings Settings => settings;
        #endregion
        #region Navigation
        int CurrentScene = 0;
        /**<summary>Method that provides scene navigation<br/>Note: up and down both true does nothing.</summary>*/
        public void Navigate(bool up, bool down) {
            if (data.Places.Count == 0) return;
            if (up && down) return;
            if (up) CurrentScene+=1;
            if (down) CurrentScene-=1;
        }
        public void Navigate(int position) {
            if (data.Places.Count == 0 | data.Places.Count < position
               | position < 0) return;
            CurrentScene = position;

        }
        #endregion
        #region Places stuff
        public void ShowPlace() {
            data.Places[CurrentScene].Display();
        }
        #endregion
    }
}
