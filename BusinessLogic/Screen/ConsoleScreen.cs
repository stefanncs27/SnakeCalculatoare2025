using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic.Screen
{
    class ConsoleScreen : IScreen
    {
        public ConsoleScreen() {
            Console.CursorVisible = false;
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
                case ScreenColor.Yellow:
                    return ConsoleColor.Yellow;
                case ScreenColor.Cyan:
                    return ConsoleColor.Cyan;
                case ScreenColor.Magenta:
                    return ConsoleColor.Magenta;
            }
            throw new NotImplementedException();
        }

        public void Clear(ScreenColor color = ScreenColor.Black) {
            Console.BackgroundColor = consoleColor(color);
            Console.Clear();
        }

        public void Flush() {            
        }

        public void Write(Position2D position, string text, ScreenColor color = ScreenColor.White, ScreenColor backgroundColor = ScreenColor.Black) {
            Console.SetCursorPosition(position.x, position.y);
            Console.ForegroundColor = consoleColor(color);
            Console.BackgroundColor = consoleColor(backgroundColor);
            Console.Write(text);
        }
    }
}
