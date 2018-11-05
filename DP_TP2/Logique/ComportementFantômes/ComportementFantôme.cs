using System;
using System.Collections.Generic;
using System.Linq;
using DP_TP2.Utilitaire;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ComportementFantômes
{
    /// <summary>
    /// Classe pour les comportement de fantomes, va definir les strategies que chaque fantomes utilisent
    /// </summary>
    internal abstract class ComportementFantôme : IComportemental
    {
        protected ComportementFantôme()
        {
            DernierDéplacement = Déplacement.Arrêt;
            Origine = Déplacement.Arrêt;
            ListeInterdite = new List<Déplacement>();
            Cadran = 0;
            DernierCadran = 0;
            DernièreCoordonnée = new Coordonnée(0, 0);
        }

        protected Déplacement DernierDéplacement { get; set; }

        protected Déplacement Origine { get; set; }

        protected int Cadran { get; set; }

        protected int DernierCadran { get; set; }

        protected Coordonnée DernièreCoordonnée { get; set; }

        protected List<Déplacement> ListeInterdite { get; set; }

      
        /// <summary>
        /// Pour les changement de direction et pour trouver de quel direction le fantome vient
        /// </summary>
        /// <param name="déplacement">Le deplacement actuel</param>
        protected void TrouverInverse(Déplacement déplacement)
        {
            switch (déplacement)
            {
                case Déplacement.Droite:
                    Origine = Déplacement.Gauche;
                    break;
                case Déplacement.Haut:
                    Origine = Déplacement.Bas;
                    break;
                case Déplacement.Gauche:
                    Origine = Déplacement.Droite;
                    break;
                case Déplacement.Bas:
                    Origine = Déplacement.Haut;
                    break;
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(déplacement), déplacement, null);
            }
        }

        /// <summary>
        /// Chacun des fantomes va devoir appliquer sa propre strategie au deplacement
        /// </summary>
        /// <param name="p_actuel">La position actuel du fantome</param>
        /// <param name="p_destination">La Destination du fantome (souvent Pacman)</param>
        /// <param name="p_direction">l'orientation du fantome actuel</param>
        /// <param name="p_estApeuré">si le fantome est apeure</param>
        /// <returns>l'orientation que le fantome doit prendre</returns>
        protected abstract Déplacement AppliquerStratégie(Coordonnée p_actuel, Coordonnée p_destination, Déplacement p_direction, bool p_estApeuré);

        /// <summary>
        /// Instinct de base du fantome, va tente d'aller vers sa destination en trouvant l'axe le plus court
        /// </summary>
        /// <param name="p_actuel">La position actuel du fantome</param>
        /// <param name="p_destination">La Destination du fantome (souvent Pacman)</param>
        /// <param name="p_direction">l'orientation du fantome actuel</param>
        /// <param name="p_estApeuré">si le fantome est apeure</param>
        /// <returns>l'orientation que le fantome doit prendre</returns>
        public Déplacement AppliquerInstinct(Coordonnée p_actuel, Coordonnée p_destination, Déplacement p_direction, bool p_estApeuré)
        {
            Déplacement nouveauDéplacement = AppliquerStratégie(p_actuel, p_destination, p_direction, p_estApeuré);

            if (Cadran != DernierCadran && ListeInterdite.Count >= 3)
            {
                DernierCadran = Cadran;
                ListeInterdite.Clear();
            }

            if (!ListeInterdite.Any())
                DernierDéplacement = nouveauDéplacement;

            if (!p_actuel.Equals(DernièreCoordonnée))
                DernièreCoordonnée = p_actuel;

            Déplacement déplacementExecuter = CompenserTopographie(p_actuel);

            if (Cadran == DernierCadran && DernièreCoordonnée.Equals(p_actuel))
            {
                ListeInterdite.Clear();
                TrouverInverse(déplacementExecuter);
            }

            return déplacementExecuter;
        }

        /// <summary>
        /// Permet de calculer dans quel direction se diriger selon la difference en X et Y
        /// en etablissant des cadrans comme le plan cartesien
        /// La position 0,0 serait la position du fantome sur le plan cartesien
        /// et sa destination va etre dans un des 4 cadran
        /// (1 = En Haut a Gauche, 2 = En Haut a Droite, 3 = En Bas a Gauche...)
        /// </summary>
        /// <param name="directionX">La distance en X relative a sa position actuel</param>
        /// <param name="directionY">La distance en Y relative a sa position actuel</param>
        /// <returns>l'orientation que le fantome doit prendre</returns>
        protected Déplacement CalculerOrientation(int directionX, int directionY)
        {
            Déplacement déplacement = Déplacement.Arrêt;

            //Cible Trouvé
            if (directionX == 0 && directionY == 0)
            {
                déplacement = Déplacement.Arrêt;
            }
            else if (directionX == 0)
            {
                déplacement = directionY > 0 ? Déplacement.Bas : Déplacement.Haut;
                Cadran = 0;
            }
            else if (directionY == 0)
            {
                déplacement = directionX > 0 ? Déplacement.Droite : Déplacement.Gauche;
                Cadran = 0;
            }
            //Cadran Supérieur Gauche
            else if (directionX < 0 && directionY < 0)
            {
                déplacement = Math.Abs(directionX) >= Math.Abs(directionY) ? Déplacement.Gauche : Déplacement.Haut;

                Cadran = 1;
            }
            //Cadran Supérieur Droite
            else if (directionX > 0 && directionY < 0)
            {
                déplacement = Math.Abs(directionX) >= Math.Abs(directionY) ? Déplacement.Droite : Déplacement.Haut;

                Cadran = 2;
            }

            //Cadran Inférieur Gauche
            else if (directionX < 0 && directionY > 0)
            {
                déplacement = Math.Abs(directionX) >= Math.Abs(directionY) ? Déplacement.Gauche : Déplacement.Bas;

                Cadran = 3;
            }
            //Cadran  Inférieur Droite
            else if (directionX > 0 && directionY > 0)
            {
                déplacement = Math.Abs(directionX) >= Math.Abs(directionY) ? Déplacement.Droite : Déplacement.Bas;

                Cadran = 4;
            }


            return déplacement;
        }

        /// <summary>
        /// Lorsque le fantome va tenter de se deplacer mais va pris dans un coin
        /// En eliminant progressivement les mouvements les plus probables mais impossible
        /// on obtient le prochain mouvement valide et on l'execute tant et aussi
        /// longtemps que le deplacement de l'instinct de base n'est pas capable d'etre executer
        /// 
        /// *** Le retour sur ses pas est toujours l'option la moins probable ***
        /// </summary>
        /// <param name="p_actuel">La position actuel du fantome</param>
        /// <returns>l'orientation que le fantome doit prendre</returns>
        public Déplacement CompenserTopographie(Coordonnée p_actuel)
        {
            if (Origine != DernierDéplacement && !VérifierProchaineCase(p_actuel, DernierDéplacement))
            {
                switch (DernierDéplacement)
                {
                    case Déplacement.Droite:
                        {
                            Déplacement plusProbable = (Cadran == 2) ? Déplacement.Haut : Déplacement.Bas;
                            Déplacement moinsProbable = (Cadran == 2) ? Déplacement.Bas : Déplacement.Haut;

                            if (VérifierSiDirectionPlusProbable(p_actuel, plusProbable))
                                return plusProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, moinsProbable))
                                return moinsProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, Déplacement.Gauche))
                                return Déplacement.Gauche;
                            break;
                        }
                    case Déplacement.Haut:
                        {

                            Déplacement plusProbable = (Cadran == 1) ? Déplacement.Gauche : Déplacement.Droite;
                            Déplacement moinsProbable = (Cadran == 1) ? Déplacement.Droite : Déplacement.Gauche;

                            if (VérifierSiDirectionPlusProbable(p_actuel, plusProbable))
                                return plusProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, moinsProbable))
                                return moinsProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, Déplacement.Bas))
                                return Déplacement.Bas;
                            break;
                        }
                    case Déplacement.Gauche:
                        {
                            Déplacement plusProbable = (Cadran == 1) ? Déplacement.Haut : Déplacement.Bas;
                            Déplacement moinsProbable = (Cadran == 1) ? Déplacement.Bas : Déplacement.Haut;

                            if (VérifierSiDirectionPlusProbable(p_actuel, plusProbable))
                                return plusProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, moinsProbable))
                                return moinsProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, Déplacement.Droite))
                                return Déplacement.Droite;

                            break;
                        }
                    case Déplacement.Bas:
                        {
                            if (VérifierSiDirectionPlusProbable(p_actuel, Déplacement.Haut))
                                return Déplacement.Haut;

                            Déplacement plusProbable = (Cadran == 3) ? Déplacement.Gauche : Déplacement.Droite;
                            Déplacement moinsProbable = (Cadran == 3) ? Déplacement.Droite : Déplacement.Gauche;

                            if (VérifierSiDirectionPlusProbable(p_actuel, plusProbable))
                                return plusProbable;

                            if (VérifierSiDirectionPlusProbable(p_actuel, moinsProbable))
                                return moinsProbable;

                            break;
                        }
                    case Déplacement.Arrêt:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                TrouverInverse(DernierDéplacement);
                ListeInterdite.Clear();
            }

            return DernierDéplacement;
        }

        private bool VérifierSiDirectionPlusProbable(Coordonnée p_actuel, Déplacement p_direction)
        {
            if (ListeInterdite.All(c => c != p_direction)
                               && Origine != p_direction
                               && VérifierProchaineCase(p_actuel, p_direction))
            {
                return true;
            }

            if (ListeInterdite.All(c => c != p_direction))
                ListeInterdite.Add(p_direction);

            return false;
        }

        /// <summary>
        /// Permet de simuler le prochain deplacement et de voir si la
        /// prochaine case est valide
        /// </summary>
        /// <param name="p_actuel">La position actuel</param>
        /// <param name="p_déplacement">la direction du deplacement</param>
        /// <returns>si le deplacement est valide</returns>
        private bool VérifierProchaineCase(Coordonnée p_actuel, Déplacement p_déplacement)
        {
            int valeurTemp = 0;

            switch (p_déplacement)
            {
                case Déplacement.Droite:
                    {
                        valeurTemp = p_actuel.X + TailleCase / 2 + VitesseFantôme;
                        break;
                    }
                case Déplacement.Haut:
                    {
                        valeurTemp = p_actuel.Y - TailleCase / 2 - VitesseFantôme;
                        break;
                    }
                case Déplacement.Gauche:
                    {
                        valeurTemp = p_actuel.X - TailleCase / 2 - VitesseFantôme;
                        break;
                    }
                case Déplacement.Bas:
                    {
                        valeurTemp = p_actuel.Y + TailleCase / 2 + VitesseFantôme;
                        break;
                    }
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(p_déplacement), p_déplacement, null);
            }

            return Partie.Instance.Grille.VérifierProchaineCaseTraversable(p_actuel, valeurTemp, p_déplacement);
        }
    }
}
