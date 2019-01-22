using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CRepas;
using ChessTion.Modele.MRepas;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    /// <summary>
    /// Classe vue gérant le panneau de <see cref="Repas"/>.
    /// </summary>
    class RepasPanel : AddDeletePanel, IChesstionPanel
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

        private static readonly int spaceBetweenRepas = 24;
        private static readonly int panelStartingX = 10;
        private static readonly int panelEndingY = 40;
        private static int panelStartingY;
        private int lastSelected = int.MinValue;
        private bool readOnly = false;
        public Panel Panel { get; } = new Panel();










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
        public RepasPanel() { }










        /************************************************************
         *   ___  ____  ____    ____  ____    ___  ____  ____  ___  *
         *  / __)( ___)(_  _)  ( ___)(_  _)  / __)( ___)(_  _)/ __) *
         * ( (_-. )__)   )(     )__)   )(    \__ \ )__)   )(  \__ \ *
         *  \___/(____) (__)   (____) (__)   (___/(____) (__) (___/ *
         *                                                          *
         *       Ensemble des getters et setters de la classe.      *
         *                                                          *
         ************************************************************/

        /// <summary>
        /// Retourne la liste des text box repas.
        /// </summary>
        public List<RepasTextBox> Boxes
        {
            get
            {
                List<RepasTextBox> buttons = new List<RepasTextBox>();

                foreach (Control c in Panel.Controls)
                    if (c is RepasTextBox)
                        buttons.Add((RepasTextBox)c);

                return buttons;
            }
        }

        /// <summary>
        /// Vrai si le panneau n'est pas modifiable.
        /// </summary>
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                foreach (RepasTextBox rtb in Boxes)
                    rtb.ReadOnly = value;

                readOnly = value;
            }
        }









        /********************************************************
         *  __  __  ____  ____  _   _  _____  ____   ____  ___  *
         * (  \/  )( ___)(_  _)( )_( )(  _  )(  _ \ ( ___)/ __) *
         *  )    (  )__)   )(   ) _ (  )(_)(  )(_) ) )__) \__ \ *
         * (_/\/\_)(____) (__) (_) (_)(_____)(____/ (____)(___/ *
         *                                                      *
         *      Ensemble des méthodes autres de la classe.      *
         *                                                      *
         ********************************************************/

        /// <summary>
        /// Initialise le control.
        /// </summary>
        public void Init()
        {
            CreatePanel();

            panelStartingY = HeaderHeight + 10;

            Panel.Location = DPI.Instance.MultipliedPoint(panelStartingX, panelStartingY);
            Panel.Size = new System.Drawing.Size(Size.Width - panelStartingX*2,
                this.Size.Height - Panel.Location.Y - panelEndingY);
            Panel.BackColor = System.Drawing.Color.Transparent;

            Controls.Add(Panel);

            AllowAdd = true;
            AllowDelete = false;

            MouseWheel += RepasPanel_MouseWheel;
            DeleteButtonClicked += RepasPanel_DeleteButtonClicked;
            AddButtonClicked += RepasPanel_AddButtonClicked;
        }

        /// <summary>
        /// Crée le panneau.
        /// </summary>
        private void CreatePanel()
        {
            BackColor = Theme.Style.RepasBodyBackColor;
            HeaderBackColor = Theme.Style.RepasHeaderBackColor;
            HeaderFont = Theme.Style.RepasHeaderFont;
            HeaderForeColor = Theme.Style.RepasHeaderForeColor;
            HeaderHeight = (int)(DPI.Instance.RelativeMultiplier.Y * Theme.Style.RepasHeaderHeight);

            //Location = DPI.Instance.MultipliedPoint(0, 382);
            //Size = DPI.Instance.MultipliedSize(200, 341);
            Title = "Repas";

            RelocateAndResize();
        }

        /// <summary>
        /// Redimensionne et repositionne le panneau en fonction de la taille de la fenêtre.
        /// </summary>
        public void RelocateAndResize()
        {
            Location = new Point(0, CChesstion.OpensPanel.Bottom + 2);
            Size = new Size(CChesstion.OpensPanel.Size.Width, (Parent.ClientSize.Height - (CChesstion.MsMenu.Height + 2)) / 2);
            Panel.Location = DPI.Instance.MultipliedPoint(panelStartingX, panelStartingY);

            RelocateButtons();
        }



        /// <summary>
        /// Ajoute un repas.
        /// </summary>
        /// <param name="reference">Référence du nouveau repas.</param>
        public void AddRepas(int reference)
        {
            RepasTextBox txt = new RepasTextBox();
            txt.CheckPlaceHolder(true);
            txt.LastValue = GRepas.GetRepas(reference).NomEtPrix; txt.Text = GRepas.GetRepas(reference).NomEtPrix;
            txt.Size = new Size(Panel.Size.Width, txt.Size.Height);
            txt.Ref = reference;
            txt.Entered += Txt_Entered;

            if (Boxes.Count > 0)
                txt.Location = new Point(0, Boxes.Last().Location.Y + spaceBetweenRepas);
            else
                txt.Location = new Point(0, 0);

            Panel.Controls.Add(txt);
            txt.CheckPlaceHolder(false);

            if (Panel.Size.Height < Boxes.Count * spaceBetweenRepas)
                Panel.Size = new Size(Panel.Size.Width, Panel.Size.Height + spaceBetweenRepas);

        }

        /// <summary>
        /// Supprime un repas.
        /// </summary>
        /// <param name="reference">Référence du repas à supprimer.</param>
        public void DeleteRepas(int reference)
        {
            Panel.Size = new Size(Panel.Width, Panel.Height - spaceBetweenRepas);

            foreach (RepasTextBox txt in Boxes)
                if (txt.Ref == reference)
                {
                    Panel.Controls.Remove(txt);

                    foreach (RepasTextBox txtt in Boxes)
                        if (txtt.Ref > reference)
                            txtt.Location = new Point(txtt.Location.X, txtt.Location.Y - spaceBetweenRepas);

                    return;
                }
        }



        /// <summary>
        /// Fait défiler le panneau verticalement.
        /// </summary>
        /// <param name="amount">Quantité de défilement.</param>
        public new void Scroll(int amount)
        {
            if (amount == 0)
                return;

            if (amount > 0) // Move panel down
            {
                if (Panel.Location.Y < panelStartingY)
                {
                    if (Panel.Location.Y + amount > panelStartingY)
                        Panel.Location = new Point(Panel.Location.X, panelStartingY);
                    else
                        Panel.Location = new Point(Panel.Location.X, Panel.Location.Y + amount);
                }
            }
            else // Move panel up
            {
                if (Panel.Location.Y >= panelStartingY || Panel.Bottom > Size.Height - panelEndingY)
                {
                    if (Panel.Bottom + amount < Size.Height - panelEndingY)
                        Panel.Location = new Point(Panel.Location.X, Size.Height - panelEndingY - Panel.Size.Height);
                    else
                        Panel.Location = new Point(Panel.Location.X, Panel.Location.Y + amount);
                }
                
            }
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
        /// Permet la supression du repas sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Entered(object sender, EventArgs e)
        {
            if (readOnly)
                return;

            lastSelected = ((RepasTextBox)sender).Ref;
            AllowDelete = true;
        }

        /// <summary>
        /// Effectue les actions de scroll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepasPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            Scroll(e.Delta / 2);
        }

        /// <summary>
        /// Ajoute un repas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepasPanel_AddButtonClicked(object sender, EventArgs e)
        {
            int reference = CChesstion.CreerRepas("Nouveau repas", 0);
            foreach (RepasTextBox rtb in Boxes)
                if (rtb.Ref == reference)
                    rtb.Focus();
        }

        /// <summary>
        /// Supprime un repas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepasPanel_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (lastSelected == int.MinValue || readOnly)
                return;

            if (GRepas.GetRepas(lastSelected) == null)
            {
                CustomQuickDialog q =
                    new CustomQuickDialog("Sélectionnez le repas à supprimer\npuis cliquez sur ce bouton.",
                        GeneralControls.CustomDialogs.QuickDialogType.Info, DeleteButton,
                        GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                q.DisplayDelay = 3000;
                q.Show();
                return;
            }

            try
            {
                CChesstion.SupprimerRepas(GRepas.GetRepas(lastSelected).Ref);
            }
            catch (ArgumentException ae)
            {
                CustomQuickDialog cqd = new CustomQuickDialog(
                    "Suppression impossible !\n" + ae.Message,
                    GeneralControls.CustomDialogs.QuickDialogType.Error,
                    DeleteButton,
                    new Point(0, -panelEndingY),
                    GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);

                cqd.DisplayDelay = 4000;
                cqd.Show();
            }
        }




    }
}
