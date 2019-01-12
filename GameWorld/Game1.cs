using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SpriteFont font;

        //SOUNDEFFECTS
        SoundEffect effect;

        Camera camera;
        Map level;
        Enemy enemy1, enemy2;
        List<Enemy> enemies;
        Coin coin1, coin2;
        List<Coin> coins;
        Map map;
        Player player;
        //bool isDeadly;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            level = new Map();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            enemy1 = new Enemy();
            enemy2 = new Enemy();
            coin1 = new Coin();
            coin2 = new Coin();
            player = new Player();
            initiateEnemies();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        private void initiateEnemies()
        {
            coin1.position = new Vector2(250, 296);
            coin2.position = new Vector2(405, 296);
            enemy1.position = new Vector2(900, 380);
            enemy2.position = new Vector2(100, 380);
            enemy1.speed = 1f;
            enemy2.speed = 2f;
            enemies.Add(enemy1);
            enemies.Add(enemy2);
            coins.Add(coin1);
            coins.Add(coin2);
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font"); // Use the name of your sprite font file here instead of 'Score'.
            IsMouseVisible = true;
            //enemy = new Enemy(Content.Load<Texture2D>("Player"), new Vector2(900, 384), 1000);
            
            Tiles.Content = Content;
            
            camera = new Camera(GraphicsDevice.Viewport);

            level.Level2();
           
            //soundeffect
            effect = Content.Load<SoundEffect>("WOO");


            player.Load(Content);
            foreach (Enemy enemy in enemies)
            {

                enemy.Load(Content);
            }
            foreach (Coin coin in coins)
            {
                coin.Load(Content);
            }
            
            //enemy.Load(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime, player);
            }
            foreach (Coin coin in coins)
            {
                coin.Update(gameTime, player);
            }
            //enemy.Update(gameTime, player);
            foreach (CollisionTiles tile in level.CollisionTiles)
            {
                player.Collision(tile.Rectangle, level.Width, level.Height, tile.isDeadly);
                foreach (Enemy enemy in enemies)
                {
                    enemy.Collision(tile.Rectangle, level.Width, level.Height);
                }
               
                //enemy.Collision(tile.Rectangle, level.Width, level.Height);
                camera.Update(player.Position, level.Width, level.Height);
            }
            foreach (Enemy enemy in enemies)
            { 
            player.checkEnemyCollision(enemy);
            }
            foreach (Coin coin in coins)
            {
                player.checkCoinColision(coin,effect);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,camera.Transform);
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Coin coin in coins)
            {
                if(coin.picked == false)
                {
                    coin.Draw(spriteBatch);
                }
                
                
            }
            //enemy.Draw(spriteBatch);
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
