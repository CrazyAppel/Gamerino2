using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Speedyboi : Enemy
    {
        private Texture2D texture;

        Slowboi slow;
        public bool speedybool =false;
        public void Load(ContentManager Content)
        {

            texture = Content.Load<Texture2D>("Player");
            

        }
        public Speedyboi(/*Texture2D texture,*/ Vector2 position, Player player, bool speedy) 
        {
            //slow.slowy = false;
            speedybool = speedy;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 0, 64);
            playerDistance = 200;
            speed = 1f;
            
        }

    }
}
