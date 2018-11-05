using DP_TP2.ObjetAnimables;
using DP_TP2.Utilitaire;

namespace DP_TP2.Logique.ComportementFantômes
{
    internal interface IComportemental
    {
        ObjetAnimable.Déplacement AppliquerInstinct(Coordonnée p_actuel, Coordonnée p_destination, ObjetAnimable.Déplacement p_direction, bool p_estApeuré);
    }
}
