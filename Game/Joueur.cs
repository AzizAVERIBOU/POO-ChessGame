using echec_poo.Models;

namespace echec_poo.Game
{
    /// <summary>
    /// Représente un joueur aux échecs
    /// </summary>
    public class Joueur
    {
        public string Nom { get; private set; }
        public Couleur Couleur { get; private set; }
        public bool EstEnEchec { get; set; }

        public Joueur(string nom, Couleur couleur)
        {
            Nom = nom;
            Couleur = couleur;
            EstEnEchec = false;
        }

        public override string ToString()
        {
            return $"{Nom} ({Couleur})";
        }
    }
}
