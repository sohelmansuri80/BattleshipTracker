using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Core
{
    public static class SquareHelper
    {
        /// <summary>
        /// At extention method to get a square at given location
        /// </summary>
        /// <param name="squares"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static Square At(this List<Square> squares, int row, int column)
        {
            return squares.First(x => x.Location.Row == row && x.Location.Column == column);
        }
    }
}
