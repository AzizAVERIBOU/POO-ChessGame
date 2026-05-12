using echec_poo.Game;
using echec_poo.Models;

namespace echec_poo.Tests;

public class SimulationPlateauTests
{
    [Fact]
    public void RoiSeraitEnEchecApresCoup_e2e4_ne_met_pas_le_roi_blanc_en_échec()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();
        // pion blanc (1,4) → (3,4), équivalent notation projet « e7-e5 »
        var coup = Coup.Normal(new Position(1, 4), new Position(3, 4));

        // Act
        bool roiEnEchec = SimulationPlateau.RoiSeraitEnEchecApresCoup(ech, coup);

        // Assert
        Assert.False(roiEnEchec);
    }

    [Fact]
    public void AppliquerNormal_puis_AnnulerNormal_restauré_plateau_et_ADejaBouge()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();
        var depart = new Position(1, 4);
        var arrivee = new Position(3, 4);
        var coup = Coup.Normal(depart, arrivee);
        Piece pion = ech.ObtenirPiece(depart)!;

        // Act — application
        var snap = SimulationPlateau.AppliquerNormal(ech, coup);

        // Assert — après application
        Assert.Equal(arrivee, pion.Position);
        Assert.True(pion.ADejaBouge);

        // Act — annulation
        SimulationPlateau.AnnulerNormal(ech, snap);

        // Assert — plateau et drapeau restaurés (dont état initial du pion)
        Assert.Equal(depart, pion.Position);
        Assert.False(pion.ADejaBouge);
        Assert.NotNull(ech.ObtenirPiece(depart));
        Assert.Null(ech.ObtenirPiece(arrivee));
    }

    [Fact]
    public void RoiSeraitEnEchecApresCoup_case_départ_vide_retourne_faux()
    {
        // Arrange
        var ech = new Echiquier();
        ech.InitialiserPositionDepart();
        var coup = Coup.Normal(new Position(3, 3), new Position(3, 4));

        // Act
        bool roiEnEchec = SimulationPlateau.RoiSeraitEnEchecApresCoup(ech, coup);

        // Assert
        Assert.False(roiEnEchec);
    }
}
