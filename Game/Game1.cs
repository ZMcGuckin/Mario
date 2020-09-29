using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Keys = Microsoft.Xna.Framework.Input.Keys;

[assembly: CLSCompliant(true)]
namespace TheKoopaTroopas
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager Graphics { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public GameTime GameTime { get; set; }
        public LevelLoader Level { get; set; }
        public GameVariables GameVariables { get; set; }
        public GameLists GameLists { get; set; }
        public TransitionScreen TransitionScreen { get; set; }
        public Camera Camera { get; set; }
        public HUD HUD { get; set; }
        public Music Music { get; set; }
        public IList<IMario> Marios { get; private set; }

        public enum GameState { Playing, Paused, Transition, MainMenu, End, Dead, GameOver, GameComplete }
        public GameState CurrentState { get; set; }

        KeyController keyController;
        PadController padController;

        Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
        }

        public static Game1 Instance { get; } = new Game1();

        protected override void Initialize()
        {
            base.Initialize();
            GameVariables = new GameVariables();
            Marios = new List<IMario>();
            GameVariables.LevelNumber = 1;
            GameVariables.ScreenWidth = GraphicsDevice.Viewport.Width;
            GameVariables.ScreenHeight = GraphicsDevice.Viewport.Height;
            Level = new LevelLoader();
            StartMainMenu();
            HUD = new HUD(GameVariables.LevelTimer);
            Camera = new Camera(new Point(0, 0));
            Music = new Music();
            TransitionScreen = new TransitionScreen();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>("HUD");
            MarioSpriteFactory.Instance.LoadAllTextures(Content);
            MusicFactory.Instance.LoadAllTextures(Content);
            SoundEffectsFactory.Instance.LoadAllTextures(Content);
            UniversalSpriteFactory.Instance.LoadAllTextures(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime time)
        {
            GameTime = time;
            keyController.Update();
            padController.Update();
            TransitionScreen.Update(time);
            Music.Update();
            if (!GameVariables.Underground)
            {
                Camera.Update();
            }
            GameVariables.Update(time);

            if (CurrentState != GameState.GameOver && CurrentState != GameState.MainMenu && CurrentState != GameState.GameComplete)
            {
                if (CurrentState == GameState.Playing || CurrentState == GameState.End || CurrentState == GameState.Dead)
                {
                    CollisionDetector.Update();

                    GameLists.Update(GameTime);

                    for (int i = 0; i < Marios.Count; i++)
                    {
                        Marios[i].Update(GameTime);

                    }

                    HUD.Update(GameVariables.LevelTimer, new Vector2(Camera.Point.X, Camera.Point.Y), GameVariables.LevelNumber);
                }
            }

            base.Update(GameTime);
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(GameVariables.BackgroundColor);
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,null, null, null, null, Camera.Transform);

            GameLists.Draw(SpriteBatch);
            for (int i = 0; i < Marios.Count; i++)
            {
                Marios[i].Draw(SpriteBatch);
            }
            HUD.Draw(SpriteBatch, SpriteFont, Color.White);
            base.Draw(GameTime);
            if (CurrentState != GameState.Playing)
            {
                if (CurrentState == GameState.MainMenu)
                    TransitionScreen.Draw(SpriteBatch, SpriteFont, "MainMenu");
                else if (CurrentState == GameState.Paused)
                    TransitionScreen.Draw(SpriteBatch, SpriteFont, "Paused");
                else if (CurrentState == GameState.GameOver)
                    TransitionScreen.Draw(SpriteBatch, SpriteFont, "GameOver");
                else if (CurrentState == GameState.Transition)
                    TransitionScreen.Draw(SpriteBatch, SpriteFont, "Transition");
                else if (CurrentState == GameState.GameComplete)
                    TransitionScreen.Draw(SpriteBatch, SpriteFont, "GameComplete");
            }

            SpriteBatch.End();           
        }

        public void StarMario(IMario mario)
        {
            for (int i = 0; i < Marios.Count; i++)
            {
                if (mario == Marios[i])
                {
                    if (mario is StarMario starMario)
                        starMario.ResetStarTimer();
                    else
                        Marios[i] = new StarMario(mario);
                }
            }
        }

        public void UpgradeMario(IMario mario, IMarioState newState)
        {
            for (int i = 0; i < Marios.Count; i++)
            {
                if (mario == Marios[i])
                {
                    if (newState.SequenceOrder > this.Marios[i].State.SequenceOrder)
                    {
                        Marios[i] = new TransitionMario(mario, mario.State, newState);
                        new SoundEffects().PlayPowerup();
                    }
                }
            }
        }

        public void DamageMario(IMario mario, IMarioState newState)
        {
            for (int i = 0; i < Marios.Count; i++)
            {
                if (mario == Marios[i])
                {
                    Marios[i] = new TransitionMario(mario, mario.State, newState);
                    new SoundEffects().PlayPowerDown();
                }
            }
        }

        public void RegularMario(IMario decoratedMario, IMario originalMario)
        {
            for (int i = 0; i < Marios.Count; i++)
            {
                if (decoratedMario == Marios[i])
                {
                    Marios[i] = originalMario;
                }
            }
        }

        public void KillMario(int playerNumber)
        {
            GameVariables.Lives--;
            GameVariables.CurrentlyAlive--;
            Marios.RemoveAt(playerNumber);
            if (!Marios.Any())
                LevelReset();
        }

        public void EndLevel()
        {
            CurrentState = GameState.End;
            if (GameVariables.PlayFinal)
            {
                GameVariables.PlayFinal = false;
                new SoundEffects().PlayFlagpoleSlide();
                GameVariables.Score += GameVariables.LevelTimer * 100;
                GameVariables.LevelTimer = 0;
            }
            else
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    if (GameVariables.LevelNumber != GameConstants.lastLevel)
                    {
                        GameVariables.LevelNumber++;
                        LevelReset();
                    }
                    else
                    {
                        CurrentState = GameState.GameComplete;
                    }
                }
            }
        }

        public void PauseUnpauseGame()
        {
            if(GameVariables.LastPaused > GameVariables.PausedInterval)
            {
                if (CurrentState == GameState.Paused)
                {
                    MediaPlayer.Resume();
                    new SoundEffects().PlayPause();
                    CurrentState = GameState.Playing;
                }
                else
                {
                    MediaPlayer.Pause();
                    new SoundEffects().PlayPause();
                    CurrentState = GameState.Paused;

                }
                GameVariables.LastPaused = 0;
            }
        }

        public void GoUnderground(Vector2 location)
        {
            GameVariables.Underground = true;
            GameVariables.BackgroundColor = Color.Black;
            Camera.LookAt(new Point(700, 650));
            for (int i = 0; i < Marios.Count; i++)
            {
                Marios[i].Location = location;
            }
            new SoundEffects().PlayPipe();
        }

        public void GoAboveGround(Vector2 location)
        {
            GameVariables.Underground = false;
            GameVariables.BackgroundColor = Color.CornflowerBlue;
            for (int i = 0; i < Marios.Count; i++)
            {
                Marios[i].Location = location;
            }
            new SoundEffects().PlayPipe();
        }

        public void StartMainMenu()
        {
            CurrentState = GameState.MainMenu;
            keyController = new KeyController();
            padController = new PadController();
            RegisterCommands();
            GameLists = new GameLists();
            Level.LoadLevel(0);
        }

        public void ChooseMode()
        {
            if (TransitionScreen.CursorPos.Y == TransitionScreen.OnePlayerPos.Y)
            {
                GameVariables.PlayerNumber = 1;
            }
            else
            {
                GameVariables.PlayerNumber = 2;
            }
            new SoundEffects().PlayMenuChoose();
            GameReset();
        }

        public void GameReset()
        {
            GameVariables.LevelNumber = 1;
            GameVariables.CollectedCoins = 0;
            GameVariables.Score = 0;
            GameVariables.Lives = 3 * GameVariables.PlayerNumber;
            CurrentState = GameState.Transition;
            LevelReset();
        }

        public void LevelReset()
        {
            if(GameVariables.Lives > 0)
            {
                GameVariables.CurrentlyAlive = GameVariables.PlayerNumber;
                GameLists = new GameLists();
                Level.LoadLevel(GameVariables.LevelNumber);
                CurrentState = GameState.Transition;
                TransitionScreen.TransitionTimer = 0;
                keyController = new KeyController();
                padController = new PadController();
                RegisterCommands();
                GameVariables.Underground = false;
                GameVariables.BackgroundColor = Color.CornflowerBlue;
                MediaPlayer.IsRepeating = true;
                GameVariables.LevelTimer = GameVariables.TotalTime;
                GameVariables.ElapsedTime = 0;
                GameVariables.PlayFinal = true;
            }
            else
            {
                CurrentState = GameState.GameOver;
            }
        }

        public void RegisterCommands()
        {
            if(CurrentState == GameState.MainMenu)
            {
                keyController.RegisterCommand(Keys.Up, new ModeSelectionCommand());
                keyController.RegisterCommand(Keys.Down, new ModeSelectionCommand());
                keyController.RegisterCommand(Keys.Enter, new ModeChooseCommand());
                padController.RegisterCommand(Buttons.LeftThumbstickUp, new ModeSelectionCommand());
                padController.RegisterCommand(Buttons.LeftThumbstickDown, new ModeSelectionCommand());
                padController.RegisterCommand(Buttons.Start, new ModeChooseCommand());
            }
            else
            {
                keyController.RegisterCommand(Keys.R, new ResetCommand());
                keyController.RegisterCommand(Keys.P, new PauseCommand());

                keyController.RegisterCommand(Keys.Up, new JumpCommand(Marios[0]));
                keyController.RegisterCommand(Keys.RightShift, new ActionCommand(Marios[0]));
                keyController.RegisterCommand(Keys.Down, new CrouchCommand(Marios[0]));
                keyController.RegisterCommand(Keys.Left, new LeftRunCommand(Marios[0]));
                keyController.RegisterCommand(Keys.Right, new RightRunCommand(Marios[0]));

                if (GameVariables.PlayerNumber == 2)
                {
                    keyController.RegisterCommand(Keys.W, new JumpCommand(Marios[1]));
                    keyController.RegisterCommand(Keys.E, new ActionCommand(Marios[1]));
                    keyController.RegisterCommand(Keys.S, new CrouchCommand(Marios[1]));
                    keyController.RegisterCommand(Keys.A, new LeftRunCommand(Marios[1]));
                    keyController.RegisterCommand(Keys.D, new RightRunCommand(Marios[1]));
                }

                padController.RegisterCommand(Buttons.LeftThumbstickDown, new CrouchCommand(Marios[0]));
                padController.RegisterCommand(Buttons.A, new JumpCommand(Marios[0]));
                padController.RegisterCommand(Buttons.B, new ActionCommand(Marios[0]));
                padController.RegisterCommand(Buttons.LeftThumbstickLeft, new LeftRunCommand(Marios[0]));
                padController.RegisterCommand(Buttons.LeftThumbstickRight, new RightRunCommand(Marios[0]));
                padController.RegisterCommand(Buttons.Start, new PauseCommand());
            }
            keyController.RegisterCommand(Keys.Q, new ExitCommand());
        }
    }
}
