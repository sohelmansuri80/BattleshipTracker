namespace Battleship.Core
{
    /// <summary>
    /// To get some extra information regarding a given location
    /// </summary>
    public static class LocationHelper
    {
        public static Quadrant GetQuadrant(int row, int column)
        {
            int minRow = 1;
            int minCol = 1;
            int maxRow = Configuration.Rows;
            int maxCol = Configuration.Columns;

            if ((row >= minRow && row <= maxRow / 2) && (column >= minCol && column <= maxCol / 2))
            {
                return Quadrant.First;
            }
            if ((row >= minRow && column >= maxRow / 2) && (column >= maxCol / 2 && column <= maxCol))
            {
                return Quadrant.Second;
            }
            if ((row >= maxRow / 2 && row <= maxRow) && (column >= minCol && column <= maxCol / 2))
            {
                return Quadrant.Third;
            }
            else
            {
                return Quadrant.Fourth;
            }
        }
    }
}