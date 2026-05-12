using echec_poo.Models;
using echec_poo.Pieces;

namespace echec_poo.Game
{
    /// <summary>
    /// Applique et annule des coups sur l'échiquier pour la simulation (échec au roi, futur mat, etc.).
    /// </summary>
    internal static class SimulationPlateau
    {
        internal readonly record struct SnapshotNormal(
            Piece PieceDeplacee,
            Piece? PieceCapturee,
            Position Depart,
            bool ADejaBougeAvant);

        /// <summary>
        /// Indique si le roi du camp qui joue le coup serait en échec après application.
        /// </summary>
        public static bool RoiSeraitEnEchecApresCoup(Echiquier echiquier, Coup coup)
        {
            return coup.Type switch
            {
                TypeCoup.Normal => EvaluerNormal(echiquier, coup),
                TypeCoup.PriseEnPassant => EvaluerPriseEnPassant(echiquier, coup),
                TypeCoup.RoquePetit or TypeCoup.RoqueGrand or TypeCoup.Promotion =>
                    throw new NotSupportedException(
                        $"Le type de coup {coup.Type} sera pris en charge dans une phase ultérieure."),
                _ => throw new ArgumentOutOfRangeException(nameof(coup), coup.Type, "Valeur d'énumération inconnue.")
            };
        }

        private static bool EvaluerPriseEnPassant(Echiquier echiquier, Coup coup)
        {
            Piece? piece = echiquier.ObtenirPiece(coup.Depart);
            if (piece == null || piece is not Pion)
                return false;

            Couleur camp = piece.Couleur;
            bool aDejaBougeAvant = piece.ADejaBouge;
            int dir = camp == Couleur.Blanc ? 1 : -1;
            Position casePionPris = new Position(coup.Arrivee.Ligne - dir, coup.Arrivee.Colonne);
            Piece? pionPris = echiquier.ObtenirPiece(casePionPris);
            Position? pepAvant = echiquier.CasePriseEnPassant;

            echiquier.RetirerPiece(coup.Depart);
            echiquier.RetirerPiece(casePionPris);
            piece.DeplacerVers(coup.Arrivee);
            echiquier.PlacerPiece(piece);
            echiquier.CasePriseEnPassant = null;

            bool enEchec = echiquier.RoiEstEnEchec(camp);

            echiquier.RetirerPiece(coup.Arrivee);
            piece.RestaurerEtatApresSimulation(coup.Depart, aDejaBougeAvant);
            echiquier.PlacerPiece(piece);
            if (pionPris != null)
                echiquier.PlacerPiece(pionPris);
            echiquier.CasePriseEnPassant = pepAvant;

            return enEchec;
        }

        private static bool EvaluerNormal(Echiquier echiquier, Coup coup)
        {
            Piece? piece = echiquier.ObtenirPiece(coup.Depart);
            if (piece == null)
                return false;

            Couleur camp = piece.Couleur;
            SnapshotNormal snap = AppliquerNormal(echiquier, coup);
            bool enEchec = echiquier.RoiEstEnEchec(camp);
            AnnulerNormal(echiquier, snap);
            return enEchec;
        }

        /// <summary>Applique un coup normal (y compris capture sur la case d'arrivée).</summary>
        public static SnapshotNormal AppliquerNormal(Echiquier echiquier, Coup coup)
        {
            if (coup.Type != TypeCoup.Normal)
                throw new ArgumentException("Seul un coup normal est supporté.", nameof(coup));

            Piece? piece = echiquier.ObtenirPiece(coup.Depart);
            if (piece == null)
                throw new InvalidOperationException("Aucune pièce sur la case de départ.");

            Piece? capturee = echiquier.ObtenirPiece(coup.Arrivee);
            bool aDejaBougeAvant = piece.ADejaBouge;

            echiquier.RetirerPiece(coup.Depart);
            if (capturee != null)
                echiquier.RetirerPiece(coup.Arrivee);

            piece.DeplacerVers(coup.Arrivee);
            echiquier.PlacerPiece(piece);

            return new SnapshotNormal(piece, capturee, coup.Depart, aDejaBougeAvant);
        }

        public static void AnnulerNormal(Echiquier echiquier, SnapshotNormal snap)
        {
            Position caseArrivee = snap.PieceDeplacee.Position;

            echiquier.RetirerPiece(caseArrivee);
            snap.PieceDeplacee.RestaurerEtatApresSimulation(snap.Depart, snap.ADejaBougeAvant);
            echiquier.PlacerPiece(snap.PieceDeplacee);

            if (snap.PieceCapturee != null)
                echiquier.PlacerPiece(snap.PieceCapturee);
        }
    }
}
