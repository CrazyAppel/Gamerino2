﻿using Microsoft.Xna.Framework;
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
         //   PauseGame
        }
        GameState _currentGameState;
        private Texture2D _titleScreenTexture, _endGameTexture;//, _pauseTexture;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SpriteFont font;

        //SOUNDEFFECTS
        private Song Backgroundmusic;
        private SoundEffect effect, effect2;
        //SCORE
        private SpriteFont _scorefont;
        private Score _score;

        //BACKGROUND
        private Camera _camera;
        Texture2D backgroundimage;

        Camera camera;
        Map level;
        Enemy enemy1, enemy2;
        List<Enemy> enemies;
        List<Enemy> enemies2;
        
        Coin coin1, coin2, coin3, coin4, coin5, coin6, coin7, coin8, coin9, coin10, coin11, coin12, coin13, coin14, coin15, coin16, coin17, coin18, coin19, coin20;
        List<Coin> coinslvl1, coinslvl2;
        

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
            enemies2 = new List<Enemy>();
            coinslvl1 = new List<Coin>();
            coinslvl2 = new List<Coin>();
            enemy1 = new Enemy();
            enemy2 = new Enemy();
            // enemy3 = new Speedyboi(new Vector2(450,150),player,true); 
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
            coin11 = new Coin();
            coin12 = new Coin();
            coin13 = new Coin();
            coin14 = new Coin();
            coin15 = new Coin();
            coin16 = new Coin();
            coin17 = new Coin();
            coin18 = new Coin();
            coin19 = new Coin();
            coin20 = new Coin();
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
            
            if (level.isLevel1 == true)
            {
                
                //COINS
                coin1.position = new Vector2(464, 601);
                coin2.position = new Vector2(14, 87);
                coin3.position = new Vector2(718, 280);
                coin4.position = new Vector2(1486, 601);
                coin5.position = new Vector2(1167, 155);
                coin6.position = new Vector2(2216, 86);
                coin7.position = new Vector2(2106, 351);
                coin8.position = new Vector2(2372, 602);
                coin9.position = new Vector2(3090, 350);
                coin10.position = new Vector2(3279, 607);
                coinslvl1.Add(coin1);
                coinslvl1.Add(coin2);
                coinslvl1.Add(coin3);
                coinslvl1.Add(coin4);
                coinslvl1.Add(coin5);
                coinslvl1.Add(coin6);
                coinslvl1.Add(coin7);
                coinslvl1.Add(coin8);
                coinslvl1.Add(coin9);
                coinslvl1.Add(coin10);

                coinslvl2.Add(coin11);
                coinslvl2.Add(coin12);
                coinslvl2.Add(coin13);
                coinslvl2.Add(coin14);
                coinslvl2.Add(coin15);
                coinslvl2.Add(coin16);
                coinslvl2.Add(coin17);
                coinslvl2.Add(coin18);
                coinslvl2.Add(coin19);
                coinslvl2.Add(coin20);

                //ENEMIES
                enemy1.position = new Vector2(900, 380);
                enemy2.position = new Vector2(450, 380);
                enemy1.speed = 1f;
                enemy2.speed = 2f;
                enemies.Add(enemy1);
                enemies2.Add(enemy2);
            }
            else
            {
                coin11.position = new Vector2(100, 50);
                coin12.position = new Vector2(100, 100);
                coin13.position = new Vector2(100, 150);
                coin14.position = new Vector2(100, 200);
                coin15.position = new Vector2(100, 250);
                coin16.position = new Vector2(100, 300);
                coin17.position = new Vector2(100, 351);
                coin18.position = new Vector2(100, 400);
                coin19.position = new Vector2(100, 450);
                coin20.position = new Vector2(100, 50);
            }

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

            level.Level1();

            //soundeffect
            effect = Content.Load<SoundEffect>("WOO");
            effect2 = Content.Load<SoundEffect>("Deadaf");
            Backgroundmusic = Content.Load<Song>("Song");
            //background
            backgroundimage = Content.Load<Texture2D>("Background");
            _camera = new Camera(GraphicsDevice.Viewport);


            //CHANGE VOLUME VAN GELUIDEN
            MediaPlayer.Volume = 0.03f;
            SoundEffect.MasterVolume = 0.03f;
            MediaPlayer.Play(Backgroundmusic);
            MediaPlayer.IsRepeating = true;
            //TEXTURES
          //  _pauseTexture = Content.Load<Texture2D>("titlescreen");
            _titleScreenTexture = Content.Load<Texture2D>("Menu");
            _endGameTexture = Content.Load<Texture2D>("End");

            //SCORE
            _scorefont = Content.Load<SpriteFont>("score");
            _score = new Score(_scorefont);

            player.Load(Content);
            
            if (level.isLevel1 == true)
            {
                foreach (Coin coin in coinslvl1)
                {
                    coin.Load(Content);

                }
                foreach (Coin coin in coinslvl2)
                {
                    var prevpos = coin.position;
                    coin.position.X = 7000 + prevpos.X + 30;
                    coin.Load(Content);
                }
                foreach (Enemy enemy in enemies)
                {

                    enemy.Load(Content, false, true);
                }
                foreach (Enemy enemy in enemies2)
                {

                    enemy.Load(Content, true, false);
                }
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

                    if (level.isLevel1 == true)
                    {
                        foreach (Coin coin in coinslvl1)
                        {

                            coin.Update(gameTime, player);
                        }
                        foreach (Coin coin in coinslvl1)
                        {
                            player.checkCoinColision(coin, effect);
                        }
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.Update(gameTime, player);
                        }
                        foreach (Enemy enemy in enemies2)
                        {
                            enemy.Update(gameTime, player);
                        }
                        foreach (Enemy enemy in enemies)
                        {
                            player.checkEnemyCollision(enemy, effect2);
                        }
                        foreach (Enemy enemy in enemies2)
                        {
                            player.checkEnemyCollision(enemy, effect2);
                        }
                    }
                    else
                    {
                        foreach (Coin coin in coinslvl2)
                        {

                            coin.Update(gameTime, player);
                        }
                        foreach (Coin coin in coinslvl2)
                        {
                            player.checkCoinColision(coin, effect);
                        }
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.Update(gameTime, player);
                        }
                        foreach (Enemy enemy in enemies2)
                        {
                            enemy.Update(gameTime, player);
                        }
                        foreach (Enemy enemy in enemies)
                        {
                            player.checkEnemyCollision(enemy, effect2);
                        }
                        foreach (Enemy enemy in enemies2)
                        {
                            player.checkEnemyCollision(enemy, effect2);
                        }
                    }
                    //enemy.Update(gameTime, player);
                    if (player.score == 10)
                        {
                        ResetLevel();
                        level.Level2();
                        
                        //_currentGameState = GameState.EndGame;
                        player.score = 0;
                        }
                    if (player.score == 5 && level.isLevel1 == false)
                    {
                        _currentGameState = GameState.EndGame;
                    }

                    break;
                case GameState.EndGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        this.Exit();
                    }
                    break;


            }

            foreach (CollisionTiles tile in level.CollisionTiles)
            {
                player.Collision(tile.Rectangle, level.Width, level.Height, tile.isDeadly, tile.isEnd, effect2);
                foreach (Enemy enemy in enemies)
                {
                    enemy.Collision(tile.Rectangle, level.Width, level.Height);
                }
                foreach (Enemy enemy in enemies2)
                {
                    enemy.Collision(tile.Rectangle, level.Width, level.Height);
                }
                //enemy.Collision(tile.Rectangle, level.Width, level.Height);
                _camera.Update(player.Position, level.Width, level.Height);
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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, _camera.Transform);

            switch (_currentGameState)
            {

                case GameState.TitleScreen:
                    spriteBatch.Draw(_titleScreenTexture, new Rectangle((int)_camera.topLeft.X, (int)_camera.topLeft.Y, 800, 480), Color.White);
                    break;
                case GameState.InGame:

                    //spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(backgroundimage, new Rectangle((int)_camera.topLeft.X, (int)_camera.topLeft.Y, 800, 480), Color.White);


                    if (level.isLevel1 == true)
                    {
                        foreach (Coin coin in coinslvl1)
                        {
                            if (coin.picked == false)
                            {
                                coin.Draw(spriteBatch);
                            }

                        }
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.Draw(spriteBatch);
                        }
                        foreach (Enemy enemy in enemies2)
                        {
                            enemy.Draw(spriteBatch);
                        }
                    }
                    if (level.isLevel1 == false)
                    {
                        foreach (Coin coin in coinslvl2)
                        {
                            if (coin.picked == false)
                            {
                                coin.Draw(spriteBatch);
                            }

                        }
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.Draw(spriteBatch);
                        }
                        foreach (Enemy enemy in enemies2)
                        {
                            enemy.Draw(spriteBatch);
                        }
                    }
                    

                    //enemy.Draw(spriteBatch);
                    level.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    spriteBatch.DrawString(_scorefont, "score: " + player.score, new Vector2(_camera.topLeft.X, _camera.topLeft.Y), Color.White);


                    break;

                case GameState.EndGame:
                    spriteBatch.Draw(_endGameTexture, new Rectangle((int)_camera.topLeft.X, (int)_camera.topLeft.Y, 800, 480), Color.White);
                    break;
            }
            // TODO: Add your drawing code here

            


            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void ResetLevel()
        {
            level.Changelevel();
            enemies.Clear();
            enemies2.Clear();
            player.position = player.spawn;
            level.isLevel1 = false;
            initiateObjects();
        }
    }
}
