using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetDessinables
{
    /// <summary>
    /// Sera utiliser par chaque element qui doit etre imprimer a l'ecran
    /// </summary>
    internal abstract class ObjetDessinable : IDessinable
    {
        public enum Orientation { Haut, Gauche, Bas, Droite }

        protected ObjetDessinable(Coordonnée p_coordonnée, Dimension p_dimension)
        {
            Coordonnée = p_coordonnée;
            Dimension = p_dimension;
        }

        public Coordonnée Coordonnée { get; private set; }

        public Dimension Dimension { get; private set; }

        public abstract void Dessiner();

        public void MettreAJourDimension(Dimension p_dimension)
        {
            Dimension = p_dimension;
        }

        public void MettreAJourCoordonnée(Coordonnée p_coordonnée)
        {
            Coordonnée = p_coordonnée;
        }


    }
}
