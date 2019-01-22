using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.CLieu;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant la pop-up servant à ajouter une ville.
    /// </summary>
    class AjouterVilleDialog : Form
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
        /// Bouton d'annulation.
        /// </summary>
        private Button BtnCancel;

        /// <summary>
        /// Bouton de validation.
        /// </summary>
        private Button BtnOk;

        /// <summary>
        /// Label titre.
        /// </summary>
        private Label LblTitle;

        /// <summary>
        /// Textbox du nom de la ville.
        /// </summary>
        private ControlledTextBox TxtNom;

        /// <summary>
        /// Textbox du code postal.
        /// </summary>
        private ControlledTextBox TxtCodePostal;










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
        public AjouterVilleDialog()
        {
            InitializeComponent();


            BtnCancel.Click += (object sender, EventArgs e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            BtnOk.Click += BtnOk_Click;
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
        /// Crée les controls.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblTitle = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.TxtNom = new ControlledTextBox();
            this.TxtCodePostal = new ControlledTextBox();
            this.SuspendLayout();
            // 
            // AjouterVilleDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(514, 131);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.TxtNom);
            this.Controls.Add(this.TxtCodePostal);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterVilleDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = FormStartPosition.CenterParent;

            //
            // TxtNom 
            //
            this.TxtNom.AllowControls = true;
            this.TxtNom.AllowDecimal = false;
            this.TxtNom.AllowedCharacters = new List<char>() { '-', '\'' };
            this.TxtNom.AllowEmpty = false;
            this.TxtNom.AllowEuro = false;
            this.TxtNom.AllowLetters = true;
            this.TxtNom.AllowNumbers = true;
            this.TxtNom.AllowSpace = true;
            this.TxtNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNom.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.LastValue = null;
            this.TxtNom.Location = new System.Drawing.Point(12, 43);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNom.PlaceholderText = "Strasbourg";
            this.TxtNom.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNom.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNom.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtNom.Size = new System.Drawing.Size(2 * (this.ClientSize.Width - 24 - 12) / 3, 27);
            this.TxtNom.TabIndex = 1;
            this.TxtNom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // TxtCodePostal 
            //
            this.TxtCodePostal.AllowControls = true;
            this.TxtCodePostal.AllowDecimal = false;
            this.TxtCodePostal.AllowedCharacters = new List<char>() { '-', '\'' };
            this.TxtCodePostal.AllowEmpty = false;
            this.TxtCodePostal.AllowEuro = false;
            this.TxtCodePostal.AllowLetters = true;
            this.TxtCodePostal.AllowNumbers = true;
            this.TxtCodePostal.AllowSpace = true;
            this.TxtCodePostal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtCodePostal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodePostal.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtCodePostal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtCodePostal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtCodePostal.LastValue = null;
            this.TxtCodePostal.Location = new System.Drawing.Point(TxtNom.Right + 12, 43);
            this.TxtCodePostal.Name = "TxtCodePostal";
            this.TxtCodePostal.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtCodePostal.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtCodePostal.PlaceholderText = "67100";
            this.TxtCodePostal.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtCodePostal.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtCodePostal.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtCodePostal.Size = new System.Drawing.Size((this.ClientSize.Width - 24 - 12) / 3, 27);
            this.TxtCodePostal.TabIndex = 2;
            this.TxtCodePostal.MaxLength = 5;
            this.TxtCodePostal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitle.Location = new System.Drawing.Point(12, 10);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(490, 22);
            this.LblTitle.TabIndex = 1;
            this.LblTitle.Text = "Ajouter une ville";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.Location = new System.Drawing.Point(278, 74);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 45);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Annuler";
            this.BtnCancel.UseVisualStyleBackColor = false;
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnOk.Location = new System.Drawing.Point(393, 74);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(109, 45);
            this.BtnOk.TabIndex = 10;
            this.BtnOk.Text = "Valider";
            this.BtnOk.UseVisualStyleBackColor = false;

            this.ResumeLayout(false);

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
        /// Crée la ville quand on appuie sur le bouton OK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!TxtNom.Regex.IsMatch(TxtNom.LastValue ?? "")
                || !TxtCodePostal.Regex.IsMatch(TxtCodePostal.LastValue ?? ""))
            {
                CustomQuickDialog d = new CustomQuickDialog("Les valeurs entrées ne sont pas correctes.",
                    GeneralControls.CustomDialogs.QuickDialogType.Error, BtnOk,
                    GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                d.DisplayDelay = 3000;
                d.Show();
                return;
            }
            GLieu.CreerVille(TxtNom.LastValue, TxtCodePostal.LastValue);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Applique le thème aux controls.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Theme.Style.DialogBodyBackColor;
            this.ForeColor = Theme.Style.DialogBodyForeColor;
            this.Font = Theme.Style.DialogBodyFont;

            this.TxtNom.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNom.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNom.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNom.Font = Theme.Style.DialogTextBoxFont;
            this.TxtNom.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtCodePostal.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtCodePostal.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtCodePostal.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtCodePostal.Font = Theme.Style.DialogTextBoxFont;
            this.TxtCodePostal.NormalFont = Theme.Style.DialogTextBoxFont;

            this.LblTitle.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblTitle.Font = Theme.Style.DialogBodyFont;

            this.BtnOk.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.BtnOk.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.BtnOk.Font = Theme.Style.DialogMainButtonsFont;

            this.BtnCancel.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.BtnCancel.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.BtnCancel.Font = Theme.Style.DialogSecondaryButtonsFont;

            TxtNom.CheckPlaceHolder(false);
            TxtCodePostal.CheckPlaceHolder(false);
        }
    }
}
