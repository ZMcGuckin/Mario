using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class FireBall : IProjectile
    {

        UniversalSprite Sprite;
       
        double elapsedTime = 0;
        double deathInterval = 300;
        const int speed = 6;
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public enum FireBallStatus { Moving, Exploded };
        public FireBallStatus Status { get; set; }
        public Boolean Grounded { get; set; }
        public IMario Shooter { get; set; }

        public string CollisionType => "IProjectile";
        public FireBall(Vector2 location, Boolean facingRight, IMario shooter)
        {
          
            Location = location;
            Velocity = facingRight ? new Vector2(speed, Velocity.Y) : new Vector2(-speed, Velocity.Y);
            Status = FireBallStatus.Moving;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FireBall",Location);
            Shooter = shooter;
        }

        public void Update(GameTime gameTime)
        {
            if(Status == FireBallStatus.Moving)
            {
              
                Velocity = new Vector2(Velocity.X, Velocity.Y + 0.25f);
                Location += Velocity;
            }
            else
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if(elapsedTime>deathInterval)
                {
                    Game1.Instance.GameLists.PurgeList.Add(this);
                }
            }
            Sprite.Update(gameTime,Location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, false);
        }

        public void Explode()
        {
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FireBallExplosion",Location);
            Status = FireBallStatus.Exploded;
        }

        public void Bounce()
        {
            Velocity = new Vector2(Velocity.X, -4);
        }

        public Rectangle LocationRect
        {
            get
            {
                return Sprite.HitBox;
            }
        }
        public string SpecificCollisionType
        {
            get
            {
                switch (Status)
                {
                    case (FireBallStatus.Moving):
                        return "FireBall";
                    default:
                        return "ExplodedFireBall";
                }
            }
        }
    }
}
