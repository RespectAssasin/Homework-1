using System;

namespace TicTacToe
{
    public class TicTacToeLogic
    {
        private string[] _board;
        private string _currentPlayer;
        private bool _gameOver;
        private string _winner;

        public string[] Board { get { return _board; } }
        public string CurrentPlayer { get { return _currentPlayer; } }
        public bool GameOver { get { return _gameOver; } }
        public string Winner { get { return _winner; } }

        private Random _rnd;
        public TicTacToeLogic()
        {
            this._board = new string[9];
            for (int i = 0; i < 9; i++)
            {
                this._board[i] = "";
            }
            this._currentPlayer = "X";
            this._gameOver = false;
            this._winner = "";
            this._rnd = new Random();
        }

        public void MakeMove(int x, int y)
        {
            int index = x * 3 + y;
            if (!this._gameOver && index >= 0 && index < 9 && this._board[index] == "")
            {
                this._board[index] = this._currentPlayer;
                CheckWinCondition();
                if (!this._gameOver)
                {
                    this._currentPlayer = this._currentPlayer == "X" ? "O" : "X";
                }
            }
        }

        public void MakeAiMove(int difficulty, string aiSymbol)
        {
            if (_gameOver) return;
            if (_currentPlayer != aiSymbol) return;
            if (difficulty == 1)
            {
                AiRandom();
            }
            else if (difficulty == 2)
            {
                AiRandom();
            }
            else
            {
                AiBestMove(aiSymbol);
            }
        }


        private void AiRandom()
        {
            while (true)
            {
                int index = this._rnd.Next(0, 9);
                if (this._board[index] == "")
                {
                    this._board[index] = this._currentPlayer;
                    CheckWinCondition();
                    if (!this._gameOver)
                    {
                        this._currentPlayer = this._currentPlayer == "X" ? "O" : "X";
                    }
                    break;
                }
            }
        }
        private void AiBestMove(string aiSymbol)
        {
            bool done = TryToWinOrBlock(aiSymbol);
            if (!done)
            {
                bool blocked = TryToWinOrBlock(aiSymbol == "X" ? "O" : "X");
                if (!blocked)
                {
                    AiRandom();
                }
            }
        }
        private bool TryToWinOrBlock(string symbol)
        {
            int[][] lines = new int[][]
            {
        new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
        new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
        new int[]{0,4,8}, new int[]{2,4,6}
            };
            for (int i = 0; i < lines.Length; i++)
            {
                int a = lines[i][0];
                int b = lines[i][1];
                int c = lines[i][2];

                if (_board[a] == symbol && _board[b] == symbol && _board[c] == "")
                {
                    _board[c] = _currentPlayer;
                    CheckWinCondition();
                    if (!_gameOver)
                    {
                        _currentPlayer = _currentPlayer == "X" ? "O" : "X";
                    }
                    return true;
                }
                if (_board[a] == symbol && _board[b] == "" && _board[c] == symbol)
                {
                    _board[b] = _currentPlayer;
                    CheckWinCondition();
                    if (!_gameOver)
                    {
                        _currentPlayer = _currentPlayer == "X" ? "O" : "X";
                    }
                    return true;
                }
                if (_board[a] == "" && _board[b] == symbol && _board[c] == symbol)
                {
                    _board[a] = _currentPlayer;
                    CheckWinCondition();
                    if (!_gameOver)
                    {
                        _currentPlayer = _currentPlayer == "X" ? "O" : "X";
                    }
                    return true;
                }
            }
            return false;
        }

        private void CheckWinCondition()
        {
            int[][] lines = new int[][]
            {
                new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
                new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
                new int[]{0,4,8}, new int[]{2,4,6}
            };
            for (int i = 0; i < lines.Length; i++)
            {
                int a = lines[i][0];
                int b = lines[i][1];
                int c = lines[i][2];
                if (this._board[a] != "" &&
                    this._board[a] == this._board[b] &&
                    this._board[b] == this._board[c])
                {
                    this._gameOver = true;
                    this._winner = this._board[a];
                    return;
                }
            }
            bool isDraw = true;
            for (int i = 0; i < 9; i++)
            {
                if (this._board[i] == "")
                {
                    isDraw = false;
                    break;
                }
            }
            if (isDraw)
            {
                this._gameOver = true;
                this._winner = "Draw";
            }
        }

        public void ResetGame()
        {
            for (int i = 0; i < 9; i++)
            {
                this._board[i] = "";
            }
            this._currentPlayer = "X";
            this._gameOver = false;
            this._winner = "";
        }
    }
}
