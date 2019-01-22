using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up qui permet une entrée avec un label de réponse.
    /// </summary>
    class SingleInputWithAnswerDialog : SingleInputDialog
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
        /// Textbox de réponse.
        /// </summary>
        protected GeneralControls.CustomTextBoxes.ControlledTextBox AnswerTextBox;

        /// <summary>
        /// Label de réponse.
        /// </summary>
        protected Label AnswerLabel;










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
        /// <param name="title">Titre.</param>
        /// <param name="message">Message.</param>
        public SingleInputWithAnswerDialog(string title = "Input dialog", string message = "Please input value")
            : base(title, message)
        {
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPlayerFromFFEDialog));
            this.AnswerTextBox = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.AnswerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AnswerTextBox
            // 
            this.AnswerTextBox.AllowControls = true;
            this.AnswerTextBox.AllowDecimal = false;
            this.AnswerTextBox.AllowedCharacters = new List<char>();
            this.AnswerTextBox.AllowEmpty = false;
            this.AnswerTextBox.AllowEuro = false;
            this.AnswerTextBox.AllowLetters = true;
            this.AnswerTextBox.AllowNumbers = true;
            this.AnswerTextBox.AllowSpace = true;
            this.AnswerTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.AnswerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AnswerTextBox.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.AnswerTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AnswerTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.AnswerTextBox.LastValue = null;
            this.AnswerTextBox.Location = new System.Drawing.Point(12, 139);
            this.AnswerTextBox.Name = "AnswerTextBox";
            this.AnswerTextBox.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.AnswerTextBox.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.AnswerTextBox.PlaceholderText = "";
            this.AnswerTextBox.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.AnswerTextBox.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.AnswerTextBox.ReadOnly = true;
            this.AnswerTextBox.Regex = new System.Text.RegularExpressions.Regex(".?");
            this.AnswerTextBox.Size = new System.Drawing.Size(278, 27);
            this.AnswerTextBox.TabIndex = 7;
            this.AnswerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AnswerLabel
            // 
            this.AnswerLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AnswerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.AnswerLabel.Location = new System.Drawing.Point(12, 116);
            this.AnswerLabel.Name = "AnswerLabel";
            this.AnswerLabel.Size = new System.Drawing.Size(278, 20);
            this.AnswerLabel.TabIndex = 8;
            this.AnswerLabel.Text = "Found:";
            this.AnswerLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // AddPlayerFromFFEDialog
            // 
            this.Controls.Add(this.AnswerLabel);
            this.Controls.Add(this.AnswerTextBox);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
