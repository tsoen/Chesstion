using ChessTion.Controleur;
using System;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomButtons
{
    /// <summary>
    /// Classe vue gérant les boutons d'ajout (les +).
    /// </summary>
    class AddButton : RoundImageButton
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
        public AddButton()
        {
            this.baseImage =
                System.Drawing.Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "AddEnabled.png");
            this.hoverImage =
                System.Drawing.Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "AddHover.png");
            this.clickImage =
                System.Drawing.Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "AddClick.png");

            Init(baseImage.Width);
        }


    }
}
