namespace Battleship.Core
{
    public abstract class Ship
    {
        public int Size { get; set; }
        public int Hits { get; set; }

        public bool IsSunk
        {
            get { return Hits >= Size; }
        }

        public string Name { get; set; }
    }

    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Size = 2;
        }
    }

    public class AircraftCarrier : Ship
    {
        public AircraftCarrier()
        {
            Name = "AircraftCarrier";
            Size = 7;
        }
    }

    public class Cruiser : Ship
    {
        public Cruiser()
        {
            Name = "Cruiser";
            Size = 4;
        }
    }

    public class Submarine : Ship
    {
        public Submarine()
        {
            Name = "Submarine";
            Size = 3;
        }
    }

    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Size = 4;
        }
    }
}