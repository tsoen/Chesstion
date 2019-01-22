using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomButtons;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;
using ChessTion.Vue.CustomControls.SpecificControls.CustomLabels;
using System.Linq;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using ChessTion.Vue.CustomControls.SpecificControls.CustomMenus;
using Debug = ChessTion.Test.Debug;
using Panel = System.Windows.Forms.Panel;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    /// <summary>
    /// Classe vue gérant la panneau central.
    /// </summary>
    class CentrePanel : AddDeletePanel, IChesstionPanel
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


        private static readonly Point spacesBetweenNames = DPI.Instance.MultipliedPoint(300, 24);
        private CustomQuickDialog confirmDeleteDialog;
        private CustomQuickDialog errorDeleteDialog;
        private AddPlayerFromFFEDialog addPlayerFromFFEDialog;
        private AddPlayerDialog addPlayerDialog;
        private static readonly int panelStartingX = 30;
        private int numberOfCols = 0;


        public Panel Panel { get; } = new Panel();
        protected ImageButton LeftButton { get; } = new ImageButton();
        protected ImageButton RightButton { get; } = new ImageButton();
        protected SelectableImageButton VisibleButton { get; } = new SelectableImageButton();
        protected SelectableImageButton SortByNameButton { get; } = new SelectableImageButton();
        protected Label SearchLabel { get; } = new Label();
        protected NoBorderButton StatusCloseButton { get; } = new NoBorderButton();
        protected HiddenTextBox SearchTextBox { get; } = new HiddenTextBox();

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
        public CentrePanel() : base()
        {
            AllowAdd = true;
            AllowDelete = true;

            MouseWheel += CentrePanel_MouseWheel;
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
        /// Espace entre les noms des joueurs.
        /// </summary>
        public static Point SpacesBetweenNames
        {
            get { return spacesBetweenNames; }
        }

        /// <summary>
        /// Ensemble des labels joueurs du panneau.
        /// </summary>
        public List<CentreJoueurLabel> Labels
        {
            get
            {
                List<CentreJoueurLabel> labels = new List<CentreJoueurLabel>();

                foreach (Control c in Panel.Controls)
                    if (c is CentreJoueurLabel)
                        labels.Add((CentreJoueurLabel)c);

                return labels;
            }
        }

        /// <summary>
        /// Label joueur sélectionné sur le panneau.
        /// </summary>
        public CentreJoueurLabel LabelSelectionne
        {
            get
            {
                foreach (CentreJoueurLabel c in Labels)
                    if (c.Selected)
                        return c;

                return null;
            }
        }

        /// <summary>
        /// Retourne le label représentant le joueur avec la réf donnée.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <returns></returns>
        public CentreJoueurLabel GetLabel(int reference)
        {
            if (LabelSelectionne != null && LabelSelectionne.Ref == reference)
                return LabelSelectionne;

            foreach (CentreJoueurLabel cjl in Labels)
                if (cjl.Ref == reference)
                    return cjl;

            return null;
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
        /// Initialise les controls.
        /// </summary>
        public void Init()
        {
            BackColor = Theme.Style.CentreBodyBackColor;
            HeaderBackColor = Theme.Style.CentreHeaderBackColor;
            HeaderFont = Theme.Style.CentreHeaderFont;
            HeaderForeColor = Theme.Style.CentreHeaderForeColor;
            HeaderHeight = (int)(DPI.Instance.RelativeMultiplier.Y * Theme.Style.CentreHeaderHeight);
            DeleteButtonClicked += CentrePanel_DeleteButtonClicked;

            Panel.BackColor = System.Drawing.Color.Transparent;

            Controls.Add(Panel);
            Controls.Add(LeftButton);
            Controls.Add(RightButton);
            Controls.Add(VisibleButton);
            Controls.Add(SortByNameButton);
            Controls.Add(SearchLabel);
            Controls.Add(SearchTextBox);

            LeftButton.EnabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "LeftArrowEnabled.png");
            LeftButton.DisabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "LeftArrowDisabled.png");
            //LeftButton.Location = DPI.Instance.MultipliedPoint(806, 318);
            LeftButton.Size = Properties.Resources.LeftArrowEnabled.Size;
            LeftButton.Enabled = true;
            LeftButton.BringToFront();
            LeftButton.MouseUp += LeftButton_MouseUp;

            RightButton.EnabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "RightArrowEnabled.png");
            RightButton.DisabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "RightArrowDisabled.png");
            //RightButton.Location = DPI.Instance.MultipliedPoint(806, 281);
            RightButton.Size = Properties.Resources.LeftArrowEnabled.Size;
            RightButton.Enabled = true;
            RightButton.MouseUp += RightButton_MouseUp;
            RightButton.BringToFront();

            VisibleButton.EnabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "VisibleEnabledFalse.png");
            VisibleButton.DisabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "VisibleDisabled.png");
            VisibleButton.SelectedImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "VisibleEnabledTrue.png");
            VisibleButton.EnabledToolTipCaption = "Masquage des joueurs confirmés";
            VisibleButton.SelectedToolTipCaption = "Affichage de tous les joueurs";
            //VisibleButton.Location = DPI.Instance.MultipliedPoint(780, 56);
            VisibleButton.Size = VisibleButton.EnabledImage.Size;
            VisibleButton.Enabled = false;
            VisibleButton.Selected = true;
            VisibleButton.SelectedChanged += VisibleButton_SelectedChanged;
            VisibleButton.BringToFront();

            SortByNameButton.EnabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "SortNumericalEnabled.png");
            SortByNameButton.DisabledImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "SortAlphabeticalDisabled.png");
            SortByNameButton.SelectedImage = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/" + "SortAlphabeticalEnabled.png");
            SortByNameButton.EnabledToolTipCaption = "Tri par référence du joueur";
            SortByNameButton.SelectedToolTipCaption = "Tri par nom du joueur";
            //SortByNameButton.Location = DPI.Instance.MultipliedPoint(810, 56);
            SortByNameButton.Size = SortByNameButton.EnabledImage.Size;
            SortByNameButton.Enabled = false;
            SortByNameButton.Selected = true;
            SortByNameButton.SelectedChanged += SortByNameButton_SelectedChanged;
            SortByNameButton.BringToFront();

            SearchLabel.ForeColor = Theme.Style.CentreBodyLabelForeColor;
            SearchLabel.Font = Theme.Style.CentreBodyLabelFont;
            SearchLabel.AutoSize = true;
            //SearchLabel.Location = DPI.Instance.MultipliedPoint(239, 598);
            SearchLabel.Text = "Recherche :";

            SearchTextBox.AllowControls = true;
            SearchTextBox.AllowEmpty = true;
            SearchTextBox.AllowLetters = true;
            SearchTextBox.AllowNumbers = true;
            SearchTextBox.AllowSpace = true;
            SearchTextBox.BackColor = Theme.Style.CentreBodyTextBoxBackColor;
            SearchTextBox.ForeColor = Theme.Style.CentreBodyTextBoxForeColor;
            SearchTextBox.Font = Theme.Style.CentreBodyTextBoxFont;
            SearchTextBox.NormalColor = Theme.Style.CentreBodyTextBoxForeColor;
            SearchTextBox.NormalFont = Theme.Style.CentreBodyTextBoxFont;
            SearchTextBox.PlaceholdingColor = Theme.Style.CentreBodyTextPlaceHolderForeColor;
            SearchTextBox.PlaceholdingFont = Theme.Style.CentreBodyTextPlaceHolderFont;
            SearchTextBox.Location = DPI.Instance.MultipliedPoint(334, 598);
            SearchTextBox.Size = DPI.Instance.MultipliedSize(255, 20);
            SearchTextBox.PlaceholderText = "Nom, FFE ou FIDE";
            SearchTextBox.NewValue = string.Empty;
            SearchTextBox.Validated += SearchTextBox_Validated;
            SearchTextBox.CheckPlaceHolder(false);

            AddButton.ContextMenuStrip = new ContextMenuStrip();
            AddButton.ContextMenuStrip.BackColor = BackColor;
            AddButton.ContextMenuStrip.ForeColor = Theme.Style.CentreBodyJoueursForeColor;
            AddButton.ContextMenuStrip.Font = Theme.Style.CentreBodyJoueursFont;
            AddButton.ContextMenuStrip.ShowImageMargin = false;
            AddButton.ContextMenuStrip.ShowCheckMargin = false;

            CustomToolStripMenuItem tsi1 = new CustomToolStripMenuItem();
            tsi1.BackColor = AddButton.ContextMenuStrip.BackColor;
            tsi1.ForeColor = AddButton.ContextMenuStrip.ForeColor;
            tsi1.Font = AddButton.ContextMenuStrip.Font;
            tsi1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsi1.Text = "Ajouter un joueur";


            CustomToolStripMenuItem tsi11 = new CustomToolStripMenuItem();
            tsi11.BackColor = AddButton.ContextMenuStrip.BackColor;
            tsi11.ForeColor = AddButton.ContextMenuStrip.ForeColor;
            tsi11.Font = AddButton.ContextMenuStrip.Font;
            tsi11.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsi11.Text = "Depuis son numéro FFE";
            tsi11.Click += AddPlayerFromFFE_Click;
            tsi1.DropDownItems.Add(tsi11);

            CustomToolStripMenuItem tsi12 = new CustomToolStripMenuItem();
            tsi12.BackColor = AddButton.ContextMenuStrip.BackColor;
            tsi12.ForeColor = AddButton.ContextMenuStrip.ForeColor;
            tsi12.Font = AddButton.ContextMenuStrip.Font;
            tsi12.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsi12.Text = "Manuellement";
            tsi12.Click += AddPlayer_Click;
            tsi1.DropDownItems.Add(tsi12);


            CustomToolStripMenuItem tsi2 = new CustomToolStripMenuItem();
            tsi2.BackColor = AddButton.ContextMenuStrip.BackColor;
            tsi2.ForeColor = AddButton.ContextMenuStrip.ForeColor;
            tsi2.Font = AddButton.ContextMenuStrip.Font;
            tsi2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsi2.Text = "Ajouter un club";

            CustomToolStripMenuItem tsi21 = new CustomToolStripMenuItem();
            tsi21.BackColor = AddButton.ContextMenuStrip.BackColor;
            tsi21.ForeColor = AddButton.ContextMenuStrip.ForeColor;
            tsi21.Font = AddButton.ContextMenuStrip.Font;
            tsi21.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsi21.Text = "Depuis son nom";
            tsi21.Click += AddClubFromName_Click;
            tsi2.DropDownItems.Add(tsi21);

            AddButton.ContextMenuStrip.Items.Add(tsi1);
            AddButton.ContextMenuStrip.Items.Add(tsi2);


            RelocateAndResize();

            AddButtonClicked += (object sender, EventArgs e) =>
            {
                AddButton.ContextMenuStrip.Show(AddButton, new Point(AddButton.ClientSize.Width/2, AddButton.ClientSize.Height / 2));
            };




            Title = "Le titre";
        }

        /// <summary>
        /// Redimensionne et repositionne le panneau en fonction de la taille de la fenêtre.
        /// </summary>
        public void RelocateAndResize()
        {
            Location = new Point(CChesstion.OpensPanel.Right + 2, CChesstion.MsMenu.Bottom + 2);
            Size =
                new Size(Parent.ClientSize.Width - CChesstion.OpensPanel.Size.Width - CChesstion.JoueurPanel.Width - 4,
                    Parent.ClientSize.Height - CChesstion.MsMenu.Height - 2 - CChesstion.ActionPanel.Size.Height);

            Panel.Location = DPI.Instance.MultipliedPoint(panelStartingX, 80);
            Panel.Size = new System.Drawing.Size(spacesBetweenNames.X,
                this.Size.Height - Panel.Location.Y - (int)(DPI.Instance.RelativeMultiplier.Y * 45));


            RelocateButtons();

            if (Panel.Controls.Count > 0)
                Load();
        }

        /// <summary>
        /// Repositionne les boutons.
        /// </summary>
        protected override void RelocateButtons()
        {
            int margin = 6;
            LeftButton.Location = new Point(Size.Width - LeftButton.Size.Width - margin, (int)(InnerSize.Height * 0.55));
            RightButton.Location = new Point(Size.Width - LeftButton.Size.Width - margin, (int)(InnerSize.Height * 0.50));
            SortByNameButton.Location = new Point(InnerSize.Width - SortByNameButton.Width - margin, HeaderHeight + margin);
            VisibleButton.Location = new Point(SortByNameButton.Left - margin - VisibleButton.Size.Width, HeaderHeight + margin);

            SearchLabel.Location = new Point(Size.Width / 2 - (SearchLabel.Width + SearchTextBox.Width) / 2, Size.Height - SearchLabel.Size.Height - margin); 
            SearchTextBox.Location = new Point(SearchLabel.Right + margin, Size.Height - SearchLabel.Size.Height - margin);

            base.RelocateButtons();

        }

        /// <summary>
        /// Vide le panneau.
        /// </summary>
        public void Reset()
        {
            Panel.Controls.Clear();
            SearchTextBox.LastValue = string.Empty;
            SearchTextBox.Text = string.Empty;
            SearchTextBox.CheckPlaceHolder(false);
        }

        /// <summary>
        /// Charge la liste de joueur en labels joueurs.
        /// </summary>
        /// <param name="joueurs">Joueurs à charger.</param>
        /// <param name="withConfirmed">Vrai pour également afficher les joueurs confirmés.</param>
        private void LoadJoueurs(List<Joueur> joueurs, bool withConfirmed)
        {
            /* Debug */
            Stopwatch sw = new Stopwatch();
            //string bef = "LoadJoueurs - ";
            sw.Start();
            /* END Debug */

            int count = joueurs.Count;

            Panel.Controls.Clear();
            numberOfCols = 1;

            int selectedRef = CChesstion.JoueurSelectionne != null ? CChesstion.JoueurSelectionne.Ref : -1;

            int y = 0, x = 0;
            int yy = (int)SpacesBetweenNames.Y, xx = (int)SpacesBetweenNames.X;
            int maxy = CChesstion.CentrePanel.Panel.Height;

            /* Debug */
            sw.Stop();
            //Debug.WriteLine(bef + "setup takes " + sw.ElapsedMilliseconds + " ms.");
            Stopwatch sw2 = new Stopwatch();
            //string bef2 = "for loop - ";
            sw.Restart();
            /* END Debug */

            for (int i = 0; i < count; i++)
            {
                if (!withConfirmed && joueurs[i].Confirme)
                    continue;

                /* Debug */
                sw2.Start();
                /* END Debug */

                CentreJoueurLabel jl = new CentreJoueurLabel(joueurs[i]);

                /* Debug */
                sw2.Stop();
                if (sw2.ElapsedMilliseconds > 0)
                    //Debug.WriteLine(bef2 + "CentreJoueurLabel jl = new CentreJoueurLabel(joueurs[i]); takes " + sw2.ElapsedMilliseconds + " ms");
                sw2.Restart();
                /* END Debug */

                jl.JustSelected += Jl_JustSelected;

                if (jl.Ref == selectedRef)
                    jl.Selected = true;

                /* Debug */
                sw2.Stop();
                if (sw2.ElapsedMilliseconds > 0)
                    //Debug.WriteLine(bef2 + "if (jl.Ref == selectedRef) jl.Selected = true; takes " + sw2.ElapsedMilliseconds + " ms");
                sw2.Restart();
                /* END Debug */

                Panel.Controls.Add(jl);

                /* Debug */
                sw2.Stop();
                if (sw2.ElapsedMilliseconds > 0)
                    //Debug.WriteLine(bef2 + "Panel.Controls.Add(jl); takes " + sw2.ElapsedMilliseconds + " ms");
                sw2.Restart();
                /* END Debug */

                CChesstion.DisplayJoueurErrored(jl.Ref, CChesstion.IsJoueurErrored(jl.Ref));

                /* Debug */
                sw2.Stop();
                if (sw2.ElapsedMilliseconds > 0)
                    //Debug.WriteLine(bef2 + "CChesstion.DisplayJoueurErrored(jl.Ref, CChesstion.IsJoueurErrored(jl.Ref)); takes " + sw2.ElapsedMilliseconds + " ms");
                sw2.Restart();
                /* END Debug */

                jl.Location = new Point(x, y);
                y += yy;

                /* Debug */
                sw2.Stop();
                if (sw2.ElapsedMilliseconds > 0)
                    //Debug.WriteLine(bef2 + "jl.Location = new Point(x, y); takes " + sw2.ElapsedMilliseconds + " ms");
                sw2.Restart();
                /* END Debug */
                

                if (y + jl.Size.Height >= maxy && i != count - 1)
                {
                    y = 0;
                    x += xx;
                    if (Panel.Size.Width <= x)
                        Panel.Size =
                            new System.Drawing.Size(Panel.Size.Width + xx,
                                Panel.Size.Height);
                    numberOfCols++;
                }

                /* Debug */
                sw2.Stop();
                if (sw2.ElapsedMilliseconds > 0)
                    //Debug.WriteLine(bef2 + "Ending takes " + sw2.ElapsedMilliseconds + " ms");
                sw2.Reset();
                /* END Debug */
            }

            /* Debug */
            sw.Stop();
            //Debug.WriteLine(bef + "for loop takes " + sw.ElapsedMilliseconds + " ms.");
            /* END Debug */

            //Debug.WriteLine("Number of rows : " + numberOfRows + " ; cols : " + numberOfCols);

            RightButton.Enabled = false;
            LeftButton.Enabled = false;

            if (numberOfCols > 1)
                RightButton.Enabled = true;

        }

        /// <summary>
        /// Recharge les joueurs de l'<see cref="CChesstion.OpenSelectionne"/>.
        /// </summary>
        public void Load()
        {
            if (CChesstion.OpenSelectionne == null)
            {
                Panel.Controls.Clear();
                VisibleButton.Enabled = false;
                SortByNameButton.Enabled = false;
                Title = "";
                return;
            }
            VisibleButton.Enabled = true;
            SortByNameButton.Enabled = true;

            List<Joueur> results = new List<Joueur>();

            if (!string.IsNullOrWhiteSpace(SearchTextBox.LastValue) &&
                SearchTextBox.LastValue != SearchTextBox.PlaceholderText)
            {
                results =
                (from joueur in
                    (SortByNameButton.Selected
                        ? CChesstion.OpenSelectionne.TsLesJoueurs.OrderBy(o => o.Nom).ToList()
                        : CChesstion.OpenSelectionne.TsLesJoueurs.OrderBy(o => o.Ref).ToList())
                    where
                    (joueur.Nom.ToLower() + " " + joueur.Prenom.ToLower()).Contains(SearchTextBox.LastValue.ToLower()) ||
                    joueur.NrFFE.ToString().Contains(SearchTextBox.LastValue) ||
                    joueur.FideCode.Contains(SearchTextBox.LastValue)
                    select joueur).ToList();
            }
            else
            {
                results = SortByNameButton.Selected
                    ? CChesstion.OpenSelectionne.TsLesJoueurs.OrderBy(o => o.Nom).ToList()
                    : CChesstion.OpenSelectionne.TsLesJoueurs.OrderBy(o => o.Ref).ToList();
            }

            LoadJoueurs(results, VisibleButton.Selected);
            Title = CChesstion.TournoiSelectionne.Nom + " — " + (CChesstion.OpenSelectionne != null ? CChesstion.OpenSelectionne.Nom : "");
        }

        /// <summary>
        /// Fait défiler le panneau.
        /// </summary>
        /// <param name="amount">Quantité de défilement.</param>
        /// <param name="alignOnCol">Vrai pour s'aliner sur les noms de joueurs les plus proches.</param>
        public new void Scroll(int amount, bool alignOnCol = false)
        {
            if (amount == 0)
                return;

            int multipliedPanelStartingX = (int) (DPI.Instance.RelativeMultiplier.X*panelStartingX);

            Debug.WriteLine("Trying to scroll by " + amount);
            int maxLeft = multipliedPanelStartingX - (numberOfCols - 2)*spacesBetweenNames.X;
            int maxRight = multipliedPanelStartingX;
            int oldX = Panel.Location.X;
            Debug.WriteLine("MaxRight = " + maxRight + " ; MaxLeft = " + maxLeft);
            Debug.WriteLine("oldX = " + oldX);
            int newX = Panel.Location.X + amount;
            Debug.WriteLine("newX (before) = " + newX);
            if (newX < maxLeft)
                newX = maxLeft;
            else if (newX > maxRight)
                newX = maxRight;

            Debug.WriteLine("newX (after) = " + newX);

            if (newX == oldX)
                return;

            Debug.WriteLine("multipliedPanelStartingX = " + multipliedPanelStartingX);
            Debug.WriteLine("(newX - multipliedPanelStartingX) % SpacesBetweenNames.X = " +
                            (newX - multipliedPanelStartingX)%SpacesBetweenNames.X);

            if (alignOnCol && (newX - multipliedPanelStartingX)%SpacesBetweenNames.X != 0)
            {
                Debug.WriteLine("Need to align !");
                if (newX < oldX) // went left
                {
                    newX = newX - (newX - multipliedPanelStartingX)%SpacesBetweenNames.X;
                    Debug.WriteLine("Aligned ?");
                }
                /*else // went right
                {
                    newX = newX + (newX + spacesBetweenNames.X - multipliedPanelStartingX) % SpacesBetweenNames.X;
                    Debug.WriteLine("Aligned ?");
                }*/
            }
            Panel.Location =
               new Point(newX, Panel.Location.Y);

            if (newX == maxLeft)
            {
                RightButton.Enabled = false;
                LeftButton.Enabled = true;
            }
            else if (newX == maxRight)
            {
                RightButton.Enabled = true;
                LeftButton.Enabled = false;
            }
            else
            {
                RightButton.Enabled = true;
                LeftButton.Enabled = true;
            }
        }

        /// <summary>
        /// Fait défiler vers la droite.
        /// </summary>
        /// <param name="amount">Quantité de défilement.</param>
        /// <param name="alignOnCol">Vrai pour s'aliner sur les noms de joueurs les plus proches.</param>
        public void ScrollRight(int amount, bool alignOnCol = false)
        {
            Scroll(-amount, alignOnCol);
        }

        /// <summary>
        /// Fait défiler vers la gauche.
        /// </summary>
        /// <param name="amount">Quantité de défilement.</param>
        /// <param name="alignOnCol">Vrai pour s'aliner sur les noms de joueurs les plus proches.</param>
        public void ScrollLeft(int amount, bool alignOnCol = false)
        {
            Scroll(+amount, alignOnCol);
        }



        /// <summary>
        /// Confirme le joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="value">Vrai pour confirmer ; faux pour annuler.</param>
        public void ConfirmerJoueur(int reference, bool value)
        {
            if (CChesstion.JoueurSelectionne != null && CChesstion.JoueurSelectionne.Ref == reference)
            {
                if (LabelSelectionne != null && LabelSelectionne.Ref == reference)
                {
                    if (value)
                        LabelSelectionne.DisplayConfirmed = true;
                    else
                        LabelSelectionne.DisplayNormal = true;
                }
            }
            else
            {
                foreach (CentreJoueurLabel cjl in Labels)
                    if (cjl.Ref == reference)
                        if (value)
                            cjl.DisplayConfirmed = true;
                        else
                            cjl.DisplayNormal = true;
            }
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
        /// Met en forme le label sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Jl_JustSelected(object sender, EventArgs e)
        {
            CentreJoueurLabel cjl = (CentreJoueurLabel) sender;

            foreach (CentreJoueurLabel cjll in Labels)
                if (cjll.Selected && cjll != cjl)
                    cjll.Selected = false;
        }

        /// <summary>
        /// Effectue les actions de défilement du panneau.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CentrePanel_MouseWheel(object sender, MouseEventArgs e)
        {
            Scroll(e.Delta);
        }

        /// <summary>
        /// Sélectionne un joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!LeftButton.ClientRectangle.Contains(LeftButton.PointToClient(Control.MousePosition)))
                return;

            ScrollLeft(spacesBetweenNames.X, true);

        }

        /// <summary>
        /// Affiche le menu contextuel du joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (!RightButton.ClientRectangle.Contains(RightButton.PointToClient(Control.MousePosition)))
                return;

            ScrollRight(spacesBetweenNames.X, true);
        }

        /// <summary>
        /// Recharge les joueurs avec les nouvelles options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleButton_SelectedChanged(object sender, EventArgs e)
        {
            Load();
        }

        /// <summary>
        /// Recharge les joueurs avec les nouvelles options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByNameButton_SelectedChanged(object sender, EventArgs e)
        {
            Load();
        }

        /// <summary>
        /// Recharge les joueurs avec les nouvelles options de recherche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTextBox_Validated(object sender, EventArgs e)
        {
            Load();
        }

        /// <summary>
        /// Supprime le joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CentrePanel_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (CChesstion.JoueurSelectionne == null)
                return;

            try
            {
                if (confirmDeleteDialog == null || !confirmDeleteDialog.Visible)
                {
                    confirmDeleteDialog = new CustomQuickDialog(
                        "Êtes-vous sûr ?\nCliquez à nouveau pour confirmer !",
                        QuickDialogType.Info,
                        DeleteButton,
                        QuickDialogRelativeStartPosition.CenterBelowParent);
                    confirmDeleteDialog.DisplayDelay = 3000;
                    confirmDeleteDialog.Show();
                }
                else
                {
                    confirmDeleteDialog.Close();
                    CChesstion.SupprimerJoueur(CChesstion.JoueurSelectionne.Ref, true);
                }
            }
            catch (ArgumentException ae)
            {

                errorDeleteDialog = new CustomQuickDialog(
                    ae.Message,
                    QuickDialogType.Error,
                    DeleteButton,
                    QuickDialogRelativeStartPosition.CenterBelowParent);
                errorDeleteDialog.DisplayDelay = 3000;
                errorDeleteDialog.Show();
            }
        }

        /// <summary>
        /// Affiche le menu d'ajout de joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPlayer_Click(object sender, EventArgs e)
        {
            addPlayerDialog = new AddPlayerDialog();
            addPlayerDialog.ShowDialog();
        }

        /// <summary>
        /// Affiche <see cref="AddPlayerFromFFEDialog"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPlayerFromFFE_Click(object sender, EventArgs e)
        {
            addPlayerFromFFEDialog = new AddPlayerFromFFEDialog();
            addPlayerFromFFEDialog.ShowDialog();
        }

        /// <summary>
        /// Affiche <see cref="AddClubFromNameDialog"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClubFromName_Click(object sender, EventArgs e)
        {
            AddClubFromNameDialog addClubFromNameDialog = new AddClubFromNameDialog();
            addClubFromNameDialog.ShowDialog();
        }





    }
}
