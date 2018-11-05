using System;
using static NetProcessing.Sketch;

namespace DP_TP2.Utilitaire
{
    public static class Constantes
    {
        // Mesure de base
        public const int Largeur = 700;
        public const int Hauteur = Largeur;
        public const int TailleGrilleJeu = Largeur - 100;
        public const int TailleCase = TailleGrilleJeu / 30;
        public const int CentreX = Largeur / 2;
        public const int CentreY = Hauteur / 2;
        public const int QuartX = Largeur / 4;
        public const int QuartY = Hauteur / 4;

        public const string GameOver = "GAME OVER";
        public const string Victoire = "VICTOIRE";

        public const int VitesseAnimation = 5;

        public const int PointagePremierFantôme = 200;

        public const int ConditionVie = 10000; // Points a obtenir pour une vie supplementaire
        public const int ConditionVictoire = 254; //254 de base

        public const int VitessePacman = 2; // Doit etre un chiffre qui est disponible par dizaine (1,2,5,10,etc)
        public const int VitesseFantôme = 2;

        // Couleur de Base
        public static Color Noir = new Color("#000000");
        public static Color Gris = new Color("#696969");
        public static Color GrisPale = new Color("#CCCCCC");
        public static Color Blanc = new Color("#FFFFFF");
        public static Color Jaune = new Color("#FFEE00");
        public static Color JaunePale = new Color("#FFF466");
        public static Color JauneFonce = new Color("#E5D600");

        // État Jeu
        public static Color Prêt = new Color("#FFFF00");
        public static Color PartieFinie = new Color("#FE0000");
        public static Color Lettres = new Color("#DEDEDE");

        // Niveau
        public static Color Mur = new Color("#2121DE");
        public static Color PorteFantôme = new Color("#FFB8DE");
        public static Color Fond = new Color("#000000");
        public static Color Point = new Color("#FFB897");
        public static int ValeurPoint = 10;
        public static int ValeurBooster = 50;

        // Pac-Man
        public static Color PacMan = new Color("#FFFF00");

        // FantômeS
        public static Color FantômePeur = new Color("#0000FE");
        public static Color FantômePeurYeux = new Color("#FFB897");
        public static Color FantômeYeux = new Color("#DEDEDE");
        public static Color FantômePupille = new Color("#2121DE");

        public static Color Blinky = new Color("#F20201"); // Rouge
        public static String DescriptionBlinky = "Blinky\n\nIl vous suit sans\n rêlache !";

        public static Color Clyde = new Color("#F8B648");  // Orange
        public static String DescriptionClyde = "Clyde\n\nIl va où le\nvent le mène !";

        public static Color Pinky = new Color("#F8BAD1");  // Rose
        public static String DescriptionPinky = "Pinky\n\nIl devine votre\n destination !";

        public static Color Inky = new Color("#00FDDB");   // Bleu
        public static String DescriptionInky = "Inky\n\nIl vérifie souvent ses\nprécieux Boosters !";

        public static Color Blob = new Color("#FFB8DE");

        // Fruits
        public static PImage Cle = LoadImage("../../Ressources/cle.png");
        public static String CleDescription = "Clé";
        public static int CleValeur = 5000;
        public static Color CleTop = new Color("#0098F8");
        public static Color CleFond = new Color("#FFFFFF");
        public static Color CleOmbre = new Color("#080808");

        public static PImage Melon = LoadImage("../../Ressources/melon.png");
        public static String MelonDescription = "Melon";
        public static int MelonValeur = 1000;
        public static Color MelonFond = new Color("#00e100");
        public static Color MelonLigne = new Color("#4f3100");
        public static Color MelonPédon = new Color("#00B400");

        public static PImage Cloche = LoadImage("../../Ressources/cloche.png");
        public static String ClocheDescription = "Cloche";
        public static int ClocheValeur = 3000;
        public static Color ClocheFond = new Color("#FFFF00");
        public static Color ClocheOmbrage = new Color("#080808");
        public static Color ClocheIntérieur = new Color("#0098F8");
        public static Color ClocheLuette = new Color("#FFFFFF");

        public static PImage Orange = LoadImage("../../Ressources/orange.png");
        public static String OrangeDescription = "Orange";
        public static int OrangeValeur = 500;
        public static Color OrangeFond = new Color("#FFB521");
        public static Color OrangePédon = new Color("#FEA955");
        public static Color OrangeFeuille = new Color("#00FF00");

        public static PImage Pomme = LoadImage("../../Ressources/pomme.png");
        public static String PommeDescription = "Pomme";
        public static int PommeValeur = 700;
        public static Color PommeFond = new Color("#DE0000");
        public static Color PommePédon = new Color("#FEA955");
        public static Color PommeFeuille = new Color("#00FF00");

        public static PImage Fraise = LoadImage("../../Ressources/fraise.png");
        public static String FraiseDescription = "Fraise";
        public static int FraiseValeur = 300;
        public static Color FraiseFond = new Color("#FE0000");
        public static Color FraisePépins = new Color("#FFF5F6");
        public static Color FraiseFeuille = new Color("#089E09");

        public static PImage Cerise = LoadImage("../../Ressources/cerise.png");
        public static String CeriseDescription = "Cerise";
        public static int CeriseValeur = 100;
        public static Color CeriseFond = new Color("#FE0000");
        public static Color CeriseLustre = new Color("#FFEDED");
        public static Color CerisePédon = new Color("#B10000");

        public static PImage Galboss = LoadImage("../../Ressources/galboss.png");
        public static String GalbossDescription = "Galboss";
        public static int GalbossValeur = 2000;
        public static Color GalbossTop = new Color("#FE0000");
        public static Color GalbossFond = new Color("#0204E5");
        public static Color GalbossAile = new Color("#1868FF");

        public static Color[] CouleurRandom = { Fond, CleFond, ClocheFond, OrangeFond, PommeFond, GalbossFond, MelonFond, CeriseFond, Pinky, Inky, Blinky, Clyde, PacMan };

        // Texte dans le Jeu

        public static string MessageToucheInfo = "Vous pouvez appuyer sur Enter pour démarrer ou ESC pour revenir";
        public static string MessageToucheVictoire = "Vous pouvez appuyer sur Enter pour démarrer, CTRL pour recommencer ou ESC pour revenir";
        public static string MessageToucheDefaite = "Vous pouvez appuyer sur CTRL pour recommencer ou ESC pour revenir";
        public static string MessageInfoTableau = "Voici le tableau indiquant\nles différents fruits et leurs valeurs";

        public static string MessagePrecedent = "< Précédent";
        public static string MessageDemarrer = "Démarrer >";
        
        public static string MessageErreurEtatIllegal = "L'état actuel n'est pas supposé être appeler, état Illégal ! La police a été contacter !";
   }
}
