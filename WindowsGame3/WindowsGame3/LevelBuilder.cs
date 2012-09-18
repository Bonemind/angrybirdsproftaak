using System;
using System.Collections.Generic;
using System.Collections;
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
    class LevelBuilder
    {
        private ArrayList objectList;
        string[][] levelInfo;
        string[] lineArray;
        

        public void readLevel(String levelname)
        {
            string lineHolder = "";
            int counter = 0;
            try
            {
                StreamReader sr = new StreamReader(levelname);
                string wholeFile = sr.ReadToEnd();
                lineArray = wholeFile.Split(new char[] { '\n' });
                int temp = int.Parse(lineArray[0].Trim());
                levelInfo = new string[temp][];
                levelInfo[temp] = lineArray[counter].Split(',') ;
                
            } catch (Exception e)
            {
                Console.WriteLine("Could not read levelFile " + levelname);
                Console.WriteLine(e.Message);
            }
        }

        public ArrayList getLevelObjects()
        {
            return objectList;
        }
    }
}
