using ChessTion.Vue.CustomControls;
using ChessTion.Vue.CustomControls.GeneralControls.CustomButtons;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls;
using ChessTion.Vue.CustomControls.SpecificControls.CustomMenus;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;

namespace ChessTion
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.msMenu = new CustomMenuStrip();
            this.panelCentre = new CentrePanel();
            this.statusPanel = new StatusPanel();
#if DEBUG
            this.btnTest4 = new System.Windows.Forms.Button();
            this.btnTest3 = new System.Windows.Forms.Button();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.btnTest1 = new System.Windows.Forms.Button();
#endif
            this.btnOpen = new System.Windows.Forms.Button();
            this.panelJoueur = new JoueurPanel();
            this.panelOpen = new OpenPanel();
            this.panelAction = new ActionPanel();
            this.btnTerminerTournoi = new System.Windows.Forms.Button();
            this.btnDebuterTournoi = new System.Windows.Forms.Button();
            this.btnCloreInscriptions = new System.Windows.Forms.Button();
            this.btnOuvrirInscription = new System.Windows.Forms.Button();
            this.panelRepas = new RepasPanel();
            this.hiddenTextBox3 = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.HiddenTextBox();
            this.hiddenTextBox2 = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.HiddenTextBox();
            this.hiddenTextBox1 = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.HiddenTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelOpens = new OpensPanel();
            this.msMenu.SuspendLayout();
            this.panelCentre.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelRepas.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.msMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.msMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1348, 28);
            this.msMenu.TabIndex = 1;
            this.msMenu.Text = "menuStrip1";
            // 
            // panelJoueur
            // 
            this.panelJoueur.ReadOnly = false;
            this.panelJoueur.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.panelJoueur.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            this.panelJoueur.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.panelJoueur.HeaderHeight = 0;
            this.panelJoueur.Location = new System.Drawing.Point(1048, 31);
            this.panelJoueur.Name = "panelJoueur";
            this.panelJoueur.ShowBorderWhenFocused = false;
            this.panelJoueur.Size = new System.Drawing.Size(300, 453);
            this.panelJoueur.TabIndex = 60;
            this.panelJoueur.Title = "Joueur";
            // 
            // panelOpen
            // 
            this.panelOpen.ReadOnly = false;
            this.panelOpen.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.panelOpen.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            this.panelOpen.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.panelOpen.HeaderHeight = 0;
            this.panelOpen.Location = new System.Drawing.Point(1048, 600);
            this.panelOpen.Name = "panelOpen";
            this.panelOpen.ShowBorderWhenFocused = false;
            this.panelOpen.Size = new System.Drawing.Size(300, 237);
            this.panelOpen.TabIndex = 70;
            this.panelOpen.Title = "Open";
            // 
            // panelCentre
            // 
            this.panelCentre.AllowAdd = true;
            this.panelCentre.AllowDelete = true;
            this.panelCentre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panelCentre.Controls.Add(this.btnOpen);
            this.panelCentre.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelCentre.HeaderFont = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.panelCentre.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.panelCentre.HeaderHeight = 48;
            this.panelCentre.Location = new System.Drawing.Point(202, 31);
            this.panelCentre.Margin = new System.Windows.Forms.Padding(2);
            this.panelCentre.Name = "panelCentre";
            this.panelCentre.ShowBorderWhenFocused = false;
            this.panelCentre.Size = new System.Drawing.Size(844, 628);
            this.panelCentre.TabIndex = 9;
            this.panelCentre.Title = "Open de la Thur - Open B";
