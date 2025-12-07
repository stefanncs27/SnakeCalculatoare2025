using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic.Screen
{
    struct ConsoleCell : IEquatable<ConsoleCell>
    {
        public char symbol;
        public ScreenColor backgroundColor;
        public ScreenColor foregroundColor;

        public bool Equals(ConsoleCell cellToCompare) {
            return this.symbol.Equals(cellToCompare.symbol)
                && this.backgroundColor.Equals(cellToCompare.backgroundColor)
                && this.foregroundColor.Equals(cellToCompare.foregroundColor);
        }

        public override bool Equals([NotNullWhen(true)] object? obj) {
            return obj is ConsoleCell cell && this.Equals(cell);
        }

        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}
