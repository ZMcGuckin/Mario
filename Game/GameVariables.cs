using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class GameVariables
    {
        public SpriteFont SpriteFont { get; set; }
        public Color BackgroundColor { get; set; }
        public int LastPaused { get; set; }
        public int PausedInterval { get; set; }
        public Boolean PlayFinal { get; set; }
        public Boolean Underground { get; set; }
        public int Score { get; set; }
        public int CollectedCoins { get; set; }
        public int Lives { get; set; }
        public int LevelNumber { get; set; }
        public int PlayerNumber { get; set; }
        public int CurrentlyAlive { get; set; }
        public int LevelTimer { get; set; }
        public double ElapsedTime { get; set; }
        public int TotalTime { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }


        public GameVariables()
        {
            PausedInterval = 300;
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.Instance.CurrentState != Game1.GameState.GameOver && Game1.Instance.CurrentState != Game1.GameState.MainMenu)
            {
                if (Game1.Instance.CurrentState == Game1.GameState.Playing || Game1.Instance.CurrentState == Game1.GameState.End || Game1.Instance.CurrentState == Game1.GameState.Dead)
                {
                    if (CollectedCoins > GameConstants.CoinsFor1UP)
                    {
                        CollectedCoins = CollectedCoins % GameConstants.CoinsFor1UP;
                        new OneUp(new Collision(null, null, CollisionSide.Default)).Execute();
                    }

                    ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (LevelTimer > 0 && Game1.Instance.CurrentState != Game1.GameState.Dead)
                    {
                        LevelTimer = TotalTime - (int)Math.Floor((double)ElapsedTime / 1000);
                    }
                }

                LastPaused += gameTime.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
