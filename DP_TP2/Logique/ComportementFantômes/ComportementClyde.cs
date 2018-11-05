using DP_TP2.Utilitaire;
using System;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ComportementFantômes
{
    /// <summary>
    /// Clyde va obtenir une destination au hasard et va aller vers ce point
    /// proche de la destination il va se generer un nouveau point a aller
    /// Il ne va pas aller souvent dans les coins
    /// </summary>
    internal class ComportementClyde : ComportementFantôme
    {
        public ComportementClyde()
        {
            CoordonnéeAléatoire = ObtenirPositionAléatoire();
        }

        private Coordonnée CoordonnéeAléatoire { get; set; }

        private static Coordonnée ObtenirPositionAléatoire()
        {
            Random random = new Random();

            // Se trouve une case au hasard sur la grille
            return (Partie.Instance != null) ? Partie.Instance.Grille.TrouverCoordonnéeCase(
                                                 random.Next(1, 28), random.Next(1, 28)) : new Coordonnée(0, 0);
        }

        protected override Déplacement AppliquerStratégie(Coordonnée p_actuel, Coordonnée p_destination, Déplacement p_direction, bool p_estApeuré)
        {
            int directionX = p_estApeuré ? p_actuel.X - CoordonnéeAléatoire.X : CoordonnéeAléatoire.X - p_actuel.X;
            int directionY = p_estApeuré ? p_actuel.Y - CoordonnéeAléatoire.Y : CoordonnéeAléatoire.Y - p_actuel.Y;

            if (Math.Abs(directionX) < TailleCase * 4 || Math.Abs(directionY) < TailleCase * 4)
            {
                CoordonnéeAléatoire = ObtenirPositionAléatoire();
                directionX = p_estApeuré ? p_actuel.X - CoordonnéeAléatoire.X : CoordonnéeAléatoire.X - p_actuel.X;
                directionY = p_estApeuré ? p_actuel.Y - CoordonnéeAléatoire.Y : CoordonnéeAléatoire.Y - p_actuel.Y;
            }

            return CalculerOrientation(directionX, directionY);
        }
    
    }
}
