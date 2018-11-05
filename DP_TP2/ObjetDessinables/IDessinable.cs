using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetDessinables
{
    /// <summary>
    /// Contrat pour tout les objets qui doivent etre dessiner 
    /// </summary>
    internal interface IDessinable
    {
        void Dessiner();

        void MettreAJourCoordonnée(Coordonnée p_coordonnée);

        void MettreAJourDimension(Dimension p_dimension);
    }
}
