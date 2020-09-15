using System;

namespace Assignment_2_Theseus_and_Minotaur
{
    public abstract class FloorTile
    {
        protected int xPos;
        protected int yPos;
        protected char image;
        public bool Top;
        public bool Right;
        public bool Bottom;
        public bool Left;
        public bool Exit;

        public FloorTile(int newY, int newX, bool[] walls)
        {
            xPos = newX;
            yPos = newY;
            Top = walls[0];
            Right = walls[1];
            Bottom = walls[2];
            Left = walls[3];
        }
        new
        public string ToString => image.ToString();

        internal Moveable OnTile { get; set; }

        internal Special TileExtra { get; set; }

        public bool Minotaur => CheckTile("M");

        public bool Theseus => CheckTile("T");

        public string SpecialType => GetTileName();

        private string GetTileName()
        {
            if (TileExtra != null)
            {
                return TileExtra.Name;
            }
            return null;
        }

        private bool CheckTile(string type)
        {
            if (OnTile == null)
            {
                return false;
            }
            else
            {
                switch (type)
                {
                    case "T":
                        return OnTile.GetType() == typeof(Theseus);
                    case "M":
                        return OnTile.GetType() == typeof(Minotaur);
                    default:
                        return false;
                }
            }
        }
    }
}