#if DEBUG
            // 
            // btnTest4
            // 
            this.btnTest4.ForeColor = System.Drawing.Color.Black;
            this.btnTest4.Location = new System.Drawing.Point(1500, 242);
            this.btnTest4.Name = "btnTest4";
            this.btnTest4.Size = new System.Drawing.Size(98, 26);
            this.btnTest4.TabIndex = 63;
            this.btnTest4.Text = "Test 4";
            this.btnTest4.UseVisualStyleBackColor = true;
            this.btnTest4.BringToFront();
            // 
            // btnTest3
            // 
            this.btnTest3.ForeColor = System.Drawing.Color.Black;
            this.btnTest3.Location = new System.Drawing.Point(1500, 191);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(98, 26);
            this.btnTest3.TabIndex = 62;
            this.btnTest3.Text = "Test 3";
            this.btnTest3.UseVisualStyleBackColor = true;
            this.btnTest3.BringToFront();
            // 
            // btnTest2
            // 
            this.btnTest2.ForeColor = System.Drawing.Color.Black;
            this.btnTest2.Location = new System.Drawing.Point(1500, 145);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(98, 26);
            this.btnTest2.TabIndex = 61;
            this.btnTest2.Text = "Test 2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.BringToFront();
            // 
            // btnTest1
            // 
            this.btnTest1.ForeColor = System.Drawing.Color.Black;
            this.btnTest1.Location = new System.Drawing.Point(1500, 104);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(98, 26);
            this.btnTest1.TabIndex = 60;
            this.btnTest1.Text = "Test 1";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.BringToFront();
