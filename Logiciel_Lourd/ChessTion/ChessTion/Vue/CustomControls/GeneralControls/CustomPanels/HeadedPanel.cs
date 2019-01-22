using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomPanels
{
    /// <summary>
    /// Classe vue représentant un panel avec un titre.
    /// </summary>
    class HeadedPanel : FocusablePanel
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
        /// Label représentant le titre.
        /// </summary>
        protected Label header = new Label();

        /// <summary>
        /// Texte du titre.
        /// </summary>
        public string Title
        {
            get { return header.Text; }
            set { header.Text = value;  }
        }

        /// <summary>
        /// Couleur d'arrière plan du titre.
        /// </summary>
        public Color HeaderBackColor
        {
            get { return header.BackColor; }
            set { header.BackColor = value; }
        }

        /// <summary>
        /// Couleur du texte du titre.
        /// </summary>
        public Color HeaderForeColor
        {
            get { return header.ForeColor; }
            set { header.ForeColor = value; }
        }

        /// <summary>
        /// Police du titre.
        /// </summary>
        public Font HeaderFont
        {
            get { return header.Font; }
            set { header.Font = value; }
        }

        /// <summary>
        /// Taille (en hauteur) du titre.
        /// </summary>
        public int HeaderHeight
        {
            get { return header.Height; }
            set { header.Height = value; header.TextAlign = ContentAlignment.MiddleCenter; }
        }

        /// <summary>
        /// Taille du panneau sans le titre.
        /// </summary>
        public Size InnerSize
        {
            get { return new Size(this.Width, this.Height - this.HeaderHeight); }
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
        public HeadedPanel() : this("Title", 32) { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="title">Texte du titre.</param>
        public HeadedPanel(string title) : this(title, 32) { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="title">Texte du titre.</param>
        /// <param name="headerHeight">Taille (hauteur) du titre.</param>
        public HeadedPanel(string title, int headerHeight)
        {
            this.Controls.Add(header);
            header.Location = new Point(0, 0);
            header.AutoSize = false;
            header.TextAlign = ContentAlignment.MiddleCenter;
            header.Font = new Font(header.Font, FontStyle.Bold);
            header.Text = title;
            header.Height = headerHeight;
            header.Width = this.Width;
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
        /// Lorsque le panneau est redimensionné, redimensionne le titre.
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            header.Width = this.Width;
        }

    }
}
