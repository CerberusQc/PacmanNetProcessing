using System;
using DP_TP2.InterfaceGraphique;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ÉtatProgrammes
{
    internal class ÉtatIntroduction : ÉtatProgramme
    {
        public ÉtatIntroduction(Programme p_programme) : base(p_programme)
        {
        }

        public override void AfficherInformations()
        {
            Programme.ModifierProgramme(new Informations(Programme));
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
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }

        public override void Précédent()
        {
            NetProcessing.Sketch.Exit();
        }

        public override void TerminéPartie(bool p_victoire)
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }
    }
}
