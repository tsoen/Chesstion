using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CTournoi;
using ChessTion.Test;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomComboBoxes;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    class OpenPanel : HeadedPanel, IChesstionPanel
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

        private bool _readOnly;

        private Label lblEloMax = new Label();
        private Label lblTournoi = new Label();
        private Label lblDate = new Label();
        private Label lblDateFin = new Label();
        private Label lblLieu = new Label();
        private Label lblMaxParticipants = new Label();
        private Label lblMajoration = new Label();

        private OpenTextBox htbEloMax = new OpenTextBox();
        private TournoiDateTextBox htbDate = new TournoiDateTextBox();
        private TournoiDateTextBox htbDateFin = new TournoiDateTextBox();
        private LieuComboBox lcbLieu = new LieuComboBox();
        private TournoiTextBox htbMaxParticipants = new TournoiTextBox();
        private TournoiTextBox htbMajoration = new TournoiTextBox();










        /*************************************************************************************
         *   ___  _____  _  _  ___  ____  ____  __  __   ___  ____  ____  __  __  ____  ___  *
         *  / __)(  _  )( \( )/ __)(_  _)(  _ \(  )(  ) / __)(_  _)( ___)(  )(  )(  _ \/ __) *
         * ( (__  )(_)(  )  ( \__ \  )(   )   / )(__)( ( (__   )(   )__)  )(__)(  )   /\__ \ *
         *  \___)(_____)(_)\_)(___/ (__) (_)\_)(______) \___) (__) (____)(______)(_)\_)(___/ *
         *                                                                                   *
         *                      Ensemble des constructeurs de la classe.                     *
         *                                                                                   *
         *************************************************************************************/

        public OpenPanel()
        {
            CreateLabels();
            CreateBoxes();
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
        /// Retourne la liste d'<see cref="HiddenTextBox"/> et de <see cref="HiddenComboBox"/>  composant le panel.
        /// </summary>
        public List<Control> Boxes
        {
            get
            {
                List<Control> boxes = new List<Control>();
                foreach (Control c in Controls)
                    if (c is CustomHiddenTextBox || c is CustomComboBox)
                        boxes.Add(c);

                return boxes;
            }
        }

        public List<CustomHiddenTextBox> TextBoxes
        {
            get
            {
                List<CustomHiddenTextBox> htboxes = new List<CustomHiddenTextBox>();
                foreach (Control c in Boxes)
                    if (c is CustomHiddenTextBox)
                        htboxes.Add((CustomHiddenTextBox)c);
                return htboxes;
            }
        }

        public List<CustomComboBox> ComboBoxes
        {
            get
            {
                List<CustomComboBox> hcboxes = new List<CustomComboBox>();

                foreach (Control c in Boxes)
                    if (c is CustomComboBox)
                        hcboxes.Add((CustomComboBox)c);

                return hcboxes;
            }
        }

        /// <summary>
        /// Get ou Set toutes les <see cref="Boxes"/> en lecture seule.
        /// </summary>
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                Debug.WriteLine("Readonly to " + value);
                _readOnly = value;
                foreach (Control c in Boxes)
                {
                    if (c is HiddenTextBox)
                        ((HiddenTextBox)c).ReadOnly = value;
                    if (c is ComboBox)
                        ((ComboBox)c).Enabled = !value;
                }
            }
        }

        public string FormattedTitle
        {
            get { return Title; }
            set
            {
                if (!value.ToLower().Contains("open"))
                    Title = "Open " + value;
                else
                    Title = value;
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

        //
        // Création des controls
        //

        public void Init()
        {
            CreatePanel();

#if DEBUG
            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add("Header width", (object sender, EventArgs args) =>
            {
                MessageBox.Show("Header.Width: " + header.Width + " (panel.Width:" + Width);
            });
#endif
        }

        private void CreatePanel()
        {
            //Location = DPI.Instance.MultipliedPoint(1048, 486);
            //Size = DPI.Instance.MultipliedSize(300, 237);
            HeaderHeight = (int)(DPI.Instance.RelativeMultiplier.Y * Theme.Style.OpenHeaderHeight);
            BackColor = Theme.Style.OpenBodyBackColor;
            HeaderBackColor = Theme.Style.OpenHeaderBackColor;
            HeaderForeColor = Theme.Style.OpenHeaderForeColor;
            HeaderFont = Theme.Style.OpenHeaderFont;
            Title = "Open";

            RelocateAndResize();

            CheckAllTextBoxesPlaceHolders(false);

        }

        private void CreateLabels()
        {
            // Label Elo max
            Controls.Add(lblEloMax);
            lblEloMax.Text = "Elo Max :";
            lblEloMax.AutoSize = true;
            lblEloMax.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblEloMax.Font = Theme.Style.OpenLabelsFont;
            lblEloMax.Location = new Point(4, 41);
            ToolTip t1 = new ToolTip();
            t1.InitialDelay = 1;
            t1.SetToolTip(lblEloMax, "Elo maximum accepté dans cet open");

            // Label Tournoi
            Controls.Add(lblTournoi);
            lblTournoi.Text = "Valables pour tout le tournoi :";
            lblTournoi.AutoSize = true;
            lblTournoi.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblTournoi.Font = Theme.Style.OpenLabelsFont;
            lblTournoi.Font = new Font(lblTournoi.Font, FontStyle.Italic);
            lblTournoi.Location = new Point(4, 73);
            

            // Label Date
            Controls.Add(lblDate);
            lblDate.Text = "Date :";
            lblDate.AutoSize = true;
            lblDate.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblDate.Font = Theme.Style.OpenLabelsFont;
            lblDate.Location = new Point(4, 102);  // Y + 50
            ToolTip t2 = new ToolTip();
            t2.InitialDelay = 1;
            t2.SetToolTip(lblDate, "Date de début du tournoi");

            // Label DateFin
            Controls.Add(lblDateFin);
            lblDateFin.Text = "Date fin :";
            lblDateFin.AutoSize = true;
            lblDateFin.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblDateFin.Font = Theme.Style.OpenLabelsFont;
            lblDateFin.Location = new Point(4, 128);  // Y + 50
            ToolTip t3 = new ToolTip();
            t3.InitialDelay = 1;
            t3.SetToolTip(lblDateFin, "Date de fin du tournoi");

            // Label Lieu
            Controls.Add(lblLieu);
            lblLieu.Text = "Lieu :";
            lblLieu.AutoSize = true;
            lblLieu.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblLieu.Font = Theme.Style.OpenLabelsFont;
            lblLieu.Location = new Point(4, 154);  // Y + 50
            ToolTip t4 = new ToolTip();
            t4.InitialDelay = 1;
            t4.SetToolTip(lblLieu, "Lieu où se déroule le tournoi");

            // Label MaxParticipants
            Controls.Add(lblMaxParticipants);
            lblMaxParticipants.Text = "Max participants :";
            lblMaxParticipants.AutoSize = true;
            lblMaxParticipants.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblMaxParticipants.Font = Theme.Style.OpenLabelsFont;
            lblMaxParticipants.Location = new Point(4, 180);  // Y + 50
            ToolTip t5 = new ToolTip();
            t5.InitialDelay = 1;
            t5.SetToolTip(lblMaxParticipants, "Nombre maximal de participants au tournoi");

            // Label Majoration
            Controls.Add(lblMajoration);
            lblMajoration.Text = "Majoration :";
            lblMajoration.AutoSize = true;
            lblMajoration.ForeColor = Theme.Style.OpenLabelsForeColor;
            lblMajoration.Font = Theme.Style.OpenLabelsFont;
            lblMajoration.Location = new Point(4, 206);  // Y + 50
            ToolTip t6 = new ToolTip();
            t6.InitialDelay = 1;
            t6.SetToolTip(lblMajoration, "Majoration appliquée aux joueurs s'inscrivant le jour du tournoi");
        }
        private void CreateBoxes()
        {
            // Hidden Elo max
            Controls.Add(htbEloMax);
            htbEloMax.Size = new Size(206, 20);
            htbEloMax.MaxLength = 4;
            htbEloMax.Location = new Point(82, 41);
            htbEloMax.AllowNumbers = true;
            htbEloMax.AllowedCharacters = new List<char> {'-'};
            htbEloMax.Regex = new Regex(@"(-1|[0-9]{3}[0-9]?)");
            htbEloMax.PlaceholderText = "1500";
            htbEloMax.Info = "EloMax";
            ToolTip t = new ToolTip();
            t.SetToolTip(htbEloMax, "Indiquez -1 pour accepter tous les elo");

            // Hidden DateDebut
            Controls.Add(htbDate);
            htbDate.Size = new Size(231, 20);
            htbDate.Location = new Point(57, 101); // Y + 50
            htbDate.TabStop = false;
            htbDate.Info = "DateDebut";
            htbDate.Validating += HtbDate_Validating;

            // Hidden DateFin
            Controls.Add(htbDateFin);
            htbDateFin.Size = new Size(208, 20);
            htbDateFin.Location = new Point(80, 127); // Y + 50
            htbDateFin.TabStop = false;
            htbDateFin.Info = "DateFin";
            htbDateFin.Validating += HtbDateFin_Validating;

            // Hidden MaxParticipants
            Controls.Add(htbMaxParticipants);
            htbMaxParticipants.Size = new Size(146, 20);
            htbMaxParticipants.MaxLength = 4;
            htbMaxParticipants.Location = new Point(142, 179);
            htbMaxParticipants.AllowNumbers = true;
            htbMaxParticipants.PlaceholderText = "500";
            htbMaxParticipants.Info = "MaxParticipants";
            htbMaxParticipants.Validating += HtbMaxParticipants_Validating;

            // Hidden Majoration
            Controls.Add(htbMajoration);
            htbMajoration.Size = new Size(104, 20);
            htbMajoration.MaxLength = 4;
            htbMajoration.Location = new Point(100, 205);
            htbMajoration.AllowNumbers = true;
            htbMajoration.AllowDecimal = true;
            htbMajoration.PlaceholderText = "2";
            htbMajoration.Info = "Majoration";

            //
            // Combo Box
            //

            // Hidden Lieu
            Controls.Add(lcbLieu);
            lcbLieu.Size = new Size(216, 20);
            lcbLieu.BackColor = Theme.Style.OpenBoxesBackColor;
            lcbLieu.ForeColor = Theme.Style.OpenBoxesForeColor;
            lcbLieu.Font = Theme.Style.OpenBoxesFont;
            lcbLieu.Location = new Point(53, 152); // Y + 50
            lcbLieu.DropDownStyle = ComboBoxStyle.DropDownList;
            lcbLieu.AttributeToDisplay = "Nom";
            lcbLieu.SelectedValueChanged += LcbLieu_SelectedValueChanged;
        }


        public void RelocateAndResize()
        {
            Location = new Point(CChesstion.JoueurPanel.Location.X, CChesstion.JoueurPanel.Bottom + 2);
            Size = new Size(CChesstion.JoueurPanel.Size.Width,
                (int) ((Parent.ClientSize.Height - (CChesstion.MsMenu.Height + 2))*0.34));
        }

        private void AddEvents()
        {

        }

        private void CheckAllTextBoxesPlaceHolders(bool gotFocus)
        {
            foreach (HiddenTextBox htb in TextBoxes)
                htb.CheckPlaceHolder(gotFocus);
        }
        public void Reset()
        {
            EmptyBoxes();
            Title = "Open";
        }
        private void EmptyBoxes()
        {
            foreach (HiddenTextBox tb in TextBoxes)
                EmptyTextBox(tb);
            foreach (HiddenComboBox hcb in ComboBoxes)
            {
                try { hcb.SelectedIndex = -1; }
                catch
                {
                    try { hcb.SelectedIndex = 0; }
                    catch { try { hcb.SelectedIndex = 1; } catch { } }
                }
            }
            Title = "";

        }
        public void EmptyTextBox(Control c)
        {
            if (!Boxes.Contains(c))
                throw new ArgumentException("EmptyTextBox - La box donnée en paramètre ne correspond à aucune box du panel Joueur");

            if (c is HiddenTextBox)
            {
                HiddenTextBox htb = (HiddenTextBox)c;
                htb.Text = string.Empty;
                htb.LastValue = string.Empty;
                htb.CheckPlaceHolder(false);
            }
        }

        public void LoadInfoToPanel()
        {
            //Debug.WriteLine("1 " + CChesstion.OpenSelectionne.EloMax);

            Reset();
            //Debug.WriteLine("2 " + CChesstion.OpenSelectionne.EloMax);

            if (CChesstion.OpenSelectionne == null)
                return;
            //Debug.WriteLine("3 " + CChesstion.OpenSelectionne.EloMax);

            bool v = ReadOnly;
            ReadOnly = false;
            CheckAllTextBoxesPlaceHolders(true);
            //Debug.WriteLine("4 " + CChesstion.OpenSelectionne.EloMax);
            htbEloMax.NewValue = CChesstion.OpenSelectionne.EloMax.ToString();
            if (GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref) != null)
            {
                htbDate.NewValue = GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).DateDebut.ToString("dd/MM/yyyy");
                htbDateFin.NewValue = GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).DateFin.ToString("dd/MM/yyyy");
                htbMaxParticipants.NewValue = GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).MaxParticipants.ToString();
                htbMajoration.NewValue = GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).Majoration.ToString();
            }
            lcbLieu.SelectedValue = GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).Lieu.Ref;

            FormattedTitle = CChesstion.OpenSelectionne.Nom;

            CheckAllTextBoxesPlaceHolders(false);
            ReadOnly = v;
        }
        public void LoadInfoFromPanel(bool resetPanel = false)
        {
            if (string.IsNullOrEmpty(htbEloMax.LastValue) || CChesstion.OpenSelectionne == null || GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref) == null)
                return;
            GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).Lieu = GLieu.GetLieu(int.Parse(lcbLieu.SelectedValue.ToString()));
            GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).MaxParticipants =
                !string.IsNullOrWhiteSpace(htbMaxParticipants.LastValue)
                    ? int.Parse(htbMaxParticipants.LastValue)
                    : 10000;
            GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).Majoration =
                !string.IsNullOrWhiteSpace(htbMajoration.LastValue)
                    ? float.Parse(htbMajoration.LastValue)
                    : 0f;

        }


        private void HtbDateFin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (htbDateFin.Regex.IsMatch(htbDateFin.Text) && Convert.ToDateTime(htbDateFin.Text) < CChesstion.TournoiSelectionne.DateDebut)
            {
                CustomQuickDialog q =
                    new CustomQuickDialog("La date de fin du tournoi est\ninférieure à la date de début !",
                        GeneralControls.CustomDialogs.QuickDialogType.Error, htbDateFin,
                        GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                q.Closable = true;
                q.DisplayDelay = 5000;
                q.Show();
            }
        }

        private void HtbDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (htbDate.Regex.IsMatch(htbDate.Text) && Convert.ToDateTime(htbDate.Text) < DateTime.Today)
            {
                CustomQuickDialog q =
                    new CustomQuickDialog("La date de début du tournoi\n est déjà passée.",
                        GeneralControls.CustomDialogs.QuickDialogType.Error, htbDate,
                        GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                q.Closable = true;
                q.DisplayDelay = 5000;
                q.Show();
                e.Cancel = true;
            }
        }
        private void HtbMaxParticipants_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (htbMaxParticipants.Regex.IsMatch(htbMaxParticipants.Text) && int.Parse(htbMaxParticipants.Text) < 2)
            {
                CustomQuickDialog q =
                    new CustomQuickDialog("Attentin : le nombre de participants est inférieure à 2.\nEst-ce voulu ? (Echap pour confirmer)",
                        GeneralControls.CustomDialogs.QuickDialogType.Warning, htbMaxParticipants,
                        GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                q.Closable = true;
                q.DisplayDelay = 5000;
                q.Show();
                e.Cancel = false;
            }
        }

        private void LcbLieu_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lcbLieu.SelectedValue != null && GLieu.GetLieu(int.Parse(lcbLieu.SelectedValue.ToString())) != null)
                {
                    ToolTip t = new ToolTip();
                    t.SetToolTip(lcbLieu, GLieu.GetLieu(int.Parse(lcbLieu.SelectedValue.ToString())).ToString());
                }
            } catch { }
        }




    }
}
