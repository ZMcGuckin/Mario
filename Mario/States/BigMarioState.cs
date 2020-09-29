using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class BigMarioState : IMarioState
    {
        enum MarioMovement { Jumping, Crouching, Idle, Moving, Slide };
        MarioMovement movement;
        public int SequenceOrder => 2;
        public IMarioSprite Sprite { get; set; }

        public BigMarioState()
        {
            movement = MarioMovement.Idle;
            Sprite = MarioSpriteFactory.Instance.CreateBigMarioIdleSprite();
        }

        public void Jump()
        {
            Sprite = MarioSpriteFactory.Instance.CreateBigMarioJumpingSprite();
            movement = MarioMovement.Jumping;
        }

        public void Crouch()
        {
            Sprite = MarioSpriteFactory.Instance.CreateBigMarioCrouchingSprite();
            movement = MarioMovement.Crouching;
        }

        public void Move()
        {
            if (movement != MarioMovement.Moving && movement != MarioMovement.Jumping)
            {
                Sprite = MarioSpriteFactory.Instance.CreateBigMarioMovingSprite();
                movement = MarioMovement.Moving;
            }
        }

        public void Idle()
        {
            movement = MarioMovement.Idle;
            Sprite = MarioSpriteFactory.Instance.CreateBigMarioIdleSprite();
        }

        public void FlagSlide()
        {
            movement = MarioMovement.Slide;
            Sprite = MarioSpriteFactory.Instance.CreateBigMarioFlagSprite();
        }

        public void Update(GameTime gameTime, Boolean facingRight)
        {
            Sprite.Update(gameTime, facingRight);
        }

        public void Draw(SpriteBatch spriteBatch, int rowAlter, Vector2 location)
        {
            Sprite.Draw(spriteBatch, rowAlter, location);
        }
    }
}
