using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Core
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player
    {
        #region Properties

        public string Name { get; set; }

        public bool HasLost
        {
            get { return Ships.All(x => x.IsSunk); }
        }

        public GameBoard GameBoard { get; set; }
        public TrackerBoard TrackerBoard { get; set; }

        public List<Ship> Ships { get; set; }

        public int TotalShots { get; set; }
        public int SuccessfulShots { get; set; }

        public int CalculatedShots { get; set; }
        public int RandomShots { get; set; }

        public double AISuccessRatio
        {
            get { return (double)SuccessfulShots / CalculatedShots; }
        }

        #endregion

        #region Constructors

        public Player(string name)
        {
            Name = name;
            Ships = new List<Ship>
            {
                new Submarine(),
                new Cruiser(),
                new AircraftCarrier(),
                new Battleship(),
                new Destroyer()
            };
            GameBoard = new GameBoard(false);
            TrackerBoard = new TrackerBoard(true);
        }

        #endregion

        #region Methods

        // TODO - Avoid ships touching each other
        public void DeployShips()
        {
            foreach (var ship in Ships)
            {
                bool flag = true;
                while (flag)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int startRow = rand.Next(1, Configuration.Rows + 1);
                    int startCol = rand.Next(1, Configuration.Columns + 1);
                    int endRow = startRow;
                    int endCol = startCol;

                    // Randomly pick orientation of ship
                    bool isHorizontal = rand.Next(1, 101) % 2 == 0;
                    for (int i = 1; i < ship.Size; i++)
                    {
                        if (isHorizontal)
                        {
                            endCol++;
                        }
                        else
                        {
                            endRow++;
                        }
                    }

                    // If end rows or columns exceed boundaries then try again
                    if (endRow > Configuration.Rows || endCol > Configuration.Columns)
                    {
                        flag = true;
                        continue;
                    }

                    List<Square> deploymentSqaures = GetDeploymentSquares(startRow, startCol, endRow, endCol);
                    // if any of the squares are occupied then try again
                    if (deploymentSqaures.Any(x => x.IsOccupied))
                    {
                        flag = true;
                        continue;
                    }

                    // Don't let ships to touch each other
                    if (Configuration.Difficulty == Difficulty.Medium)
                    {
                        // Check if there are any adjacent ships
                        if (deploymentSqaures.Any(x =>
                            GameBoard.GetNeighbours(x.Location)
                                .Any(y => y.IsOccupied && y.Ship.Name == ship.Name)))
                        {
                            flag = true;
                            continue;
                        }
                    }

                    if (Configuration.Difficulty == Difficulty.Hard)
                    {
                        if (deploymentSqaures.Any(x =>
                            GameBoard.GetNeighbours(x.Location)
                                .Any(y => GameBoard.GetNeighbours(y.Location).Any(z => z.IsOccupied))))
                        {
                            flag = true;
                            continue;
                        }
                    }

                    foreach (var square in deploymentSqaures)
                    {
                        square.Ship = ship;
                    }

                    flag = false;
                }
            }
        }

        private List<Square> GetDeploymentSquares(int startRow, int startCol, int endRow, int endCol)
        {
            return GameBoard.Squares.Where(x => x.Location.Row >= startRow
                                                && x.Location.Column >= startCol
                                                && x.Location.Row <= endRow
                                                && x.Location.Column <= endCol)
                .ToList();
        }

        /// <summary>
        /// Displays boards for the player
        /// </summary>
        /// <returns></returns>
        public string DisplayBoard()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(Environment.NewLine);
            sb.Append("Board                                            Tracker");
            sb.Append(Environment.NewLine);
            for (int i = 1; i <= Configuration.Rows; i++)
            {
                for (int j = 1; j <= Configuration.Columns; j++)
                {
                    Square square = GameBoard.Squares.At(i, j);
                    sb.Append(square.Label);
                    sb.Append(' ');
                }

                sb.Append("                              ");
                for (int j = 1; j <= Configuration.Columns; j++)
                {
                    Square square = TrackerBoard.Squares.At(i, j);
                    sb.Append(square.Label);
                    sb.Append(' ');
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Automatic Fire method
        /// </summary>
        /// <returns></returns>
        public Location Fire()
        {
            TotalShots++;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            // TODO - Check for calculated shot
            List<Location> affectedNeighbours = TrackerBoard.GetAffectedNeighbours();
            if (affectedNeighbours.Any())
            {
                int neighbourKey = rand.Next(affectedNeighbours.Count);
                //Console.WriteLine($"{Name}: Calculated Shot!");
                CalculatedShots++;
                return affectedNeighbours[neighbourKey];
            }
            else // If calculate shot is not available create a random shot at a panel not hit before
            {
                List<Location> notHitLocations = TrackerBoard.GetRandomLocations();
                int locationKey = rand.Next(notHitLocations.Count);
                //Console.WriteLine($"{Name}: Random Shot");
                RandomShots++;
                return notHitLocations[locationKey];
            }
        }

        public AttackResult ProcessAttack(Location location)
        {
            Square square = GameBoard.Squares.At(location.Row, location.Column);
            if (square.IsOccupied)
            {
                var ship = Ships.FirstOrDefault(x => x.Name == square.Ship.Name);
                ship.Hits++;
                square.Hits++;
                Console.WriteLine($"{Name}: I've been HIT at ({location.Row}, {location.Column})");
                if (ship.IsSunk)
                {
                    Console.WriteLine($"{Name} says my {ship.Name} SUNK!!!");
                }

                return AttackResult.Hit;
            }
            else
            {
                Console.WriteLine($"{Name}: Ha ha! You MISSED it!");
                square.Hits++;
                return AttackResult.Miss;
            }
        }

        public void RecordAttackResult(Location location, AttackResult result)
        {
            var square = TrackerBoard.Squares.At(location.Row, location.Column);
            square.AttackResult = result;
            square.Hits++;
            if (result == AttackResult.Hit)
            {
                SuccessfulShots++;
            }
        }

        #endregion
    }
}