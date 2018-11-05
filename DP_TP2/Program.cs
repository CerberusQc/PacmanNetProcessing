/*
 * Jeu inspire du celebre PacMan
 * Fait sous NetProcessing
 * Dans le cadre du cours de Design Pattern
 * 
 * Création :  4 Mai 2018
 * Dernière modification le : 25 Mai 2018
 * 
 * Auteur : Bastien Roy-Mazoyer
 * 
 */

using DP_TP2.Logique;
using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2
{
    class Program : NetProcessing.Sketch
    {
        static void Main()
        {
            new Program().Start();
        }

        public override void Setup()
        {
            ImageMode(CENTER);
            HideConsole();
            Size(Largeur, Hauteur);
            m_coordonnéeSouris = new Coordonnée(0, 0);

            m_programme = new Programme();
            FrameRate(50);
        }

        Coordonnée m_coordonnéeSouris;

        Programme m_programme;

        public override void Draw()
        {
            m_programme.DessinerTout(FrameCount);
        }

        public override void MouseMoved()
        {         
            try
            {
                // On met a jours la variable pour une utilisation plus naturel de Coordonnee : 
                m_coordonnéeSouris.X = MouseX;
                m_coordonnéeSouris.Y = MouseY;

                m_programme.VerifierBoutonsSiParDessus(m_coordonnéeSouris);
            }
            catch
            {
                // Pour éviter les erreurs lors du démarrage de NetProcessing
                // qui capte parfois les mouvements de la souris avant que la grille soit initialiser
            }
        }

        // SECTION ON EVENT 
        // Pour capter les actions de la souris ou du clavier

        public override void MouseClicked()
        {
            m_programme.Cliquer(m_coordonnéeSouris);
        }

        public override void KeyPressed()
        {
            m_programme.CapterClavier(KeyCode);
        }
    }
}
