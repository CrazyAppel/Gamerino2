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
        Coin coin1, coin2, coin3, coin4, coin5, coin6, coin7, coin8, coin9, coin10;
       public List<Coin> coinslvl1;
        public Coin() { }


        public void Initialize()
        {

           /* coinslvl1 = new List<Coin>();

            coin1 = new Coin();
            coin2 = new Coin();
            coin3 = new Coin();
            coin4 = new Coin();
            coin5 = new Coin();
            coin6 = new Coin();
            coin7 = new Coin();
            coin8 = new Coin();
            coin9 = new Coin();
            coin10 = new Coin();
            coin1.position = new Vector2(464, 601);
            coin2.position = new Vector2(14, 87);
            coin3.position = new Vector2(718, 280);
            coin4.position = new Vector2(967, 507);
            coin5.position = new Vector2(1167, 155);
            coin6.position = new Vector2(2216, 86);
            coin7.position = new Vector2(2106, 351);
            coin8.position = new Vector2(2372, 602);
            coin9.position = new Vector2(2960, 413);
            coin10.position = new Vector2(3146, 282);
            coinslvl1.Add(coin1);
            coinslvl1.Add(coin2);
            coinslvl1.Add(coin3);
            coinslvl1.Add(coin4);
            coinslvl1.Add(coin5);
            coinslvl1.Add(coin6);
            coinslvl1.Add(coin7);
            coinslvl1.Add(coin8);
            coinslvl1.Add(coin9);
            coinslvl1.Add(coin10);*/
        }
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Tile4");
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            coinsound = Content.Load<SoundEffect>("WOO");
           // coin1.position = new Vector2(750, 296);
           // coin2.position = new Vector2(605, 296);

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

