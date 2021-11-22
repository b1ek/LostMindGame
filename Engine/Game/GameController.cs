using LostMind.Engine.UI;
using LostMind.Engine.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;


namespace LostMind.Engine.Game {
    public class GameController {
        List<GamePlace> places = new List<GamePlace>();
        int placeSel = 0;
        bool placeAdded = false;
        BackgroundWorker worker;
        public GameController() {
        }
        public void Start() {
            if (!placeAdded) throw new Exception("No places in the game controller!");
            worker = new BackgroundWorker();
            worker.DoWork += work;
        }
        void work(object sender, EventArgs e) {
            places[placeSel].Update();
            Update();
        }
        void Update() {
        }

        /**
         * <summary>
         *  Add a place to the controller list.<br/>
         *  Don't forget to add at least one before starting the controller!
         * </summary>
         */
        public void AddPlace(GamePlace value) {
            places.Add(value);
            placeAdded = true;
        }
        /**
         * <summary>
         *  Remove place from list.<br/>
         * </summary>
         */
        public void RemovePlace(int index) {
            places.RemoveAt(index);
        }
        /**
         * <summary>
         *  Remove place from list.<br/>
         * </summary>
         */
        public void RemovePlace(GamePlace value) {
            places.Remove(value);
        }
        /**
         * <summary>
         *  Remove place from list.<br/>
         * </summary>
         */
        public void RemovePlace(string id) {
            for (int c = 0; c > places.Count; c++) {
                if (places[c].Id == id) {
                    break;
                    places.RemoveAt(c);
                }
            }

        }
    }
}
