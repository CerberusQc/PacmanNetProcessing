using System;
using DP_TP2.Utilitaire;
using DP_TP2.ÉlémentNiveau.ÉlémentJeuDessinable;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;
using Mur = DP_TP2.ÉlémentNiveau.ÉlémentJeuDessinable.Mur;

namespace DP_TP2.ÉlémentNiveau
{
    /// <summary>
    /// Permet de generer et manipuler la grille de jeu qui sera de base 30x30
    /// </summary>
    class GrilleJeu
    {
        public GrilleJeu(bool p_animation)
        {
            int dimension = TailleGrilleJeu / TailleCase;

            m_grille = new Case[dimension][];

            for (int ligne = 0; ligne < dimension; ligne++)
            {
                m_grille[ligne] = new Case[dimension];

                for (int colonne = 0; colonne < dimension; colonne++)
                {
                    m_grille[ligne][colonne] = new Corridor(new Coordonnée(ligne * TailleCase + 50 + (TailleCase / 2), colonne * TailleCase + 90));
                }
            }

            if (!p_animation)
            {
                m_grille.GénérerPartieClassique();
                Mur.GénérerNouvelleCouleur();
            }
        }

        private readonly Case[][] m_grille;

        public void DessinerTout()
        {
            foreach (var t in m_grille)
            {
                for (int colonne = 0; colonne < m_grille.Length; colonne++)
                {
                    t[colonne].Dessiner();
                    
                    // Decommenter pour faire afficher les numeros de la grille
                    //AfficherGrilleNumeroter(m_grille[ligne][colonne]);
                }
            }
        }

        public Coordonnée TrouverCoordonnéeCase(int p_x, int p_y)
        {
            return new  Coordonnée(m_grille[p_x][p_y].ObtenirCoordonnée());
        }

        /// <summary>
        /// Va permette de verifier si la prochaine case est une collision avec un objet non traversable comme un mur
        /// </summary>
        /// <param name="p_coordonnée">les coordonnes actuel de l'objet a verifier</param>
        /// <param name="p_valeur">la largeur de son deplacement</param>
        /// <param name="p_déplacement">la direction de son deplacement</param>
        /// <returns>Si la prochain deplacement est valide, retourne vrai</returns>
        public bool VérifierProchaineCaseTraversable(Coordonnée p_coordonnée, int p_valeur, Déplacement p_déplacement)
        {
            int x = (p_coordonnée.X - (50 + (TailleCase / 2))) / TailleCase;
            int y = (p_coordonnée.Y - 90) / TailleCase;

            Case prochaineCase;

            switch (p_déplacement)
            {
                case Déplacement.Haut:
                    {
                        prochaineCase = m_grille[x][y - 1];

                        return
                            p_coordonnée.X == prochaineCase.ObtenirCoordonnée().X
                            && (prochaineCase.EstTraversable || (!prochaineCase.EstTraversable
                            && (p_valeur >= (prochaineCase.ObtenirCoordonnée().Y + TailleCase / 2))));
                    }
                case Déplacement.Bas:
                    {
                        prochaineCase = m_grille[x][y + 1];

                        return
                            p_coordonnée.X == prochaineCase.ObtenirCoordonnée().X
                            && (prochaineCase.EstTraversable || (!prochaineCase.EstTraversable
                            && (p_valeur <= (prochaineCase.ObtenirCoordonnée().Y - TailleCase / 2))));
                    }
                case Déplacement.Gauche:
                    {
                        prochaineCase = m_grille[x - 1][y];
                        return 
                            p_coordonnée.Y == prochaineCase.ObtenirCoordonnée().Y 
                            && (prochaineCase.EstTraversable || (!prochaineCase.EstTraversable 
                            && (p_valeur >= (prochaineCase.ObtenirCoordonnée().X + TailleCase / 2))));
                    }
                case Déplacement.Droite:
                    {
                        prochaineCase = m_grille[x + 1][y];
                        return 
                            p_coordonnée.Y == prochaineCase.ObtenirCoordonnée().Y 
                            && (prochaineCase.EstTraversable || (!prochaineCase.EstTraversable 
                            && (p_valeur <= (prochaineCase.ObtenirCoordonnée().X - TailleCase / 2))));
                    }
                case Déplacement.Arrêt:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(p_déplacement), p_déplacement, null);
            }

            return false;
        }

        /// <summary>
        /// Va permettre de verifier les cases que Pacman traverse en enlevant les objets qui si trouve, si valide
        /// </summary>
        /// <param name="p_coordonnée">les coordonnes de l'objet a verifier</param>
        public void VérifierCaseActuelle(Coordonnée p_coordonnée)
        {
            int x = (p_coordonnée.X - (50 + (TailleCase / 2))) / TailleCase;
            int y = (p_coordonnée.Y - 90) / TailleCase;

            Case caseActuelle = m_grille[x][y];
            Coordonnée caseActuelleCoord = caseActuelle.ObtenirCoordonnée();

            if (caseActuelle.GetType() == typeof(Corridor) && (p_coordonnée.X == caseActuelleCoord.X && p_coordonnée.Y == caseActuelleCoord.Y))
            {
                Corridor corridor = caseActuelle as Corridor;

                corridor?.EnleverObjet();
            }

        }

        /// <summary>
        /// Seulement pour le developpement, fait afficher les numeros des cases sur la grille
        /// </summary>
        /// <param name="c">La case a faire afficher le numero</param>
        private static void AfficherGrilleNumeroter(Case c)
        {
            Coordonnée co = c.ObtenirCoordonnée();
            int x = (co.X - (50 + (TailleCase / 2))) / TailleCase;
            int y = (co.Y - 90) / TailleCase;

            Fill(Blanc);
            TextSize(15);
            if (x == 0)
                Text(y.ToString(), co.X, co.Y);

            if (y == 0)
                Text(x.ToString(), co.X, co.Y);
        }
    }
}
