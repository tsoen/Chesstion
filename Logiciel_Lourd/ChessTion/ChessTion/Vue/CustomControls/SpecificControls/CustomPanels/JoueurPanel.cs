using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Modele.MTournoi;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomComboBoxes;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using ChessTion.Vue.CustomControls.SpecificControls.CustomMenus;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    /// <summary>
    /// Classe gérant l'ensemble du panel consacré au joueur sélectionné dans le panel central.
    /// Ce panel se trouve en haut à droite du logiciel.
    /// </summary>
    class JoueurPanel : HeadedPanel, IChesstionPanel
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
        /// <see cref="EventHandler"/> activé lorsque l'attribut <see cref="Joueur.Confirme"/> du <see cref="Joueur"/> est modifié. 
        /// </summary>
        public event EventHandler JoueurConfirmeChanged;

        private bool _readOnly = false;
        private bool isHandling = false;
        private bool errored = false;

        private Button btnConfirmer = new Button();
        private Button btnDetails = new Button();

        private Label lblFide = new Label();
        private Label lblFfe = new Label();
        private Label lblNele = new Label();
        private Label lblSexe = new Label();
        private Label lblCat = new Label();
        private Label lblFede = new Label();
        private Label lblElo = new Label();
        private Label lblTitre = new Label();
        private Label lblClub = new Label();
        private Label lblMail = new Label();
        private Label lblFrais = new Label();
        private Label lblRepas = new Label();
        private Label lblOpen = new Label();

        private Label lblAPayer = new Label();

        private JoueurTextBox txtFideCode = new JoueurTextBox();
        private JoueurTextBox txtNrFFE = new JoueurTextBox();
        private JoueurDateTextBox txtNeLe = new JoueurDateTextBox();
        private JoueurTextBox txtSexe = new JoueurTextBox();
        private JoueurTextBox txtCat = new JoueurTextBox();
        private JoueurTextBox txtFederation = new JoueurTextBox();
        private JoueurTextBox txtElo = new JoueurTextBox();
        private JoueurTextBox txtFideTitre = new JoueurTextBox();
        private JoueurEmailTextBox txtJoueurEmail = new JoueurEmailTextBox();
        private JoueurTextBox txtFraisInscription = new JoueurTextBox();
        private RepasComboBox rcbRepasRef = new RepasComboBox();
        private OpenComboBox ocbOpenRef = new OpenComboBox();
        private ClubComboBox ccbClubRef = new ClubComboBox();

        public bool IsHandling { get { return isHandling; } }










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
        /// Constructeur du panel Joueur
        /// </summary>
        public JoueurPanel() : base()
        {
            CreateButtons();
            CreateLabels();
            CreateBoxes();
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
        /// Retourne la liste d'<see cref="CustomHiddenTextBox"/> et de <see cref="CustomComboBox"/> composant le panel.
        /// </summary>
        public List<Control> Boxes
        {
            get
            {
                List<Control> boxes = new List<Control>();
                foreach (Control c in Controls)
                    if (c is HiddenTextBox || c is HiddenComboBox)
                        boxes.Add(c);

                return boxes;
            }
        }

        /// <summary>
        /// Retourne la liste d'<see cref="HiddenTextBox"/> composant le panel.
        /// </summary>
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

        /// <summary>
        /// Retourne la liste de <see cref="CustomComboBox"/> composant le panel.
        /// </summary>
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

        /// <summary>
        /// Si vrai, la confirmation n'est pas possible et le <see cref="JoueurPanel"/> apparaît en rouge.
        /// </summary>
        public bool Errored
        {
            get { return errored; }
            set
            {
                if (errored == value)
                    return;

                errored = value;
                if (errored)
                {
                    this.HeaderBackColor = Theme.Style.JoueurBoxesErrorBackColor;
                }
                else
                {
                    this.HeaderBackColor = Theme.Style.JoueurHeaderBackColor;
                }
            }
        }

        /// <summary>
        /// Retourne la <see cref="OpenComboBox"/> du panel. 
        /// </summary>
        public OpenComboBox OpenComboBox
        {
            get { return ocbOpenRef; }
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
            RelocateButtons();
            Reset();
        }



        /// <summary>
        /// Crée et paramètre l'objet <see cref="Panel"/> en lui-même. 
        /// </summary>
        private void CreatePanel()
        {
            HeaderHeight = (int)(DPI.Instance.RelativeMultiplier.Y * Theme.Style.JoueurHeaderHeight);
            BackColor = Theme.Style.JoueurBodyBackColor;
            HeaderBackColor = Theme.Style.JoueurHeaderBackColor;
            HeaderForeColor = Theme.Style.JoueurHeaderForeColor;
            HeaderFont = Theme.Style.JoueurHeaderFont;
            Title = "Joueur";

            RelocateAndResize();

            CheckAllTextBoxesPlaceHolders(false);

        }

        /// <summary>
        /// Crée et paramètre les deux <see cref="Button"/> du panel. 
        /// </summary>
        private void CreateButtons()
        {
            //
            // Buttons
            //

            // Button Confirmer
            Controls.Add(btnConfirmer);
            btnConfirmer.Text = "Confirmer";
            btnConfirmer.FlatStyle = FlatStyle.Flat;
            btnConfirmer.BackColor = Theme.Style.JoueurConfirmButtonBackColor;
            btnConfirmer.ForeColor = Theme.Style.JoueurConfirmButtonForeColor;
            btnConfirmer.Font = Theme.Style.JoueurConfirmButtonFont;
            btnConfirmer.Size = new Size(109, 45);
            btnConfirmer.Tag = true;
            btnConfirmer.Click += BtnConfirmer_Click;

            // Button Détails
            Controls.Add(btnDetails);
            btnDetails.Text = "Détails";
            btnDetails.FlatStyle = FlatStyle.Flat;
            btnDetails.BackColor = Theme.Style.JoueurDetailsButtonBackColor;
            btnDetails.ForeColor = Theme.Style.JoueurDetailsButtonForeColor;
            btnDetails.Font = Theme.Style.JoueurDetailsButtonFont;
            btnDetails.Size = new Size(109, 45);
        }

        /// <summary>
        /// Crée et paramètre les <see cref="Label"/> du panel.
        /// </summary>
        private void CreateLabels()
        {
            //
            // Labels
            //

            // Label Fide
            Controls.Add(lblFide);
            lblFide.Text = "Fide :";
            lblFide.AutoSize = true;
            lblFide.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblFide.Font = Theme.Style.JoueurLabelsFont;
            lblFide.Location = new Point(4, 71);
            ToolTip t1 = new ToolTip();
            t1.InitialDelay = 1;
            t1.SetToolTip(lblFide, "Code fide");

            // Label FFE
            Controls.Add(lblFfe);
            lblFfe.Text = "FFE :";
            lblFfe.AutoSize = true;
            lblFfe.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblFfe.Font = Theme.Style.JoueurLabelsFont;
            lblFfe.Location = new Point(172, 71);
            ToolTip t2 = new ToolTip();
            t2.InitialDelay = 1;
            t2.SetToolTip(lblFfe, "Numéro FFE");

            // Label Né le
            Controls.Add(lblNele);
            lblNele.Text = "Né le :";
            lblNele.AutoSize = true;
            lblNele.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblNele.Font = Theme.Style.JoueurLabelsFont;
            lblNele.Location = new Point(4, 102);
            ToolTip t3 = new ToolTip();
            t3.InitialDelay = 1;
            t3.SetToolTip(lblNele, "Date de naissance");

            // Label Sexe
            Controls.Add(lblSexe);
            lblSexe.Text = "Sexe :";
            lblSexe.AutoSize = true;
            lblSexe.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblSexe.Font = Theme.Style.JoueurLabelsFont;
            lblSexe.Location = new Point(172, 102);
            ToolTip t4 = new ToolTip();
            t4.InitialDelay = 1;
            t4.SetToolTip(lblSexe, "[M]asculin ou [F]éminin");

            // Label Cat
            Controls.Add(lblCat);
            lblCat.Text = "Cat :";
            lblCat.AutoSize = true;
            lblCat.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblCat.Font = Theme.Style.JoueurLabelsFont;
            lblCat.Location = new Point(4, 133);
            ToolTip t5 = new ToolTip();
            t5.InitialDelay = 1;
            t5.SetToolTip(lblCat, "Catégorie");

            // Label Fédé
            Controls.Add(lblFede);
            lblFede.Text = "Fédé :";
            lblFede.AutoSize = true;
            lblFede.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblFede.Font = Theme.Style.JoueurLabelsFont;
            lblFede.Location = new Point(172, 133);
            ToolTip t6 = new ToolTip();
            t6.InitialDelay = 1;
            t6.SetToolTip(lblFede, "Fédération");

            // Label Elo
            Controls.Add(lblElo);
            lblElo.Text = "Elo :";
            lblElo.AutoSize = true;
            lblElo.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblElo.Font = Theme.Style.JoueurLabelsFont;
            lblElo.Location = new Point(4, 164);
            ToolTip t7 = new ToolTip();
            t7.InitialDelay = 1;
            t7.SetToolTip(lblElo, "Classement elo");

            // Label Titre
            Controls.Add(lblTitre);
            lblTitre.Text = "Titre :";
            lblTitre.AutoSize = true;
            lblTitre.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblTitre.Font = Theme.Style.JoueurLabelsFont;
            lblTitre.Location = new Point(172, 164);
            ToolTip t8 = new ToolTip();
            t8.InitialDelay = 1;
            t8.SetToolTip(lblTitre, "Titre du joueur");

            // Label Club
            Controls.Add(lblClub);
            lblClub.Text = "Club :";
            lblClub.AutoSize = true;
            lblClub.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblClub.Font = Theme.Style.JoueurLabelsFont;
            lblClub.Location = new Point(4, 195);

            // Label Mail
            Controls.Add(lblMail);
            lblMail.Text = "Mail :";
            lblMail.AutoSize = true;
            lblMail.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblMail.Font = Theme.Style.JoueurLabelsFont;
            lblMail.Location = new Point(4, 226);

            // Label Frais
            Controls.Add(lblFrais);
            lblFrais.Text = "Frais :";
            lblFrais.AutoSize = true;
            lblFrais.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblFrais.Font = Theme.Style.JoueurLabelsFont;
            lblFrais.Location = new Point(4, 257);

            // Label Repas
            Controls.Add(lblRepas);
            lblRepas.Text = "Repas :";
            lblRepas.AutoSize = true;
            lblRepas.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblRepas.Font = Theme.Style.JoueurLabelsFont;
            lblRepas.Location = new Point(4, 288);

            // Label Open
            Controls.Add(lblOpen);
            lblOpen.Text = "Open :";
            lblOpen.AutoSize = true;
            lblOpen.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblOpen.Font = Theme.Style.JoueurLabelsFont;
            lblOpen.Location = new Point(4, 319);

            // Label À payer 
            Controls.Add(lblAPayer);
            lblAPayer.Text = "À payer :";
            lblAPayer.AutoSize = false;
            lblAPayer.TextAlign = ContentAlignment.MiddleCenter;
            lblAPayer.Size = new Size(300, 32);
            lblAPayer.ForeColor = Theme.Style.JoueurLabelsForeColor;
            lblAPayer.Font = Theme.Style.JoueurLabelsFont;
            lblAPayer.Location = new Point(3, 349);
        }

        /// <summary>
        /// Crée et paramètre les <see cref="CustomHiddenTextBox"/> et <see cref="CustomComboBox"/> du panel.
        /// </summary>
        private void CreateBoxes()
        {
            //
            // Hiden text boxes
            //

            // Hidden Fide
            Controls.Add(txtFideCode);
            txtFideCode.Size = new Size(103, 20);
            txtFideCode.MaxLength = 8;
            txtFideCode.Location = new Point(63, 71);
            txtFideCode.AllowNumbers = true;
            txtFideCode.Regex = new Regex(@"[0-9]{8}");
            txtFideCode.PlaceholderText = "12345678";
            txtFideCode.Info = "FideCode";

            // Hidden Ffe
            Controls.Add(txtNrFFE);
            txtNrFFE.Size = new Size(57, 20);
            txtNrFFE.MaxLength = 6;
            txtNrFFE.Location = new Point(231, 71);
            txtNrFFE.AllowNumbers = true;
            txtNrFFE.AllowLetters = true;
            txtNrFFE.Regex = new Regex(@"[a-zA-Z]{1}[0-9]{5}");
            txtNrFFE.PlaceholderText = "A12345";
            txtNrFFE.Info = "NrFFE";


            // Hidden Né le
            Controls.Add(txtNeLe);
            txtNeLe.Size = new Size(103, 20);
            txtNeLe.Location = new Point(63, 102);
            txtNeLe.Info = "NeLe";

            // Hidden Sexe
            Controls.Add(txtSexe);
            txtSexe.Size = new Size(57, 20);
            txtSexe.MaxLength = 1;
            txtSexe.Location = new Point(231, 102);
            txtSexe.AllowLetters = true;
            txtSexe.PlaceholderText = "F";
            txtSexe.Info = "Sexe";

            // Hidden Cat
            Controls.Add(txtCat);
            txtCat.Size = new Size(103, 20);
            txtCat.MaxLength = 5;
            txtCat.Location = new Point(63, 133);
            txtCat.AllowLetters = true;
            txtCat.PlaceholderText = "JunF";
            txtCat.Info = "Cat";

            // Hidden Fédé
            Controls.Add(txtFederation);
            txtFederation.Size = new Size(57, 20);
            txtFederation.MaxLength = 3;
            txtFederation.Location = new Point(231, 133);
            txtFederation.AllowLetters = true;
            txtFederation.PlaceholderText = "FRA";
            txtFederation.Info = "Federation";

            // Hidden Elo
            Controls.Add(txtElo);
            txtElo.Size = new Size(103, 20);
            txtElo.MaxLength = 4;
            txtElo.Location = new Point(63, 164);
            txtElo.AllowNumbers = true;
            txtElo.PlaceholderText = "1500";
            txtElo.Info = "Elo";

            // Hidden Titre
            Controls.Add(txtFideTitre);
            txtFideTitre.Size = new Size(57, 20);
            txtFideTitre.MaxLength = 2;
            txtFideTitre.Location = new Point(231, 164);
            txtFideTitre.AllowLetters = true;
            txtFideTitre.PlaceholderText = "m";
            txtFideTitre.Info = "FideTitre";

            // Hidden Mail
            Controls.Add(txtJoueurEmail);
            txtJoueurEmail.Size = new Size(225, 20);
            txtJoueurEmail.Location = new Point(63, 226);
            txtJoueurEmail.Info = "Email";

            // Hidden Frais
            Controls.Add(txtFraisInscription);
            txtFraisInscription.Size = new Size(225, 20);
            txtFraisInscription.Location = new Point(63, 257);
            txtFraisInscription.AllowNumbers = true;
            txtFraisInscription.AllowEuro = true;
            txtFraisInscription.AllowSpace = true;
            txtFraisInscription.Regex = new Regex(@"[0-9]+ ?€?");
            txtFraisInscription.PlaceholderText = "01 €";
            txtFraisInscription.Info = "FraisInscription";

            //
            // Repas Combo Box
            //

            // Hidden Club
            Controls.Add(ccbClubRef);
            ccbClubRef.Size = new Size(230, 20);
            ccbClubRef.Location = new Point(58, 191);
            ccbClubRef.AttributeToDisplay = "Nom";
            ccbClubRef.Info = "ClubRef";

            // Hidden Repas
            Controls.Add(rcbRepasRef);
            rcbRepasRef.Size = new Size(225, 20);
            rcbRepasRef.Location = new Point(63, 284);
            rcbRepasRef.AttributeToDisplay = "NomEtPrix";
            rcbRepasRef.Info = "RepasRef";

            // Hidden Open
            Controls.Add(ocbOpenRef);
            ocbOpenRef.Size = new Size(225, 20);
            ocbOpenRef.Location = new Point(63, 316);
            ocbOpenRef.AttributeToDisplay = "Nom";
            ocbOpenRef.Info = "OpenRef";


        }



        /// <summary>
        /// Crée les évènements du panel et de ses controls.
        /// </summary>
        private void AddEvents()
        {
            rcbRepasRef.SelectedValueChanged += RcbRepasRefSelectedValueChanged;
            txtFraisInscription.Validated += HtbFraisInscriptionValidated;
            txtNeLe.Validated += HtbNeLe_Validated;
            btnDetails.Click += (object sender, EventArgs e) =>
            {
                if (CChesstion.JoueurSelectionne == null)
                    return;

                DetailsJoueurDialog d = new DetailsJoueurDialog(CChesstion.JoueurSelectionne.Ref);
                d.ShowDialog();
            };

            foreach (CustomHiddenTextBox htb in TextBoxes)
            {
                htb.Unvalidated += Htb_Unvalidated;
            }
        }

        /// <summary>
        /// Redimensionne et repositionne le panneau en fonction de la taille de la fenêtre.
        /// </summary>
        public void RelocateAndResize()
        {
            Size = new Size((int) (DPI.Instance.RelativeMultiplier.X*300), (int)((Parent.ClientSize.Height - (CChesstion.MsMenu.Height + 2)) * 0.66) - 2);
            Location = new Point(Parent.ClientSize.Width - Size.Width, CChesstion.MsMenu.Height + 2);
            RelocateButtons();
        }

        /// <summary>
        /// Repositionne les boutons.
        /// </summary>
        private void RelocateButtons()
        {
            //btnDetails.Location = new System.Drawing.Point(160, this.Size.Height - 60);
            btnDetails.Location = new Point((int)(160 * DPI.Instance.RelativeMultiplier.X), Size.Height - (int)(60 * DPI.Instance.RelativeMultiplier.Y));
            btnConfirmer.Location = new Point((int)(31 * DPI.Instance.RelativeMultiplier.X), Size.Height - (int)(60 * DPI.Instance.RelativeMultiplier.Y));
        }

        /// <summary>
        /// Active ou désactive les boutons.
        /// </summary>
        /// <param name="value"></param>
        public void EnabledButtons(bool value = true)
        {
            btnConfirmer.Enabled = value;
            btnDetails.Enabled = value;
        }



        //
        // Gestion des Boxes
        //



        /// <summary>
        /// Remplit les <see cref="Boxes" /> du panel avec les données de <see cref="CChesstion.JoueurSelectionne"/>.
        /// </summary>
        public void LoadInfoToPanel()
        {
            Reset();
            isHandling = false;

            if (CChesstion.JoueurSelectionne == null)
                return;
            CChesstion.JoueurErrorsCache.Remove(CChesstion.JoueurSelectionne.Ref);
            ReadOnly = false;
            CheckAllTextBoxesPlaceHolders(true);

            Debug.WriteLine("Réf LoadInfoToPanel " + CChesstion.JoueurSelectionne.Ref);
            foreach (Control c in Boxes)
            {
                if (c is CustomComboBox) ((CustomComboBox)c).Ref = CChesstion.JoueurSelectionne.Ref;
                if (c is CustomHiddenTextBox) ((CustomHiddenTextBox)c).Ref = CChesstion.JoueurSelectionne.Ref;
            }

            UpdateTotalAPayer();

            Title = CChesstion.JoueurSelectionne.Nom + " " + CChesstion.JoueurSelectionne.Prenom;

            isHandling = true;

            ConfirmeJoueur(CChesstion.JoueurSelectionne.Confirme);

            CheckAllTextBoxesPlaceHolders(false);
        }

        /// <summary>
        /// Charge les données des <see cref="Boxes"/> dans <see cref="CChesstion.JoueurSelectionne"/>.
        /// </summary>
        /// <param name="resetPanel">Vrai appelle <see cref="Reset"/>.</param>
        public void LoadInfoFromPanel(bool resetPanel = false)
        {
            if (string.IsNullOrEmpty(txtFideCode.LastValue))
                return;
            //CChesstion.JoueurSelectionne.ClubRef = int.Parse(ccbClubRef.SelectedValue.ToString());
            if (!CChesstion.JoueurSelectionne.TotalAPayer().Equals(float.Parse(lblAPayer.Tag.ToString())))
                CChesstion.JoueurSelectionne.SetFraisManually(float.Parse(txtFraisInscription.Text.Replace("€", string.Empty).Trim()));
            //CChesstion.JoueurSelectionne.RepasRef = int.Parse(rcbRepasRef.SelectedValue.ToString());
            //CChesstion.JoueurSelectionne.Confirme = (bool) btnConfirmer.Tag;
            //CChesstion.JoueurSelectionne.OpenRef = int.Parse(ocbOpenRef.SelectedValue.ToString());

            if (resetPanel)
                Reset();
        }



        /// <summary>
        /// Appelle les fonctions en charge de vider l'ensemble des <see cref="Boxes"/> du panel.
        /// </summary>
        public void Reset()
        {
            isHandling = false;
            ReadOnly = true;
            EmptyBoxes();
            lblAPayer.Text = string.Empty;
            isHandling = true;
            ContextMenu = null;
        }

        /// <summary>
        /// Vide l'ensemble des <see cref="Boxes"/> du panel.
        /// </summary>
        private void EmptyBoxes()
        {
            foreach (CustomHiddenTextBox tb in TextBoxes)
                EmptyTextBox(tb);
            foreach (HiddenComboBox hcb in ComboBoxes)
            {
                if (hcb is CustomComboBox)
                {
                    CustomComboBox ccb = (CustomComboBox) hcb;
                    ccb.Ref = -1;
                }
                try { hcb.SelectedIndex = -1; }
                catch { try { hcb.SelectedIndex = 0; }
                    catch { try { hcb.SelectedIndex = 1; } catch { } }
                }
            }
            Title = "Joueur";
            HeaderForeColor = Theme.Style.JoueurHeaderForeColor;
            lblAPayer.ForeColor = Theme.Style.JoueurLabelsForeColor;
            ConfirmeJoueur(false);

        }

        /// <summary>
        /// Vide le control.
        /// </summary>
        /// <param name="c"><see cref="Control"/> à vider</param>
        public void EmptyTextBox(Control c)
        {
            if (!Boxes.Contains(c))
                throw new ArgumentException("EmptyTextBox - La box donnée en paramètre ne correspond à aucune box du panel Joueur");

            if (c is HiddenTextBox)
            {
                HiddenTextBox chtb = (HiddenTextBox)c;
                chtb.Text = string.Empty;
                chtb.LastValue = string.Empty;
                chtb.CheckPlaceHolder(false);
            }
        }

        /// <summary>
        /// Vérifie l'ensemble des PlaceHolders des <see cref="CustomHiddenTextBox"/> du panel.
        /// </summary>
        /// <param name="gotFocus">Vrai si <see cref="CustomHiddenTextBox"/> a été focus</param>
        private void CheckAllTextBoxesPlaceHolders(bool gotFocus)
        {
            foreach (HiddenTextBox chtb in TextBoxes)
                chtb.CheckPlaceHolder(gotFocus);
        }



        /// <summary>
        /// Met à jour le panel en fonction de l'état de confirmation du joueur.
        /// </summary>
        /// <param name="confirme">Vrai si le joueur est confirmé</param>
        public void ConfirmeJoueur(bool confirme)
        {
            if (confirme)
            {
                ReadOnly = true;
                btnConfirmer.Text = "Annuler";
                btnConfirmer.Tag = true;
                HeaderForeColor = Theme.Style.JoueurHeaderConfirmedForeColor;
                lblAPayer.ForeColor = Theme.Style.JoueurLabelsConfirmedForeColor;
                JoueurConfirmeChanged?.Invoke(this, new EventArgs());
                ContextMenu = null;
            }
            else
            {
                if (CChesstion.JoueurSelectionne != null)
                {
                    ReadOnly = false;
                    ContextMenu = new JoueurContextMenu(CChesstion.JoueurSelectionne.Ref, true, false);
                    CChesstion.DisplayJoueurErrored(CChesstion.JoueurSelectionne.Ref,
                        CChesstion.IsJoueurErrored(CChesstion.JoueurSelectionne.Ref));
                }
                btnConfirmer.Text = "Confirmer";
                btnConfirmer.Tag = false;
                HeaderForeColor = Theme.Style.JoueurHeaderForeColor;
                lblAPayer.ForeColor = Theme.Style.JoueurLabelsForeColor;
                JoueurConfirmeChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Met à jour le total à payer.
        /// </summary>
        public void UpdateTotalAPayer()
        {
            if (CChesstion.JoueurSelectionne != null)
            {
                lblAPayer.Text = "À payer : " + CChesstion.JoueurSelectionne.TotalAPayer().ToString("F") + " €";
                lblAPayer.Tag = CChesstion.JoueurSelectionne.TotalAPayer();
            }
            else
            {
                lblAPayer.Text = "";
                lblAPayer.Tag = null;
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

        //
        // Evènements implentés
        //

        /// <summary>
        /// Confirme ou annule la confirmation du joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirmer_Click(object sender, EventArgs e)
        {
            if (CChesstion.JoueurSelectionne != null && isHandling)
                CChesstion.ConfimerJoueur(CChesstion.JoueurSelectionne.Ref, !(bool) btnConfirmer.Tag);
        }

        /// <summary>
        /// Met à jour le montant à payer par le joueur. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RcbRepasRefSelectedValueChanged(object sender, EventArgs e)
        {
            UpdateTotalAPayer();
        }

        /// <summary>
        /// Modifie les frais d'inscription.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HtbFraisInscriptionValidated(object sender, EventArgs e)
        {
            /*if (!txtFraisInscription.Text.Contains("€"))
                txtFraisInscription.Text += " €";*/

            CChesstion.JoueurSelectionne.SetFraisManually(float.Parse(txtFraisInscription.Text.Replace("€", string.Empty).Trim()));
            UpdateTotalAPayer();
        }

        /// <summary>
        /// Formatte la date de naissance et met à jour le montant à payer par le joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HtbNeLe_Validated(object sender, EventArgs e)
        {
            txtNeLe.Text = txtNeLe.Text.Replace('-', '/').Replace('.', '/');
            CChesstion.JoueurSelectionne.NeLe = txtNeLe.Text;
            txtFraisInscription.NewValue = CChesstion.JoueurSelectionne.FraisInscription + " €";
            UpdateTotalAPayer();
        }

        /// <summary>
        /// Affiche un message d'erreur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Htb_Unvalidated(object sender, EventArgs e)
        {
            HiddenTextBox htb = (HiddenTextBox) sender;

            CustomQuickDialog cqd = new CustomQuickDialog(
                htb.LastErrorMessage,
                GeneralControls.CustomDialogs.QuickDialogType.Error,
                htb,
                GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOnRightOfParent);
            cqd.DisplayDelay = 2000;
            cqd.Show();
        }

    }
}
