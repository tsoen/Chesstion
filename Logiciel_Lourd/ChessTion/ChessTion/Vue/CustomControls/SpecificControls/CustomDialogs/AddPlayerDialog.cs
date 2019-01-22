using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up d'ajout de <see cref="Joueur"/>.
    /// </summary>
    class AddPlayerDialog : Form
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
        /// Textbox du nom du joueur.
        /// </summary>
        protected ControlledTextBox TxtNom { get; } = new ControlledTextBox();

        /// <summary>
        /// Textbox du prénom du joueur.
        /// </summary>
        protected ControlledTextBox TxtPrenom { get; } = new ControlledTextBox();

        /// <summary>
        /// Textbox de la date de naissance du joueur.
        /// </summary>
        protected ControlledTextBox TxtNele { get; } = new ControlledTextBox();

        /// <summary>
        /// Textbox de l'elo du joueur.
        /// </summary>
        protected ControlledTextBox TxtElo { get; } = new ControlledTextBox();

        /// <summary>
        /// Textbox du sexe du joueur.
        /// </summary>
        protected ControlledTextBox TxtSexe { get; } = new ControlledTextBox();

        /// <summary>
        /// Textbox de la nationalité du joueur.
        /// </summary>
        protected ControlledTextBox TxtNationalite { get; } = new ControlledTextBox();

        /// <summary>
        /// Combo box du club du joueur.
        /// </summary>
        protected ComboBox CboClub { get; } = new ComboBox();



        /// <summary>
        /// Label du nom du joueur.
        /// </summary>
        protected Label LblNom { get; } = new Label();

        /// <summary>
        /// Label du prénom du joueur.
        /// </summary>
        protected Label LblPrenom { get; } = new Label();

        /// <summary>
        /// Label de la date de naissance du joueur.
        /// </summary>
        protected Label LblNele { get; } = new Label();

        /// <summary>
        /// Label de l'elo du joueur.
        /// </summary>
        protected Label LblElo { get; } = new Label();

        /// <summary>
        /// Label du sexe du joueur.
        /// </summary>
        protected Label LblSexe { get; } = new Label();

        /// <summary>
        /// Label de la nationalité du joueur.
        /// </summary>
        protected Label LblNationalite { get; } = new Label();
        
        /// <summary>
        /// Label du club du joueur.
        /// </summary>
        protected Label LblClub { get; } = new Label();



        /// <summary>
        /// Bouton de validation.
        /// </summary>
        protected Button OKButton { get; } = new Button();

        /// <summary>
        /// Bouton d'annulation.
        /// </summary>
        protected new Button CancelButton { get; } = new Button();










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
        public AddPlayerDialog()
        {
            InitializeComponent();


            CboClub.DataSource = GClub.ListerClubs().OrderBy(o => o.Nom).ToList(); ;
            CboClub.DisplayMember = "Nom";
            CboClub.ValueMember = "Ref";
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
            int ySpace = 31;
            int xSpace = 6;
            int marginH = 12;
            int marginV = 28;
            int sizeY = 27;

            this.SuspendLayout();
            // 
            // AddPlayerDialog (1/2)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(532, 300);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPlayerDialog";
            this.Text = "Ajouter un joueur";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(LblNom);
            this.Controls.Add(LblPrenom);
            this.Controls.Add(LblNele);
            this.Controls.Add(LblElo);
            this.Controls.Add(LblSexe);
            this.Controls.Add(LblNationalite);
            this.Controls.Add(LblClub);
            this.Controls.Add(TxtNom);
            this.Controls.Add(TxtPrenom);
            this.Controls.Add(TxtNele);
            this.Controls.Add(TxtElo);
            this.Controls.Add(TxtSexe);
            this.Controls.Add(TxtNationalite);
            this.Controls.Add(CboClub);
            this.Controls.Add(OKButton);
            this.Controls.Add(CancelButton);
            //
            // LblNom
            //
            this.LblNom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNom.Location = new System.Drawing.Point(marginH, marginV);
            this.LblNom.Name = "LblNom";
            this.LblNom.Size = new System.Drawing.Size(100, sizeY);
            this.LblNom.TabIndex = 2;
            this.LblNom.Text = "Nom";
            this.LblNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LblPrenom
            //
            this.LblPrenom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblPrenom.Location = new System.Drawing.Point(LblNom.Location.X, LblNom.Location.Y + ySpace); // y +45
            this.LblPrenom.Name = "LblPrenom";
            this.LblPrenom.Size = LblNom.Size;
            this.LblPrenom.TabIndex = 0;
            this.LblPrenom.Text = "Prénom";
            this.LblPrenom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LblNele
            //
            this.LblNele.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNele.Location = new System.Drawing.Point(LblPrenom.Location.X, LblPrenom.Location.Y + ySpace); // y +45
            this.LblNele.Name = "LblNele";
            this.LblNele.Size = LblNom.Size;
            this.LblNele.TabIndex = 3;
            this.LblNele.Text = "Né le";
            this.LblNele.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LblElo
            //
            this.LblElo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblElo.Location = new System.Drawing.Point(LblNele.Location.X, LblNele.Location.Y + ySpace); // y +45
            this.LblElo.Name = "LblElo";
            this.LblElo.Size = LblNom.Size;
            this.LblElo.TabIndex = 3;
            this.LblElo.Text = "Elo";
            this.LblElo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LblSexe
            //
            this.LblSexe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblSexe.Location = new System.Drawing.Point(LblElo.Location.X, LblElo.Location.Y + ySpace); // y +45
            this.LblSexe.Name = "LblSexe";
            this.LblSexe.Size = LblNom.Size;
            this.LblSexe.TabIndex = 3;
            this.LblSexe.Text = "Sexe";
            this.LblSexe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LblNationalite
            //
            this.LblNationalite.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNationalite.Location = new System.Drawing.Point(LblSexe.Location.X, LblSexe.Location.Y + ySpace); // y +45
            this.LblNationalite.Name = "LblNationalite";
            this.LblNationalite.Size = LblNom.Size;
            this.LblNationalite.TabIndex = 3;
            this.LblNationalite.Text = "Nationalité";
            this.LblNationalite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LblClub
            //
            this.LblClub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblClub.Location = new System.Drawing.Point(LblNationalite.Location.X, LblNationalite.Location.Y + ySpace); // y +45
            this.LblClub.Name = "LblClub";
            this.LblClub.Size = LblNom.Size;
            this.LblClub.TabIndex = 3;
            this.LblClub.Text = "Club";
            this.LblClub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // TxtNom
            //
            this.TxtNom.AllowControls = true;
            this.TxtNom.AllowDecimal = false;
            this.TxtNom.AllowedCharacters = new List<char>();
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
            this.TxtNom.Location = new System.Drawing.Point(LblNom.Right + xSpace, marginV);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNom.PlaceholderText = "Nom";
            this.TxtNom.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNom.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNom.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtNom.Size = new System.Drawing.Size(ClientSize.Width - TxtNom.Location.X - marginH, sizeY);
            this.TxtNom.TabIndex = 1;
            this.TxtNom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            //
            // TxtPrenom
            //
            this.TxtPrenom.AllowControls = true;
            this.TxtPrenom.AllowDecimal = false;
            this.TxtPrenom.AllowedCharacters = new List<char>();
            this.TxtPrenom.AllowEmpty = false;
            this.TxtPrenom.AllowEuro = false;
            this.TxtPrenom.AllowLetters = true;
            this.TxtPrenom.AllowNumbers = true;
            this.TxtPrenom.AllowSpace = true;
            this.TxtPrenom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtPrenom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrenom.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtPrenom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtPrenom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtPrenom.LastValue = null;
            this.TxtPrenom.Location = new System.Drawing.Point(LblPrenom.Right + xSpace, TxtNom.Location.Y + ySpace);
            this.TxtPrenom.Name = "TxtPrenom";
            this.TxtPrenom.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtPrenom.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtPrenom.PlaceholderText = "Prenom";
            this.TxtPrenom.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtPrenom.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtPrenom.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtPrenom.Size = new System.Drawing.Size(TxtNom.Width, sizeY);
            this.TxtPrenom.TabIndex = 2;
            this.TxtPrenom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            //
            // TxtNele
            //
            this.TxtNele.AllowControls = true;
            this.TxtNele.AllowDecimal = false;
            this.TxtNele.AllowedCharacters = new List<char> {'/', '.', '-'};
            this.TxtNele.AllowEmpty = false;
            this.TxtNele.AllowEuro = false;
            this.TxtNele.AllowLetters = true;
            this.TxtNele.AllowNumbers = true;
            this.TxtNele.AllowSpace = true;
            this.TxtNele.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNele.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNele.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNele.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNele.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNele.LastValue = null;
            this.TxtNele.Location = new System.Drawing.Point(LblNele.Right + xSpace, TxtPrenom.Location.Y + ySpace);
            this.TxtNele.Name = "TxtNele";
            this.TxtNele.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNele.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNele.PlaceholderText = "01/01/1950";
            this.TxtNele.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNele.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNele.Regex = new System.Text.RegularExpressions.Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            this.TxtNele.Size = new System.Drawing.Size(TxtNom.Width, sizeY);
            this.TxtNele.TabIndex = 3;
            this.TxtNele.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtNele.MaxLength = 10;
            //
            // TxtElo
            //
            this.TxtElo.AllowControls = true;
            this.TxtElo.AllowDecimal = false;
            this.TxtElo.AllowedCharacters = new List<char>();
            this.TxtElo.AllowEmpty = false;
            this.TxtElo.AllowEuro = false;
            this.TxtElo.AllowLetters = false;
            this.TxtElo.AllowNumbers = true;
            this.TxtElo.AllowSpace = false;
            this.TxtElo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtElo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtElo.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtElo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtElo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtElo.LastValue = null;
            this.TxtElo.Location = new System.Drawing.Point(LblElo.Right + xSpace, TxtNele.Location.Y + ySpace);
            this.TxtElo.Name = "TxtElo";
            this.TxtElo.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtElo.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtElo.PlaceholderText = "1500";
            this.TxtElo.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtElo.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtElo.Regex = new System.Text.RegularExpressions.Regex("[1-3][0-9]{3}");
            this.TxtElo.Size = new System.Drawing.Size(TxtNom.Width, sizeY);
            this.TxtElo.TabIndex = 4;
            this.TxtElo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtElo.MaxLength = 4;
            //
            // TxtSexe
            //
            this.TxtSexe.AllowControls = true;
            this.TxtSexe.AllowDecimal = false;
            this.TxtSexe.AllowedCharacters = new List<char>() { 'm', 'M', 'f', 'F' };
            this.TxtSexe.AllowEmpty = false;
            this.TxtSexe.AllowEuro = false;
            this.TxtSexe.AllowLetters = false;
            this.TxtSexe.AllowNumbers = false;
            this.TxtSexe.AllowSpace = false;
            this.TxtSexe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtSexe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtSexe.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtSexe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtSexe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtSexe.LastValue = null;
            this.TxtSexe.Location = new System.Drawing.Point(LblSexe.Right + xSpace, TxtElo.Location.Y + ySpace);
            this.TxtSexe.Name = "TxtSexe";
            this.TxtSexe.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtSexe.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtSexe.PlaceholderText = "F";
            this.TxtSexe.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtSexe.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtSexe.Regex = new System.Text.RegularExpressions.Regex("(m|M|f|F)");
            this.TxtSexe.Size = new System.Drawing.Size(TxtNom.Width, sizeY);
            this.TxtSexe.TabIndex = 5;
            this.TxtSexe.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtSexe.MaxLength = 1;
            //
            // TxtNationalite
            //
            this.TxtNationalite.AllowControls = true;
            this.TxtNationalite.AllowDecimal = false;
            this.TxtNationalite.AllowedCharacters = new List<char>();
            this.TxtNationalite.AllowEmpty = false;
            this.TxtNationalite.AllowEuro = false;
            this.TxtNationalite.AllowLetters = true;
            this.TxtNationalite.AllowNumbers = false;
            this.TxtNationalite.AllowSpace = false;
            this.TxtNationalite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNationalite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNationalite.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNationalite.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNationalite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNationalite.LastValue = null;
            this.TxtNationalite.Location = new System.Drawing.Point(LblNationalite.Right + xSpace, TxtSexe.Location.Y + ySpace);
            this.TxtNationalite.Name = "TxtNationalite";
            this.TxtNationalite.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNationalite.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNationalite.PlaceholderText = "FRA";
            this.TxtNationalite.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNationalite.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNationalite.Regex = new System.Text.RegularExpressions.Regex("[a-zA-Z]+");
            this.TxtNationalite.Size = new System.Drawing.Size(TxtNom.Width, sizeY);
            this.TxtNationalite.TabIndex = 6;
            this.TxtNationalite.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtNationalite.MaxLength = 3;
            //
            // CboClub
            //
            this.CboClub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.CboClub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CboClub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.CboClub.Location = new System.Drawing.Point(LblClub.Right + xSpace, TxtNationalite.Location.Y + ySpace);
            this.CboClub.Size = new System.Drawing.Size(TxtNom.Width, sizeY);
            this.CboClub.TabIndex = 7;
            this.CboClub.FlatStyle = FlatStyle.Flat;
            this.CboClub.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OKButton.Location = new System.Drawing.Point(411, CboClub.Bottom + marginV/2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(109, 45);
            this.OKButton.TabIndex = 20;
            this.OKButton.Text = "Valider";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Enabled = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CancelButton.Location = new System.Drawing.Point(OKButton.Left - xSpace - OKButton.Width, OKButton.Location.Y);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = OKButton.Size;
            this.CancelButton.TabIndex = 21;
            this.CancelButton.Text = "Annuler";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddPlayerDialog (2/2)
            // 
            this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, CancelButton.Bottom + marginV/2);
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
        /// Ajoute le joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (
                !(TxtNom.Regex.IsMatch(TxtNom.Text) && TxtPrenom.Regex.IsMatch(TxtPrenom.Text) &&
                  TxtNele.Regex.IsMatch(TxtNele.Text) && TxtElo.Regex.IsMatch(TxtElo.Text) &&
                  TxtSexe.Regex.IsMatch(TxtSexe.Text) && TxtNationalite.Regex.IsMatch(TxtNationalite.Text) && CboClub.SelectedValue != null))
                return;

            CChesstion.CreerJoueur(TxtNom.Text.ToUpper(), TxtPrenom.Text, TxtSexe.Text.ToUpper(), TxtNationalite.Text,
                int.Parse(TxtElo.Text), TxtNele.Text, int.Parse(CboClub.SelectedValue.ToString()));

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Ferme la pop-up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

            this.TxtElo.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtElo.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtElo.Font = Theme.Style.DialogTextBoxFont;

            this.TxtNationalite.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNationalite.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNationalite.Font = Theme.Style.DialogTextBoxFont;

            this.TxtNele.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNele.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNele.Font = Theme.Style.DialogTextBoxFont;

            this.TxtNom.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNom.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNom.Font = Theme.Style.DialogTextBoxFont;

            this.TxtPrenom.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtPrenom.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtPrenom.Font = Theme.Style.DialogTextBoxFont;

            this.TxtSexe.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtSexe.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtSexe.Font = Theme.Style.DialogTextBoxFont;

            this.LblElo.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblElo.Font = Theme.Style.DialogBodyFont;

            this.LblClub.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblClub.Font = Theme.Style.DialogBodyFont;

            this.LblNationalite.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNationalite.Font = Theme.Style.DialogBodyFont;

            this.LblNele.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNele.Font = Theme.Style.DialogBodyFont;

            this.LblNom.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNom.Font = Theme.Style.DialogBodyFont;

            this.LblPrenom.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblPrenom.Font = Theme.Style.DialogBodyFont;

            this.LblSexe.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblSexe.Font = Theme.Style.DialogBodyFont;

            this.CboClub.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.CboClub.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.CboClub.Font = Theme.Style.DialogTextBoxFont;

            this.OKButton.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.OKButton.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.OKButton.Font = Theme.Style.DialogMainButtonsFont;

            this.CancelButton.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.CancelButton.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.CancelButton.Font = Theme.Style.DialogSecondaryButtonsFont;

            TxtNom.CheckPlaceHolder(false);
            TxtPrenom.CheckPlaceHolder(false);
            TxtNele.CheckPlaceHolder(false);
            TxtElo.CheckPlaceHolder(false);
            TxtSexe.CheckPlaceHolder(false);
            TxtNationalite.CheckPlaceHolder(false);

        }
    }
}
