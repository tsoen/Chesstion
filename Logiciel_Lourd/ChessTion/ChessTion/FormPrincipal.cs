using ChessTion.Utilitaires;
using System;
using System.Linq;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.ImportExport;
using ChessTion.Modele.MLieu;
using ChessTion.Test;

// Icons : https://icons8.com/web-app/for/win10/

namespace ChessTion
{
    /// <summary>
    /// Classe vue principale qui gère la fenêtre Chesstion.
    /// </summary>
    public partial class FormPrincipal : Form
    {
#if DEBUG
        private GaetanTest gaetanTest;
        private TimotheeTest timTest;
#endif

        /*************************************************************************************
         *   ___  _____  _  _  ___  ____  ____  __  __   ___  ____  ____  __  __  ____  ___  *
         *  / __)(  _  )( \( )/ __)(_  _)(  _ \(  )(  ) / __)(_  _)( ___)(  )(  )(  _ \/ __) *
         * ( (__  )(_)(  )  ( \__ \  )(   )   / )(__)( ( (__   )(   )__)  )(__)(  )   /\__ \ *
         *  \___)(_____)(_)\_)(___/ (__) (_)\_)(______) \___) (__) (____)(______)(_)\_)(___/ *
         *                                                                                   *
         *                      Ensemble des constructeurs de la classe.                     *
         *                                                                                   *
         *************************************************************************************/

        /// <summary>
        /// Constructeur.
        /// </summary>
        public FormPrincipal()
        {
            DPI.CreateInstance(this);
            Theme.LoadStyle();
            InitializeComponent();

            CChesstion.JoueurPanel = panelJoueur;
            CChesstion.OpenPanel = panelOpen;
            CChesstion.OpensPanel = panelOpens;
            CChesstion.MsMenu = msMenu;
            CChesstion.CentrePanel = panelCentre;
            CChesstion.RepasPanel = panelRepas;
            CChesstion.ActionPanel = panelAction;
            CChesstion.StatusPanel = statusPanel;

            Resize += FormPrincipal_Resize;
            FormClosing += FormPrincipal_FormClosing;
        }

        /*****************************************************************
         *  ____  _  _  ____  _  _  ____  __  __  ____  _  _  ____  ___  *
         * ( ___)( \/ )( ___)( \( )( ___)(  \/  )( ___)( \( )(_  _)/ __) *
         *  )__)  \  /  )__)  )  (  )__)  )    (  )__)  )  (   )(  \__ \ *
         * (____)  \/  (____)(_)\_)(____)(_/\/\_)(____)(_)\_) (__) (___/ *
         *                                                               *
         *        Ensemble des évènements gérés par la classe.           *
         *                                                               *
         *****************************************************************/

        /// <summary>
        /// Redimensionn tous les panneaux.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPrincipal_Resize(object sender, EventArgs e)
        {
            CChesstion.ResizePanels();
        }

        /// <summary>
        /// Applique le thème et initialise les panneaux.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formPrincipal_Load(object sender, EventArgs e)
        {
            this.BackColor = Theme.Style.GeneralBackColor;

            CChesstion.Init();
        }

        /// <summary>
        /// Demande confirmation pour sauvegarder le tournoi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CChesstion.TournoiSelectionne != null &&
                CChesstion.TournoiSelectionne.Etat == GTournoi.ETAT__TOURNOI_TERMINE)
                return;

            DialogResult r = MessageBox.Show("Voulez-vous enregistrer le tournoi avant de quitter ?", "Confirmation",
                MessageBoxButtons.YesNoCancel);

            if (r == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (r == DialogResult.No)
                return;

            Save.PerformSave();
        }

    }
}
