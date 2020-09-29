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
    public class UndergroundBreakableBlock : AbstractBlock
    {
        private new const int interval = 82;

        public UndergroundBreakableBlock(Vector2 location, Items item)
        {
            Item = item;
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("UndergroundBreakableBlock", location);
            IsBumped = false;
            DeleteBlock = false;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime,Location);
            if (IsBumped)
            {
                if (ElapsedTime < interval)
                {
                    Location = new Vector2(Location.X, Location.Y + blockGravity);
                    Sprite.Location = Location;
                }
                else
                {
                    IsBumped = false;
                }
                if (DeleteBlock)
                {
                    new DestroyBlockCommand(this).Execute();
                }
                ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch,false);
        }

        public override string SpecificCollisionType => "BreakableBlock";

        public override void Bump(IMario Mario)
        {
            ElapsedTime = 0;
            IsBumped = true;
            Bumper = Mario;
            if (!Mario.CanBreakBlocks() && Item == Items.Default)
            {
                Location = new Vector2(Location.X, Location.Y - bumpVelocity);
                Sprite.Location = Location;
                new SoundEffects().PlayBump();
            }
            else if (Mario.CanBreakBlocks() && Item == Items.Default)
            {
                Location = new Vector2(Location.X, Location.Y - bumpVelocity);
                Sprite.Location = Location;
                DeleteBlock = true;
                new SoundEffects().PlayBreakBlock();
            }
            else
            {
                Location = new Vector2(Location.X, Location.Y - bumpVelocity);
                Sprite.Location = Location;
                OpenedBlock.SpawnItem(Bumper, Item, this);
                new OpenBlockCommand(this).Execute();
            }
        }
    }
}
