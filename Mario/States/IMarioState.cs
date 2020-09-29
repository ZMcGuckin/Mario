using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TheKoopaTroopas
{
    public interface IMarioState
    {
        void Jump();
        void Crouch();
        void Move();
        void Idle();
        void FlagSlide();
        void Update(GameTime gameTime, Boolean facingRight);
        void Draw(SpriteBatch spriteBatch, int rowAlter, Vector2 location);
        IMarioSprite Sprite { get; set; }
        int SequenceOrder { get; }
    }
}
