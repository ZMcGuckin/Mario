using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TheKoopaTroopas
{
    public class DeadMarioState : IMarioState
    {
        public IMarioSprite Sprite { get; set; }
        public int SequenceOrder => 0;

        public DeadMarioState()
        {
            Sprite = MarioSpriteFactory.Instance.CreateDeadMarioSprite();
        }

        public void Jump()
        {
            //Does nothing
        }
        public void Crouch()
        {
            //Does nothing
        }
        public void Move()
        {
            //Does nothing
        }
        public static Boolean Right()
        {
            return true;
        }
        public static void ChangeDirection()
        {
            //Does nothing
        }
        public void Idle()
        {
            //Does Nothing
        }

        public void FlagSlide()
        {
            //Does Nothing
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
