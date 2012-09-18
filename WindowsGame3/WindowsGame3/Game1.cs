using System;
using System.Collections.Generic;
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

        Body rectangle1;
        Body rectangle2;
        Body rectangle3;
        Body rectangle4;
        Body floor;
        Body circle;

        Texture2D rectangleTexture;
        Texture2D floorTexture;
        Texture2D circleTexture;
        SpriteFont font;

        KeyboardState aKeyboardState;
        KeyboardState previousKeyboardState;

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
                world = new World(new Vector2(0, 9.82f));
            }
            else
            {
                world.Clear();
            }

            this.IsMouseVisible = true;

            

            rectangleTexture = Content.Load<Texture2D>("Crate");
            floorTexture = Content.Load<Texture2D>("floor");
            circleTexture = Content.Load<Texture2D>("circle");

            rectangle2 = BodyFactory.CreateRectangle(world, (float)ConvertUnits.ToSimUnits(rectangleTexture.Width - 2), (float)ConvertUnits.ToSimUnits(rectangleTexture.Height - 2), 1.0f);
            rectangle2.BodyType = BodyType.Dynamic;
            rectangle2.Position = new Vector2(5.6f, 2);

            rectangle3 = BodyFactory.CreateRectangle(world, (float)ConvertUnits.ToSimUnits(rectangleTexture.Width - 2), (float)ConvertUnits.ToSimUnits(rectangleTexture.Height - 2), 1.0f);
            rectangle3.BodyType = BodyType.Dynamic;
            rectangle3.Position = new Vector2(5.8f, 3);

            rectangle1 = BodyFactory.CreateRectangle(world, (float)ConvertUnits.ToSimUnits(rectangleTexture.Width - 2), (float)ConvertUnits.ToSimUnits(rectangleTexture.Height - 2), 1.0f);
            rectangle1.BodyType = BodyType.Dynamic;
            rectangle1.Position = new Vector2(5.7f, 0);
            rectangle1.Restitution = 0.2f;
            rectangle1.Friction = 1.0f;
            
            rectangle4 = BodyFactory.CreateRectangle(world, (float)ConvertUnits.ToSimUnits(rectangleTexture.Width - 2), (float)ConvertUnits.ToSimUnits(rectangleTexture.Height - 2), 1.0f);
            rectangle4.BodyType = BodyType.Dynamic;
            rectangle4.Position = new Vector2(5.7f, 4);
            rectangle4.Restitution = 0.2f;
            rectangle4.Friction = 1.0f;
            
            floor = BodyFactory.CreateRectangle(world, (float)ConvertUnits.ToSimUnits(floorTexture.Width), (float)ConvertUnits.ToSimUnits(floorTexture.Height), 1.0f);
            floor.BodyType = BodyType.Static;
            floor.Position = new Vector2(ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Width / 2), ConvertUnits.ToSimUnits(480));

            circle = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits((circleTexture.Height / 2) - 2), 1.0f);
            circle.BodyType = BodyType.Dynamic;
            circle.Position = new Vector2(1.0f, 2f);
            circle.Restitution = 0.5f;
            //FarseerPhysics.Collision
            font = Content.Load<SpriteFont>("font");

            debugView = new DebugViewXNA(world);
            debugView.LoadContent(this.GraphicsDevice, this.Content);
            debugView.AppendFlags(FarseerPhysics.DebugViewFlags.Shape);
            debugView.AppendFlags(FarseerPhysics.DebugViewFlags.PolygonPoints);
            //debugView.AppendFlags(FarseerPhysics.DebugViewFlags.DebugPanel);
            debugView.AppendFlags(FarseerPhysics.DebugViewFlags.CenterOfMass);

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
            if (aKeyboardState.IsKeyDown(Keys.F2) && previousKeyboardState != aKeyboardState) 
            {
                rectangle1.Position = new Vector2(3.3f, 0.1f);
                rectangle1.AngularVelocity = 0;
                rectangle1.LinearVelocity = new Vector2(0 , 0.1f);
                rectangle1.Rotation = 0;
            }
            else if (aKeyboardState.IsKeyDown(Keys.F1) && previousKeyboardState != aKeyboardState)
            {
                rectangle1.Position = new Vector2(2.7f, 0.1f);
                rectangle1.AngularVelocity = 0;
                rectangle1.LinearVelocity = new Vector2(0, 0.1f);
                rectangle1.Rotation = 0;
            }
            else if (aKeyboardState.IsKeyDown(Keys.F12) && previousKeyboardState != aKeyboardState)
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
                circle.LinearVelocity = new Vector2(7, 12);
            }
            else if (aKeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            previousKeyboardState = aKeyboardState;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            FarseerPhysics.Common.Transform t;
            circle.GetTransform(out t);
            Matrix view = Matrix.CreateTranslation(new Vector3(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height / 2.0f, 0.0f));
            debugView.RenderDebugData(ref view);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            spriteBatch.Draw(rectangleTexture, ConvertUnits.ToDisplayUnits(rectangle1.Position), null, Color.White, rectangle1.Rotation, new Vector2(rectangleTexture.Width / 2.0f, rectangleTexture.Height / 2.0f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(rectangleTexture, ConvertUnits.ToDisplayUnits(rectangle2.Position), null, Color.White, rectangle2.Rotation, new Vector2(rectangleTexture.Width / 2.0f, rectangleTexture.Height / 2.0f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(rectangleTexture, ConvertUnits.ToDisplayUnits(rectangle3.Position), null, Color.White, rectangle3.Rotation, new Vector2(rectangleTexture.Width / 2.0f, rectangleTexture.Height / 2.0f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(rectangleTexture, ConvertUnits.ToDisplayUnits(rectangle4.Position), null, Color.White, rectangle4.Rotation, new Vector2(rectangleTexture.Width / 2.0f, rectangleTexture.Height / 2.0f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(floorTexture, ConvertUnits.ToDisplayUnits(floor.Position), null, Color.White, floor.Rotation, new Vector2(floorTexture.Width / 2.0f, floorTexture.Height / 2.0f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(circleTexture, ConvertUnits.ToDisplayUnits(circle.Position), null, Color.White, circle.Rotation, new Vector2(circleTexture.Width / 2.0f, circleTexture.Height / 2.0f), 1f, SpriteEffects.None, 0f);
            
            spriteBatch.End();
            
           
            base.Draw(gameTime);
        }
    }
}
