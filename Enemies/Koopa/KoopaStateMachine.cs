using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
  
    public class KoopaStateMachine
    {
        const int XWalkVelocity = 1;
        const int XShellVelocity = 5;
        const int koopaHead = 8;
        Boolean Collided;
        enum KoopaHealth { Normal, Stomped, Flipped };
        KoopaHealth health;
        UniversalSprite Sprite { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }


        public KoopaStateMachine(Vector2 location)
        {
            Collided = false;
            Location = location;
            health = KoopaHealth.Normal;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("MovingKoopa", Location);
            Velocity = new Vector2(XWalkVelocity, 0);
        }
        public string SpecificCollisionType
        {
            get
            {
                if (health == KoopaHealth.Stomped && Collided)
                {
                    return "MovingShell";
                }
                else if (health == KoopaHealth.Stomped && !Collided)
                {
                    return "Shell";
                }
                else
                {
                    return "Koopa";
                }
            }
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
                return health == KoopaHealth.Flipped;
            }
        }
        public void Move()
        {
            Location = new Vector2(Location.X - Velocity.X, Location.Y);
        }

        public void BeStomped()
        {
            health = KoopaHealth.Stomped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("ShellKoopa", Location);
            Velocity = new Vector2(XShellVelocity, Velocity.Y);
        }

        public void BeFlipped()
        {
            health = KoopaHealth.Flipped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FlippedKoopa", Location);
            Velocity = new Vector2(0, 0);
        }
       
        public void PushShell()
        {
            Collided = true;
        }
        public void TurnAround()
        {
            if (health != KoopaHealth.Stomped || Collided) {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
            }
        }
        public void Update(GameTime gameTime, Vector2 location)
        {
            Location = new Vector2(location.X, location.Y + Velocity.Y);

            if (Collided || health == KoopaHealth.Normal)
            {
                Move();
            }

            if (!Grounded && health != KoopaHealth.Flipped)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.GeneralGravity);
            }
            else
            {
                Velocity = new Vector2(Velocity.X, 0);
            }

            if (Location.Y > Game1.Instance.GameVariables.ScreenHeight + 100)
            {
                BeFlipped();
            }
            Sprite.Update(gameTime, Location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Velocity.X > 0);
        }
    }
}
