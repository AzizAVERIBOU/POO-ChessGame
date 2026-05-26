using echec_poo.Models;
using echec_poo.Pieces;

namespace echec_poo.Game
{
    /// <summary>
    /// Représente l'échiquier et gère les pièces
    /// Démonstration des concepts POO : composition, encapsulation
    /// </summary>
    public class Echiquier
    {
        private Piece?[,] _pieces;

        /// <summary>
        /// Case où un pion adverse peut être pris en passant (case traversée par un double pas),
        /// valable uniquement pour le prochain demi-coup.
        /// </summary>
        public Position? CasePriseEnPassant { get; set; }

        public bool BlancRoquePetit { get; set; }
        public bool BlancRoqueGrand { get; set; }
        public bool NoirRoquePetit { get; set; }
        public bool NoirRoqueGrand { get; set; }

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

            if (EstDeplacementRoque2Cases(depart, arrivee))
                return ExecuterRoque(depart, arrivee);

            bool priseEp = EstCoupPriseEnPassant(depart, arrivee);
            Position? casePionPrisEp = null;
            if (priseEp)
            {
                int dir = piece.Couleur == Couleur.Blanc ? 1 : -1;
                casePionPrisEp = Position.CreerSiValide(arrivee.Ligne - dir, arrivee.Colonne);
                if (casePionPrisEp == null)
                    return false;
            }

            RetirerPiece(depart);

            if (priseEp)
                RetirerPiece(casePionPrisEp!);
            else
            {
                Piece? capturee = ObtenirPiece(arrivee);
                if (capturee != null)
                    RetirerPiece(arrivee);
            }

            piece.DeplacerVers(arrivee);
            PlacerPiece(piece);

            return true;
        }

        /// <summary>
        /// Indique si le déplacement est un roque de deux cases (roi e → g ou e → c sur la rangée initiale).
        /// </summary>
        public bool EstDeplacementRoque2Cases(Position depart, Position arrivee)
        {
            Piece? p = ObtenirPiece(depart);
            if (p is not Roi)
                return false;
            int ligneFond = p.Couleur == Couleur.Blanc ? 0 : 7;
            if (depart.Ligne != ligneFond || depart.Colonne != 4)
                return false;
            if (arrivee.Ligne != depart.Ligne || Math.Abs(arrivee.Colonne - depart.Colonne) != 2)
                return false;
            return arrivee.Colonne is 2 or 6;
        }

        public bool EstCoupRoquePetit(Position depart, Position arrivee) =>
            EstDeplacementRoque2Cases(depart, arrivee) && arrivee.Colonne > depart.Colonne;

        public bool EstCoupRoqueGrand(Position depart, Position arrivee) =>
            EstDeplacementRoque2Cases(depart, arrivee) && arrivee.Colonne < depart.Colonne;

        /// <summary>
        /// Roque côté roi : e1-g1 / e8-g8, chemins libres, roi non en échec, cases traversées non attaquées.
        /// </summary>
        public bool PeutRoquerPetit(Couleur couleur)
        {
            if (couleur == Couleur.Blanc && !BlancRoquePetit)
                return false;
            if (couleur == Couleur.Noir && !NoirRoquePetit)
                return false;

            int ligne = couleur == Couleur.Blanc ? 0 : 7;
            Couleur adverse = couleur == Couleur.Blanc ? Couleur.Noir : Couleur.Blanc;

            Piece? roi = ObtenirPiece(new Position(ligne, 4));
            Piece? tour = ObtenirPiece(new Position(ligne, 7));
            if (roi is not Roi || tour is not Tour || tour.Couleur != couleur || roi.Couleur != couleur)
                return false;
            if (roi.ADejaBouge || tour.ADejaBouge)
                return false;

            if (!PositionEstLibre(new Position(ligne, 5)) || !PositionEstLibre(new Position(ligne, 6)))
                return false;

            if (RoiEstEnEchec(couleur))
                return false;
            if (PositionEstAttaquee(new Position(ligne, 5), adverse) || PositionEstAttaquee(new Position(ligne, 6), adverse))
                return false;

            return true;
        }

