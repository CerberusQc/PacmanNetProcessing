using System;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.Utilitaire;
using NetProcessing;

namespace DP_TP2.ObjetDessinables.UI
{
    internal class Bouton : ObjetDessinable
    {
        public Bouton(Coordonnée p_coordonnée, Dimension p_dimension, Sketch.Color p_couleur, Sketch.Color p_couleurSelectionné, String p_texte,int p_taillePolice, ÉtatProgramme.TypeActionBouton p_action) : base(p_coordonnée, p_dimension)
        {
            EstSélectionné = false;
            Texte = p_texte;
            Couleur = p_couleur;
            CouleurSelectionné = p_couleurSelectionné;
            Action = p_action;
            TaillePolice = p_taillePolice;
        }

        String Texte { get; set; }
        Sketch.Color Couleur { get; }
        Sketch.Color CouleurSelectionné { get; }
        ÉtatProgramme.TypeActionBouton Action { get; set; }
        bool EstSélectionné { get; set; }

        int TaillePolice { get; }

        public override void Dessiner()
        {
            Sketch.RectMode(Sketch.CENTER);
            Sketch.Fill(EstSélectionné ? CouleurSelectionné : Couleur);

            Sketch.StrokeWeight(10);
            Sketch.Rect(Coordonnée.X, Coordonnée.Y, Dimension.Largeur - Dimension.Hauteur, Dimension.Hauteur);

            int calculXCercleGauche = Coordonnée.X - Dimension.Largeur / 2 + Dimension.Hauteur / 2;
            int calculXCercleDroite = Coordonnée.X + Dimension.Largeur / 2 - Dimension.Hauteur / 2;

            Sketch.Ellipse(calculXCercleGauche, Coordonnée.Y, Dimension.Hauteur, Dimension.Hauteur);
            Sketch.Ellipse(calculXCercleDroite, Coordonnée.Y, Dimension.Hauteur, Dimension.Hauteur);

            // On Triche en redessinant un autre rectangle mais sans bordure pour faire disparaitre 2 lignes au centres : 
            Sketch.NoStroke();
            Sketch.Rect(Coordonnée.X, Coordonnée.Y, Dimension.Largeur - Dimension.Hauteur, Dimension.Hauteur);

            new Texte(Coordonnée, Texte, Constantes.Noir, TaillePolice).Dessiner();
        }

        public bool EstParDessus(Coordonnée p_coordonnée)
        {
            return EstSélectionné = (Coordonnée.X - (Dimension.Largeur / 2) <= p_coordonnée.X)  // Limite Gauche
                                        &&
                                      (p_coordonnée.X <= Coordonnée.X + (Dimension.Largeur / 2))  // Limite Droite
                                        &&
                                      (Coordonnée.Y - (Dimension.Hauteur / 2) <= p_coordonnée.Y)  // Limite Haut 
                                        &&
                                      (p_coordonnée.Y <= Coordonnée.Y + (Dimension.Hauteur / 2)); // Limite Bas
        }

        public void MettreAJourTexte(String p_nouveauTexte)
        {
            Texte = p_nouveauTexte;
        }

        public void MettreAJourAction(ÉtatProgramme.TypeActionBouton p_action)
        {
            Action = p_action;
        }
        public ÉtatProgramme.TypeActionBouton ObtenirAction()
        {
            return Action;
        }
    }
}
