using System;

namespace TicTacToe
{
    public class TicTacToeLogic
    {
        private string[] _field;
        private string _currentPlayer;
        private bool _gameEnd;
        private string _winner;
        private Random random;
        private int _nextAIMove;

        public string[] Field { get { return _field; } }
        public string CurrentPlayer { get { return _currentPlayer; } }
        public bool GameOver { get { return _gameEnd; } }
        public string Winner { get { return _winner; } }

        public TicTacToeLogic()
        {
            _field = new string[9];
            for (int i = 0; i < 9; i++)
            {
                _field[i] = "";
            }
            _currentPlayer = "X";
            _gameEnd = false;
            _winner = "";
            random = new Random();
            _nextAIMove = 0;
        }

        public void MakeMove(int x, int y)
        {
            int index = x * 3 + y;
            if (!_gameEnd && index >= 0 && index < 9 && _field[index] == "")
            {
                _field[index] = _currentPlayer;
                CheckWinCondition();
                if (!_gameEnd)
                {
                    _currentPlayer = (_currentPlayer == "X") ? "O" : "X";
                }
            }
        }

        public void MakeAiMove(int difficulty, string aiSymbol)
        {
            if (_gameEnd) return;
            if (_currentPlayer != aiSymbol) return;
            if (difficulty == 1)
            {
                AiRandom();
            }
            else if (difficulty == 2)
            {
                _nextAIMove++;
                if (_nextAIMove % 2 == 0)
                {
                    AiRandom();
                }
                else
                {
                    AiMinimaxMove(aiSymbol);
                }
            }
            else
            {
                AiMinimaxMove(aiSymbol);
            }
        }

        private void AiRandom()
        {
            while (true)
            {
                int index = random.Next(0, 9);
                if (_field[index] == "")
                {
                    _field[index] = _currentPlayer;
                    CheckWinCondition();
                    if (!_gameEnd)
                    {
                        _currentPlayer = (_currentPlayer == "X") ? "O" : "X";
                    }
                    break;
                }
            }
        }

        private void AiMinimaxMove(string aiSymbol)
        {
            int bestMoveIndex = GetBestMoveIndex(aiSymbol);
            if (bestMoveIndex >= 0 && bestMoveIndex < 9 && _field[bestMoveIndex] == "")
            {
                _field[bestMoveIndex] = _currentPlayer;
                CheckWinCondition();
                if (!_gameEnd)
                {
                    _currentPlayer = (_currentPlayer == "X") ? "O" : "X";
                }
            }
            else
            {
                AiRandom();
            }
        }

        private int GetBestMoveIndex(string aiSymbol)
        {
            int bestScore = int.MinValue;
            int bestMove = -1;
            for (int i = 0; i < 9; i++)
            {
                if (_field[i] == "")
                {
                    _field[i] = aiSymbol;
                    int score = Minimax(0, false, aiSymbol);
                    _field[i] = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }
            return bestMove;
        }

        private int Minimax(int depth, bool isMaximizing, string aiSymbol)
        {
            string result = CheckWinnerForMinimax();
            if (result == aiSymbol) return 10 - depth;
            else if (result == GetOpponentSymbol(aiSymbol)) return depth - 10;
            else if (result == "Draw") return 0;
            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (_field[i] == "")
                    {
                        _field[i] = aiSymbol;
                        int score = Minimax(depth + 1, false, aiSymbol);
                        _field[i] = "";
                        bestScore = Math.Max(bestScore, score);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                string opponent = GetOpponentSymbol(aiSymbol);
                for (int i = 0; i < 9; i++)
                {
                    if (_field[i] == "")
                    {
                        _field[i] = opponent;
                        int score = Minimax(depth + 1, true, aiSymbol);
                        _field[i] = "";
                        bestScore = Math.Min(bestScore, score);
                    }
                }
                return bestScore;
            }
        }

        private string CheckWinnerForMinimax()
        {
            int[][] lines = new int[][]
            {
                new int[] {0,1,2}, new int[] {3,4,5}, new int[] {6,7,8},
                new int[] {0,3,6}, new int[] {1,4,7}, new int[] {2,5,8},
                new int[] {0,4,8}, new int[] {2,4,6}
            };
            for (int i = 0; i < lines.Length; i++)
            {
                int a = lines[i][0];
                int b = lines[i][1];
                int c = lines[i][2];
                if (_field[a] != "" && _field[a] == _field[b] && _field[b] == _field[c])
                {
                    return _field[a];
                }
            }
            bool isDraw = true;
            for (int i = 0; i < 9; i++)
            {
                if (_field[i] == "")
                {
                    isDraw = false;
                    break;
                }
            }
            if (isDraw) return "Draw";
            return null;
        }

        private void CheckWinCondition()
        {
            int[][] lines = new int[][]
            {
                new int[] {0,1,2}, new int[] {3,4,5}, new int[] {6,7,8},
                new int[] {0,3,6}, new int[] {1,4,7}, new int[] {2,5,8},
                new int[] {0,4,8}, new int[] {2,4,6}
            };
            for (int i = 0; i < lines.Length; i++)
            {
                int a = lines[i][0];
                int b = lines[i][1];
                int c = lines[i][2];
                if (_field[a] != "" && _field[a] == _field[b] && _field[b] == _field[c])
                {
                    _gameEnd = true;
                    _winner = _field[a];
                    return;
                }
            }
            bool isDraw = true;
            for (int i = 0; i < 9; i++)
            {
                if (_field[i] == "")
                {
                    isDraw = false;
                    break;
                }
            }
            if (isDraw)
            {
                _gameEnd = true;
                _winner = "Draw";
            }
        }

        public void ResetGame()
        {
            for (int i = 0; i < 9; i++)
            {
                _field[i] = "";
            }
            _currentPlayer = "X";
            _gameEnd = false;
            _winner = "";
            _nextAIMove = 0;
        }

        private string GetOpponentSymbol(string symbol)
        {
            return (symbol == "X") ? "O" : "X";
        }
    }
}