        /// <summary>
        /// Roque côté dame : e1-c1 / e8-c8.
        /// </summary>
        public bool PeutRoquerGrand(Couleur couleur)
        {
            if (couleur == Couleur.Blanc && !BlancRoqueGrand)
                return false;
            if (couleur == Couleur.Noir && !NoirRoqueGrand)
                return false;

            int ligne = couleur == Couleur.Blanc ? 0 : 7;
            Couleur adverse = couleur == Couleur.Blanc ? Couleur.Noir : Couleur.Blanc;

            Piece? roi = ObtenirPiece(new Position(ligne, 4));
            Piece? tour = ObtenirPiece(new Position(ligne, 0));
            if (roi is not Roi || tour is not Tour || tour.Couleur != couleur || roi.Couleur != couleur)
                return false;
            if (roi.ADejaBouge || tour.ADejaBouge)
                return false;

            if (!PositionEstLibre(new Position(ligne, 1)) || !PositionEstLibre(new Position(ligne, 2)) ||
                !PositionEstLibre(new Position(ligne, 3)))
                return false;

            if (RoiEstEnEchec(couleur))
                return false;
            if (PositionEstAttaquee(new Position(ligne, 3), adverse) || PositionEstAttaquee(new Position(ligne, 2), adverse))
                return false;

            return true;
        }

        private bool ExecuterRoque(Position departRoi, Position arriveeRoi)
        {
            bool petit = arriveeRoi.Colonne > departRoi.Colonne;
            int ligne = departRoi.Ligne;
            int colTourDepart = petit ? 7 : 0;
            int colTourArrivee = petit ? 5 : 3;

            Piece? roi = ObtenirPiece(departRoi);
            Position posTour = new Position(ligne, colTourDepart);
            Piece? tour = ObtenirPiece(posTour);
            if (roi is not Roi || tour is not Tour)
                return false;

            RetirerPiece(departRoi);
            RetirerPiece(posTour);
            roi.DeplacerVers(arriveeRoi);
            tour.DeplacerVers(new Position(ligne, colTourArrivee));
            PlacerPiece(roi);
            PlacerPiece(tour);

            return true;
        }

        /// <summary>
        /// Met à jour les droits au roque après un demi-coup (roi, tour, ou prise d'une tour sur coin initial).
        /// </summary>
        public void MettreAJourDroitsRoqueApresDemiCoup(Position depart, Position arrivee, Piece pieceDeplacee, Piece? captureeSurArrivee)
        {
            if (pieceDeplacee is Roi)
            {
                if (pieceDeplacee.Couleur == Couleur.Blanc)
                {
                    BlancRoquePetit = false;
                    BlancRoqueGrand = false;
                }
                else
                {
                    NoirRoquePetit = false;
                    NoirRoqueGrand = false;
                }
            }
            else if (pieceDeplacee is Tour)
            {
                if (depart.Ligne == 0 && depart.Colonne == 7)
                    BlancRoquePetit = false;
                if (depart.Ligne == 0 && depart.Colonne == 0)
                    BlancRoqueGrand = false;
                if (depart.Ligne == 7 && depart.Colonne == 7)
                    NoirRoquePetit = false;
                if (depart.Ligne == 7 && depart.Colonne == 0)
                    NoirRoqueGrand = false;
            }

            if (captureeSurArrivee is Tour)
            {
                if (captureeSurArrivee.Couleur == Couleur.Blanc)
                {
                    if (arrivee.Ligne == 0 && arrivee.Colonne == 7)
                        BlancRoquePetit = false;
                    if (arrivee.Ligne == 0 && arrivee.Colonne == 0)
                        BlancRoqueGrand = false;
                }
                else
                {
                    if (arrivee.Ligne == 7 && arrivee.Colonne == 7)
                        NoirRoquePetit = false;
                    if (arrivee.Ligne == 7 && arrivee.Colonne == 0)
                        NoirRoqueGrand = false;
                }
            }
        }

