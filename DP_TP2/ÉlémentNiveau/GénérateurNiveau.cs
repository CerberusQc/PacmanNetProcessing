using DP_TP2.ObjetDessinables.ObjetJeuValeur;
using DP_TP2.Utilitaire;
using System;
using System.Collections.Generic;
using DP_TP2.ÉlémentNiveau.ÉlémentJeuDessinable;

namespace DP_TP2.ÉlémentNiveau
{
    /// <summary>
    /// Permet de generer un niveau sur un Array de String en 2D
    /// 'M' represente un Mur
    /// 'F' represente une porte fantome
    /// 'C' represente un corridor vide
    /// 'O' represente un Point (pellet) de base
    /// 'B' represente un Booster (qui change l'etat du fantome)
    /// </summary>
    internal static class GénérateurNiveau
    {
        private const char Mur = 'M';
        private const char PorteFantome = 'F';
        private const char Corridor = 'C';
        private const char Point = 'O';
        private const char Booster = 'B';

        public static void GénérerPartieClassique(this Case[][] p_grille)
        {
            // M = Mur, F= Porte Fantome, C = Corridor, O = Point, B = Booster
            string[] tableau = new string[30];
            tableau[00] = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
            tableau[01] = "MOOOOOOOOOOOOOMMOOOOOOOOOOOOOM";
            tableau[02] = "MOMMMMOMMMMMMOMMOMMMMMMOMMMMOM";
            tableau[03] = "MBMMMMOMMMMMMOMMOMMMMMMOMMMMBM";
            tableau[04] = "MOMMMMOMMMMMMOMMOMMMMMMOMMMMOM";
            tableau[05] = "MOOOOOOOOOOOOOOOOOOOOOOOOOOOOM";
            tableau[06] = "MOMMMMOMMOMMMMMMMMMMOMMOMMMMOM";
            tableau[07] = "MOMMMMOMMOMMMMMMMMMMOMMOMMMMOM";
            tableau[08] = "MOOOOOOMMOOOOOMMOOOOOMMOOOOOOM";
            tableau[09] = "MMMMMMOMMMMMMCMMCMMMMMMOMMMMMM";
            tableau[10] = "MCCCCMOMMMMMMCMMCMMMMMMOMCCCCM";
            tableau[11] = "MCCCCMOMMCCCCCCCCCCCCMMOMCCCCM";
            tableau[12] = "MCCCCMOMMCMMMMFFMMMMCMMOMCCCCM";
            tableau[13] = "MMMMMMOMMCMCCCCCCCCMCMMOMMMMMM";
            tableau[14] = "CCCCCCOCCCMCCCCCCCCMCCCOCCCCCC";
            tableau[15] = "MMMMMMOMMCMCCCCCCCCMCMMOMMMMMM";
            tableau[16] = "MCCCCMOMMCMMMMMMMMMMCMMOMCCCCM";
            tableau[17] = "MCCCCMOMMCCCCCCCCCCCCMMOMCCCCM";
            tableau[18] = "MCCCCMOMMCMMMMMMMMMMCMMOMCCCCM";
            tableau[19] = "MMMMMMOMMCMMMMMMMMMMCMMOMMMMMM";
            tableau[20] = "MOOOOOOOOOOOOOMMOOOOOOOOOOOOOM";
            tableau[21] = "MOMMMMOMMMMMMOMMOMMMMMMOMMMMOM";
            tableau[22] = "MOMMMMOMMMMMMOMMOMMMMMMOMMMMOM";
            tableau[23] = "MBOOMMOOOOOOOOOOOOOOOOOOMMOOBM";
            tableau[24] = "MMMOMMOMMOMMMMMMMMMMOMMOMMOMMM";
            tableau[25] = "MMMOMMOMMOMMMMMMMMMMOMMOMMOMMM";
            tableau[26] = "MOOOOOOMMOOOOOMMOOOOOMMOOOOOOM";
            tableau[27] = "MOMMMMMMMMMMMOMMOMMMMMMMMMMMOM";
            tableau[28] = "MOOOOOOOOOOOOOMMOOOOOOOOOOOOOM";
            tableau[29] = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";

            GénérerGrille(p_grille, tableau);

        }

        private static void GénérerGrille(IReadOnlyList<Case[]> p_grille, string[] tableau)
        {
            for (int ligne = 0; ligne < 30; ligne++)
            {
                for (int colonne = 0; colonne < 30; colonne++)
                {
                    p_grille[ligne][colonne] = CréerCase(tableau[colonne][ligne], p_grille[ligne][colonne].ObtenirCoordonnée());
                }
            }
        }

        private static Case CréerCase(char p_lettreTableau, Coordonnée p_coordonnée)
        {
            switch (p_lettreTableau)
            {
                case Mur: return CréerMur(p_coordonnée);
                case PorteFantome: return CréerPorteFantôme(p_coordonnée);
                case Corridor: return CréerCorridorVide(p_coordonnée);
                case Point: return CréerCorridorPoint(p_coordonnée);
                case Booster: return CréerCorridorBooster(p_coordonnée);
                default:         
                        throw new Exception("La lettre n'est pas valide pour le générateur de tableau");      
            }
        }

        private static Case CréerMur(Coordonnée p_coordonnée)
        {
            return new Mur(p_coordonnée);
        }

        private static Case CréerCorridorVide(Coordonnée p_coordonnée)
        {
            return new Corridor(p_coordonnée);
        }

        private static Case CréerCorridorPoint(Coordonnée p_coordonnée)
        {
            Corridor corridor = new Corridor(p_coordonnée);
            corridor.MettreAJourObjet(new Point(p_coordonnée));
            return corridor;
        }

        private static Case CréerCorridorBooster(Coordonnée p_coordonnée)
        {
            Corridor corridor = new Corridor(p_coordonnée);
            corridor.MettreAJourObjet(new Booster(p_coordonnée));
            return corridor;
        }

        private static Case CréerPorteFantôme(Coordonnée p_coordonnée)
        {
            Mur mur = new Mur(p_coordonnée) {EstPorteFantôme = true};
            return mur;
        }
    }
}
