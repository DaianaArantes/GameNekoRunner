/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class Score
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DArantesFinalProject
{
    public class Score : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private string message;
        private float scoreCount;
        private SpriteFont font;
        private Vector2 position;
        private Color color;

        public string Message { get => message; set => message = value; }
        public float ScoreCount { get => scoreCount; set => scoreCount = value; }

        public Score(
            Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position,
            Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.color = color;
            this.ScoreCount = 0f;
            this.message = "Score: ";
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message + (int)ScoreCount, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
        
        public void IncreaseScore ()
        {
            
            ScoreCount += .05f;
        }
    }
}
