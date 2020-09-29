using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class MarioStateMachine
    {
        Boolean facingRight = true;
        readonly int textureDifference = 32;
        readonly double movementInterval = 100;
        readonly double fireBallInterval = 500;
        double lastMoved;
        double elapsedTime;
        double fireBallLastShot;
        public Boolean Grounded { get; set; }
        public Boolean FlagPoleRiding { get; set; }
        public enum MarioMovement { Jumping, Crouching, Idle, Moving, Slide };
        public MarioMovement Movement { get; set; }
        public IMarioState State { get; set; }
        public IMario Parent { get; set; }

        readonly int runningVelocity = 8;
        readonly int standardVelocity = 5;
        readonly int jumpVelocity = 9;
        readonly int hopVelocity = 4;
        int maxXVelocity;
        public IMarioSprite Sprite { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public String SpecificCollisionType{
            get{
                if (State is FireMarioState)
                {
                    return "FireMario";
                }
                if (State is BigMarioState)
                {
                    return "BigMario";
                }
                if (State is LittleMarioState)
                {
                    return "LittleMario";
                }
                return "DeadMario";
            }
        }
        public MarioStateMachine(Vector2 location, IMario parent)
        {
            Location = location;
            maxXVelocity = standardVelocity;
            State = new LittleMarioState();
            Sprite = State.Sprite;
            Idle();
            Parent = parent;
        }

        public void Crouch()
        {
            if (!(State is DeadMarioState) && Movement != MarioMovement.Jumping)
            {
                Velocity = new Vector2(0, Velocity.Y);
                Movement = MarioMovement.Crouching;
                lastMoved = 0;
                State.Crouch();
            }
        }

        public void Idle()
        {
            Movement = MarioMovement.Idle;
            State.Idle();
        }

        public void Jump()
        {
            if (!(State is DeadMarioState) && Grounded)
            {
                Velocity = new Vector2(Velocity.X, -jumpVelocity);
                //jump initially
                Location = new Vector2(Location.X, Location.Y - 2);
                Movement = MarioMovement.Jumping;
                new SoundEffects().PlayJumpSuper();
            }
            State.Jump();
        }

        public void EnemyJump()
        {
            new SoundEffects().PlayStomp();
            Velocity = new Vector2(Velocity.X, -hopVelocity);
            //jump initially
            Location = new Vector2(Location.X, Location.Y - 2);
            Movement = MarioMovement.Jumping;
            State.Jump();
        }

        public void Move(GameTime gameTime, Boolean movingRight)
        {
            if (!(State is DeadMarioState) && !FlagPoleRiding)
            {
                //Velocity for movement change
                if (facingRight != movingRight && Math.Abs(Velocity.X) > 0)
                {
                    Velocity = facingRight ? new Vector2(Velocity.X - 2, Velocity.Y) : new Vector2(Velocity.X + 2, Velocity.Y);
                }
                facingRight = movingRight;

                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if ((Movement == MarioMovement.Moving || Movement == MarioMovement.Jumping) && Math.Abs(Velocity.X) < maxXVelocity && elapsedTime > movementInterval)
                {
                    Velocity = facingRight ? new Vector2(Velocity.X + 1, Velocity.Y) : new Vector2(Velocity.X - 1, Velocity.Y);
                    elapsedTime = 0;
                }
                if (Movement != MarioMovement.Jumping)
                {
                    Movement = MarioMovement.Moving;
                    lastMoved = 0;
                }

                State.Move();
            }
        }

        public bool Right()
        {
            return facingRight;
        }

        public void ChangeToLittle()
        {
            //if Mario is big/fire adjust the y location to make up for texture size difference
            if (!(State is DeadMarioState) && !(State is LittleMarioState))
            {
                Location = new Vector2(Location.X, Location.Y + textureDifference);
            }
            State = new LittleMarioState();
        }

        public void ChangeToBig()
        {
            if (State is LittleMarioState)
            {
                //if Mario is little adjust the y location to make up for texture size difference
                Location = new Vector2(Location.X, Location.Y - textureDifference);
            }
            State = new BigMarioState();
        }

        public void ChangeToFire()
        {
            //if Mario is little adjust the y location to make up for texture size difference
            if (State is LittleMarioState)
            {
                Location = new Vector2(Location.X, Location.Y - textureDifference);
            }
            State = new FireMarioState();
        }

        public void ChangeToDead()
        {
            new SoundEffects().PlayDead();
            Movement = MarioMovement.Idle;
            //if Mario is big adjust the y location to make up for texture size difference
            if (!(State is DeadMarioState) && !(State is LittleMarioState))
            {
                Location = new Vector2(Location.X, Location.Y + textureDifference);
            }
            Velocity = new Vector2(0,-6);
            State = new DeadMarioState();
        }

        public void Update(GameTime gameTime)
        {
            //Don't move mario if he's too far from the other player
            if (Game1.Instance.GameVariables.CurrentlyAlive > 1)
            {
                if (Game1.Instance.Marios[0] == Parent)
                {
                    if (Math.Abs(Location.X + Velocity.X - Game1.Instance.Marios[1].Location.X) < GameConstants.PlayerSeparation)
                        Location += Velocity;
                    else
                        Location += new Vector2(0, Velocity.Y);
                }
                else
                {
                    if (Math.Abs(Location.X + Velocity.X - Game1.Instance.Marios[0].Location.X) < GameConstants.PlayerSeparation)
                        Location += Velocity;
                    else
                        Location += new Vector2(0, Velocity.Y);
                }
            }
            else
                Location += Velocity;

            if (fireBallLastShot < fireBallInterval)
            {
                fireBallLastShot += gameTime.ElapsedGameTime.Milliseconds;
            }

            lastMoved += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (lastMoved > movementInterval)
            {
                lastMoved = 0;
                ResetXVelocity();
                Idle();
            }

            if (!Grounded)
            {
                lastMoved = 0;
                State.Jump();
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.PlayerGravity);
            }

            //if time runs out, kill mario
            if (!(State is DeadMarioState) && Game1.Instance.GameVariables.LevelTimer == 0 && Game1.Instance.CurrentState != Game1.GameState.End)
            {
                ChangeToDead();
            }

            //If Mario goes off the screen, he should die
            if (!Game1.Instance.GameVariables.Underground && Location.Y > Game1.Instance.GameVariables.ScreenHeight + 25 && !(State is DeadMarioState))
            {
                ChangeToDead();
            } //He is also not allowed to go past the start of the game
            else if (Location.X < Game1.Instance.Level.BeginningOfLevel)
            {
                Location = new Vector2(Game1.Instance.Level.BeginningOfLevel, Location.Y);
            } //And if he hits the end, finish the game!
            else if (Location.X >= Game1.Instance.Level.EndOfLevel.X)
            {
                FinishLevel();
            }

            Sprite = State.Sprite;
            State.Update(gameTime, facingRight);
        }

        public void Draw(SpriteBatch spriteBatch, int rowAlter)
        {
            State.Draw(spriteBatch, rowAlter, Location);
        }

        public void FinishLevel()
        {
            Game1.Instance.EndLevel();
            //First run
            if(!FlagPoleRiding)
            {
                Location = new Vector2(Game1.Instance.Level.EndOfLevel.X, Location.Y);
                //If Mario is at the top of the pole, give a new life, otherwise give 500 points
                if(Location.Y <= Game1.Instance.Level.EndOfLevel.Y)
                {
                    new OneUp(new Collision(Parent, null, CollisionSide.Default)).Execute();
                }
                else
                {
                    new AquirePoints(Parent, GameConstants.FlagPoints).Execute();
                }
            }
            FlagPoleRiding = true;
            if (!Grounded)
            {
                State.FlagSlide();
                Velocity = new Vector2(0, 2);
            }
            else
            {
                Velocity = new Vector2(2, 0);
                State.Move();
            }
            if (Location.X > Game1.Instance.Level.CastleDoor)
            {
                Idle();
                Velocity = new Vector2(0, 0);
            }
            lastMoved = 0;
        }

        public Boolean CanBreakBlocks()
        {
            return (State is BigMarioState) || (State is FireMarioState);
        }

        public void ActionPressed()
        {
            if ((State is FireMarioState) && fireBallLastShot > fireBallInterval)
            {
                ShootFireball();
            }
            if (Grounded)
            {
                maxXVelocity = runningVelocity;
            }
        }

        public void ShootFireball()
        {
            Vector2 fireballSpawnLocation;
            if (facingRight)
            {
                fireballSpawnLocation = new Vector2(Location.X + Sprite.Texture.Width / Sprite.NumCols, Location.Y + Sprite.Texture.Height / Sprite.NumRows / 2);
            }
            else
            {
                fireballSpawnLocation = new Vector2(Location.X, Location.Y + Sprite.Texture.Height / Sprite.NumRows / 2);
            }
            new SpawnFireBallCommand(fireballSpawnLocation, facingRight, Parent).Execute();
            new SoundEffects().PlayFireball();
            fireBallLastShot = 0;
        }

        public void ResetXVelocity()
        {
            Velocity = new Vector2(0, Velocity.Y);
            maxXVelocity = standardVelocity;
        }

        public Boolean IsJumping()
        {
            return (Movement == MarioMovement.Jumping && Velocity.Y < 0);
        }

        public Boolean IsCrouching()
        {
            return (Movement == MarioMovement.Crouching && Grounded);
        }

        public Rectangle HitBox()
        {
            return Sprite.HitBox(Location);
        }

        public void Land()
        {
            Velocity = new Vector2(Velocity.X, 0);
            Idle();
        }
    }
}
