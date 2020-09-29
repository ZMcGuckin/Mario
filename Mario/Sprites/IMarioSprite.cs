using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public interface IMarioSprite
    {
        Texture2D Texture { get; set; }
        int NumRows { get; }
        int NumCols { get; }
        Rectangle HitBox(Vector2 location);
        void Update(GameTime gameTime, Boolean facingRight);
        void Draw(SpriteBatch spriteBatch, int rowAlter, Vector2 location);
    }
}
