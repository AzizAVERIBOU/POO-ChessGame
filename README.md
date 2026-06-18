# Chess Game in C# - OOP Project

A console chess game built during an OOP (Object-Oriented Programming) course, developed in C# to demonstrate Object-Oriented Programming concepts.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![C#](https://img.shields.io/badge/C%23-12.0-purple.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## Play Online

The game is available in the browser via a web terminal:

**[https://poo-chessgame.onrender.com/](https://poo-chessgame.onrender.com/)**

## Project Goals

This project aims to demonstrate mastery of fundamental OOP concepts:
- **Inheritance**: Specialized classes inheriting from `Piece`
- **Polymorphism**: Virtual and abstract methods
- **Encapsulation**: Private properties with accessors
- **Abstraction**: Abstract class `Piece`
- **Composition**: `Echiquier` contains `Piece` objects

## Features

### ✅ Implemented Features
- [x] **All chess pieces**: Pawn, Rook, Knight, Bishop, Queen, King
- [x] **Movement rules**: Piece-specific moves
- [x] **User interface**: Interactive console menu
- [x] **Enhanced display**: Visual indication of the current player's pieces
- [x] **Move validation**: Chess rule verification
- [x] **Algebraic notation**: Support for moves in "e2-e4" format
- [x] **Turn system**: White and black player management
- [x] **Check detection**: Basic check verification

## Architecture

```
echec-poo/
├── Models/
│   ├── Position.cs      # Coordinates and FIDE algebraic notation
│   ├── Couleur.cs       # Color enumeration
│   └── Piece.cs         # Base abstract class
├── Game/
│   ├── Echiquier.cs     # Board management and display
│   ├── JeuEchecs.cs     # Main game logic
│   ├── Joueur.cs        # Player representation
│   └── InterfaceJeu.cs  # User interface
├── Pieces/
│   ├── Pion.cs          # Pawn implementation
│   ├── Tour.cs          # Rook implementation
│   ├── Cavalier.cs      # Knight implementation
│   ├── Fou.cs           # Bishop implementation
│   ├── Dame.cs          # Queen implementation
│   └── Roi.cs           # King implementation
├── echec-poo.Tests/     # Unit tests (xUnit)
└── Program.cs           # Entry point and console tests
```

## Usage

### Prerequisites
- [.NET 8.0](https://dotnet.microsoft.com/download) or later

### Installation and Execution

```bash
# Clone the repository
git clone https://github.com/AzizAVERIBOU/POO-ChessGame.git
cd POO-ChessGame

# Build the project
dotnet build

# Run the game
dotnet run
```

### How to Play

1. **Start the game**: Run `dotnet run`
2. **Display the board**: Choose option 2
3. **Make a move**: Choose option 3 and enter your move (e.g. "e2-e4")
4. **View possible moves**: Choose option 4 to see the legal moves for a piece

## Special Features

### Visual Indication
- The current player's pieces are surrounded by asterisks (*P*)
- Clear interface with explanatory legend

### Algebraic Notation (FIDE)

- **Ranks**: 1 = white side (bottom), 8 = black side — aligned with the console display and with `Position.DepuisNotation` / `ToString`.
- **Moves**: `e2e4` or `e2-e4` (from square then to square, two squares in standard notation).

### OOP Architecture
- **Inheritance**: Each piece inherits from the `Piece` class
- **Polymorphism**: Virtual methods overridden for each piece
- **Encapsulation**: Private properties with public accessors
- **Abstraction**: Abstract `Piece` class with abstract methods
- **Composition**: `Echiquier` contains an array of `Piece` objects

## Tests

The project includes integrated tests that run automatically on startup:
- Chessboard initialization test
- Basic move test
- Position validation test

## Future Improvements

- [ ] En passant
- [ ] Pawn promotion
- [ ] Castling (kingside and queenside)
- [ ] Graphical interface
- [ ] Save/load games

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

*Developed as part of an Object-Oriented Programming learning project in C#*
