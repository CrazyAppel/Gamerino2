using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    internal class Spike :Template
    {
        private Vector2 position;
        public Color[] textureData { get; set; }

        public Spike(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
    }
}

