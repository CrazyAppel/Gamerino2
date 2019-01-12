using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Enemy
    {
        private Texture2D texture;
        public Rectangle rectangle;
        public Color[] TextureData { get; set; }
        public Vector2 position;
        private Vector2 origin;
        public Vector2 velocity;
        public float speed;

        float rotation = 0f;

        bool right;
        float distance;
        float oldDistance;

        public Enemy() { }


        public void Load(ContentManager Content, bool enemy, bool enemy2)
        {
            if (enemy == true && enemy2 == false)
            {
                texture = Content.Load<Texture2D>("Player");

                TextureData = new Color[texture.Width * texture.Height];
                texture.GetData(TextureData);
                distance = 1000;

                oldDistance = distance;
            }
            if (enemy == false && enemy2 == true)
            {
                texture = Content.Load<Texture2D>("Ghost");

                TextureData = new Color[texture.Width * texture.Height];
                texture.GetData(TextureData);
                distance = 1000;

                oldDistance = distance;
            }




        }
        protected float playerDistance;
        public void Update(GameTime gameTime, Player player)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            origin = new Vector2(0, 0);

            if (velocity.Y < 10)
            {
                velocity.Y += 0.7f;
            }

            if (distance <= 0)
            {
                right = true;
                velocity.X = 0f;
            }
            else if (distance <= oldDistance)
            {
                right = false;
                velocity.X = 0f;
            }
            if (right)
            {
                distance += 1; // +1
            }
            else
            {
                distance -= 1;
            }

            playerDistance = player.Position.X - position.X;

            if (playerDistance >= -500 && playerDistance <= 500)
            {
                if (playerDistance < -1)
                {
                    velocity.X = -(speed);
                }
                else if (playerDistance >= 1)
                {
                    velocity.X = speed;
                }
                else if (playerDistance == 0)
                {
                    velocity.X = 0f;
                }
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int Yoffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 6; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + rectangle.Width + 12; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }


            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > Yoffset - rectangle.Height) position.Y = Yoffset - rectangle.Height;

            /*  foreach (var Spike in spikes)
              {
                  if (IntersectsPixel(this.rectangle, this.textureData, Spike.Rectangle, Spike.textureData))
                  {
                      hasDied = true;
                  }
              }*/
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X >= 0 && playerDistance > 0)
            {
                //spriteBatch.Draw(texture, position, null, Color.Red, rotation, origin, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, rectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
                //spriteBatch.Draw()

            }
            else
            {
                //spriteBatch.Draw(texture, position, null, Color.Red, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(texture, rectangle, null, Color.White, rotation, origin, SpriteEffects.FlipHorizontally, 0f);

            }
        }
    }
}
