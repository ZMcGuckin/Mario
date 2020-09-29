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
    public class OpenedBlock : AbstractBlock
    {
        event OnBumpHandler OnBump;

        public OpenedBlock(Vector2 location)
        {
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("OpenedBlock", location);
            IsBumped = true;
            OnBump += OpenedBlock_OnBump;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime,Location);
            OnBump?.Invoke(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch,false);
        }

        public override string SpecificCollisionType => "OpenedBlock";

        public override void Bump(IMario Mario)
        {
            //Opened blocks do not bump
        }

        private void OpenedBlock_OnBump(GameTime gameTime)
        {
            if (ElapsedTime < interval)
            {
                Location = new Vector2(Location.X, Location.Y + blockGravity);
                Sprite.Location = Location;
            }
            else
            {
                IsBumped = false;
                OnBump -= OpenedBlock_OnBump;
            }
            ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public static void SpawnItem(IMario Bumper, Items item, IGameObject block)
        {
            switch (item)
            {
                case Items.Coin:
                    new SpawnCoinCommand(((IBlock)block).Location).Execute();
                    Game1.Instance.GameVariables.CollectedCoins += 1;
                    (new AquirePoints(Bumper, 200)).Execute();
                    break;
                case Items.BigMushroom:
                    new SpawnBigMushroomCommand(((IBlock)block).Location).Execute();
                    break;
                case Items.FireFlower:
                    new SpawnFireFlowerCommand(((IBlock)block).Location).Execute();
                    break;
                case Items.HealthMushroom:
                    new SpawnHealthMushroomCommand(((IBlock)block).Location).Execute();
                    break;
                case Items.Star:
                    new SpawnStarCommand(((IBlock)block).Location).Execute();
                    break;
            }
        }
    }
}
