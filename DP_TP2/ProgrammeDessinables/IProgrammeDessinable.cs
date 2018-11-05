using DP_TP2.Utilitaire;

namespace DP_TP2.ProgrammeDessinables
{
    /// <summary>
    /// Contrat pour tout les etat du programme pour Dessiner l'ensembles des elements
    /// </summary>
    internal interface IProgrammeDessinable
    {
        void DessinerTout(int p_cptFrame);

        void VerifierBoutonsSiParDessus(Coordonnée p_coordonnée);

        /// <summary>
        ///  Permet de capter les Clique de la souris pour exécuter les actions
        ///  nécessaire
        /// </summary>
        /// <param name="p_coordonnée">Les coordonnées de la souris relatif à l'écran NP</param>
        void Cliquer(Coordonnée p_coordonnée);

        /// <summary>
        /// Permet de définir les touches de clavier a capter 
        /// pour exécuter des actions
        /// </summary>
        /// <param name="p_codeTouche">Le code de la touche appuyé</param>
        void CapterClavier(int p_codeTouche);
    }
}
