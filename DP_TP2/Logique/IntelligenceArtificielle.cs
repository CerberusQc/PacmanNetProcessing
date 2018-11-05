using System;
using DP_TP2.Logique.ComportementFantômes;
using DP_TP2.ObjetAnimables.ActeurAnimables;
using static DP_TP2.ObjetAnimables.ObjetAnimable;

namespace DP_TP2.Logique
{
    /// <summary>
    /// Sera utilise pour simuler 4 autres joueurs qui vont jouer les 4 fantomes et
    /// appliquer chaque comportement a chaque fantome
    /// </summary>
    internal class IntelligenceArtificielle
    {
        public enum TypeComportement { Blinky, Pinky, Inky, Clyde }

        public IntelligenceArtificielle()
        {
            m_blinky = new ComportementBlinky();
            m_pinky = new ComportementPinky();
            m_inky = new ComportementInky();
            m_clyde = new ComportementClyde();
        }

        private ComportementBlinky m_blinky;
        private ComportementPinky m_pinky;
        private ComportementInky m_inky;
        private ComportementClyde m_clyde;

        /// <summary>
        /// Permet d'appliquer la bonne strategie de comportement selon le fantome
        /// </summary>
        /// <param name="p_fantôme">Le fantome a appliquer le comportement</param>
        /// <param name="p_pacman">Le pacman</param>
        /// <returns>Le deplacement que le fantome doit prendre</returns>
        public Déplacement AppliquerComportement(Fantôme p_fantôme, PacMan p_pacman)
        {
            switch (p_fantôme.ObtenirComportement())
            {
                case TypeComportement.Blinky:
                    {
                         return m_blinky.AppliquerInstinct(p_fantôme.Coordonnée,
                             p_pacman.Coordonnée,p_pacman.DéplacementActuel,p_fantôme.EstApeuré());
                        
                    }
                case TypeComportement.Pinky:
                    {
                        return m_pinky.AppliquerInstinct(p_fantôme.Coordonnée,
                             p_pacman.Coordonnée, p_pacman.DéplacementActuel, p_fantôme.EstApeuré());

                    }
                case TypeComportement.Inky:
                    {
                        return m_inky.AppliquerInstinct(p_fantôme.Coordonnée,
                             p_pacman.Coordonnée, p_pacman.DéplacementActuel, p_fantôme.EstApeuré());

                    }
                case TypeComportement.Clyde:
                    {
                        return m_clyde.AppliquerInstinct(p_fantôme.Coordonnée,
                             p_pacman.Coordonnée, p_pacman.DéplacementActuel, p_fantôme.EstApeuré());

                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RéinitialiserBlinky()
        {
            m_blinky = new ComportementBlinky();
        }

        public void RéinitialiserPinky()
        {
            m_pinky = new ComportementPinky();
        }

        public void RéinitialiserInky()
        {
            m_inky = new ComportementInky();
        }

        public void RéinitialiserClyde()
        {
            m_clyde = new ComportementClyde();
        }

    }
}
