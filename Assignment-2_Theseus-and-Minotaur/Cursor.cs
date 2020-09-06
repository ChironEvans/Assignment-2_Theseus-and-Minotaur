namespace Assignment_2_Theseus_and_Minotaur
{
    public class Cursor
    {
        protected int yPos;
        protected int xPos;

        public Cursor(int newYPos, int newXPos)
        {
            yPos = newYPos;
            xPos = newXPos;
        }

        public void SetNewPos(int newX, int newY)
        {
            xPos = newX;
            yPos = newY;
        }

        public int GetYPos => yPos;
 
        public int GetXPos => xPos;
    }
}