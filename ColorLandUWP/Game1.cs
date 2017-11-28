using ColorLand;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace ColorLandUWP
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public static String sPROGRESS_FILE_NAME = "progresxxs.lol";

        public static int sSCREEN_RESOLUTION_WIDTH = 800;//640;
        public static int sSCREEN_RESOLUTION_HEIGHT = 600;//480;

        public static int sHALF_SCREEN_RESOLUTION_WIDTH = sSCREEN_RESOLUTION_WIDTH / 2;
        public static int sHALF_SCREEN_RESOLUTION_HEIGHT = sSCREEN_RESOLUTION_HEIGHT / 2;

        public static bool sKINECT_BASED = false;

        public static ProgressObject progressObject;

        //Original structure
        private static Game1 instance;
        ScreenManager mScreenManager;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public Game1()
        {
            progressObject = ExtraFunctions.loadProgress();

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            //* Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            graphics.IsFullScreen = false;

            mScreenManager = new ScreenManager(this);

            Components.Add(mScreenManager);

            instance = this;
            SoundManager.Initialize(this);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            if (sKINECT_BASED)
            {
                // KinectManager.getInstance().init();
            }
#if WINDOWS_PHONE
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800; 
#else
            graphics.PreferredBackBufferWidth = sSCREEN_RESOLUTION_WIDTH;
            graphics.PreferredBackBufferHeight = sSCREEN_RESOLUTION_HEIGHT;
#endif
            graphics.ApplyChanges();
            graphics.GraphicsDevice.Clear(Color.Black);


            base.Initialize();



        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public static Game1 getInstance()
        {
            return instance;
        }

        public ScreenManager getScreenManager()
        {
            return this.mScreenManager;
        }

        public void toggleFullscreen()
        {
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();
            graphics.GraphicsDevice.Clear(Color.Black);
        }

        public void setFullscreen(bool fullscreen)
        {
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
            graphics.GraphicsDevice.Clear(Color.Black);
        }

        protected override void UnloadContent()
        {
            /*  kinectSensor.Uninitialize();  */
        }

        public static Tuple<Vector2, Vector2> GetDisplayTransform(Vector2 outputSize, Vector2 sourceSize)
        {
            // Scale the display to fill the control.
            var scale = outputSize / sourceSize;
            var offset = Vector2.Zero;

            // Letterbox or pillarbox to preserve aspect ratio.
            if (scale.X > scale.Y)
            {
                scale.X = scale.Y;
                offset.X = (outputSize.X - sourceSize.X * scale.X) / 2;
            }
            else
            {
                scale.Y = scale.X;
                offset.Y = (outputSize.Y - sourceSize.Y * scale.Y) / 2;
            }

            return new Tuple<Vector2, Vector2>(scale,offset);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            graphics.GraphicsDevice.Clear(Color.Black);

            SpriteBatch targetBatch = new SpriteBatch(GraphicsDevice);
            RenderTarget2D target = new RenderTarget2D(GraphicsDevice, sSCREEN_RESOLUTION_WIDTH, sSCREEN_RESOLUTION_HEIGHT);
            GraphicsDevice.SetRenderTarget(target);
            base.Draw(gameTime);
            //set rendering back to the back buffer
            GraphicsDevice.SetRenderTarget(null);

            var result = GetDisplayTransform(new Vector2(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), new Vector2(sSCREEN_RESOLUTION_WIDTH, sSCREEN_RESOLUTION_HEIGHT));

            //render target to back buffer
            targetBatch.Begin();
            targetBatch.Draw(target, new Rectangle((int)result.Item2.X,(int) result.Item2.Y, (int)(sSCREEN_RESOLUTION_WIDTH*result.Item1.X), (int)(sSCREEN_RESOLUTION_HEIGHT * result.Item1.Y)), Color.White);
            targetBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            if (KeyboardManager.getInstance().pressed(Keys.A))
            {
                Exit();
            }
        }

        public static void print(String message)
        {
            //System.Diagnostics.Debug.WriteLine(message);
            Debug.WriteLine(message);
        }

    }
}
