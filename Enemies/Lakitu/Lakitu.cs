using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas 
{
    public class Lakitu : IEnemy
    {
        double flipElapsedTime = 0;
        LakituStateMachine lakituStateMachine;
        public Lakitu(Vector2 location)
        {
            lakituStateMachine = new LakituStateMachine(location);
        }
        public string CollisionType => "IEnemy";
        public string SpecificCollisionType => "Lakitu";
        public Rectangle LocationRect
        {
            get
            {
                return lakituStateMachine.HitBox;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return lakituStateMachine.Velocity;
            }
            set
            {
                lakituStateMachine.Velocity = value;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return lakituStateMachine.Grounded;
            }
            set
            {
                lakituStateMachine.Grounded = value;
            }
        }
        public Vector2 Location
        {
            get
            {
                return lakituStateMachine.Location;
            }
            set
            {
                lakituStateMachine.Location = value;
            }
        }
        public void BeStomped(IMario mario)
        {
            lakituStateMachine.BeStomped();

            if (mario.EnemyMultiplier == GameConstants.StompsFor1UP)
            {
                new OneUp(new Collision(mario, null, CollisionSide.Default)).Execute();
            }
            else
            {
                new AquirePoints(mario, GameConstants.BasePoints * mario.EnemyMultiplier).Execute();
                mario.EnemyMultiplier++;
            }
        }

        public void BeFlipped(IMario mario)
        {
            lakituStateMachine.BeFlipped();
            if (mario != null)
            {
                new AquirePoints(mario, GameConstants.BasePoints).Execute();
                mario.EnemyMultiplier++;
            }
        }

        public void TurnAround()
        {
            Velocity = new Vector2(Velocity.X * -1, Velocity.Y);
        }

        public void Update(GameTime gameTime)
        {
            if (lakituStateMachine.IsFlipped)
            {
                flipElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (flipElapsedTime > GameConstants.DeathInterval)
                {
                    Game1.Instance.GameLists.PurgeList.Add(this);
                }
            }
            lakituStateMachine.Update(gameTime, Location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            lakituStateMachine.Draw(spriteBatch);
        }
    }
}
