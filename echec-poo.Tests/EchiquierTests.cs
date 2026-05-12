using echec_poo.Game;
using echec_poo.Models;

namespace echec_poo.Tests;

public class EchiquierTests
{
    [Fact]
    public void InitialiserPositionDepart_place_32_pièces()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();

        // Act
        int total = ech.ObtenirPieces(Couleur.Blanc).Count + ech.ObtenirPieces(Couleur.Noir).Count;

        // Assert
        Assert.Equal(32, total);
    }

    [Fact]
    public void TrouverRoi_retrouve_les_rois()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();

        // Act
        var roiB = ech.TrouverRoi(Couleur.Blanc);
        var roiN = ech.TrouverRoi(Couleur.Noir);

        // Assert
        Assert.NotNull(roiB);
        Assert.NotNull(roiN);
        Assert.Equal("Roi", roiB!.ObtenirNom());
        Assert.Equal("Roi", roiN!.ObtenirNom());
        Assert.Equal(new Position(0, 4), roiB.Position);
        Assert.Equal(new Position(7, 4), roiN.Position);
    }

    [Fact]
    public void RoiEstEnEchec_faux_en_position_initiale()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();

        // Act
        bool echecBlanc = ech.RoiEstEnEchec(Couleur.Blanc);
        bool echecNoir = ech.RoiEstEnEchec(Couleur.Noir);

        // Assert
        Assert.False(echecBlanc);
        Assert.False(echecNoir);
    }

    [Fact]
    public void DeplacerPiece_pion_e2_vers_e4()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();
        var depart = new Position(1, 4);
        var arrivee = new Position(3, 4);

        // Act
        bool ok = ech.DeplacerPiece(depart, arrivee);

        // Assert
        Assert.True(ok);
        Assert.Null(ech.ObtenirPiece(depart));
        Assert.NotNull(ech.ObtenirPiece(arrivee));
        Assert.Equal("Pion", ech.ObtenirPiece(arrivee)!.ObtenirNom());
    }
}
