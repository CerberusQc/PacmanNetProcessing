using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetDessinables.ObjetJeuValeur
{
    internal abstract class ObjetJeu : ObjetDessinable, IPointable
    {
        protected ObjetJeu(Coordonnée p_coordonnée, Dimension p_dimension, int p_valeur) : base(p_coordonnée, p_dimension)
        {
            Valeur = p_valeur;
        }

        private int Valeur { get; set; }

        public int ValeurActuel { get => Valeur; set => Valeur = value; }

        public abstract override void Dessiner();
      
    }
}
