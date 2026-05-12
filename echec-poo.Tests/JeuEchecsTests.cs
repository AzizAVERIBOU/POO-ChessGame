using echec_poo.Game;
using echec_poo.Models;

namespace echec_poo.Tests;

public class JeuEchecsTests
{
    [Fact]
    public void EffectuerMouvement_premier_coup_pion_blanc_réussit()
    {
        // Arrange
        var jeu = new JeuEchecs("Alice", "Bob");
        // Notation alignée sur Position.DepuisNotation / plateau initial (équivalent « e2-e4 » classique)

        // Act
        bool ok = jeu.EffectuerMouvement("e7e5");

        // Assert
        Assert.True(ok);
        Assert.Equal(Couleur.Noir, jeu.JoueurActuel.Couleur);
        Assert.Equal("Pion", jeu.Echiquier.ObtenirPiece(new Position(3, 4))!.ObtenirNom());
    }

    [Fact]
    public void EffectuerMouvement_rejette_pièce_adverse_au_tour_blanc()
    {
        // Arrange
        var jeu = new JeuEchecs();

        // Act
        bool ok = jeu.EffectuerMouvement("e2e4");

        // Assert
        Assert.False(ok);
    }

    [Fact]
    public void EffectuerMouvement_rejette_syntaxe_invalide()
    {
        // Arrange
        var jeu = new JeuEchecs();

        // Act
        bool ok = jeu.EffectuerMouvement("xyz");

        // Assert
        Assert.False(ok);
    }
}
