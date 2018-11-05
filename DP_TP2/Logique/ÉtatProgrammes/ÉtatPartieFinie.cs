using System;
using DP_TP2.InterfaceGraphique;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ÉtatProgrammes
{
    internal class ÉtatPartieFinie : ÉtatProgramme
    {
        public ÉtatPartieFinie(Programme p_programme) : base(p_programme)
        {
        }

        public override void AfficherInformations()
        {
            Partie.Instance.RéinitialiserPartie();
            Programme.ModifierProgramme(new Informations(Programme));         
        }

        public override void AfficherIntroduction()
        {
            Partie.Instance.RéinitialiserPartie();
            Programme.ModifierProgramme(new Introduction(Programme));
        }

        public override void DémarrerPartie()
        {
            Partie.Instance.ProchainNiveau();
            Programme.ModifierProgramme(new Jeu(Programme));
        }

        public override void Pause()
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }

        public override void Précédent()
        {
            Partie.Instance.RéinitialiserPartie();
            Programme.ModifierProgramme(new Introduction(Programme));
        }

        public override void TerminéPartie(bool p_victoire)
        {
            Partie.Instance.RéinitialiserPartie();
            Programme.ModifierProgramme(new Introduction(Programme));
        }
    }
}
