using echec_poo.Models;

namespace echec_poo.Game
{
    /// <summary>
    /// Représente l'échiquier et gère les pièces
    /// Démonstration des concepts POO : composition, encapsulation
    /// </summary>
    public class Echiquier
    {
        private Piece?[,] _pieces;

        public Echiquier()
        {
            _pieces = new Piece?[8, 8];
        }

        /// <summary>
        /// Obtient la pièce à une position donnée
        /// </summary>
        public Piece? ObtenirPiece(Position position)
        {
            if (!position.EstValide())
                return null;
            
            return _pieces[position.Ligne, position.Colonne];
        }

        /// <summary>
        /// Place une pièce sur l'échiquier
        /// </summary>
        public void PlacerPiece(Piece piece)
        {
            if (!piece.Position.EstValide())
                throw new ArgumentException("Position invalide");

            _pieces[piece.Position.Ligne, piece.Position.Colonne] = piece;
        }

        /// <summary>
        /// Retire une pièce de l'échiquier
        /// </summary>
        public void RetirerPiece(Position position)
        {
            if (position.EstValide())
                _pieces[position.Ligne, position.Colonne] = null;
        }

        /// <summary>
        /// Déplace une pièce d'une position à une autre
        /// </summary>
        public bool DeplacerPiece(Position depart, Position arrivee)
        {
            Piece? piece = ObtenirPiece(depart);
            if (piece == null)
                return false;

            if (!piece.PeutSeDeplacerVers(arrivee, this))
                return false;

            // Retirer la pièce de sa position actuelle
            RetirerPiece(depart);
            
            // Placer la pièce à la nouvelle position
            piece.DeplacerVers(arrivee);
            PlacerPiece(piece);

            return true;
        }

        /// <summary>
        /// Vérifie si une position est libre
        /// </summary>
        public bool PositionEstLibre(Position position)
        {
            return ObtenirPiece(position) == null;
        }

        /// <summary>
        /// Obtient toutes les pièces d'une couleur donnée
        /// </summary>
        public List<Piece> ObtenirPieces(Couleur couleur)
        {
            List<Piece> pieces = new List<Piece>();
            
            for (int ligne = 0; ligne < 8; ligne++)
            {
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Piece? piece = _pieces[ligne, colonne];
                    if (piece != null && piece.Couleur == couleur)
                        pieces.Add(piece);
                }
            }
            
            return pieces;
        }

        /// <summary>
        /// Trouve le roi d'une couleur donnée
        /// </summary>
        public Piece? TrouverRoi(Couleur couleur)
        {
            return ObtenirPieces(couleur).FirstOrDefault(p => p.ObtenirNom() == "Roi");
        }

        /// <summary>
        /// Vérifie si une position est attaquée par une couleur donnée
        /// </summary>
        public bool PositionEstAttaquee(Position position, Couleur couleurAttaquante)
        {
            List<Piece> piecesAttaquantes = ObtenirPieces(couleurAttaquante);
            
            foreach (Piece piece in piecesAttaquantes)
            {
                if (piece.PeutSeDeplacerVers(position, this))
                    return true;
            }
            
            return false;
        }

        /// <summary>
        /// Vérifie si le roi d'une couleur est en échec
        /// </summary>
        public bool RoiEstEnEchec(Couleur couleur)
        {
            Piece? roi = TrouverRoi(couleur);
            if (roi == null)
                return false;

            Couleur couleurAdverse = couleur == Couleur.Blanc ? Couleur.Noir : Couleur.Blanc;
            return PositionEstAttaquee(roi.Position, couleurAdverse);
        }

        /// <summary>
        /// Initialise l'échiquier avec la position de départ
        /// </summary>
        public void InitialiserPositionDepart()
        {
            // Cette méthode sera implémentée dans la prochaine itération
            // avec la création des pièces spécifiques
        }

        /// <summary>
        /// Obtient une représentation textuelle de l'échiquier
        /// </summary>
        public override string ToString()
        {
            // Cette méthode sera implémentée dans l'itération 3 (Affichage)
            return "Échiquier - Affichage à implémenter";
        }
    }
}
