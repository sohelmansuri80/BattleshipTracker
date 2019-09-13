using System;

namespace Battleship.Core
{
    public class Square
    {
        #region Properties

        public Location Location { get; set; }
        public Ship Ship { get; set; }
        public int Hits { get; set; }

        public SquareType Type { get; set; }

        public bool IsEmpty
        {
            get
            {
                if (!IsTracker)

                    return Ship == null && Hits == 0;

                else
                {
                    return !AttackResult.HasValue;
                }
            }
        }

        public bool IsMiss
        {
            get
            {
                if (!IsTracker)
                {
                    return Ship == null && Hits > 0;
                }
                else
                {
                    return AttackResult == Core.AttackResult.Miss;
                }
            }
        }

        public bool IsOccupied
        {
            get { return Ship != null && Hits == 0; }
        }

        public bool IsHit
        {
            get
            {
                if (!IsTracker)
                {
                    return Ship != null && Hits > 0;
                }
                else
                {
                    return AttackResult == Core.AttackResult.Hit;
                }
            }
        }

        public char Label
        {
            get
            {
                if (IsHit)
                    return 'X';
                if (IsOccupied && !IsTracker)
                    return Ship.Name[0];
                if (IsMiss)
                    return 'M';
                return '.';
            }
        }

        public bool IsRandom
        {
            get
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int n = rand.Next(1, Configuration.Rows * Configuration.Columns + 1);
                return n % 2 == 0;
            }
        }

        public Quadrant Quadrant
        {
            get { return LocationHelper.GetQuadrant(Location.Row, Location.Column); }
        }

        public AttackResult? AttackResult { get; set; }
        public bool IsTracker { get; set; }

        #endregion

        #region Constructors

        public Square(int row, int column)
        {
            Location = new Location(row, column);
        }

        #endregion
    }
}