using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up qui permet une entrée avec une combo box de réponse.
    /// </summary>
    class SingleInputWithComboAnswerDialog : SingleInputDialog
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
        /// Combo box de réponse.
        /// </summary>
        protected ComboBox AnswerComboBox { get; } = new ComboBox();

        /// <summary>
        /// Label de réponse.
        /// </summary>
        protected Label AnswerLabel { get; } = new Label();










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
        /// <param name="title">Titre</param>
        /// <param name="message">Message</param>
        public SingleInputWithComboAnswerDialog(string title = "Input dialog", string message = "Please input value")
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
            this.SuspendLayout();
            // 
            // AnswerComboBox
            // 
            this.AnswerComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.AnswerComboBox.FlatStyle = FlatStyle.Flat;
            this.AnswerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.AnswerComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AnswerComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.AnswerComboBox.Location = new System.Drawing.Point(12, 139);
            this.AnswerComboBox.Name = "AnswerComboBox";
            this.AnswerComboBox.Size = new System.Drawing.Size(278, 22);
            this.AnswerComboBox.TabIndex = 7;
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
            this.Controls.Add(this.AnswerComboBox);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
