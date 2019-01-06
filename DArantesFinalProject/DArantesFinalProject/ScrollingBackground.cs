/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class ScrollingBackground
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
    public class ScrollingBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 position1,position2;
        private Vector2 speed;
        public Vector2 Speed { get => speed; set => speed = value; }
        public ScrollingBackground(Game game,
            SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Rectangle srcRect,
            Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position1 = position;
            this.srcRect = srcRect;
            this.position2 = new Vector2(position1.X + srcRect.Width, position1.Y);

            this.Speed = speed;
        }

        

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //version 4
            spriteBatch.Draw(tex, position1, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position1 += Speed;
            position2 += Speed;

            if (position1.X < -srcRect.Width)
            {
                position1.X = position2.X + srcRect.Width;
            }

            if (position2.X < -srcRect.Width)
            {
                position2.X = position1.X + srcRect.Width;
            }
            base.Update(gameTime);
        }
    }
}
