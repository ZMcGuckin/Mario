using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class StarMario : IMario
    {
        IMario mario;
        double starTimer;
        readonly int colorChangeInterval = 100;
        double count;
        public int ColorAlter { get { return mario.ColorAlter; } set { mario.ColorAlter = value; } }
        public int EnemyMultiplier { get; set; }
        public string CollisionType => "IMario";
        public string SpecificCollisionType => "StarMario";
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

        public StarMario(IMario mario)
        {
            this.mario = mario;
            starTimer = 10;
            EnemyMultiplier = 1;
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
            starTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            count += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (count > colorChangeInterval)
            {
                mario.ColorAlter++;
                if (mario.ColorAlter >= 5)
                {
                    mario.ColorAlter = 0;
                }
                count = 0;
            }
            if (starTimer < 0)
            {
                RemoveStar();
            }
            mario.Update(gameTime);
        }
        public void RemoveStar()
        {
            mario.ColorAlter = 0;
            Game1.Instance.RegularMario(this, mario);
        }
        public void ResetStarTimer()
        {
            starTimer = 10;
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
        public static void ChangeToStar()
        {

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
            mario.ActionPressed();
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
