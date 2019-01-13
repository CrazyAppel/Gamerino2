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
        /// <summary>
        /// MAKES THE TILES SO YOU CAN COLLIDE WITH THEM
        /// SETS TEXTURE TO TILE
        /// </summary>
       
        public CollisionTiles(int tileId, Rectangle newRectangle, bool isDeadly,bool isEnd, string side)
        {
            
            if (isDeadly == true)
            {
                this.isDeadly = true;
                texture = Content.Load<Texture2D>("Tile" + tileId);
                //newRectangle.Size = newRectangle.Size - (new Point(64, 40));
                newRectangle.Height = texture.Height;
                newRectangle.Width = 64;
                newRectangle.Location = new Point(newRectangle.Location.X, newRectangle.Location.Y + (64-texture.Height));
                this.Rectangle = newRectangle;
            }
            if (isEnd == true)
            {
                texture = Content.Load<Texture2D>("Tile" + tileId);
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
