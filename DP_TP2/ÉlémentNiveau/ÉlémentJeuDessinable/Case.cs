using DP_TP2.ObjetDessinables;
using DP_TP2.Utilitaire;

namespace DP_TP2.ÉlémentNiveau.ÉlémentJeuDessinable
{
    /// <summary>
    /// Classe abstraite permettant de représenter chaque case du jeu dans le Grille de jeu
    /// </summary>
    abstract class Case : IDessinable
    {
        protected Case(Coordonnée p_coordonnée)
        {
            Coordonnée = p_coordonnée;
            Taille = Constantes.TailleCase;
            Traversable = true;
        }

        Coordonnée Coordonnée { get; set; }

        public int Taille { get; set; }

        bool Traversable { get; set; }

        /// <summary>
        /// Pour savoir si la case est traversable par les Objets Animables du jeu
        /// </summary>
        public bool EstTraversable { get => Traversable; set => Traversable = value; }

        public Coordonnée ObtenirCoordonnée()
        {
            return Coordonnée;
        }

        public abstract void Dessiner();

        public void MettreAJourCoordonnée(Coordonnée p_coordonnée)
        {
            Coordonnée = p_coordonnée;
        }

        public void MettreAJourDimension(Dimension p_dimension)
        {
            Taille = p_dimension.Largeur;
        }
    }
}
