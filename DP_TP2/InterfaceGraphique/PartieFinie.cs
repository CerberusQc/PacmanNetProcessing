using DP_TP2.Logique;
using DP_TP2.ProgrammeDessinables;
using System;
using DP_TP2.Logique.ÉtatProgrammes;
using DP_TP2.ObjetDessinables;
using static NetProcessing.Sketch;
using static DP_TP2.Utilitaire.Constantes;
using DP_TP2.ObjetDessinables.UI;
using DP_TP2.Utilitaire;

namespace DP_TP2.InterfaceGraphique
{
    internal class PartieFinie : ProgrammeDessinable
    {
        public PartieFinie(Programme p_programme, bool p_victoire) : base(new ÉtatPartieFinie(p_programme))
        {
            m_victoire = p_victoire;
            Fond = Constantes.Fond;
            TextAlign(CENTER);
            String texteScore = "Votre score : " + Partie.Instance.Score.ToString("D3");
            Texte score = new Texte(new Coordonnée(CentreX, QuartY * 3 - 50), texteScore, Inky, 50);

            Texte gameOver = new Texte(new Coordonnée(CentreX, CentreY), p_victoire ? Victoire : GameOver, p_victoire ? MelonFond : Blinky, 100);

            String texteRecord = "Record : " + Partie.Instance.Record.ToString("D3");
            Texte record = new Texte(new Coordonnée(CentreX, QuartY), texteRecord, Clyde, 50);

            if (p_victoire)
            {
                Texte texteSuivant = new Texte(new Coordonnée(QuartX * 3, Hauteur - 40), "Suivant", MelonFond, 20);
                Bouton suivant = new Bouton(new Coordonnée(QuartX * 3, Hauteur - 100), new Dimension(75, 75),
                    MelonFond, MelonPédon, "⇢", 50, ÉtatProgramme.TypeActionBouton.DémarrerPartie);

                AjouterÉlément(texteSuivant);
                AjouterBouton(suivant);
            }

            Texte informationsTouche = new Texte(new Coordonnée(CentreX, 3 * QuartY + 160), p_victoire ? MessageToucheVictoire : MessageToucheDefaite, Blanc, 15);
            AjouterÉlément(informationsTouche);

            Texte texteRecommencer = new Texte(new Coordonnée(CentreX, Hauteur - 40), "Recommencer", PacMan, 20);
            Bouton recommencer = new Bouton(new Coordonnée(CentreX, Hauteur - 100), new Dimension(75, 75),
                PacMan, JauneFonce, "⇣", 50, ÉtatProgramme.TypeActionBouton.Information);

            Texte texteQuitter = new Texte(new Coordonnée(QuartX, Hauteur - 40), "Quitter", CeriseFond, 20);
            Bouton quitter = new Bouton(new Coordonnée(QuartX, Hauteur - 100), new Dimension(75, 75), CeriseFond,
                CerisePédon, "⇠", 50, ÉtatProgramme.TypeActionBouton.TerminéPartie);

            AjouterEnsembleÉléments(new ObjetDessinable[] { score, gameOver, record, texteRecommencer, texteQuitter });
            AjouterBouton(recommencer);
            AjouterBouton(quitter);
        }

        private readonly bool m_victoire;

        public override void CapterClavier(int p_codeTouche)
        {
            if (p_codeTouche == ESC)
                Actions.Précédent();

            if (p_codeTouche == RETURN && m_victoire)
                Actions.DémarrerPartie();

            if (p_codeTouche == KC_CONTROL)
                Actions.AfficherInformations();
        }
    }
}
