using GameWorld.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Background
    {

         public Matrix Transform { get; private set; }
          public Texture2D texture;
          public Rectangle rectangle;

         public void Follow(Vector2 target)
          {
              var position = Matrix.CreateTranslation(-target.X, -target.Y,0);

              var offset = Matrix.CreateTranslation(1800,640,0);

              Transform = position * offset;
          }
          public void Draw(SpriteBatch spriteBatch)
          {
              spriteBatch.Draw(texture, rectangle, Color.White);
          }
      }

      class Scrolling : Background
      {
          public Scrolling(Texture2D newTexture, Rectangle newRectangle)
          {
              texture = newTexture;
              rectangle = newRectangle;
          }
          public void Update()
          {
              rectangle.X -= 3;
          }
      }
      
    }
