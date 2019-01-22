using ChessTion.Controleur;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomButtons
{
    /// <summary>
    /// Classe vue gérant les boutons de suppression (les -).
    /// </summary>
    class DeleteButton : RoundImageButton
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
        public DeleteButton()
        {
            this.baseImage =
                System.Drawing.Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "DeleteEnabled.png");
            this.hoverImage =
                System.Drawing.Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "DeleteHover.png");
            this.clickImage =
                System.Drawing.Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "DeleteClick.png");

            Init(baseImage.Width);
        }

    }
}
