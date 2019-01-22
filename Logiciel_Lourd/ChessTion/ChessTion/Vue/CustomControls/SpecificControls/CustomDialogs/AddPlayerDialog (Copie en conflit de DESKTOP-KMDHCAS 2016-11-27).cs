using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    class AddPlayerDialog : Form
    {
        protected ControlledTextBox TxtNom { get; } = new ControlledTextBox();
        protected ControlledTextBox TxtPrenom { get; } = new ControlledTextBox();
        protected ControlledTextBox TxtNele { get; } = new ControlledTextBox();
        protected ControlledTextBox TxtElo { get; } = new ControlledTextBox();
        protected ControlledTextBox TxtSexe { get; } = new ControlledTextBox();
        protected ControlledTextBox TxtNationalite { get; } = new ControlledTextBox();
        protected Label LblNom { get; } = new Label();
        protected Label LblPrenom { get; } = new Label();
        protected Label LblNele { get; } = new Label();
        protected Label LblElo { get; } = new Label();
        protected Label LblSexe { get; } = new Label();
        protected Label LblNationalite { get; } = new Label();
        protected Button OKButton { get; } = new Button();
        protected Button CancelButton { get; } = new Button();

        public AddPlayerDialog()
        {
            InitializeComponent();

            TxtNom.CheckPlaceHolder(false);
            TxtPrenom.CheckPlaceHolder(false);
            TxtNele.CheckPlaceHolder(false);
            TxtElo.CheckPlaceHolder(false);
            TxtSexe.CheckPlaceHolder(false);
            TxtNationalite.CheckPlaceHolder(false);
        }

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
            this.MaximizeBox = true;
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
            this.Controls.Add(TxtNom);
            this.Controls.Add(TxtPrenom);
            this.Controls.Add(TxtNele);
            this.Controls.Add(TxtElo);
            this.Controls.Add(TxtSexe);
            this.Controls.Add(TxtNationalite);
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
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OKButton.Location = new System.Drawing.Point(411, TxtNationalite.Bottom + ySpace);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(109, 45);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "Valider";
            this.OKButton.UseVisualStyleBackColor = false;
            //this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CancelButton.Location = new System.Drawing.Point(OKButton.Left - xSpace - OKButton.Width, OKButton.Location.Y);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = OKButton.Size;
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Annuler";
            this.CancelButton.UseVisualStyleBackColor = false;
            //this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddPlayerDialog (2/2)
            // 
            this.Size = new System.Drawing.Size(this.Width, CancelButton.Bottom + marginV);
            this.ResumeLayout(false);

        }
    }
}
