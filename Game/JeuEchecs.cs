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
            
            // Initialiser automatiquement la partie
            NouvellePartie();
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
#if DEBUG
                Console.WriteLine($"🔍 Débogage: Tentative de mouvement '{mouvement}'");
#endif

                // Parser le mouvement (format: "e2-e4" ou "e2e4")
                var (depart, arrivee) = ParserMouvement(mouvement);
                
                if (depart == null || arrivee == null)
                {
#if DEBUG
                    Console.WriteLine("❌ Échec: Impossible de parser le mouvement");
#endif
                    return false;
                }
                
#if DEBUG
                Console.WriteLine($"🔍 Débogage: Départ = {depart}, Arrivée = {arrivee}");
#endif

                // Vérifier que c'est au tour du bon joueur
                Piece? piece = Echiquier.ObtenirPiece(depart);
                if (piece == null)
                {
#if DEBUG
                    Console.WriteLine("❌ Échec: Aucune pièce en position de départ");
#endif
                    return false;
                }
                
                if (piece.Couleur != JoueurActuel.Couleur)
                {
#if DEBUG
                    Console.WriteLine($"❌ Échec: Ce n'est pas au tour de cette pièce (couleur: {piece.Couleur}, tour: {JoueurActuel.Couleur})");
#endif
                    return false;
                }
                
#if DEBUG
                Console.WriteLine($"🔍 Débogage: Pièce trouvée: {piece.ObtenirNom()} {piece.Couleur}");
#endif

                // Vérifier que le mouvement est valide
                bool mouvementValide = piece.PeutSeDeplacerVers(arrivee, Echiquier);
#if DEBUG
                Console.WriteLine($"🔍 Débogage: Mouvement valide selon les règles: {mouvementValide}");
#endif
                
                if (!mouvementValide)
                    return false;

                // Vérifier que le mouvement ne met pas le roi en échec
                bool metRoiEnEchec = MouvementMetRoiEnEchec(depart, arrivee);
#if DEBUG
                Console.WriteLine($"🔍 Débogage: Met le roi en échec: {metRoiEnEchec}");
#endif
                
                if (metRoiEnEchec)
                    return false;

                // Effectuer le mouvement
                bool mouvementReussi = Echiquier.DeplacerPiece(depart, arrivee);
#if DEBUG
                Console.WriteLine($"🔍 Débogage: Déplacement réussi: {mouvementReussi}");
#endif
                
                if (mouvementReussi)
                {
                    // Changer de joueur
                    ChangerTour();
                    
                    // Vérifier l'échec
                    VerifierEchec();
                    
                    // Vérifier l'échec et mat
                    VerifierEchecEtMat();
                }

                return mouvementReussi;
            }
#if DEBUG
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur: {ex.Message}");
                return false;
            }
#else
            catch (Exception)
            {
                return false;
            }
#endif
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
        /// Vérifie si un mouvement met le roi en échec
        /// </summary>
        private bool MouvementMetRoiEnEchec(Position depart, Position arrivee)
        {
            Piece? piece = Echiquier.ObtenirPiece(depart);
            if (piece == null)
                return false;

            Coup coup = Coup.Normal(depart, arrivee);
            return SimulationPlateau.RoiSeraitEnEchecApresCoup(Echiquier, coup);
        }

        /// <summary>
        /// Vérifie l'échec et mat
        /// </summary>
        private void VerifierEchecEtMat()
        {
            Couleur couleurAdverse = JoueurActuel.Couleur == Couleur.Blanc ? Couleur.Noir : Couleur.Blanc;
            
            if (Echiquier.RoiEstEnEchec(couleurAdverse))
            {
                // Vérifier si le joueur adverse peut sortir de l'échec
                if (!PeutSortirDeEchec(couleurAdverse))
                {
                    PartieTerminee = true;
                    Gagnant = JoueurActuel.Nom;
                }
            }
        }

        /// <summary>
        /// Vérifie si un joueur peut sortir de l'échec
        /// </summary>
        private bool PeutSortirDeEchec(Couleur couleur)
        {
            // Pour l'instant, simplifier cette méthode
            List<Piece> pieces = Echiquier.ObtenirPieces(couleur);
            return pieces.Count > 0; // Si il y a des pièces, on peut théoriquement bouger
        }

        /// <summary>
        /// Vérifie le pat (nul)
        /// </summary>
        public bool EstPat()
        {
            if (Echiquier.RoiEstEnEchec(JoueurActuel.Couleur))
                return false; // Pas de pat si en échec

            return !PeutSortirDeEchec(JoueurActuel.Couleur);
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
            return Echiquier.AfficherEchiquierAvecMouvements(position, JoueurActuel.Couleur);
        }

        /// <summary>
        /// Affiche l'échiquier avec indication des pièces du joueur actuel
        /// </summary>
        public string AfficherEchiquierAvecJoueurActuel()
        {
            return Echiquier.AfficherEchiquier(JoueurActuel.Couleur);
        }
    }
}