        /// <summary>
        /// Indique si le déplacement est une prise en passant (case d'arrivée vide, cible <see cref="CasePriseEnPassant"/>).
        /// </summary>
        public bool EstCoupPriseEnPassant(Position depart, Position arrivee)
        {
            Piece? p = ObtenirPiece(depart);
            if (p is not Pion || CasePriseEnPassant is null || !arrivee.Equals(CasePriseEnPassant))
                return false;
            if (ObtenirPiece(arrivee) != null)
                return false;

            int dir = p.Couleur == Couleur.Blanc ? 1 : -1;
            if (arrivee.Ligne - depart.Ligne != dir || Math.Abs(arrivee.Colonne - depart.Colonne) != 1)
                return false;

            Position? casePionPris = Position.CreerSiValide(arrivee.Ligne - dir, arrivee.Colonne);
            if (casePionPris == null)
                return false;

            Piece? adjacent = ObtenirPiece(casePionPris);
            return adjacent is Pion ap && ap.Couleur != p.Couleur;
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
            CasePriseEnPassant = null;
            BlancRoquePetit = BlancRoqueGrand = true;
            NoirRoquePetit = NoirRoqueGrand = true;
            // Vider l'échiquier
            _pieces = new Piece?[8, 8];

            // Pièces noires (en haut - ligne 7 = rangée 8 affichée)
            PlacerPiece(new Pieces.Tour(Couleur.Noir, new Position(7, 0)));
            PlacerPiece(new Pieces.Cavalier(Couleur.Noir, new Position(7, 1)));
            PlacerPiece(new Pieces.Fou(Couleur.Noir, new Position(7, 2)));
            PlacerPiece(new Pieces.Dame(Couleur.Noir, new Position(7, 3)));
            PlacerPiece(new Pieces.Roi(Couleur.Noir, new Position(7, 4)));
            PlacerPiece(new Pieces.Fou(Couleur.Noir, new Position(7, 5)));
            PlacerPiece(new Pieces.Cavalier(Couleur.Noir, new Position(7, 6)));
            PlacerPiece(new Pieces.Tour(Couleur.Noir, new Position(7, 7)));

            // Pions noirs (ligne 6 = rangée 7 affichée)
            for (int colonne = 0; colonne < 8; colonne++)
            {
                PlacerPiece(new Pieces.Pion(Couleur.Noir, new Position(6, colonne)));
            }

            // Pions blancs (ligne 1 = rangée 2 affichée)
            for (int colonne = 0; colonne < 8; colonne++)
            {
                PlacerPiece(new Pieces.Pion(Couleur.Blanc, new Position(1, colonne)));
            }

            // Pièces blanches (en bas - ligne 0 = rangée 1 affichée)
            PlacerPiece(new Pieces.Tour(Couleur.Blanc, new Position(0, 0)));
            PlacerPiece(new Pieces.Cavalier(Couleur.Blanc, new Position(0, 1)));
            PlacerPiece(new Pieces.Fou(Couleur.Blanc, new Position(0, 2)));
            PlacerPiece(new Pieces.Dame(Couleur.Blanc, new Position(0, 3)));
            PlacerPiece(new Pieces.Roi(Couleur.Blanc, new Position(0, 4)));
            PlacerPiece(new Pieces.Fou(Couleur.Blanc, new Position(0, 5)));
            PlacerPiece(new Pieces.Cavalier(Couleur.Blanc, new Position(0, 6)));
            PlacerPiece(new Pieces.Tour(Couleur.Blanc, new Position(0, 7)));
        }

        /// <summary>
        /// Obtient une représentation textuelle de l'échiquier
        /// </summary>
        public override string ToString()
        {
            return AfficherEchiquier();
        }

        /// <summary>
        /// Affiche l'échiquier avec les pièces et les coordonnées
        /// </summary>
        public string AfficherEchiquier()
        {
            return AfficherEchiquier(null);
        }

