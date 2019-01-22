using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes
{
    /// <summary>
    /// Classe vue représentant un <see cref="PlaceHolderTextBox"/> dont les valeurs entrées sont contrôlées.
    /// </summary>
    public class ControlledTextBox : PlaceHolderTextBox
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
        /// Evénement émit si le texte ne respecte pas les conditions de validation.
        /// </summary>
        public event EventHandler Unvalidated;

        /// <summary> 
        /// Dernier message d'erreur.
        /// </summary>
        protected string lastErrorMessage = "";

        /// <summary>
        /// Regex à respecter pour valider le texte entré.
        /// </summary>
        private System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(".?");

        /// <summary>
        /// Couleur d'erreur pour l'arrière plan.
        /// </summary>
        public Color ErrorBackColor { get; set; }

        /// <summary>
        /// Couleur normale pour l'arrière plan.
        /// </summary>
        private Color NormalBackColor { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de rentrer des lettres (minuscules et majuscules).
        /// </summary>
        public bool AllowLetters { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de rentrer des chiffres.
        /// </summary>
        public bool AllowNumbers { get; set; }

        /// <summary>
        /// Permet à l'utilisateur d'utiliser les touches de navigation (flèches, backspace, etc.).
        /// </summary>
        public bool AllowControls { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de rentrer des points et des virgules.
        /// </summary>
        public bool AllowDecimal { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de rentrer des signes euros.
        /// </summary>
        public bool AllowEuro { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de rentrer des espaces.
        /// </summary>
        public bool AllowSpace { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de valider le champ s'il est vide.
        /// </summary>
        public bool AllowEmpty { get; set; }

        /// <summary>
        /// Permet à l'utilisateur de rentrer les caractères de la liste.
        /// </summary>
        public List<char> AllowedCharacters { get; set; }










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
        public ControlledTextBox()
        {
            AllowLetters = false;
            AllowNumbers = false;
            AllowControls = true;
            AllowDecimal = false;
            AllowEuro = false;
            AllowSpace = false;
            AllowedCharacters = new List<char>();
            AllowEmpty = true;

            NormalBackColor = this.BackColor;
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
        /// Couleur d'erreur pour l'arrière plan.
        /// </summary>
        public string LastErrorMessage { get { return lastErrorMessage; } }

        /// <summary>
        /// Dernière valeur correcte.
        /// </summary>
        public string LastValue { get; set; }

        /// <summary>
        /// Entre une nouvelle valeur.
        /// </summary>
        public string NewValue
        {
            set
            {
                this.LastValue = value;
                this.Text = value;
            }
        }

        /// <summary>
        /// Regex à respecter pour valider le texte entré.
        /// </summary>
        public System.Text.RegularExpressions.Regex Regex
        {
            get { return regex; }
            set { regex = value; }
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
        /// Vérifie le texte entré.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            this.Text = this.Text.Trim();

            if (!this.Regex.IsMatch(this.Text))
            {
                //System.Diagnostics.Debug.WriteLine("Does not match regex; cancelling");
                this.NormalBackColor = this.BackColor;
                this.BackColor = this.ErrorBackColor;
                e.Cancel = true;
                lastErrorMessage = "Le texte entré ne respecte pas le format demandé !";

                Unvalidated?.Invoke(this, new EventArgs());
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("Matches regex");
                this.BackColor = NormalBackColor;
            }
        }

        /// <summary>
        /// Si le texte est validé, le concidère comme nouvelle.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            this.NewValue = this.Text;
        }

        /// <summary>
        /// Vérifie si la touche entrée est acceptée.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (this.BackColor == ErrorBackColor) { BackColor = NormalBackColor; }

            if (e.KeyChar == (Char) Keys.Escape)
            {
                e.Handled = true;
                this.CausesValidation = false;
                this.Text = this.LastValue;
                this.Parent.Focus();
                if (this.Focused)
                {
                    Parent.Controls[0].Focus();
                    if (this.Focused && Parent.Controls.Count > 1)
                        Parent.Controls[1].Focus();
                }
                this.CausesValidation = true;
                this.CheckPlaceHolder(false);
                BackColor = NormalBackColor;
            }
            if (char.IsLetter(e.KeyChar) && this.AllowLetters) { /*System.Diagnostics.Debug.WriteLine("AllowLetters");*/ return; }
            if (char.IsDigit(e.KeyChar) && this.AllowNumbers) { /*System.Diagnostics.Debug.WriteLine("AllowNumbers");*/ return; }
            if (char.IsControl(e.KeyChar) && this.AllowControls) { /*System.Diagnostics.Debug.WriteLine("AllowControls");*/ return; }
            if ((char)e.KeyChar == ' ' && this.AllowSpace) { /*System.Diagnostics.Debug.WriteLine("AllowSpace");*/ return; }
            if (".,".Contains(e.KeyChar) && this.AllowDecimal) { /*System.Diagnostics.Debug.WriteLine("AllowDecimal");*/ return; }
            if ((char)e.KeyChar == '€' && this.AllowEuro) { /*System.Diagnostics.Debug.WriteLine("!AllowEuro");*/ return; }
            if (this.AllowedCharacters.Contains(e.KeyChar)) { /*System.Diagnostics.Debug.WriteLine("!AllowedCharacters");*/ return; }

            e.Handled = true;

        }

        /// <summary>
        /// Si l'arrière plan change, il est enregistré comme nouvel arrière plan normal.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (this.BackColor != NormalBackColor && BackColor != ErrorBackColor)
                NormalBackColor = this.BackColor;
        }
    }
}
