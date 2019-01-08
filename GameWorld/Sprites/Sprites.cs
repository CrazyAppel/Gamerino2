using GameWorld.Managers;
using GameWorld.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld.Sprite
{
    class Sprites
    {
        protected AnimationManager _animationManager;
        

        protected Texture2D _texture;
        protected Dictionary<string,Animations>_animations;
        protected Vector2 _position;

        public Input Input;

        
        public float Speed = 5f;
        public Vector2 Velocity;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value;
                if (_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Position, Color.White);
            }
            else if (_animationManager != null)
            {
                _animationManager.Draw(spriteBatch);
            }
            else throw new Exception("This ain't right..");
        }

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Jump)) Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Left)) Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right)) Velocity.X = Speed;
        }
        public Sprites(Dictionary<string, Animations> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprites(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update (GameTime gameTime, List<Sprites> sprites)
        {
            Move();

            SetAnimations();

            _animationManager.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
            {
                _animationManager.Play(_animations["WalkRight"]);
            }
            else if (Velocity.X < 0)
            {
                _animationManager.Play(_animations["WalkLeft"]);
            }
            else if (Velocity.Y < 0)
            {
                _animationManager.Play(_animations["WalkUp"]);
            }
        }
    }
}
