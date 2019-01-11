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

        public Vector2 scorelocation;
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }
        
        public Vector2 centre;
        public Vector2 topLeft;
        private Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }
        
        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            if (position.X < viewport.Width/2)//if player is less than 400. dan blijft de camera daar die gaat niet meer naar links
            {
               
                centre.X = viewport.Width / 2;
                topLeft.X = viewport.Width/2-400;
            }
            else if (position.X > xOffset - (viewport.Width/2))// niet meer naar rechts aan de rechterkant
            {
                centre.X = xOffset - (viewport.Width / 2);
                topLeft.X = xOffset - (viewport.Width/2)-400;
            }
            else
            {

                centre.X = position.X;
                topLeft.X = position.X-400;
            }

            if (position.Y < viewport.Height / 2)
            {
                centre.Y = viewport.Height / 2;
                topLeft.Y = (viewport.Height/2)-240;
            }
            else if (position.Y > yOffset - (viewport.Height / 2))
            {
                centre.Y = yOffset - (viewport.Height / 2);
                topLeft.Y = yOffset - (viewport.Height/2) -240;
            }
            else
            {
                centre.Y = position.Y;
                topLeft.Y = position.Y -240;
            }
            
            scorelocation = new Vector2(topLeft.X, topLeft.Y);
            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0));
        }
        
    }
}
