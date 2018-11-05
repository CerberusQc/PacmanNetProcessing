using System;
using DP_TP2.Utilitaire;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ComportementFantômes
{
    /// <summary>
    /// Pinky voit dans le futur et va tenter de deviner la position de pacman dans 4 cases, ce qui lui permet de
    /// t'attraper en sandwich avec blinky tres souvent...
    /// C'est le fantome le plus frustrant que je connaisse...
    /// </summary>
    internal class ComportementPinky : ComportementFantôme
    {
        protected override Déplacement AppliquerStratégie(Coordonnée p_actuel, Coordonnée p_destination, Déplacement p_direction, bool p_estApeuré)
        {
            Coordonnée destination = ObtenirCoordonnéeFuture(p_destination, p_direction);

            int directionX = p_estApeuré ? p_actuel.X - destination.X : destination.X - p_actuel.X;
            int directionY = p_estApeuré ? p_actuel.Y - destination.Y : destination.Y - p_actuel.Y;

            return CalculerOrientation(directionX, directionY);
        } 

        /// <summary>
        /// Permet d'obtenir la position du pacman dans 4 cases selon sa direction
        /// </summary>
        /// <param name="p_destination">la position du pacman</param>
        /// <param name="p_déplacement">l'orientation du pacman</param>
        /// <returns>la position</returns>
        private Coordonnée ObtenirCoordonnéeFuture(Coordonnée p_destination, Déplacement p_déplacement)
        {
            switch (p_déplacement)
            {
                case Déplacement.Droite:
                    return new Coordonnée(p_destination.X + (TailleCase * 4), p_destination.Y);
                case Déplacement.Haut:
                    return new Coordonnée(p_destination.X, p_destination.Y + (TailleCase * 4));
                case Déplacement.Gauche:
                    return new Coordonnée(p_destination.X - (TailleCase * 4), p_destination.Y);
                case Déplacement.Bas:
                    return new Coordonnée(p_destination.X, p_destination.Y - (TailleCase * 4));
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(p_déplacement), p_déplacement, null);
            }
            return p_destination;
        }
    }
}
