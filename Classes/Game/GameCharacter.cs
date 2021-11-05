using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.Game {
    public enum Condition {
        Alive,
        Deceased,
        Damaged,
        Dead,
    }
    public class GameCharacter {
        Condition currentCondition;
        public Condition Condition { get => currentCondition; }
        public struct Stats {
            byte health;
            byte hunger;
            byte energy;
            byte psychHealth;
            byte friendship;
        }

        Stats currentStats;
        public Stats Stat { get => currentStats; }

        bool isEvil;

        public GameCharacter(Condition condition, Stats _stats, bool evil = false) {
            currentCondition = condition;
            currentStats = _stats;
            isEvil = evil;

        }
    }
}
