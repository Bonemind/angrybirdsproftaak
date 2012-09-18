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
    abstract class GameObject
    {
        public Texture2D objectTexture;
        public Body theBody;
        public abstract void LoadContent(World aWorld, Texture2D aTexture);
        public abstract void Draw(SpriteBatch aSpriteBatch, GraphicsDeviceManager aGraphicsDevice);
        public void setBodyType(BodyType theType)
        {
            theBody.BodyType = theType;
        }

        public Body getBody()
        {
            return theBody;
        }

        public void setFriction(float theFriction)
        {
            theBody.Friction = theFriction;
        }
        
        public void setPosition(Vector2 thePosition)
        {
            theBody.Position = thePosition;
        }

        public void setRestitution(float theRestitution)
        {
            theBody.Restitution = theRestitution;
        }

        public void setRotation(float theRotation)
        {
            theBody.Rotation = theRotation;
        }
        
        public void setMass(float theMass)
        {
            theBody.Mass = theMass;
        }
    }
}
