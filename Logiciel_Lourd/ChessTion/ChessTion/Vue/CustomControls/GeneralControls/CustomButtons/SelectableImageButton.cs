using System;
using System.Drawing;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomButtons
{
    /// <summary>
    /// Classe vue gérant un bouton se servant d'image qui peut être selectionnable.
    /// </summary>
    class SelectableImageButton : ImageButton
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
        /// Image du bouton lorsque celui-ci est sélectionné.
        /// </summary>
        private Image selectedImage;

        /// <summary>
        /// Texte du tool tip du bouton lorsque celui-ci est sélectionné.
        /// </summary>
        private string selectedToolTipCaption = "";

        /// <summary>
        /// Evènement appelé lorsque l'état de la sélection à changé.
        /// </summary>
        public event EventHandler SelectedChanged;










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
        /// Image du bouton lorsque celui-ci est sélectionné.
        /// </summary>
        public Image SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value;
                CheckImage();
            }
        }

        /// <summary>
        /// Texte du tool tip lorsque le bouton est sélectionné.
        /// </summary>
        public string SelectedToolTipCaption
        {
            get { return selectedToolTipCaption; } 
            set {
                selectedToolTipCaption = value;
                CheckImage();
            }
        }

        /// <summary>
        /// Vrai si le bouton est sélectionné, non sinon.
        /// </summary>
        public bool Selected { get; set; }










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
        /// Change l'image et le tool tip en fonction de l'état de sélection du bouton.
        /// </summary>
        protected override void CheckImage()
        {
            base.CheckImage();
            if (Enabled && Selected)
            {
                Image = SelectedImage;
                toolTip.SetToolTip(this, SelectedToolTipCaption);
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
        /// Lorsque l'on clique, on sélectionne le bouton (s'il est activé).
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled)
            {
                Selected = !Selected;

                CheckImage();


                SelectedChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
