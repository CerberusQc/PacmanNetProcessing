using DP_TP2.Utilitaire;
using NetProcessing;

namespace DP_TP2.ObjetDessinables.ObjetJeuValeur
{
    /// <summary>
    /// Les petits points jaunes sur la carte qui sont la base du pointage de PacMan
    /// </summary>
    internal class Point : ObjetJeu
    {
        public Point(Coordonnée p_coordonnée) : base(p_coordonnée, new Dimension(Constantes.TailleCase / 4, Constantes.TailleCase / 4), Constantes.ValeurPoint)
        {}

        public override void Dessiner()
        {
            Sketch.Fill(Constantes.Point);
            Sketch.NoStroke();
            Sketch.Ellipse(Coordonnée.X, Coordonnée.Y, Dimension.Largeur, Dimension.Hauteur);
        }
    }
}
