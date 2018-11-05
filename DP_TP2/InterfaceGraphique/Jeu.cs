using DP_TP2.Logique;
using DP_TP2.ProgrammeDessinables;
using DP_TP2.Utilitaire;
using System.Collections.Generic;
using static NetProcessing.Sketch;
using static DP_TP2.Utilitaire.Constantes;
using DP_TP2.ObjetDessinables;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.ObjetDessinables.Acteur;
using DP_TP2.ObjetDessinables.UI;

namespace DP_TP2.InterfaceGraphique
{
    internal class Jeu : ProgrammeDessinable
    {
        public Jeu(Programme p_programme) : base(new ÉtatJeu(p_programme))
        {
            
            m_vies = new List<ObjetDessinable>();

            Fond = Constantes.Fond;

            Texte oneUp = new Texte(new Coordonnée(QuartX, 25), "1UP", Lettres, 25);
            m_pointage = new Texte(new Coordonnée(QuartX, 50), Partie.Instance.Score.ToString("D3"), Lettres, 25);

            Texte record = new Texte(new Coordonnée(3 * QuartX, 25), "Record", Lettres, 25);
            m_record = new Texte(new Coordonnée(3 * QuartX, 50), Partie.Instance.Record.ToString("D3"), Lettres, 25);

            Texte niveau = new Texte(new Coordonnée(3 * QuartX + 100, 25), "Niveau", Lettres, 25);
            m_niveau = new Texte(new Coordonnée(3 * QuartX + 100, 50), Partie.Instance.Niveau.ToString("D3"), Lettres, 25);

           AjouterEnsembleÉléments(new ObjetDessinable[] { oneUp, m_pointage, record, m_record, niveau, m_niveau });

            Bouton aide = new Bouton(new Coordonnée(CentreX, 37), new Dimension(200, 55),
                PacMan, JauneFonce, "AIDE (F1)", 30, ÉtatProgramme.TypeActionBouton.Pause);
            AjouterBouton(aide);

            MettreAJourVies();
        }

        readonly Texte m_pointage;

        readonly Texte m_record;

        readonly Texte m_niveau;

        private Texte m_vie;

        private readonly List<ObjetDessinable> m_vies;

        public void MettreAJourScoreRecord()
        {
            m_pointage.MettreAJourTexte(Partie.Instance.Score.ToString("D3"));
            m_record.MettreAJourTexte(Partie.Instance.Record.ToString("D3"));
            m_niveau.MettreAJourTexte(Partie.Instance.Niveau.ToString("D3"));

            if (Partie.Instance.Compteur == ConditionVictoire)
                Actions.TerminéPartie(true);
        }

        /// <summary>
        /// Va permette d'aller verifier dans l'etat de la partie son nombre de vie pour mettre a jour
        /// l'affichage et va aller changer son etat si le nombre atteint 0
        /// </summary>
        public void MettreAJourVies()
        {
            if (m_vies.Count != Partie.Instance.ObtenirNbVies())
            {
                int nbVie = Partie.Instance.ObtenirNbVies();

                if (nbVie == 0)
                {
                    Actions.TerminéPartie(false);
                }

                EnleverEnsembleÉlémentsMemeType(typeof(ObjetPacMan));
                EnleverÉlément(m_vie);

                m_vies.Clear();

                if (nbVie > 3)
                {
                    ObjetPacMan vie = new ObjetPacMan(new Coordonnée(50, 25), 1, ObjetDessinable.Orientation.Droite);
                    m_vie = new Texte(new Coordonnée(75, 25), " x" + nbVie, Lettres, 25);

                    m_vies.Add(vie);
                   AjouterÉlément(m_vie);
                    AjouterÉlément(vie);
                }
                else
                {
                    for (int i = 0; i < nbVie; i++)
                    {
                        ObjetPacMan vie = new ObjetPacMan(new Coordonnée(50 + (25 * i), 25), 1, ObjetDessinable.Orientation.Droite);
                        m_vies.Add(vie);
                    }

                    AjouterEnsembleÉléments(m_vies.ToArray());
                }
            }
        }

        /// <summary>
        /// Les directives sur l'ordre et les choses a dessiner a chaque Draw()
        /// </summary>
        /// <param name="p_cptFrame">le cpt de Frame actuel</param>
        public new void DessinerTout(int p_cptFrame)
        {
            //Pour afficher une bande de rafraichissement durant le jeu
            Fill(Constantes.Fond);
            Rect(CentreX, 25, Largeur, 100);

            base.DessinerTout(p_cptFrame);

            Partie.Instance.Grille.DessinerTout();

            Partie.Instance.CalculerComportement();
            Partie.Instance.Animer(p_cptFrame);

            MettreAJourScoreRecord();
            MettreAJourVies();
        }

        public override void CapterClavier(int p_codeTouche)
        {
            if (p_codeTouche == KC_UP)      // Bouge Haute
            {
                Partie.Instance.DéplacerPacMan(Déplacement.Haut);
            }

            if (p_codeTouche == KC_DOWN)    // Bouge Bas
            {
                Partie.Instance.DéplacerPacMan(Déplacement.Bas);
            }

            if (p_codeTouche == KC_LEFT)    // Bouge Gauche
            {
                Partie.Instance.DéplacerPacMan(Déplacement.Gauche);
            }

            if (p_codeTouche == KC_RIGHT)   // Bouge Droite
            {
                Partie.Instance.DéplacerPacMan(Déplacement.Droite);
            }

            if (p_codeTouche == KC_F1)      // Menu Pause
            {
                Actions.Pause();
            }
        }
    }
}
