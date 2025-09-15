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

        public override bool PeutSeDeplacerVers(Position nouvellePosition, Echiquier echiquier)
        {
            if (echiquier == null || !nouvellePosition.EstValide())
                return false;

            int deltaLigne = nouvellePosition.Ligne - Position.Ligne;
            int deltaColonne = nouvellePosition.Colonne - Position.Colonne;

            // Vérifier si c'est un mouvement vers l'avant
            bool mouvementVersAvant = Couleur == Couleur.Blanc ? deltaLigne > 0 : deltaLigne < 0;
            if (!mouvementVersAvant)
                return false;

            // Mouvement simple d'une case
            if (Math.Abs(deltaLigne) == 1 && deltaColonne == 0)
            {
                return echiquier.PositionEstLibre(nouvellePosition);
            }

            // Premier mouvement de 2 cases
            if (Math.Abs(deltaLigne) == 2 && deltaColonne == 0 && !ADejaBouge)
            {
                // Vérifier que la case intermédiaire est libre
                Position caseIntermediaire = new Position(
                    Position.Ligne + (deltaLigne / 2),
                    Position.Colonne
                );
                return echiquier.PositionEstLibre(caseIntermediaire) && 
                       echiquier.PositionEstLibre(nouvellePosition);
            }

            // Prise en diagonale
            if (Math.Abs(deltaLigne) == 1 && Math.Abs(deltaColonne) == 1)
            {
                Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
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
            return Couleur == Couleur.Blanc ? "♙" : "♟";
        }

        public override string ObtenirNom()
        {
            return "Pion";
        }
    }
}

