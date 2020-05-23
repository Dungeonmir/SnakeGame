using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Circle()
        {
            //Эта функция восстанавливает первоначальные значения x и y  
            X = 0;
            Y = 0;
        }
    }
}
