using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TheKoopaTroopas
{
    public enum CollisionSide { Left, Right, Top, Bottom, Default }

    public static class CollisionDetector
    {
        public static void Update()
        {
            //all moving objects get added into the dynamic objects list
            List<IGameObject> dynamicMasterList = Game1.Instance.GameLists.DynamicMasterList.ToList();
            Dictionary<int, ListPair> staticLocDictionary = Game1.Instance.GameLists.StaticLocationDictionary;
            // List<IGameObject> staticMasterList = Game1.Instance.GameLists.StaticMasterList.ToList();
            //puts mario at the front of dynamic masterlist because for collision he acts the same as any other Dynamic Object
            dynamicMasterList.AddRange(Game1.Instance.Marios);
            dynamicMasterList.Reverse();

            foreach (IGameObject dynamicObj in dynamicMasterList)
            {
                dynamicObj.Grounded = false;
            }

            //for each dynamic object check through all other game object for a collision 
            //If a collision exists call the CollisionHandler with a collision Instance
            Collision detected;
            // Each dynamic object only checks 1/4 screen width around them for collisions 1/8 on each side
            foreach(IGameObject dynObj in dynamicMasterList)
            {
                Tuple<int, int> range = new Tuple<int, int>((int)(dynObj.Location.X - Game1.Instance.GameVariables.ScreenWidth / 8), (int)(dynObj.Location.X + Game1.Instance.GameVariables.ScreenWidth / 8));
                for(int k = range.Item1; k < range.Item2; k++)
                {
                    if (staticLocDictionary.ContainsKey(k))
                    {
                        foreach (IGameObject staticObj in staticLocDictionary[k].ObjList)
                        {
                            detected = DetectCollision(dynObj, staticObj);
                            if (detected.ObjectCollidedWith != null)
                            {
                                CollisionHandler.Instance.Collide(detected);
                            }
                        }
                    }
                }
            }
            //Copies the dynamic range, does take n time but the benefit outweighs the cost
            List<IGameObject> dynamicObjectsNotChecked = new List<IGameObject>();
            dynamicObjectsNotChecked.AddRange(dynamicMasterList);

            //The first loop removes the object being checked from the not checked list so that it will not check itself
            //By checking the collision this way each iteration through the outer for will do less work as the dynamicObjectsNot Checked shrinks
            //so the first dynamic object checks collision with n-1 other objects and the next n-2
            //this is so that collisions are handled only once for each collision which cuts out more than n collision checks
            int i;
            for (i = 0; i < dynamicMasterList.Count; i++)
            {
                IGameObject dynObj = dynamicMasterList.ElementAt(i);
                dynamicObjectsNotChecked.Remove(dynObj);
                int j;
                for(j = 0; j < dynamicObjectsNotChecked.Count; j++)
                {
                    detected = DetectCollision(dynObj, dynamicObjectsNotChecked.ElementAt(j));
                    if (detected.ObjectCollidedWith != null)
                    {
                        CollisionHandler.Instance.Collide(detected);
                    }
                }
            }
            foreach(IMario mario in Game1.Instance.Marios)
                dynamicMasterList.Remove(mario);
        }

        private static Collision DetectCollision(IGameObject dynamObj, IGameObject staticObj)
        {
            Rectangle dynamicHitbox = dynamObj.LocationRect;
            Rectangle intersection = Rectangle.Intersect(staticObj.LocationRect, dynamicHitbox);
            if (intersection != Rectangle.Empty)
            {
                return new Collision(dynamObj, staticObj, ChooseSide(staticObj.LocationRect, intersection));
            }
            return new Collision();
        }

        private static CollisionSide ChooseSide(Rectangle obj, Rectangle intersection)
        {
            
            if (obj.Y == intersection.Y && intersection.Width > intersection.Height)
            {
                return CollisionSide.Top;
            }
            if (obj.Y + obj.Height == intersection.Y + intersection.Height && intersection.Width > intersection.Height)
            {
                return CollisionSide.Bottom;
            }
            if (obj.X + obj.Width == intersection.X + intersection.Width && intersection.Width < intersection.Height)
            {
                return CollisionSide.Right;
            }
            if (obj.X == intersection.X && intersection.Width < intersection.Height)
            {
                return CollisionSide.Left;
            }
            return CollisionSide.Default;
        }
    }
}
