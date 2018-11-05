using DP_TP2.InterfaceGraphique;
using DP_TP2.ProgrammeDessinables;
using DP_TP2.Utilitaire;
using System;
using static NetProcessing.Sketch;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique
{
    /// <summary>
    /// Va permettre de gerer l'etat du programme ainsi que de faire office de controlleur principale entre
    /// la vue et le modele
    /// </summary>
    internal class Programme : IProgrammeDessinable
    {
        public Programme()
        {
            // On débute toujours un programme avec l'intro
            m_programmes = new Introduction(this);
        }

        private ProgrammeDessinable m_programmes;

        public void ModifierProgramme(ProgrammeDessinable p_programme)
        {
            m_programmes = p_programme;
        }

        /// <summary>
        /// Va dessiner tout ce qui est contenue dans le programme et son etat en cours
        /// </summary>
        /// <param name="p_cptFrame"></param>
        public void DessinerTout(int p_cptFrame)
        {
            Type type = m_programmes.GetType();

            // On va forcer l'utilisation d'un new .DessinerTout() qui ecrase celui de la classe parent
            // dans 2 cas particulier car il doivent animer des objets
            if (type == typeof(Jeu) && m_programmes != null)
            {
                (m_programmes as Jeu)?.DessinerTout(p_cptFrame);
            }
            else if (type == typeof(Introduction) && m_programmes != null)
            {
                Background(Fond);

               if (p_cptFrame > 10)
                    (m_programmes as Introduction)?.DessinerTout(p_cptFrame);
            }
            else
            {
                Background(Fond);

                m_programmes?.DessinerTout(p_cptFrame);
            }
        }

        /// <summary>
        /// Va verifier si la souris est par dessus un bouton pour illuminer les boutons et prevoir les cliques
        /// </summary>
        /// <param name="p_coordonnée"></param>
        public void VerifierBoutonsSiParDessus(Coordonnée p_coordonnée)
        {
            m_programmes.VerifierBoutonsSiParDessus(p_coordonnée);
        }

        /// <summary>
        /// Va verifier si un action est a prendre selon l'etat du programme en cliquant
        /// </summary>
        /// <param name="p_coordonnée"></param>
        public void Cliquer(Coordonnée p_coordonnée)
        {
            m_programmes.Cliquer(p_coordonnée);
        }

        /// <summary>
        /// Va verifier les actions du clavier selon l'etat du programme
        /// </summary>
        /// <param name="p_codeTouche"></param>
        public void CapterClavier(int p_codeTouche)
        {
            m_programmes.CapterClavier(p_codeTouche);
        }
    }
}
