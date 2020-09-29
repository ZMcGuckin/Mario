using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class LevelLoader
    {
        Vector2 Location;
        Vector2 TeleportLocation;
        List<String> blocks;
        List<String> scenery;
        List<String> enemies;
        List<String> items;
        public String Instances { get; set; }
        public String Rows { get; set; }
        public String Columns { get; set; }
        public String FirstLevelString { get; set; }
        public String SecondLevelString { get; set; }
        public String TitleBackground { get; set; }
        public String LevelString { get; set; }
        public int ExtendPipe { get; set; }
        public float CastleDoor { get; set; }
        public Vector2 EndOfLevel { get; set; }
        public int BeginningOfLevel { get; set; }

        public LevelLoader()
        {
            Initialize();
        }

        public void Initialize()
        {
            blocks = new List<String>();
            blocks.Add("BrickBlock");
            blocks.Add("CoinBrickBlock");
            blocks.Add("StarBrickBlock");
            blocks.Add("FireFlowerBrickBlock");
            blocks.Add("BigMushroomBlock");
            blocks.Add("HealthMushroomBrickBlock");
            blocks.Add("FireFlowerHiddenBlock");
            blocks.Add("BigMushroomHiddenBlock");
            blocks.Add("HealthMushroomHiddenBlock");
            blocks.Add("StarHiddenBlock");
            blocks.Add("CoinHiddenBlock");
            blocks.Add("StarItemBlock");
            blocks.Add("CoinItemBlock");
            blocks.Add("BigMushroomItemBlock");
            blocks.Add("HealthMushroomItemBlock");
            blocks.Add("FireFlowerItemBlock");
            blocks.Add("UndergroundBreakableBlock");
            blocks.Add("HardBlock");
            blocks.Add("OpenedBlock");
            blocks.Add("GroundBlock");
            blocks.Add("UndergroundGroundBlock");
            blocks.Add("RegularPipe");
            blocks.Add("UpsidedownPipe");

            scenery = new List<String>();
            scenery.Add("Cloud1");
            scenery.Add("Cloud2");
            scenery.Add("Cloud3");
            scenery.Add("LittleHill");
            scenery.Add("BigHill");
            scenery.Add("Bush1");
            scenery.Add("Bush2");
            scenery.Add("Bush3");
            scenery.Add("FlagPole");
            scenery.Add("Castle");

            enemies = new List<String>();
            enemies.Add("Koopa");
            enemies.Add("Goomba");
            enemies.Add("HammerBro");
            enemies.Add("Lakitu");
            enemies.Add("Spiny");

            items = new List<string>();
            items.Add("Coin");
            items.Add("FireFlower");
            items.Add("BigMushroom");
            items.Add("HealthMushroom");
            items.Add("Star");

            Rows = "1";
            Columns = "1";
            FirstLevelString = "FirstLevel";
            SecondLevelString = "SecondLevel";
            TitleBackground = "TitleBackground";
            ExtendPipe = 0;
        }

        public void Build(String objectName)
        {
            Dictionary<int, ListPair> staticDictionary = Game1.Instance.GameLists.StaticLocationDictionary;
            int rows = Convert.ToInt32(Rows);
            int cols = Convert.ToInt32(Columns);

            // i = horizontal block number - 1
            for (int i = 0; i < cols; i++)
            {
                // j = vertical block number - 1
                for (int j = 0; j < rows; j++)
                {
                    if (staticDictionary.ContainsKey((int)Location.X))
                    {
                        staticDictionary[(int)Location.X].ObjList.Add(BlockFactory.Instance.CreateBlocks(Location, objectName, new Vector2(0, 0), ExtendPipe));
                    }
                    else
                    {
                        staticDictionary.Add((int)Location.X, new ListPair((int)Location.X, BlockFactory.Instance.CreateBlocks(Location, objectName, new Vector2(0, 0), ExtendPipe)));
                    }
                    Location.Y -= 45;
                }
                Location.Y += 45 * rows;
                if (objectName == "GroundBlock" || objectName == "UndergroundGroundBlock" || objectName == "HardBlock")
                {
                    Location.X += 50;
                }
                else
                {
                    Location.X += 45;
                }
            }
            Rows = "1";
            Columns = "1";
        }

        public void CreateObjects(String objectName)
        {
            if (objectName == "Mario")
            {
                Game1.Instance.Marios.Clear();
                Game1.Instance.Marios.Add(new Mario(Location, 0));
                if (Game1.Instance.GameVariables.PlayerNumber == 2)
                    Game1.Instance.Marios.Add(new Mario(new Vector2(Location.X + 50, Location.Y), 1));

            }
            //Enemies
            else if (enemies.Contains(objectName))
            {
                Game1.Instance.GameLists.DynamicMasterList.Add(EnemyFactory.Instance.CreateEnemies(Location, objectName));
            }
            else if (objectName == "Flag")
            {
                Game1.Instance.GameLists.DynamicMasterList.Add(new Flag(Location));
            }
            //Scenery
            else if (scenery.Contains(objectName))
            {
                Game1.Instance.GameLists.BackgroundElements.Add(UniversalSpriteFactory.Instance.CreateSprite(objectName, Location));
                CastleDoor = objectName == "Castle" ? Location.X + 100 : CastleDoor;
                EndOfLevel = objectName == "FlagPole" ? Location : EndOfLevel;
            }
            else if (blocks.Contains(objectName))
            {
                Build(objectName);
            }
            else if (blocks.Contains(objectName))
            {
                Dictionary<int, ListPair> staticDictionary = Game1.Instance.GameLists.StaticLocationDictionary;
                if (staticDictionary.ContainsKey((int)Location.X))
                {
                    staticDictionary[(int)Location.X].ObjList.Add(BlockFactory.Instance.CreateBlocks(Location, objectName, new Vector2(0, 0), ExtendPipe));
                }
                else
                {
                    staticDictionary.Add((int)Location.X, new ListPair((int)Location.X, BlockFactory.Instance.CreateBlocks(Location, objectName, new Vector2(0, 0), ExtendPipe)));
                }
            }
        }

        public void CreateTeleportingPipes(String objectName)
        {
            if (objectName.Contains("Pipe"))
            {
                Dictionary<int, ListPair> staticDictionary = Game1.Instance.GameLists.StaticLocationDictionary;
                if (staticDictionary.ContainsKey((int)Location.X))
                {
                    staticDictionary[(int)Location.X].ObjList.Add(BlockFactory.Instance.CreateBlocks(Location, objectName, TeleportLocation, ExtendPipe));
                }
                else
                {
                    staticDictionary.Add((int)Location.X, new ListPair((int)Location.X, BlockFactory.Instance.CreateBlocks(Location, objectName, TeleportLocation, ExtendPipe)));
                }
            }
        }

        public void LevelChoice(int level)
        {
            if (level == 1)
            {
                LevelString = FirstLevelString;
            }
            else if (level == 2)
            {
                LevelString = SecondLevelString;
            }
            else
            {
                LevelString = TitleBackground;
            }
        }

        public void LoadLevel(int level)
        {
            LevelChoice(level);

            // Create an XML reader for this file.
            using (XmlReader reader = XmlReader.Create("file:///Users/mcguckin/Projects/MyMario/Levels/" + LevelString + ".xml"))
            {
                String objectName = "";
                String locationValue = "";

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "LevelStartTotalTime":
                                if(reader.Read())
                                {
                                    String[] parse = reader.Value.Trim().Split(' ');
                                    Game1.Instance.Level.BeginningOfLevel = Convert.ToInt32(parse[0]);
                                    Game1.Instance.GameVariables.TotalTime = Convert.ToInt32(parse[1]);
                                }
                                break;
                            case "Build":
                                if (reader.Read())
                                {
                                    string[] build = reader.Value.Trim().Split(new char[0]);
                                    Columns = build[0];
                                    Rows = build[1];
                                }
                                break;
                            case "Extend":
                                if (reader.Read())
                                {
                                    ExtendPipe = Convert.ToInt32(reader.Value.Trim());
                                }
                                break;
                            case "ObjectName":
                                if (reader.Read())
                                {
                                    objectName = reader.Value.Trim();
                                }
                                break;
                            case "Location":
                                if (reader.Read())
                                {
                                    locationValue = reader.Value.Trim();
                                    String[] locations = locationValue.Split(' ');
                                    Location.X = Convert.ToInt32(locations[0]);
                                    Location.Y = Convert.ToInt32(locations[1]);

                                    CreateObjects(objectName);
                                }
                                break;
                            case "TeleportLocation":
                                if (reader.Read())
                                {
                                    locationValue = reader.Value.Trim();
                                    String[] locations = locationValue.Split(' ');
                                    TeleportLocation.X = Convert.ToInt32(locations[0]);
                                    TeleportLocation.Y = Convert.ToInt32(locations[1]);

                                    CreateTeleportingPipes(objectName);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
