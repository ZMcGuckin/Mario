using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class SoundEffects
    {
        readonly SoundEffectInstance jumpSmall = SoundEffectsFactory.Instance.JumpSmallSFX().CreateInstance();
        readonly SoundEffectInstance jumpSuper = SoundEffectsFactory.Instance.JumpSuperSFX().CreateInstance();
        readonly SoundEffectInstance stomp = SoundEffectsFactory.Instance.StompSFX().CreateInstance();
        readonly SoundEffectInstance fireball = SoundEffectsFactory.Instance.FireballSFX().CreateInstance();
        readonly SoundEffectInstance powerup = SoundEffectsFactory.Instance.PowerupSFX().CreateInstance();
        readonly SoundEffectInstance bump = SoundEffectsFactory.Instance.BumpSFX().CreateInstance();
        readonly SoundEffectInstance breakBlock = SoundEffectsFactory.Instance.BreakBlockSFX().CreateInstance();
        readonly SoundEffectInstance coin = SoundEffectsFactory.Instance.CoinSFX().CreateInstance();
        readonly SoundEffectInstance powerupAppear = SoundEffectsFactory.Instance.PowerupAppearSFX().CreateInstance();
        readonly SoundEffectInstance pipe = SoundEffectsFactory.Instance.PipeSFX().CreateInstance();
        readonly SoundEffectInstance oneup = SoundEffectsFactory.Instance.OneupSFX().CreateInstance();
        readonly SoundEffectInstance flagepole = SoundEffectsFactory.Instance.FlagpoleSlideSFX().CreateInstance();
        readonly SoundEffectInstance pause = SoundEffectsFactory.Instance.PauseSFX().CreateInstance();
        readonly SoundEffectInstance kick = SoundEffectsFactory.Instance.KickSFX().CreateInstance();
        readonly SoundEffectInstance shrink = SoundEffectsFactory.Instance.ShrinkSFX().CreateInstance();
        readonly SoundEffectInstance dead = SoundEffectsFactory.Instance.DeadSFX().CreateInstance();
        readonly SoundEffectInstance menuSelect = SoundEffectsFactory.Instance.MenuSelectSFX().CreateInstance();
        readonly SoundEffectInstance menuChoose = SoundEffectsFactory.Instance.MenuChooseSFX().CreateInstance();
        readonly SoundEffectInstance powerdown = SoundEffectsFactory.Instance.PowerDownSFX().CreateInstance();


        public SoundEffects()
        {
        }

        public void PlayJumpSmall()
        {
            jumpSmall.Pitch -= 0.5f;
            jumpSmall.Play();
        }

        public void PlayDead()
        {
            dead.Play();
        }

        public void PlayJumpSuper()
        {
            jumpSuper.Play();
        }

        public void PlayStomp()
        {
            stomp.Play();
        }

        public void PlayFireball()
        {
            fireball.Play();
        }

        public void PlayPowerup()
        {
            powerup.Play();
        }

        public void PlayBump()
        {
            bump.Play();
        }

        public void PlayBreakBlock()
        {
            breakBlock.Play();
        }

        public void PlayCoin()
        {
            coin.Volume -= 0.5f;
            coin.Play();
        }

        public void PlayPowerupAppears()
        {
            powerupAppear.Play();
        }

        public void PlayPipe()
        {
            pipe.Play();
        }

        public void PlayOneup()
        {
            oneup.Play();
        }

        public void PlayFlagpoleSlide()
        {
            flagepole.Play();
        }

        public void PlayPause()
        {
            pause.Play();
        }

        public void PlayKick()
        {
            kick.Play();
        }

        public void PlayShrink()
        {
            shrink.Play();
        }

        public void PlayMenuSelect()
        {
            menuSelect.Play();
        }

        public void PlayMenuChoose()
        {
            menuChoose.Play();
        }

        public void PlayPowerDown()
        {
            powerdown.Play();
        }
    }
}
