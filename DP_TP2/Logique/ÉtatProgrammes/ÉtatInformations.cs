using System;
using DP_TP2.InterfaceGraphique;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.Logique.ÉtatProgrammes
{
    internal class ÉtatInformations : ÉtatProgramme
    {
        public ÉtatInformations(Programme p_programme) : base(p_programme)
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
            Partie.Instance.RéinitialiserPartie();
            Programme.ModifierProgramme(new Jeu(Programme));        
        }

        public override void Pause()
        {
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
