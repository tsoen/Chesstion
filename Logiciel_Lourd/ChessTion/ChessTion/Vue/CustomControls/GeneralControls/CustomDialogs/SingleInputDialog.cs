using System;
using System.Windows.Forms;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up demandant de rentrer une valeur.
    /// </summary>
    public partial class SingleInputDialog : Form
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
        /// Texte entrée dans la pop-up.
        /// </summary>
        private string input = string.Empty;










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
        /// <param name="title">Titre de la pop-up.</param>
        /// <param name="message">Message de la pop-up.</param>
        public SingleInputDialog(string title = "Input Dialog", string message = "Please input value")
        {
            InitializeComponent();
            Title = title;
            Message = message;
            txtInput.KeyPress += TxtInput_KeyPress;
            txtInput.Validated += TxtInput_Validated;
            StartPosition = FormStartPosition.CenterParent;
        }










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
        /// Texte entrée dans la pop-up.
        /// </summary>
        public string Input
        {
            get { return input; }
            set
            {
                input = value;
                if (input != Text)
                    Text = input;
            }
        }

        /// <summary>
        /// Titre de la pop-up.
        /// </summary>
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        /// <summary>
        /// Message de la pop-up.
        /// </summary>
        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        /// <summary>
        /// Label du message.
        /// </summary>
        protected Label Label { get { return lblMessage; } }

        /// <summary>
        /// Textbox de l'input.
        /// </summary>
        protected ControlledTextBox TextBox { get { return txtInput; } }

        /// <summary>
        /// Bouton OK.
        /// </summary>
        protected Button OKButton { get { return btnValider; } }

        /// <summary>
        /// Bouton Annuler.
        /// </summary>
        protected new Button CancelButton { get { return btnAnnuler; } }










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
        /// Ferme la pop-up et récupère la valeur entrée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValider_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.LastValue))
                return;
            DialogResult = DialogResult.OK;
            Input = txtInput.LastValue;
            if (Input == txtInput.PlaceholderText)
                Input = "Nouvel Open";
            this.Close();
        }

        /// <summary>
        /// Ferme la pop-up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Si la touche est entrée, focus le bouton valider.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                btnValider.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (txtInput.Focused)
                btnAnnuler.Focus();
        }

        /// <summary>
        /// Si le texte est ok, sauvegarde le texte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtInput_Validated(object sender, EventArgs e)
        {
            txtInput.LastValue = txtInput.Text;
        }
    }
}
