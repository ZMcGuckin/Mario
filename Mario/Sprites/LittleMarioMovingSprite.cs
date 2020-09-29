using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class LittleMarioMovingSprite : AbstractMarioSprite
    {
        double elapsedTime = 0;
        //Little Mario Spritesheet locations
        readonly int LittleMarioRunningOne = 0;
        readonly int LittleMarioRunningTwo = 5;

        public LittleMarioMovingSprite(Texture2D littleMarioSheet)
        {
            Texture = littleMarioSheet;
            CurrentFrame = LittleMarioRunningOne;
        }

        public override void Update(GameTime gameTime, Boolean faceRight)
        {
            this.FacingRight = faceRight;
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > Interval)
            {
                if(CurrentFrame == LittleMarioRunningOne)
                {
                    CurrentFrame = LittleMarioRunningTwo;
                }
                else
                {
                    CurrentFrame = LittleMarioRunningOne;
                }
                elapsedTime = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, int rowAlter, Vector2 location)
        {
            int width;
            int height;
            int row = CurrentFrame / Cols;
            row += rowAlter;
            row %= Rows;
            int column = CurrentFrame % Cols;
            width = Texture.Width / Cols;
            height = Texture.Height / Rows;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * GameConstants.PlayerScaleFactor, height * GameConstants.PlayerScaleFactor);

            if (!FacingRight)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public override Rectangle HitBox(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, Texture.Width / Cols * GameConstants.PlayerScaleFactor, (int)(Texture.Height / Rows) * GameConstants.PlayerScaleFactor);
        }
    }
}
