using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheKoopaTroopas
{
    public class Goomba : IEnemy
    {
        double elapsedTime = 0;
        GoombaStateMachine goombaStateMachine;
        public Goomba(Vector2 location)
        {
            goombaStateMachine = new GoombaStateMachine(location);
        }
        public string CollisionType => "IEnemy";
        public string SpecificCollisionType => "Goomba";
        public Vector2 Velocity {
            get
            {
                return goombaStateMachine.Velocity;
            }
            set
            {
                goombaStateMachine.Velocity = value;
            }
        }
        public Rectangle LocationRect
        {
            get
            {
                return goombaStateMachine.HitBox;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return goombaStateMachine.Grounded;
            }
            set
            {
                goombaStateMachine.Grounded = value;
            }
        }
        public Vector2 Location
        {
            get
            {
                return goombaStateMachine.Location;
            }
            set
            {
                goombaStateMachine.Location = value;
            }
        }

        public void BeStomped(IMario mario)
        {
            goombaStateMachine.BeStomped();
            if (mario.EnemyMultiplier == GameConstants.StompsFor1UP)
            {
                new OneUp(new Collision(mario, null, CollisionSide.Default)).Execute();
            }
            else
            {
                new AquirePoints(mario, GameConstants.BasePoints*mario.EnemyMultiplier).Execute();
                mario.EnemyMultiplier++;
            }
        }

        public void BeFlipped(IMario mario)
        {
            goombaStateMachine.BeFlipped();
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
                if (goombaStateMachine.IsStompedOrFlipped)
                {
                    elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                    if (elapsedTime > GameConstants.DeathInterval)
                    {
                        Game1.Instance.GameLists.PurgeList.Add(this);
                    }
                }

                goombaStateMachine.Update(gameTime, Location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                goombaStateMachine.Draw(spriteBatch);
        }


    }
}
