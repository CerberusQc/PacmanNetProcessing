using DP_TP2.Utilitaire;
using static DP_TP2.Utilitaire.Constantes;

namespace DP_TP2.ObjetDessinables.ObjetJeuValeur
{
    /// <summary>
    /// Simple fabrique pour construire des fruits facilements
    /// </summary>
    internal class FabriqueFruit
    {
        public static Fruit FabriquerCerise(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Cerise, CeriseDescription, CeriseValeur,CeriseFond);
        }

        public static Fruit FabriquerFraise(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Fraise, FraiseDescription, FraiseValeur,FraiseFond);
        }

        public static Fruit FabriquerOrange(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Orange, OrangeDescription, OrangeValeur,OrangeFond);
        }

        public static Fruit FabriquerPomme(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Pomme, PommeDescription, PommeValeur,PommeFond);
        }

        public static Fruit FabriquerMelon(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Melon, MelonDescription, MelonValeur,MelonFond);
        }

        public static Fruit FabriquerGalboss(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Galboss, GalbossDescription, GalbossValeur,GalbossFond);
        }
        public static Fruit FabriquerCloche(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Cloche, ClocheDescription, ClocheValeur,ClocheFond);
        }
        public static Fruit FabriquerCle(Coordonnée p_coordonnée)
        {
            return new Fruit(p_coordonnée, Cle, CleDescription, CleValeur,CleFond);
        }
    }
}