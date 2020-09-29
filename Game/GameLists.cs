using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class ListPair
    {
        public int XLocation { get; set; }
        private readonly ICollection<IGameObject> objList = new List<IGameObject>();
        public ICollection<IGameObject> ObjList { get { return objList; } }
        public ListPair(int loc)
        {
            XLocation = loc;
        }
        public ListPair(int loc, IGameObject obj)
        {
            XLocation = loc;
            objList = new List<IGameObject> { obj };
        }
    }
        public class GameLists
        {
            private readonly ICollection<IGameObject> dynamicMasterList = new List<IGameObject>();
            public ICollection<IGameObject> DynamicMasterList { get { return dynamicMasterList; } }

            private readonly Dictionary<int, ListPair> staticLocationDictionary = new Dictionary<int, ListPair>();
            public Dictionary<int, ListPair> StaticLocationDictionary { get { return staticLocationDictionary; } }

            private readonly ICollection<UniversalSprite> backgroundList = new List<UniversalSprite>();
            public ICollection<UniversalSprite> BackgroundElements { get { return backgroundList; } }
            private readonly ICollection<IGameObject> purgeList = new List<IGameObject>();
            public ICollection<IGameObject> PurgeList { get { return purgeList; } }
            private readonly ICollection<IGameObject> addList = new List<IGameObject>();
            public ICollection<IGameObject> AddList { get { return addList; } }
            private readonly ICollection<IndicatorText> indicatorText = new List<IndicatorText>();
            public ICollection<IndicatorText> IndicatorText { get { return indicatorText; } }

            public GameLists()
            {
                staticLocationDictionary.Clear();
                dynamicMasterList.Clear();
                backgroundList.Clear();
                purgeList.Clear();
                addList.Clear();
            }

            public void Update(GameTime gameTime)
            {
                foreach (IGameObject gameObject in AddList)
                {
                    if (staticLocationDictionary.ContainsKey((int)gameObject.Location.X))
                    {
                        staticLocationDictionary[(int)gameObject.Location.X].ObjList.Add(gameObject);
                    }
                    else
                    {
                        staticLocationDictionary.Add((int)gameObject.Location.X, new ListPair((int)gameObject.Location.X, gameObject));
                    }
                }
                addList.Clear();

                foreach (IGameObject gameObject in PurgeList)
                {
                    if (DynamicMasterList.Contains(gameObject))
                    {
                        DynamicMasterList.Remove(gameObject);
                    }
                    else
                    {
                        staticLocationDictionary[(int)gameObject.Location.X].ObjList.Remove(gameObject);
                    }
                }
                purgeList.Clear();

                foreach (KeyValuePair<int, ListPair> entry in staticLocationDictionary)
                {
                    foreach (IGameObject gameObject in entry.Value.ObjList)
                    {
                        gameObject.Update(gameTime);
                    }
                }

                for (int i = 0; i < DynamicMasterList.Count; i++)
                {
                    DynamicMasterList.ElementAt(i).Update(gameTime);
                }
                foreach (UniversalSprite sprite in BackgroundElements)
                {
                    sprite.Update(gameTime, sprite.Location);
                }
                for (int i = 0; i < IndicatorText.Count; i++)
                {
                    IndicatorText.ElementAt(i).Update(gameTime);
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                foreach (UniversalSprite sprite in BackgroundElements)
                {
                    sprite.Draw(spriteBatch, false);
                }
                foreach (KeyValuePair<int, ListPair> entry in staticLocationDictionary)
                {
                    foreach (IGameObject gameObject in entry.Value.ObjList)
                    {
                        gameObject.Draw(spriteBatch);
                    }
                }
                foreach (IGameObject gameObject in dynamicMasterList)
                {
                    gameObject.Draw(spriteBatch);
                }
                foreach (IndicatorText ptext in IndicatorText)
                {
                    ptext.Draw(spriteBatch, Game1.Instance.SpriteFont);
                }
            }
        }
    
}
