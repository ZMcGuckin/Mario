using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class HUD
    {
        private int Score;
        private int Coins;
        private int Lives;
        private int CountDown;
        private string World;
        private Vector2 Location;
        private readonly int Seperation = (int)(Game1.Instance.GameVariables.ScreenWidth / 5);

        public HUD(int countDown)
        {
            Score = 0;
            Coins = 0;
            Lives = 0;
            CountDown = countDown;
            World = "1-1";
            Location = new Vector2(0, 0);
        }
        public void Update(int countDown, Vector2 location, int level)
        {
            Score = Game1.Instance.GameVariables.Score;
            Coins = Game1.Instance.GameVariables.CollectedCoins;
            Lives = Game1.Instance.GameVariables.Lives;
            CountDown = countDown;
            Location = location;
            World = "1-" + level;
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont,Color textColor)
        {
            float locationX = Location.X;
            spriteBatch.DrawString(spriteFont, "Mario\n" + Score, new Vector2(locationX, Location.Y), textColor);
            locationX += Seperation;
            spriteBatch.DrawString(spriteFont, "Coins\n" + Coins, new Vector2(locationX, Location.Y), textColor);
            locationX += Seperation;
            spriteBatch.DrawString(spriteFont, "World\n"+ World, new Vector2(locationX, Location.Y), textColor);
            locationX += Seperation;
            spriteBatch.DrawString(spriteFont, "Time\n" + CountDown, new Vector2(locationX, Location.Y), textColor);
            locationX += Seperation;
            spriteBatch.DrawString(spriteFont, "Lives\n" + Lives, new Vector2(locationX, Location.Y), textColor);

        }
    }
}
