using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class LittleMarioState : IMarioState
    {
        enum MarioMovement { Jumping, Crouching, Idle, Moving, Slide };
        MarioMovement movement;
        public IMarioSprite Sprite { get; set; }
        public int SequenceOrder => 1;

        public LittleMarioState()
        {
            Sprite = MarioSpriteFactory.Instance.CreateLittleMarioIdleSprite();
        }

        public void Jump()
        {
            movement = MarioMovement.Jumping;
            Sprite = MarioSpriteFactory.Instance.CreateLittleMarioJumpingSprite();
        }

        public void Crouch()
        {
            movement = MarioMovement.Crouching;
            Sprite = MarioSpriteFactory.Instance.CreateLittleMarioIdleSprite();
        }

        public void Move()
        {
            if(movement != MarioMovement.Moving && movement != MarioMovement.Jumping)
            {
                movement = MarioMovement.Moving;
                Sprite = MarioSpriteFactory.Instance.CreateLittleMarioMovingSprite();
            }
        }

        public void Idle()
        {
            movement = MarioMovement.Idle;
            Sprite = MarioSpriteFactory.Instance.CreateLittleMarioIdleSprite();
        }

        public void FlagSlide()
        {
            movement = MarioMovement.Slide;
            Sprite = MarioSpriteFactory.Instance.CreateLittleMarioFlagSprite();
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
