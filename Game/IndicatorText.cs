using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class IndicatorText
    {
        string Indicator;
        int ElapsedTime;
        Vector2 Location;
        readonly int VisableInterval = 1000;
        public IndicatorText(String value, Vector2 location)
        {
            ElapsedTime = 0;
            Indicator = value;
            Location = location;
        }

        public void Draw(SpriteBatch spriteBatch,SpriteFont spriteFont)
        {
            Vector2 centeredLocation = new Vector2(Location.X - spriteFont.MeasureString(Indicator).X/2,Location.Y - spriteFont.MeasureString(Indicator).Y);
            spriteBatch.DrawString(spriteFont, Indicator, centeredLocation, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if(VisableInterval > ElapsedTime)
            {
                ElapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                Location.Y -= 0.3f;
            }
            else
            {
                Game1.Instance.GameLists.IndicatorText.Remove(this);
            }
        }
    }
}
