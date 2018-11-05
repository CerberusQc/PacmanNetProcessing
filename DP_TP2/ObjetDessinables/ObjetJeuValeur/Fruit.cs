using System;
using DP_TP2.Utilitaire;
using NetProcessing;

namespace DP_TP2.ObjetDessinables.ObjetJeuValeur
{
    internal class Fruit : ObjetJeu
    {
        public Fruit(Coordonnée p_coordonnée, Sketch.PImage p_image, String p_description, int p_valeur, Sketch.Color p_couleurPrincipale) : base(p_coordonnée, new Dimension(p_image.Width, p_image.Height), p_valeur)
        {
            Image = p_image;
            Description = p_description;
            CouleurPrincipale = p_couleurPrincipale;
        }

        Sketch.PImage Image { get; }

        public String Description { get; }

        public Sketch.Color CouleurPrincipale { get; }

        public override void Dessiner()
        {
            Sketch.Image(Image, Coordonnée.X, Coordonnée.Y);
        }

        public override string ToString()
        {
            return Description + "\n Point : " + ValeurActuel;
        }
    }
}
