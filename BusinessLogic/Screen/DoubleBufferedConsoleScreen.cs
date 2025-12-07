using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic.Screen
{
    class DoubleBufferedConsoleScreen : IScreen
    {
        private ConsoleCell[,] buffer;
        private ConsoleCell[,] previousBuffer;
        private int width;
        private int height;

        public DoubleBufferedConsoleScreen() {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            Console.Clear();
            buffer = new ConsoleCell[width, height];
            this.Clear();
            previousBuffer = (ConsoleCell[,])buffer.Clone();
        }

        private ConsoleColor consoleColor(ScreenColor color) {
            switch (color) {
                case ScreenColor.White:
                    return ConsoleColor.White;
                case ScreenColor.Black:
                    return ConsoleColor.Black;
                case ScreenColor.Red:
                    return ConsoleColor.Red;
                case ScreenColor.Green:
                    return ConsoleColor.Green;
                case ScreenColor.Blue:
                    return ConsoleColor.Blue;
            }
            throw new NotImplementedException();
        }

        public void Clear(ScreenColor color = ScreenColor.Black) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    buffer[x, y].symbol = ' ';
                    buffer[x, y].backgroundColor = color;
                }
            }
        }

        public void Flush() {
            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (!buffer[x, y].Equals(previousBuffer[x,y])) {
                        Console.SetCursorPosition(x, y);

                        foregroundColor = consoleColor(buffer[x, y].foregroundColor);
                        if (consoleColor(buffer[x, y].foregroundColor) != Console.ForegroundColor)
                            Console.ForegroundColor = foregroundColor;

                        backgroundColor = consoleColor(buffer[x, y].backgroundColor);
                        if (consoleColor(buffer[x, y].backgroundColor) != Console.BackgroundColor)
                            Console.BackgroundColor = backgroundColor;
                        
                        Console.Out.Write(buffer[x, y].symbol);

                        previousBuffer[x, y] = buffer[x, y];
                    }
                }
            }
            Console.Out.Flush();
        }

        public void Write(Position2D position, string text, ScreenColor color = ScreenColor.White, ScreenColor backgroundColor = ScreenColor.Black) {
            int x = position.x;
            int y = position.y;
            foreach (char character in text) {
                if (x >= 0 && x < width && y >= 0 && y < height) {
                    buffer[x, y].symbol = character;
                    buffer[x, y].foregroundColor = color;
                    buffer[x, y].backgroundColor = backgroundColor;
                }
                x++;
            }
        }
    }
}
