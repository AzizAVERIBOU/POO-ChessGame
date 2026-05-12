using echec_poo.Game;
using echec_poo.Models;

namespace echec_poo.Tests;

/// <summary>
/// Coups légaux, mat et pat (phase B moteur).
/// </summary>
public class JeuEchecsReglesTests
{
    [Fact]
    public void ObtenirMouvementsPossibles_pion_e2_deux_cases_au_depart()
    {
        // Arrange
        var jeu = new JeuEchecs();

        // Act
        var coups = jeu.ObtenirMouvementsPossibles(Position.DepuisNotation("e2"));

        // Assert
        Assert.Equal(2, coups.Count);
        Assert.Contains(Position.DepuisNotation("e3"), coups);
        Assert.Contains(Position.DepuisNotation("e4"), coups);
    }

    [Fact]
    public void EstPat_faux_en_position_initiale()
    {
        // Arrange
        var jeu = new JeuEchecs();

        // Act
        bool pat = jeu.EstPat();

        // Assert
        Assert.False(pat);
    }

    [Fact]
    public void FoolsMate_apres_Qh4_partie_terminee_gagnant_noir()
    {
        // Fool's mate : 1.f3 e5 2.g4 Qh4#
        var jeu = new JeuEchecs();

        Assert.True(jeu.EffectuerMouvement("f2f3"));
        Assert.True(jeu.EffectuerMouvement("e7e5"));
        Assert.True(jeu.EffectuerMouvement("g2g4"));
        Assert.True(jeu.EffectuerMouvement("d8h4"));

        Assert.True(jeu.PartieTerminee);
        Assert.Equal("Joueur Noir", jeu.Gagnant);
    }

    [Fact]
    public void Double_pas_pion_definit_case_prise_en_passant()
    {
        var jeu = new JeuEchecs();

        Assert.True(jeu.EffectuerMouvement("e2e4"));

        Assert.NotNull(jeu.Echiquier.CasePriseEnPassant);
        Assert.Equal(Position.DepuisNotation("e3"), jeu.Echiquier.CasePriseEnPassant);
    }

    [Fact]
    public void Prise_en_passant_e5xd6_retire_pion_d5()
    {
        // 1.e4 e6 2.e5 d5 3.exd6 e.p.
        var jeu = new JeuEchecs();

        Assert.True(jeu.EffectuerMouvement("e2e4"));
        Assert.True(jeu.EffectuerMouvement("e7e6"));
        Assert.True(jeu.EffectuerMouvement("e4e5"));
        Assert.True(jeu.EffectuerMouvement("d7d5"));

        Assert.Equal(Position.DepuisNotation("d6"), jeu.Echiquier.CasePriseEnPassant);

        Assert.True(jeu.EffectuerMouvement("e5d6"));

        Assert.Null(jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("d5")));
        Piece? surD6 = jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("d6"));
        Assert.NotNull(surD6);
        Assert.Equal("Pion", surD6!.ObtenirNom());
        Assert.Equal(Couleur.Blanc, surD6.Couleur);
    }

    [Fact]
    public void Prise_en_passant_impossible_apres_autre_coup_blanc()
    {
        var jeu = new JeuEchecs();

        Assert.True(jeu.EffectuerMouvement("e2e4"));
        Assert.True(jeu.EffectuerMouvement("d7d5"));
        Assert.NotNull(jeu.Echiquier.CasePriseEnPassant);

        Assert.True(jeu.EffectuerMouvement("a2a3"));
        Assert.Null(jeu.Echiquier.CasePriseEnPassant);

        Assert.True(jeu.EffectuerMouvement("a7a6"));
        Assert.True(jeu.EffectuerMouvement("e4e5"));
        Assert.True(jeu.EffectuerMouvement("h7h6"));
        Assert.False(jeu.EffectuerMouvement("e5d6"));
    }

    [Fact]
    public void Roque_petit_impossible_depuis_position_initiale()
    {
        var jeu = new JeuEchecs();
        Assert.False(jeu.EffectuerMouvement("e1g1"));
    }

    [Fact]
    public void Roque_petit_blanc_e1g1_tour_sur_f1()
    {
        // 1.e4 e5 2.Nf3 Nc6 3.Bc4 Bc5 4.O-O
        var jeu = new JeuEchecs();

        Assert.True(jeu.EffectuerMouvement("e2e4"));
        Assert.True(jeu.EffectuerMouvement("e7e5"));
        Assert.True(jeu.EffectuerMouvement("g1f3"));
        Assert.True(jeu.EffectuerMouvement("b8c6"));
        Assert.True(jeu.EffectuerMouvement("f1c4"));
        Assert.True(jeu.EffectuerMouvement("f8c5"));

        Assert.True(jeu.EffectuerMouvement("e1g1"));

        Assert.NotNull(jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("g1")));
        Assert.Equal("Roi", jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("g1"))!.ObtenirNom());
        Assert.Equal("Tour", jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("f1"))!.ObtenirNom());
        Assert.Null(jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("h1")));
        Assert.False(jeu.Echiquier.BlancRoquePetit);
        Assert.False(jeu.Echiquier.BlancRoqueGrand);
    }

    [Fact]
    public void Roque_grand_blanc_e1c1_tour_sur_d1()
    {
        var jeu = new JeuEchecs();

        Assert.True(jeu.EffectuerMouvement("d2d4"));
        Assert.True(jeu.EffectuerMouvement("d7d5"));
        Assert.True(jeu.EffectuerMouvement("d1d3"));
        Assert.True(jeu.EffectuerMouvement("a7a6"));
        Assert.True(jeu.EffectuerMouvement("b1c3"));
        Assert.True(jeu.EffectuerMouvement("a6a5"));
        Assert.True(jeu.EffectuerMouvement("c1d2"));
        Assert.True(jeu.EffectuerMouvement("b8c6"));

        Assert.True(jeu.EffectuerMouvement("e1c1"));

        Assert.Equal("Roi", jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("c1"))!.ObtenirNom());
        Assert.Equal("Tour", jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("d1"))!.ObtenirNom());
        Assert.Null(jeu.Echiquier.ObtenirPiece(Position.DepuisNotation("a1")));
        Assert.False(jeu.Echiquier.BlancRoquePetit);
        Assert.False(jeu.Echiquier.BlancRoqueGrand);
    }
}
