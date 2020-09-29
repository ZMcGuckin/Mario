using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TheKoopaTroopas
{
    public class Mario : IMario
    {
        public MarioStateMachine StateMachine { get; set; }
        public int ColorAlter { get; set; }
        public int EnemyMultiplier { get; set; }
        public int PlayerNumber { get; set; }
        double deathElapsedTime;
        public string CollisionType => "IMario";
        public string SpecificCollisionType
        {
            get
            {
                return StateMachine.SpecificCollisionType;
            }
        }
        public Mario(Vector2 location, int playerNumber)
        {
            PlayerNumber = playerNumber;
            EnemyMultiplier = 1;
            StateMachine = new MarioStateMachine(location, this);
        }
        public void Update(GameTime gameTime)
        {
            //if mario is dead, take him off the game after a specific amount of time
            if (State is DeadMarioState)
            {
                deathElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (deathElapsedTime > GameConstants.MarioDeathInterval)
                {
                    Game1.Instance.KillMario(PlayerNumber);
                }
            }

            if (Grounded)
            {
                EnemyMultiplier = 1;
            }

            StateMachine.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            StateMachine.Draw(spriteBatch, ColorAlter + 2 * PlayerNumber);
        }
        public void TakeDamage()
        {
            if(StateMachine.State is LittleMarioState)
            {
                StateMachine.ChangeToDead();
            }
            else
            {
                Game1.Instance.DamageMario(this, new LittleMarioState());
            }
        }
        public void Jump()
        {
            StateMachine.Jump();
        }
        public void EnemyJump()
        {
            StateMachine.EnemyJump();
        }
        public void Crouch()
        {
            StateMachine.Crouch();
        }
        public void Move(GameTime gameTime, Boolean movingRight)
        {
            StateMachine.Move(gameTime, movingRight);
        }
        public void Idle()
        {
            StateMachine.Idle();
        }
        public Boolean Right()
        {
            return StateMachine.Right();
        }
        public void ChangeToLittle()
        {
            StateMachine.ChangeToLittle();
        }
        public void ChangeToBig()
        {
            StateMachine.ChangeToBig();
        }
        public void ChangeToFire()
        {
            StateMachine.ChangeToFire();
        }
        public void ChangeToDead()
        {
            StateMachine.ChangeToDead();
        }
        public Vector2 Location
        {
            get
            {
                return StateMachine.Location;
            }
            set
            {
                StateMachine.Location = value;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return StateMachine.Grounded;
            }
            set
            {
                StateMachine.Grounded = value;
            }
        }
        public Boolean CanBreakBlocks()
        {
            return StateMachine.CanBreakBlocks();
        }
        public Boolean IsJumping()
        {
            return StateMachine.IsJumping();
        }
        public Boolean IsCrouching()
        {
            return StateMachine.IsCrouching();
        }
        public Boolean IsLittleMario()
        {
            return StateMachine.State is LittleMarioState;
        }
        public void ActionPressed()
        {
            StateMachine.ActionPressed();
        }
        public void Land()
        {
            StateMachine.Land();
        }
        public int GetXVelocity
        {
            get
            {
                return (int)Math.Abs(StateMachine.Velocity.X);
            }
        }

        public Rectangle LocationRect
        {
            get
            {
                return StateMachine.HitBox();
            }
        }
      
        public int GetYVelocity
        {
            get
            {
                return (int)Math.Abs(StateMachine.Velocity.Y);
            }
        }
        public IMarioState State
        {
            get
            {
                return StateMachine.State;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return StateMachine.Velocity;
            }
            set
            {
                StateMachine.Velocity = value;
            }
        }
    }
}
