using System;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    /// <summary>
    /// Classe vue gérant une text box qui gère un open.
    /// </summary>
    class OpenTextBox : CustomHiddenTextBox
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
        /// Référence de l'open géré.
        /// </summary>
        public override int Ref
        {
            get
            {
                return base.Ref;
            }

            set
            {
                base.Ref = value;
                this.NewValue = GOpen.GetOpen(Ref).GetType().GetProperty(Info).GetValue(GOpen.GetOpen(Ref)).ToString();
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
        public OpenTextBox()
        {
            PlaceholdingColor = Theme.Style.OpenBoxesPlaceHolderForeColor;
            BackColor = Theme.Style.OpenBoxesBackColor;
            ForeColor = Theme.Style.OpenBoxesForeColor;
            NormalColor = ForeColor;
            ErrorBackColor = Theme.Style.OpenBoxesErrorBackColor;
            Font = Theme.Style.OpenBoxesFont;
            NormalFont = Font;
            PlaceholdingFont = Theme.Style.OpenBoxesPlaceHolderFont;
            TabStop = false;
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
        /// Quand validé, l'objet est mis à jour.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            object value = LastValue;

            if (CChesstion.OpenSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.OpenSelectionne) is int)
                value = int.Parse(LastValue);
            else if (CChesstion.OpenSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.OpenSelectionne) is float)
                value = float.Parse(LastValue);
            else if (CChesstion.OpenSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.OpenSelectionne) is DateTime)
                value = Convert.ToDateTime(LastValue);


            CChesstion.OpenSelectionne.GetType().GetProperty(Info).SetValue(CChesstion.OpenSelectionne, value);
        }
    }
}
