using System;
using DP_TP2.Logique;
using DP_TP2.Utilitaire;
using NetProcessing;

namespace DP_TP2.ÉlémentNiveau.ÉlémentJeuDessinable
{
    /// <summary>
    /// Classe simulant les murs de la grille de jeu, ne seront pas traversable par les objetAnimable
    /// </summary>
    internal class Mur : Case
    {
        public Mur(Coordonnée p_coordonnée) : base(p_coordonnée)
        {
            EstTraversable = false;
            EstPorteFantôme = false;
        }

        public bool EstPorteFantôme { get; set; }

        private static Sketch.Color m_couleurAlea = Constantes.CouleurRandom[new Random().Next(0, Constantes.CouleurRandom.Length - 1)];

        public override void Dessiner()
        {
            if (!Partie.Instance.MurDejaDessiner)
            {
                if (EstPorteFantôme)
                {
                    Coordonnée coordonnée = ObtenirCoordonnée();

                    Sketch.Fill(Constantes.Fond);
                    Sketch.Rect(coordonnée.X, coordonnée.Y, Taille, Taille);

                    Sketch.Fill(Constantes.PorteFantôme);                 
                    Sketch.Rect(coordonnée.X, coordonnée.Y, Taille, Taille / 3);
                }
                else
                {
                    Sketch.Fill(Constantes.Mur);
                    Coordonnée coordonnée = ObtenirCoordonnée();
                    Sketch.Rect(coordonnée.X, coordonnée.Y, Taille, Taille);

                    Sketch.Fill(m_couleurAlea);
                    Sketch.Rect(coordonnée.X, coordonnée.Y, Taille / 4, Taille);
                    Sketch.Rect(coordonnée.X, coordonnée.Y, Taille, Taille / 4);
                }
            }
        }

        public static void GénérerNouvelleCouleur()
        {
            m_couleurAlea = Constantes.CouleurRandom[new Random().Next(0, Constantes.CouleurRandom.Length - 1)];
        }
    }
}
