using System;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Test;
using ChessTion.Vue.CustomControls.GeneralControls.CustomComboBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes
{
    /// <summary>
    /// Classe vue gérant les combo box de l'application.
    /// </summary>
    class CustomComboBox : HiddenComboBox
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
        /// Vrai si une erreur est détectée.
        /// </summary>
        private bool errored = false;

        /// <summary>
        /// Evénement émit lorsque la valeur de <see cref="errored"/> change.
        /// </summary>
        public event EventHandler ErroredChanged;

        /// <summary>
        /// Vrai quand la combo box est en train de charger ses valeurs.
        /// </summary>
        protected bool Loading { get; set; } = false;

        /// <summary>
        /// Tool tip de la combo box.
        /// </summary>
        protected virtual ToolTip ToolTip { get; } = new ToolTip();

        /// <summary>
        /// Vrai si une erreur est détectée.
        /// </summary>
        public bool Errored
        {
            get { return errored; }
        }

        /// <summary>
        /// Attribut géré par la combo box.
        /// </summary>
        public string Info
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        /// <summary>
        /// Référence gérée par la combo box.
        /// </summary>
        public virtual int Ref
        {
            get
            {
                return (int) (Tag ?? -1);
            }
            set
            {
                Tag = value;
                if (value == -1)
                    return;

                Joueur j = GJoueur.GetJoueur(value);
                SelectedValue = j.GetType().GetProperty(Info).GetValue(j);
                CheckErrored();
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
        public CustomComboBox() : base()
        {
            BackColor = Theme.Style.JoueurBoxesBackColor;
            ForeColor = Theme.Style.JoueurBoxesForeColor;
            Font = Theme.Style.JoueurBoxesFont;
            DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public void CheckErrored()
        {
            string error = string.Empty;
            foreach (string[] s in CChesstion.GetJoueurErrors(Ref))
                if (s[0] == Info)
                {
                    error = s[1];
                    break;
                }
            Debug.WriteLine(Info + " CheckErrored() (Ref " + Ref + "; error " + error + ")");
            if (Ref != -1 && !error.Equals(string.Empty))
            {
                bool prev = errored;
                if (prev)
                    return;

                Debug.WriteLine(Info + " Errored = true");
                errored = true;
                BackColor = Theme.Style.JoueurBoxesErrorBackColor;
                label.BackColor = Theme.Style.JoueurBoxesErrorBackColor;
                label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ToolTip?.SetToolTip(this, "Actuel : " + SelectedValue.ToString() + " ; trouvé en base : " + error);
                ToolTip?.SetToolTip(label, "Actuel : " + SelectedValue.ToString() + " ; trouvé en base : " + error);
                ErroredChanged?.Invoke(this, new EventArgs());
            }
            else
            {
                Debug.WriteLine(Info + " No error starting (errored = " + errored + ")");
                bool prev = errored;
                if (!prev)
                    return;
                Debug.WriteLine(Info + " errored = false");
                ToolTip?.RemoveAll();
                errored = false;
                BackColor = Theme.Style.JoueurBoxesBackColor;
                label.BackColor = Theme.Style.JoueurBoxesBackColor;
                label.BorderStyle = System.Windows.Forms.BorderStyle.None;
                ErroredChanged?.Invoke(this, new EventArgs());
            }

            CChesstion.DisplayJoueurErrored(Ref, errored);
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);

            try { 

                if (Loading || Ref == -1) return;


                GJoueur.GetJoueur(Ref).GetType()
                    .GetProperty(this.Info)
                    .SetValue(GJoueur.GetJoueur(Ref), int.Parse(SelectedValue.ToString()));
                CChesstion.JoueurErrorsCache.Remove(Ref);
                CheckErrored();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("CustomComboBox ex : " + ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source);
            }
        }



    }
}
