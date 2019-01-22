using System.Drawing;
using System.Windows.Forms;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up rapide d'info.
    /// </summary>
    class CustomQuickDialog : QuickDialog
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
        public CustomQuickDialog() : base()
        {
            CustomizeColors();
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de la pop-up.</param>
        /// <param name="dialogType">Type de la pop-up.</param>
        /// <param name="parent">Control parent de la pop-up.</param>
        public CustomQuickDialog(string message, QuickDialogType dialogType, Control parent) : base(message, dialogType, parent)
        {
            CustomizeColors();
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de la pop-up.</param>
        /// <param name="dialogType">Type de la pop-up.</param>
        /// <param name="parent">Control parent de la pop-up.</param>
        /// <param name="relativeStartPosition">Position relative au parent de la pop-up.</param>
        public CustomQuickDialog(string message, QuickDialogType dialogType, Control parent, QuickDialogRelativeStartPosition relativeStartPosition) : base(message, dialogType, parent, relativeStartPosition)
        {
            CustomizeColors();
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de la pop-up.</param>
        /// <param name="dialogType">Type de la pop-up.</param>
        /// <param name="parent">Control parent de la pop-up.</param>
        /// <param name="relativeShift">Déplacement par rapport à <see cref="QuickDialog.RelativeStartPosition"/>.</param>
        /// <param name="relativeStartPosition">Position relative au parent de la pop-up.</param>
        public CustomQuickDialog(string message, QuickDialogType dialogType, Control parent, Point relativeShift, QuickDialogRelativeStartPosition relativeStartPosition = QuickDialogRelativeStartPosition.Manual) : base(message, dialogType, parent, relativeShift, relativeStartPosition)
        {
            CustomizeColors();
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
        /// Applique le thème aux controls.
        /// </summary>
        private void CustomizeColors()
        {
            Label.Font = Theme.Style.QuickDialogFont;

            InfoBackColor = Theme.Style.QuickDialogInfoBackColor;
            InfoForeColor = Theme.Style.QuickDialogInfoForeColor;

            SuccessBackColor = Theme.Style.QuickDialogSuccessBackColor;
            SuccessForeColor = Theme.Style.QuickDialogSuccessForeColor;

            WarningBackColor = Theme.Style.QuickDialogWarningBackColor;
            WarningForeColor = Theme.Style.QuickDialogWarningForeColor;

            ErrorBackColor = Theme.Style.QuickDialogErrorBackColor;
            ErrorForeColor = Theme.Style.QuickDialogErrorForeColor;

            DialogType = DialogType;

            Label.TextAlign = ContentAlignment.MiddleCenter;
        }

    }
}
