using SnakeCalculatoare2025.BusinessLogic;
using SnakeCalculatoare2025.BusinessLogic.Screen;


IScreen screen = new DoubleBufferedConsoleScreen();

Game game = new Game(screen);

while (true) {
    if (game.IsGameWon()) {
        game.DrawWinMessage();
        return;
    }

    ConsoleKey? keyPressed = null;
    if (Console.KeyAvailable) {
        ConsoleKeyInfo key = Console.ReadKey();
        keyPressed = key.Key;
        switch (keyPressed) {
            case ConsoleKey.Escape:
                return;
        }
    }

    game.Update(keyPressed);

    screen.Clear();
    game.Draw();
    screen.Flush();

    Thread.Sleep(100);
}

