using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class SpinyStateMachine
    {
        Boolean Landed;
        const int XVelocity = 1;
        enum SpinyHealth { Normal, Flipped };
        SpinyHealth Health { get; set; }
        UniversalSprite Sprite { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }
       
        public SpinyStateMachine(Vector2 location)
        {
            Location = location;
            Health = SpinyHealth.Normal;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FallingSpiny", Location);
            Velocity = new Vector2(0, 0);
            Landed = false;
        }
        public Rectangle HitBox
        {
            get
            {
                return Sprite.HitBox;
            }
        }
        public Boolean IsFlipped
        {
            get
            {
                return Health != SpinyHealth.Normal;
            }
        }
        public void Move()
        {
            Location = new Vector2(Location.X - Velocity.X, Location.Y);
        }

        public void BeFlipped()
        {
            Health = SpinyHealth.Flipped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FlippedSpiny", Location);
        }
        public void Update(GameTime gameTime, Vector2 location)
        {
            Location = new Vector2(location.X, location.Y + Velocity.Y);
            if (Health == SpinyHealth.Normal)
            {
                Move();
            }
            if (!Grounded && Health == SpinyHealth.Normal)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.GeneralGravity);
            }
            else
            {
                Velocity = new Vector2(Velocity.X, 0);
                if (!Landed)
                {
                    Velocity = new Vector2(XVelocity, 0);
                    Sprite = UniversalSpriteFactory.Instance.CreateSprite("MovingSpiny", Location);
                    Landed = true;
                }
            }
            if (Location.Y > Game1.Instance.GameVariables.ScreenHeight + 100)
            {
                BeFlipped();
            }
            Sprite.Update(gameTime, Location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Velocity.X < 0);
        }
    }
}
