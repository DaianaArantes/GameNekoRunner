/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class GameOverScene
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DArantesFinalProject
{
    public class GameOverScene : GameScene
    {
        Game game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Texture2D inputTex;
        private SoundEffect gameOverMusic;
        private string messageInsertName;
        private string messageScore;
        private string messageContinue;
        SpriteFont font;
        Score score;
        Vector2 position = new Vector2(255,225);
        private SoundEffectInstance gameOverMusicInstance;
        public SoundEffectInstance GameOverMusicInstance { get => gameOverMusicInstance; set => gameOverMusicInstance = value; }


        public GameOverScene(Game game,
           SpriteBatch spriteBatch,
           Score score) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.messageInsertName = "Your Name: ";
            this.score = score;
            this.messageScore = "Your Score: " + (int)score.ScoreCount;
            this.messageContinue = "Insert your name and press enter to continue...";
            tex = game.Content.Load<Texture2D>("Images/backgroundGameOver");
            inputTex = game.Content.Load<Texture2D>("Images/InputField");
            font = game.Content.Load<SpriteFont>("Fonts/hilightfont");
            gameOverMusic = game.Content.Load<SoundEffect>("Sounds/GameOver");
            GameOverMusicInstance = gameOverMusic.CreateInstance();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            
            spriteBatch.Draw(tex, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(inputTex, new Vector2(250, 220), Color.White);
            spriteBatch.DrawString(font, ((Game1)game).playerName, position, Color.Black);
            spriteBatch.DrawString(font,messageInsertName, new Vector2(250,180), Color.Black);
            spriteBatch.DrawString(font, messageScore, new Vector2(250, 150), Color.Black);
            spriteBatch.DrawString(font, messageContinue, new Vector2(55, 290), Color.Black);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SaveScore()
        {

            
            StreamWriter sw;
            string fileName = Environment.CurrentDirectory + "/" + "highScore.txt";

            sw = new StreamWriter(fileName, append: true);
            //highScoreSortedList.Add((int)score.ScoreCount, ((Game1)game).playerName);

            //foreach (KeyValuePair<int, string> item in highScoreSortedList.Reverse())
            //{
            string line = ((Game1)game).playerName + ";" + (int)score.ScoreCount;
            //}

            sw.WriteLine(line);
            sw.Close();
        }

    }

}
