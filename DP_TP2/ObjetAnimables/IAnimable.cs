using DP_TP2.Utilitaire;

namespace DP_TP2.ObjetAnimables
{
    /// <summary>
    /// Contrat pour les objets qui auront de l'animation
    /// pour assurer une homogénéité dans le programme 
    /// </summary>
    internal interface IAnimable
    {
        void Animer(int p_cptFrame);

        void MettreAJourCoordonnée(Coordonnée p_coordonnée);

        void MettreAJourDimension(Dimension p_dimension);

        void MettreAJourVitesseAnimation(int p_vitesse);
    }
}
