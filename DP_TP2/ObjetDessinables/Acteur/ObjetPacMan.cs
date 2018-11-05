using System;
using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.ObjetDessinables.Acteur
{
    internal class ObjetPacMan : ObjetDessinable
    {
        public ObjetPacMan(Coordonnée p_coordonnée, int p_largeurBouche, Orientation p_orientation) 
            : base(p_coordonnée, new Dimension(TailleCase, TailleCase))
        {
            m_largeurBouche = p_largeurBouche;
            m_orientation = p_orientation;
        }

        private readonly int m_largeurBouche;

        private readonly Orientation m_orientation;

        public override void Dessiner()
        {
            Fill(PacMan);
            Ellipse(Coordonnée.X, Coordonnée.Y, Dimension.Largeur - 3, Dimension.Hauteur - 3);

            Fill(Fond);

            switch (m_orientation)
            {
                case Orientation.Haut:
                    {
                        int y = Coordonnée.Y - Dimension.Largeur / 2 - 1;
                        int x1 = Coordonnée.X - (Dimension.Hauteur / 4) * m_largeurBouche;
                        int x2 = Coordonnée.X + (Dimension.Hauteur / 4) * m_largeurBouche;

                        Triangle(Coordonnée.X, Coordonnée.Y, x1, y, x2, y);

                        break;
                    }

                case Orientation.Gauche:
                    {
                        int x = Coordonnée.X - Dimension.Largeur / 2 - 1;
                        int y1 = Coordonnée.Y - (Dimension.Hauteur / 4) * m_largeurBouche;
                        int y2 = Coordonnée.Y + (Dimension.Hauteur / 4) * m_largeurBouche;

                        Triangle(Coordonnée.X, Coordonnée.Y, x, y1, x, y2);

                        break;
                    }

                case Orientation.Bas:
                    {
                        int y = Coordonnée.Y + Dimension.Largeur / 2 - 1;
                        int x1 = Coordonnée.X - (Dimension.Hauteur / 4) * m_largeurBouche;
                        int x2 = Coordonnée.X + (Dimension.Hauteur / 4) * m_largeurBouche;

                        Triangle(Coordonnée.X, Coordonnée.Y, x1, y, x2, y);

                        break;
                    }

                case Orientation.Droite:
                    {
                        int x = Coordonnée.X + Dimension.Largeur / 2 - 1;
                        int y1 = Coordonnée.Y - (Dimension.Hauteur / 4) * m_largeurBouche;
                        int y2 = Coordonnée.Y + (Dimension.Hauteur / 4) * m_largeurBouche;

                        Triangle(Coordonnée.X, Coordonnée.Y, x, y1, x, y2);

                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }       
        }
    }
}
