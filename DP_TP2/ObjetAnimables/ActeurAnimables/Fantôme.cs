using System;
using System.Collections.Generic;
using DP_TP2.Logique;
using DP_TP2.ObjetDessinables;
using DP_TP2.ObjetDessinables.Acteur;
using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetAnimables.ActeurAnimables
{
    internal class Fantôme : ObjetAnimable
    {
        public Fantôme(Coordonnée p_coordonnée, ObjetFantôme.CouleurFantôme p_couleur) :
              base(p_coordonnée, new Dimension(Constantes.TailleCase, Constantes.TailleCase),
                  GénérerFantômeHaut(p_couleur), GénérerFantômeGauche(p_couleur),
                  GénérerFantômeDroite(p_couleur), GénérerFantômeBas(p_couleur),
                  Constantes.VitesseAnimation, Constantes.VitesseFantôme)
        {
            Apeuré = false;
            CoordonnéeInitiale = new Coordonnée(p_coordonnée);
            switch (p_couleur)
            {
                case ObjetFantôme.CouleurFantôme.Blinky:
                    {
                        Comportement = IntelligenceArtificielle.TypeComportement.Blinky;
                        break;
                    }
                case ObjetFantôme.CouleurFantôme.Pinky:
                    {
                        Comportement = IntelligenceArtificielle.TypeComportement.Pinky;
                        break;
                    }
                case ObjetFantôme.CouleurFantôme.Inky:
                    {
                        Comportement = IntelligenceArtificielle.TypeComportement.Inky;
                        break;
                    }
                case ObjetFantôme.CouleurFantôme.Clyde:
                    {
                        Comportement = IntelligenceArtificielle.TypeComportement.Clyde;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(p_couleur), p_couleur, null);
            }
        }

        private Coordonnée CoordonnéeInitiale { get; }

        private bool Apeuré { get; set; }

        private  IntelligenceArtificielle.TypeComportement Comportement { get; }

        public IntelligenceArtificielle.TypeComportement ObtenirComportement()
        {
            return Comportement;
        }

        public bool EstApeuré()
        {
            return Apeuré;
        }

        public void RéinitialiserPosition()
        {
            MettreAJourCoordonnée(new Coordonnée(CoordonnéeInitiale));
            ModifierÉtatPeur(false);
        }

        /// <summary>
        /// Va modifer l'etat du fantome mais aussi tout les dessins des listes d'objets dessinables
        /// </summary>
        /// <param name="p_état">Vrai si le fantome est apeure</param>
        public void ModifierÉtatPeur(bool p_état)
        {
            Apeuré = p_état;

            foreach (ObjetDessinable dessin in ListeAnimationBas)
            {
                (dessin as ObjetFantôme)?.ChangerÉtatApeuré(Apeuré);
            }

            foreach (ObjetDessinable dessin in ListeAnimationDroite)
            {
                (dessin as ObjetFantôme)?.ChangerÉtatApeuré(Apeuré);
            }


            foreach (ObjetDessinable dessin in ListeAnimationGauche)
            {
                (dessin as ObjetFantôme)?.ChangerÉtatApeuré(Apeuré);
            }

            foreach (ObjetDessinable dessin in ListeAnimationHaut)
            {
                (dessin as ObjetFantôme)?.ChangerÉtatApeuré(Apeuré);
            }
        }

        public static ObjetDessinable[] GénérerFantômeBas(ObjetFantôme.CouleurFantôme p_couleur)
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                // Frame 1
                new ObjetFantôme(origine, true, ObjetDessinable.Orientation.Bas, p_couleur),
                // Frame 2
                new ObjetFantôme(origine, false, ObjetDessinable.Orientation.Bas, p_couleur)
            };

            return animations.ToArray();
        }

        public static ObjetDessinable[] GénérerFantômeHaut(ObjetFantôme.CouleurFantôme p_couleur)
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                // Frame 1
                new ObjetFantôme(origine, true, ObjetDessinable.Orientation.Haut, p_couleur),
                // Frame 2
                new ObjetFantôme(origine, false, ObjetDessinable.Orientation.Haut, p_couleur)
            };

            return animations.ToArray();
        }
        public static ObjetDessinable[] GénérerFantômeDroite(ObjetFantôme.CouleurFantôme p_couleur)
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                // Frame 1
                new ObjetFantôme(origine, true, ObjetDessinable.Orientation.Droite, p_couleur),
                // Frame 2
                new ObjetFantôme(origine, false, ObjetDessinable.Orientation.Droite, p_couleur)
            };

            return animations.ToArray();
        }
        public static ObjetDessinable[] GénérerFantômeGauche(ObjetFantôme.CouleurFantôme p_couleur)
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                // Frame 1
                new ObjetFantôme(origine, true, ObjetDessinable.Orientation.Gauche, p_couleur),
                // Frame 2
                new ObjetFantôme(origine, false, ObjetDessinable.Orientation.Gauche, p_couleur)
            };

            return animations.ToArray();
        }
    }
}

