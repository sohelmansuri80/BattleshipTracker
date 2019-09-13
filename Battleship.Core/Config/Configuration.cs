using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Core
{
    /// <summary>
    /// Configuration class to specify grid rows and columns
    /// </summary>
    public static class Configuration
    {
        public static int Rows { get; set; } = 10;
        public static int Columns { get; set; } = 10;
        // Higher difficulty => Sparser Ship deployment
        public static Difficulty Difficulty { get; set; } = Difficulty.Easy;
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}
