using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    public class Camera
    {
        /// <summary>
        /// DETERMINS THE MIDDLE OF THE SCREEN FOR THE PLAYER (AND TOPLEFT FOR IMAGES)
        /// </summary>
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 centre;
        private Viewport viewport;
        public Vector2 topLeft;
        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width/2)
            {
                centre.X = viewport.Width / 2;
                topLeft.X = viewport.Width / 2 - 400;
            }
            else if (position.X > xOffset - (viewport.Width/2))
            {
                centre.X = xOffset - (viewport.Width / 2);
                topLeft.X = xOffset - (viewport.Width / 2) - 400;
            }
            else
            {
                centre.X = position.X;
                topLeft.X = position.X - 400;
            }

            if (position.Y < viewport.Height / 2)
            {
                centre.Y = viewport.Height / 2;
                topLeft.Y = (viewport.Height / 2) - 240;
            }
            else if (position.Y > yOffset - (viewport.Height / 2))
            {
                centre.Y = yOffset - (viewport.Height / 2);
                topLeft.Y = yOffset - (viewport.Height / 2) - 240;
            }
            else
            {
                centre.Y = position.Y;
                topLeft.Y = position.Y - 240;
            }
            
            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0));
        }
    }
}
