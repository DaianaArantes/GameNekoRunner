/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class HighScoreScene
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
    class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private string message;
        private SoundEffect highScoreMusic;
        private SoundEffectInstance highScoreMusicInstance;
        public SoundEffectInstance HighScoreMusicInstance { get => highScoreMusicInstance; set => highScoreMusicInstance = value; }
        string[] nameArr;
        int[] scoreArr;
        SpriteFont font;

        SortedList<int, string> highScoreSortedList = new SortedList<int, string>();

        public HighScoreScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/backgroundHighScore");
            font = game.Content.Load<SpriteFont>("Fonts/hilightfont");
            highScoreMusic = game.Content.Load<SoundEffect>("Sounds/highScoreMusic");
            message = "No high scores";
            highScoreMusicInstance = highScoreMusic.CreateInstance();

            ReadScores();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            string line = "";

            for (int i = 0; i < nameArr.Length && i < 5; i++)
            {
                
                    line += nameArr[i] + " " + scoreArr[i] + "\n";
                
            }

            spriteBatch.Draw(tex, new Vector2(0, 0), Color.White);
            if (line != "")
            {
                spriteBatch.DrawString(font, line, new Vector2(260, 100), Color.Black);

            }
            else
            {
                spriteBatch.DrawString(font, message, new Vector2(260, 100), Color.Black);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ReadScores()
        {
            string fileName = Environment.CurrentDirectory + "/" + "highScore.txt";

            StreamReader sr = new StreamReader(fileName);

            // sr = blaus
            int items = 0;
            // while items++
            while (!sr.EndOfStream)
            {
                items++;
                sr.ReadLine();
            }
            sr.Close();

            nameArr = new string[items];

            scoreArr = new int[items];



            sr = new StreamReader(fileName);

            int i = 0;
            while (!sr.EndOfStream)
            {

                String[] lineArr = sr.ReadLine().Split(';');
                nameArr[i] = lineArr[0];
                scoreArr[i] = Convert.ToInt16(lineArr[1]);
                i++;
            }
            sr.Close();

            Array.Sort(scoreArr, nameArr);
            Array.Reverse(scoreArr);
            Array.Reverse(nameArr);
        }
    }
}
