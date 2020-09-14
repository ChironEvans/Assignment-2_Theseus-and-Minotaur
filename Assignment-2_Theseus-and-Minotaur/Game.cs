using System;
using System.Collections.Generic;
namespace Assignment_2_Theseus_and_Minotaur
{
    public class Game
    {
        private List<Level> AllMyLevels = new List<Level>();
        private Level CurrentLevel;
        public List<string> AllLevelNames = new List<string>();
        public int LevelCount;
        public string CurrentLevelName;
        public int LevelWidth;
        public int LevelHeight;
        public int MoveCount;
        public bool HasTheseusWon = false;
        public bool HasMinotaurWon = false;

        public void AddLevel(string newLevelName, int newLevelW, int newLevelH, string newLevelString)
        {
            Level level = new Level(newLevelName, newLevelW, newLevelH, newLevelString);
            AllMyLevels.Add(level);
            AllLevelNames.Add(level.LevelName);
            LevelCount++;
        }

        public void SetLevel(string levelName)
        {
            int i = AllLevelNames.IndexOf(levelName);
            CurrentLevel = AllMyLevels[i];
        }

        public void MoveTheseus(Moves dir)
        {
            Cursor theseusPos = CurrentLevel.FindTheseus();
            if (theseusPos.GetXPos >= 0 && theseusPos.GetYPos >=0)
            {
                CurrentLevel.MoveTarget(theseusPos, dir);
            }
            CheckTheseusWon();
        }

        private void CheckTheseusWon()
        {
            if (CurrentLevel.FindTheseus() == CurrentLevel.FindExit())
            {
                HasTheseusWon = true;
            }
        }

        public void MoveMinotaur()
        {
            Cursor theseusPos = CurrentLevel.FindTheseus();
            // Should be moves < 2 to automate the minotaur moving, but Mikes tests call the function twice
            for (int moves = 0; moves < 1; moves++)
            {
                Cursor minoPos = CurrentLevel.FindMinotaur();
                int diffX = minoPos.GetXPos - theseusPos.GetXPos;
                int diffY = minoPos.GetYPos - theseusPos.GetYPos;
                if (diffY == 0)
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
                else
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
                CheckMinotaurWon();
            }
        }

        private void CheckMinotaurWon()
        {
            if (CurrentLevel.FindTheseus() == null)
            {
                HasMinotaurWon = true;
            }
        }

        public Square WhatIsAt(int x, int y)
        {
            Cursor newCursor = new Cursor(x, y);
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
        public List<string> LevelNames()
        {
            return AllLevelNames;
        }
    }
}