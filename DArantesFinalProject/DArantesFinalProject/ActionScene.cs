/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class ActionScene
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace DArantesFinalProject
{
    public class ActionScene : GameScene
    {
        //Declarations
        private SpriteBatch spriteBatch;
        private Cat cat;
        private Obstacle obstacle;
        private Coin coin;
        private Game game;
        private ScrollingBackground backgroundFloor;
        private ScrollingBackground backgroundSky;
        private ScrollingBackground backgroundTree1;
        private ScrollingBackground backgroundTree2;
        private CollisionManager collisionManager;

        // Default jump direction. Moves 3 pixels up per update
        int jumpDirection = -3;
        Vector2 positionFloor;

        private Score score;
        private bool gameOver = false;
        public bool GameOver { get => gameOver; set => gameOver = value; }
        public Score Score { get => score; set => score = value; }

        Texture2D texCatStanding;
        Texture2D texCatRunning;
        Texture2D texCatJumping;
        Texture2D texFloor;

        // Music for action scene
        private SoundEffect playMusic;
        private SoundEffectInstance playMusicInstance;
        public SoundEffectInstance PlayMusicInstance { get => playMusicInstance; set => playMusicInstance = value; }

        public ActionScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            // Display sky on game
            Texture2D texSky = game.Content.Load<Texture2D>("Images/SkyBacground");
            Rectangle srcRectSky = new Rectangle(0, 0, texSky.Width, texSky.Height);
            Vector2 positionSky = new Vector2(0);
            backgroundSky = new ScrollingBackground(game, spriteBatch, texSky, positionSky, srcRectSky, new Vector2(-0.5f, 0));
            this.Components.Add(backgroundSky);

            texFloor = game.Content.Load<Texture2D>("Images/Floor");

            //display tree2
            Texture2D texTree2 = game.Content.Load<Texture2D>("Images/Tree2");
            Rectangle srcRectTree2 = new Rectangle(0, 0, texTree2.Width, texTree2.Height);
            Vector2 positionTree2 = new Vector2(0, Shared.stage.Y - texFloor.Height - 300);
            backgroundTree2 = new ScrollingBackground(game, spriteBatch, texTree2, positionTree2, srcRectTree2, new Vector2(-1f, 0));
            this.Components.Add(backgroundTree2);

            //Display tree1
            Texture2D texTree1 = game.Content.Load<Texture2D>("Images/Tree1");
            Rectangle srcRectTree1 = new Rectangle(0, 0, texTree1.Width, texTree1.Height);
            Vector2 positionTree1 = new Vector2(0, Shared.stage.Y - texFloor.Height - 200);
            backgroundTree1 = new ScrollingBackground(game, spriteBatch, texTree1, positionTree1, srcRectTree1, new Vector2(-2, 0));
            this.Components.Add(backgroundTree1);

            //Display floor
            Rectangle srcRectFloor = new Rectangle(0, 0, texFloor.Width, texFloor.Height);
            positionFloor = new Vector2(0, Shared.stage.Y - texFloor.Height);
            backgroundFloor = new ScrollingBackground(game, spriteBatch, texFloor, positionFloor, srcRectFloor, new Vector2(-3, 0));
            this.Components.Add(backgroundFloor);

            // display cat
            this.spriteBatch = spriteBatch;
            texCatStanding = game.Content.Load<Texture2D>("Images/cat-standing");
            texCatRunning = game.Content.Load<Texture2D>("Images/cat-running");
            texCatJumping = game.Content.Load<Texture2D>("Images/cat-jumping-2");
            Vector2 position = new Vector2(40, positionFloor.Y - 93);

            cat = new Cat(game, spriteBatch, texCatStanding, position);
            this.Components.Add(cat);
            cat.Enabled = true;
            cat.Visible = true;

            // Display Obstacle
            Vector2 obsPosition = new Vector2(700, positionFloor.Y - 40);
            Rectangle srcRectObs = new Rectangle(0, 0, texFloor.Width, texFloor.Height);
            Texture2D texObs = game.Content.Load<Texture2D>("Images/Obstacle");
            obstacle = new Obstacle(game, spriteBatch, texObs, obsPosition, srcRectObs, new Vector2(0));
            this.Components.Add(obstacle);
            obstacle.Enabled = true;
            obstacle.Visible = true;

            // Display Coin
            Vector2 coinPosition = new Vector2(1100, positionFloor.Y - 140);
            Rectangle srcRectCoin = new Rectangle(0, 0, texFloor.Width, texFloor.Height);
            Texture2D texCoin = game.Content.Load<Texture2D>("Images/Coin");
            coin = new Coin(game, spriteBatch, texCoin, coinPosition, srcRectCoin, new Vector2(0));
            this.Components.Add(coin);
            coin.Enabled = true;
            coin.Visible = true;

            //display Score
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/hilightfont");
            Vector2 pos1 = new Vector2(5, 5);
            Score = new Score(game, spriteBatch, font, pos1, Color.Black);
            this.Components.Add(Score);

            // play sound
            playMusic = game.Content.Load<SoundEffect>("Sounds/PlayMusic");
            playMusicInstance = playMusic.CreateInstance();

            //Collision Manager
            collisionManager = new CollisionManager();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (cat.Jumping)
            {
                // 2x of original speed
                backgroundFloor.Speed = new Vector2(-6f, 0); // -3
                backgroundSky.Speed = new Vector2(-1f, 0); // -.5
                backgroundTree2.Speed = new Vector2(-2f, 0); // -1
                backgroundTree1.Speed = new Vector2(-4f, 0); // -2
                obstacle.Speed = new Vector2(-6f, 0);
                coin.Speed = new Vector2(-6f, 0);
                cat.Tex = texCatJumping;

                float catFloorPosition = (positionFloor.Y - 93);
                
                // reached top
                if (catFloorPosition - cat.Position.Y > cat.JumpHeight)
                {
                    jumpDirection = -jumpDirection;
                }

                cat.Position = new Vector2(cat.Position.X, cat.Position.Y + jumpDirection);

                if (cat.Position.Y == catFloorPosition)
                {
                    cat.Jumping = false;
                    jumpDirection = -3;
                }
                Score.IncreaseScore();
            }

            else { 
                if (ks.IsKeyDown(Keys.Right))
                {
                    // modify speed backgrounds
                    backgroundFloor.Speed = new Vector2(-3, 0);
                    backgroundSky.Speed = new Vector2(-0.5f,0);
                    backgroundTree2.Speed = new Vector2(-1f, 0);
                    backgroundTree1.Speed = new Vector2(-2f, 0);
                    obstacle.Speed = new Vector2(-3, 0);
                    coin.Speed = new Vector2(-3, 0);

                    // modify texture cat
                    cat.Tex = texCatRunning;
                    Score.IncreaseScore();
                }
                else if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.Space))
                {
                    cat.Jumping = true;
                }
                else
                {
                    backgroundFloor.Speed = new Vector2(0);
                    backgroundSky.Speed = new Vector2(0);
                    backgroundTree2.Speed = new Vector2(0);
                    backgroundTree1.Speed = new Vector2(0);
                    obstacle.Speed = new Vector2(0);
                    coin.Speed = new Vector2(0);
                    cat.Tex = texCatStanding;
                }
            }

            if (collisionManager.hasObstacleCollision(this, cat))
            {
                gameOver = true;
            }
            else if (collisionManager.hasCoinCollision(this, cat))
            {
                Score.ScoreCount += 100;
                coin.CoinMusicInstance.Play();
                this.Components.Remove(coin);
            }

            DisplayObstacle();
            DisplayCoin();
            base.Update(gameTime);
        }

        private void DisplayObstacle()
        {
            if (isObstacleInFrontOfCat() == false)
            {
                int stageX = (int)Shared.stage.X;
                int distance = 1000;
                Random random = new Random();
                float newObstacleX = random.Next(stageX,(stageX + distance));
                Vector2 obsPosition = new Vector2(newObstacleX, positionFloor.Y - 40);
                Rectangle srcRectObs = new Rectangle(0, 0, texFloor.Width, texFloor.Height);
                Texture2D texObs = game.Content.Load<Texture2D>("Images/Obstacle");
                obstacle = new Obstacle(game, spriteBatch, texObs, obsPosition, srcRectObs, new Vector2(0));
                this.Components.Add(obstacle);
                obstacle.Enabled = true;
                obstacle.Visible = true;
            }
        }

        private bool isObstacleInFrontOfCat()
        {
            float catX = cat.Position.X;

            foreach (var component in this.Components)
            {
                if (component is Obstacle)
                {
                    if (((Obstacle)component).Position.X > catX)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DisplayCoin()
        {
            if (isCoinInFrontOfCat() == false)
            {
                int stageX = (int)Shared.stage.X;
                int distance = 600;
                Random random = new Random();
                float newCoinX = random.Next(stageX, (stageX + distance));
                Vector2 coinPosition = new Vector2(newCoinX, positionFloor.Y - 140);
                Rectangle srcRectCoin = new Rectangle(0, 0, texFloor.Width, texFloor.Height);
                Texture2D texCoin = game.Content.Load<Texture2D>("Images/Coin");
                coin = new Coin(game, spriteBatch, texCoin, coinPosition, srcRectCoin, new Vector2(0));
                this.Components.Add(coin);
                coin.Enabled = true;
                coin.Visible = true;
            }
        }

        private bool isCoinInFrontOfCat()
        {
            float catX = cat.Position.X;

            foreach (var component in this.Components)
            {
                if (component is Coin)
                {
                    if (((Coin)component).Position.X > catX)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
