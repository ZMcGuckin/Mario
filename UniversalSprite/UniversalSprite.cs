using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas { 
    public class UniversalSprite : ISprite
    {
        int Rows;
        int Cols;
        int[] FrameList;
        int CurrentFrameIndex;
        int HitBoxAlterX;
        int HitBoxAlterY;
        int ScaleFactor;         
        double Interval = 150;
        double elapsedTime = 0;

        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public UniversalSprite(Texture2D spriteSheet,int rows, int columns,int[] frameList, Vector2 location,int hitBoxAlterX, int hitBoxAlterY, int scaleFactor)
        {
            Location = location;
            Cols = columns;
            Rows = rows;
            FrameList = frameList;
            Texture = spriteSheet;
            CurrentFrameIndex = 0;
            HitBoxAlterX = hitBoxAlterX;
            HitBoxAlterY = hitBoxAlterY;
            ScaleFactor = scaleFactor;
        }

        public void Update(GameTime gameTime, Vector2 location)
        {
            Location = location;
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > Interval)
            {
                CurrentFrameIndex++;
                if (CurrentFrameIndex >= FrameList.Length)
                    CurrentFrameIndex = 0;
                elapsedTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Boolean faceLeft)
        {
            int width;
            int height;
            int row = (FrameList[CurrentFrameIndex] / Cols) % Rows;
            int column = FrameList[CurrentFrameIndex] % Cols;
            width = Texture.Width / Cols;
            height = Texture.Height / Rows;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, width * ScaleFactor, height * ScaleFactor);

            if (!faceLeft)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
        }
        public Rectangle HitBox
        {
            get
            {
                if (HitBoxAlterY != -1) { 
                    Rectangle temp = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width / Cols * ScaleFactor, Texture.Height / Rows * ScaleFactor);
                    return new Rectangle((int)temp.X + HitBoxAlterX * ScaleFactor, ((int)temp.Y) + HitBoxAlterY * ScaleFactor, temp.Width - HitBoxAlterX * ScaleFactor, temp.Height - HitBoxAlterY * ScaleFactor);
                }
                else
                {
                    return new Rectangle();
                }
            }
        }
    }
}
