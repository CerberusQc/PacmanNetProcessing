namespace DP_TP2.Utilitaire
{
    internal class Coordonnée
    {
        public Coordonnée(int p_x, int p_y)
        {
            X = p_x;
            Y = p_y;
        }

        public Coordonnée(Coordonnée p_coordonnée)
        {
            X = p_coordonnée.X;
            Y = p_coordonnée.Y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            Coordonnée coordonnée = obj != null && obj.GetType() == typeof(Coordonnée) ? obj as Coordonnée : null;

            if (coordonnée == null)
                return false;

            return (X == coordonnée.X && Y == coordonnée.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public bool EstProche(Coordonnée p_coordonnée,int p_tolérance)
        {
            return (p_coordonnée.X - p_tolérance <= X && X <= p_coordonnée.X + p_tolérance)
                    && 
                   (p_coordonnée.Y - p_tolérance <= Y && Y <= p_coordonnée.Y + p_tolérance);
        }
    }
}
