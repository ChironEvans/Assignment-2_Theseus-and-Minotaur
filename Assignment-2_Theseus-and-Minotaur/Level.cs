using System;

namespace Assignment_2_Theseus_and_Minotaur
{
    internal class Level
    {
        public string LevelName;
        protected FloorTile[,] LevelBoard;
        public int MoveCount = 0;

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
            //FetchAt(newCursor).Minotaur = true;

            newCursor = new Cursor(specials[1][0], specials[1][1]);
            //FetchAt(newCursor).Theseus = true;

             newCursor = new Cursor(specials[2][0], specials[2][1]);
            //FetchAt(newCursor).Exit = true;

        }

        public void MovePlus()
        {
            MoveCount++;
        }
        public Square FetchAt(Cursor cursor)
        {
            return (Square)LevelBoard[cursor.GetYPos,cursor.GetXPos];
        }

        public Cursor FindTheseus()
        {
            for (int i = 0; i < LevelBoard.GetLength(0); i++)
            {
                for (int j = 0; j < LevelBoard.GetLength(1); j++)
                {
                    if (LevelBoard[i,j].Theseus)
                    {
                        return new Cursor(i, j);
                    }
                }
            }
            throw new TheseusNotExist();
        }

        public Cursor FindMinotaur()
        {
            for (int i = 0; i < LevelBoard.GetLength(0); i++)
            {
                for (int j = 0; j < LevelBoard.GetLength(1); j++)
                {
                    if (LevelBoard[i, j].Minotaur)
                    {
                        return new Cursor(i, j);
                    }
                }
            }
            return new Cursor(-1, -1);
        }

        public bool MoveTarget(Cursor targetPos, Moves dir)
        {
            switch (dir)
            {
                case Moves.UP:
                    return TryMoveUP(targetPos, dir);
                case Moves.DOWN:
                    return TryMoveDOWN(targetPos, dir);
                case Moves.LEFT:
                    return TryMoveLEFT(targetPos, dir);
                case Moves.RIGHT:
                    return TryMoveRIGHT(targetPos, dir);
            }
            return false;
        }

        public bool TryMoveUP(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos - 1, currentPos.GetXPos);
            return DoMove(currentPos, targetPointer, dir);
        }

        public bool TryMoveDOWN(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos + 1, currentPos.GetXPos);
            return DoMove(currentPos, targetPointer, dir);

        }

        public bool TryMoveLEFT(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos, currentPos.GetXPos - 1);
            return DoMove(currentPos, targetPointer, dir);
        }

        public bool TryMoveRIGHT(Cursor currentPos, Moves dir)
        {
            Cursor targetPointer = new Cursor(currentPos.GetYPos, currentPos.GetXPos + 1);
            return DoMove(currentPos, targetPointer, dir);
        }

        private bool DoMove(Cursor currentPos, Cursor targetCursor, Moves dir)
        {
            if (!CheckCollision(currentPos, targetCursor, dir))
            {
                return false;
            }
            FloorTile currentTile = FetchAt(currentPos);
            FloorTile moveTile = FetchAt(targetCursor);

            Moveable toMove = currentTile.OnTile;

            moveTile.OnTile = toMove;
            currentTile.OnTile = null;
            return true;
        }

        internal bool CheckCollision(Cursor currentPos, Cursor targetCursor, Moves dir)
        {
            bool moveImpeded = true;
            FloorTile currentTile = FetchAt(currentPos);
            FloorTile moveTile = FetchAt(targetCursor);
            if (currentPos.GetXPos - targetCursor.GetXPos != 0)
            {
                switch (currentPos.GetXPos - targetCursor.GetXPos)
                {
                    // Moving LtR
                    case -1:
                        if (currentTile.Right == false && moveTile.Left == false)
                        {
                            moveImpeded = false;
                        }
                        break;
                    // Moving RtL
                    case 1:
                        if (currentTile.Left == false && moveTile.Right == false)
                        {
                            moveImpeded = false;
                        }
                        break;
                }
            }
            else
            {
                switch (currentPos.GetYPos - targetCursor.GetYPos)
                {
                    // Moving Up
                    case -1:
                        if (currentTile.Top == false && moveTile.Bottom == false)
                        {
                            moveImpeded = false;
                        }
                        break;
                    // Moving Down
                    case 1:
                        if (currentTile.Bottom == false && moveTile.Top == false)
                        {
                            moveImpeded = false;
                        }
                        break;
                }
            }
            return moveImpeded;

        }
    }
}