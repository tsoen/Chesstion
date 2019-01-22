using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MRepas;
using ChessTion.Modele.MTournoi;
using ChessTion.Test;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;

namespace ChessTion.Controleur.Etats
{
    /// <summary>
    /// Classe gérant l'étape de création et configuration du tournoi.
    /// </summary>
    class Creation : Etat
    {


        /*************************************************************
         *    __    ____  ____  ____  ____  ____  __  __  ____  ___  *
         *   /__\  (_  _)(_  _)(  _ \(_  _)(  _ \(  )(  )(_  _)/ __) *
         *  /(__)\   )(    )(   )   / _)(_  ) _ < )(__)(   )(  \__ \ *
         * (__)(__) (__)  (__) (_)\_)(____)(____/(______) (__) (___/ *
         *                                                           *
         *      Ensemble des attributs utilisés dans la classe.      *
         *                                                           *
         *************************************************************/

        /// <summary>
        /// Numéro de l'état. 
        /// </summary>
        public override int Etape { get; protected set; } = 1;

        /// <summary>
        /// Nom de l'état.
        /// </summary>
        public override string Name { get; protected set; } = "Configuration du tournoi";

        /*************************************************************
         *    __    ____  ____  ____  ____  ____  __  __  ____  ___  *
         *   /__\  (_  _)(_  _)(  _ \(_  _)(  _ \(  )(  )(_  _)/ __) *
         *  /(__)\   )(    )(   )   / _)(_  ) _ < )(__)(   )(  \__ \ *
         * (__)(__) (__)  (__) (_)\_)(____)(____/(______) (__) (___/ *
         *                                                           *
         *      Ensemble des attributs utilisés dans la classe.      *
         *                                                           *
         *************************************************************/

        /// <summary>
        /// Charge l'interface pour coller à l'état.
        /// </summary>
        public override void LoadInterface()
        {
            CChesstion.EnableAll(true);
            CChesstion.JoueurPanel.ReadOnly = true;
            CChesstion.CentrePanel.Enabled = false;
            CChesstion.OpenPanel.ReadOnly = false;
            CChesstion.OpensPanel.AllowAdd = true;
            CChesstion.OpensPanel.AllowDelete = true;
            CChesstion.RepasPanel.AllowAdd = true;
            CChesstion.JoueurPanel.EnabledButtons(false);

            CChesstion.StatusPanel.Title = FullName;
            CChesstion.StatusPanel.Message =
                "Un nouveau tournoi a été créé, il faut maintenant le configurer. Pour cela :\n"
                + "- Renommez ou ajoutez des opens dans le panneau Opens à haut gauche ;\n"
                + "- Ajoutez des repas que les joueurs pourront réserver dans le panneau en bas à gauche ;\n"
                + "- Configurez les options de chaque Open et du Tournoi dans le panneau en bas à droite.\n\n"
                + "Une fois la configuration terminée, vous pouvez ouvrir les inscriptions en utilisant le bouton plus bas. Attention, une fois les inscriptions ouvertes, "
                + "toutes les informations mentionnées ne pourront plus êtres modifiées.";
            CChesstion.StatusPanel.Tip = "TIP : pour renommer un open, cliquez-droit sur son bouton !";
            CChesstion.ShowStatusPanel(true, true);
            CChesstion.ActionPanel.SetProgressed(-1, false);
            CChesstion.ActionPanel.EnableAll(false);
            CChesstion.ActionPanel.EnableOuvrirInscriptions = true;
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.LEFT_BUTTON);
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);

        }

        /// <summary>
        /// Méthode à appeler pour effectuer l'ensemble des actions qui mènent de l'état précédent à cet état.
        /// </summary>
        /// <param name="loadInterface">Vrai pour appeler <see cref="LoadInterface"/> à la fin de cette méthode.</param>
        public override void Transition(bool loadInterface = true)
        {
            CChesstion.EnableAll(false);

            CChesstion.JoueurPanel.Reset();
            CChesstion.CentrePanel.Reset();

            CChesstion.DeleteAll();
            CChesstion.MsMenu.EnableUpdateInscrits = false;
            CChesstion.OpensPanel.ReadOnly = false;


            GRepas.RecommencerProchaineRef();
            GOpen.RecommencerProchaineRef();

            CChesstion.CreerOpen("Nouvel Open", -1);

            if (loadInterface) LoadInterface();

        }
    }
}
