using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs
{
    /// <summary>
    /// Représente le type de la pop-up.
    /// </summary>
    public enum QuickDialogType
    {
        /// <summary>
        /// Type information.
        /// </summary>
        Info,

        /// <summary>
        /// Type de réussite.
        /// </summary>
        Success,

        /// <summary>
        /// Type attention.
        /// </summary>
        Warning,

        /// <summary>
        /// Type erreur.
        /// </summary>
        Error
    }

    /// <summary>
    /// Représente la position relative au control parent.
    /// </summary>
    public enum QuickDialogRelativeStartPosition
    {
        /// <summary>
        /// Centre sur le parent.
        /// </summary>
        CenterOnParent,

        /// <summary>
        /// Centre au-dessus du parent.
        /// </summary>
        CenterOverParent,

        /// <summary>
        /// Centre sous le parent.
        /// </summary>
        CenterBelowParent,

        /// <summary>
        /// Centre à droite du parent.
        /// </summary>
        CenterOnRightOfParent,

        /// <summary>
        /// Centre à gauche du parent.
        /// </summary>
        CenterOnLeftOfParent,

        /// <summary>
        /// Aucune position par défaut.
        /// </summary>
        Manual
    }

    /// <summary>
    /// Classe vue gérant une pop-up rapide d'info.
    /// </summary>
    class QuickDialog : Form
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
        /// Label du message de la pop-up.
        /// </summary>
        protected Label Label;

        /// <summary>
        /// Type de la pop-up.
        /// </summary>
        protected QuickDialogType dialogType = QuickDialogType.Info;

        /// <summary>
        /// Position d'affichage de la pop-up.
        /// </summary>
        protected QuickDialogRelativeStartPosition relativeStartPosition = QuickDialogRelativeStartPosition.Manual;

        /// <summary>
        /// Contrôle parent de la pop-up (servant à l'affichage à une position relative à un contrôle).
        /// </summary>
        protected Control parent;

        /// <summary>
        /// Timer servant à fermer automatiquement la pop-up.
        /// </summary>
        protected Timer timer = new Timer();



        /// <summary>
        /// Couleur arrière-plan pour le type Info.
        /// </summary>
        public static Color InfoBackColor { get; set; } = Color.DarkBlue;

        /// <summary>
        /// Couleur du texte pour le type info.
        /// </summary>
        public static Color InfoForeColor { get; set; } = Color.White;

        /// <summary>
        /// Couleur arrière-plan pour le type Success.
        /// </summary>
        public static Color SuccessBackColor { get; set; } = Color.DarkGreen;

        /// <summary>
        /// Couleur du texte pour le type Success.
        /// </summary>
        public static Color SuccessForeColor { get; set; } = Color.White;

        /// <summary>
        /// Couleur arrière-plan pour le type Warning.
        /// </summary>
        public static Color WarningBackColor { get; set; } = Color.Yellow;

        /// <summary>
        /// Couleur du texte pour le type Warning.
        /// </summary>
        public static Color WarningForeColor { get; set; } = Color.Black;

        /// <summary>
        /// Couleur arrière-plan pour le type Error.
        /// </summary>
        public static Color ErrorBackColor { get; set; } = Color.DarkRed;

        /// <summary>
        /// Couleur du texte pour le type Error.
        /// </summary>
        public static Color ErrorForeColor { get; set; } = Color.White;



        /// <summary>
        /// Delai (en milliseconds) après lequel la pop-up se ferme automatiquement.
        /// </summary>
        public int DisplayDelay { get; set; } = 0;

        /// <summary>
        /// Vrai laisse la possibilité à l'utilisateur de fermer la pop-up en cliquant dessus.
        /// </summary>
        public bool Closable { get; set; } = true;



        /// <summary>
        /// Si cet attribut ne vaut pas (0;0), la position de départ sera décallée d'autant.
        /// </summary>
        public Point Shift { get; set; } = new Point();










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
        public QuickDialog() : this("Info !", QuickDialogType.Info, null)
        {}

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de la pop-up.</param>
        /// <param name="dialogType">Type de la pop-up.</param>
        /// <param name="parent">Control parent de la pop-up.</param>
        public QuickDialog(string message, QuickDialogType dialogType, Control parent) : this (message, dialogType, parent, new Point()) { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de la pop-up.</param>
        /// <param name="dialogType">Type de la pop-up.</param>
        /// <param name="parent">Control parent de la pop-up.</param>
        /// <param name="relativeStartPosition">Position relative au parent de la pop-up.</param>
        public QuickDialog(string message, QuickDialogType dialogType, Control parent, QuickDialogRelativeStartPosition relativeStartPosition) : this(message, dialogType, parent, new Point(), relativeStartPosition) { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de la pop-up.</param>
        /// <param name="dialogType">Type de la pop-up.</param>
        /// <param name="parent">Control parent de la pop-up.</param>
        /// <param name="relativeShift">Déplacement par rapport à <see cref="relativeStartPosition"/>.</param>
        /// <param name="relativeStartPosition">Position relative au parent de la pop-up.</param>
        public QuickDialog(string message, QuickDialogType dialogType, Control parent,
            Point relativeShift, QuickDialogRelativeStartPosition relativeStartPosition = QuickDialogRelativeStartPosition.Manual)
        {
            InitializeComponent();
            Message = message;
            DialogType = dialogType;
            StartPosition = FormStartPosition.Manual;
            Parent = parent;
            RelativeStartPosition = relativeStartPosition;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Shift = relativeShift;


            AddEvents();
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
        /// Message de la pop-up.
        /// </summary>
        public string Message
        {
            get { return Label.Text; }
            set
            {
                Label.Text = value;
            }
        }

        /// <summary>
        /// Type de la pop-up.
        /// </summary>
        public QuickDialogType DialogType
        {
            get { return dialogType; }
            set
            {
                dialogType = value;
                if (dialogType == QuickDialogType.Info)
                {
                    BackColor = InfoBackColor;
                    ForeColor = InfoForeColor;
                }
                else if (dialogType == QuickDialogType.Success)
                {
                    BackColor = SuccessBackColor;
                    ForeColor = SuccessForeColor;
                }
                else if (dialogType == QuickDialogType.Warning)
                {
                    BackColor = WarningBackColor;
                    ForeColor = WarningForeColor;
                }
                else if (dialogType == QuickDialogType.Error)
                {
                    BackColor = ErrorBackColor;
                    ForeColor = ErrorForeColor;
                }
            }
        }

        /// <summary>
        /// Position relative au parent de a pop-up.
        /// </summary>
        public QuickDialogRelativeStartPosition RelativeStartPosition
        {
            get { return relativeStartPosition; }
            set
            {
                relativeStartPosition = value;
                if (this.Created)
                    RefreshLocation();
            }
        }

        /// <summary>
        /// Contrôle parent de la pop-up (servant à l'affichage à une position relative à un contrôle).
        /// </summary>
        public new Control Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                if (this.Created)
                    RefreshLocation();
            }
        }

        /// <summary>
        /// Taille maximum du texte (permet de limite la taille du pop-up).
        /// </summary>
        public Size MaximumTextSize
        {
            get { return Label.MaximumSize; }
            set { Label.MaximumSize = value; }
        }

        /// <summary>
        /// Retourne la position absolue de la pop-up.
        /// </summary>
        public Point AbsoluteLocation
        {
            get
            {
                if (Parent != null)
                    return new Point(Parent.PointToScreen(Point.Empty).X,
                        Parent.PointToScreen(Point.Empty).Y);

                return new Point();
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
        /// Ajoute les évènements à la pop-up.
        /// </summary>
        private void AddEvents()
        {
            Click += QuickDialog_Click;
            Label.Click += QuickDialog_Click;
            Label.Resize += Label_Resize;
            Shown += QuickDialog_Shown;
        }

        /// <summary>
        /// Actualise la position de la pop-up.
        /// </summary>
        protected void RefreshLocation()
        {
            Point relativeStartPositionPoint = new Point();

            switch (RelativeStartPosition)
            {
                case QuickDialogRelativeStartPosition.CenterOverParent:
                    relativeStartPositionPoint = new Point((Parent.Size.Width - this.Size.Width) / 2,
                        - this.Size.Height);
                    break;
                case QuickDialogRelativeStartPosition.CenterBelowParent:
                    relativeStartPositionPoint = new Point((Parent.Size.Width - this.Size.Width) / 2,
                        Parent.Size.Height);
                    break;
                case QuickDialogRelativeStartPosition.CenterOnParent:
                    relativeStartPositionPoint = new Point((Parent.Size.Width - this.Size.Width) / 2,
                        (Parent.Size.Height - this.Size.Height) / 2);
                    break;
                case QuickDialogRelativeStartPosition.CenterOnLeftOfParent:
                    relativeStartPositionPoint = new Point(-this.Size.Width,
                        (Parent.Size.Height - this.Size.Height) / 2);
                    break;
                case QuickDialogRelativeStartPosition.CenterOnRightOfParent:
                    relativeStartPositionPoint = new Point(parent.Size.Width,
                        (Parent.Size.Height - this.Size.Height) / 2);
                    break;
            }
           

            Location = new Point(AbsoluteLocation.X + relativeStartPositionPoint.X + Shift.X, AbsoluteLocation.Y + relativeStartPositionPoint.Y + Shift.Y);
            this.TopMost = true;
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
        /// Si le label est redimensionné, redimensionne la pop-up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Resize(object sender, EventArgs e)
        {
            if (this.Size != Label.Size)
                this.Size = Label.Size;
        }

        /// <summary>
        /// Ferme la pop-up quand l'utilisateur clique (si autorisé par <see cref="Closable"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickDialog_Click(object sender, EventArgs e)
        {
            if (Closable)
                this.Close();
        }

        /// <summary>
        /// Si la pop-up est redimensionnée, redimensionne également le Label.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Size != Label.Size)
                this.Size = Label.Size;
            if (this.Created)
                RefreshLocation();
        }

        /// <summary>
        /// Actualise la position lorsque le control est créé.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            RefreshLocation();
        }

        /// <summary>
        /// Lance le timer qui fermera automatiquement la pop-up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickDialog_Shown(object sender, EventArgs e)
        {
            if (DisplayDelay > 0)
            {
                timer.Interval = DisplayDelay;
                timer.Enabled = true;
                timer.Tick += Timer_Tick;
                timer.Start();
            }

            Parent.Focus();
        }

        /// <summary>
        /// Evite au pop-up d'être focused.
        /// </summary>
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        /// <summary>
        /// Ferme la pop-up lorsque le timer s'est terminé.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Crée les controls.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(0, 0);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(0, 17);
            this.Label.TabIndex = 0;
            // 
            // QuickDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Name = "QuickDialog";
            this.Opacity = 0.9D;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
