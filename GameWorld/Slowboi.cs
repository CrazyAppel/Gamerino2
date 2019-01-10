using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Slowboi : Enemy
    {
        private Texture2D texture;
        Speedyboi speedyboi;
        public bool slowy=false;

        public Slowboi(/*Texture2D texture,*/ Vector2 position, Player player, bool slowyboi)
        {
            //speedyboi.speedy = false;
            //public slowy = true;
            slowy = slowyboi;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 0, 64);
            playerDistance = 200;
            speed = 0.2f;
        }
    }
}
