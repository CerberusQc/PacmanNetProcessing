using DP_TP2.ObjetDessinables;
using DP_TP2.ObjetDessinables.ObjetJeuValeur;
using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetAnimables.ActeurAnimables
{
    internal class FruitAnimable : ObjetAnimable
    {
        internal FruitAnimable(Fruit p_fruit)
            : base(p_fruit.Coordonnée, p_fruit.Dimension,
                new ObjetDessinable[] {p_fruit}, new ObjetDessinable[] {p_fruit},
                new ObjetDessinable[] {p_fruit}, new ObjetDessinable[] {p_fruit},
                Constantes.VitesseAnimation, Constantes.VitesseFantôme)
        {
            m_fruit = p_fruit;
        }

        private readonly Fruit m_fruit;

        public int ObtenirValeurFruit()
        {
            return m_fruit.ValeurActuel;
        }
    }
}