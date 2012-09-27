using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.IO;
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
    /// Creates objects in specifiedlocations according to the passed text file
    /// Expects the format: 
    /// blocktype, bodytype, texture name, position.x, position.y, rotation
    /// The first line should contain an integer with the number of objects to be read
    /// </summary>
    class LevelBuilder
    {
        private ArrayList objectList = new ArrayList();
        string[][] levelInfo;
        string[] lineArray;
        ContentManager Content;
        World theWorld;
        const String fallBackTexture = "Crate";

        public LevelBuilder(ContentManager aManger, World aWorld)
        {
            Content = aManger;
            theWorld = aWorld;
        }
        /// <summary>
        /// Reads the level from a textfile into a multidimensional array
        /// Every row is an object, every column is a property
        /// </summary>
        
        public void readLevel(String levelname)
        {
            try
            {
                StreamReader sr = new StreamReader(levelname);
                string wholeFile = sr.ReadToEnd();
                sr.Close();
                lineArray = wholeFile.Split(new char[] { '\n' });
                levelInfo = new string[lineArray.Length][];
                for (int i = 0; i < lineArray.Length; i++)
                {
                    levelInfo[i] = lineArray[i].Split(',');
                }
                                
            } catch (Exception e)
            {
                Console.WriteLine("Could not read levelFile " + Directory.GetCurrentDirectory() + "\\" +  levelname);
                Console.WriteLine(e.Message);
                return;
            }
            for (int i = 0; i < levelInfo.Length; i++)
            {
                GameObject tempObject = buildFromLine(levelInfo[i]);
                if (tempObject != null)
                {
                    objectList.Add(tempObject);
                }
                
            }

        }

        /// <summary>
        /// Uses the current line to read the properties the object needs, and sets the for the object 
        /// </summary>
        private GameObject buildFromLine(String[] line)
        {
            if (line.Length == 6)
            {
                GameObject tempObject = setBlockType(line[0]);
                tempObject.LoadContent(theWorld, setTexture(line[1]));
                tempObject.setBodyType(setBodyType(line[2]));
                tempObject.setPosition(setPosition(line[3], line[4]));
                tempObject.setRotation(setRotation(line[5]));
                return tempObject;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Parses and returns the rotation of an object
        /// Default is zero
        /// </summary>
        private float setRotation(string rotationString)
        {
            float rotationFloat;
            if (!float.TryParse(rotationString, out rotationFloat))
            {
                rotationFloat = 0.0f;
                Console.WriteLine("Failed to parse rotation float:" + rotationString);
            }
            return rotationFloat;
        }

        /// <summary>
        /// Reads the X and Y values for a position
        /// Then builds a Vector2 type and returns it
        /// Defaults to (0, 0)
        /// </summary>
        private Vector2 setPosition(string xString, string yString)
        {
            float x;
            float y;

            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            xString = xString.Replace('.', ci.NumberFormat.CurrencyDecimalSeparator.ToCharArray()[0]);
            yString = yString.Replace('.', ci.NumberFormat.CurrencyDecimalSeparator.ToCharArray()[0]);

            if (!float.TryParse(xString, out x))
            {
                x = 0.0f;
                Console.WriteLine("Failed to parse x float:" + xString);
            }
            if (!float.TryParse(yString, out y))
            {
                y = 0.0f;
                Console.WriteLine("Failed to parse y float:" + yString);
            }
            return new Vector2(x,y);
        }

        /// <summary>
        /// Tries to load the texture passed to it
        /// If this fails it loads fallBackTexture, a texture that should alwys exist to show failure
        /// </summary>
        private Texture2D setTexture(string textureName)
        {
            try
            {
                return Content.Load<Texture2D>(textureName);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading texture");
                Console.WriteLine(e.Message);
                return Content.Load<Texture2D>(fallBackTexture);
            }
        }

        /// <summary>
        /// Reads the bodytype that should be set, defaults to a dynamic bodytype
        /// </summary>
        private BodyType setBodyType(string bodyTypeString)
        {
            if (bodyTypeString.Trim().ToLower().Equals("static"))
            {
                return BodyType.Static;
            }
            else
            {
                return BodyType.Dynamic;
            }
        }

        /// <summary>
        /// Sets the type of block that should be built, thos are constructed using the GameObject class
        /// </summary>
        private GameObject setBlockType(string blockType)
        {
            
            if (blockType.Trim().ToLower().Equals("woodblock"))
            {
                return new RectangleObject();
            }
            else if (blockType.Trim().ToLower().Equals("birdblock"))
            {
                return new CircleObject();
            }
            else
            {
                return new RectangleObject();
            }
        }
        /// <summary>
        /// Returns an arraylist with all the objects needed for this level
        /// </summary>
        public ArrayList getLevelObjects()
        {
            return objectList;
        }
    }
}
