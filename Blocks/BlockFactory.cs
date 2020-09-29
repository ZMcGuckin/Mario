using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TheKoopaTroopas
{
    public class BlockFactory
    {
        delegate IBlock CreateStaticBlock(Vector2 Location);
        delegate IBlock CreateDynamicBlock(Vector2 Location, Items item);
        Dictionary <String, CreateStaticBlock> staticBlockDictionary;
        Dictionary<String, CreateDynamicBlock> dynamicBlockDictionary;

        private static BlockFactory instance = new BlockFactory();

        public static BlockFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private BlockFactory()
        {
            staticBlockDictionary = new Dictionary<string, CreateStaticBlock>();
            dynamicBlockDictionary = new Dictionary<string, CreateDynamicBlock>();
            staticBlockDictionary.Add("HardBlock", CreateHardBlock);
            staticBlockDictionary.Add("OpenedBlock", CreateOpenedBlock);
            staticBlockDictionary.Add("GroundBlock", CreateGroundBlock);
            staticBlockDictionary.Add("UndergroundGroundBlock", CreateUndergroundGroundBlock);
            dynamicBlockDictionary.Add("ItemBlock", CreateItemBlock);
            dynamicBlockDictionary.Add("BrickBlock", CreateBreakableBlock);
            dynamicBlockDictionary.Add("UndergroundBreakableBlock", CreateUndergroundBreakableBlock);
            dynamicBlockDictionary.Add("HiddenBlock", CreateHiddenBlock);
        }

        public static IBlock CreateHardBlock(Vector2 location)
        {
            return new HardBlock(location);
        }

        public static IBlock CreateItemBlock(Vector2 location, Items item)
        {
            return new ItemBlock(location, item);
        }

        public static IBlock CreateOpenedBlock(Vector2 location)
        {
            return new OpenedBlock(location);
        }

        public static IBlock CreateBreakableBlock(Vector2 location, Items item)
        {
            return new BreakableBlock(location, item);
        }

        public static IBlock CreateGroundBlock(Vector2 location)
        {
            return new GroundBlock(location);
        }

        public static IBlock CreateUndergroundBreakableBlock(Vector2 location, Items item)
        {
            return new UndergroundBreakableBlock(location, item);
        }

        public static IBlock CreateUndergroundGroundBlock(Vector2 location)
        {
            return new UndergroundGroundBlock(location);
        }

        public static IBlock CreateHiddenBlock(Vector2 location, Items item)
        {
            return new HiddenBlock(location, item);
        }

        public static IBlock CreatePipe(Vector2 location, int pipeExtention, string direction, Boolean teleporter, Vector2 teleportLocation)
        {
            return new Pipe(location, pipeExtention, direction, teleporter, teleportLocation);
        }

        public static Items IdentifyItem(String objectName)
        {
            if (objectName.Contains("Coin"))
            {
                return Items.Coin;
            }
            else if (objectName.Contains("BigMushroom"))
            {
                return Items.BigMushroom;
            }
            else if (objectName.Contains("FireFlower"))
            {
                return Items.FireFlower;
            }
            else if(objectName.Contains("HealthMushroom"))
            {
                return Items.HealthMushroom;
            }
            else if (objectName.Contains("Star"))
            {
                return Items.Star;
            }
            else
            {
                return Items.Default;
            }
        }

        public IBlock CreateBlocks(Vector2 location, String objectName, Vector2 teleportLocation, int extendPipe)
        {
            if (staticBlockDictionary.ContainsKey(objectName))
            {
                return staticBlockDictionary[objectName](location);
            }
            else
            {
                foreach (String blockName in dynamicBlockDictionary.Keys)
                {
                    if (objectName.Contains(blockName))
                    {
                        return dynamicBlockDictionary[blockName](location, IdentifyItem(objectName));
                    }
                }
            }
            if(objectName == "RegularPipe")
            {
                return BlockFactory.CreatePipe(location, extendPipe, "up", false, new Vector2(0, 0));
            }
            else if (objectName == "UpsidedownPipe")
            {
                return BlockFactory.CreatePipe(location, extendPipe, "down", false, new Vector2(0, 0));
            }
            else if(objectName == "TeleportPipe")
            {
                return BlockFactory.CreatePipe(location, extendPipe, "up", true, teleportLocation);
            }
            else
            {
                return BlockFactory.CreatePipe(location, extendPipe, "left", true, teleportLocation);
            }
        }
    }
}

