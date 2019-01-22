using System;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Test;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    /// <summary>
    /// Classe vue gérant un joueur.
    /// </summary>
    class JoueurTextBox : CustomHiddenTextBox
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
        /// Vrai si une erreur est détecté.
        /// </summary>
        protected bool errored = false;

        /// <summary>
        /// Evénement émit lorsque la valeur de <see cref="errored"/> change.
        /// </summary>
        public virtual event EventHandler ErroredChanged;










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
        public JoueurTextBox()
        {
            PlaceholdingColor = Theme.Style.JoueurBoxesPlaceHolderForeColor;
            BackColor = Theme.Style.JoueurBoxesBackColor;
            ForeColor = Theme.Style.JoueurBoxesForeColor;
            NormalColor = ForeColor;
            ErrorBackColor = Theme.Style.JoueurBoxesErrorBackColor;
            Font = Theme.Style.JoueurBoxesFont;
            NormalFont = Font;
            PlaceholdingFont = Theme.Style.JoueurBoxesPlaceHolderFont;

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
        /// Vrai si une erreur est détecté.
        /// </summary>
        public bool Errored { get { return errored; } }

        /// <summary>
        /// Référence du joueur géré.
        /// </summary>
        public override int Ref
        {
            get { return (int)(Tag ?? -1); }
            set
            {
                Tag = value;
                this.NewValue =
                    GJoueur.GetJoueur(Ref).GetType().GetProperty(Info).GetValue(GJoueur.GetJoueur(Ref)).ToString();
                Debug.WriteLine("NewValue de " + this.Info + " : " + this.LastValue);
                //Debug.WriteLine("Check Ref");
                CheckErrored();
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
        /// Vérifie s'il existe des erreurs.
        /// </summary>
        public void CheckErrored()
        {
            bool check = false;
            foreach (string[] s in CChesstion.GetJoueurErrors(Ref))
                if (s[0] == Info)
                {
                    check = true;
                    break;
                }
            if (Ref != -1 && check)
            {
                errored = true;
                BackColor = Theme.Style.JoueurBoxesErrorBackColor;
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                CChesstion.DisplayJoueurErrored(Ref);
            }
            else
            {
                errored = false;
                BackColor = Theme.Style.JoueurBoxesBackColor;
                BorderStyle = System.Windows.Forms.BorderStyle.None;
                CChesstion.DisplayJoueurErrored(Ref, false);
            }
            ErroredChanged?.Invoke(this, new EventArgs());
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
        /// Quand le texte est validé, on met à joueur l'objet joueur géré.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            if (Ref == -1)
                return;

            object value = LastValue;

            if (CChesstion.JoueurSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.JoueurSelectionne) is int)
                value = int.Parse(LastValue);
            else if (CChesstion.JoueurSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.JoueurSelectionne) is float)
                value = float.Parse(LastValue);
            else if (CChesstion.JoueurSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.JoueurSelectionne) is DateTime)
                value = Convert.ToDateTime(LastValue);

            CChesstion.JoueurSelectionne.GetType().GetProperty(Info).SetValue(CChesstion.JoueurSelectionne, value);

            CChesstion.JoueurErrorsCache.Remove(Ref);
            CheckErrored();

        }
    }
}
