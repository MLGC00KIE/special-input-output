using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speech_RPG
{
    class player
    {
        private int lives = 100;
        private Random r = new Random();
        
        public int Heal()
        {
            lives += 50;
            return lives;
        }

        public int TakeDamage()
        {
            int damageToTake = r.Next(1, 24);
            lives -= damageToTake;
            return damageToTake;
        }

        public int GetLives()
        {
            return lives;
        }
    }
}
