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
    /// <summary>
    /// CREATES THE ENEMIES LOADS THE TEXTURES etc.
    /// MAKES ENEMIES CHASE THE PLAYER
    /// </summary>
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

        float distancex;
        float oldDistancex;

        float distancey;
        float oldDistancey;
        protected bool _ghostEnemy;
        protected bool _hasjumped;
        public Enemy() { }


        public void Load(ContentManager Content, bool enemy, bool enemy2)
        {
            if (enemy == true && enemy2 == false)
            {
                texture = Content.Load<Texture2D>("Player");

                TextureData = new Color[texture.Width * texture.Height];
                texture.GetData(TextureData);
                distancex = 1000;
                oldDistancex = distancex;
                _ghostEnemy = false;

            }
            if (enemy == false && enemy2 == true)
            {
                texture = Content.Load<Texture2D>("Ghost");

                TextureData = new Color[texture.Width * texture.Height];
                texture.GetData(TextureData);
                distancex = 1000;

                oldDistancex = distancex;
                distancey = 1000;
                oldDistancey = distancey;
                _ghostEnemy = true;
            }




        }
        protected float playerDistancex;
        protected float playerDistancey;
        public void Update(GameTime gameTime, Player player)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            origin = new Vector2(0, 0);

          
                if (velocity.Y < 9.81)
                {
                    velocity.Y += 0.7f;
                }

                if (distancex <= 0)
                {
                    right = true;
                    velocity.X = 0f;

                }
                else if (distancex <= oldDistancex)
                {
                    right = false;
                    velocity.X = 0f;


                }
                if (right)
                {
                    distancex += 1; // +1
                }
                else
                {
                    distancex -= 1;
                }

                playerDistancex = player.Position.X - position.X;

                if (playerDistancex >= -500 && playerDistancex <= 500)
                {
                    if (playerDistancex < -1)
                    {
                        velocity.X = -(speed);
                    }
                    else if (playerDistancex >= 1)
                    {
                        velocity.X = speed;
                    }
                    else if (playerDistancex == 0)
                    {
                        velocity.X = 0f;
                    }


                }
          
            //YYYY
            if (_ghostEnemy == true )
            {
                if (distancey <= 0)
                {
                    right = true;

                    velocity.Y = 0f;
                }
                else if (distancey <= oldDistancey)
                {
                    right = false;

                    velocity.Y = 0f;

                }
                if (right)
                {
                    distancey += 1; // +1
                }
                else
                {
                    distancey -= 1;
                }

                playerDistancey = player.Position.Y - position.Y;

                if (playerDistancey >= -500 && playerDistancey <= 500 && playerDistancex >= -500 && playerDistancex <= 500)
                {
                    if (playerDistancey < -1)
                    {
                        velocity.Y = -(speed);
                    }
                    else if (playerDistancey >= 1)
                    {
                        velocity.Y = speed;
                    }
                    else if (playerDistancey == 0)
                    {
                        velocity.Y = 0f;
                    }


                }

            }
            
        }

        public void Collision(Rectangle newRectangle, int xOffset, int Yoffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                _hasjumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 6; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
                if (_ghostEnemy == false && _hasjumped == false)
                {
                    position.Y -= 5f;
                    velocity.Y = -10f;
                    _hasjumped = true;
                }
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                if (_ghostEnemy == true)
                {
                    position.X = newRectangle.X + rectangle.Width + 12; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
                }
                else if (_ghostEnemy==false && _hasjumped == false)
                {
                    
                   // position.Y -= 5f;
                   // velocity.Y = -10f;
                   // _hasjumped = true;
                    position.X = newRectangle.X + rectangle.Width + 12; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
                }
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
            if (velocity.X >= 0 && playerDistancex > 0)
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
