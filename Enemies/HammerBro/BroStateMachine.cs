using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class BroStateMachine
    {
        const double jumpInterval = 1000;
        double jumpElapsedTime = 0;
        const double throwInterval = 250;
        double throwElapsedTime = 0;
        Boolean throwing;
        enum BroHealth { Normal, Flipped };
        BroHealth Health;
        Tuple<float,float> MovementRange;
        UniversalSprite Sprite { get; set; }
        Random rnd = new Random();
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }
        public BroStateMachine(Vector2 location)
        {
            Location = location;
            Health = BroHealth.Normal;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("MovingBro", Location);
            throwing = false;
            Velocity = new Vector2(1, 0);
            MovementRange = new Tuple<float,float>(Location.X - Sprite.HitBox.Width, Location.X + Sprite.HitBox.Width);
        }
        public Rectangle HitBox
        {
            get
            {
                return Sprite.HitBox;
            }
        }
        public Boolean IsFlipped {
            get
            {
                return Health == BroHealth.Flipped;
            }
        }
        public void Move()
        {
            Location = new Vector2(Location.X - Velocity.X, Location.Y);
            if(Location.X < MovementRange.Item1)
            {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
                Location = new Vector2(MovementRange.Item1+1, Location.Y);
            }
            if(Location.X > MovementRange.Item2)
            {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
                Location = new Vector2(MovementRange.Item2-1, Location.Y);
            }
        }
        public void BeStomped()
        {
            BeFlipped();
        }

        public void BeFlipped()
        {
            Velocity = new Vector2(0, 0);
            Health = BroHealth.Flipped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FlippedBro", Location);
        }
        public void Jump()
        {
            Velocity = new Vector2(Velocity.X, -rnd.Next(4, 8));
        }
        public void Update(GameTime gameTime, Vector2 location)
        {
            if(throwing)
            {
                throwElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(throwElapsedTime > throwInterval)
            {
                Sprite = UniversalSpriteFactory.Instance.CreateSprite("MovingBro", Location);
                throwing = false;
                (new SpawnHammerCommand(Location)).Execute();
                throwElapsedTime = 0;
            }

            jumpElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (jumpElapsedTime > jumpInterval && Health == BroHealth.Normal)
            {
                if (Grounded)
                {
                    Jump();
                }
                Sprite = UniversalSpriteFactory.Instance.CreateSprite("ThrowBro", Location);
                throwing = true;
                jumpElapsedTime = 0;
            }

            Location = new Vector2(location.X, location.Y + Velocity.Y);
            if (Health == BroHealth.Normal)
            {
                Move();
            }
            if (!Grounded)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.GeneralGravity);
            }
            if (Location.Y > Game1.Instance.GameVariables.ScreenHeight + 100)
            {
                BeFlipped();
            }
            Sprite.Update(gameTime, Location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, true);
        }
    }
}
