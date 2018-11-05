using DP_TP2.ÉlémentNiveau;
using DP_TP2.Utilitaire;
using System;
using System.Collections.Generic;
using System.Linq;
using DP_TP2.Logique.ComportementFantômes;
using DP_TP2.ObjetAnimables.ActeurAnimables;
using DP_TP2.ObjetDessinables.ObjetJeuValeur;
using static DP_TP2.ObjetAnimables.ObjetAnimable;
using static DP_TP2.ObjetDessinables.Acteur.ObjetFantôme;
using static DP_TP2.Utilitaire.Constantes;
using static NetProcessing.Sketch;

namespace DP_TP2.Logique
{
    internal class Partie
    {
        //Variables membres privees : 

        private readonly List<Fantôme> m_listeFantômes;

        private readonly List<Fantôme> m_listeFantômesActifs;

        private readonly Queue<Fantôme> m_listeFantômesMangers;

        private readonly Queue<FruitAnimable> m_listeFruits;

        private FruitAnimable m_fruitActif;

        private readonly ComportementClyde m_comportementFruit; // Les fruits vont avoir le meme comportement que Clyde (Aleatoire)

        private readonly IntelligenceArtificielle m_ia;

        private int m_cptAction;
        private int m_cptFantômesMorts;

        private int m_cptPointPourVie;
        private int m_nbVies;

        private int m_cptFruit;

        private int m_cptFantômePeur;

        /// <summary>
        /// Constructeur par defaut d'une partie, va instancier tout les elements
        /// necessaire a la partie
        /// </summary>
        private Partie()
        {
            FrameRate(50);
            Score = 0;
            Record = 0;
            m_cptPointPourVie = 1;
            m_nbVies = 3;
            Grille = new GrilleJeu(false);

            Pacman = new PacMan(new Coordonnée(CentreX, Grille.TrouverCoordonnéeCase(0, 17).Y));

            m_listeFantômes = new List<Fantôme>();
            m_listeFantômesActifs = new List<Fantôme>();
            m_listeFantômesMangers = new Queue<Fantôme>();
            m_listeFruits = new Queue<FruitAnimable>();
            m_fruitActif = null;
            m_cptFantômesMorts = 0;

            Blinky = new Fantôme(Grille.TrouverCoordonnéeCase(12, 13), CouleurFantôme.Blinky);
            Pinky = new Fantôme(Grille.TrouverCoordonnéeCase(17, 13), CouleurFantôme.Pinky);
            Inky = new Fantôme(Grille.TrouverCoordonnéeCase(12, 15), CouleurFantôme.Inky);
            Clyde = new Fantôme(Grille.TrouverCoordonnéeCase(17, 15), CouleurFantôme.Clyde);

            m_listeFantômes.AddRange(new[] { Blinky, Pinky, Inky, Clyde });

            m_comportementFruit = new ComportementClyde(); // Les fruits vont avoir le meme comportement que Clyde (Aleatoire)

            m_ia = new IntelligenceArtificielle();

            MurDejaDessiner = false;

            Niveau = 1;

            m_cptAction = 0;
            m_cptFantômePeur = 0;
            m_cptFruit = FrameRateValue * 2;
        }


        /// <summary>
        /// Et Oui, le singleton, car il ne serait pas logique d'avoir plusieurs
        /// partie en meme temps alors le singleton semble adapter pour une partie
        /// </summary>
        internal static Partie Instance { get; } = new Partie();

        // Proprieter de la classe : 

        internal int Score { get; private set; }

        internal int Record { get; private set; }

        internal int Niveau { get; private set; }

        internal int Compteur { get; private set; }

        internal bool MurDejaDessiner { get; private set; }

        internal GrilleJeu Grille { get; private set; }

        internal PacMan Pacman { get; }

        internal Fantôme Blinky { get; }

        internal Fantôme Pinky { get; }

        internal Fantôme Inky { get; }

        internal Fantôme Clyde { get; }


        // Methode de la classe

