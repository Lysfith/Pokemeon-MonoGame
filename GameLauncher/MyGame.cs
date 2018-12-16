using GameMapLibrary;
using GameUILibrary;
using GameUILibrary.Components.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class MyGame : Game
    {
        GraphicsDeviceManager graphics;

        private int nativeScreenWidth;
        private int nativeScreenHeight;

        private float _aspectRatio;
        private Point _oldWindowSize;

        private SpriteBatch _spriteBatch;
        private RenderTarget2D _offScreenRenderTarget;

        private UI _mainMenu;

        private Map _map;

        public MyGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreparingDeviceSettings += graphics_PreparingDeviceSettings;
            graphics.DeviceCreated += graphics_DeviceCreated;

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            this.Window.AllowUserResizing = true;
            this.Window.Title = "Test";

            
        }

        void graphics_DeviceCreated(object sender, EventArgs e)
        {
            
        }

        private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            nativeScreenWidth = graphics.PreferredBackBufferWidth;
            nativeScreenHeight = graphics.PreferredBackBufferHeight;

            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            //e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 16;

            _aspectRatio = 1366f/768f;
            _oldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            // Remove this event handler, so we don't call it when we change the window size in here
            Window.ClientSizeChanged -= Window_ClientSizeChanged;

            if (Window.ClientBounds.Width != _oldWindowSize.X)
            { // We're changing the width
                // Set the new backbuffer size
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = (int)(Window.ClientBounds.Width / _aspectRatio);
            }
            else if (Window.ClientBounds.Height != _oldWindowSize.Y)
            { // we're changing the height
                // Set the new backbuffer size
                graphics.PreferredBackBufferWidth = (int)(Window.ClientBounds.Height * _aspectRatio);
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            }

            graphics.ApplyChanges();

            // Update the old window size with what it is currently
            _oldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);

            _offScreenRenderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);

            // add this event handler back
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            if (_map != null)
            {
                _map.Resize(this);
            }
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var callbacks = new Dictionary<string, Action<object, EventArgs>>();
            callbacks.Add("Button_HoverStart", (sender, e) => {
                var b = (UIButton)sender;

                b.Color = Color.Yellow;
            });

            callbacks.Add("Button_HoverEnd", (sender, e) => {
                var b = (UIButton)sender;

                b.Color = Color.White;
            });

            callbacks.Add("Quit_OnGainFocus", (sender, e) => {
                Exit();
            });

            //_mainMenu = UI.Load("UIDescription/MainMenu/MainMenu.xml", this, new Dictionary<string, string>(), callbacks);
            _mainMenu = UI.Load("UIDescription/Game/Game.xml", this, new Dictionary<string, string>(), callbacks);

            this.Components.Add(_mainMenu);

            //var popup = UI.LoadControl("UIDescription/Common/Popup.xml", this, new Dictionary<string, string>(), new Dictionary<string, Action>());

            //var layer = _mainMenu.GetLayer(1);
            //layer.AddControl(popup);

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.IsMouseVisible = true;

            _map = new Map("Maps/test.tmx");
            _map.Initialize(this);

            _map.OnInteract += (e, obj) =>
            {
                var texts = new Dictionary<string, string>();
                texts.Add("pancarte_text", obj);
                var pancarte = UI.LoadControl("UIDescription/Game/Pancarte.xml", this, texts, new Dictionary<string, Action<object, EventArgs>>());

                _mainMenu.GetLayer(0).Clear();
                _mainMenu.GetLayer(0).AddControl(pancarte);
            };

            _map.OnMove += (e, obj) =>
            {
                _mainMenu.GetLayer(0).Clear();
            };
        }


        private void ExitEvent(object parameter)
        {
            Exit();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _map.Move(0, 1);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _map.Move(0, -1);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _map.Move(-1, 0);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _map.Move(1, 0);
            }

            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.SetRenderTarget(_offScreenRenderTarget);
            return base.BeginDraw();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _map.DrawFloor(gameTime, _spriteBatch);
            _map.DrawCollisions(gameTime, _spriteBatch);
            //_map.DrawInteractions(gameTime, _spriteBatch);
            _map.DrawPlayer(gameTime, _spriteBatch);
            _map.DrawForeground(gameTime, _spriteBatch);

            _mainMenu.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            if (_offScreenRenderTarget != null)
            {
                GraphicsDevice.SetRenderTarget(null);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_offScreenRenderTarget, GraphicsDevice.Viewport.Bounds, Color.White);
                _spriteBatch.End();
            }
            base.EndDraw();
        }
    }
}
