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
    public class MarioSpriteFactory
    {
        Texture2D littleMarioSpritesheet;
        Texture2D bigMarioSpritesheet;

        private static MarioSpriteFactory instance = new MarioSpriteFactory();

        public static MarioSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private MarioSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            littleMarioSpritesheet = content.Load<Texture2D>("LittleMario");
            bigMarioSpritesheet = content.Load<Texture2D>("BigMario");
        }

        public IMarioSprite CreateLittleMarioIdleSprite()
        {
            return new LittleMarioIdleSprite(littleMarioSpritesheet);
        }

        public IMarioSprite CreateLittleMarioJumpingSprite()
        {
            return new LittleMarioJumpingSprite(littleMarioSpritesheet);
        }

        public IMarioSprite CreateLittleMarioMovingSprite()
        {
            return new LittleMarioMovingSprite(littleMarioSpritesheet);
        }

        public IMarioSprite CreateLittleMarioFlagSprite()
        {
            return new LittleMarioFlagSprite(littleMarioSpritesheet);
        }

        public IMarioSprite CreateBigMarioIdleSprite()
        {
            return new BigMarioIdleSprite(bigMarioSpritesheet);
        }

        public IMarioSprite CreateBigMarioJumpingSprite()
        {
            return new BigMarioJumpingSprite(bigMarioSpritesheet);
        }

        public IMarioSprite CreateBigMarioMovingSprite()
        {
            return new BigMarioMovingSprite(bigMarioSpritesheet);
        }

        public IMarioSprite CreateBigMarioCrouchingSprite()
        {
            return new BigMarioCrouchingSprite(bigMarioSpritesheet);
        }

        public IMarioSprite CreateBigMarioFlagSprite()
        {
            return new BigMarioFlagSprite(bigMarioSpritesheet);
        }

        public IMarioSprite CreateDeadMarioSprite()
        {
            return new DeadMarioSprite(littleMarioSpritesheet);
        }
    }
}