        /// <summary>
        /// Permet de changer la valeur du score et de mettre une vie supplementaire
        /// si jamaisle bon nombre de point est atteint
        /// </summary>
        /// <param name="p_point">Le nombre de point a ajouter au score actuel</param>
        internal void MettreAJourScore(int p_point)
        {
            Score += p_point;

            if (p_point <= ValeurBooster)
                ++Compteur;

            if (Score / ConditionVie == m_cptPointPourVie)
            {
                m_cptPointPourVie++;
                m_nbVies++;
            }

            if (Score > Record) // Pour mettre a jour le record 
                Record = Score;
        }

        /// <summary>
        /// Redemarrer une partie au complet en resettant le score
        /// </summary>
        internal void RéinitialiserPartie()
        {
            RedessinerMur();
            Compteur = 0;
            Score = 0;
            m_cptPointPourVie = 1;
            Niveau = 1;
            RéinitialiserPositions();
            m_listeFantômesActifs.Clear();
            MurDejaDessiner = false;
            Grille = new GrilleJeu(false);
            m_cptFruit = FrameRateValue * 2;
            m_nbVies = 3;
            m_cptAction = 0;
            m_cptFantômePeur = 0;

            m_listeFantômesMangers.Clear();
            m_cptFantômesMorts = 0;

            int vitesseAnimation = 50 + Niveau;

            FrameRate(vitesseAnimation <= 100 ? vitesseAnimation : 100); // le taux de rafraichissement va affecter la difficulte (rapidite animation)

            RemplirFruits();
        }

        /// <summary>
        /// Ramene tout les objets animables a leur position initiales
        /// </summary>
        private void RéinitialiserPositions()
        {
            Pacman.MettreAJourCoordonnée(new Coordonnée(CentreX, Grille.TrouverCoordonnéeCase(0, 17).Y));
            Pacman.Déplacer(Déplacement.Arrêt);

            m_listeFantômes.ForEach(f =>
            {
                f.RéinitialiserPosition();
                f.Déplacer(Déplacement.Arrêt);
            });
        }

        /// <summary>
        /// Permet de remettre certain elements a 0 mais d'augmenter le niveau
        /// et de faire une nouvelle partie
        /// </summary>
        internal void ProchainNiveau()
        {
            RedessinerMur();
            m_cptFruit = FrameRateValue * 2;
            Compteur = 0;
            Niveau++;
            RéinitialiserPositions();
            Grille = new GrilleJeu(false);
            m_cptAction = 0;
            m_cptFantômePeur = 0;
            m_listeFantômesActifs.Clear();
            m_listeFantômesMangers.Clear();
            m_cptFantômesMorts = 0;

            int vitesseAnimation = 50 + Niveau;

            FrameRate(vitesseAnimation <= 100 ? vitesseAnimation : 100); // le taux de rafraichissement va affecter la difficulte (rapidite animation)

            RemplirFruits();
        }

        /// <summary>
        /// Permet de ramener les elements en place apres la mort du joueur (pacman) par un fantome
        /// </summary>
        internal void RéinitialiserAprèsMort()
        {
            RedessinerMur();
            RéinitialiserPositions();
            m_listeFantômesActifs.Clear();
            m_cptAction = 0;
            m_cptFantômePeur = 0;
            m_listeFantômesMangers.Clear();
            m_cptFantômesMorts = 0;
            m_cptFruit = FrameRateValue * 2;

            m_fruitActif = null; // on perd le fruit en cours
        }

        /// <summary>
        /// Fait partie du sequenceur d'animation, permet d'appliquer le comportement de chaque fantomes actifs
        /// </summary>
        internal void CalculerComportement()
        {
            VérifierGénérateurÉvénement();
            m_listeFantômesActifs.ForEach(f => f.Déplacer(m_ia.AppliquerComportement(f, Pacman)));

            m_fruitActif?.Déplacer(m_comportementFruit.AppliquerInstinct(m_fruitActif.Coordonnée, Pacman.Coordonnée,
                Pacman.DéplacementActuel, false));
        }


