/*
 * Neko Runner Game Application
 * Daiana Arantes, Dec 2018
 * Final Project
 * Revision history
 * Class StartScene
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
    public class StartScene : GameScene
    {
        private SpriteBatch spriteBatch;
        public MenuComponent Menu { get; set; }
        public SoundEffectInstance MenuMusicInstance { get => menuMusicInstance; set => menuMusicInstance = value; }
        private Texture2D tex;
        private SoundEffect menuMusic;
        private SoundEffectInstance menuMusicInstance;

        private string[] menuItems =
        {
            "Start Game",
            "Help",
            "High Score",
            "Credit",
            "Quit"
        };
        

        public StartScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            //TODO: construct any game components here
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularfont");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/hilightfont");
            tex = game.Content.Load<Texture2D>("Images/backgroundMainMenu");
            Menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems);
            this.Components.Add(Menu);
            menuMusic = game.Content.Load<SoundEffect>("Sounds/menuMusic");
            MenuMusicInstance = menuMusic.CreateInstance();
            MenuMusicInstance.Play();
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
