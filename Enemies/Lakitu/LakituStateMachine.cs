using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class LakituStateMachine
    {
        const double throwInterval = 3000;
        double throwElapsedTime = 0;
        const double duckInterval = 250;
        double duckElapsedTime = 0;
        const int lakituHead = 8;
        Boolean ducking;
        enum LakituHealth { Normal, Flipped };
        LakituHealth Health;
        UniversalSprite Sprite { get; set; }
        Random rnd = new Random();
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }
        public LakituStateMachine(Vector2 location)
        {
            Location = location;
            Health = LakituHealth.Normal;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("NormalLakitu",Location);
            ducking = false;
            Velocity = new Vector2(3, 0);
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
                return Health == LakituHealth.Flipped;
            }
        }
        public void Move()
        {
            Location = new Vector2(Location.X - Velocity.X, Location.Y);
            if (Location.X < Game1.Instance.Camera.Point.X)
            {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
                Location = new Vector2(Game1.Instance.Camera.Point.X + 1, Location.Y);
            }
            if (Location.X > Game1.Instance.Camera.Point.X + Game1.Instance.GameVariables.ScreenWidth - Sprite.HitBox.Width)
            {
                Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
                Location = new Vector2((float)(Game1.Instance.Camera.Point.X + Game1.Instance.GameVariables.ScreenWidth - Sprite.HitBox.Width - 1), Location.Y);
            }
        }
        public void BeStomped()
        {
            BeFlipped();
        }

        public void BeFlipped()
        {
            Velocity = new Vector2(0, 0);
            Health = LakituHealth.Flipped;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FlippedLakitu", Location);
        }
        public void Jump()
        {
            Velocity = new Vector2(Velocity.X, -rnd.Next(4, 8));
        }
        public void Update(GameTime gameTime, Vector2 location)
        {
            if (ducking)
            {
                duckElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (duckElapsedTime > duckInterval)
            {
                Sprite = UniversalSpriteFactory.Instance.CreateSprite("NormalLakitu",Location);
                ducking = false;
                new ThrowSpiny(new Vector2(Location.X, Location.Y)).Execute();
                duckElapsedTime = 0;
            }

            throwElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (throwElapsedTime > throwInterval && Health == LakituHealth.Normal && Game1.Instance.CurrentState == Game1.GameState.Playing)
            {
                Sprite = UniversalSpriteFactory.Instance.CreateSprite("DuckLakitu",Location);
                ducking = true;
                throwElapsedTime = 0;
            }


            Location = new Vector2(location.X, location.Y + Velocity.Y);
            if (Health == LakituHealth.Normal)
            {
                Move();
            }
            else{
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.GeneralGravity);
            }
            Sprite.Update(gameTime, Location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, true);
        }
    }
}
