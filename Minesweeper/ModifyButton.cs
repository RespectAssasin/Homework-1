using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    internal class ModifyButton : Button
    {
        public bool IsNumber = false;
        public bool IsMine = false;
        public bool IsNone = true;
        public int row = 0;
        public int col = 0;
        public void ToMine()
        {
            this.IsMine = true;
            this.IsNone = false;
        }
        public void ToNumber()
        {
            this.IsNumber = true;
            this.IsNone = false;
        }
        public void DeActive()
        {
            this.IsEnabled = false;
        }
    }
}
