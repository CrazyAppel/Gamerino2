using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    class Player
    {

        public int score = 0;

        public bool hasMovement;

        private Texture2D texture;
        private Vector2 position = new Vector2(1000,150);
        public Vector2 velocity;
        private Rectangle rectangle;
        public Color[] textureData { get; set; }
        private bool hasJumped = false;
        public bool hasDied = false;
        float rotation = 0f;
        private bool looksRight = false;
        public Vector2 origin;
        public Vector2 Position
        {
            get { return position; }
        }

        public Player() { }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Player");
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            //rectangle = new Rectangle((int)position.X, (int)position.Y, 64, 64);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            origin = new Vector2(0, 0);

            Movement(gameTime);
            if (velocity.Y < 10)
            {
                hasJumped = true;
                velocity.Y += 0.7f;
            }
        }


        private void Movement(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                looksRight = true;
                hasMovement = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                looksRight = false;
                hasMovement = true;
            }
            else
            {
                velocity.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -14f;
                hasJumped = true;
                hasMovement = true;
            }
        }

        public void checkEnemyCollision(Enemy enemy, SoundEffect effect)
        {
            if (IntersectsPixel(this.rectangle, this.textureData, enemy.rectangle, enemy.TextureData))
            {
                //snowball.IsRemoved = true;
                //hasDied = true;
                position = new Vector2(450, 100);
                effect.Play();
            }
        }

        public void checkCoinColision(Coin coin, SoundEffect effect)
        {
            if (IntersectsPixel(this.rectangle, this.textureData, coin.rectangle, coin.TextureData))
            {


                if (coin.picked == false) { effect.Play(); score++; Score.ScorePoints++; } 
                
                coin.picked = true;
            }
        }
        
        public void Collision(Rectangle newRectangle, int xOffset, int Yoffset, bool isDeadly, SoundEffect effect)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + rectangle.Width + 8; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            /*if (IntersectsPixel(this.rectangle, this.textureData, enemy.rectangle, enemy.textureData))
                {
                    //snowball.IsRemoved = true;
                    //hasDied = true;
                    position = new Vector2(150, 384);
                }*/
            if (isDeadly == true && (rectangle.TouchTopOf(newRectangle) /*|| rectangle.TouchLeftOf(newRectangle) || rectangle.TouchRightOf(newRectangle)*/ || rectangle.TouchBottomOf(newRectangle)))
            {
                
                position = new Vector2(450, 100);
                effect.Play();
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
            //spriteBatch.Draw(texture, rectangle, Color.White);
            if(looksRight == true)
            {

                spriteBatch.Draw(texture, rectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
            }
            else
            {

                spriteBatch.Draw(texture, rectangle, null, Color.White, rotation, origin, SpriteEffects.FlipHorizontally, 0f);
            }

        }

        private static bool IntersectsPixel(Rectangle rect1, Color[] data1,
                                  Rectangle rect2, Color[] data2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
                    Color color2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];
                    if (color1.A != 0 && color2.A != 0)

                        return true;
                }
            }
            return false;
        }




        /*private static bool IsColliding(Entity a, Entity b)
        {
            float radius = a.Radius + b.Radius;
            return !a.IsExpired && !b.IsExpired && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        }*/
    }
}
