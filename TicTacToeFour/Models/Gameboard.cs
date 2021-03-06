﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TicTacToeFour.Presentation;

namespace TicTacToeFour.Models
{
    public class Gameboard : ObservableObject
    {
        #region ENUMS

        public const string PLAYER_PIECE_X = "X";
        public const string PLAYER_PIECE_O = "O";
        public const string PLAYER_PIECE_NONE = "";

        public enum GameboardState
        {
            NewRound,
            PlayerXTurn,
            PlayerOTurn,
            PlayerXWin,
            PlayerOWin,
            CatsGame
        }

        #endregion

        #region FIELDS

        private const int MAX_NUM_OF_ROWS_COLUMNS = 4;

        private string[][] _currentBoard;

        #endregion

        #region PROPERTIES

        public int MaxNumOfRowsColumns
        {
            get { return MAX_NUM_OF_ROWS_COLUMNS; }
        }

        public string[][] CurrentBoard
        {
            get { return _currentBoard; }
            set
            {
                _currentBoard = value;
                OnPropertyChanged(nameof(CurrentBoard));
            }
        }

        public GameboardState CurrentRoundState { get; set; }
        #endregion

        #region CONSTRUCTORS

        public Gameboard()
        {
            CurrentBoard = new string[4][];
            CurrentBoard[0] = new string[4];
            CurrentBoard[1] = new string[4];
            CurrentBoard[2] = new string[4];
            CurrentBoard[3] = new string[4];

            InitializeGameboard();
        }

        #endregion

        #region METHODS
        public void InitializeGameboard()
        {
            CurrentRoundState = GameboardState.NewRound;

            for (int row = 0; row < MAX_NUM_OF_ROWS_COLUMNS; row++)
            {
                for (int column = 0; column < MAX_NUM_OF_ROWS_COLUMNS; column++)
                {
                    CurrentBoard[row][column] = PLAYER_PIECE_NONE;
                }
            }
        }

        public bool GameboardPositionAvailable(GameboardPosition gameboardPosition)
        {

            if (CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] == PLAYER_PIECE_NONE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateGameboardState()
        {
            if (FourInARow(PLAYER_PIECE_X))
            {
                CurrentRoundState = GameboardState.PlayerXWin;
            }

            else if (FourInARow(PLAYER_PIECE_O))
            {
                CurrentRoundState = GameboardState.PlayerOWin;
            }

            else if (IsCatsGame())
            {
                CurrentRoundState = GameboardState.CatsGame;
            }
        }

        public bool IsCatsGame()
        {

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (CurrentBoard[row][column] == PLAYER_PIECE_NONE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool FourInARow(string playerPieceToCheck)
        {
            for (int row = 0; row < 4; row++)
            {
                if (CurrentBoard[row][0] == playerPieceToCheck &&
                    CurrentBoard[row][1] == playerPieceToCheck &&
                    CurrentBoard[row][2] == playerPieceToCheck &&
                    CurrentBoard[row][3] == playerPieceToCheck)
                {
                    return true;
                }
            }

            for (int column = 0; column < 3; column++)
            {
                if (CurrentBoard[0][column] == playerPieceToCheck &&
                    CurrentBoard[1][column] == playerPieceToCheck &&
                    CurrentBoard[2][column] == playerPieceToCheck &&
                    CurrentBoard[3][column] == playerPieceToCheck)
                {
                    return true;
                }
            }

            if (
                (CurrentBoard[0][0] == playerPieceToCheck &&
                 CurrentBoard[1][1] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck
                 )
                ||
                (CurrentBoard[0][3] == playerPieceToCheck &&
                 CurrentBoard[1][2] == playerPieceToCheck &&
                 CurrentBoard[2][1] == playerPieceToCheck &&
                 CurrentBoard[3][0] == playerPieceToCheck)
                )
            {
                return true;
            }

            return false;
        }

        public void SetPlayerPiece(GameboardPosition gameboardPosition, string PlayerPiece)
        {

            CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] = PlayerPiece;

            SetNextPlayer();

        }

        private void SetNextPlayer()
        {
            if (CurrentRoundState == GameboardState.PlayerXTurn)
            {
                CurrentRoundState = GameboardState.PlayerOTurn;
            }
            else
            {
                CurrentRoundState = GameboardState.PlayerXTurn;
            }
        }

        #endregion
    }
}
