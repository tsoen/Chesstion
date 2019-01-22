using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomButtons
{
    /// <summary>
    /// Classe vue gérant des boutons ronds se servant d'images comme texture.
    /// </summary>
    class RoundImageButton : NoBorderButton
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
        /// Image de base.
        /// </summary>
        protected System.Drawing.Image baseImage;

        /// <summary>
        /// Image quand la souris passe au-dessus du bouton.
        /// </summary>
        protected System.Drawing.Image hoverImage;

        /// <summary>
        /// Image lorsque la souris clique sur le bouton.
        /// </summary>
        protected System.Drawing.Image clickImage;










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
        public RoundImageButton() { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="size">Taille du bouton.</param>
        /// <param name="baseImage">Image de base.</param>
        /// <param name="hoverImage">Image quand la souris passe au-dessus du bouton.</param>
        /// <param name="clickImage">Image lorsque la souris clique sur le bouton.</param>
        public RoundImageButton(int size, System.Drawing.Image baseImage, System.Drawing.Image hoverImage, System.Drawing.Image clickImage)
        {
            this.baseImage = baseImage;
            this.hoverImage = hoverImage;
            this.clickImage = clickImage;

            Init(size);
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
        /// On attribut un texte nul (l'image remplaçant le texte) et on empêche de le modifier.
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = "";
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
        /// Initialise le bouton.
        /// </summary>
        /// <param name="imageSize">T'aille des images.</param>
        public void Init(int imageSize)
        {
            Image = baseImage;
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            Size = new System.Drawing.Size(imageSize, imageSize);
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
        /// On dessine un bouton rond à chaque fois que le bouton est peint.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }

        /// <summary>
        /// Lorsque la souris passe sur le bouton, on dessine <see cref="hoverImage"/>.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            Image = hoverImage;
            // base.OnMouseEnter(e);
        }

        /// <summary>
        /// Lorsque la souris quitte le bouton, on dessine <see cref="baseImage"/>.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            Image = baseImage;
            // base.OnMouseLeave(e);
        }

        /// <summary>
        /// Lorsque la souris clique sur le bouton, on dessine <see cref="clickImage"/>.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            Image = clickImage;
            // base.OnMouseDown(mevent);
        }

        /// <summary>
        /// Lorsque la souris relache le clic, on dessine <see cref="hoverImage"/> sauf si la souris ne se trouve plus sur le bouton, dans quel cas on dessine <see cref="baseImage"/>.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            Image = hoverImage;
            //base.OnMouseUp(mevent);
            if (ClientRectangle.Contains(PointToClient(Control.MousePosition)))
            {
                this.PerformClick();
            }
        }

    }
}
