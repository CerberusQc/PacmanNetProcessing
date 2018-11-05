using System;
using DP_TP2.InterfaceGraphique;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ÉtatProgrammes
{
    internal class ÉtatPause : ÉtatProgramme
    {
        public ÉtatPause(Programme p_programme) : base(p_programme)
        {
        }

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
            Partie.Instance.RedessinerMur();
            Programme.ModifierProgramme(new Jeu(Programme));
        }

        public override void Précédent()
        {
            Programme.ModifierProgramme(new Introduction(Programme));
        }

        public override void TerminéPartie(bool p_victoire)
        {
            throw new InvalidOperationException(MessageErreurEtatIllegal);
        }
    }
}