        /// <summary>
        /// Permet de verifier si des collisions entre pacman et des objets (fantomes,fruits) arrivent ainsi que
        /// d'appliquer l'evenement relier au collision
        /// </summary>
        private void VérifierCollision()
        {
            Fantôme fantôme = m_listeFantômesActifs.Find(c => c.Coordonnée.EstProche(Pacman.Coordonnée, TailleCase / 4));

            if (m_fruitActif != null && m_fruitActif.Coordonnée.EstProche(Pacman.Coordonnée, TailleCase / 4))
            {
                MettreAJourScore(m_fruitActif.ObtenirValeurFruit());
                m_fruitActif = null;
            }

            if (fantôme == null) return;

            if (fantôme.EstApeuré())
            {
                MettreFantômeManger(fantôme);
            }
            else
            {
                m_nbVies--;
                RéinitialiserAprèsMort();
            }
        }

        /// <summary>
        /// Action a faire lorsque Pacman mange un fantome
        /// </summary>
        /// <param name="fantôme">le fantome a retourner</param>
        private void MettreFantômeManger(Fantôme fantôme)
        {
            fantôme.RéinitialiserPosition();

            m_listeFantômesActifs.Remove(fantôme);
            m_listeFantômesMangers.Enqueue(fantôme);

            m_cptFantômesMorts += (FrameRateValue * 6 - (Niveau / 5 * FrameRateValue / 2));
            MettreAJourScore(PointagePremierFantôme * (int)Math.Pow(2, m_listeFantômesMangers.Count));
        }

        /// <summary>
        /// Permet de modifier l'etat de peur de tout les fantomes actifs
        /// </summary>
        /// <param name="p_état">Vrai si les fantomes actifs doivent avoir peur</param>
        public void ModifierEtatPeurFantomes(bool p_état)
        {
            if (p_état)
            {
                m_cptFantômePeur = FrameRateValue * 6 - (Niveau / 5 * FrameRateValue / 2); // 6 Secondes - une demie seconde par 5 niveaux  

                // jusqua un maximum de 1 seconde
                if (m_cptFantômePeur <= FrameRateValue * 1)
                    m_cptFantômePeur = FrameRateValue * 1;
            }

            m_listeFantômes.ForEach(c => c.ModifierÉtatPeur(p_état));
        }

        /// <summary>
        /// Va etre appeler a chaque frame pour permettre aux acteurs de s'animer sur la grille de jeu
        /// </summary>
        /// <param name="p_cptFrame">Le nombre de frame actuel</param>
        internal void Animer(int p_cptFrame)
        {
            Pacman.Animer(p_cptFrame);

            m_listeFantômes.ForEach(f => f.Animer(p_cptFrame));

            m_fruitActif?.Animer(p_cptFrame);

            if (!MurDejaDessiner)
                MurDejaDessiner = true;

            VérifierCollision();

            if (m_cptFantômePeur != 0)
            {
                m_cptFantômePeur--;

                if (m_cptFantômePeur == 1)
                {
                    ModifierEtatPeurFantomes(false);
                }
            }

            if (m_listeFantômesMangers.Count != 0)
            {
                VérifierSiFantômeÀRelacher();
            }
        }

        /// <summary>
        /// Apres que les fantomes aillent ete manger, permet de savoir si un fantome doit etre relachger
        /// </summary>
        private void VérifierSiFantômeÀRelacher()
        {
            // On va relacher un fantome chaque 6 secondes - (1 demie secondes par niveau)
            if (--m_cptFantômesMorts % (FrameRateValue * 6 - (Niveau / 5 * FrameRateValue / 2)) == 0)
            {
                Fantôme fantôme = m_listeFantômesMangers.Dequeue();
                m_listeFantômesActifs.Add(fantôme);
                fantôme.MettreAJourCoordonnée(Grille.TrouverCoordonnéeCase(14, 11));
            }
        }

        public void DéplacerPacMan(Déplacement p_déplacement)
        {
            Pacman.Déplacer(p_déplacement);
        }

