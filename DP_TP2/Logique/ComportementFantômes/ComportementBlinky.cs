using DP_TP2.Utilitaire;
using static DP_TP2.ObjetAnimables.ObjetAnimable;

namespace DP_TP2.Logique.ComportementFantômes
{
    /// <summary>
    /// Blinky va tenter de traquer le pacman en tout moment en evaluant le chemin le plus court vers lui
    /// </summary>
    internal class ComportementBlinky : ComportementFantôme
    {
        protected override Déplacement AppliquerStratégie(Coordonnée p_actuel, Coordonnée p_destination, Déplacement p_direction, bool p_estApeuré)
        {
            int directionX = p_estApeuré ? p_actuel.X - p_destination.X :p_destination.X - p_actuel.X;
            int directionY = p_estApeuré ? p_actuel.Y - p_destination.Y : p_destination.Y - p_actuel.Y;

            return CalculerOrientation(directionX, directionY);
        }
    }
}
