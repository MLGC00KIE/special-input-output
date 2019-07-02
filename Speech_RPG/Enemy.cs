using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speech_RPG
{
    class Enemy
    {
        private int lives = 100;
        private Random r = new Random();

        public int TakeDamage()
        {
            int damageToTake = r.Next(75, 300);
            lives -= damageToTake;
            return damageToTake;
        }

        public int GetLives()
        {
            return lives;
        }
    }
}
