using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.DebugViews;

namespace WindowsGame3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world;
        SpriteFont font;

        ArrayList objectList = new ArrayList();

        KeyboardState aKeyboardState;
        KeyboardState previousKeyboardState;

        GameObject aRectangle = new RectangleObject();
        GameObject anotherRectangle = new RectangleObject();
        GameObject floorRectangle = new RectangleObject();
        GameObject aCircle = new CircleObject();

        LevelBuilder lvlBuilder;

        DebugViewXNA debugView;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            
            if (world == null)
            {
                world = new World(new Vector2(0, 98.2f));
            }
            else
            {
                world.Clear();
            }

            this.IsMouseVisible = true;

            lvlBuilder = new LevelBuilder(Content, world);
            lvlBuilder.readLevel("testlvl.txt");

            Console.WriteLine(GraphicsDevice.Viewport.Width + "x" + GraphicsDevice.Viewport.Height);

            objectList = lvlBuilder.getLevelObjects();

            //FarseerPhysics.Collision
            font = Content.Load<SpriteFont>("font");

            debugView = new DebugViewXNA(world);
            debugView.LoadContent(this.GraphicsDevice, this.Content);
            debugView.AppendFlags(FarseerPhysics.DebugViewFlags.Shape);
            debugView.AppendFlags(FarseerPhysics.DebugViewFlags.PolygonPoints);
            //debugView.AppendFlags(FarseerPhysics.DebugViewFlags.DebugPanel);
            debugView.AppendFlags(FarseerPhysics.DebugViewFlags.CenterOfMass);

            foreach (GameObject currObject in objectList)
            {
                Console.WriteLine(currObject.getBody().Position.X.ToString() + "X" + currObject.getBody().Position.Y.ToString());
            }
            
            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            aKeyboardState = Keyboard.GetState();
            if (aKeyboardState.IsKeyDown(Keys.F12) && previousKeyboardState != aKeyboardState)
            {
                if (debugView.Flags.HasFlag(FarseerPhysics.DebugViewFlags.DebugPanel))
                {
                    debugView.RemoveFlags(FarseerPhysics.DebugViewFlags.DebugPanel);
                }
                else
                {
                    debugView.AppendFlags(FarseerPhysics.DebugViewFlags.DebugPanel);
                    debugView.DebugPanelPosition = new Vector2(0, 0);
                }
                
            }
            else if (aKeyboardState.IsKeyDown(Keys.F11) && previousKeyboardState != aKeyboardState)
            {
                if (debugView.Enabled)
                {
                    debugView.Enabled = false;
                }
                else if (!debugView.Enabled)
                {
                    debugView.Enabled = true;
                }
            }
            else if (aKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState != aKeyboardState)
            {
                //circle.LinearVelocity = new Vector2(7, 12);
            }
            else if (aKeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f/30f)));
            previousKeyboardState = aKeyboardState;
            // TODO: Add your update logic here

            foreach (GameObject currObject in objectList)
            {
                currObject.Update();
                
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Matrix view = Matrix.CreateTranslation(new Vector3(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height / 2.0f, 0.0f));
            debugView.RenderDebugData(ref view);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (GameObject currObject in objectList)
            {
                currObject.Draw(spriteBatch, graphics);
            }
            spriteBatch.DrawString(font, "TEST", Vector2.Zero, Color.Red);
            spriteBatch.End();

            
           
            base.Draw(gameTime);
        }
    }
}
