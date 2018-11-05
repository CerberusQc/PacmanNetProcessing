using System;
using System.Collections.Generic;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.ObjetDessinables;
using DP_TP2.ObjetDessinables.UI;
using DP_TP2.Utilitaire;
using static NetProcessing.Sketch;

namespace DP_TP2.ProgrammeDessinables
{
    /// <summary>
    /// Squelette pour tout les etats du programmes qui vont etre dessinable
    /// </summary>
    internal abstract class ProgrammeDessinable : IProgrammeDessinable
    {
        protected ProgrammeDessinable(ÉtatProgramme p_actions)
        {
            ListeBoutons = new List<Bouton>();
            ListeÉléments = new List<ObjetDessinable>();
            Actions = p_actions;
        }

        private List<Bouton> ListeBoutons { get; }

        private List<ObjetDessinable> ListeÉléments { get; }

        public ÉtatProgramme Actions { get; set; }

        public Color Fond { get; set; }

        public void AjouterÉlément(ObjetDessinable p_objet)
        {
            ListeÉléments.Add(p_objet);
        }

        public void AjouterEnsembleÉléments(ObjetDessinable[] p_listes)
        {
            ListeÉléments.AddRange(p_listes);
        }

        public void AjouterBouton(Bouton p_bouton)
        {
            ListeBoutons.Add(p_bouton);
        }

        public void EnleverÉlément(ObjetDessinable p_objet)
        {
            ListeÉléments.Remove(p_objet);
        }

        public void EnleverEnsembleÉlémentsMemeType(Type p_type)
        {
            ListeÉléments.RemoveAll(c => c.GetType() == p_type);
        }

        public void DessinerTout(int p_cptFrame)
        {
        
            ListeBoutons.ForEach(b => b.Dessiner());
            ListeÉléments.ForEach(e => e.Dessiner());
        }

        public void VerifierBoutonsSiParDessus(Coordonnée p_coordonnée)
        {
            ListeBoutons.ForEach(b => b.EstParDessus(p_coordonnée));
        }

        public void Cliquer(Coordonnée p_coordonnée)
        {
            Bouton button = ListeBoutons.Find(b => b.EstParDessus(p_coordonnée));

            if (button != null)
            {
                switch (button.ObtenirAction())
                {
                    case ÉtatProgramme.TypeActionBouton.Information:
                        Actions.AfficherInformations();
                        break;
                    case ÉtatProgramme.TypeActionBouton.Introduction:
                        Actions.AfficherIntroduction();
                        break;
                    case ÉtatProgramme.TypeActionBouton.DémarrerPartie:
                        Actions.DémarrerPartie();
                        break;
                    case ÉtatProgramme.TypeActionBouton.TerminéPartie:
                        Actions.TerminéPartie(false);
                        break;
                    case ÉtatProgramme.TypeActionBouton.Précédent:
                        Actions.Précédent();
                        break;
                    case ÉtatProgramme.TypeActionBouton.Pause:
                        Actions.Pause();
                        break;
                    default: throw new InvalidProgramException("Le bouton contient un TypeActionBouton non implémenter");

                }
            }
        }

        public abstract void CapterClavier(int p_codeTouche);
    }
}
