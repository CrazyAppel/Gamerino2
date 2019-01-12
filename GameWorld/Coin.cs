using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Coin
    {
        private Texture2D texture;
        public Rectangle rectangle;
        public Color[] textureData { get; set; }
        public Vector2 position;
        public SoundEffect coinsound;
        public bool picked = false;

        public Coin() { }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Tile4");
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            coinsound = Content.Load<SoundEffect>("WOO");
        }
        
        public void Update(GameTime gameTime, Player player)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

       
        public void Draw(SpriteBatch spriteBatch)
        {
            
                spriteBatch.Draw(texture, rectangle, Color.White);
               
        }
    }

}

