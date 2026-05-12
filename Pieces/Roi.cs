using echec_poo.Models;
using echec_poo.Game;

namespace echec_poo.Pieces
{
    /// <summary>
    /// Représente un roi aux échecs
    /// Règles : Se déplace d'une case dans toutes les directions
    /// Ne peut pas se mettre en échec
    /// </summary>
    public class Roi : Piece
    {
        public Roi(Couleur couleur, Position position) : base(couleur, position)
        {
        }

        public override bool PeutSeDeplacerVers(Position nouvellePosition, Echiquier echiquier)
        {
            if (echiquier == null || !nouvellePosition.EstValide())
                return false;

            int deltaLigne = nouvellePosition.Ligne - Position.Ligne;
            int deltaColonneAbs = Math.Abs(nouvellePosition.Colonne - Position.Colonne);

            int ligneFond = Couleur == Couleur.Blanc ? 0 : 7;
            if (deltaLigne == 0 && deltaColonneAbs == 2 && Position.Ligne == ligneFond && Position.Colonne == 4)
            {
                return nouvellePosition.Colonne > Position.Colonne
                    ? echiquier.PeutRoquerPetit(Couleur)
                    : echiquier.PeutRoquerGrand(Couleur);
            }

            int deltaLigneAbs = Math.Abs(deltaLigne);
            if (deltaLigneAbs > 1 || deltaColonneAbs > 1)
                return false;

            // Vérifier que la case d'arrivée est libre ou contient une pièce adverse
            Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
            if (pieceCible != null && pieceCible.Couleur == Couleur)
                return false;

            // TODO: Vérifier que le roi ne se met pas en échec
            // Cette vérification sera implémentée dans l'itération 5

            return true;
        }

        public override List<Position> ObtenirMouvementsPossibles(Echiquier echiquier)
        {
            List<Position> mouvements = new List<Position>();

            // Toutes les directions possibles (8 directions)
            int[,] directions = { 
                { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 },  // Horizontal et vertical
                { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }  // Diagonal
            };

            for (int i = 0; i < 8; i++)
            {
                Position? nouvellePosition = Position.CreerSiValide(
                    Position.Ligne + directions[i, 0],
                    Position.Colonne + directions[i, 1]);
                if (nouvellePosition == null)
                    continue;

                Piece? pieceCible = echiquier.ObtenirPiece(nouvellePosition);
                if (pieceCible == null || pieceCible.Couleur != Couleur)
                    mouvements.Add(nouvellePosition);
            }

            int ligneFond = Couleur == Couleur.Blanc ? 0 : 7;
            if (Position.Ligne == ligneFond && Position.Colonne == 4)
            {
                if (echiquier.PeutRoquerPetit(Couleur))
                {
                    Position? g = Position.CreerSiValide(Position.Ligne, 6);
                    if (g != null)
                        mouvements.Add(g);
                }
                if (echiquier.PeutRoquerGrand(Couleur))
                {
                    Position? c = Position.CreerSiValide(Position.Ligne, 2);
                    if (c != null)
                        mouvements.Add(c);
                }
            }

            return mouvements;
        }

        public override string ObtenirSymbole()
        {
            return Couleur == Couleur.Blanc ? "R" : "r";
        }

        public override string ObtenirNom()
        {
            return "Roi";
        }
    }
}

