using echec_poo.Models;

namespace echec_poo.Tests;

public class PositionTests
{
    [Theory]
    [InlineData("a1", 7, 0)]
    [InlineData("h8", 0, 7)]
    [InlineData("e4", 4, 4)]
    public void DepuisNotation_parse_correctement(string notation, int ligne, int colonne)
    {
        // Arrange
        // (notation, ligne, colonne) fournis par InlineData

        // Act
        Position p = Position.DepuisNotation(notation);

        // Assert
        Assert.Equal(ligne, p.Ligne);
        Assert.Equal(colonne, p.Colonne);
    }

    [Fact]
    public void ToString_retourne_notation_algébrique()
    {
        // Arrange
        var posE4 = new Position(4, 4);
        var posA1 = new Position(7, 0);

        // Act
        string sE4 = posE4.ToString();
        string sA1 = posA1.ToString();

        // Assert
        Assert.Equal("e4", sE4);
        Assert.Equal("a1", sA1);
    }

    [Fact]
    public void Equals_même_case()
    {
        // Arrange
        var a = new Position(3, 4);
        var b = new Position(3, 4);

        // Act
        bool egaux = a.Equals(b);

        // Assert
        Assert.Equal(a, b);
        Assert.True(egaux);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(8, 0)]
    [InlineData(0, -1)]
    [InlineData(0, 8)]
    public void Constructeur_rejette_hors_plateau(int ligne, int colonne)
    {
        // Arrange
        // (ligne, colonne) hors [0,7]

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Position(ligne, colonne));
    }

    [Theory]
    [InlineData("")]
    [InlineData("e")]
    [InlineData("e44")]
    [InlineData("i4")]
    public void DepuisNotation_rejette_entrée_invalide(string notation)
    {
        // Arrange
        // notation invalide fournie par InlineData

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Position.DepuisNotation(notation));
    }
}
