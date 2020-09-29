using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class HammerBro : IEnemy
    {
      
        double flipElapsedTime = 0;
        BroStateMachine broStateMachine;
        public HammerBro(Vector2 location)
        {
            broStateMachine = new BroStateMachine(location);
        }
        public string CollisionType => "IEnemy";
        public string SpecificCollisionType => "HammerBro";
        public Rectangle LocationRect
        {
            get
            {
                return broStateMachine.HitBox;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return broStateMachine.Velocity;
            }
            set
            {
                broStateMachine.Velocity = value;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return broStateMachine.Grounded;
            }
            set
            {
                broStateMachine.Grounded = value;
            }
        }
        public Vector2 Location
        {
            get
            {
                return broStateMachine.Location;
            }
            set
            {
                broStateMachine.Location = value;
            }
        }
        public void BeStomped(IMario mario)
        {
            broStateMachine.BeStomped();

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
            broStateMachine.BeFlipped();
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
            if (broStateMachine.IsFlipped)
            {
                flipElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (flipElapsedTime > GameConstants.DeathInterval)
                {
                    Game1.Instance.GameLists.PurgeList.Add(this);
                }
            }
            broStateMachine.Update(gameTime, Location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            broStateMachine.Draw(spriteBatch);
        }
    }
}
