using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public interface ISprite
    {
        Texture2D Texture { get; set; }
        Vector2 Location { get; set; }
        void Update(GameTime gameTime,Vector2 location);
        void Draw(SpriteBatch spriteBatch,Boolean faceLeft);
    }
}
