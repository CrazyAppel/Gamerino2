using GameWorld.Managers;
using GameWorld.Models;
using GameWorld.Sprites;
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

       
        private Vector2 position = new Vector2(455,127);
        private SpriteFont font;
        private Vector2 velocity;
        private Rectangle rectangle;
        private Vector2 debugPos;
        IEnumerable<int> offsetXY = Enumerable.Range(-3, 3);

        public Texture2D _TEXTURE;
        public Color[] textureData { get; set; }
        private bool hasJumped = false;
        public bool hasDied = false;
        float rotation = 0f;
        private bool looksRight = false;
        public Vector2 origin;

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animations> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;

                if (_animationManager != null)
                    _animationManager.Position = position;
            }
        }

        public Player() { }

        public void Load(ContentManager Content)
        {
            var _animationset = new Dictionary<string, Animations>()
              {
                { "WalkLeft", new Animations(Content.Load<Texture2D>("playerRun"), 6, true) },
                { "WalkRight", new Animations(Content.Load<Texture2D>("playerRun"), 6, false) },
                { "JumpRight", new Animations(Content.Load<Texture2D>("Jump"), 1, false) },
                { "JumpLeft", new Animations(Content.Load<Texture2D>("Jump"), 1, true) },
                { "IdleLeft", new Animations(Content.Load<Texture2D>("playerIdle"), 4, true) },
                { "IdleRight", new Animations(Content.Load<Texture2D>("playerIdle"), 4, false) }
              };
            font = Content.Load<SpriteFont>("font");
            _animations = _animationset;
            _animationManager = new AnimationManager(_animations.First().Value);

            //_animationManager.Position = position;

            textureData = new Color[Constants.TEXTURE.Width * Constants.TEXTURE.Height];
            Constants.TEXTURE.GetData(textureData);

        }



        public void Update(GameTime gameTime)
        {
            Position += velocity;
            //rectangle = new Rectangle((int)position.X, (int)position.Y, 64, 64);
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, Constants.TEXTURE.Width / 6, Constants.TEXTURE.Height);
            origin = new Vector2(0, 0);
            _animationManager.Update(gameTime);
            Movement(gameTime);
            debugPos.X = Position.X;
            debugPos.Y = Position.Y - 50;

            if (velocity.Y < 9.81)
            {
                velocity.Y += 0.7f;
                hasJumped = true;
                if ((velocity.Y + 0.7f) > 9.81f)
                {
                    velocity.Y = 9.81f;
                }   
            }


            // zonder (int) is velocity -1 tot 1
            if (velocity.X > 0 && (int)velocity.Y == 0)
            {
                _animationManager.Play(_animations["WalkRight"]);
            }
                                  
            if (velocity.X < 0 && (int)velocity.Y == 0)
            {
                _animationManager.Play(_animations["WalkLeft"]);
            }

            if (velocity.X < 0 && (int)velocity.Y != 0 && hasJumped)
            {
                _animationManager.Play(_animations["JumpLeft"]);

            }
            if (velocity.X > 0 && (int)velocity.Y != 0 && hasJumped)
            {
                _animationManager.Play(_animations["JumpRight"]);

            }
            if (velocity.X == 0 && (int)velocity.Y == 0)
            { 
                if (looksRight)
                {
                    _animationManager.Play(_animations["IdleRight"]);
                }
                else
                {
                    _animationManager.Play(_animations["IdleLeft"]);
                }
            }
            if (velocity.X == 0 && (int)velocity.Y != 0 && hasJumped)
            {
                if (looksRight)
                {
                    _animationManager.Play(_animations["JumpRight"]);
                }
                else
                {
                    _animationManager.Play(_animations["JumpLeft"]);
                }
            }

            
        }

        private void Movement(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                looksRight = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                looksRight = false;
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
            }



                /*if (Keyboard.GetState().IsKeyDown(Keys.Right) && hasJumped == true)
            {
                _animationManager.Play(_animations["JumpRight"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && hasJumped == true)
            {
                _animationManager.Play(_animations["JumpLeft"]);
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right) && hasJumped == true)
            {
                
            }*/
        }

        public void checkEnemyCollision(Enemy enemy)
        {
            if (IntersectsPixel(this.rectangle, this.textureData, enemy.rectangle, enemy.textureData))
            {
                //snowball.IsRemoved = true;
                //hasDied = true;
                position = new Vector2(450, 100);
            }
        }

        public void checkCoinColision(Coin coin, SoundEffect effect)
        {
            if (IntersectsPixel(this.rectangle, this.textureData, coin.rectangle, coin.textureData))
            {
               
                
                if (coin.picked == false) effect.Play();
                coin.picked = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int Yoffset, bool isDeadly)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;

                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 20; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + rectangle.Width + 40; // moet niet 2 zijn verander dit als je problemen hebt. hangt af van sprite.
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
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                spriteBatch.DrawString(font, velocity.ToString() + " | " + position.ToString() + " | " + hasJumped.ToString()/*velocity.ToString()*/, debugPos, Color.Black);
            }
            //spriteBatch.Draw(texture, rectangle, Color.White);
            if (looksRight == true)
            {

                //spriteBatch.Draw(_animations[_animations.Keys.ElementAt(1)].Texture, rectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
                _animationManager.Draw(spriteBatch);
            }
            else
            {

                //spriteBatch.Draw(_animations[_animations.Keys.ElementAt(1)].Texture, rectangle, null, Color.White, rotation, origin, SpriteEffects.FlipHorizontally, 0f);
                _animationManager.Draw(spriteBatch);
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
    }
}
