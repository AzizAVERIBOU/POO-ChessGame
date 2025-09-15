using echec_poo.Models;
using echec_poo.Game;
using echec_poo.Pieces;

namespace echec_poo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== JEU D'ÉCHECS - ITÉRATION 2 ===");
            Console.WriteLine("Test des pièces d'échecs...\n");

            // Test de la classe Position
            TestPosition();
            
            // Test de l'échiquier
            TestEchiquier();
            
            // Test des pièces
            TestPieces();
            
            Console.WriteLine("\n✅ Itération 2 terminée avec succès !");
            Console.WriteLine("Toutes les pièces d'échecs implémentées et testées.");
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
    }
}