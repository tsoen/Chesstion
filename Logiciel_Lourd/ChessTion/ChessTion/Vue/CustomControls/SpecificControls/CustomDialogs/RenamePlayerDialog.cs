using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up permettant de renommer un <see cref="Joueur"/>.
    /// </summary>
    class RenamePlayerDialog : SingleInputWithAnswerDialog
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
        /// Textbox du nom.
        /// </summary>
        protected ControlledTextBox NomTextBox { get { return TextBox; } }

        /// <summary>
        /// Textbox du prénom.
        /// </summary>
        protected ControlledTextBox PrenomTextBox { get; } = new ControlledTextBox();

        /// <summary>
        /// Anciens noms.
        /// </summary>
        private readonly string[] oldName = new string[2];

        /// <summary>
        /// Référence du joueur.
        /// </summary>
        private int reference;










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
        /// <param name="reference">Référence du joueur.</param>
        public RenamePlayerDialog(int reference) : base("Renommer un joueur", "Veuillez entrer le nouveau nom et prénom")
        {
            this.reference = reference;
            oldName[0] = GJoueur.GetJoueur(reference).Nom;
            oldName[1] = GJoueur.GetJoueur(reference).Prenom;

            InitializeComponent();

            OKButton.Click += OKButton_Click;
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
            int marginH = 12, spaceH = 6;

            //
            // NomTextBox
            //
            NomTextBox.Location = new Point(marginH, NomTextBox.Location.Y);
            NomTextBox.Size = new Size(ClientSize.Width / 2 - spaceH / 2 - marginH, NomTextBox.Height);
            NomTextBox.PlaceholderText = oldName[0];
            NomTextBox.CheckPlaceHolder(false);
            //
            // PrenomTextBox
            //
            PrenomTextBox.AllowLetters = true;
            PrenomTextBox.AllowSpace = true;
            PrenomTextBox.AllowControls = true;
            PrenomTextBox.AllowedCharacters = new List<char>() { '\'' };
            PrenomTextBox.BackColor = NomTextBox.BackColor;
            PrenomTextBox.ForeColor = NomTextBox.ForeColor;
            PrenomTextBox.Font = NomTextBox.Font;
            PrenomTextBox.ErrorBackColor = NomTextBox.ErrorBackColor;
            PrenomTextBox.NormalColor = NomTextBox.NormalColor;
            PrenomTextBox.NormalFont = NomTextBox.NormalFont;
            PrenomTextBox.PlaceholdingColor = NomTextBox.PlaceholdingColor;
            PrenomTextBox.PlaceholdingFont = NomTextBox.PlaceholdingFont;
            PrenomTextBox.TextAlign = NomTextBox.TextAlign;
            PrenomTextBox.TabIndex = 2;
            PrenomTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            PrenomTextBox.Size = NomTextBox.Size;
            PrenomTextBox.Location = new Point(NomTextBox.Right + spaceH, NomTextBox.Location.Y);
            PrenomTextBox.PlaceholderText = oldName[1];
            PrenomTextBox.Regex = new System.Text.RegularExpressions.Regex(".+");
            PrenomTextBox.CheckPlaceHolder(false);
            //
            // AnswerLabel
            //
            AnswerLabel.Text = "Ancien nom :";
            //
            // AnswerTextBox
            //
            AnswerTextBox.Text = oldName[0] + " " + oldName[1];
            AnswerTextBox.TabStop = false;
            //
            // RenameplayerDialog
            //
            Controls.Add(PrenomTextBox);
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
        /// Renomme le joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (!(NomTextBox.Regex.IsMatch(NomTextBox.Text) && PrenomTextBox.Regex.IsMatch(PrenomTextBox.Text)))
                return;

            CChesstion.RenommerJoueur(reference, NomTextBox.Text, PrenomTextBox.Text);
        }

    }
}
