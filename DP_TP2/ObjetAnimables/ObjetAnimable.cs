using System;
using DP_TP2.Logique;
using DP_TP2.ObjetDessinables;
using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.ObjetAnimables
{
    /// <summary>
    /// Classe abstraite qui fera office de squelette pour la base des objets qui auront de
    /// l'animation
    /// </summary>
    internal abstract class ObjetAnimable : IAnimable
    {
        public enum Déplacement { Arrêt, Droite, Haut, Gauche, Bas }

        /// <summary>
        /// Constructeur Complexe pour avoir l'ensemble des elements a l'animation
        /// </summary>
        /// <param name="p_coordonnée">Coordonne de l'objet animable</param>
        /// <param name="p_dimension">la dimension de l'objet dessinable</param>
        /// <param name="p_listeHaut">Liste objet Dessinable pour la direction HAUT</param>
        /// <param name="p_listeGauche">Liste objet Dessinable pour la direction GAUCHE</param>
        /// <param name="p_listeDroite">Liste objet Dessinable pour la direction DROITE</param>
        /// <param name="p_listeBas">Liste objet Dessinable pour la direction BAS</param>
        /// <param name="p_vitesseAnimation">Le nombre de fois que la sequence d'image va "shifter" par lapse de temps</param>
        /// <param name="p_vitesseDeplacement">La distance d'un deplacement a chaque rafraichissement</param>
        protected ObjetAnimable(Coordonnée p_coordonnée, Dimension p_dimension,
            ObjetDessinable[] p_listeHaut, ObjetDessinable[] p_listeGauche,
            ObjetDessinable[] p_listeDroite, ObjetDessinable[] p_listeBas,
            int p_vitesseAnimation, int p_vitesseDeplacement)
        {
            Coordonnée = p_coordonnée;
            Dimension = p_dimension;

            ListeAnimationBas = p_listeBas;
            ListeAnimationDroite = p_listeDroite;
            ListeAnimationGauche = p_listeGauche;
            ListeAnimationHaut = p_listeHaut;

            IndiceFrame = 0;
            VitesseAnimation = p_vitesseAnimation;
            DéplacementActuel = Déplacement.Arrêt;
            ProchainDéplacement = Déplacement.Arrêt;
            VitesseDéplacement = p_vitesseDeplacement;
        }

        public Coordonnée Coordonnée { get; private set; }

        public Dimension Dimension { get; private set; }

        public int VitesseAnimation { get; private set; }

        public ObjetDessinable[] ListeAnimationHaut { get; }
        public ObjetDessinable[] ListeAnimationGauche { get; }
        public ObjetDessinable[] ListeAnimationBas { get; }
        public ObjetDessinable[] ListeAnimationDroite { get; }

        public int IndiceFrame { get; private set; }

        public Déplacement DéplacementActuel { get;private set; }

        Déplacement ProchainDéplacement { get; set; }

        private int VitesseDéplacement { get; }

        public void Animer(int p_cptFrame)
        {
            Déplacer();

            // Fera imprimer a chaque m_vitesseAnimation de Frame :
            if (p_cptFrame % VitesseAnimation == 0 && DéplacementActuel != Déplacement.Arrêt)
            {
                // Remet le cpt a 0 si on depasse le nombre d'animation
                // m_listeAnimationHaut pris au hasard etant donner que les 4 listes ont la meme taille
                IndiceFrame = ++IndiceFrame % ListeAnimationHaut.Length;
            }

            DessinerAnimable(DéplacementActuel);

        }

        private void DessinerAnimable(Déplacement p_déplacement)
        {
            switch (p_déplacement)
            {
                case Déplacement.Droite:
                    {
                        ListeAnimationDroite[IndiceFrame].MettreAJourCoordonnée(Coordonnée);
                        ListeAnimationDroite[IndiceFrame].Dessiner();
                        break;
                    }
                case Déplacement.Haut:
                    {
                        ListeAnimationHaut[IndiceFrame].MettreAJourCoordonnée(Coordonnée);
                        ListeAnimationHaut[IndiceFrame].Dessiner();
                        break;
                    }
                case Déplacement.Gauche:
                    {
                        ListeAnimationGauche[IndiceFrame].MettreAJourCoordonnée(Coordonnée);
                        ListeAnimationGauche[IndiceFrame].Dessiner();
                        break;
                    }
                case Déplacement.Bas:
                    {
                        ListeAnimationBas[IndiceFrame].MettreAJourCoordonnée(Coordonnée);
                        ListeAnimationBas[IndiceFrame].Dessiner();
                        break;
                    }
                default: // On doit tout de meme imprimer meme si on ne bouge pas la droite sera prise dans ce cas
                    {
                        ListeAnimationDroite[0].MettreAJourCoordonnée(Coordonnée);
                        ListeAnimationDroite[0].Dessiner();
                        break;
                    }
            }
        }

        private void Déplacer()
        {
            VérifierProchainDéplacement();

            MettreAJourDéplacement();
        }

        /// <summary>
        /// Va servir l'animation d'intro (splash) pour faire animer des objets mais de "bypasser" les regles de jeu
        /// donc ne verifie pas si le deplacement est valide, va seulement mettre a jour la position
        /// </summary>
        /// <param name="p_cptFrame">Nombre de fois que Draw() a ete appeler</param>
        /// <param name="p_déplacement">La direction du deplacement</param>
        public void DéplacerPourLogo(int p_cptFrame, Déplacement p_déplacement)
        {
            switch (p_déplacement)
            {
                case Déplacement.Droite:
                    Coordonnée.X += VitesseDéplacement;
                    break;
                case Déplacement.Haut:
                    Coordonnée.Y -= VitesseDéplacement;
                    break;
                case Déplacement.Gauche:
                    Coordonnée.X -= VitesseDéplacement;
                    break;
                case Déplacement.Bas:
                    Coordonnée.Y += VitesseDéplacement;
                    break;
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(p_déplacement), p_déplacement, null);
            }

            if (p_cptFrame % VitesseAnimation == 0)
            {
                // Remet le cpt a 0 si on depasse le nombre d'animation
                // m_listeAnimationHaut pris au hasard etant donner que les 4 listes ont la meme taille
                IndiceFrame = ++IndiceFrame % ListeAnimationHaut.Length;
            }

            DessinerAnimable(p_déplacement);
        }

        /// <summary>
        /// Va permettre de mettre a jour les coordonnes de l'objet selon sont deplacement
        /// </summary>
        protected void MettreAJourDéplacement()
        {
            switch (DéplacementActuel)
            {
                case Déplacement.Haut:
                    {
                        int valeurTemp = Coordonnée.Y - TailleCase / 2 - VitesseDéplacement;

                        VérifierDéplacement(valeurTemp, -VitesseDéplacement);

                        break;
                    }
                case Déplacement.Bas:
                    {
                        int valeurTemp = Coordonnée.Y + TailleCase / 2 + VitesseDéplacement;

                        VérifierDéplacement(valeurTemp, VitesseDéplacement);

                        break;
                    }
                case Déplacement.Gauche:
                    {
                        if (Coordonnée.X <= 100 && Coordonnée.Y == CentreY + 20)
                            Coordonnée.X = Largeur - 100;

                        int valeurTemp = Coordonnée.X - TailleCase / 2 - VitesseDéplacement;

                        VérifierDéplacement(valeurTemp, -VitesseDéplacement);

                        break;
                    }

                case Déplacement.Droite:
                    {
                        if (Coordonnée.X >= Largeur - 100 && Coordonnée.Y == CentreY + 20)
                            Coordonnée.X = 100;

                        int valeurTemp = Coordonnée.X + TailleCase / 2 + VitesseDéplacement;

                        VérifierDéplacement(valeurTemp, VitesseDéplacement);

                        
                        break;
                    }
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Verifie si le deplacement de l'objet est valide 
        /// </summary>
        /// <param name="valeurTemp"></param>
        /// <param name="p_vitesseDéplacement"></param>
        private void VérifierDéplacement(int valeurTemp, int p_vitesseDéplacement)
        {
            if (Partie.Instance.Grille.VérifierProchaineCaseTraversable(Coordonnée, valeurTemp, DéplacementActuel))
            {
                if (DéplacementActuel == Déplacement.Bas || DéplacementActuel == Déplacement.Haut)
                    Coordonnée.Y += p_vitesseDéplacement;
                else
                    Coordonnée.X += p_vitesseDéplacement;
            }
            else
            {
                DéplacementActuel = Déplacement.Arrêt;
            }
        }

        /// <summary>
        /// va verifier si le prochain deplacement est possible pour eviter les collisions avec des murs
        /// ca fait mal sinon...
        /// </summary>
        private void VérifierProchainDéplacement()
        {
            switch (ProchainDéplacement)
            {
                case Déplacement.Haut:
                    {
                        int valeurTemp = Coordonnée.Y - TailleCase / 2 - VitesseDéplacement;
                        VérifierProchainDéplacement(valeurTemp);
                        break;
                    }
                case Déplacement.Bas:
                    {
                        int valeurTemp = Coordonnée.Y + TailleCase / 2 + VitesseDéplacement;

                        VérifierProchainDéplacement(valeurTemp);
                        break;
                    }
                case Déplacement.Gauche:
                    {


                        int valeurTemp = Coordonnée.X - TailleCase / 2 - VitesseDéplacement;

                        VérifierProchainDéplacement(valeurTemp);
                        break;
                    }

                case Déplacement.Droite:
                    {
                        int valeurTemp = Coordonnée.X + TailleCase / 2 + VitesseDéplacement;

                        VérifierProchainDéplacement(valeurTemp);

                        break;
                    }
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void VérifierProchainDéplacement(int valeurTemp)
        {
            if (Partie.Instance.Grille.VérifierProchaineCaseTraversable(Coordonnée, valeurTemp, ProchainDéplacement))
                DéplacementActuel = ProchainDéplacement;
        }

        public void MettreAJourCoordonnée(Coordonnée p_coordonnée)
        {
            Coordonnée = p_coordonnée;
        }

        public void MettreAJourDimension(Dimension p_dimension)
        {
            Dimension = p_dimension;
        }

        public void MettreAJourVitesseAnimation(int p_vitesse)
        {
            VitesseAnimation = p_vitesse;
        }


        public void Déplacer(Déplacement p_déplacement)
        {
            ProchainDéplacement = p_déplacement;
        }
    }
}
