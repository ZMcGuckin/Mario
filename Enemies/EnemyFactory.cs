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
    public class EnemyFactory
    {
        delegate IEnemy CreateEnemy(Vector2 location);
        Dictionary<String, CreateEnemy> enemyDictionary;


        private static EnemyFactory instance = new EnemyFactory();

        public static EnemyFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemyFactory()
        {
            enemyDictionary = new Dictionary<string, CreateEnemy>();
            enemyDictionary.Add("Koopa", CreateKoopa);
            enemyDictionary.Add("Goomba", CreateGoomba);
            enemyDictionary.Add("HammerBro", CreateHammerBro);
            enemyDictionary.Add("Lakitu", CreateLakitu);
            enemyDictionary.Add("Spiny", CreateSpiny);
        }

        public static IEnemy CreateGoomba(Vector2 location)
        {
            return new Goomba(location);
        }

        public static IEnemy CreateKoopa(Vector2 location)
        {
            return new Koopa(location);
        }

        public static IEnemy CreateHammerBro(Vector2 location)
        {
            return new HammerBro(location);
        }
        public static IEnemy CreateLakitu(Vector2 location)
        {
            return new Lakitu(location);
        }
        public static IEnemy CreateSpiny(Vector2 location)
        {
            return new Spiny(location);
        }

        public IEnemy CreateEnemies(Vector2 location, String objectName)
        { 
            return enemyDictionary[objectName](location);
        }
    }
}
