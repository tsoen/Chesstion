using System;
using ChessTion.Vue.CustomControls.GeneralControls.CustomButtons;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomPanels
{
    /// <summary>
    /// Classe vue représentant un <see cref="HeadedPanel"/> qui intègre deux boutons d'ajout et de suppression.
    /// </summary>
    class AddDeletePanel : HeadedPanel
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
        /// Bouton d'ajout.
        /// </summary>
        protected AddButton AddButton = new AddButton();

        /// <summary>
        /// Bouton de suppression.
        /// </summary>
        protected DeleteButton DeleteButton = new DeleteButton();



        /// <summary>
        /// Evènement déclangé quand le bouton d'ajout est cliqué.
        /// </summary>
        public event EventHandler AddButtonClicked;

        /// <summary>
        /// Evènement déclangé quand le bouton de suppression est cliqué.
        /// </summary>
        public event EventHandler DeleteButtonClicked;

        /// <summary>
        /// Distance des boutons du bord du panel.
        /// </summary>
        private static readonly int distanceFromTheSides = 10;










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
        public AddDeletePanel()
        {
            AllowAdd = true;
            AllowDelete = true;

            Controls.Add(AddButton);
            Controls.Add(DeleteButton);

            AddButton.Click += new EventHandler((object sender, EventArgs e) => { AddButtonClicked?.Invoke(AddButton, e); });
            DeleteButton.Click += new EventHandler((object sender, EventArgs e) => { DeleteButtonClicked?.Invoke(DeleteButton, e); });
        }

        /// <summary>
        /// Active ou désactive le bouton d'ajout.
        /// </summary>
        public bool AllowAdd
        {
            get { return AddButton.Visible; }
            set { AddButton.Visible = value; }
        }

        /// <summary>
        /// Active ou désactive le bouton de suppression.
        /// </summary>
        public bool AllowDelete
        {
            get { return DeleteButton.Visible; }
            set { DeleteButton.Visible = value; }
        }

        /// <summary>
        /// Recalcule la position des boutons.
        /// </summary>
        protected virtual void RelocateButtons()
        {
            DeleteButton.Location = new System.Drawing.Point(Size.Width - distanceFromTheSides - DeleteButton.Size.Width, Size.Height - distanceFromTheSides - DeleteButton.Size.Height);
            AddButton.Location = new System.Drawing.Point(distanceFromTheSides, Size.Height - distanceFromTheSides - AddButton.Size.Height);
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
        /// Lorsque le control est créé, calcule la position des boutons.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            RelocateButtons();
        }

    }
}
