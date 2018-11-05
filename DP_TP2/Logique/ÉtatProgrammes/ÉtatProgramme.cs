namespace DP_TP2.Logique.ÉtatProgrammes
{
    /// <summary>
    /// Classe abstraite du patron de conception Etat
    /// Va permettre au programme d'avoir un etat et de savoir quoi faire exactement
    /// et chacun des etats va savoir comment executer les fonctions et la transition entre les etats
    /// parfaitement
    /// </summary>
    internal abstract class ÉtatProgramme
    {
        public enum TypeActionBouton { Information, Introduction, DémarrerPartie, TerminéPartie, Précédent, Pause }

        protected ÉtatProgramme(Programme p_programme)
        {
            Programme = p_programme;
        }

        protected Programme Programme { get; }

        public abstract void AfficherIntroduction();

        public abstract void AfficherInformations();

        public abstract void DémarrerPartie();

        public abstract void TerminéPartie(bool p_victoire);

        public abstract void Précédent();

        public abstract void Pause();
    }
}
