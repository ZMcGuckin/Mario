using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class TransitionMario : IMario
    {
        IMario mario;
        int transitionTime = 1000;
        int elapsedTime;
        int interval = 50;
        Boolean isNewState = false;
        IMarioState oldState;
        IMarioState newState;
        public int ColorAlter { get { return mario.ColorAlter; } set { mario.ColorAlter = value; } }
        public int EnemyMultiplier
        {
            get
            {
                return mario.EnemyMultiplier;
            }
            set
            {
                mario.EnemyMultiplier = value;
            }
        }
        public string CollisionType => "IMario";
        public string SpecificCollisionType => "TransitionMario";
        public Vector2 Velocity
        {
            get
            {
                return mario.Velocity;
            }
            set
            {
                mario.Velocity = value;
            }
        }
        public TransitionMario(IMario mario, IMarioState oldState, IMarioState newState)
        {
            this.oldState = oldState;
            this.newState = newState;
            this.mario = mario;
        }
        public void Crouch()
        {
            mario.Crouch();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            mario.Draw(spriteBatch);
        }
        public void Jump()
        {
            mario.Jump();
        }
        public void EnemyJump()
        {
            mario.EnemyJump();
        }
        public void Move(GameTime gameTime, Boolean movingRight)
        {
            mario.Move(gameTime, movingRight);
        }
        public void TakeDamage()
        {
            //Doesn't take damage
        }
        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime>interval)
            {
                if(isNewState)
                {
                    ChangeToOldStatus();
                }
                else
                {
                    ChangeToNewStatus();
                }
                transitionTime -= elapsedTime;
                elapsedTime = 0;
            }
            if (transitionTime < 0)
            {
                RemoveTransition();
            }
            mario.Update(gameTime);
        }
        public void ChangeToNewStatus()
        {
            isNewState = true;
            if(newState is FireMarioState)
            {
                ChangeToFire();
            }
            else if(newState is LittleMarioState)
            {
                ChangeToLittle();
            }
            else if(newState is BigMarioState)
            {
                ChangeToBig();
            }
        }
        public void ChangeToOldStatus()
        {
            isNewState = false;
            if (oldState is FireMarioState)
            {
                ChangeToFire();
            }
            else if (oldState is LittleMarioState)
            {
                ChangeToLittle();
            }
            else if (oldState is BigMarioState)
            {
                ChangeToBig();
            }
        }
        public void RemoveTransition()
        {
            ChangeToNewStatus();
            Game1.Instance.RegularMario(this, mario);
        }
        public void Idle()
        {
            mario.Idle();
        }
        public void ChangeToLittle()
        {
            mario.ChangeToLittle();
        }
        public void ChangeToBig()
        {
            mario.ChangeToBig();
        }
        public void ChangeToFire()
        {
            mario.ChangeToFire();
        }
        public void ChangeToDead()
        {
            mario.ChangeToDead();
        }
        public bool Right()
        {
            return mario.Right();
        }
        public Rectangle LocationRect
        {
            get
            {
                return mario.LocationRect;
            }
        }
        public Vector2 Location
        {
            get
            {
                return mario.Location;
            }
            set
            {
                mario.Location = value;
            }
        }
        public Boolean Grounded
        {
            get
            {
                return mario.Grounded;
            }
            set
            {
                mario.Grounded = value;
            }
        }
        public Boolean CanBreakBlocks()
        {
            return mario.CanBreakBlocks();
        }
        public Boolean IsJumping()
        {
            return mario.IsJumping();
        }
        public Boolean IsCrouching()
        {
            return mario.IsCrouching();
        }
        public Boolean IsLittleMario()
        {
            return mario.IsLittleMario();
        }
        public void ActionPressed()
        {
            //Cannot commit action during transition
        }
        public void Land()
        {
            mario.Land();
        }
        public int GetXVelocity
        {
            get
            {
                return (int)Math.Abs(mario.GetXVelocity);
            }
        }
        public int GetYVelocity
        {
            get
            {
                return (int)Math.Abs(mario.GetYVelocity);
            }
        }
        public IMarioState State
        {
            get
            {
                return mario.State;
            }
        }
    }
}
