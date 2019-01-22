using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes
{
    /// <summary>
    /// Classe vue représentant une <see cref="TextBox"/> qui contient un texte de placeholder.
    /// </summary>
    public class PlaceHolderTextBox : TextBox
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
        /// Vrai si le texte de placeholder est affiché.
        /// </summary>
        private bool isPlaceHolding = true;

        /// <summary>
        /// Evènement émit lorsque l'état <see cref="isPlaceHolding"/> a changé.
        /// </summary>
        public event EventHandler IsPlaceHoldingChanged;










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
        /// Texte du placeholder.
        /// </summary>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// Couleur du texte de placeholder.
        /// </summary>
        public Color PlaceholdingColor { get; set; }

        /// <summary>
        /// Police du texte du placeholder.
        /// </summary>
        public Font PlaceholdingFont { get; set; }

        /// <summary>
        /// Couleur du texte normal du champ.
        /// </summary>
        public Color NormalColor { get; set; }

        /// <summary>
        /// Police du texte normal du champ.
        /// </summary>
        public Font NormalFont { get; set; }

        /// <summary>
        /// Vrai si le texte de placeholder est affiché.
        /// </summary>
        public bool IsPlaceHolding { get { return isPlaceHolding; } }









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
        /// Vérifie l'état du champ et affiche ou masque le placeholder si nécessaire.
        /// </summary>
        /// <param name="gotFocus">Vrai si le champ vient d'être focused.</param>
        public void CheckPlaceHolder(bool gotFocus)
        {
            if (!gotFocus && this.Text == string.Empty)
            {
                this.Text = this.PlaceholderText;
                //System.Diagnostics.Debug.WriteLine("LostFocus - String empty (text now equals '" + this.Text + "')");
                isPlaceHolding = true;
                IsPlaceHoldingChanged?.Invoke(this, new EventArgs());
                ForeColor = PlaceholdingColor;
                Font = PlaceholdingFont;
                //System.Diagnostics.Debug.WriteLine("ForeColor = PlaceholdingColor; (" + ForeColor.ToString() + ")");

            }
            else if (gotFocus && this.Text == this.PlaceholderText && this.IsPlaceHolding && !ReadOnly)
            {
                //System.Diagnostics.Debug.WriteLine("GotFocus - String == placeHolder && isPlaceHolding");
                this.Text = string.Empty;
                isPlaceHolding = false;
                IsPlaceHoldingChanged?.Invoke(this, new EventArgs());
                ForeColor = NormalColor;
                Font = NormalFont;
                //System.Diagnostics.Debug.WriteLine("ForeColor = NormalColor; (" + ForeColor.ToString() + ")");
            }
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
        /// Quand on quitte le champ, affiche le placeholder.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            CheckPlaceHolder(false);
        }

        /// <summary>
        /// Quand on rentre dans le champ, masque le placeholder.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            CheckPlaceHolder(true);
        }


    }
}