#endif
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            //
            // panelAction
            // 
            this.panelAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelAction.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelAction.HeaderFont = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.panelAction.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.panelAction.HeaderHeight = 2;
            this.panelAction.Location = new System.Drawing.Point(202, 659);
            this.panelAction.Margin = new System.Windows.Forms.Padding(2);
            this.panelAction.Name = "panelAction";
            this.panelAction.ShowBorderWhenFocused = false;
            this.panelAction.Size = new System.Drawing.Size(844, 68);
            this.panelAction.TabIndex = 8;
            this.panelAction.Title = "";
            // 
            // btnTerminerTournoi
            // 
            this.btnTerminerTournoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnTerminerTournoi.Enabled = false;
            this.btnTerminerTournoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminerTournoi.Location = new System.Drawing.Point(595, 10);
            this.btnTerminerTournoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnTerminerTournoi.Name = "btnTerminerTournoi";
            this.btnTerminerTournoi.Size = new System.Drawing.Size(170, 45);
            this.btnTerminerTournoi.TabIndex = 24;
            this.btnTerminerTournoi.Text = "Terminer le tournoi";
            this.btnTerminerTournoi.UseVisualStyleBackColor = false;
            // 
            // btnDebuterTournoi
            // 
            this.btnDebuterTournoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnDebuterTournoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDebuterTournoi.Location = new System.Drawing.Point(419, 10);
            this.btnDebuterTournoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnDebuterTournoi.Name = "btnDebuterTournoi";
            this.btnDebuterTournoi.Size = new System.Drawing.Size(170, 45);
            this.btnDebuterTournoi.TabIndex = 23;
            this.btnDebuterTournoi.Text = "Débuter le tournoi";
            this.btnDebuterTournoi.UseVisualStyleBackColor = false;
            // 
            // btnCloreInscriptions
            // 
            this.btnCloreInscriptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnCloreInscriptions.Enabled = false;
            this.btnCloreInscriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloreInscriptions.Location = new System.Drawing.Point(242, 10);
            this.btnCloreInscriptions.Margin = new System.Windows.Forms.Padding(2);
            this.btnCloreInscriptions.Name = "btnCloreInscriptions";
            this.btnCloreInscriptions.Size = new System.Drawing.Size(170, 45);
            this.btnCloreInscriptions.TabIndex = 22;
            this.btnCloreInscriptions.Text = "Clôre les inscriptions";
            this.btnCloreInscriptions.UseVisualStyleBackColor = false;
            // 
            // btnOuvrirInscription
            // 
            this.btnOuvrirInscription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnOuvrirInscription.Enabled = false;
            this.btnOuvrirInscription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOuvrirInscription.Location = new System.Drawing.Point(68, 10);
            this.btnOuvrirInscription.Margin = new System.Windows.Forms.Padding(2);
            this.btnOuvrirInscription.Name = "btnOuvrirInscription";
            this.btnOuvrirInscription.Size = new System.Drawing.Size(170, 45);
            this.btnOuvrirInscription.TabIndex = 21;
            this.btnOuvrirInscription.Text = "Ouvrir les inscriptions";
            this.btnOuvrirInscription.UseVisualStyleBackColor = false;
            // 
            // panelRepas
            // 
           /* this.panelRepas.AllowAdd = true;
            this.panelRepas.AllowDelete = true;
            this.panelRepas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelRepas.Controls.Add(this.hiddenTextBox3);
            this.panelRepas.Controls.Add(this.hiddenTextBox2);
            this.panelRepas.Controls.Add(this.hiddenTextBox1);
            this.panelRepas.Controls.Add(this.label2);
            this.panelRepas.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panelRepas.HeaderFont = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.panelRepas.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.panelRepas.HeaderHeight = 32;
            this.panelRepas.Location = new System.Drawing.Point(0, 382);
            this.panelRepas.Margin = new System.Windows.Forms.Padding(2);
            this.panelRepas.Name = "panelRepas";
            this.panelRepas.ShowBorderWhenFocused = false;
            this.panelRepas.Size = new System.Drawing.Size(200, 341);
            this.panelRepas.TabIndex = 4;
            this.panelRepas.Title = "Repas";*/
            // 
            // hiddenTextBox3
            // 
            this.hiddenTextBox3.AllowControls = true;
            this.hiddenTextBox3.AllowDecimal = false;
            this.hiddenTextBox3.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("hiddenTextBox3.AllowedCharacters")));
            this.hiddenTextBox3.AllowEmpty = true;
            this.hiddenTextBox3.AllowEuro = false;
            this.hiddenTextBox3.AllowLetters = false;
            this.hiddenTextBox3.AllowNumbers = false;
            this.hiddenTextBox3.AllowSpace = false;
            this.hiddenTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.hiddenTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hiddenTextBox3.ErrorBackColor = System.Drawing.Color.Empty;
            this.hiddenTextBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.hiddenTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.hiddenTextBox3.LastValue = null;
            this.hiddenTextBox3.Location = new System.Drawing.Point(5, 111);
            this.hiddenTextBox3.Margin = new System.Windows.Forms.Padding(2);
            this.hiddenTextBox3.Name = "hiddenTextBox3";
            this.hiddenTextBox3.NormalColor = System.Drawing.Color.Empty;
            this.hiddenTextBox3.NormalFont = null;
            this.hiddenTextBox3.PlaceholderText = "";
            this.hiddenTextBox3.PlaceholdingColor = System.Drawing.Color.Empty;
            this.hiddenTextBox3.PlaceholdingFont = null;
            this.hiddenTextBox3.Regex = null;
            this.hiddenTextBox3.Size = new System.Drawing.Size(190, 20);
            this.hiddenTextBox3.TabIndex = 5;
            this.hiddenTextBox3.Text = "Merguez 3 €";
            // 
            // hiddenTextBox2
            // 
            this.hiddenTextBox2.AllowControls = true;
            this.hiddenTextBox2.AllowDecimal = false;
            this.hiddenTextBox2.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("hiddenTextBox2.AllowedCharacters")));
            this.hiddenTextBox2.AllowEmpty = true;
            this.hiddenTextBox2.AllowEuro = false;
            this.hiddenTextBox2.AllowLetters = false;
            this.hiddenTextBox2.AllowNumbers = false;
            this.hiddenTextBox2.AllowSpace = false;
            this.hiddenTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.hiddenTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hiddenTextBox2.ErrorBackColor = System.Drawing.Color.Empty;
            this.hiddenTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.hiddenTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.hiddenTextBox2.LastValue = null;
            this.hiddenTextBox2.Location = new System.Drawing.Point(5, 80);
            this.hiddenTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.hiddenTextBox2.Name = "hiddenTextBox2";
            this.hiddenTextBox2.NormalColor = System.Drawing.Color.Empty;
            this.hiddenTextBox2.NormalFont = null;
            this.hiddenTextBox2.PlaceholderText = "";
            this.hiddenTextBox2.PlaceholdingColor = System.Drawing.Color.Empty;
            this.hiddenTextBox2.PlaceholdingFont = null;
            this.hiddenTextBox2.Regex = null;
            this.hiddenTextBox2.Size = new System.Drawing.Size(190, 20);
            this.hiddenTextBox2.TabIndex = 4;
            this.hiddenTextBox2.Text = "Frites 2 €";
            // 
            // hiddenTextBox1
            // 
            this.hiddenTextBox1.AllowControls = true;
            this.hiddenTextBox1.AllowDecimal = false;
            this.hiddenTextBox1.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("hiddenTextBox1.AllowedCharacters")));
            this.hiddenTextBox1.AllowEmpty = true;
            this.hiddenTextBox1.AllowEuro = false;
            this.hiddenTextBox1.AllowLetters = false;
            this.hiddenTextBox1.AllowNumbers = false;
            this.hiddenTextBox1.AllowSpace = false;
            this.hiddenTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.hiddenTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hiddenTextBox1.ErrorBackColor = System.Drawing.Color.Empty;
            this.hiddenTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.hiddenTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.hiddenTextBox1.LastValue = null;
            this.hiddenTextBox1.Location = new System.Drawing.Point(5, 49);
            this.hiddenTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.hiddenTextBox1.Name = "hiddenTextBox1";
            this.hiddenTextBox1.NormalColor = System.Drawing.Color.Empty;
            this.hiddenTextBox1.NormalFont = null;
            this.hiddenTextBox1.PlaceholderText = "";
            this.hiddenTextBox1.PlaceholdingColor = System.Drawing.Color.Empty;
            this.hiddenTextBox1.PlaceholdingFont = null;
            this.hiddenTextBox1.Regex = null;
            this.hiddenTextBox1.Size = new System.Drawing.Size(190, 20);
            this.hiddenTextBox1.TabIndex = 1;
            this.hiddenTextBox1.Text = "Burger 4 €";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Repas";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelOpens
            // 
            this.panelOpens.AllowAdd = true;
            this.panelOpens.AllowDelete = true;
            this.panelOpens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelOpens.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panelOpens.HeaderFont = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.panelOpens.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.panelOpens.HeaderHeight = 32;
            this.panelOpens.Location = new System.Drawing.Point(0, 30);
            this.panelOpens.Margin = new System.Windows.Forms.Padding(2);
            this.panelOpens.Name = "panelOpens";
            this.panelOpens.ShowBorderWhenFocused = false;
            this.panelOpens.Size = new System.Drawing.Size(100, 350);
            this.panelOpens.TabIndex = 0;
            this.panelOpens.Title = "Opens";
            
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.panelCentre);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelRepas);
            this.Controls.Add(this.msMenu);
            this.Controls.Add(this.panelOpens);
            this.Controls.Add(this.panelJoueur);
            this.Controls.Add(this.panelOpen);
