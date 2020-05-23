using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public enum Directions
    {
        Left,
        Right,
        Up,
        Down
    };
    class Settings
    {
        
        public static int Width { get; set; } // Позваляет задать ширину как integer
        public static int Height { get; set; } // Позваляет задать длинну как integer
        public static int Speed { get; set; } // Позваляет задать скорость как integer
        public static int Score { get; set; } // Позваляет задать счет как integer
        public static int Points { get; set; } // Позваляет задать поинтс как integer
        public static bool GameOver { get; set; } // Позваляет задать окончание игры как да или нет 
        public static Directions direction { get; set; } //  Позволяет задать направление как заранее заготовленный класс
        
        public Settings()
        {
            // Задаем значения по умолчанию
            Width = 16;
            Height = 16;
            Speed = 5;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Directions.Down;
        }

    }
}
