using System;
using System.Windows.Forms;
using ChessTion.Utilitaires;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomComboBoxes
{
    /// <summary>
    /// Classe vue gérant des combo box qui masque leur bordure quand elles ne sont pas utilisées.
    /// Cette classe affiche en fait un label à la place de la combo box et ne fait appraître celle-ci que lorsque l'on clique sur le label.
    /// </summary>
    class HiddenComboBox : ComboBox
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
        /// Label affiché lorsque la combo box est masquée.
        /// </summary>
        protected Label label = new Label();

        /// <summary>
        /// Propriété de l'objet à afficher dans le label.
        /// </summary>
        public string AttributeToDisplay { set; get; }









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
        public HiddenComboBox() : base()
        {
            this.FlatStyle = FlatStyle.Flat;
            label.AutoSize = false;
            this.Visible = false;
            label.Visible = true;

            label.Click += Label_Click;
            label.Enter += Label_Enter;
            label.LocationChanged += Label_LocationChanged;
            this.DropDownClosed += HiddenComboBox_DropDownClosed;
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
        /// Met à jour le texte du label.
        /// </summary>
        protected void RefreshLabelText()
        {
            if (this.SelectedItem != null)
            {
                try
                {
                    label.Text = SelectedItem.GetType().GetProperty(AttributeToDisplay).GetValue(SelectedItem).ToString();
                }
                catch
                {
                    label.Text = this.SelectedItem.ToString();
                }

            }
        }

        /// <summary>
        /// Affiche ou non le label.
        /// </summary>
        /// <param name="value"></param>
        public void SetLabelVisible(bool value = true)
        {
            label.Visible = value;
            this.Visible = !value;

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
        /// Met à jour la position du label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_LocationChanged(object sender, EventArgs e)
        {
            if (label.Location != new System.Drawing.Point(this.Location.X + 1, this.Location.Y + 4))
                label.Location = new System.Drawing.Point(this.Location.X + 1, this.Location.Y + 4);
        }

        /// <summary>
        /// Affiche la label quand la combo box est fermée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiddenComboBox_DropDownClosed(object sender, EventArgs e)
        {
            SetLabelVisible(true);
        }

        /// <summary>
        /// Masque le label TAB dessus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Enter(object sender, EventArgs e)
        {
            if (this.Enabled)
                SetLabelVisible(false);
            //System.Diagnostics.Debug.WriteLine("LABEL ENETER This " + this.Location + " label " + label.Location);
        }

        /// <summary>
        /// Masque le label quand on clique dessus et affiche la combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Click(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                SetLabelVisible(false);
                this.DroppedDown = true;
            }
            //System.Diagnostics.Debug.WriteLine("CLICK This " + this.Location + " label " + label.Location);
            //System.Diagnostics.Debug.WriteLine("CLICK This " + this.Size + " label " + label.Size);

        }

        /// <summary>
        /// Met à jour le texte du label quand la sélection de la combo box a changé.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            RefreshLabelText();
        }

        /// <summary>
        /// Met le focus sur la combo box quand celle-ci est ouverte.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            this.Focus();
        }

        /// <summary>
        /// Quand le parent de la combo box change, met à jour le parent du label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent is Panel)
            {
                Panel p = (Panel)Parent;
                p.Controls.Add(label);
            }

            //System.Diagnostics.Debug.WriteLine("PARENT CHANGED This " + this.Location + " label " + label.Location);

        }

        /// <summary>
        /// Quand la combo box bouge, bouge aussi le label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            label.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + (int)(DPI.Instance.RelativeMultiplier.Y * 4));
            //System.Diagnostics.Debug.WriteLine("LOCATION CHANGED This " + this.Location + " label " + label.Location);
        }

        /// <summary>
        /// Quand la couleur d'arrière plan de la combo box change, change aussi celle du label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            label.BackColor = this.BackColor;
        }

        /// <summary>
        /// Quand la couleur du texte de la combo box change, change aussi celle du label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            label.ForeColor = this.ForeColor;
        }

        /// <summary>
        /// Quand on quitte la combo box, affiche le label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            SetLabelVisible();
        }

        /// <summary>
        /// Quand la taille de la combo box change, change aussi celle du label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            label.Size = this.Size;
        }

        /// <summary>
        /// Quand la police de la combo box change, change aussi celle du label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            label.Font = this.Font;
        }

        /// <summary>
        /// Quand la data source change, affiche le label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataSourceChanged(EventArgs e)
        {
            try
            {
                base.OnDataSourceChanged(e);
            } catch { }

            SetLabelVisible();
        }
    }
}
