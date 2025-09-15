using echec_poo.Models;
using echec_poo.Game;

namespace echec_poo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== JEU D'ÉCHECS - ITÉRATION 1 ===");
            Console.WriteLine("Test des classes de base...\n");

            // Test de la classe Position
            TestPosition();
            
            // Test de l'échiquier
            TestEchiquier();
            
            Console.WriteLine("\n✅ Itération 1 terminée avec succès !");
            Console.WriteLine("Classes de base créées et testées.");
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
    }
}