using DP_TP2.Utilitaire;
using System;
using static DP_TP2.ObjetAnimables.ObjetAnimable;

namespace DP_TP2.Logique.ComportementFantômes
{
    /// <summary>
    /// Inky va toujours patrouiller entre ses booster (meme s'il ne sont plus la)
    /// Ce qui l'amene au 4 coins tres souvent
    /// </summary>
    internal class ComportementInky : ComportementFantôme
    {

        private Coordonnée[] PositionBoosters { get; set; }

        private int PositionBoosterActuel { get; set; }

        private void ProchainePositionChoisit()
        {
            Random random = new Random();

            PositionBoosterActuel = random.Next(0, 4);
        }

        protected override Déplacement AppliquerStratégie(Coordonnée p_actuel, Coordonnée p_destination, Déplacement p_direction, bool p_estApeuré)
        {
            if(PositionBoosters == null)
            {
                GénérerPositionCoin();
            }

            int directionX = p_estApeuré ? p_actuel.X - PositionBoosters[PositionBoosterActuel].X : PositionBoosters[PositionBoosterActuel].X - p_actuel.X;
            int directionY = p_estApeuré ? p_actuel.Y - PositionBoosters[PositionBoosterActuel].Y : PositionBoosters[PositionBoosterActuel].Y - p_actuel.Y;

            if (Math.Abs(directionX) < 5 && Math.Abs(directionY) < 5)
            {
                ProchainePositionChoisit();
                directionX = p_estApeuré ? p_actuel.X - PositionBoosters[PositionBoosterActuel].X : PositionBoosters[PositionBoosterActuel].X - p_actuel.X;
                directionY = p_estApeuré ? p_actuel.Y - PositionBoosters[PositionBoosterActuel].Y : PositionBoosters[PositionBoosterActuel].Y - p_actuel.Y;
            }

            return CalculerOrientation(directionX, directionY);
        }

        private void GénérerPositionCoin()
        {
            PositionBoosters = new Coordonnée[4];

            //Coin Gauche supérieur
            PositionBoosters[0] = new Coordonnée(Partie.Instance.Grille.TrouverCoordonnéeCase(1, 3));

            //Coin Gauche inférieur
            PositionBoosters[1] = new Coordonnée(Partie.Instance.Grille.TrouverCoordonnéeCase(1, 23));

            //Coin Droit inférieur
            PositionBoosters[2] = new Coordonnée(Partie.Instance.Grille.TrouverCoordonnéeCase(28, 23));

            //Coin Droit supérieur
            PositionBoosters[3] = new Coordonnée(Partie.Instance.Grille.TrouverCoordonnéeCase(28, 3));

            Random random = new Random();

            PositionBoosterActuel = random.Next(0, 4);
        }
    
    }
}
