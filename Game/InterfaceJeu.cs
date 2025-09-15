using echec_poo.Models;

namespace echec_poo.Game
{
    /// <summary>
    /// Interface utilisateur pour le jeu d'échecs
    /// </summary>
    public class InterfaceJeu
    {
        private JeuEchecs _jeu;

        public InterfaceJeu(JeuEchecs jeu)
        {
            _jeu = jeu;
        }

        /// <summary>
        /// Affiche le menu principal
        /// </summary>
        public void AfficherMenu()
        {
            Console.Clear();
            Console.WriteLine("=== JEU D'ÉCHECS ===");
            Console.WriteLine($"État: {_jeu.ObtenirEtatJeu()}");
            Console.WriteLine();
            Console.WriteLine("1. Nouvelle partie");
            Console.WriteLine("2. Afficher l'échiquier");
            Console.WriteLine("3. Jouer un coup");
            Console.WriteLine("4. Afficher les mouvements d'une pièce");
            Console.WriteLine("5. Quitter");
            Console.WriteLine();
        }

        /// <summary>
        /// Affiche l'échiquier avec l'état du jeu
        /// </summary>
        public void AfficherEchiquier()
        {
            Console.Clear();
            Console.WriteLine(_jeu.ObtenirEtatJeu());
            Console.WriteLine();
            
            // Vérifier que l'échiquier est initialisé
            int piecesBlanches = _jeu.Echiquier.ObtenirPieces(Couleur.Blanc).Count;
            int piecesNoires = _jeu.Echiquier.ObtenirPieces(Couleur.Noir).Count;
            
            if (piecesBlanches == 0 && piecesNoires == 0)
            {
                Console.WriteLine("⚠️  L'échiquier est vide ! Création d'une nouvelle partie...");
                _jeu.NouvellePartie();
            }
            
            // Afficher l'échiquier avec indication des pièces du joueur actuel
            Console.WriteLine(_jeu.AfficherEchiquierAvecJoueurActuel());
            Console.WriteLine();
        }

        /// <summary>
        /// Demande et effectue un mouvement
        /// </summary>
        public void JouerCoup()
        {
            Console.Clear();
            Console.WriteLine(_jeu.ObtenirEtatJeu());
            Console.WriteLine();
            Console.WriteLine(_jeu.AfficherEchiquierAvecJoueurActuel());
            Console.WriteLine();
            Console.WriteLine("Entrez votre mouvement (ex: e2-e4 ou e2e4):");
            Console.Write("> ");

            string? mouvement = Console.ReadLine();
            if (string.IsNullOrEmpty(mouvement))
            {
                Console.WriteLine("Mouvement invalide!");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            bool succes = _jeu.EffectuerMouvement(mouvement);
            
            if (succes)
            {
                Console.WriteLine("Mouvement effectué avec succès!");
            }
            else
            {
                Console.WriteLine("Mouvement invalide! Vérifiez les règles d'échecs.");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        /// <summary>
        /// Affiche les mouvements possibles d'une pièce
        /// </summary>
        public void AfficherMouvementsPiece()
        {
            Console.Clear();
            Console.WriteLine("Entrez la position de la pièce (ex: e2):");
            Console.Write("> ");

            string? positionStr = Console.ReadLine();
            if (string.IsNullOrEmpty(positionStr))
            {
                Console.WriteLine("Position invalide!");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            try
            {
                Position position = Position.DepuisNotation(positionStr);
                List<Position> mouvements = _jeu.ObtenirMouvementsPossibles(position);
                
                Console.Clear();
                Console.WriteLine($"Mouvements possibles pour la pièce en {positionStr}:");
                Console.WriteLine();
                Console.WriteLine(_jeu.AfficherAvecMouvements(position));
                Console.WriteLine();
                Console.WriteLine($"Nombre de mouvements possibles: {mouvements.Count}");
                
                if (mouvements.Count > 0)
                {
                    Console.WriteLine("Mouvements:");
                    foreach (Position pos in mouvements.Take(10)) // Afficher les 10 premiers
                    {
                        Console.WriteLine($"  - {pos}");
                    }
                    if (mouvements.Count > 10)
                    {
                        Console.WriteLine($"  ... et {mouvements.Count - 10} autres");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Position invalide!");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        /// <summary>
        /// Lance le jeu interactif
        /// </summary>
        public void LancerJeu()
        {
            bool continuer = true;
            
            while (continuer)
            {
                AfficherMenu();
                Console.Write("Votre choix: ");
                
                string? choix = Console.ReadLine();
                
                switch (choix)
                {
                    case "1":
                        _jeu.NouvellePartie();
                        Console.WriteLine("Nouvelle partie créée!");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                        
                    case "2":
                        AfficherEchiquier();
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                        
                    case "3":
                        JouerCoup();
                        break;
                        
                    case "4":
                        AfficherMouvementsPiece();
                        break;
                        
                    case "5":
                        continuer = false;
                        break;
                        
                    default:
                        Console.WriteLine("Choix invalide!");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
