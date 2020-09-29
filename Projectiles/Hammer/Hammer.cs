using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class Hammer : IProjectile
    {
        UniversalSprite Sprite;
        const int XVelocity = -4;
        const int InitialYVelocity = -4;
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }

        public string CollisionType => "IProjectile";

        public string SpecificCollisionType => "Hammer";

        public Hammer(Vector2 location)
        {
            Location = location;
            Velocity = new Vector2(XVelocity, InitialYVelocity);
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("Hammer",Location);
        }

        public Rectangle LocationRect {
            get
            {
                return Sprite.HitBox;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, true);
        }

        public void Update(GameTime gameTime)
        {
            if(Location.Y > Game1.Instance.GameVariables.ScreenHeight)
            {
                Game1.Instance.GameLists.PurgeList.Add(this);
            }
            if (Velocity.Y < 4)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.ProjectileGravity);
            }
            Location += Velocity;
            Sprite.Update(gameTime,Location);
        }
    }
}
