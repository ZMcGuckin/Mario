using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
namespace TheKoopaTroopas
{
    public class UniversalSpriteFactory
    {
        //SpriteSheetName,Texture,Rows,Cols
        Dictionary<String, Tuple<Texture2D, int, int, int>> SpriteSheetInfo;
        Dictionary<String,Tuple<String, int[], int, int>> SpriteFrameData;

        private static UniversalSpriteFactory instance = new UniversalSpriteFactory();

        public static UniversalSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private UniversalSpriteFactory()
        {
            SpriteSheetInfo = new Dictionary<string, Tuple<Texture2D, int, int, int>>();
            SpriteFrameData = new Dictionary<String, Tuple<String, int[], int,int>>();
        }
        public UniversalSprite CreateSprite(String FrameName,Vector2 location)
        {
            Tuple<String, int[], int, int> frameInfo = SpriteFrameData[FrameName];
            Tuple<Texture2D, int, int, int> sheetInfo = SpriteSheetInfo[frameInfo.Item1];
            return new UniversalSprite(sheetInfo.Item1, sheetInfo.Item2, sheetInfo.Item3, frameInfo.Item2,location,frameInfo.Item3,frameInfo.Item4, sheetInfo.Item4);
        }
        public void LoadAllTextures(ContentManager content)
        {
            String key = "";
            Texture2D texture = null;
            int rows = 0;
            int cols = 0;
            int scaleFactor;
            using (XmlReader reader = XmlReader.Create("file:///Users/mcguckin/Projects/MyMario/UniversalSprite/SpriteSheetInfo.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch(reader.Name)
                        {
                            case "Name":
                                reader.Read();
                                key = reader.Value.Trim();
                                texture = content.Load<Texture2D>(key);
                                break;
                            case "Rows":
                                reader.Read();
                                rows = Convert.ToInt32(reader.Value.Trim());
                                break;
                            case "Columns":
                                reader.Read();
                                cols = Convert.ToInt32(reader.Value.Trim());
                                break;
                            case "ScaleFactor":
                                reader.Read();
                                scaleFactor = Convert.ToInt32(reader.Value.Trim());
                                Tuple<Texture2D, int, int,  int> tempTuple = new Tuple<Texture2D, int, int, int>(texture, rows, cols, scaleFactor);
                                SpriteSheetInfo.Add(key,tempTuple);
                                break;
                        }
                    }
                }
            }
            LoadFrameData();
        }
        private void LoadFrameData()
        {
            String key = "";
            String frames = "";
            String textureName = "";
            int[] frameList = new int[1];
            int hitBoxAlterX = 0;
            int hitBoxAlterY = 0;
            using (XmlReader reader = XmlReader.Create("file:///Users/mcguckin/Projects/MyMario/UniversalSprite/SpriteFrameData.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "TextureName":
                                reader.Read();
                                textureName = reader.Value.Trim();
                                break;
                            case "FrameName":
                                reader.Read();
                                key = reader.Value.Trim();   
                                break;
                            case "FrameList":
                                reader.Read();
                                frames = reader.Value.Trim();
                                String[] frameArray = frames.Split(new char[] { ' ' });
                                frameList = new int[frameArray.Length];
                                for(int i = 0;i < frameArray.Length; i++)
                                {
                                    frameList[i] = Convert.ToInt32(frameArray[i]);
                                }
                                break;
                            case "HitBoxAlterX":
                                reader.Read();
                                hitBoxAlterX = Convert.ToInt32(reader.Value.Trim());
                                break;
                            case "HitBoxAlterY":
                                reader.Read();
                                hitBoxAlterY = Convert.ToInt32(reader.Value.Trim());
                                SpriteFrameData.Add(key, new Tuple<String, int[], int, int>(textureName, frameList, hitBoxAlterX, hitBoxAlterY));
                                break;
                        }
                    }
                }
            }
        }
    }
}
