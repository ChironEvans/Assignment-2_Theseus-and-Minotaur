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
        public bool Minotaur;
        public bool Theseus;
        public bool Exit;

        public FloorTile(int newX, int newY, bool[] walls)
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
    }
}