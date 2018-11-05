using System;
using DP_TP2.InterfaceGraphique;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ÉtatProgrammes
{
    internal class ÉtatJeu : ÉtatProgramme
    {
        public ÉtatJeu(Programme p_programme) : base(p_programme)
        {}

        public override void AfficherInformations()
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }

        public override void AfficherIntroduction()
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }

        public override void DémarrerPartie()
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }

        public override void Pause()
        {
            Programme.ModifierProgramme(new Pause(Programme));
            Partie.Instance.RedessinerMur();
        }

        public override void Précédent()
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }

        public override void TerminéPartie(bool p_victoire)
        {
            Programme.ModifierProgramme(new PartieFinie(Programme, p_victoire));
          //  Partie.Instance.ArrêterPartie();
            Partie.Instance.RedessinerMur();
        }
    }
}
