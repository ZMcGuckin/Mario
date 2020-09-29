using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TheKoopaTroopas
{
    public interface IMario : IGameObject
    {
        void TakeDamage();
        void Jump();
        void EnemyJump();
        void Crouch();
        void Move(GameTime gameTime, Boolean movingRight);
        void Idle();
        Boolean Right();
        void ChangeToLittle();
        void ChangeToBig();
        void ChangeToFire();
        void ChangeToDead();

        Boolean CanBreakBlocks();
        Boolean IsJumping();
        Boolean IsCrouching();
        Boolean IsLittleMario();
        void ActionPressed();
        void Land();
        int GetXVelocity { get; }
        int GetYVelocity { get; }
        IMarioState State { get; }
        int ColorAlter { get; set; }
        int EnemyMultiplier { get; set; }
    }
}
