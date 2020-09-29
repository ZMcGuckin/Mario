using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class Spiny :IEnemy
    {
        double elapsedTime = 0;
        SpinyStateMachine spinyStateMachine;
        public Spiny(Vector2 location)
        {
            spinyStateMachine = new SpinyStateMachine(location);
        }
        public string CollisionType => "IEnemy";
        public string SpecificCollisionType => "Spiny";
        public Vector2 Velocity
        {
            get
            {
                return spinyStateMachine.Velocity;
            }
            set
            {
                spinyStateMachine.Velocity = value;
            }
        }
        public Rectangle LocationRect
        {
            get
            {
                return spinyStateMachine.HitBox;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return spinyStateMachine.Grounded;
            }
            set
            {
                spinyStateMachine.Grounded = value;
            }
        }
        public Vector2 Location
        {
            get
            {
                return spinyStateMachine.Location;
            }
            set
            {
                spinyStateMachine.Location = value;
            }
        }

        public void BeStomped(IMario mario)
        {
            // cant be stomped
        }

        public void BeFlipped(IMario mario)
        {
            spinyStateMachine.BeFlipped();
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
            if (spinyStateMachine.IsFlipped)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime > GameConstants.DeathInterval)
                {
                    Game1.Instance.GameLists.PurgeList.Add(this);
                }
            }

            spinyStateMachine.Update(gameTime, Location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spinyStateMachine.Draw(spriteBatch);
        }

    }
}
