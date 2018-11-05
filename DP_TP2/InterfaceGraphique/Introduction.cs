using System.Collections.Generic;
using DP_TP2.Logique;
using DP_TP2.ProgrammeDessinables;
using DP_TP2.Utilitaire;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.ObjetDessinables.UI;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.InterfaceGraphique
{
    internal class Introduction : ProgrammeDessinable
    {
        /// <summary>
        /// Permet de construire l'interface graphique (boutons, menu, texte, dessins)
        /// </summary>
        public Introduction(Programme p_programme) : base(new ÉtatIntroduction(p_programme))
        {
            Fond = Constantes.Fond;

            m_logoIntro = new List<Texte>();

            ConstruireLogo();

            Texte texteDemarrer = new Texte(new Coordonnée(CentreX, 3 * QuartY + 140), "Vous pouvez appuyer sur Enter pour démarrer ou ESC pour quitter", Blanc, 15);
            AjouterÉlément(texteDemarrer);

            Bouton btnDémarrer = new Bouton(new Coordonnée(CentreX, 3 * QuartY + 60),
                new Dimension(200, 100), PacMan, JauneFonce,
                "Démarrer", 30, ÉtatProgramme.TypeActionBouton.Information);

            AjouterBouton(btnDémarrer);

            m_déplacement = Déplacement.Droite;

            Partie.Instance.Pacman.MettreAJourCoordonnée(new Coordonnée(-200, CentreY - 90));
            Partie.Instance.Blinky.MettreAJourCoordonnée(new Coordonnée(-250, CentreY - 90));
            Partie.Instance.Pinky.MettreAJourCoordonnée(new Coordonnée(-275, CentreY - 90));
            Partie.Instance.Inky.MettreAJourCoordonnée(new Coordonnée(-300, CentreY - 90));
            Partie.Instance.Clyde.MettreAJourCoordonnée(new Coordonnée(-325, CentreY - 90));

        }

        private void ConstruireLogo()
        {
            Texte intro = new Texte(new Coordonnée(CentreX, QuartY / 2), "PacMan rencontre", Blinky, 40);
            m_logoIntro.Add(intro);

            Texte intro2 = new Texte(new Coordonnée(CentreX, QuartY / 2), "\n\nDesign Pattern", Pinky, 40);
            m_logoIntro.Add(intro2);

            Texte nom = new Texte(new Coordonnée(CentreX, CentreY), "Bastien Roy-Mazoyer", Clyde, 25);
            m_logoIntro.Add(nom);
        }

        Déplacement m_déplacement;

        private readonly List<Texte> m_logoIntro;

        /// <summary>
        /// Les directives sur l'ordre et les choses a dessiner a chaque Draw()
        /// Cache la methode de base pour permettre l'animation de l'intro
        /// </summary>
        /// <param name="p_cptFrame">le cpt de Frame actuel</param>
        public new void DessinerTout(int p_cptFrame)
        {

            base.DessinerTout(p_cptFrame);

            TextFont(CreateFont("PacFont", 40));
            m_logoIntro.ForEach(c => c.Dessiner());
            TextFont(CreateFont("Microsoft", 40));

            Partie.Instance.Pacman.DéplacerPourLogo(p_cptFrame, (m_déplacement));
            Partie.Instance.Blinky.DéplacerPourLogo(p_cptFrame, (m_déplacement));
            Partie.Instance.Clyde.DéplacerPourLogo(p_cptFrame, (m_déplacement));
            Partie.Instance.Pinky.DéplacerPourLogo(p_cptFrame, (m_déplacement));
            Partie.Instance.Inky.DéplacerPourLogo(p_cptFrame, (m_déplacement));

            if (Partie.Instance.Pacman.Coordonnée.X < -200 || Partie.Instance.Pacman.Coordonnée.X >= Largeur + 200)
            {
                m_déplacement = (m_déplacement == Déplacement.Droite) ? Déplacement.Gauche : Déplacement.Droite;
                bool peur = (m_déplacement == Déplacement.Gauche);
                Partie.Instance.Blinky.ModifierÉtatPeur(peur);
                Partie.Instance.Clyde.ModifierÉtatPeur(peur);
                Partie.Instance.Pinky.ModifierÉtatPeur(peur);
                Partie.Instance.Inky.ModifierÉtatPeur(peur);
            }
        }

        public override void CapterClavier(int p_codeTouche)
        {
            if (p_codeTouche == ESC)
                Actions.Précédent();
            else
                Actions.AfficherInformations();
        }
    }
}
