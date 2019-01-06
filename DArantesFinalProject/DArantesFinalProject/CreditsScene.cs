/*
 * Neko Runner Game Application
 * Daiana Arantes Dec 2018
 * Final Project
 * Revision history
 * Class CreditsScene
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DArantesFinalProject
{
    class CreditsScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private SoundEffect creditMusic;
        private SoundEffectInstance creditMusicInstance;
        public SoundEffectInstance CreditMusicInstance { get => creditMusicInstance; set => creditMusicInstance = value; }

        public CreditsScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/backgroundCredits");

            creditMusic = game.Content.Load<SoundEffect>("Sounds/creditsMusic");
            CreditMusicInstance = creditMusic.CreateInstance(); 
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
