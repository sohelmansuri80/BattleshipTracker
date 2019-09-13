using System;
using System.Collections.Generic;
using System.Text;
using Battleship.Core;

namespace Battleship.Services
{
    public class BattleshipService
    {
        private Game _game;
        private Player _winner;
        /// <summary>
        /// To Start Game
        /// </summary>
        public void StartGame()
        {
            _game = new Game();
        }

        public bool IsGameOver()
        {
            return _game.IsOver;
        }

        public string GetWinnerName()
        {
            return IsGameOver()
                ? _game.Player2.HasLost ? _game.Player1.Name : _game.Player2.Name
                : "Game is still in progress";
        }

        private Player GetWinner()
        {
            return IsGameOver() ? _game.Player2.HasLost ? _game.Player1 : _game.Player2 : null;
        }
        public string GetWinnerStatistics()
        {
            if (IsGameOver())
            {
                var winner = GetWinner();
                if (winner != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"{winner.Name} Won!");
                    sb.Append(Environment.NewLine);
                    sb.Append($"Successful/Total Shots : {winner.SuccessfulShots} / {winner.TotalShots}");
                    sb.Append(Environment.NewLine);
                    sb.Append($"Calculated Shots:{winner.CalculatedShots}");
                    return sb.ToString();
                }
                else
                {
                    return "An Error occured while retrieving winner statistics";
                }
            }
            else
            {
                return "Game is in Progress, try again later!";
            }
        }

        public string DisplayBoards()
        {
            string potentialWinner = string.Empty;
            if (!IsGameOver())
            {
                potentialWinner = "Potential Winner: " + PotentialWinner().Name;
            }
            return $"Turn: {_game.Turns}\n" +_game.Player1.DisplayBoard() + Environment.NewLine +
            _game.Player2.DisplayBoard() + Environment.NewLine + potentialWinner;
        }

        private Player PotentialWinner()
        {
            if (_game.Player1.SuccessfulShots > _game.Player2.SuccessfulShots)
            {
                return _game.Player1;
            }
            else
            {
                return _game.Player2;
            }
        }
        public void PlayTurn()
        {
            _game.PlayTurn();
        }
    }


}
