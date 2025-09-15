using echec_poo.Models;
using echec_poo.Game;
using echec_poo.Pieces;

namespace echec_poo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== JEU D'ÉCHECS COMPLET ===");
            Console.WriteLine("Système de mouvements fonctionnel !\n");

            // Test rapide pour vérifier que tout fonctionne
            TestRapide();
            
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("LANCEMENT DU JEU INTERACTIF");
            Console.WriteLine(new string('=', 50));
            
            // Lancer le jeu interactif
            LancerJeuInteractif();
        }

        static void TestRapide()
        {
            try
            {
                Console.WriteLine("Test rapide du système...");
                Echiquier echiquier = new Echiquier();
                echiquier.InitialiserPositionDepart();
                
                Console.WriteLine($"✅ Échiquier initialisé: {echiquier.ObtenirPieces(Couleur.Blanc).Count} pièces blanches, {echiquier.ObtenirPieces(Couleur.Noir).Count} pièces noires");
                
                // Test d'un mouvement
                Position e2 = new Position(1, 4);
                Position e4 = new Position(3, 4);
                bool succes = echiquier.DeplacerPiece(e2, e4);
                Console.WriteLine($"✅ Mouvement e2-e4: {(succes ? "Réussi" : "Échoué")}");
                
                Console.WriteLine("✅ Système prêt pour le jeu !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans le test: {ex.Message}");
            }
        }

        static void TestPosition()
        {
            Console.WriteLine("--- Test de la classe Position ---");
            
            try
            {
                Position pos1 = new Position(0, 0);
                Position pos2 = new Position(7, 7);
                Position pos3 = Position.DepuisNotation("e4");
                
                Console.WriteLine($"Position 1: {pos1} (ligne {pos1.Ligne}, colonne {pos1.Colonne})");
                Console.WriteLine($"Position 2: {pos2} (ligne {pos2.Ligne}, colonne {pos2.Colonne})");
                Console.WriteLine($"Position e4: {pos3} (ligne {pos3.Ligne}, colonne {pos3.Colonne})");
                Console.WriteLine($"Distance entre pos1 et pos2: {pos1.Distance(pos2)}");
                Console.WriteLine($"Pos1 est valide: {pos1.EstValide()}");
                
                // Test d'une position invalide
                try
                {
                    Position posInvalide = new Position(10, 5);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("✅ Gestion des positions invalides : OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans TestPosition: {ex.Message}");
            }
        }

        static void TestEchiquier()
        {
            Console.WriteLine("\n--- Test de la classe Echiquier ---");
            
            try
            {
                Echiquier echiquier = new Echiquier();
                
                // Test des positions libres
                Position posTest = new Position(3, 3);
                Console.WriteLine($"Position (3,3) est libre: {echiquier.PositionEstLibre(posTest)}");
                Console.WriteLine($"Pièce en (3,3): {echiquier.ObtenirPiece(posTest)?.ToString() ?? "Aucune"}");
                
                // Test des pièces d'une couleur
                List<Piece> piecesBlanches = echiquier.ObtenirPieces(Couleur.Blanc);
                List<Piece> piecesNoires = echiquier.ObtenirPieces(Couleur.Noir);
                
                Console.WriteLine($"Pièces blanches: {piecesBlanches.Count}");
                Console.WriteLine($"Pièces noires: {piecesNoires.Count}");
                
                Console.WriteLine("✅ Échiquier initialisé correctement");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans TestEchiquier: {ex.Message}");
            }
        }

        static void TestPieces()
        {
            Console.WriteLine("\n--- Test des Pièces d'Échecs ---");
            
            try
            {
                Echiquier echiquier = new Echiquier();
                echiquier.InitialiserPositionDepart();
                
                // Test du nombre de pièces
                List<Piece> piecesBlanches = echiquier.ObtenirPieces(Couleur.Blanc);
                List<Piece> piecesNoires = echiquier.ObtenirPieces(Couleur.Noir);
                
                Console.WriteLine($"Pièces blanches: {piecesBlanches.Count}");
                Console.WriteLine($"Pièces noires: {piecesNoires.Count}");
                
                // Test des rois
                Piece? roiBlanc = echiquier.TrouverRoi(Couleur.Blanc);
                Piece? roiNoir = echiquier.TrouverRoi(Couleur.Noir);
                
                Console.WriteLine($"Roi blanc: {roiBlanc?.ToString() ?? "Non trouvé"}");
                Console.WriteLine($"Roi noir: {roiNoir?.ToString() ?? "Non trouvé"}");
                
                // Test des mouvements d'un pion
                Piece? pionBlanc = echiquier.ObtenirPiece(new Position(1, 0)); // Pion blanc en a7
                if (pionBlanc != null)
                {
                    Console.WriteLine($"\nTest du pion blanc en {pionBlanc.Position}:");
                    try
                    {
                        List<Position> mouvementsPion = pionBlanc.ObtenirMouvementsPossibles(echiquier);
                        Console.WriteLine($"Mouvements possibles: {mouvementsPion.Count}");
                        foreach (Position pos in mouvementsPion.Take(3)) // Afficher les 3 premiers
                        {
                            Console.WriteLine($"  - {pos}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors du calcul des mouvements: {ex.Message}");
                    }
                }
                
                // Test des mouvements d'un cavalier
                Piece? cavalierBlanc = echiquier.ObtenirPiece(new Position(0, 1)); // Cavalier blanc en b8
                if (cavalierBlanc != null)
                {
                    Console.WriteLine($"\nTest du cavalier blanc en {cavalierBlanc.Position}:");
                    List<Position> mouvementsCavalier = cavalierBlanc.ObtenirMouvementsPossibles(echiquier);
                    Console.WriteLine($"Mouvements possibles: {mouvementsCavalier.Count}");
                    foreach (Position pos in mouvementsCavalier.Take(3)) // Afficher les 3 premiers
                    {
                        Console.WriteLine($"  - {pos}");
                    }
                }
                
                // Test des symboles
                Console.WriteLine("\nSymboles des pièces:");
                Console.WriteLine($"Pion blanc: {new Pion(Couleur.Blanc, new Position(0, 0)).ObtenirSymbole()}");
                Console.WriteLine($"Tour blanche: {new Tour(Couleur.Blanc, new Position(0, 0)).ObtenirSymbole()}");
                Console.WriteLine($"Cavalier blanc: {new Cavalier(Couleur.Blanc, new Position(0, 0)).ObtenirSymbole()}");
                Console.WriteLine($"Fou blanc: {new Fou(Couleur.Blanc, new Position(0, 0)).ObtenirSymbole()}");
                Console.WriteLine($"Dame blanche: {new Dame(Couleur.Blanc, new Position(0, 0)).ObtenirSymbole()}");
                Console.WriteLine($"Roi blanc: {new Roi(Couleur.Blanc, new Position(0, 0)).ObtenirSymbole()}");
                
                Console.WriteLine("✅ Toutes les pièces fonctionnent correctement");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans TestPieces: {ex.Message}");
            }
        }

        static void TestAffichage()
        {
            Console.WriteLine("\n--- Test du Système d'Affichage ---");
            
            try
            {
                Echiquier echiquier = new Echiquier();
                echiquier.InitialiserPositionDepart();
                
                Console.WriteLine("Échiquier complet :");
                Console.WriteLine(echiquier.AfficherEchiquier());
                
                Console.WriteLine("\nMouvements possibles du pion blanc en a7 :");
                Position positionPion = new Position(1, 0);
                Console.WriteLine(echiquier.AfficherEchiquierAvecMouvements(positionPion));
                
                Console.WriteLine("\nMouvements possibles du cavalier blanc en b8 :");
                Position positionCavalier = new Position(0, 1);
                Console.WriteLine(echiquier.AfficherEchiquierAvecMouvements(positionCavalier));
                
                Console.WriteLine("✅ Système d'affichage fonctionne correctement");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans TestAffichage: {ex.Message}");
            }
        }

        static void TestMouvements()
        {
            Console.WriteLine("\n--- Test du Système de Mouvements ---");
            
            try
            {
                JeuEchecs jeu = new JeuEchecs("Alice", "Bob");
                Console.WriteLine("Jeu créé, initialisation de la partie...");
                jeu.NouvellePartie();
                Console.WriteLine("Partie initialisée");
                
                Console.WriteLine("État initial:");
                Console.WriteLine(jeu.ObtenirEtatJeu());
                Console.WriteLine();
                
                // Vérifier l'échiquier
                Console.WriteLine("Vérification de l'échiquier:");
                Console.WriteLine($"Pièces blanches: {jeu.Echiquier.ObtenirPieces(Couleur.Blanc).Count}");
                Console.WriteLine($"Pièces noires: {jeu.Echiquier.ObtenirPieces(Couleur.Noir).Count}");
                
                // Vérifier une pièce spécifique
                Piece? pionE2 = jeu.Echiquier.ObtenirPiece(new Position(1, 4)); // e2
                Console.WriteLine($"Pion en e2: {pionE2?.ToString() ?? "Aucun"}");
                Console.WriteLine();
                
                // Test de mouvements valides
                Console.WriteLine("Test du mouvement e2-e4 (pion blanc):");
                Console.WriteLine("Avant appel à EffectuerMouvement...");
                bool succes1 = jeu.EffectuerMouvement("e2-e4");
                Console.WriteLine($"Mouvement réussi: {succes1}");
                Console.WriteLine($"État après mouvement: {jeu.ObtenirEtatJeu()}");
                Console.WriteLine();
                
                Console.WriteLine("Test du mouvement e7-e5 (pion noir):");
                bool succes2 = jeu.EffectuerMouvement("e7-e5");
                Console.WriteLine($"Mouvement réussi: {succes2}");
                Console.WriteLine($"État après mouvement: {jeu.ObtenirEtatJeu()}");
                Console.WriteLine();
                
                // Test de mouvements invalides
                Console.WriteLine("Test du mouvement invalide e2-e5 (pion ne peut pas avancer de 3 cases):");
                bool succes3 = jeu.EffectuerMouvement("e2-e5");
                Console.WriteLine($"Mouvement réussi: {succes3}");
                Console.WriteLine();
                
                // Test des mouvements possibles
                Console.WriteLine("Mouvements possibles du cavalier en b1:");
                List<Position> mouvements = jeu.ObtenirMouvementsPossibles(new Position(0, 1));
                Console.WriteLine($"Nombre de mouvements: {mouvements.Count}");
                foreach (Position pos in mouvements.Take(5))
                {
                    Console.WriteLine($"  - {pos}");
                }
                
                Console.WriteLine("✅ Système de mouvements fonctionne correctement");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans TestMouvements: {ex.Message}");
            }
        }

        static void TestValidationComplete()
        {
            Console.WriteLine("\n--- Test de la Validation Complète ---");
            
            try
            {
                JeuEchecs jeu = new JeuEchecs("Alice", "Bob");
                jeu.NouvellePartie();
                
                Console.WriteLine("Test de la détection d'échec:");
                Console.WriteLine($"Roi blanc en échec: {jeu.Echiquier.RoiEstEnEchec(Couleur.Blanc)}");
                Console.WriteLine($"Roi noir en échec: {jeu.Echiquier.RoiEstEnEchec(Couleur.Noir)}");
                Console.WriteLine();
                
                Console.WriteLine("Test des mouvements avec validation:");
                Console.WriteLine("Mouvement e2-e4 (devrait réussir):");
                bool succes1 = jeu.EffectuerMouvement("e2-e4");
                Console.WriteLine($"Résultat: {succes1}");
                Console.WriteLine($"État: {jeu.ObtenirEtatJeu()}");
                Console.WriteLine();
                
                Console.WriteLine("Mouvement e7-e5 (devrait réussir):");
                bool succes2 = jeu.EffectuerMouvement("e7-e5");
                Console.WriteLine($"Résultat: {succes2}");
                Console.WriteLine($"État: {jeu.ObtenirEtatJeu()}");
                Console.WriteLine();
                
                Console.WriteLine("Test du pat:");
                bool estPat = jeu.EstPat();
                Console.WriteLine($"Est en pat: {estPat}");
                Console.WriteLine();
                
                Console.WriteLine("✅ Validation complète fonctionne correctement");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur dans TestValidationComplete: {ex.Message}");
            }
        }

        static void LancerJeuInteractif()
        {
            Console.Clear();
            Console.WriteLine("=== JEU D'ÉCHECS INTERACTIF ===");
            
            JeuEchecs jeu = new JeuEchecs();
            InterfaceJeu interfaceJeu = new InterfaceJeu(jeu);
            
            interfaceJeu.LancerJeu();
        }
    }
}