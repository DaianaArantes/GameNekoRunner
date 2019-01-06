/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class Obstacle
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
    public class Obstacle : DrawableGameComponent
    {
        
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Rectangle srcRect;
        private Vector2 dimension;
        private Vector2 speed;


        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Tex { get => tex; set => tex = value; }
        public Vector2 Speed { get => speed; set => speed = value; }

        public Obstacle(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Rectangle srcRect, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Tex = tex;
            this.Position = position;
            this.srcRect = srcRect;
            this.Speed = speed;
            this.dimension = new Vector2(50, 50);
            this.Enabled = false;
            this.Visible = false;

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
           
            spriteBatch.Draw(Tex, Position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position += Speed;

            base.Update(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
