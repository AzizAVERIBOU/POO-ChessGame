using echec_poo.Game;

namespace echec_poo.Models
{
    /// <summary>
    /// Classe abstraite de base pour toutes les pièces d'échecs
    /// Démonstration des concepts POO : abstraction, héritage, polymorphisme
    /// </summary>
    public abstract class Piece
    {
        public Couleur Couleur { get; private set; }
        public Position Position { get; protected set; }
        public bool ADejaBouge { get; protected set; }

        protected Piece(Couleur couleur, Position position)
        {
            Couleur = couleur;
            Position = position;
            ADejaBouge = false;
        }

        /// <summary>
        /// Vérifie si le mouvement est valide pour cette pièce
        /// Méthode abstraite - doit être implémentée par chaque type de pièce
        /// </summary>
        public abstract bool PeutSeDeplacerVers(Position nouvellePosition, Echiquier echiquier);

        /// <summary>
        /// Obtient tous les mouvements possibles pour cette pièce
        /// </summary>
        public abstract List<Position> ObtenirMouvementsPossibles(Echiquier echiquier);

        /// <summary>
        /// Déplace la pièce vers une nouvelle position
        /// </summary>
        public virtual void DeplacerVers(Position nouvellePosition)
        {
            Position = nouvellePosition;
            ADejaBouge = true;
        }

        /// <summary>
        /// Vérifie si la pièce peut capturer une autre pièce
        /// </summary>
        public virtual bool PeutCapturer(Piece pieceCible, Echiquier echiquier)
        {
            return pieceCible.Couleur != Couleur && PeutSeDeplacerVers(pieceCible.Position, echiquier);
        }

        /// <summary>
        /// Obtient le symbole Unicode de la pièce pour l'affichage
        /// </summary>
        public abstract string ObtenirSymbole();

        /// <summary>
        /// Obtient le nom de la pièce
        /// </summary>
        public abstract string ObtenirNom();

        /// <summary>
        /// Vérifie si le chemin est libre entre deux positions
        /// </summary>
        protected bool CheminEstLibre(Position depart, Position arrivee, Echiquier echiquier)
        {
            int deltaLigne = arrivee.Ligne - depart.Ligne;
            int deltaColonne = arrivee.Colonne - depart.Colonne;
            
            int pasLigne = deltaLigne == 0 ? 0 : (deltaLigne > 0 ? 1 : -1);
            int pasColonne = deltaColonne == 0 ? 0 : (deltaColonne > 0 ? 1 : -1);

            int distance = Math.Max(Math.Abs(deltaLigne), Math.Abs(deltaColonne));

            for (int i = 1; i < distance; i++)
            {
                Position positionIntermediaire = new Position(
                    depart.Ligne + i * pasLigne,
                    depart.Colonne + i * pasColonne
                );

                if (echiquier.ObtenirPiece(positionIntermediaire) != null)
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{ObtenirNom()} {Couleur} en {Position}";
        }
    }
}
