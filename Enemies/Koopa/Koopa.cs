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
    public class Koopa : IEnemy
    {
        double flipElapsedTime = 0;
        KoopaStateMachine koopaStateMachine;
        public IMario Pusher { get; set; }
        public Koopa(Vector2 location)
        {
            koopaStateMachine = new KoopaStateMachine(location);
        }
        public string CollisionType => "IEnemy";
        public string SpecificCollisionType
        {
            get
            {
                return koopaStateMachine.SpecificCollisionType;
            }
        }
        public Rectangle LocationRect
        {
            get
            {
                return koopaStateMachine.HitBox;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return koopaStateMachine.Velocity;
            }
            set
            {
                koopaStateMachine.Velocity = value;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return koopaStateMachine.Grounded;
            }
            set
            {
                koopaStateMachine.Grounded = value;
            }
        }
        public Vector2 Location
        {
            get
            {
                return koopaStateMachine.Location;
            }
            set
            {
                koopaStateMachine.Location = value;
            }
        }
        public void BeStomped(IMario mario)
        {
            koopaStateMachine.BeStomped();

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
            koopaStateMachine.BeFlipped();
            if (mario != null)
            {
                new AquirePoints(mario, GameConstants.BasePoints).Execute();
                mario.EnemyMultiplier++;
            }
        }
  
        public void PushShell()
        {
            koopaStateMachine.PushShell();
        }

        public void TurnAround()
        {
            koopaStateMachine.TurnAround();
        }

        public void Update(GameTime gameTime)
        {
                if (koopaStateMachine.IsFlipped)
                {
                    flipElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                    if (flipElapsedTime > GameConstants.DeathInterval)
                    {
                        Game1.Instance.GameLists.PurgeList.Add(this);
                    }
                }
                koopaStateMachine.Update(gameTime, Location);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                koopaStateMachine.Draw(spriteBatch);
        }
    }
}
