using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class CollisionTiles : Tiles
    {
        public CollisionTiles(int tileId, Rectangle newRectangle, bool isDeadly)
        {
            
            if (isDeadly == true)
            {
                this.isDeadly = true;
                texture = Content.Load<Texture2D>("Tile" + tileId);
                //newRectangle.Size = newRectangle.Size - (new Point(64, 40));
                newRectangle.Height = 55;
                newRectangle.Width = 64;
                newRectangle.Location = new Point(newRectangle.Location.X, newRectangle.Location.Y + (64-55));
                this.Rectangle = newRectangle;
            }
            else
            {
                texture = Content.Load<Texture2D>("Tile" + tileId);
                this.Rectangle = newRectangle;
            }
        }
    }
}
