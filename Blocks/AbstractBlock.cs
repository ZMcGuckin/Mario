using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheKoopaTroopas
{
    public enum Items { Coin, BigMushroom, FireFlower, HealthMushroom, Star, Default };
    public abstract class AbstractBlock : IBlock
    {
        protected UniversalSprite Sprite { get; set; }
        public Boolean IsBumped { get; set; }
        public Boolean DeleteBlock { get; set; }
        public IMario Bumper { get; set; }
        public Items Item { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Boolean Grounded { get; set; }
        public double ElapsedTime { get; set; }
        protected delegate void OnBumpHandler(GameTime gameTime);
        public const int interval = 75;
        public const int bumpVelocity = 5;
        protected const int blockGravity = 1;
        public virtual Rectangle LocationRect
        {
            get
            {
                return Sprite.HitBox;
            }
        }
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public virtual string CollisionType => "IBlock";
        public abstract string SpecificCollisionType { get; }
        public abstract void Bump(IMario Mario);
    }
}
