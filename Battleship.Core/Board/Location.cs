namespace Battleship.Core
{
    public class Location
    {
        public Location(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Column { get; set; }

        public int Row { get; set; }
    }
}