using System;
using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.ObjetDessinables.Acteur
{
    internal class ObjetFantôme : ObjetDessinable
    {
        public enum CouleurFantôme { Blinky, Pinky, Inky, Clyde }
        public ObjetFantôme(Coordonnée p_coordonnée, bool p_positionOndulation,
            Orientation p_orientationYeux, CouleurFantôme p_couleur) :
            base(p_coordonnée, new Dimension(TailleCase, TailleCase))
        {
            MandibuleDeplacer = p_positionOndulation;
            OrientationYeux = p_orientationYeux;
            Couleur = p_couleur;
            Apeuré = false;
        }

        private bool Apeuré { get; set; }
        private bool MandibuleDeplacer { get; }
        private Orientation OrientationYeux { get; }
        private CouleurFantôme Couleur { get; }

        public void ChangerÉtatApeuré(bool p_estApeuré)
        {
            Apeuré = p_estApeuré;
        }

        public override void Dessiner()
        {
            //Calculer a Partir d'un modele en ligne
            int unite = TailleCase / 15;
            RectMode(Parameter.Corner);

            DéfinirCouleur();

            DessinerCorps(unite);

            DessinerYeux(unite);

            RectMode(CENTER);
        }

        private void DessinerYeux(int unite)
        {
            int positionOeilX;
            int positionOeilY;

            int positionPupilleX;
            int positionPupilleY;

            switch (OrientationYeux)
            {
                case Orientation.Haut:
                    {
                        positionOeilX = unite * 4;
                        positionOeilY = unite * 4;

                        positionPupilleX = unite * 5;
                        positionPupilleY = unite * 3;
                        break;
                    }

                case Orientation.Gauche:
                    {
                        positionOeilX = unite * 3;
                        positionOeilY = unite * 5;

                        positionPupilleX = unite * 3;
                        positionPupilleY = unite * 7;
                        break;
                    }

                case Orientation.Bas:
                    {
                        positionOeilX = unite * 4;
                        positionOeilY = unite * 6;

                        positionPupilleX = unite * 5;
                        positionPupilleY = unite * 9;
                        break;
                    }

                case Orientation.Droite:
                    {
                        positionOeilX = unite * 6;
                        positionOeilY = unite * 5;

                        positionPupilleX = unite * 9;
                        positionPupilleY = unite * 7;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Apeuré)
                DéfinirCouleur();
            else
                Fill(FantômeYeux);

            int positionX = Coordonnée.X - TailleCase / 2;
            int positionY = Coordonnée.Y - TailleCase / 2;

            //Oeil Gauche
            Rect(positionX + positionOeilX, positionY + positionOeilY, unite * 5, unite * 4);
            Rect(positionX + positionOeilX + unite, positionY + positionOeilY - unite, unite * 3, unite * 6);

            //Oeil Droite
            Rect(positionX + positionOeilX + (7 * unite), positionY + positionOeilY, unite * 5, unite * 4);
            Rect(positionX + positionOeilX + (8 * unite), positionY + positionOeilY - unite, unite * 3, unite * 6);

            //Pupille
            Fill(Apeuré ? FantômePeurYeux : FantômePupille);

            if (Apeuré)
            {
                Rect(positionX + unite * 6, positionY + unite * 8, unite * 3, unite * 3);
                Rect(positionX + unite * 13, positionY + unite * 8, unite * 3, unite * 3);
            }
            else
            {
                Rect(positionX + positionPupilleX, positionY + positionPupilleY, unite * 2, unite * 2);
                Rect(positionX + positionPupilleX + (7 * unite), positionY + positionPupilleY, unite * 2, unite * 2);
            }
        }

        private void DessinerCorps(int p_tailleCase)
        {
            int positionX = Coordonnée.X - TailleCase / 2;
            int positionY = Coordonnée.Y - TailleCase / 2;

            //Section Corp
            Rect(positionX + p_tailleCase * 2, positionY + 7 * p_tailleCase, p_tailleCase * 16, p_tailleCase * 10);
            Rect(positionX + p_tailleCase * 3, positionY + 4 * p_tailleCase, p_tailleCase * 14, p_tailleCase * 4);
            Rect(positionX + p_tailleCase * 4, positionY + 3 * p_tailleCase, p_tailleCase * 12, p_tailleCase);
            Rect(positionX + p_tailleCase * 5, positionY + 2 * p_tailleCase, p_tailleCase * 10, p_tailleCase);
            Rect(positionX + p_tailleCase * 7, positionY + p_tailleCase, p_tailleCase * 6, p_tailleCase);

            if (MandibuleDeplacer)
            {
                Rect(positionX + p_tailleCase * 2, positionY + p_tailleCase * 17, p_tailleCase, p_tailleCase * 2);
                Rect(positionX + p_tailleCase * 3, positionY + p_tailleCase * 17, p_tailleCase, p_tailleCase);
                Rect(positionX + p_tailleCase * 5, positionY + p_tailleCase * 17, p_tailleCase, p_tailleCase);
                Rect(positionX + p_tailleCase * 6, positionY + p_tailleCase * 17, p_tailleCase * 3, p_tailleCase * 2);
                Rect(positionX + p_tailleCase * 11, positionY + p_tailleCase * 17, p_tailleCase * 3, p_tailleCase * 2);
                Rect(positionX + p_tailleCase * 14, positionY + p_tailleCase * 17, p_tailleCase, p_tailleCase);
                Rect(positionX + p_tailleCase * 16, positionY + p_tailleCase * 17, p_tailleCase, p_tailleCase);
                Rect(positionX + p_tailleCase * 17, positionY + p_tailleCase * 17, p_tailleCase, p_tailleCase * 2);
            }
            else
            {
                Rect(positionX + p_tailleCase * 2, positionY + p_tailleCase * 17, p_tailleCase * 4, p_tailleCase);
                Rect(positionX + p_tailleCase * 3, positionY + p_tailleCase * 18, p_tailleCase * 2, p_tailleCase);
                Rect(positionX + p_tailleCase * 8, positionY + p_tailleCase * 17, p_tailleCase * 4, p_tailleCase);
                Rect(positionX + p_tailleCase * 9, positionY + p_tailleCase * 18, p_tailleCase * 2, p_tailleCase);
                Rect(positionX + p_tailleCase * 14, positionY + p_tailleCase * 17, p_tailleCase * 4, p_tailleCase);
                Rect(positionX + p_tailleCase * 15, positionY + p_tailleCase * 18, p_tailleCase * 2, p_tailleCase);
            }
        }

        private void DéfinirCouleur()
        {
            switch (Couleur)
            {
                case CouleurFantôme.Blinky:
                    Fill(Blinky);
                    break;
                case CouleurFantôme.Pinky:
                    Fill(Pinky);
                    break;
                case CouleurFantôme.Inky:
                    Fill(Inky);
                    break;
                case CouleurFantôme.Clyde:
                    Fill(Clyde);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Apeuré)
                Fill(FantômePeur);
        }
    }
}
