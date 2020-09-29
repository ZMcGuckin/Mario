using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheKoopaTroopas
{
    public sealed class TransitionScreen : IDisposable
    {
        Color[] data;
        Texture2D rectTexture;
        Texture2D BlackTexture;
        Texture2D PausedShader;
        public Vector2 OnePlayerPos { get; set; }
        public Vector2 TwoPlayerPos { get; set; }
        public Vector2 CursorPos { get; set; }
        public int LastSelected { get; set; }
        public int Cycle { get; set; }
        public int NextCycle { get; set; }
        private IList<String> Names { get; set; }
        public int SelectInterval { get; set; }
        public double TransitionTimer { get; set; }
        public String Name { get; set; }

        public TransitionScreen()
        {
            BlackTexture = Game1.Instance.Content.Load<Texture2D>("CarbonFiberBlackTexture");
            PausedShader = Game1.Instance.Content.Load<Texture2D>("PausedShader");
            data = new Color[((int)Game1.Instance.GameVariables.ScreenWidth * 2 * (int)Game1.Instance.GameVariables.ScreenHeight * 2)];
            rectTexture = new Texture2D(Game1.Instance.GraphicsDevice, (int)Game1.Instance.GameVariables.ScreenWidth * 2, (int)Game1.Instance.GameVariables.ScreenHeight * 2);
            rectTexture.SetData(data);
            CursorPos = new Vector2(0, 0);
            SelectInterval = 300;
            Name = "Shawn Harkins";
            Names = new List<String>();
            Names.Add("Shawn Harkins");
            Names.Add("Chris King");
            Names.Add("Zach McGuckin");
            Names.Add("Ahmir Robinson");
            NextCycle = 3000;
        }

        public void Dispose()
        {
            rectTexture.Dispose();
        }

        public void SetCursorPos()
        {
            if (LastSelected > SelectInterval)
            {
                if (CursorPos.Y == OnePlayerPos.Y)
                {
                    CursorPos = new Vector2(TwoPlayerPos.X - 100, TwoPlayerPos.Y);
                }
                else
                {
                    CursorPos = new Vector2(OnePlayerPos.X - 100, OnePlayerPos.Y);
                }
                new SoundEffects().PlayMenuSelect();
                LastSelected = 0;
            }
        }

        public void CycleCredits()
        {
            if(Cycle > NextCycle)
            {
                int index = Names.IndexOf(Name);
                index++;
                if(index > Names.Count - 1)
                {
                    index = 0;
                }
                Name = Names.ElementAt(index);
                NextCycle += 3000;
            }
        }

        public void Update(GameTime gameTime)
        {
            LastSelected += gameTime.ElapsedGameTime.Milliseconds;
            if (Game1.Instance.CurrentState != Game1.GameState.GameOver && Game1.Instance.CurrentState != Game1.GameState.MainMenu)
            {
                TransitionTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (TransitionTimer > 2800 && Game1.Instance.CurrentState == Game1.GameState.Transition)
                {
                    Game1.Instance.CurrentState = Game1.GameState.Playing;
                }
            }
            if(Game1.Instance.CurrentState == Game1.GameState.GameComplete)
            {
                Cycle += gameTime.ElapsedGameTime.Milliseconds;
                CycleCredits();
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, String transition)
        {
            switch(transition)
            {
                case ("MainMenu"):
                    String title = "Super" + "\n" + "Koopa Bros";
                    String onePlayer = "1 Player Mode" + "\n\n\n\n";
                    String twoPlayer = "2 Player Mode";
                    String cursor = "--->";
                    OnePlayerPos = new Vector2((float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(onePlayer).X / 2, (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(onePlayer).Y / 2);
                    TwoPlayerPos = new Vector2((float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(twoPlayer).X / 2, (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(twoPlayer).Y / 2);
                    spriteBatch.DrawString(spriteFont, title, new Vector2(OnePlayerPos.X, OnePlayerPos.Y - 100), Color.White);
                    spriteBatch.DrawString(spriteFont, onePlayer, OnePlayerPos, Color.White);
                    spriteBatch.DrawString(spriteFont, twoPlayer, TwoPlayerPos, Color.White);
                    if (CursorPos == new Vector2(0,0))
                    {
                        CursorPos = new Vector2(OnePlayerPos.X - 100, OnePlayerPos.Y);
                    }
                    spriteBatch.DrawString(spriteFont, cursor, CursorPos, Color.White);
                    break;
                case("Paused"):
                    spriteBatch.Draw(PausedShader, new Rectangle(Game1.Instance.Camera.Point, new Point((int)Game1.Instance.GameVariables.ScreenWidth * 2, (int)Game1.Instance.GameVariables.ScreenHeight * 2)), Color.White);
                    spriteBatch.DrawString(spriteFont, "Paused", new Vector2(Game1.Instance.Camera.Point.X + (float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString("Paused").X / 2, Game1.Instance.Camera.Point.Y + (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString("Paused").Y / 2), Color.White);
                    break;
                case ("GameOver"):
                    spriteBatch.Draw(BlackTexture, new Rectangle(Game1.Instance.Camera.Point, new Point((int)Game1.Instance.GameVariables.ScreenWidth * 2, (int)Game1.Instance.GameVariables.ScreenHeight * 2)), Color.Black);
                    String gameOver = "Game Over" + "\n\n\n\n";
                    String reset = "Please Press \"r\" to start over";
                    spriteBatch.DrawString(spriteFont, gameOver, new Vector2((float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(gameOver).X / 2, (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(gameOver).Y / 2), Color.White);
                    spriteBatch.DrawString(spriteFont, reset, new Vector2((float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(reset).X / 2, (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(reset).Y / 2), Color.White);
                    break;
                case ("Transition"):
                    spriteBatch.Draw(BlackTexture, new Rectangle(Game1.Instance.Camera.Point, new Point((int)Game1.Instance.GameVariables.ScreenWidth * 2, (int)Game1.Instance.GameVariables.ScreenHeight * 2)), Color.Black);
                    String printLives = "Lives: " + Game1.Instance.GameVariables.Lives + "\n\n\n\n";
                    String printLevel = "World: 1-" + Game1.Instance.GameVariables.LevelNumber;
                    spriteBatch.DrawString(spriteFont, printLives, new Vector2((float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(printLives).X / 2, (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(printLives).Y / 2), Color.White);
                    spriteBatch.DrawString(spriteFont, printLevel, new Vector2((float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(printLevel).X / 2, (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(printLevel).Y / 2), Color.White);
                    break;
                case ("GameComplete"):
                    spriteBatch.Draw(BlackTexture, new Rectangle(Game1.Instance.Camera.Point, new Point((int)Game1.Instance.GameVariables.ScreenWidth * 2, (int)Game1.Instance.GameVariables.ScreenHeight * 2)), Color.Black);
                    String gameComplete = "From the KoopaTroopas...";
                    Vector2 gameCompletePos = new Vector2(Game1.Instance.Camera.Point.X + (float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(gameComplete).X / 2, (Game1.Instance.Camera.Point.Y + (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(gameComplete).Y / 2) - 100);
                    String thanks = "Thanks for playing!";
                    spriteBatch.DrawString(spriteFont, gameComplete, gameCompletePos, Color.White);
                    spriteBatch.DrawString(spriteFont, thanks, new Vector2(Game1.Instance.Camera.Point.X + (float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(thanks).X / 2, (Game1.Instance.Camera.Point.Y + (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(thanks).Y / 2) - 50), Color.White);
                    spriteBatch.DrawString(spriteFont, Name, new Vector2(Game1.Instance.Camera.Point.X + (float)Game1.Instance.GameVariables.ScreenWidth / 2 - spriteFont.MeasureString(Name).X / 2, (Game1.Instance.Camera.Point.Y + (float)Game1.Instance.GameVariables.ScreenHeight / 2 - spriteFont.MeasureString(Name).Y / 2) + 50), Color.White);
                    break;
            }
        }
    }
}
