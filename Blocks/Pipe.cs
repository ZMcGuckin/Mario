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
    public class Pipe : AbstractBlock
    {
        private const int scalingFactor1 = 47;
        private const int scalingFactor2 = 31;
        List<UniversalSprite> pipeSprites;
        string direction;
        Boolean teleporter;
        public Vector2 TeleportLocation { get; set; }
        public Pipe(Vector2 location, int extention, string direction, Boolean teleporter, Vector2 teleportLocation)
        {
            Location = location;
            this.direction = direction;
            this.teleporter = teleporter;
            TeleportLocation = teleportLocation;
            
            pipeSprites = new List<UniversalSprite>();
            pipeSprites.Add(UniversalSpriteFactory.Instance.CreateSprite(direction, location));

            for (int i = 1; i <= extention; i++) {
                pipeSprites.Add(UniversalSpriteFactory.Instance.CreateSprite("Extention", new Vector2(location.X,location.Y+i*(pipeSprites.ElementAt(0).HitBox.Height/2-1))));
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (UniversalSprite uSprite in pipeSprites)
            {
                uSprite.Update(gameTime, uSprite.Location);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
          foreach(UniversalSprite uSprite in pipeSprites)
            {
                uSprite.Draw(spriteBatch, false);
            }
        }

        public override Rectangle LocationRect
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y,pipeSprites.ElementAt(0).HitBox.Width,(int)(pipeSprites.ElementAt(pipeSprites.Count-1).HitBox.Y + pipeSprites.ElementAt(pipeSprites.Count - 1).HitBox.Height - Location.Y));
            }
        }
        public override string SpecificCollisionType
        {
            get
            {
                switch(direction){
                    case("left"):
                        return "SidewaysPipe";
                    case("down"):
                        return "UpsidedownPipe";
                    case ("up"):
                        return teleporter ? "TeleportPipe" : "RegularPipe";
                    default:
                        return "RegularPipe";
                }
            }
        }

        public override void Bump(IMario Mario)
        {
            //Pipes do not bump
        }
    }
}
