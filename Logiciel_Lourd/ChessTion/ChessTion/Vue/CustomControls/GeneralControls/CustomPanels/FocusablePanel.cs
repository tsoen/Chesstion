using System;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomPanels
{
    /// <summary>
    /// Classe vue représentant un <see cref="Panel"/> que l'on peut focus.
    /// </summary>
    class FocusablePanel : Panel
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
        /// Vrai pour afficher la bordure lorsque le panneau est focus.
        /// </summary>
        public bool ShowBorderWhenFocused { get; set; }












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
        /// Affiche la bordure.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (ShowBorderWhenFocused)
                this.BorderStyle = BorderStyle.FixedSingle;
            //this.Location = new System.Drawing.Point(this.Location.X - 1, this.Location.Y - 1);
        }

        /// <summary>
        /// Masque la bordure.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.BorderStyle = BorderStyle.None;
            //this.Location = new System.Drawing.Point(this.Location.X + 1, this.Location.Y + 1);
        }

        /// <summary>
        /// Focus lorsque l'utilisateur clique.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.Focus();
        }
    }
}
