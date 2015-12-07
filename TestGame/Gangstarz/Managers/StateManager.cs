namespace Gangstarz.Managers
{
    using Removable;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Audio;
    using System.Collections.Generic;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class StateManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //private Texture2D background;
        //private Texture2D mainChar;
        //private Texture2D mainPathEffects;

        //private SpriteFont font;
        //private int score = 0;

        //private AnimatedSprite animatedSprite;

        //private MouseState oldState;

        //private List<SoundEffect> soundEffects;

        public StateManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //soundEffects = new List<SoundEffect>();

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Dimensions.Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenManager.Instance.LoadContent(Content);
            //this.background = Content.Load<Texture2D>("Images/MainMap/MirrorMainMap");
            ////this.mainChar = Content.Load<Texture2D>("Images/Sprites/MainMap/Characters/Beat");
            //this.mainPathEffects = Content.Load<Texture2D>("Images/Sprites/MainMap/Effects/Flammes");

            //animatedSprite = new AnimatedSprite(mainPathEffects, 8, 1);

            //font = Content.Load<SpriteFont>("Fonts/MainFont");


            //soundEffects.Add(Content.Load<SoundEffect>("airlockclose"))
            //                 ;
            //soundEffects.Add(Content.Load<SoundEffect>("ak47"));
            //soundEffects.Add(Content.Load<SoundEffect>("icecream"));
            //soundEffects.Add(Content.Load<SoundEffect>("sneeze"));

            //// Fire and forget play
            //soundEffects[0].Play();

            //// Play that can be manipulated after the fact
            //var instance = soundEffects[0].CreateInstance();
            //instance.IsLooped = true;
            //instance.Play();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
            //Content.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Instance.Update(gameTime);

            //MouseState newState = Mouse.GetState();

            //if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            //{
            //    // do something here
            //}

            //oldState = newState;
            ////score++;
            //animatedSprite.Update();

            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    if (SoundEffect.MasterVolume == 0.0f)
            //        SoundEffect.MasterVolume = 1.0f;
            //    else
            //        SoundEffect.MasterVolume = 0.0f;
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);           

            spriteBatch.Begin();
            ScreenManager.Instance.Draw(spriteBatch);
            //spriteBatch.Draw(background, new Rectangle(0, 0, 2500, 1250), Color.White);
            //spriteBatch.Draw(mainChar, new Vector2(500, 100), Color.White);
            //spriteBatch.Draw(mainPathEffects, new Vector2(400, 200), Color.White);

            //spriteBatch.DrawString(font, $"Score: {score}", new Vector2(100, 100), Color.White); 
            spriteBatch.End();

            //animatedSprite.Draw(spriteBatch, new Vector2(100, 200));

            base.Draw(gameTime);
        }
    }
}
