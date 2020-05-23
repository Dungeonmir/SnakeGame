using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>(); //создание листа 
        private Circle food = new Circle(); // создание класса "еда"


        public Form1()
        {
            InitializeComponent();
            new Settings(); // Привязка класса настроек к форме

            gameTimer.Interval = 1000 / Settings.Speed; // Изменяет настройки скорости игры согласно установленным
            gameTimer.Tick += updateScreen; // Привязка updateScreen функции к таймеру
            gameTimer.Start();

            StartGame();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics; // Новый класс canvas
            if (Settings.GameOver == false)
            {
                Brush snakeColour;
                
                // Проверка на части змеи
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        // Покрасить голову змеи
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        // Остальная часть змеи
                        snakeColour = Brushes.Green;
                    }
                    // Отрисовка змеи и частей
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(
                            Snake[i].X * Settings.Width,
                            Snake[i].Y * Settings.Height,
                            Settings.Width, Settings.Height));
                    // Отрисовка еды для змейки
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(
                            food.X * Settings.Width,
                            food.Y * Settings.Height,
                            Settings.Width, Settings.Height));

                }
            }
            else
            {
                string gameOver = "Game Over \n" + "Final score is " + Settings.Score + "\n Press enter to restart \n";
                label3.Text = gameOver;
                label3.Visible = true;
            }


        }
        private void StartGame()
        {
            // Эта функция запускает игру

            label3.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 }; // Новая голова для змеи
            Snake.Add(head);
            label2.Text = Settings.Score.ToString(); // Показывает счет в label2

            generateFood();

        }

        private void generateFood()
        {
            int maxXpos = pbCanvas.Size.Width / Settings.Width;
            int maxYpos = pbCanvas.Size.Height / Settings.Height;
            Random rnd = new Random(); // Создаётся новый класс random
            food = new Circle { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
            // Создание еды на рандомном x и y

        }

        private void eat()
        {
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();
            generateFood();
        }

        

        private void updateScreen(object sender, EventArgs e)
        {
            // Функция обновления экрана

            if (Settings.GameOver == true)
            {
                // Если игра завершена и игрок нажимает enter
                // Игра начинается заново

                if (Input.KeyPress(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                // Если игра не окончена действуют следующие команды:
                if (Input.KeyPress(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                if (Input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                if (Input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                if (Input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }

                movePlayer();
            }
            pbCanvas.Invalidate(); // обновляет picture box и графику в нём.
        }
        
        private void movePlayer()
        {
            // Главный луп для змейки и ее частей
            for (int i = Snake.Count - 1; i>= 0; i--)
            {
                // Если голова существует
                if (i == 0)
                {
                    //Двигает остальное тело согласно направлению движения
                    switch (Settings.direction)
                    {
                        case Directions.Right:
                            Snake[i].X++;
                            break;
                        case Directions.Left:
                            Snake[i].X--;
                            break;
                        case Directions.Up:
                            Snake[i].Y--;
                            break;
                        case Directions.Down:
                            Snake[i].Y++;
                            break;
                    }


                    // Не позволяет змейки выйти за рамки экрана
                    int maxXpos = pbCanvas.Size.Width / Settings.Width;
                    int maxYpos = pbCanvas.Size.Height / Settings.Height;

                    if (
                        Snake[i].X < 0 || Snake[i].Y < 0 ||
                        Snake[i].X > maxXpos || Snake[i].Y > maxYpos
                        )
                    {
                        // В таком случае смерть...
                        die();
                    }

                    // проверка на касание
                    // змеи и ее частей
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            die();
                        }
                    }
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        eat();
                    }
                }
                else
                {

                    // Если коллизии отсутсвуют - движение продолжается
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;

                }
                



            }
            
        }
        private void die()
        {
            Settings.GameOver = true;
        }
    }
}
