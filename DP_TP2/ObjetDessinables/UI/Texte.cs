using System;
using DP_TP2.Utilitaire;
using NetProcessing;

namespace DP_TP2.ObjetDessinables.UI
{
    internal class Texte : ObjetDessinable
    {
        public Texte(Coordonnée p_coordonnée, String p_texteAffiché, Sketch.Color p_couleur, int p_taillePolice) : base(p_coordonnée, new Dimension(0,0))
        {
            TexteAffiché = p_texteAffiché;
            Couleur = p_couleur;
            TaillePolice = p_taillePolice;
        }

        String TexteAffiché { get; set; }
        Sketch.Color Couleur { get; }
        int TaillePolice { get; }

        public override void Dessiner()
        {
            Sketch.Fill(Couleur);
            Sketch.TextSize(TaillePolice);
            Sketch.TextAlign(Sketch.CENTER);
            Sketch.Text(TexteAffiché, Coordonnée.X, Coordonnée.Y + TaillePolice / 3);
        }

        public void MettreAJourTexte(String p_texte)
        {
            TexteAffiché = p_texte;
        }
    }
}
