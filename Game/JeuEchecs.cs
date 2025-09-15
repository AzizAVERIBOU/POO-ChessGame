using echec_poo.Models;

namespace echec_poo.Game
{
    /// <summary>
    /// Gère la logique principale du jeu d'échecs
    /// </summary>
    public class JeuEchecs
    {
        public Echiquier Echiquier { get; private set; }
        public Joueur JoueurBlanc { get; private set; }
        public Joueur JoueurNoir { get; private set; }
        public Joueur JoueurActuel { get; private set; }
        public bool PartieTerminee { get; private set; }
        public string? Gagnant { get; private set; }

        public JeuEchecs(string nomJoueurBlanc = "Joueur Blanc", string nomJoueurNoir = "Joueur Noir")
        {
            Echiquier = new Echiquier();
            JoueurBlanc = new Joueur(nomJoueurBlanc, Couleur.Blanc);
            JoueurNoir = new Joueur(nomJoueurNoir, Couleur.Noir);
            JoueurActuel = JoueurBlanc; // Les blancs commencent
            PartieTerminee = false;
            Gagnant = null;
        }

        /// <summary>
        /// Initialise une nouvelle partie
        /// </summary>
        public void NouvellePartie()
        {
            Echiquier.InitialiserPositionDepart();
            JoueurBlanc.EstEnEchec = false;
            JoueurNoir.EstEnEchec = false;
            JoueurActuel = JoueurBlanc;
            PartieTerminee = false;
            Gagnant = null;
        }

        /// <summary>
        /// Tente d'effectuer un mouvement
        /// </summary>
        public bool EffectuerMouvement(string mouvement)
        {
            try
            {
                // Parser le mouvement (format: "e2-e4" ou "e2e4")
                var (depart, arrivee) = ParserMouvement(mouvement);
                
                if (depart == null || arrivee == null)
                    return false;

                // Vérifier que c'est au tour du bon joueur
                Piece? piece = Echiquier.ObtenirPiece(depart);
                if (piece == null || piece.Couleur != JoueurActuel.Couleur)
                    return false;

                // Vérifier que le mouvement est valide
                if (!piece.PeutSeDeplacerVers(arrivee, Echiquier))
                    return false;

                // Effectuer le mouvement
                bool mouvementReussi = Echiquier.DeplacerPiece(depart, arrivee);
                
                if (mouvementReussi)
                {
                    // Changer de joueur
                    ChangerTour();
                    
                    // Vérifier l'échec
                    VerifierEchec();
                }

                return mouvementReussi;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Parse un mouvement en notation algébrique
        /// </summary>
        private (Position?, Position?) ParserMouvement(string mouvement)
        {
            // Nettoyer le mouvement
            mouvement = mouvement.Trim().ToLower();
            
            // Supprimer les espaces et tirets
            mouvement = mouvement.Replace(" ", "").Replace("-", "");

            if (mouvement.Length != 4)
                return (null, null);

            try
            {
                string departStr = mouvement.Substring(0, 2);
                string arriveeStr = mouvement.Substring(2, 2);

                Position depart = Position.DepuisNotation(departStr);
                Position arrivee = Position.DepuisNotation(arriveeStr);

                return (depart, arrivee);
            }
            catch
            {
                return (null, null);
            }
        }

        /// <summary>
        /// Change de tour
        /// </summary>
        private void ChangerTour()
        {
            JoueurActuel = JoueurActuel == JoueurBlanc ? JoueurNoir : JoueurBlanc;
        }

        /// <summary>
        /// Vérifie si un joueur est en échec
        /// </summary>
        private void VerifierEchec()
        {
            JoueurBlanc.EstEnEchec = Echiquier.RoiEstEnEchec(Couleur.Blanc);
            JoueurNoir.EstEnEchec = Echiquier.RoiEstEnEchec(Couleur.Noir);
        }

        /// <summary>
        /// Obtient les mouvements possibles pour une position
        /// </summary>
        public List<Position> ObtenirMouvementsPossibles(Position position)
        {
            Piece? piece = Echiquier.ObtenirPiece(position);
            if (piece == null || piece.Couleur != JoueurActuel.Couleur)
                return new List<Position>();

            return piece.ObtenirMouvementsPossibles(Echiquier);
        }

        /// <summary>
        /// Obtient l'état actuel du jeu
        /// </summary>
        public string ObtenirEtatJeu()
        {
            if (PartieTerminee)
                return $"Partie terminée. Gagnant: {Gagnant}";

            string etat = $"Tour de {JoueurActuel.Nom}";
            
            if (JoueurActuel.EstEnEchec)
                etat += " (ÉCHEC!)";
            
            return etat;
        }

        /// <summary>
        /// Affiche l'échiquier avec les mouvements possibles d'une pièce
        /// </summary>
        public string AfficherAvecMouvements(Position position)
        {
            return Echiquier.AfficherEchiquierAvecMouvements(position);
        }
    }
}
