using echec_poo.Models;
using echec_poo.Game;

namespace echec_poo.Pieces
{
    /// <summary>
    /// Représente un pion aux échecs
    /// Règles : Avance d'une case, peut avancer de 2 cases au premier coup
    /// Prend en diagonale d'une case
    /// </summary>
    public class Pion : Piece
    {
        public Pion(Couleur couleur, Position position) : base(couleur, position)
        {
        }

        public override bool PeutSeDeplacerVers(Position destination, Echiquier echiquier)
        {
            // Vérifier que la destination est valide
            if (!destination.EstValide())
                return false;

            // Vérifier que la destination est différente de la position actuelle
            if (destination.Equals(Position))
                return false;

            // Vérifier que la destination est libre ou contient une pièce adverse
            Piece? pieceCible = echiquier.ObtenirPiece(destination);
            if (pieceCible != null && pieceCible.Couleur == Couleur)
                return false;

            int direction = Couleur == Couleur.Blanc ? 1 : -1;
            int deltaLigne = destination.Ligne - Position.Ligne;
            int deltaColonne = destination.Colonne - Position.Colonne;

            // Mouvement vers l'avant
            if (deltaColonne == 0)
            {
                // Une case vers l'avant
                if (deltaLigne == direction && pieceCible == null)
                    return true;
                
                // Deux cases vers l'avant (premier mouvement)
                if (deltaLigne == 2 * direction && pieceCible == null && 
                    ((Couleur == Couleur.Blanc && Position.Ligne == 1) || 
                     (Couleur == Couleur.Noir && Position.Ligne == 6)))
                    return true;
            }
            // Prise en diagonale
            else if (Math.Abs(deltaColonne) == 1 && deltaLigne == direction)
            {
                return pieceCible != null && pieceCible.Couleur != Couleur;
            }

            return false;
        }

        public override List<Position> ObtenirMouvementsPossibles(Echiquier echiquier)
        {
            List<Position> mouvements = new List<Position>();
            int direction = Couleur == Couleur.Blanc ? 1 : -1;

            // Mouvement d'une case
            Position mouvement1 = new Position(Position.Ligne + direction, Position.Colonne);
            if (mouvement1.EstValide() && echiquier.PositionEstLibre(mouvement1))
            {
                mouvements.Add(mouvement1);
            }

            // Premier mouvement de 2 cases
            if (!ADejaBouge)
            {
                Position mouvement2 = new Position(Position.Ligne + 2 * direction, Position.Colonne);
                if (mouvement2.EstValide() && echiquier.PositionEstLibre(mouvement2) && 
                    echiquier.PositionEstLibre(mouvement1))
                {
                    mouvements.Add(mouvement2);
                }
            }

            // Prises en diagonale
            for (int deltaColonne = -1; deltaColonne <= 1; deltaColonne += 2)
            {
                int nouvelleLigne = Position.Ligne + direction;
                int nouvelleColonne = Position.Colonne + deltaColonne;
                
                if (nouvelleLigne >= 0 && nouvelleLigne <= 7 && nouvelleColonne >= 0 && nouvelleColonne <= 7)
                {
                    Position prise = new Position(nouvelleLigne, nouvelleColonne);
                    Piece? pieceCible = echiquier.ObtenirPiece(prise);
                    if (pieceCible != null && pieceCible.Couleur != Couleur)
                    {
                        mouvements.Add(prise);
                    }
                }
            }

            return mouvements;
        }

        public override string ObtenirSymbole()
        {
            return Couleur == Couleur.Blanc ? "P" : "p";
        }

        public override string ObtenirNom()
        {
            return "Pion";
        }
    }
}

