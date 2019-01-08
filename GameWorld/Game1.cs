using GameWorld.Models;
using GameWorld.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace GameWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        
          private enum GameState
          {
              TitleScreen,
              InGame,
              EndGame,
              GameOver,
              PauseGame
          }
        GameState _currentGameState;
        private Texture2D _titleScreenTexture, _endGameTexture;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //SOUNDEFFECTS
        private Song Backgroundmusic;
        private SoundEffect effect, effect2;
        
        
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
            _currentGameState = GameState.TitleScreen;
            
            level = new Map();
            enemies = new List<Enemy>();
            coins = new List<Coin>();
            enemy1 = new Enemy();
            enemy2 = new Enemy();
            coin1 = new Coin();
            coin2 = new Coin();
            player = new Player();
            initiateObjects();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        private void initiateObjects()
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
            IsMouseVisible = true;
            //enemy = new Enemy(Content.Load<Texture2D>("Player"), new Vector2(900, 384), 1000);
            
            Tiles.Content = Content;
            
            camera = new Camera(GraphicsDevice.Viewport);

            level.Level2();

            //soundeffect
             effect = Content.Load<SoundEffect>("WOO");
             effect2 = Content.Load<SoundEffect>("Deadaf");
            Backgroundmusic = Content.Load<Song>("Song");
            MediaPlayer.Volume = 0.03f;
            MediaPlayer.Play(Backgroundmusic);
            MediaPlayer.IsRepeating = true;
            //TEXTURES
            _titleScreenTexture = Content.Load<Texture2D>("titlescreen");
            _endGameTexture = Content.Load<Texture2D>("titlescreen");
            
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
            
            switch (_currentGameState)
            {
                case GameState.TitleScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        _currentGameState = GameState.InGame;
                    }
                    break;
                case GameState.InGame:

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
                    

                    foreach (Enemy enemy in enemies)
                    {
                        player.checkEnemyCollision(enemy, effect2);
                    }
                    foreach (Coin coin in coins)
                    {
                        player.checkCoinColision(coin, effect);
                    }
                    if (player.score == 2)
                    {
                        _currentGameState = GameState.EndGame;
                    }


                    break;
                case GameState.EndGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        _currentGameState = GameState.InGame;
                        player.score = 0;
                    }
                    break;


            }

            foreach (CollisionTiles tile in level.CollisionTiles)
            {
                player.Collision(tile.Rectangle, level.Width, level.Height, tile.isDeadly, effect2);
                foreach (Enemy enemy in enemies)
                {
                    enemy.Collision(tile.Rectangle, level.Width, level.Height);
                }

                //enemy.Collision(tile.Rectangle, level.Width, level.Height);
                camera.Update(player.Position, level.Width, level.Height);
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
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            
            switch (_currentGameState)
            {
                case GameState.TitleScreen:
                    spriteBatch.Draw(_titleScreenTexture, new Rectangle(0, 0, 960, 540), Color.White);
                    break;
                case GameState.InGame:

                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Draw(spriteBatch);
                    }
                    foreach (Coin coin in coins)
                    {
                        if (coin.picked == false)
                        {
                            coin.Draw(spriteBatch);
                        }

                    }

                    //enemy.Draw(spriteBatch);
                    level.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    break;

                case GameState.EndGame:
                    spriteBatch.Draw(_titleScreenTexture, new Rectangle(0, 0, 960, 540), Color.White);
                    break;
            }
            // TODO: Add your drawing code here

            
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
