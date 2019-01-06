/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class Cat
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

    public class Cat : DrawableGameComponent
    {
        private const int FRAME_SIZE = 7;

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private bool jumping;
        private int jumpHeight;

        // Interval between each frame to be displayed
        private TimeSpan animationInterval;

        // Time elapsed - Updated in the update method
        private TimeSpan animationTimeElapsed;

        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Tex { get => tex; set => tex = value; }
        public bool Jumping { get => jumping; set => jumping = value; }
        public int JumpHeight { get => jumpHeight; }

        public Cat(Game game,
            SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.Tex = tex;
            this.Position = position;

            this.dimension = new Vector2(128, 128);
            this.Enabled = false;
            this.Visible = false;

            this.jumpHeight = 96;

            animationInterval = TimeSpan.FromMilliseconds(100);

            //create frame here
            createFrames();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();

            for (int j = 0, i = FRAME_SIZE - 1; j < FRAME_SIZE; j++, i--)
            {
                int x = i * (int)dimension.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);

                frames.Add(r);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(Tex, Position, frames.ElementAt<Rectangle>(frameIndex), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            animationTimeElapsed += gameTime.ElapsedGameTime;
            if (frameIndex >= 0)
            {
                if (animationTimeElapsed >= animationInterval)
                {
                    frameIndex++;
                    if (frameIndex > FRAME_SIZE - 1)
                    {
                        frameIndex = 0;
                        //this.Enabled = false;
                        //this.Visible = false;
                    }
                    animationTimeElapsed = TimeSpan.FromMilliseconds(0);
                }
            }
            else
            {
                frameIndex++;
            }
            base.Update(gameTime);
        }
        public Rectangle getBounds()
        {
            // compensate the cat sprite
            return new Rectangle((int)position.X + 32, (int)position.Y + 32, tex.Width/FRAME_SIZE - 64, tex.Height - 64);
        }
    }
}
