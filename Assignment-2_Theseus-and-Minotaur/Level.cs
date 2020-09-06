using System;

namespace Assignment_2_Theseus_and_Minotaur
{
    internal class Level
    {
        public string LevelName;
        protected FloorTile[,] LevelBoard;

        public Level(string newLevelName, int newLevelW, int newLevelH, string newLevelString)
        {
            LevelName = newLevelName;
            LevelBoard = new FloorTile[newLevelH, newLevelW];
            FillLevel(newLevelW, newLevelH, newLevelString);
        }

        private void FillLevel(int newLevelW, int newLevelH, string newLevelString)
        {
            int[][] specials = new int[3][];
            int specialsMax = 3;
            int step = 5;
            for (int i = 0; i < specialsMax * step; i += step)
            {
                string curr = newLevelString.Substring(i, 4);
                int split1 = int.Parse(curr.Substring(0, 2));
                int split2 = int.Parse(curr.Substring(2, 2));
                int[] specialCoords = new int[2];
                specialCoords[0] = split1;
                specialCoords[1] = split2;
                specials[i/step] = specialCoords;
            }
            int currY = 0;
            for (int i = specialsMax * step - 1; i < newLevelString.Length; i += step*newLevelW)
            {
                string row = newLevelString.Substring(i, (step * newLevelW));
                for (int s = 0; s < step * newLevelW; s += step)
                {
                    string curr = row.Substring(s, 4);
                    bool[] currWalls = new bool[4];
                    char[] chars = curr.ToCharArray();
                    for (int j = 0; j < chars.Length; j++)
                    {
                        if (chars[j] == '0')
                        {
                            currWalls[j] = false;
                        }
                        if (chars[j] == '1')
                        {
                            currWalls[j] = true;
                        }
                    }
                    FloorTile newTile = new Square(currY, s / step, currWalls);
                    LevelBoard[currY,s / step] = newTile;
                }
                currY++;
            }

            Cursor newCursor;
            newCursor = new Cursor(specials[0][0], specials[0][1]);
            FetchAt(newCursor).Minotaur = true;

            newCursor = new Cursor(specials[1][0], specials[1][1]);
            FetchAt(newCursor).Theseus = true;

             newCursor = new Cursor(specials[2][0], specials[2][1]);
            FetchAt(newCursor).Exit = true;

        }

        public Square FetchAt(Cursor cursor)
        {
            return (Square)LevelBoard[cursor.GetYPos,cursor.GetXPos];
        }

        public bool MoveTarget(Cursor targetPos, Moves dir)
        {
            switch (dir)
            {
                case Moves.UP:
                    return this.tryMoveUP(targetPos, dir);
                case Moves.DOWN:
                    return this.tryMoveDOWN(targetPos, dir);
                case Moves.LEFT:
                    return this.tryMoveLEFT(targetPos, dir);
                case Moves.RIGHT:
                    return this.tryMoveRIGHT(targetPos, dir);
            }
            return false;
        }

        public bool tryMoveUP(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos - 1, currentPos.GetXPos);
            return this.DoMove(currentPos, targetPointer, dir);
        }

        public bool tryMoveDOWN(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos + 1, currentPos.GetXPos);
            return this.DoMove(currentPos, targetPointer, dir);

        }

        public bool tryMoveLEFT(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos, currentPos.GetXPos - 1);
            return this.DoMove(currentPos, targetPointer, dir);
        }

        public bool tryMoveRIGHT(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos, currentPos.GetXPos + 1);
            return this.DoMove(currentPos, targetPointer, dir);
        }

        private bool DoMove(Cursor currentPos, Cursor targetCursor, Moves dir)
        {
            return false;

        }


    }
}