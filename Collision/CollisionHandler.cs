using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class CollisionHandler
    {
        public static CollisionHandler Instance { get; } = new CollisionHandler();

        Dictionary<Tuple<String, String>, Tuple<ConstructorInfo, ConstructorInfo>> AllSideOverride;
        Dictionary<Tuple<String, String>, Tuple<ConstructorInfo, ConstructorInfo>> CollisionMap;
        Dictionary<Tuple<String, String, CollisionSide>, Tuple<ConstructorInfo, ConstructorInfo>> SideSpecificCollisionMap;
        private CollisionHandler()
        {
            AllSideOverride = new Dictionary<Tuple<String, String>, Tuple<ConstructorInfo, ConstructorInfo>>();
            CollisionMap = new Dictionary<Tuple<String,String>, Tuple<ConstructorInfo,ConstructorInfo>>();
            SideSpecificCollisionMap = new Dictionary<Tuple<String, String, CollisionSide>, Tuple<ConstructorInfo, ConstructorInfo>>();
            RegisterAllCommands();
        }

        public void RegisterOverrideCommand(String objType1, String objType2, ConstructorInfo commandOnObj1, ConstructorInfo commandOnObj2)
        {
            Tuple<String, String> collisionTuple = new Tuple<string, string>(objType1, objType2);
            Tuple<ConstructorInfo, ConstructorInfo> commandTuple = new Tuple<ConstructorInfo, ConstructorInfo>(commandOnObj1, commandOnObj2);
            AllSideOverride.Add(collisionTuple, commandTuple);
        }

        public void RegisterCommand(String objType1, String objType2, ConstructorInfo commandOnObj1,ConstructorInfo commandOnObj2)
        {
            Tuple<String, String> collisionTuple = new Tuple<string, string>(objType1, objType2);
            Tuple<ConstructorInfo,ConstructorInfo> commandTuple = new Tuple<ConstructorInfo,ConstructorInfo>(commandOnObj1, commandOnObj2);
            CollisionMap.Add(collisionTuple,commandTuple);
        }
        public void RegisterSideSpecificCommand(String objType1, String objType2, CollisionSide side, ConstructorInfo commandOnObj1, ConstructorInfo commandOnObj2)
        {
            Tuple<String, String, CollisionSide> collisionTuple = new Tuple<string, string, CollisionSide>(objType1, objType2, side);
            Tuple<ConstructorInfo, ConstructorInfo> commandTuple = new Tuple<ConstructorInfo, ConstructorInfo>(commandOnObj1, commandOnObj2);
            SideSpecificCollisionMap.Add(collisionTuple, commandTuple);
        }

        public void Collide(Collision c)
        {
            Tuple<ConstructorInfo, ConstructorInfo> commandsToExecute = new Tuple<ConstructorInfo, ConstructorInfo>(null, null);
            commandsToExecute = GetCommandsToExecuteGeneral(c, AllSideOverride);
            if (commandsToExecute.Item1 == null || commandsToExecute.Item2 == null)
            {
                commandsToExecute = GetCommandsToExecuteSideSpecific(c);
            }
            if(commandsToExecute.Item1 == null || commandsToExecute.Item2 == null){
                commandsToExecute = GetCommandsToExecuteGeneral(c,CollisionMap);
            }

            if (commandsToExecute.Item1 != null && commandsToExecute.Item2 != null)
            {
                ((ICommand)commandsToExecute.Item1.Invoke(new object[] { c })).Execute();
                ((ICommand)commandsToExecute.Item2.Invoke(new object[] { c })).Execute();
            }
        }
        private Tuple<ConstructorInfo,ConstructorInfo> GetCommandsToExecuteSideSpecific(Collision c)
        {
            IGameObject obj1 = c.ObjectColliding;
            IGameObject obj2 = c.ObjectCollidedWith;
            CollisionSide side = c.Side;
            Tuple<ConstructorInfo, ConstructorInfo> commandsToExecute = new Tuple<ConstructorInfo, ConstructorInfo>(null, null);

            Tuple<String, String, CollisionSide> doubleSpecificKey = new Tuple<string, string, CollisionSide>(obj1.SpecificCollisionType, obj2.SpecificCollisionType, side);
            Tuple<String, String, CollisionSide> obj1SpecificKey = new Tuple<string, string, CollisionSide>(obj1.SpecificCollisionType, obj2.CollisionType, side);
            Tuple<String, String, CollisionSide> obj2SpecificKey = new Tuple<string, string, CollisionSide>(obj1.CollisionType, obj2.SpecificCollisionType, side);
            Tuple<String, String, CollisionSide> noSpecificKey = new Tuple<string, string, CollisionSide>(obj1.CollisionType, obj2.CollisionType, side);


            if (SideSpecificCollisionMap.ContainsKey(doubleSpecificKey))
            {
                commandsToExecute = SideSpecificCollisionMap[doubleSpecificKey];

            }
            else if (SideSpecificCollisionMap.ContainsKey(obj1SpecificKey))
            {
                commandsToExecute = SideSpecificCollisionMap[obj1SpecificKey];

            }
            else if (SideSpecificCollisionMap.ContainsKey(obj2SpecificKey))
            {
                commandsToExecute = SideSpecificCollisionMap[obj2SpecificKey];

            }
            else if (SideSpecificCollisionMap.ContainsKey(noSpecificKey))
            {
                commandsToExecute = SideSpecificCollisionMap[noSpecificKey];

            }
            return commandsToExecute;
        }
        
        private static Tuple<ConstructorInfo,ConstructorInfo> GetCommandsToExecuteGeneral(Collision c, Dictionary<Tuple<String,String>,Tuple<ConstructorInfo,ConstructorInfo>> map)
        {
            IGameObject obj1 = c.ObjectColliding;
            IGameObject obj2 = c.ObjectCollidedWith;
            Tuple<ConstructorInfo, ConstructorInfo> commandsToExecute = new Tuple<ConstructorInfo, ConstructorInfo>(null, null);

            Tuple<String, String> doubleSpecificKey = new Tuple<string, string>(obj1.SpecificCollisionType, obj2.SpecificCollisionType);
            Tuple<String, String> obj1SpecificKey = new Tuple<string, string>(obj1.SpecificCollisionType, obj2.CollisionType);
            Tuple<String, String> obj2SpecificKey = new Tuple<string, string>(obj1.CollisionType, obj2.SpecificCollisionType);
            Tuple<String, String> noSpecificKey = new Tuple<string, string>(obj1.CollisionType, obj2.CollisionType);


            if (map.ContainsKey(doubleSpecificKey))
            {
                commandsToExecute = map[doubleSpecificKey];

            }
            else if (map.ContainsKey(obj1SpecificKey))
            {
                commandsToExecute = map[obj1SpecificKey];

            }
            else if (map.ContainsKey(obj2SpecificKey))
            {
                commandsToExecute = map[obj2SpecificKey];

            }
            else if (map.ContainsKey(noSpecificKey))
            {
                commandsToExecute = map[noSpecificKey];

            }
            return commandsToExecute;
        }

        private void RegisterAllCommands()
        {
            Type[] constructorParams = new Type[] { typeof(Collision) };

            //How Everything default interacts with blocks  The Special Commands are more specific to type of block
            RegisterSideSpecificCommand("IMario", "IBlock",CollisionSide.Top, typeof(MarioLand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "IBlock",CollisionSide.Top, typeof(BumpEnemy).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IItem", "IBlock",CollisionSide.Top, typeof(GeneralCollideBlockTop).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("FireBall", "IBlock", CollisionSide.Top, typeof(FireballBounce).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "TeleportPipe", CollisionSide.Top, typeof(PipeUndergroundCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));


            RegisterSideSpecificCommand("IMario", "IBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "IBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IItem", "IBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("FireBall", "IBlock", CollisionSide.Bottom, typeof(FireballExplode).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "HiddenBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(BumpBlock).GetConstructor(constructorParams));


            RegisterSideSpecificCommand("IMario", "IBlock", CollisionSide.Left, typeof(MarioCollideBlockLeft).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "IBlock", CollisionSide.Left, typeof(EnemyTurnAroundLeft).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IItem", "IBlock", CollisionSide.Left, typeof(ItemTurnAroundLeft).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("FireBall", "IBlock", CollisionSide.Left, typeof(FireballExplode).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "SidewaysPipe", CollisionSide.Left, typeof(PipeLeaveUndergroundCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("ExplodedFireBall", "IBlock", CollisionSide.Left, typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));


            RegisterSideSpecificCommand("IMario", "IBlock", CollisionSide.Right, typeof(MarioCollideBlockRight).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "IBlock", CollisionSide.Right, typeof(EnemyTurnAroundRight).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IItem", "IBlock", CollisionSide.Right, typeof(ItemTurnAroundRight).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("FireBall", "IBlock", CollisionSide.Right, typeof(FireballExplode).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("ExplodedFireBall", "IBlock", CollisionSide.Right, typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));

            //Mario interacts with bumpable blocks + those with items
            RegisterSideSpecificCommand("IMario", "BreakableBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(BumpBlock).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "ItemBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(BumpBlock).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "OpenedBlock", CollisionSide.Bottom, typeof(GeneralCollideBlockBottom).GetConstructor(constructorParams), typeof(BumpBlock).GetConstructor(constructorParams));

            //Mario interacts with items
            RegisterCommand("IMario", "Star", typeof(StarMarioCommand).GetConstructor(constructorParams), typeof(RemoveObj2Command).GetConstructor(constructorParams));
            RegisterCommand("IMario", "FireFlower", typeof(FireMarioCommand).GetConstructor(constructorParams), typeof(RemoveObj2Command).GetConstructor(constructorParams));
            RegisterCommand("IMario", "BigMushroom", typeof(BigMarioCommand).GetConstructor(constructorParams), typeof(RemoveObj2Command).GetConstructor(constructorParams));
            RegisterCommand("IMario", "Coin", typeof(NullCommand).GetConstructor(constructorParams), typeof(RemoveObj2Command).GetConstructor(constructorParams));
            RegisterCommand("IMario", "HealthMushroom", typeof(OneUp).GetConstructor(constructorParams), typeof(RemoveObj2Command).GetConstructor(constructorParams));
            RegisterCommand("DeadMario", "IItem", typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));

            //Mario + Enemy Collision
            RegisterSideSpecificCommand("IMario", "IEnemy", CollisionSide.Top, typeof(MarioBounce).GetConstructor(constructorParams), typeof(StompEnemy).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "Spiny", CollisionSide.Top, typeof(MarioTakeDamage).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterCommand("IMario", "IEnemy", typeof(MarioTakeDamage).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterCommand("TransitionMario", "IEnemy", typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));

            RegisterOverrideCommand("StarMario", "IEnemy", typeof(NullCommand).GetConstructor(constructorParams), typeof(FlipEnemyTwo).GetConstructor(constructorParams));

            //Enemy + Enemy Collisions and Shell
            RegisterSideSpecificCommand("IEnemy", "IEnemy", CollisionSide.Left, typeof(NullCommand).GetConstructor(constructorParams), typeof(TurnBothLeft).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "IEnemy", CollisionSide.Right, typeof(NullCommand).GetConstructor(constructorParams), typeof(TurnBothRight).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "IEnemy", CollisionSide.Top, typeof(NullCommand).GetConstructor(constructorParams), typeof(FlipEnemyNoPoints).GetConstructor(constructorParams));

            RegisterSideSpecificCommand("IEnemy", "Shell", CollisionSide.Left, typeof(EnemyTurnAroundLeft).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterCommand("FireBall", "IEnemy", typeof(FireballExplode).GetConstructor(constructorParams), typeof(FlipEnemyTwo).GetConstructor(constructorParams));
            RegisterCommand("ExplodedFireBall", "IEnemy", typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
           
            RegisterSideSpecificCommand("IMario", "Shell", CollisionSide.Left, typeof(GeneralCollideBlockLeft).GetConstructor(constructorParams), typeof(PushShell).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "Shell", CollisionSide.Right, typeof(GeneralCollideBlockRight).GetConstructor(constructorParams), typeof(PushShell).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "Shell", CollisionSide.Top, typeof(MarioBounce).GetConstructor(constructorParams), typeof(PushShell).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IMario", "MovingShell", CollisionSide.Top, typeof(MarioBounce).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterCommand("IMario", "Hammer", typeof(MarioTakeDamage).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));

            RegisterSideSpecificCommand("MovingShell", "IEnemy", CollisionSide.Right, typeof(NullCommand).GetConstructor(constructorParams), typeof(FlipEnemyTwo).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy","MovingShell", CollisionSide.Right, typeof(FlipEnemyOne).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("MovingShell", "IEnemy", CollisionSide.Left, typeof(NullCommand).GetConstructor(constructorParams), typeof(FlipEnemyTwo).GetConstructor(constructorParams));
            RegisterSideSpecificCommand("IEnemy", "MovingShell", CollisionSide.Left, typeof(FlipEnemyOne).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));

            RegisterOverrideCommand("Lakitu", "Spiny", typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterOverrideCommand("Spiny", "Lakitu", typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
            RegisterOverrideCommand("Lakitu", "IBlock", typeof(NullCommand).GetConstructor(constructorParams), typeof(NullCommand).GetConstructor(constructorParams));
        }
    } 
}
