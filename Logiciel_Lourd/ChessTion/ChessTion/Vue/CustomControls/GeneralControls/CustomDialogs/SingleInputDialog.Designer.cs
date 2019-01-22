namespace ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs
{
    partial class SingleInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleInputDialog));
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtInput = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.SuspendLayout();
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnValider.Location = new System.Drawing.Point(411, 121);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(109, 45);
            this.btnValider.TabIndex = 20;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAnnuler.Location = new System.Drawing.Point(296, 121);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(109, 45);
            this.btnAnnuler.TabIndex = 21;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage.Location = new System.Drawing.Point(12, 28);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(508, 20);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Veuillez entrer le nom de l\'open";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtInput
            // 
            this.txtInput.AllowControls = true;
            this.txtInput.AllowDecimal = false;
            this.txtInput.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("txtInput.AllowedCharacters")));
            this.txtInput.AllowEmpty = false;
            this.txtInput.AllowEuro = false;
            this.txtInput.AllowLetters = true;
            this.txtInput.AllowNumbers = true;
            this.txtInput.AllowSpace = true;
            this.txtInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.txtInput.LastValue = null;
            this.txtInput.Location = new System.Drawing.Point(12, 61);
            this.txtInput.Name = "txtInput";
            this.txtInput.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.txtInput.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.txtInput.PlaceholderText = "Le test";
            this.txtInput.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.txtInput.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.txtInput.Regex = ((System.Text.RegularExpressions.Regex)(resources.GetObject("txtInput.Regex")));
            this.txtInput.Size = new System.Drawing.Size(508, 27);
            this.txtInput.TabIndex = 1;
            this.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SingleInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(532, 178);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleInputDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Veuillez entrer le nom de blabla";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Label lblMessage;
        private GeneralControls.CustomTextBoxes.ControlledTextBox txtInput;
    }
}