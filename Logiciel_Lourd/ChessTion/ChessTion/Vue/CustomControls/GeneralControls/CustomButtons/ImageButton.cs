using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomButtons
{
    /// <summary>
    /// Classe vue gérant les boutons utilisant des images comme texture.
    /// </summary>
    class ImageButton : NoBorderButton
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
        /// Image lorsque le bouton est enabled.
        /// </summary>
        private Image enabledImage;

        /// <summary>
        /// Image lorsque le bouton n'est pas enabled.
        /// </summary>
        private Image disabledImage;

        /// <summary>
        /// Texte de la tool tip lorsque le bouton est enabled.
        /// </summary>
        private string enabledToolTipCaption = "";

        /// <summary>
        /// Texte de la tool tip lorsque le bouton n'est pas enabled.
        /// </summary>
        private string disabledToolTipCaption = "";



        /// <summary>
        /// Tool tip du bouton.
        /// </summary>
        protected ToolTip toolTip = new ToolTip();



        /// <summary>
        /// Image lorsque le bouton est enabled.
        /// </summary>
        public Image EnabledImage
        {
            get { return enabledImage; }
            set { enabledImage = value;
                CheckImage();
            }
        }

        /// <summary>
        /// Image lorsque le bouton n'est pas enabled.
        /// </summary>
        public Image DisabledImage
        {
            get { return disabledImage; }
            set { disabledImage = value;
                CheckImage();
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
        /// Contructeur.
        /// </summary>
        public ImageButton() : base()
        {
            toolTip.ShowAlways = true;
            toolTip.InitialDelay = 500;
            toolTip.AutoPopDelay = 5000;
            EnabledChanged += ImageButton_EnabledChanged;
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
        /// Texte de la tool tip lorsque le bouton est enabled.
        /// </summary>
        public string EnabledToolTipCaption
        {
            get { return enabledToolTipCaption; }
            set
            {
                enabledToolTipCaption = value;
                CheckImage();
            }
        }

        /// <summary>
        /// Texte de la tool tip lorsque le bouton n'est pas enabled.
        /// </summary>
        public string DisabledToolTipCaption
        {
            get { return disabledToolTipCaption; }
            set
            {
                disabledToolTipCaption = value;
                CheckImage();
            }
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
        /// Affiche l'image et le tool tip correspondant à l'état du bouton.
        /// </summary>
        protected virtual void CheckImage()
        {
            Image = Enabled ? EnabledImage : DisabledImage;
            toolTip.SetToolTip(this, Enabled ? EnabledToolTipCaption : DisabledToolTipCaption);
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
        /// Evènement appelé lorsque le bouton passe d'enabled à non enabled ou l'inverse : on <see cref="CheckImage"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageButton_EnabledChanged(object sender, EventArgs e)
        {
            CheckImage();
        }

        /// <summary>
        /// Evènement appelé lorsque le control est créé : on <see cref="CheckImage"/>.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CheckImage();
        }


    }
}
