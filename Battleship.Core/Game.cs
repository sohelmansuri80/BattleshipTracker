using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Battleship.Core
{
    public class Game
    {
        #region Properties

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Turns { get; set; }

        public bool IsOver
        {
            get { return Player1.HasLost || Player2.HasLost; }
        }

        #endregion

        #region Constructors

        public Game()
        {
            Player1 = new Player("Player1");
            Player2 = new Player("Player2");

            Player1.DeployShips();
            Player2.DeployShips();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start Game
        /// </summary>
        public void PlayTurn()
        {
            Location location = Player1.Fire();
            AttackResult result = Player2.ProcessAttack(location);
            Player1.RecordAttackResult(location, result);
            if (!Player2.HasLost)
            {
                location = Player2.Fire();
                result = Player1.ProcessAttack(location);
                Player2.RecordAttackResult(location, result);
            }

            Console.WriteLine(CheckStatus());
            Turns++;
        }

        public void PlayTillEnd()
        {
            if (!IsOver)
            {
                PlayTurn();
            }
        }

        /// <summary>
        /// On-going messages based on current status of the game
        /// </summary>
        /// <returns></returns>
        public string CheckStatus()
        {
            if (Player1.SuccessfulShots == 0 && Player2.SuccessfulShots == 0)
            {
                return $"Computer: I hope you guys get better at this";
            }

            if (Player1.SuccessfulShots > 0 && Player2.SuccessfulShots == 0)
            {
                return $"{Player1.Name}: Hey {Player2.Name}, looks like your luck is on vacation!!!";
            }
            if (Player2.SuccessfulShots > 0 && Player1.SuccessfulShots == 0)
            {
                return $"{Player2.Name}: Hey {Player1.Name}, looks like your luck is on vacation!!!";
            }
            if (Player1.SuccessfulShots > Player2.SuccessfulShots)
            {
                return $"{Player2.Name}: I need serious luck to win this game";
            }
            if (Player2.SuccessfulShots > Player1.SuccessfulShots)
            {
                return $"{Player1.Name}: I need serious luck to win this game";
            }

            if (Player1.CalculatedShots > Player2.CalculatedShots && Player1.SuccessfulShots < Player2.SuccessfulShots)
            {
                return $"{Player2.Name}: Ha ha ha!!! CALCULATED SHOTS LOL!";
            }
            if (Player2.CalculatedShots > Player1.CalculatedShots && Player2.SuccessfulShots < Player1.SuccessfulShots)
            {
                return $"{Player1.Name}: Ha ha ha!!! CALCULATED SHOTS LOL!";
            }

            return "Computer: I have nothing to say right now";
        }

        #endregion
    }
}