        /// <summary>
        /// Affiche l'échiquier avec indication des pièces du joueur actuel
        /// </summary>
        public string AfficherEchiquier(Couleur? couleurJoueurActuel)
        {
            var sb = new System.Text.StringBuilder();
            
            // En-tête avec les lettres des colonnes
            sb.AppendLine("    a   b   c   d   e   f   g   h");
            sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            
            // Affichage du haut (rangée 8, Ligne 7) vers le bas (rangée 1, Ligne 0) — numéros FIDE
            for (int ligne = 7; ligne >= 0; ligne--)
            {
                int numeroAffiche = ligne + 1;
                sb.Append($"{numeroAffiche} |");
                
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Piece? piece = _pieces[ligne, colonne];
                    string symbole = piece?.ObtenirSymbole() ?? " ";
                    
                    // Mettre en évidence les pièces du joueur actuel
                    if (piece != null && couleurJoueurActuel.HasValue && piece.Couleur == couleurJoueurActuel.Value)
                    {
                        sb.Append($"*{symbole}*|"); // Entourer les pièces du joueur actuel
                    }
                    else
                    {
                        sb.Append($" {symbole} |");
                    }
                }
                
                sb.AppendLine($" {numeroAffiche}");
                sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            }
            
            // Pied de page avec les lettres des colonnes
            sb.AppendLine("    a   b   c   d   e   f   g   h");
            
            // Légende
            if (couleurJoueurActuel.HasValue)
            {
                sb.AppendLine();
                sb.AppendLine($"Pièces du joueur actuel ({couleurJoueurActuel.Value}) : *P* (entourées d'astérisques)");
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Affiche l'échiquier avec les mouvements possibles d'une pièce
        /// </summary>
        public string AfficherEchiquierAvecMouvements(Position positionPiece)
        {
            return AfficherEchiquierAvecMouvements(positionPiece, null);
        }

        /// <summary>
        /// Affiche l'échiquier avec les mouvements possibles d'une pièce et indication du joueur actuel.
        /// Si <paramref name="mouvementsSurcharges"/> est fourni (ex. coups légaux filtrés), il remplace le calcul brut.
        /// </summary>
        public string AfficherEchiquierAvecMouvements(Position positionPiece, Couleur? couleurJoueurActuel, IReadOnlyList<Position>? mouvementsSurcharges = null)
        {
            var sb = new System.Text.StringBuilder();
            Piece? piece = ObtenirPiece(positionPiece);
            
            if (piece == null)
                return AfficherEchiquier(couleurJoueurActuel);
            
            List<Position> mouvements = mouvementsSurcharges != null
                ? new List<Position>(mouvementsSurcharges)
                : piece.ObtenirMouvementsPossibles(this);
            
            // En-tête avec les lettres des colonnes
            sb.AppendLine("    a   b   c   d   e   f   g   h");
            sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            
            // Affichage du haut (rangée 8, Ligne 7) vers le bas (rangée 1, Ligne 0) — numéros FIDE
            for (int ligne = 7; ligne >= 0; ligne--)
            {
                int numeroAffiche = ligne + 1;
                sb.Append($"{numeroAffiche} |");
                
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Position pos = new Position(ligne, colonne);
                    Piece? pieceActuelle = _pieces[ligne, colonne];
                    
                    if (pieceActuelle != null)
                    {
                        string symbole = pieceActuelle.ObtenirSymbole();
                        
                        // Mettre en évidence les pièces du joueur actuel
                        if (couleurJoueurActuel.HasValue && pieceActuelle.Couleur == couleurJoueurActuel.Value)
                        {
                            sb.Append($"*{symbole}*|"); // Entourer les pièces du joueur actuel
                        }
                        else
                        {
                            sb.Append($" {symbole} |");
                        }
                    }
                    else if (mouvements.Contains(pos))
                    {
                        sb.Append(" • |"); // Point pour les mouvements possibles
                    }
                    else
                    {
                        sb.Append("   |");
                    }
                }
                
                sb.AppendLine($" {numeroAffiche}");
                sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            }
            
            // Pied de page avec les lettres des colonnes
            sb.AppendLine("    a   b   c   d   e   f   g   h");
            
            // Légende
            if (couleurJoueurActuel.HasValue)
            {
                sb.AppendLine();
                sb.AppendLine($"Pièces du joueur actuel ({couleurJoueurActuel.Value}) : *P* (entourées d'astérisques)");
                sb.AppendLine("• = Mouvements possibles de la pièce sélectionnée");
            }
            
            return sb.ToString();
        }
    }
}
