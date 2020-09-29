using System;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Point Point { get; private set; }
        public IMario Player1 { get; private set; }
        public IMario Player2 { get; private set; }
        double halfPoint;
        double leftWindowBuffer;

        public Camera(Point location)
        {
            Transform = Matrix.CreateTranslation(new Vector3(-location.X, -location.Y, 0));
            halfPoint = Game1.Instance.GameVariables.ScreenWidth / 2;
            leftWindowBuffer = halfPoint / 3;
            LookAt(location);
        }
        public void LookAt(Point location)
        {
            Transform = Matrix.CreateTranslation(new Vector3(-location.X, -location.Y, 0));
            Point = location;
        }
        public void Update()
        {
            if (Game1.Instance.CurrentState == Game1.GameState.Transition)
            {
                LookAt(new Point(0, 0));
            }
            else if (Game1.Instance.GameVariables.CurrentlyAlive == 1)
            {
                Player1 = Game1.Instance.Marios[0];
                if ((Point.X + leftWindowBuffer) > Player1.Location.X)
                {
                    if (Player1.Location.X - leftWindowBuffer < Game1.Instance.Level.BeginningOfLevel)
                    {
                        LookAt(new Point(Game1.Instance.Level.BeginningOfLevel, 0));
                    }
                    else
                    {
                        LookAt(new Point((int)Player1.Location.X - (int)leftWindowBuffer, 0));
                    }
                }
                else if ((Point.X + halfPoint) < Player1.Location.X)
                {
                    LookAt(new Point((int)Player1.Location.X - (int)halfPoint, 0));
                }
            } //If there's a second player, also let that player edit the camera
            else if (Game1.Instance.GameVariables.CurrentlyAlive == 2)
            {
                Player1 = Game1.Instance.Marios[0];
                Player2 = Game1.Instance.Marios[1];
                double middlePoint = (Player1.Location.X + Player2.Location.X) / 2;
                double startPoint = middlePoint - Game1.Instance.GameVariables.ScreenWidth / 2;
                if (startPoint > Game1.Instance.Level.BeginningOfLevel )
                {
                    LookAt(new Point((int)startPoint, 0));
                }
                else
                {
                    LookAt(new Point(Game1.Instance.Level.BeginningOfLevel, 0));
                }
            } //else if everyone is dead, point back at the start
            else
            {
                LookAt(new Point(0, 0));
            }
        }
    }
}
