using echec_poo.Models;
using echec_poo.Game;

namespace echec_poo.Pieces
{
    /// <summary>
    /// Représente un fou aux échecs
    /// Règles : Se déplace en diagonale
    /// </summary>
    public class Fou : Piece
    {
        public Fou(Couleur couleur, Position position) : base(couleur, position)
        {
        }

        public override bool PeutSeDeplacerVers(Position nouvellePosition, Echiquier echiquier)
        {
            if (echiquier == null || !nouvellePosition.EstValide())
                return false;

            int deltaLigne = nouvellePosition.Ligne - Position.Ligne;
            int deltaColonne = nouvellePosition.Colonne - Position.Colonne;

            // Vérifier que c'est un mouvement diagonal
            if (Math.Abs(deltaLigne) != Math.Abs(deltaColonne))
                return false;

            // Vérifier que le chemin est libre
            if (!CheminEstLibre(Position, nouvellePosition, echiquier))
                return false;

            // Vérifier que la case d'arrivée est libre ou contient une pièce adverse
            Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
            return pieceCible == null || pieceCible.Couleur != Couleur;
        }

        public override List<Position> ObtenirMouvementsPossibles(Echiquier echiquier)
        {
            List<Position> mouvements = new List<Position>();

            // Mouvements diagonaux
            int[,] directions = { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

            for (int i = 0; i < 4; i++)
            {
                int deltaLigne = directions[i, 0];
                int deltaColonne = directions[i, 1];

                for (int distance = 1; distance < 8; distance++)
                {
                    Position nouvellePosition = new Position(
                        Position.Ligne + distance * deltaLigne,
                        Position.Colonne + distance * deltaColonne
                    );

                    if (!nouvellePosition.EstValide())
                        break;

                    Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
                    
                    if (pieceCible == null)
                    {
                        mouvements.Add(nouvellePosition);
                    }
                    else if (pieceCible.Couleur != Couleur)
                    {
                        mouvements.Add(nouvellePosition);
                        break; // On peut capturer mais pas continuer
                    }
                    else
                    {
                        break; // Pièce alliée, on s'arrête
                    }
                }
            }

            return mouvements;
        }

        public override string ObtenirSymbole()
        {
            return Couleur == Couleur.Blanc ? "F" : "f";
        }

        public override string ObtenirNom()
        {
            return "Fou";
        }
    }
}

