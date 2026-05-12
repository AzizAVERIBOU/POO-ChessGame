using echec_poo.Models;

namespace echec_poo.Game
{
    /// <summary>
    /// Type de coup pour le moteur (extensible : roque, en passant, promotion).
    /// </summary>
    public enum TypeCoup
    {
        Normal,
        RoquePetit,
        RoqueGrand,
        PriseEnPassant,
        Promotion
    }

    /// <summary>
    /// Représente un coup pour la simulation et l'application future au plateau.
    /// </summary>
    public readonly struct Coup
    {
        public Coup(Position depart, Position arrivee, TypeCoup type = TypeCoup.Normal)
        {
            Depart = depart;
            Arrivee = arrivee;
            Type = type;
        }

        public Position Depart { get; }
        public Position Arrivee { get; }
        public TypeCoup Type { get; }

        public static Coup Normal(Position depart, Position arrivee) =>
            new(depart, arrivee, TypeCoup.Normal);
    }
}
