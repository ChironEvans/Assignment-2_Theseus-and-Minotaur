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
        public bool HasTheseusWon;
        public bool HasMinotaurWon;

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

        public void MoveTheseus(Moves LEFT)
        {
            throw new NotImplementedException();
        }

        public void MoveMinotaur()
        {
            throw new NotImplementedException();
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
        public List<String> LevelNames()
        {
            return AllLevelNames;
        }
    }
}