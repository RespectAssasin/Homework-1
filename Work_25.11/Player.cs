using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_25._11
{
    internal class Player
    {
        public string Username;
        public TimeSpan Time;
        public Player(string username, TimeSpan time) 
        {
            Username = username;
            Time = time;
        }
    }
}
