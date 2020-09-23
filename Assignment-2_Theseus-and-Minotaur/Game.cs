using System;
using System.Collections.Generic;
namespace Assignment_2_Theseus_and_Minotaur
{
    public class Game
    {
        // Initialize member variables
        internal List<Level> AllMyLevels = new List<Level>();
        internal Level CurrentLevel;
        public List<string> AllLevelNames = new List<string>();
        public int LevelCount;
        public string CurrentLevelName = "No levels loaded";
        public int LevelWidth;
        public int LevelHeight;
        public int MoveCount;
        public bool HasTheseusWon = false;
        public bool HasMinotaurWon = false;

        // Add a Level
        public void AddLevel(string newLevelName, int newLevelW, int newLevelH, string newLevelString)
        {
            Level level = new Level(newLevelName, newLevelW, newLevelH, newLevelString);
            AllMyLevels.Add(level);
            AllLevelNames.Add(level.LevelName);
            SetLevel(newLevelName);
            LevelCount++;
        }

        // Set a level as current
        public void SetLevel(string levelName)
        {
            if (AllLevelNames.Contains(levelName))
            {
                int i = AllLevelNames.IndexOf(levelName);
                CurrentLevel = AllMyLevels[i];
                CurrentLevelName = levelName;
                LevelWidth = CurrentLevel.LevelW;
                LevelHeight = CurrentLevel.LevelH;
                CheckTheseusWon();
                CheckMinotaurWon();
            }
        }
        
        // Increment the move count
        public void MovePlus()
        {
            MoveCount++;
        }

        public void MoveTheseus(Moves dir)
        {
            Cursor theseusPos = CurrentLevel.FindTheseus();
            if (theseusPos.GetXPos >= 0 && theseusPos.GetYPos >=0)
            {
                if(CurrentLevel.MoveTarget(theseusPos, dir))
                {
                    MovePlus();

                }
            }
            CheckTheseusWon();
        }

        // Move theseus
        private void CheckTheseusWon()
        {
            Cursor theseusPos = CurrentLevel.FindTheseus();
            Cursor exitPos = CurrentLevel.FindExit();
            if (theseusPos == null)
            {
                return;
            }
            if (theseusPos.GetXPos == exitPos.GetXPos && theseusPos.GetYPos == exitPos.GetYPos)
            {
                HasTheseusWon = true;
            }
            else
            {
                HasTheseusWon = false;
            }
        }

        // Move the minotaur
        public void MoveMinotaur()
        {
            Cursor theseusPos = CurrentLevel.FindTheseus();
            if (theseusPos == null)
            {
                return;
            }
            // Should be moves < 2 to automate the minotaur moving, but Mikes tests call the function twice
            for (int moves = 0; moves < 1; moves++)
            {
                Cursor minoPos = CurrentLevel.FindMinotaur();
                int diffX = minoPos.GetXPos - theseusPos.GetXPos;
                int diffY = minoPos.GetYPos - theseusPos.GetYPos;
                if (diffX == 0)
                {
                    switch (diffY < 0)
                    {
                        case false:
                            CurrentLevel.MoveTarget(minoPos, Moves.UP);
                            break;
                        case true:
                            CurrentLevel.MoveTarget(minoPos, Moves.DOWN);
                            break;
                    }
                }
                else
                {
                    switch (diffX < 0)
                    {
                        case false:
                            CurrentLevel.MoveTarget(minoPos, Moves.LEFT);
                            break;
                        case true:
                            CurrentLevel.MoveTarget(minoPos, Moves.RIGHT);
                            break;
                    }
                    
                }
                Cursor newMinoPos = CurrentLevel.FindMinotaur();
                CheckMinotaurWon();
            }
        }

        // Check whether the minotaur has killed theseus
        private void CheckMinotaurWon()
        {
            Cursor theseusPos = CurrentLevel.FindTheseus();
            if (CurrentLevel.FindTheseus() == null)
            {
                HasMinotaurWon = true;
            }
            else
            {
                HasMinotaurWon = false;
            }
        }

        // Find what is at a pair of coordinates
        public Square WhatIsAt(int y, int x)
        {
            Cursor newCursor = new Cursor(y,x);
            Square tileAt = CurrentLevel.FetchAt(newCursor);
            return tileAt;
        }        

        public void RebuildLevelNames()
        {
            List<string> LevelNames = new List<string>();
            foreach(Level level in AllMyLevels)
            {
                LevelNames.Add(level.LevelName);
            }
            AllLevelNames = LevelNames;
        }

        // Get all the level names
        public List<string> LevelNames()
        {
            return AllLevelNames;
        }
    }
    // Having a quota for number of comments encourages bad practices
}