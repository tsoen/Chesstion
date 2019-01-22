using System;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes
{
    /// <summary>
    /// Classe vue représentant une <see cref="ControlledTextBox"/> dont les bordure peuvent être masquées.
    /// </summary>
    class HiddenTextBox : ControlledTextBox
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
        /// Vrai si on est entré dans la text box.
        /// </summary>
        private bool entered = false; // L'event onEnter était fired deux fois avant ce fix

        /// <summary>
        /// Valeur de <see cref="Control.CausesValidation"/> sauvegardée.
        /// </summary>
        private bool beforeState;










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
        public HiddenTextBox() : base()
        {
            this.BorderStyle = BorderStyle.None;
            this.Text = string.Empty;
            beforeState = CausesValidation;
            ReadOnlyChanged += HiddenTextBox_ReadOnlyChanged;
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
        /// Affecte une nouvelle valeur au champ.
        /// </summary>
        public new string NewValue
        {
            set
            {
                this.Text = value;
                this.LastValue = value;
            }
        }

        /// <summary>
        /// Désactive <see cref="Control.CausesValidation"/> si le control est en readonly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiddenTextBox_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (!ReadOnly)
                CausesValidation = beforeState;
            else
            {
                beforeState = CausesValidation;
                CausesValidation = false;
            }
        }

        /// <summary>
        /// Quitte le control si on appuie sur Entrée.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar == (char)Keys.Enter)
                this.Parent.Focus();
        }

        /// <summary>
        /// Affiche le texte au format d'édition si rentre dans le text box.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (entered || ReadOnly)
                return;

            entered = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y - 1);
            if (this.Font.Italic) this.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            else this.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold);




        }

        /// <summary>
        /// Affiche le texte au format standart si on sort du text box.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            if (!entered)
                return;


            entered = false;
            this.BorderStyle = BorderStyle.None;
            this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + 1);
            if (this.Font.Italic) this.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic);
            else this.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);

            base.OnLeave(e);
        }

        /// <summary>
        /// Si le contenu est validé, on l'affecte à <see cref="ControlledTextBox.LastValue"/>.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidated(EventArgs e)
        {
            LastValue = Text;
            //Debug.WriteLine("OnValidated");
            base.OnValidated(e);
        }

    }
}
