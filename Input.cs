using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; // hashtable class
using System.Windows.Forms; // keys class


namespace Snake_Game
{
    class Input
    {
        private static Hashtable keyTable = new Hashtable();

        public static bool KeyPress(Keys key)
        {
            // Функция возвращает клавишу классу
            if (keyTable[key] == null)
            {
                // Возвращает false если таблица пуста
                return false;

            }
            // Возвращает true если таблица не пуста
            return (bool)keyTable[key];
        }

        public static void changeState(Keys key, bool state)
        {
            // Функция изменит состояние клавиш  и игрока
            // Имеет два аргумента Key и state
            keyTable[key] = state;
        }
    }
}
