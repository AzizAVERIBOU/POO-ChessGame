namespace echec_poo.Models
{
    /// <summary>
    /// Représente une position sur l'échiquier
    /// </summary>
    public class Position
    {
        public int Ligne { get; private set; }
        public int Colonne { get; private set; }

        public Position(int ligne, int colonne)
        {
            if (ligne < 0 || ligne > 7 || colonne < 0 || colonne > 7)
                throw new ArgumentOutOfRangeException("Position invalide : doit être entre 0 et 7");
            
            Ligne = ligne;
            Colonne = colonne;
        }

        /// <summary>
        /// Vérifie si la position est valide sur l'échiquier
        /// </summary>
        public bool EstValide()
        {
            return Ligne >= 0 && Ligne <= 7 && Colonne >= 0 && Colonne <= 7;
        }

        /// <summary>
        /// Calcule la distance entre deux positions
        /// </summary>
        public int Distance(Position autre)
        {
            return Math.Max(Math.Abs(Ligne - autre.Ligne), Math.Abs(Colonne - autre.Colonne));
        }

        /// <summary>
        /// Vérifie si deux positions sont égales
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Position pos)
                return Ligne == pos.Ligne && Colonne == pos.Colonne;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ligne, Colonne);
        }

        public override string ToString()
        {
            return $"{(char)('a' + Colonne)}{8 - Ligne}";
        }

        /// <summary>
        /// Crée une position à partir de la notation algébrique (ex: "e4")
        /// </summary>
        public static Position DepuisNotation(string notation)
        {
            if (string.IsNullOrEmpty(notation) || notation.Length != 2)
                throw new ArgumentException("Notation invalide");

            char colonne = char.ToLower(notation[0]);
            char ligne = notation[1];

            if (colonne < 'a' || colonne > 'h' || ligne < '1' || ligne > '8')
                throw new ArgumentException("Notation invalide");

            return new Position(8 - (ligne - '0'), colonne - 'a');
        }
    }
}
