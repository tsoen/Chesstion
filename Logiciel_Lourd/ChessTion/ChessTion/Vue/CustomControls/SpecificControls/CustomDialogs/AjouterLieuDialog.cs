using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CLieu;
using ChessTion.Modele.MLieu;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up d'ajout de lieu.
    /// </summary>
    class AjouterLieuDialog : Form
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
        /// Label de titre.
        /// </summary>
        private Label lblTitre;

        /// <summary>
        /// Label du nom du lieu.
        /// </summary>
        private Label LblNom;

        /// <summary>
        /// Label du numéro du lieu.
        /// </summary>
        private Label LblNumero;

        /// <summary>
        /// Label du nom de rue du lieu.
        /// </summary>
        private Label LblRue;

        /// <summary>
        /// Label de ville du lieu.
        /// </summary>
        private Label LblVille;



        /// <summary>
        /// Textbox du nom du lieu.
        /// </summary>
        private GeneralControls.CustomTextBoxes.ControlledTextBox TxtNom;

        /// <summary>
        /// Textbox du numéro du lieu.
        /// </summary>
        private GeneralControls.CustomTextBoxes.ControlledTextBox TxtNumero;

        /// <summary>
        /// Textbox de la rue du lieu.
        /// </summary>
        private GeneralControls.CustomTextBoxes.ControlledTextBox TxtRue;



        /// <summary>
        /// Bouton d'ajout d'une ville.
        /// </summary>
        private GeneralControls.CustomButtons.AddButton AddButton;

        /// <summary>
        /// Bouton d'annulation.
        /// </summary>
        private Button BtnCancel;

        /// <summary>
        /// Bouton de validation.
        /// </summary>
        private Button BtnOk;

        /// <summary>
        /// Combo box de sélection de ville pour le lieu.
        /// </summary>
        private ComboBox ComboVille;










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
        public AjouterLieuDialog()
        {
            InitializeComponent();

            ComboVille.DataSource = GLieu.ListerVilles();
            ComboVille.DisplayMember = "NomComplet";
            ComboVille.ValueMember = "Ref";



            BtnCancel.Click += (object sender, EventArgs e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            BtnOk.Click += BtnOk_Click;
            AddButton.Click += AddButton_Click;

            TxtNom.KeyPress += Txt_KeyPress;
            TxtNumero.KeyPress += Txt_KeyPress;
            TxtRue.KeyPress += Txt_KeyPress;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjouterLieuDialog));
            this.lblTitre = new System.Windows.Forms.Label();
            this.LblNom = new System.Windows.Forms.Label();
            this.LblNumero = new System.Windows.Forms.Label();
            this.LblRue = new System.Windows.Forms.Label();
            this.LblVille = new System.Windows.Forms.Label();
            this.ComboVille = new System.Windows.Forms.ComboBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.AddButton = new ChessTion.Vue.CustomControls.GeneralControls.CustomButtons.AddButton();
            this.TxtRue = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtNumero = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtNom = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(12, 10);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(332, 22);
            this.lblTitre.TabIndex = 2;
            this.lblTitre.Text = "Ajouter un lieu";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblNom
            // 
            this.LblNom.AutoSize = true;
            this.LblNom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNom.Location = new System.Drawing.Point(12, 50);
            this.LblNom.Name = "LblNom";
            this.LblNom.Size = new System.Drawing.Size(44, 20);
            this.LblNom.TabIndex = 3;
            this.LblNom.Text = "Nom";
            this.LblNom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblNumero
            // 
            this.LblNumero.AutoSize = true;
            this.LblNumero.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNumero.Location = new System.Drawing.Point(12, 83);
            this.LblNumero.Name = "LblNumero";
            this.LblNumero.Size = new System.Drawing.Size(67, 20);
            this.LblNumero.TabIndex = 4;
            this.LblNumero.Text = "Numéro";
            this.LblNumero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblRue
            // 
            this.LblRue.AutoSize = true;
            this.LblRue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblRue.Location = new System.Drawing.Point(12, 116);
            this.LblRue.Name = "LblRue";
            this.LblRue.Size = new System.Drawing.Size(36, 20);
            this.LblRue.TabIndex = 5;
            this.LblRue.Text = "Rue";
            this.LblRue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblVille
            // 
            this.LblVille.AutoSize = true;
            this.LblVille.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblVille.Location = new System.Drawing.Point(12, 149);
            this.LblVille.Name = "LblVille";
            this.LblVille.Size = new System.Drawing.Size(39, 20);
            this.LblVille.TabIndex = 6;
            this.LblVille.Text = "Ville";
            this.LblVille.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboVille
            // 
            this.ComboVille.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ComboVille.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboVille.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboVille.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.ComboVille.FormattingEnabled = true;
            this.ComboVille.Location = new System.Drawing.Point(93, 149);
            this.ComboVille.Name = "ComboVille";
            this.ComboVille.Size = new System.Drawing.Size(215, 28);
            this.ComboVille.TabIndex = 10;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.Location = new System.Drawing.Point(93, 210);
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
            this.BtnOk.Location = new System.Drawing.Point(235, 210);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(109, 45);
            this.BtnOk.TabIndex = 10;
            this.BtnOk.Text = "Valider";
            this.BtnOk.UseVisualStyleBackColor = false;
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.Transparent;
            this.AddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddButton.FlatAppearance.BorderSize = 0;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
            this.AddButton.Location = new System.Drawing.Point(314, 147);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(30, 30);
            this.AddButton.TabIndex = 100;
            this.AddButton.UseVisualStyleBackColor = false;
            // 
            // TxtRue
            // 
            this.TxtRue.AllowControls = true;
            this.TxtRue.AllowDecimal = false;
            this.TxtRue.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtRue.AllowedCharacters")));
            this.TxtRue.AllowEmpty = false;
            this.TxtRue.AllowEuro = false;
            this.TxtRue.AllowLetters = true;
            this.TxtRue.AllowNumbers = true;
            this.TxtRue.AllowSpace = true;
            this.TxtRue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtRue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRue.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtRue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtRue.LastValue = null;
            this.TxtRue.Location = new System.Drawing.Point(93, 114);
            this.TxtRue.Name = "TxtRue";
            this.TxtRue.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtRue.NormalFont = null;
            this.TxtRue.PlaceholderText = "Rue de la Victoire";
            this.TxtRue.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtRue.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtRue.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtRue.Size = new System.Drawing.Size(251, 27);
            this.TxtRue.TabIndex = 3;
            // 
            // TxtNumero
            // 
            this.TxtNumero.AllowControls = true;
            this.TxtNumero.AllowDecimal = false;
            this.TxtNumero.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtNumero.AllowedCharacters")));
            this.TxtNumero.AllowEmpty = false;
            this.TxtNumero.AllowEuro = false;
            this.TxtNumero.AllowLetters = true;
            this.TxtNumero.AllowNumbers = true;
            this.TxtNumero.AllowSpace = false;
            this.TxtNumero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNumero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNumero.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNumero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNumero.LastValue = null;
            this.TxtNumero.Location = new System.Drawing.Point(93, 81);
            this.TxtNumero.Name = "TxtNumero";
            this.TxtNumero.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNumero.NormalFont = null;
            this.TxtNumero.PlaceholderText = "10A";
            this.TxtNumero.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNumero.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNumero.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtNumero.Size = new System.Drawing.Size(251, 27);
            this.TxtNumero.TabIndex = 2;
            // 
            // TxtNom
            // 
            this.TxtNom.AllowControls = true;
            this.TxtNom.AllowDecimal = false;
            this.TxtNom.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtNom.AllowedCharacters")));
            this.TxtNom.AllowEmpty = false;
            this.TxtNom.AllowEuro = false;
            this.TxtNom.AllowLetters = true;
            this.TxtNom.AllowNumbers = true;
            this.TxtNom.AllowSpace = true;
            this.TxtNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNom.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.LastValue = null;
            this.TxtNom.Location = new System.Drawing.Point(93, 47);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.NormalFont = null;
            this.TxtNom.PlaceholderText = "Complexe de Jeux";
            this.TxtNom.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNom.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNom.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtNom.Size = new System.Drawing.Size(251, 27);
            this.TxtNom.TabIndex = 1;
            // 
            // AjouterLieuDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(356, 267);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ComboVille);
            this.Controls.Add(this.TxtRue);
            this.Controls.Add(this.TxtNumero);
            this.Controls.Add(this.TxtNom);
            this.Controls.Add(this.LblVille);
            this.Controls.Add(this.LblRue);
            this.Controls.Add(this.LblNumero);
            this.Controls.Add(this.LblNom);
            this.Controls.Add(this.lblTitre);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjouterLieuDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter un lieu";
            this.ResumeLayout(false);
            this.PerformLayout();

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
        /// Si l'utilisateur appuie sur Entrée, va au prochain champ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        /// <summary>
        /// Ouvre la pop-up d'ajout de lieu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            AjouterVilleDialog a = new AjouterVilleDialog();
            if (a.ShowDialog() == DialogResult.OK)
            {
                ComboVille.DataSource = null;
                ComboVille.DataSource = GLieu.ListerVilles();
                ComboVille.DisplayMember = "NomComplet";
                ComboVille.ValueMember = "Ref";

            }
        }

        /// <summary>
        /// Ajoute le lieu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!TxtNom.Regex.IsMatch(TxtNom.LastValue ?? "")
                || !TxtNumero.Regex.IsMatch(TxtNumero.LastValue ?? "")
                || !TxtRue.Regex.IsMatch(TxtRue.LastValue ?? ""))
            {
                CustomQuickDialog d = new CustomQuickDialog("Les valeurs entrées ne sont pas correctes.",
                    GeneralControls.CustomDialogs.QuickDialogType.Error, BtnOk,
                    GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                d.DisplayDelay = 3000;
                d.Show();
                return;
            }

            Lieu l = GLieu.CreerLieu(TxtNom.LastValue, TxtNumero.LastValue, TxtRue.LastValue);
            GLieu.GetVille(int.Parse(ComboVille.SelectedValue.ToString())).TsLesLieux.Add(l);
            GLieu.EnregistrerLieux();
            CChesstion.FillComboBoxes();
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

            this.TxtNumero.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNumero.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNumero.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNumero.Font = Theme.Style.DialogTextBoxFont;
            this.TxtNumero.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtRue.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtRue.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtRue.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtRue.Font = Theme.Style.DialogTextBoxFont;
            this.TxtRue.NormalFont = Theme.Style.DialogTextBoxFont;

            this.LblNom.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNom.Font = Theme.Style.DialogBodyFont;

            this.LblNumero.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNumero.Font = Theme.Style.DialogBodyFont;

            this.LblRue.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblRue.Font = Theme.Style.DialogBodyFont;

            this.LblVille.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblVille.Font = Theme.Style.DialogBodyFont;

            this.ComboVille.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.ComboVille.ForeColor = Theme.Style.DialogTextBoxForeColor;

            this.BtnOk.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.BtnOk.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.BtnOk.Font = Theme.Style.DialogMainButtonsFont;

            this.BtnCancel.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.BtnCancel.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.BtnCancel.Font = Theme.Style.DialogSecondaryButtonsFont;

            TxtNom.CheckPlaceHolder(false);
            TxtNumero.CheckPlaceHolder(false);
            TxtRue.CheckPlaceHolder(false);

        }
    }
}
