using DP_TP2.Logique;
using DP_TP2.ObjetDessinables;
using DP_TP2.ObjetDessinables.Acteur;
using DP_TP2.ProgrammeDessinables;
using DP_TP2.Utilitaire;
using System;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.ObjetDessinables.ObjetJeuValeur;
using DP_TP2.ObjetDessinables.UI;
using static DP_TP2.Logique.ÉtatProgrammes.ÉtatProgramme;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.InterfaceGraphique
{
    internal class Informations : ProgrammeDessinable
    {
        public Informations(ÉtatProgramme p_action) : base(p_action)
        {
            Construire();
        }

        public Informations(Programme p_programme) : base(new ÉtatInformations(p_programme))
        {
            Construire();
        }

        public Bouton Precedent { get; set; }

        public Bouton Suivant { get; set; }

        public Texte InformationsTouche { get; set; }

        /// <summary>
        /// Permet de construire l'interface graphique (boutons, menu, texte, dessins)
        /// </summary>
        private void Construire()
        {
            Fond = Constantes.Fond;

            Texte description = new Texte(new Coordonnée(CentreX, 100), MessageInfoTableau, PacMan, 30);
            AjouterÉlément(description);

            CreerFruits();

            CreerFantômes();

            CreerBoutons();

            InformationsTouche = new Texte(new Coordonnée(CentreX, 3 * QuartY + 140), MessageToucheInfo, Blanc, 15);
            AjouterÉlément(InformationsTouche);
        }

        private void CreerFruits()
        {
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerCerise(new Coordonnée(QuartX, 175)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerFraise(new Coordonnée(CentreX, 175)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerOrange(new Coordonnée(QuartX * 3, 175)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerPomme(new Coordonnée(QuartX, 275)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerMelon(new Coordonnée(CentreX, 275)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerGalboss(new Coordonnée(QuartX * 3, 275)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerCloche(new Coordonnée(CentreX - 100, 375)));
            AjouterObjetDessinableFruit(FabriqueFruit.FabriquerCle(new Coordonnée(CentreX + 100, 375)));
        }

        private void CreerBoutons()
        {
            Precedent = new Bouton(new Coordonnée(150, 625), new Dimension(200, 50),
                CeriseFond, CerisePédon, MessagePrecedent, 30, TypeActionBouton.Précédent);
           AjouterBouton(Precedent);
        
            Suivant = new Bouton(new Coordonnée(550, 625), new Dimension(200, 50),
                MelonFond, MelonPédon, MessageDemarrer, 30, TypeActionBouton.DémarrerPartie);
            AjouterBouton(Suivant);
        }

        private void CreerFantômes()
        {
            ObjetFantôme blinky = new ObjetFantôme(new Coordonnée(Largeur / 5, 500), false, ObjetDessinable.Orientation.Bas, ObjetFantôme.CouleurFantôme.Blinky);
            AjouterFantôme(blinky, DescriptionBlinky, Blinky);

            ObjetFantôme pinky = new ObjetFantôme(new Coordonnée(Largeur / 5 * 2, 500), false, ObjetDessinable.Orientation.Bas, ObjetFantôme.CouleurFantôme.Pinky);
            AjouterFantôme(pinky, DescriptionPinky, Pinky);

            ObjetFantôme clyde = new ObjetFantôme(new Coordonnée(Largeur / 5 * 3, 500), false, ObjetDessinable.Orientation.Bas, ObjetFantôme.CouleurFantôme.Clyde);
            AjouterFantôme(clyde, DescriptionClyde, Clyde);

            ObjetFantôme inky = new ObjetFantôme(new Coordonnée(Largeur / 5 * 4, 500), false, ObjetDessinable.Orientation.Bas, ObjetFantôme.CouleurFantôme.Inky);
            AjouterFantôme(inky, DescriptionInky, Inky);
        }

        private void AjouterFantôme(ObjetFantôme p_fantôme, String p_description, Color p_couleur)
        {
            Coordonnée coordonnéeFantôme = new Coordonnée(p_fantôme.Coordonnée.X, p_fantôme.Coordonnée.Y + p_fantôme.Dimension.Largeur + 10);
            Texte texteFantôme = new Texte(coordonnéeFantôme, p_description, p_couleur, 15);
            AjouterÉlément(texteFantôme);
            AjouterÉlément(p_fantôme);
        }

        /// <summary>
        /// Permet d'ajouter le fruit a la liste d'objet dessinable mais aussi dajouter la description de ce suivant
        /// </summary>
        /// <param name="p_fruit">Le fruit a ajouter a la liste des elements Dessinables</param>
        private void AjouterObjetDessinableFruit(Fruit p_fruit)
        {
            Coordonnée coordonnéeÉlement = new Coordonnée(p_fruit.Coordonnée.X, p_fruit.Coordonnée.Y + p_fruit.Dimension.Largeur);
            Texte objetFruitTexte = new Texte(coordonnéeÉlement, p_fruit.ToString(), p_fruit.CouleurPrincipale, p_fruit.Dimension.Hauteur);

            AjouterÉlément(objetFruitTexte);
            AjouterÉlément(p_fruit);
        }

        public override void CapterClavier(int p_codeTouche)
        {
            if (p_codeTouche == ESC)
                Actions.Précédent();

            if (p_codeTouche == RETURN)
                Actions.DémarrerPartie();
        }
    }
}
