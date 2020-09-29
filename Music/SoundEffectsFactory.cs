using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    class SoundEffectsFactory
    {
        private SoundEffect jumpSmall;
        private SoundEffect jumpSuper;
        private SoundEffect stomp;
        private SoundEffect fireball;
        private SoundEffect powerup;
        private SoundEffect bumpBlock;
        private SoundEffect breakBlock;
        private SoundEffect coin;
        private SoundEffect powerupAppear;
        private SoundEffect pipe;
        private SoundEffect oneup;
        private SoundEffect flagpole;
        private SoundEffect pause;
        private SoundEffect kick;
        private SoundEffect shrink;
        private SoundEffect dead;
        private SoundEffect menuSelect;
        private SoundEffect menuChoose;
        private SoundEffect powerDown;
        private static SoundEffectsFactory instance = new SoundEffectsFactory();

        public static SoundEffectsFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SoundEffectsFactory()
        {
            // no additional code necessary at the moment
        }

        public void LoadAllTextures(ContentManager content)
        {
            jumpSmall = content.Load<SoundEffect>("smb jump small");
            jumpSuper = content.Load<SoundEffect>("smb jump super");
            stomp = content.Load<SoundEffect>("smb stomp");
            fireball = content.Load<SoundEffect>("smb fireball");
            powerup = content.Load<SoundEffect>("smb powerup");
            bumpBlock = content.Load<SoundEffect>("smb bump");
            breakBlock = content.Load<SoundEffect>("smb3 breakbrickblock");
            coin = content.Load<SoundEffect>("smb2 coin");
            powerupAppear = content.Load<SoundEffect>("smb powerup appears");
            pipe = content.Load<SoundEffect>("smb pipe");
            oneup = content.Load<SoundEffect>("smb 1-up");
            flagpole = content.Load<SoundEffect>("nsmbFlagPole");
            pause = content.Load<SoundEffect>("smb pause");
            kick = content.Load<SoundEffect>("smb3 kick");
            shrink = content.Load<SoundEffect>("smb2 shrink");
            dead = content.Load<SoundEffect>("dead");
            menuSelect = content.Load<SoundEffect>("nsmbwiiMenuSelect");
            menuChoose = content.Load<SoundEffect>("nsmbwiiMenuChoose");
            powerDown = content.Load<SoundEffect>("powerdown");
        }

        public SoundEffect JumpSmallSFX()
        {
            return jumpSmall;
        }

        public SoundEffect JumpSuperSFX()
        {
            return jumpSuper;
        }

        public SoundEffect DeadSFX()
        {
            return dead;
        }

        public SoundEffect StompSFX()
        {
            return stomp;
        }

        public SoundEffect FireballSFX()
        {
            return fireball;
        }

        public SoundEffect PowerupSFX()
        {
            return powerup;
        }

        public SoundEffect BumpSFX()
        {
            return bumpBlock;
        }

        public SoundEffect BreakBlockSFX()
        {
            return breakBlock;
        }

        public SoundEffect CoinSFX()
        {
            return coin;
        }

        public SoundEffect PowerupAppearSFX()
        {
            return powerupAppear;
        }

        public SoundEffect PipeSFX()
        {
            return pipe;
        }

        public SoundEffect OneupSFX()
        {
            return oneup;
        }

        public SoundEffect FlagpoleSlideSFX()
        {
            return flagpole;
        }

        public SoundEffect PauseSFX()
        {
            return pause;
        }

        public SoundEffect KickSFX()
        {
            return kick;
        }

        public SoundEffect ShrinkSFX()
        {
            return shrink;
        }

        public SoundEffect MenuSelectSFX()
        {
            return menuSelect;
        }

        public SoundEffect MenuChooseSFX()
        {
            return menuChoose;
        }

        public SoundEffect PowerDownSFX()
        {
            return powerDown;
        }
    }
}
