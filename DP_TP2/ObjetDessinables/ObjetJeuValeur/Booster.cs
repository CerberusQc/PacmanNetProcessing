using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.ObjetDessinables.ObjetJeuValeur
{
    /// <summary>
    /// Les plus gros point qui serve a faire peur aux fantomes
    /// </summary>
    internal class Booster : ObjetJeu
    {
        public Booster(Coordonnée p_coordonnée) : base(p_coordonnée, new Dimension(TailleCase / 3 * 2, TailleCase / 3 * 2), ValeurBooster)
        {
        }

        public override void Dessiner()
        {
            Fill(Constantes.Point);
            NoStroke();
            Ellipse(Coordonnée.X, Coordonnée.Y, Dimension.Largeur, Dimension.Hauteur);
        }
    }
}
