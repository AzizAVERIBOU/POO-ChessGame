using echec_poo.Models;
using echec_poo.Game;

namespace echec_poo.Pieces
{
    /// <summary>
    /// Représente un cavalier aux échecs
    /// Règles : Se déplace en L (2+1 ou 1+2 cases)
    /// Peut sauter par-dessus les autres pièces
    /// </summary>
    public class Cavalier : Piece
    {
        public Cavalier(Couleur couleur, Position position) : base(couleur, position)
        {
        }

        public override bool PeutSeDeplacerVers(Position nouvellePosition, Echiquier echiquier)
        {
            if (echiquier == null || !nouvellePosition.EstValide())
                return false;

            int deltaLigne = Math.Abs(nouvellePosition.Ligne - Position.Ligne);
            int deltaColonne = Math.Abs(nouvellePosition.Colonne - Position.Colonne);

            // Vérifier que c'est un mouvement en L
            if (!((deltaLigne == 2 && deltaColonne == 1) || (deltaLigne == 1 && deltaColonne == 2)))
                return false;

            // Le cavalier peut sauter, donc pas besoin de vérifier le chemin
            // Vérifier seulement que la case d'arrivée est libre ou contient une pièce adverse
            Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
            return pieceCible == null || pieceCible.Couleur != Couleur;
        }

        public override List<Position> ObtenirMouvementsPossibles(Echiquier echiquier)
        {
            List<Position> mouvements = new List<Position>();

            // Tous les mouvements possibles en L
            int[,] mouvementsL = {
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
            };

            for (int i = 0; i < 8; i++)
            {
                int nouvelleLigne = Position.Ligne + mouvementsL[i, 0];
                int nouvelleColonne = Position.Colonne + mouvementsL[i, 1];
                
                if (nouvelleLigne >= 0 && nouvelleLigne <= 7 && nouvelleColonne >= 0 && nouvelleColonne <= 7)
                {
                    Position nouvellePosition = new Position(nouvelleLigne, nouvelleColonne);
                    Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
                    if (pieceCible == null || pieceCible.Couleur != Couleur)
                    {
                        mouvements.Add(nouvellePosition);
                    }
                }
            }

            return mouvements;
        }

        public override string ObtenirSymbole()
        {
            return Couleur == Couleur.Blanc ? "♘" : "♞";
        }

        public override string ObtenirNom()
        {
            return "Cavalier";
        }
    }
}

