using DP_TP2.Logique;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.ObjetDessinables.UI;
using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.InterfaceGraphique
{
    internal class Pause : Informations
    {
        public Pause(Programme p_programme) : base(new ÉtatPause(p_programme))
        {
            Texte pause = new Texte(new Coordonnée(CentreX, 50), "PAUSE", FantômePeur, 50);
            InformationsTouche.MettreAJourTexte("Appuyer sur une touche pour reprendre");

            Precedent.MettreAJourTexte("Quitter");
            Suivant.MettreAJourTexte("Continuer");
            Suivant.MettreAJourAction(ÉtatProgramme.TypeActionBouton.Pause);

            AjouterÉlément(pause);
        }

        public override void CapterClavier(int p_codeTouche)
        {
            if (p_codeTouche == ESC)
            {
                Actions.Précédent();
            }
            else if (p_codeTouche != KC_F1)
            {
                Actions.Pause();
            }
        }
    }
}
