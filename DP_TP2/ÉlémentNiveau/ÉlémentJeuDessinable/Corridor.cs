using DP_TP2.Logique;
using DP_TP2.ObjetDessinables.ObjetJeuValeur;
using DP_TP2.Utilitaire;
using NetProcessing;

namespace DP_TP2.ÉlémentNiveau.ÉlémentJeuDessinable
{
    /// <summary>
    /// Case de base vide, permettant d'avoir un ObjetJeuValeur ou non
    /// </summary>
    internal class Corridor : Case
    {
        public Corridor(Coordonnée p_coordonnée) : base(p_coordonnée)
        {
            EstTraversable = true;
        }

        private ObjetJeu Objet { get; set; }

        public override void Dessiner()
        {
            Sketch.Fill(Constantes.Fond);
            Sketch.RectMode(Sketch.CENTER);
            Coordonnée coordonnée = ObtenirCoordonnée();
            Sketch.Rect(coordonnée.X, coordonnée.Y, Taille, Taille);

            Objet?.Dessiner();
        }

        /// <summary>
        /// Permet de verifier si un objet est present sur la case, si oui, va mettre a jour l'instance de la partie et remet l'objet a null
        /// </summary>
        public void EnleverObjet()
        {
            if (Objet == null) return;

            Partie.Instance.MettreAJourScore(Objet.ValeurActuel);

            if(Objet.GetType() == typeof(Booster))
            {
                Partie.Instance.ModifierEtatPeurFantomes(true);
            }

            Objet = null;
        }

        public void MettreAJourObjet(ObjetJeu p_objet)
        {
            Objet = p_objet;
        }
    }
}
