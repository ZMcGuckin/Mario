using System;
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
    public abstract class AbstractItem : IItem
    {
        protected UniversalSprite Sprite { get; set; }
        protected static int Rows
        {
            get
            {
                return 6;
            }
        }
        protected static int Cols
        {
            get
            {
                return 4;
            }
        }
        protected const int scaleFactor = 2;
        public Vector2 Velocity { get; set; }
        public Vector2 Location { get; set; }
        public Boolean Grounded { get; set; }
        public Boolean movingLeft { get; set; }
        public abstract string SpecificCollisionType { get; }

        public string CollisionType => "IItem";
        public Rectangle LocationRect
        {
            get
            {
                return Sprite.HitBox;
                //return new Rectangle((int)Location.X, (int)Location.Y, Sprite.Texture.Width / Cols * scaleFactor, Sprite.Texture.Height / Rows * scaleFactor);
            }
        }

        
        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch,false);
        }

        public void ResetYVelocity()
        {
            Velocity = new Vector2(Velocity.X, 0);
        }

        public void TurnAround()
        {
            Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
            movingLeft = !movingLeft;
        }
    }
}
