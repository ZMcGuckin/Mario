using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class GoombaStateMachine
    {

        enum GoombaHealth { Normal, Stomped, Flipped };
        GoombaHealth Health { get; set; }
        UniversalSprite Sprite { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }

        public GoombaStateMachine(Vector2 location)
        {
            Location = location;
            Health = GoombaHealth.Normal;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("MovingGoomba", Location);
            Velocity = new Vector2(1, 0);
        }
        public Rectangle HitBox
        {
            get
            {
                return Sprite.HitBox;
            }
        }
        public Boolean IsStompedOrFlipped
        {
            get
            {
                return Health != GoombaHealth.Normal;
            }
        }
        public void Move()
        {
            Location = new Vector2(Location.X - Velocity.X, Location.Y);
        }
        public void BeStomped()
        {
            Health = GoombaHealth.Stomped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("StompedGoomba", Location);
        }

        public void BeFlipped()
        {
            Health = GoombaHealth.Flipped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FlippedGoomba", Location);
        }
        public void Update(GameTime gameTime, Vector2 location)
        {
            Location = new Vector2(location.X,location.Y + Velocity.Y);
            if (Health == GoombaHealth.Normal)
            {
                Move();
            }
            if (!Grounded && Health == GoombaHealth.Normal)
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
                Sprite.Draw(spriteBatch,Velocity.X < 0);
        }
    }
}