        /// <summary>
        /// Va verifier de maniere sequentiellement avec un chrono
        /// (FramerateValue * Nombre de secondes voulu) car sera appeler a chaque Draw()
        /// et que le Draw() se fait appeler X nombre de fois par seconde
        /// FrameRateValue correspond au nombre d'appel par seconde
        ///
        /// On va ajouter progressivement les fantomes,
        /// puis laisser le generateur de fruits faire son boulot selon
        /// </summary>
        private void VérifierGénérateurÉvénement()
        {
            m_cptAction++;

            if (m_cptAction == FrameRateValue * 2)
            {
                AjouterBlinky();
            }

            if (m_cptAction == FrameRateValue * 4)
            {
                AjouterPinky();
            }

            if (m_cptAction == FrameRateValue * 6)
            {
                AjouterInky();
            }

            if (m_cptAction == FrameRateValue * 10)
            {
                AjouterClyde();
            }

            if (m_cptAction >= FrameRateValue * 10 && m_listeFruits.Any())
            {
                VérifierApparitionFruit();
            }
        }

        /// <summary>
        /// Va tenter de faire apparraitre un fruit dans la carte de jeux
        /// une probabilite de 1 sur 80 en honneur a Pacman et sa date de sortie 1980
        /// </summary>
        private void VérifierApparitionFruit()
        {
            Random random = new Random();

            if (m_fruitActif == null && m_cptFruit != 0)
                --m_cptFruit;


            if (m_cptFruit == 0 && random.Next(0, 100) == 80) // une chance sur 80 a 60 fois par secondes
            {
                m_cptFruit = FrameRateValue * 4 + (Niveau / 5 * FrameRateValue / 2); // 4 secondes + une demie seconde par niveau
                m_fruitActif = m_listeFruits.Dequeue();
            }
        }

        private void AjouterBlinky()
        {
            Blinky.MettreAJourCoordonnée(Grille.TrouverCoordonnéeCase(14, 11));
            m_listeFantômesActifs.Add(Blinky);
            m_ia.RéinitialiserBlinky();
        }

        private void AjouterPinky()
        {
            Pinky.MettreAJourCoordonnée(Grille.TrouverCoordonnéeCase(14, 11));
            m_listeFantômesActifs.Add(Pinky);
            m_ia.RéinitialiserPinky();
        }

        private void AjouterClyde()
        {
            Clyde.MettreAJourCoordonnée(Grille.TrouverCoordonnéeCase(14, 11));
            m_listeFantômesActifs.Add(Clyde);
            m_ia.RéinitialiserClyde();
        }

        private void AjouterInky()
        {
            Inky.MettreAJourCoordonnée(Grille.TrouverCoordonnéeCase(14, 11));
            m_listeFantômesActifs.Add(Inky);
            m_ia.RéinitialiserInky();
        }

        /// <summary>
        /// Va permettre le rafraichissement de l'imprimage des murs de la grille de jeu
        /// </summary>
        internal void RedessinerMur()
        {
            MurDejaDessiner = false;
        }

        internal int ObtenirNbVies()
        {
            return m_nbVies;
        }

        /// <summary>
        /// Va generer tout les fruits relier au niveau actuel et les mettre dans une File
        /// </summary>
        internal void RemplirFruits()
        {
            m_fruitActif = null;
            m_listeFruits.Clear();

            Coordonnée centreXCase17 = new Coordonnée(CentreX, Grille.TrouverCoordonnéeCase(0, 17).Y);

            m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerCerise(new Coordonnée(centreXCase17))));

            if (Niveau >= 2)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerFraise(new Coordonnée(centreXCase17))));
            if (Niveau >= 3)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerOrange(new Coordonnée(centreXCase17))));

            if (Niveau >= 5)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerPomme(new Coordonnée(centreXCase17))));

            if (Niveau >= 7)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerMelon(new Coordonnée(centreXCase17))));

            if (Niveau >= 9)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerGalboss(new Coordonnée(centreXCase17))));

            if (Niveau >= 11)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerCloche(new Coordonnée(centreXCase17))));

            if (Niveau >= 13)
                m_listeFruits.Enqueue(new FruitAnimable(FabriqueFruit.FabriquerCle(new Coordonnée(centreXCase17))));
        }
    }
}
