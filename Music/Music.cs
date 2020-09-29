using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class Music
    {
        readonly Song underground = MusicFactory.Instance.CreateUnderworldTheme();
        readonly Song overworld = MusicFactory.Instance.CreateOverworldTheme();
        readonly Song hurryOverworld = MusicFactory.Instance.CreateHurryOverworldTheme();
        readonly Song hurryUnderworld = MusicFactory.Instance.CreateHurryUnderworldTheme();
        readonly Song starman = MusicFactory.Instance.CreateStarmanTheme();
        readonly Song gameOver = MusicFactory.Instance.CreateGameOverTheme();
        readonly Song levelComplete = MusicFactory.Instance.CreateLevelCompleteTheme();
        readonly Song ending = MusicFactory.Instance.CreateEndingTheme();
        readonly Song title = MusicFactory.Instance.CreateTitleTheme();
        Song currentlyPlaying;
        Song nextToPlay;
        readonly int hurryMusic = 50;

        public void Update()
        {
            if (Game1.Instance.CurrentState == Game1.GameState.Transition)
            {
                MediaPlayer.Pause();
                currentlyPlaying = null;
            }
            else if(Game1.Instance.CurrentState == Game1.GameState.MainMenu)
            {
                nextToPlay = title;
                MediaPlayer.IsRepeating = true;
            }
            else if (Game1.Instance.CurrentState == Game1.GameState.End)
            {
                nextToPlay = levelComplete;
                MediaPlayer.IsRepeating = false;
            }
            else if (Game1.Instance.CurrentState == Game1.GameState.GameOver)
            {
                nextToPlay = gameOver;
            }
            else if (Game1.Instance.CurrentState == Game1.GameState.GameComplete)
            {
                nextToPlay = ending;
            }
            if (Game1.Instance.CurrentState == Game1.GameState.Playing)
            {
                if (Game1.Instance.Marios.Count > 0 && (Game1.Instance.Marios[0] is StarMario || (Game1.Instance.Marios.Count() == 2 && Game1.Instance.Marios[1] is StarMario)))
                {
                    nextToPlay = starman;
                }
                else if (Game1.Instance.GameVariables.Underground && hurryMusic < Game1.Instance.GameVariables.LevelTimer)
                {
                    nextToPlay = underground;
                }
                else if (Game1.Instance.GameVariables.Underground && hurryMusic >= Game1.Instance.GameVariables.LevelTimer)
                {
                    nextToPlay = hurryUnderworld;
                }
                else if (!Game1.Instance.GameVariables.Underground && hurryMusic < Game1.Instance.GameVariables.LevelTimer)
                {
                    nextToPlay = overworld;
                }
                else if (!Game1.Instance.GameVariables.Underground && hurryMusic >= Game1.Instance.GameVariables.LevelTimer)
                {
                    nextToPlay = hurryOverworld;
                }
            }
            if(nextToPlay != currentlyPlaying && nextToPlay != null)
            {
                MediaPlayer.Resume();
                currentlyPlaying = nextToPlay;
                MediaPlayer.Play(currentlyPlaying);
            }
        }
    }
}
