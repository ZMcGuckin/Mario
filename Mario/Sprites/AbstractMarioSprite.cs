using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheKoopaTroopas
{
    public abstract class AbstractMarioSprite : IMarioSprite
    {
        public static double Interval { get { return 100; } }
        public static int Rows { get { return 5; } }
        public static int Cols { get { return 9; } }
        public int CurrentFrame { get; set; }
        public Boolean FacingRight { get; set; }
        public Texture2D Texture { get; set; }
        public int NumRows { get { return Rows; } }
        public int NumCols { get { return Cols; } }

        public abstract void Update(GameTime gameTime, Boolean faceRight);
        public abstract void Draw(SpriteBatch spriteBatch, int rowAlter, Vector2 location);
        public abstract Rectangle HitBox(Vector2 location);
    }
}
