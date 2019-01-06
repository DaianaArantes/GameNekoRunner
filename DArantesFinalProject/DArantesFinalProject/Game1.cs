/*
 * Neko Runner Game Application
 * Final Project
 * Revision history
 * Daiana Arantes, 2018-11-13 : started game, created menu and action scene
 * Daiana Arantes, 2018-11-17 : created cat, obstacle, collision manager
 * Daiana Arantes, 2018-11-13 : created credits scene, help scene
 * Daiana Arantes, 2018-11-13 : created game over scene, scrolling background
 * Daiana Arantes, 2018-11-13 : created coin, high socre scene, score
 * Daiana Arantes, 2018-12-06 : finished
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;

namespace DArantesFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //declare all scenes here
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private CreditsScene creditsScene;
        private HighScoreScene highScoreScene;
        private GameOverScene gameOverScene;
        public string playerName;
        private Regex regex;
        
        //declaration ends

        public Game1()
        {
            playerName = "";
            Window.TextInput += TextInputHandler;
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
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            System.Console.WriteLine(Shared.stage.X + "  " + Shared.stage.Y);

            base.Initialize();
        }

        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;

            regex = new Regex("^[a-zA-Z0-9]$");

            // Validate input, cannot have special characters
            if (regex.IsMatch(character.ToString()))
            {
                playerName += character;
            }

            if (character == '\b')
            {
                if (playerName != "")
                {
                    playerName = playerName.Substring(0, playerName.Length - 1);
                }
                
            }
          
            // IF IS SPECIAL CHARACTER, JOGA FORA.. DA PRA FAZER REGEX PRA CONSIDERAR SO azAZ09
           
            // IF BACKSPACE, APAGA ULTIMA LETRA

            
            // do something with the character (and optionally the key)
            // ...
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // instantiate all scenes here
            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            //actionScene = new ActionScene(this, spriteBatch);
            //this.Components.Add(actionScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            creditsScene = new CreditsScene(this, spriteBatch);
            this.Components.Add(creditsScene);

            //instantiation ends
            //make startScene visible/enabled

            startScene.show();
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.MenuMusicInstance.Stop();
                    startScene.hide();
                    
                    actionScene = new ActionScene(this, spriteBatch);
                    actionScene.show();
                    this.Components.Add(actionScene);
                    actionScene.PlayMusicInstance.Play();
                }
                //take care of other scenes here
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.MenuMusicInstance.Stop();
                    startScene.hide();

                    helpScene.show();
                    helpScene.HelpMusicInstance.Play();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.MenuMusicInstance.Stop();
                    startScene.hide();

                    highScoreScene = new HighScoreScene(this, spriteBatch);
                    this.Components.Add(highScoreScene);
                    highScoreScene.show();
                    highScoreScene.HighScoreMusicInstance.Play();
                }
                else if(selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.MenuMusicInstance.Stop();
                    startScene.hide();
                   
                    creditsScene.show();
                    creditsScene.CreditMusicInstance.Play();
                }
                else if(selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (actionScene != null && actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    this.Components.Remove(actionScene);
                    actionScene.PlayMusicInstance.Stop();

                    startScene.show();
                    startScene.MenuMusicInstance.Play();
                }
                
                if (actionScene.GameOver)
                {
                    // Stop game, and when initialized again it starts from zero
                    this.Components.Remove(actionScene);
                    actionScene.PlayMusicInstance.Stop();
                    actionScene.hide();

                    playerName = "";
                    gameOverScene = new GameOverScene(this, spriteBatch, actionScene.Score);
                    this.Components.Add(gameOverScene);
                    gameOverScene.show();
                    gameOverScene.GameOverMusicInstance.Play();
                }
            }
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.HelpMusicInstance.Stop();
                    helpScene.hide();

                    startScene.show();
                    startScene.MenuMusicInstance.Play();
                }
            }
            if (creditsScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    creditsScene.CreditMusicInstance.Stop();
                    creditsScene.hide();

                    startScene.show();
                    startScene.MenuMusicInstance.Play();
                }
            }
            if (highScoreScene != null && highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highScoreScene.HighScoreMusicInstance.Stop();
                    highScoreScene.hide();

                    startScene.show();
                    startScene.MenuMusicInstance.Play();
                    
                }
            }
            if (gameOverScene != null && gameOverScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    // Save score

                    this.Components.Remove(gameOverScene);
                    gameOverScene.hide();
                    
                    startScene.show();
                    startScene.MenuMusicInstance.Play();

                }
                // Go to high scoreScene to check players and high scores
                else if (ks.IsKeyDown(Keys.Enter))
                {
                    gameOverScene.SaveScore();
                    this.Components.Remove(gameOverScene);
                    gameOverScene.hide();

                    highScoreScene = new HighScoreScene(this, spriteBatch);
                    this.Components.Add(highScoreScene);
                    highScoreScene.show();
                    highScoreScene.HighScoreMusicInstance.Play();
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Teal);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
