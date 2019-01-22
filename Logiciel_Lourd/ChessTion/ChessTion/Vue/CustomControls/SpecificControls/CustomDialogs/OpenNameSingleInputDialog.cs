using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant la pop-up de (re)nommage d'un <see cref="Open"/>.
    /// </summary>
    class OpenNameSingleInputDialog : SingleInputDialog
    {










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
        /// <param name="previousName">Ancien nom de l'open.</param>
        public OpenNameSingleInputDialog(string previousName = "") : base("Nommer l'Open", "Veuillez entrer le nom de l'Open.")
        {
            this.Input = previousName;
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
        /// Applique le thème aux controls.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Theme.Style.DialogBodyBackColor;
            this.ForeColor = Theme.Style.DialogBodyForeColor;
            this.Font = Theme.Style.DialogBodyFont;

            this.TextBox.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TextBox.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TextBox.Font = Theme.Style.DialogTextBoxFont;

            this.Label.ForeColor = Theme.Style.DialogBodyForeColor;
            this.Label.Font = Theme.Style.DialogBodyFont;

            this.OKButton.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.OKButton.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.OKButton.Font = Theme.Style.DialogMainButtonsFont;

            this.CancelButton.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.CancelButton.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.CancelButton.Font = Theme.Style.DialogSecondaryButtonsFont;

        }
    }
}
