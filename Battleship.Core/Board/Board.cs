using System.Collections.Generic;
using System.Linq;

namespace Battleship.Core
{
    /// <summary>
    /// Base Board class
    /// </summary>
    public abstract class Board
    {
        #region Properties

        public List<Square> Squares { get; set; }

        public int Rows
        {
            get { return Configuration.Rows; }
        }

        public int Columns
        {
            get { return Configuration.Columns; }
        }

        #endregion

        #region Constructors

        protected Board(bool isTracker)
        {
            InitializeBoard(isTracker);
        }

        #endregion

        #region Methods

        private void InitializeBoard(bool IsTracker = false)
        {
            Squares = new List<Square>();
            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= Columns; j++)
                {
                    Square square = new Square(i, j);
                    square.IsTracker = IsTracker;
                    Squares.Add(square);
                }
            }
        }

        public List<Square> GetNeighbours(Location location)
        {
            int row = location.Row;
            int column = location.Column;
            List<Square> neighbours = new List<Square>();
            if (row > 1)
            {
                neighbours.Add(Squares.At(row - 1, column));
            }

            if (column < Configuration.Columns)
            {
                neighbours.Add(Squares.At(row, column + 1));
            }

            if (column > 1)
            {
                neighbours.Add(Squares.At(row, column - 1));
            }

            if (row < Configuration.Rows)
            {
                neighbours.Add(Squares.At(row + 1, column));
            }

            return neighbours;
        }

        #endregion
    }

    /// <summary>
    /// Game Board Class
    /// </summary>
    public class GameBoard : Board
    {
        public GameBoard(bool isTracker) : base(false)
        {
        }
    }

    /// <summary>
    /// TrackerBoard class to keep track of state
    /// </summary>
    public class TrackerBoard : Board
    {
        public List<Location> GetAffectedNeighbours()
        {
            List<Square> squares = new List<Square>();
            var hits = Squares.Where(x => x.IsHit).ToList();
            foreach (var hit in hits)
            {
                squares.AddRange(GetNeighbours(hit.Location));
            }

            return squares.Distinct().Where(x => x.IsEmpty).Select(x => x.Location)
                .ToList();
        }

        public List<Location> GetRandomLocations()
        {
            return Squares.Where(x => x.IsEmpty && x.IsRandom)
                .Select(x => x.Location).ToList();
            // TODO - Need to make following logic iterative till most hit Quadrant with empty Squares is found.
            //var quadrant = GetMostSucessfulQuadrant();
            //if (quadrant == Quadrant.Error)
            //{
            //    return Squares.Where(x => x.OccupancyType == OccupancyType.Nothing && x.IsRandom)
            //        .Select(x => x.Location).ToList();
            //}
            //else
            //{
            //    Console.WriteLine($"Quadrant: {quadrant}");
            //    if (!GetLocationsByQuadrantQuery(quadrant).Any())
            //    {
            //        if (quadrant > Quadrant.First)
            //        {
            //            quadrant -= 1;
            //        }

            //        if (quadrant < Quadrant.Fourth)
            //        {
            //            quadrant += 1;
            //        }
            //    }
            //    return GetLocationsByQuadrantQuery(quadrant).ToList();
        }

        /// <summary>
        /// Get Quadrant with most number of hits
        /// </summary>
        /// <returns></returns>
        public Quadrant GetMostSucessfulQuadrant()
        {
            var result = Squares
                .Where(x => x.IsHit)
                .GroupBy(g => g.Quadrant)
                .Select(g => new
                {
                    Quadrant = g.Key,
                    Count = g.Count()
                })
                .Where(x => x.Count > 0)
                .OrderByDescending(x => x.Count)
                .Select(x => x.Quadrant)
                .FirstOrDefault();


            return result;
        }

        public List<Location> GetLocationsByQuadrantQuery(Quadrant quadrant)
        {
            return Squares.Where(x => x.Quadrant == quadrant && x.IsEmpty)
                .Select(x => x.Location).ToList();
        }

        public TrackerBoard(bool isTracker) : base(isTracker)
        {
        }
    }
}