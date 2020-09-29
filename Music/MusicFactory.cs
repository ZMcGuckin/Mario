using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    class MusicFactory
    {
        private Song overworldMusic;
        private Song undergroundMusic;
        private Song hurryOverworldMusic;
        private Song hurryUnderworldMusic;
        private Song starmanMusic;
        private Song gameOverMusic;
        private Song levelcompleteMusic;
        private Song endingMusic;
        private Song titleMusic;
        private static MusicFactory instance = new MusicFactory();

        public static MusicFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private MusicFactory()
        {
            // no additional code necessary at the moment
        }

        public void LoadAllTextures(ContentManager content)
        {
            overworldMusic = content.Load<Song>("main theme overworld");
            undergroundMusic = content.Load<Song>("underworld");
            hurryOverworldMusic = content.Load<Song>("hurry overworld");
            hurryUnderworldMusic = content.Load<Song>("hurry underground");
            starmanMusic = content.Load<Song>("starman");
            gameOverMusic = content.Load<Song>("game over");
            levelcompleteMusic = content.Load<Song>("level complete");
            endingMusic = content.Load<Song>("ending");
            titleMusic = content.Load<Song>("title");
        }

        public Song CreateOverworldTheme()
        {
            return overworldMusic;
        }

        public Song CreateUnderworldTheme()
        {
            return undergroundMusic;
        }

        public Song CreateHurryOverworldTheme()
        {
            return hurryOverworldMusic;
        }

        public Song CreateHurryUnderworldTheme()
        {
            return hurryUnderworldMusic;
        }

        public Song CreateStarmanTheme()
        {
            return starmanMusic;
        }

        public Song CreateGameOverTheme()
        {
            return gameOverMusic;
        }

        public Song CreateLevelCompleteTheme()
        {
            return levelcompleteMusic;
        }

        public Song CreateEndingTheme()
        {
            return endingMusic;
        }

        public Song CreateTitleTheme()
        {
            return titleMusic;
        }
    }
}
