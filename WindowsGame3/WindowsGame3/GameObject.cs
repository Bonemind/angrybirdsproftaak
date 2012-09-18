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
    /// Abstract class that handles building every gameobject and farseer physics properties related
    /// to that object
    /// </summary>
    abstract class GameObject
    {
        public Texture2D objectTexture;
        public Body theBody;
        
        
        /// <summary>
        /// LoadContent sets the shape of the body according to aTexture, and create it in the world aWorld
        /// </summary>
        public abstract void LoadContent(World aWorld, Texture2D aTexture);

        /// <summary>
        /// Draw adds the location of of the object and its texture to the spritebatch
        /// </summary>
        public abstract void Draw(SpriteBatch aSpriteBatch, GraphicsDeviceManager aGraphicsDevice);

        /// <summary>
        /// Handles FarseerPhysics.BodyType changes for this object
        /// </summary>
        public void setBodyType(BodyType theType)
        {
            theBody.BodyType = theType;
        }
        
        /// <summary>
        /// Returns the FarseerPhysics Body
        /// </summary>
        public Body getBody()
        {
            return theBody;
        }
        /// <summary>
        /// Sets the body's friction as a float
        /// </summary>
        public void setFriction(float theFriction)
        {
            theBody.Friction = theFriction;
        }
        /// <summary>
        /// Sets the position with a Vector2 type
        /// </summary>
        public void setPosition(Vector2 thePosition)
        {
            theBody.Position = thePosition;
        }
        
        /// <summary>
        /// Sets the restitution (bounciness) of an object, expects float
        /// </summary>

        public void setRestitution(float theRestitution)
        {
            theBody.Restitution = theRestitution;
        }

        /// <summary>
        /// Sets the rotation of an object in radials, expects float
        /// </summary>
        public void setRotation(float theRotation)
        {
            theBody.Rotation = theRotation;
        }

        /// <summary>
        /// Sets the mass of the object in kilograms, expects float
        /// </summary>
        public void setMass(float theMass)
        {
            theBody.Mass = theMass;
        }
    }
}
