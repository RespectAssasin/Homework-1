using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class TicTacToeLogic
    {
        private char[][] arr = new char[3][];

        public TicTacToeLogic() { }

        public void AddToArray(int x, int y, char sym)
        {
            arr[x][y] = sym;
        }
        public void ResetArray()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = '\0';
                }
            }
        }
    }
}
