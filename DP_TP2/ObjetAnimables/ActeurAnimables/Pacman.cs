using System.Collections.Generic;
using DP_TP2.Logique;
using DP_TP2.ObjetDessinables;
using DP_TP2.ObjetDessinables.Acteur;
using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetAnimables.ActeurAnimables
{
    internal class PacMan : ObjetAnimable
    {
        public PacMan(Coordonnée p_coordonnée) :
            base(p_coordonnée, new Dimension(Constantes.TailleCase, Constantes.TailleCase),
                GénérerPacManHaut(), GénérerPacManGauche(),
                GénérerPacManDroite(), GénérerPacManBas(),
                Constantes.VitesseAnimation, Constantes.VitessePacman)
        {
        }

        public static ObjetDessinable[] GénérerPacManBas()
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                new ObjetPacMan(origine, 0, ObjetDessinable.Orientation.Bas), // Frame 1
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Bas), // Frame 2
                new ObjetPacMan(origine, 2, ObjetDessinable.Orientation.Bas), // Frame 3
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Bas)  // Frame 2
            };

            return animations.ToArray();
        }

        public static ObjetDessinable[] GénérerPacManHaut()
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                new ObjetPacMan(origine, 0, ObjetDessinable.Orientation.Haut), // Frame 1
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Haut), // Frame 2
                new ObjetPacMan(origine, 2, ObjetDessinable.Orientation.Haut), // Frame 3
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Haut)  // Frame 2
            };

            return animations.ToArray();
        }
        public static ObjetDessinable[] GénérerPacManDroite()
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                new ObjetPacMan(origine, 0, ObjetDessinable.Orientation.Droite), // Frame 1
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Droite), // Frame 2
                new ObjetPacMan(origine, 2, ObjetDessinable.Orientation.Droite), // Frame 3
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Droite)  // Frame 2
            };

            return animations.ToArray();
        }
        public static ObjetDessinable[] GénérerPacManGauche()
        {
            Coordonnée origine = new Coordonnée(Constantes.CentreX, Constantes.CentreY);

            List<ObjetDessinable> animations = new List<ObjetDessinable>
            {
                new ObjetPacMan(origine, 0, ObjetDessinable.Orientation.Gauche), // Frame 1
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Gauche), // Frame 2
                new ObjetPacMan(origine, 2, ObjetDessinable.Orientation.Gauche), // Frame 3
                new ObjetPacMan(origine, 1, ObjetDessinable.Orientation.Gauche)  // Frame 2
            };    

            return animations.ToArray();
        }

        public new void Animer(int p_cptFrame)
        {
            base.Animer(p_cptFrame);
            Partie.Instance.Grille.VérifierCaseActuelle(Coordonnée);
        }
    }
}
