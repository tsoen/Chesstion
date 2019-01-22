using System;
using System.Windows.Forms;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomButtons;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomButtons
{
    /// <summary>
    /// Classe gérant les boutons de <see cref="OpensPanel"/>.
    /// </summary>
    class OpensMenuButton : NoBorderButton
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
        /// Evénement émis lorsque l'open est sélectionné.
        /// </summary>
        public event EventHandler JustSelected;

        /// <summary>
        /// Evénement émis lorsque l'open est déselectionné.
        /// </summary>
        public event EventHandler JustUnselected;

        /// <summary>
        /// Vrai si l'open est sélectionné.
        /// </summary>
        private bool selected = false;

        /// <summary>
        /// Référence de l'open.
        /// </summary>
        public int Ref
        {
            get { return (int)Tag; }
            set { Tag = value; }
        }

        /// <summary>
        /// Vrai si l'open est sélectionné.
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (selected)
                    JustSelected?.Invoke(this, new EventArgs());
                else
                    JustUnselected?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }










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
        public OpensMenuButton() : base()
        {
            base.OnCreateControl();
            this.BackColor = Theme.Style.OpensButtonsBackColor;
            this.Font = Theme.Style.OpensButtonsFont;
            this.ForeColor = Theme.Style.OpensButtonsForeColor; 
            this.Margin = new Padding(2);
            this.Size = DPI.Instance.MultipliedSize(200, 40);
            this.JustSelected += OpensMenuButton_JustSelected;
            this.JustUnselected += OpensMenuButton_JustUnselected;
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
        /// Applique le thème au bouton sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpensMenuButton_JustSelected(object sender, EventArgs e)
        {
            this.BackColor = Theme.Style.OpensSelectedButtonBackColor;
            this.Font = Theme.Style.OpensSelectedButtonFont;
            this.ForeColor = Theme.Style.OpensSelectedButtonForeColor;
        }

        /// <summary>
        /// Applique le thème au bouton non selectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpensMenuButton_JustUnselected(object sender, EventArgs e)
        {
            this.BackColor = Theme.Style.OpensButtonsBackColor;
            this.Font = Theme.Style.OpensButtonsFont; 
            this.ForeColor = Theme.Style.OpensButtonsForeColor;
        }

    }
}