#if DEBUG
            this.Controls.Add(this.btnTest1);
            this.Controls.Add(this.btnTest2);
            this.Controls.Add(this.btnTest3);
            this.Controls.Add(this.btnTest4);
#endif
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.MainMenuStrip = this.msMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(399, 598);
            this.Name = "FormPrincipal";
            this.Text = "Chess\'Tion";
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.panelCentre.ResumeLayout(false);
            this.panelCentre.PerformLayout();
            this.panelAction.ResumeLayout(false);
            this.panelRepas.ResumeLayout(false);
            this.panelRepas.PerformLayout();
            this.panelOpens.ResumeLayout(false);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Load += new System.EventHandler(this.formPrincipal_Load);
        }

        #endregion

        private CustomMenuStrip msMenu;
        private OpensPanel panelOpens;
        private RepasPanel panelRepas;
        private System.Windows.Forms.Label label2;
        private HiddenTextBox hiddenTextBox1;
        private HiddenTextBox hiddenTextBox3;
        private HiddenTextBox hiddenTextBox2;
        private ActionPanel panelAction;
        private System.Windows.Forms.Button btnOuvrirInscription;
        private System.Windows.Forms.Button btnTerminerTournoi;
        private System.Windows.Forms.Button btnDebuterTournoi;
        private System.Windows.Forms.Button btnCloreInscriptions;
        private CentrePanel panelCentre;
        private JoueurPanel panelJoueur;
        private OpenPanel panelOpen;
        private StatusPanel statusPanel;
        private System.Windows.Forms.Button btnOpen;
#if DEBUG
        private System.Windows.Forms.Button btnTest4;
        private System.Windows.Forms.Button btnTest3;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Button btnTest1;
#endif
    }
}

