using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    class Game
    {
        int score = 0;
        Snake snake;
        PlayArea playArea;
        int playAreaWidth = 30;
        int playAreaHeight = 15;
        Reward reward;
        WinState winState;
        IScreen screen;
        bool ignoreBackwardMoves = true;


        public Game(IScreen screen) {
            this.screen = screen;
            this.playArea = new PlayArea(
                new Position2D(1, 3),
                width: playAreaWidth,
                height: playAreaHeight
                );
            this.snake = new Snake(
                initialPosition: new Position2D(10, 10),
                initialLength: 10,
                direction: SnakeDirection.Right
                );
            this.reward = new Reward(
                new Position2D(12, 10),
                points: 3
                );
            this.winState = new WinState();
        }

        public void Update(ConsoleKey? key) {
            SnakeDirection? newDirection = null;
            if (key != null) {
                switch (key) {
                    case ConsoleKey.LeftArrow:
                        newDirection = SnakeDirection.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        newDirection = SnakeDirection.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        newDirection = SnakeDirection.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        newDirection = SnakeDirection.Down;
                        break;
                    case ConsoleKey.D1:
                        snake.ChangeColor(ScreenColor.Red);
                        break;
                    case ConsoleKey.D2:
                        snake.ChangeColor(ScreenColor.Blue);
                        break;
                    case ConsoleKey.D3:
                        snake.ChangeColor(ScreenColor.Yellow);
                        break;
                    case ConsoleKey.D4:
                        snake.ChangeColor(ScreenColor.Cyan);
                        break;
                    case ConsoleKey.D5:
                        snake.ChangeColor(ScreenColor.Magenta);
                        break;
                }
            }

            if (ignoreBackwardMoves && newDirection.HasValue && IsMoveBackwards(newDirection.Value))
            {
                newDirection = null;
            }


            Position2D nextPosition = snake.ComputeNextPosition(newDirection);

            bool grow = false;
            if (reward.CollidesWith(nextPosition))
            {
                grow = true;
            }

            if (playArea.CollidesWith(nextPosition)
                || snake.CollidesWith(nextPosition)
                ) {
                snake.InitializeSnake();
            } else {
                snake.Update(newDirection, grow);
            }

        }

        public bool IsGameWon() {
            winState.ComputeWinState(
                currentSnakeLength: snake.GetCurrentLength(), 
                playAreaSize: (playAreaWidth - 1) * (playAreaHeight - 1)
            );
            return winState.GetWinState();
        }


        private bool IsMoveBackwards(SnakeDirection newDirection)
        {
            SnakeDirection currentDirection = snake.GetCurrentDirection();

            switch (currentDirection)
            {
                case SnakeDirection.Left: return newDirection == SnakeDirection.Right;
                case SnakeDirection.Right: return newDirection == SnakeDirection.Left;
                case SnakeDirection.Up: return newDirection == SnakeDirection.Down;
                case SnakeDirection.Down: return newDirection == SnakeDirection.Up;
                default: return false;
            }
        }


        public void Draw() {
            playArea.Draw(screen);
            reward.Draw(screen);
            snake.Draw(screen);
        }

        public void DrawWinMessage () {
            playArea.DrawWinMessage(screen);
        }
    }
}
