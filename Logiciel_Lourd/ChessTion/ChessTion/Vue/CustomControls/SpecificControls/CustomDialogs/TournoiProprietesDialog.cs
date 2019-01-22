using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up qui affiche les propriétés d'un <see cref="Tournoi"/> et permet d'en modifier les valeurs.
    /// </summary>
    class TournoiProprietesDialog : Form
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
        /// Textbox de référence du tournoi.
        /// </summary>
        private ControlledTextBox TxtRef;

        /// <summary>
        /// Label de référence du tournoi.
        /// </summary>
        private Label LblRef;

        /// <summary>
        /// Label de nom du tournoi.
        /// </summary>
        private Label LblNom;

        /// <summary>
        /// Textbox de nom du tournoi.
        /// </summary>
        private ControlledTextBox TxtNom;

        /// <summary>
        /// Label de date du tournoi.
        /// </summary>
        private Label LblDateDebut;

        /// <summary>
        /// Textbox de date du début du tournoi.
        /// </summary>
        private ControlledTextBox TxtDateDebut;

        /// <summary>
        /// Label de date de fin du tournoi.
        /// </summary>
        private Label LblDateFin;

        /// <summary>
        /// Textbox de date de fin du tounoi.
        /// </summary>
        private ControlledTextBox TxtDateFin;

        /// <summary>
        /// Label du tarif standard du tournoi.
        /// </summary>
        private Label LblPrixVieux;

        /// <summary>
        /// Textbox du tarif standard du tournoi.
        /// </summary>
        private ControlledTextBox TxtPrixVieux;

        /// <summary>
        /// Label du tarif réduit du tournoi.
        /// </summary>
        private Label LblPrixJeune;

        /// <summary>
        /// Textbox du tarif réduit du tournoi.
        /// </summary>
        private ControlledTextBox TxtPrixJeune;

        /// <summary>
        /// Label de limite d'âge du tarif réduit du tournoi.
        /// </summary>
        private Label LblLimiteAge;

        /// <summary>
        /// Textbox de la limite d'âge du tarif réduit du tournoi.
        /// </summary>
        private ControlledTextBox TxtLimiteAge;

        /// <summary>
        /// Label de majoration du tournoi.
        /// </summary>
        private Label LblMajoration;

        /// <summary>
        /// Textbox de la majoration du tournoi.
        /// </summary>
        private ControlledTextBox TxtMajoration;

        /// <summary>
        /// Label de titre de tarif.
        /// </summary>
        private Label LblTitreTarif;

        /// <summary>
        /// Label de titre du tournoi.
        /// </summary>
        private Label LblTitreTournoi;

        /// <summary>
        /// Label du mail de l'arbitre.
        /// </summary>
        private Label LblArbitre;

        /// <summary>
        /// Textbox du mail de l'arbitre.
        /// </summary>
        private ControlledTextBox TxtArbitre;

        /// <summary>
        /// Label de durée d'une ronde du tournoi.
        /// </summary>
        private Label LblDureeRonde;

        /// <summary>
        /// Textbox de durée d'une ronde du tournoi.
        /// </summary>
        private ControlledTextBox TxtDureeRonde;

        /// <summary>
        /// Label de nombre de rondes du tournoi.
        /// </summary>
        private Label LblNbRondes;

        /// <summary>
        /// Textbox de nombre de rondes du tournoi.
        /// </summary>
        private ControlledTextBox TxtNbRondes;

        /// <summary>
        /// Label 
        /// </summary>
        private Label LblMaxParticipants;
        private ControlledTextBox TxtMaxParticipants;
        private Button BtnCancel;
        private Button BtnOk;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label LblTitle;
        private CheckBox CheckConfirmMail;
        private int refTournoi;
        private bool confirm = false;
        private string confirmMessage = "";
        private CustomQuickDialog q;

        public TournoiProprietesDialog(int refTournoi)
        {
            InitializeComponent();

            this.refTournoi = refTournoi;

            Tournoi t = GTournoi.GetTournoi(refTournoi);

            TxtRef.NewValue = t.Ref.ToString();
            TxtNom.NewValue = t.Nom;
            TxtDateDebut.NewValue = t.DateDebut.ToShortDateString();
            TxtDateFin.NewValue = t.DateFin.ToShortDateString();

            TxtPrixVieux.NewValue = t.PrixVieux.ToString();
            TxtPrixJeune.NewValue = t.PrixJeune.ToString();
            TxtLimiteAge.NewValue = t.LimiteAge.ToString();
            TxtMajoration.NewValue = t.Majoration.ToString();

            TxtMaxParticipants.NewValue = t.MaxParticipants.ToString();
            TxtNbRondes.NewValue = t.NbRondes.ToString();
            TxtDureeRonde.NewValue = t.DureeRondeMinutes + ":" + t.DureeRondeSecondes;
            TxtArbitre.NewValue = t.Arbitre;

            CheckConfirmMail.Checked = t.ConfirmMail;

            foreach (Control c in this.Controls)
                if (c is ControlledTextBox)
                {
                    ((ControlledTextBox) c).CheckPlaceHolder(false);
                    ((ControlledTextBox)c).KeyPress += TournoiProprietesDialog_KeyPress;
                    ((ControlledTextBox)c).Validated += TournoiProprietesDialog_Validated;
                }

            CheckConfirmMail.KeyPress += TournoiProprietesDialog_KeyPress;
            BtnCancel.Click += (object sender, EventArgs e) => { DialogResult = DialogResult.Cancel; Close(); };
            BtnOk.Click += BtnOk_Click;

            if (t.Etat > 1)
            {
                TxtNom.ReadOnly = true;
                TxtDateDebut.ReadOnly = true;
                TxtDateFin.ReadOnly = true;

                TxtPrixVieux.ReadOnly = true;
                TxtPrixJeune.ReadOnly = true;
                TxtLimiteAge.ReadOnly = true;
                TxtMajoration.ReadOnly = true;

                TxtMaxParticipants.ReadOnly = true;
                TxtNbRondes.ReadOnly = true;
                TxtDureeRonde.ReadOnly = true;
            }
        }

        private void TournoiProprietesDialog_Validated(object sender, EventArgs e)
        {
            ((ControlledTextBox)sender).NewValue = ((ControlledTextBox)sender).Text;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = TxtNom.LastValue;
                DateTime date = Convert.ToDateTime(TxtDateDebut.LastValue);
                DateTime dateFin = Convert.ToDateTime(TxtDateFin.LastValue);

                if (dateFin < date)
                {
                    q =
                        new CustomQuickDialog("La date de fin du tournoi est\ninférieure à la date de début !",
                            GeneralControls.CustomDialogs.QuickDialogType.Error, BtnOk,
                            GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOnParent);
                    q.DisplayDelay = 5000;
                    q.Show();
                    return;
                }

                if (date < DateTime.Today)
                {
                    q =
                        new CustomQuickDialog("La date de début du tournoi\nest déjà passée.",
                            GeneralControls.CustomDialogs.QuickDialogType.Error, BtnOk,
                            GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOnParent);
                    q.DisplayDelay = 5000;
                    q.Show();
                    return;
                }


                

                float prixVieux = float.Parse(TxtPrixVieux.LastValue.Replace('.', ','));
                float prixJeune = float.Parse(TxtPrixJeune.LastValue.Replace('.', ','));
                int limiteAge = int.Parse(TxtLimiteAge.LastValue);
                float majoration = float.Parse(TxtMajoration.LastValue.Replace('.', ','));

                

                int maxParticipants = int.Parse(TxtMaxParticipants.LastValue);
                int nbRondes = int.Parse(TxtNbRondes.LastValue);
                string dureeRondes = TxtDureeRonde.LastValue;
                string arbitre = TxtArbitre.LastValue;

                


                int dureeRondeMinutes = int.Parse(dureeRondes.Split(':').First());
                int dureeRondeSecondes = int.Parse(dureeRondes.Split(':').Last());

                bool confirmMail = CheckConfirmMail.Checked;

                // Gestion des avertissements.
                if (prixJeune > prixVieux)
                    confirmMessage += "Attention, le tarif réduit jeune est supérieur au tarif standard.\n";
                if (maxParticipants < 2)
                    confirmMessage += "Attention, le nombre de participants maximal est inférieur à 2.\n";
                if (limiteAge > 100)
                    confirmMessage += "Attention, la limite d'âge au-dessous laquelle s'applique le tarif réduit jeune semble très élevée.\n";
                if (dureeRondeMinutes == 0 && dureeRondeSecondes == 0)
                    confirmMessage += "Attention, la durée de la ronde est égale à zéro.\n";
                if (nbRondes == 0)
                    confirmMessage += "Attention, vous avez indiqué qu'il n'y aura aucune ronde.\n";

                if (!confirmMessage.Equals("") && !confirm)
                {
                    q =
                        new CustomQuickDialog(
                            confirmMessage + "Est-ce voulu ? (Cliquez à nouveau pour valider)",
                            GeneralControls.CustomDialogs.QuickDialogType.Warning, BtnOk,
                            GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
                    q.DisplayDelay = 5000;
                    q.Show();
                    confirm = true;
                    return;
                }
                else
                {
                    if (q != null && q.Visible)
                        q.Close();
                    confirm = false;
                }

                Tournoi t = GTournoi.GetTournoi(refTournoi);
                t.Nom = nom;
                t.DateDebut = date;
                t.DateFin = dateFin;

                t.PrixVieux = prixVieux;
                t.PrixJeune = prixJeune;
                t.LimiteAge = limiteAge;
                t.Majoration = majoration;

                t.MaxParticipants = maxParticipants;
                t.NbRondes = nbRondes;
                t.DureeRondeMinutes = dureeRondeMinutes;
                t.DureeRondeSecondes = dureeRondeSecondes;
                t.Arbitre = arbitre;

                t.ConfirmMail = confirmMail;

                CChesstion.CentrePanel.Title = t.Nom + " - " + CChesstion.OpenSelectionne.TitreFormatte();
                CChesstion.LoadOpenToPanels();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue. Les valeurs entrées sont-elles cohérentes ?\n\n" + ex.Message + "\n" + ex.StackTrace);
            }


        }

        private void TournoiProprietesDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TournoiProprietesDialog));
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblRef = new System.Windows.Forms.Label();
            this.LblNom = new System.Windows.Forms.Label();
            this.LblDateDebut = new System.Windows.Forms.Label();
            this.LblDateFin = new System.Windows.Forms.Label();
            this.LblPrixVieux = new System.Windows.Forms.Label();
            this.LblPrixJeune = new System.Windows.Forms.Label();
            this.LblLimiteAge = new System.Windows.Forms.Label();
            this.LblMajoration = new System.Windows.Forms.Label();
            this.LblTitreTarif = new System.Windows.Forms.Label();
            this.LblTitreTournoi = new System.Windows.Forms.Label();
            this.LblArbitre = new System.Windows.Forms.Label();
            this.LblDureeRonde = new System.Windows.Forms.Label();
            this.LblNbRondes = new System.Windows.Forms.Label();
            this.LblMaxParticipants = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CheckConfirmMail = new System.Windows.Forms.CheckBox();
            this.TxtArbitre = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtDureeRonde = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtNbRondes = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtMaxParticipants = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtMajoration = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtLimiteAge = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtPrixJeune = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtPrixVieux = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtDateFin = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtDateDebut = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtNom = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.TxtRef = new ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes.ControlledTextBox();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitle.Location = new System.Drawing.Point(143, 9);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(142, 20);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "Général";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblRef
            // 
            this.LblRef.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblRef.Location = new System.Drawing.Point(12, 45);
            this.LblRef.Name = "LblRef";
            this.LblRef.Size = new System.Drawing.Size(125, 20);
            this.LblRef.TabIndex = 2;
            this.LblRef.Text = "Référence";
            this.LblRef.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblNom
            // 
            this.LblNom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNom.Location = new System.Drawing.Point(12, 78);
            this.LblNom.Name = "LblNom";
            this.LblNom.Size = new System.Drawing.Size(125, 20);
            this.LblNom.TabIndex = 4;
            this.LblNom.Text = "Nom";
            this.LblNom.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblDateDebut
            // 
            this.LblDateDebut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblDateDebut.Location = new System.Drawing.Point(12, 111);
            this.LblDateDebut.Name = "LblDateDebut";
            this.LblDateDebut.Size = new System.Drawing.Size(125, 20);
            this.LblDateDebut.TabIndex = 6;
            this.LblDateDebut.Text = "Date de début";
            this.LblDateDebut.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblDateFin
            // 
            this.LblDateFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblDateFin.Location = new System.Drawing.Point(12, 144);
            this.LblDateFin.Name = "LblDateFin";
            this.LblDateFin.Size = new System.Drawing.Size(125, 20);
            this.LblDateFin.TabIndex = 8;
            this.LblDateFin.Text = "Date de fin";
            this.LblDateFin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblPrixVieux
            // 
            this.LblPrixVieux.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblPrixVieux.Location = new System.Drawing.Point(12, 223);
            this.LblPrixVieux.Name = "LblPrixVieux";
            this.LblPrixVieux.Size = new System.Drawing.Size(125, 20);
            this.LblPrixVieux.TabIndex = 10;
            this.LblPrixVieux.Text = "Prix";
            this.LblPrixVieux.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblPrixJeune
            // 
            this.LblPrixJeune.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblPrixJeune.Location = new System.Drawing.Point(12, 256);
            this.LblPrixJeune.Name = "LblPrixJeune";
            this.LblPrixJeune.Size = new System.Drawing.Size(125, 20);
            this.LblPrixJeune.TabIndex = 12;
            this.LblPrixJeune.Text = "Prix jeune";
            this.LblPrixJeune.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblLimiteAge
            // 
            this.LblLimiteAge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblLimiteAge.Location = new System.Drawing.Point(12, 289);
            this.LblLimiteAge.Name = "LblLimiteAge";
            this.LblLimiteAge.Size = new System.Drawing.Size(125, 20);
            this.LblLimiteAge.TabIndex = 14;
            this.LblLimiteAge.Text = "Limite âge jeune";
            this.LblLimiteAge.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblMajoration
            // 
            this.LblMajoration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblMajoration.Location = new System.Drawing.Point(12, 322);
            this.LblMajoration.Name = "LblMajoration";
            this.LblMajoration.Size = new System.Drawing.Size(125, 20);
            this.LblMajoration.TabIndex = 16;
            this.LblMajoration.Text = "Majoration";
            this.LblMajoration.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblTitreTarif
            // 
            this.LblTitreTarif.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitreTarif.Location = new System.Drawing.Point(143, 188);
            this.LblTitreTarif.Name = "LblTitreTarif";
            this.LblTitreTarif.Size = new System.Drawing.Size(142, 20);
            this.LblTitreTarif.TabIndex = 19;
            this.LblTitreTarif.Text = "Tarifs";
            this.LblTitreTarif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTitreTournoi
            // 
            this.LblTitreTournoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitreTournoi.Location = new System.Drawing.Point(435, 10);
            this.LblTitreTournoi.Name = "LblTitreTournoi";
            this.LblTitreTournoi.Size = new System.Drawing.Size(142, 20);
            this.LblTitreTournoi.TabIndex = 37;
            this.LblTitreTournoi.Text = "Tournoi";
            this.LblTitreTournoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblArbitre
            // 
            this.LblArbitre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblArbitre.Location = new System.Drawing.Point(304, 144);
            this.LblArbitre.Name = "LblArbitre";
            this.LblArbitre.Size = new System.Drawing.Size(125, 20);
            this.LblArbitre.TabIndex = 36;
            this.LblArbitre.Text = "Mail orga";
            this.LblArbitre.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblDureeRonde
            // 
            this.LblDureeRonde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblDureeRonde.Location = new System.Drawing.Point(304, 111);
            this.LblDureeRonde.Name = "LblDureeRonde";
            this.LblDureeRonde.Size = new System.Drawing.Size(125, 20);
            this.LblDureeRonde.TabIndex = 34;
            this.LblDureeRonde.Text = "Durée ronde";
            this.LblDureeRonde.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            ToolTip t = new ToolTip();
            t.InitialDelay = 1;
            t.SetToolTip(LblDureeRonde, "20:10 = 20 minutes et 10 secondes");
            // 
            // LblNbRondes
            // 
            this.LblNbRondes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblNbRondes.Location = new System.Drawing.Point(304, 78);
            this.LblNbRondes.Name = "LblNbRondes";
            this.LblNbRondes.Size = new System.Drawing.Size(125, 20);
            this.LblNbRondes.TabIndex = 32;
            this.LblNbRondes.Text = "Nombre rondes";
            this.LblNbRondes.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LblMaxParticipants
            // 
            this.LblMaxParticipants.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblMaxParticipants.Location = new System.Drawing.Point(304, 45);
            this.LblMaxParticipants.Name = "LblMaxParticipants";
            this.LblMaxParticipants.Size = new System.Drawing.Size(125, 20);
            this.LblMaxParticipants.TabIndex = 30;
            this.LblMaxParticipants.Text = "Max joueurs";
            this.LblMaxParticipants.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Location = new System.Drawing.Point(326, 301);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(109, 45);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Annuler";
            this.BtnCancel.UseVisualStyleBackColor = false;
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOk.Location = new System.Drawing.Point(456, 301);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(109, 45);
            this.BtnOk.TabIndex = 100;
            this.BtnOk.Text = "Valider";
            this.BtnOk.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 20);
            this.label3.TabIndex = 40;
            this.label3.Text = "€";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 20);
            this.label4.TabIndex = 41;
            this.label4.Text = "€";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(291, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 20);
            this.label6.TabIndex = 42;
            this.label6.Text = "€";
            // 
            // CheckConfirmMail
            // 
            this.CheckConfirmMail.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CheckConfirmMail.Location = new System.Drawing.Point(326, 210);
            this.CheckConfirmMail.Name = "CheckConfirmMail";
            this.CheckConfirmMail.Size = new System.Drawing.Size(251, 66);
            this.CheckConfirmMail.TabIndex = 12;
            this.CheckConfirmMail.Text = "Recevoir une copie des inscriptions par mail";
            this.CheckConfirmMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckConfirmMail.UseVisualStyleBackColor = true;
            // 
            // TxtArbitre
            // 
            this.TxtArbitre.AllowControls = true;
            this.TxtArbitre.AllowDecimal = false;
            this.TxtArbitre.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtArbitre.AllowedCharacters")));
            this.TxtArbitre.AllowEmpty = true;
            this.TxtArbitre.AllowEuro = false;
            this.TxtArbitre.AllowLetters = true;
            this.TxtArbitre.AllowNumbers = true;
            this.TxtArbitre.AllowSpace = false;
            this.TxtArbitre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtArbitre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtArbitre.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtArbitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtArbitre.LastValue = null;
            this.TxtArbitre.Location = new System.Drawing.Point(435, 141);
            this.TxtArbitre.Name = "TxtArbitre";
            this.TxtArbitre.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtArbitre.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtArbitre.PlaceholderText = "arbitre@arbitre.com";
            this.TxtArbitre.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtArbitre.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtArbitre.Regex = new System.Text.RegularExpressions.Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            this.TxtArbitre.Size = new System.Drawing.Size(142, 27);
            this.TxtArbitre.TabIndex = 11;
            // 
            // TxtDureeRonde
            // 
            this.TxtDureeRonde.AllowControls = true;
            this.TxtDureeRonde.AllowDecimal = false;
            this.TxtDureeRonde.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtDureeRonde.AllowedCharacters")));
            this.TxtDureeRonde.AllowEmpty = false;
            this.TxtDureeRonde.AllowEuro = false;
            this.TxtDureeRonde.AllowLetters = false;
            this.TxtDureeRonde.AllowNumbers = true;
            this.TxtDureeRonde.AllowSpace = false;
            this.TxtDureeRonde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtDureeRonde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDureeRonde.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtDureeRonde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtDureeRonde.LastValue = null;
            this.TxtDureeRonde.Location = new System.Drawing.Point(435, 108);
            this.TxtDureeRonde.Name = "TxtDureeRonde";
            this.TxtDureeRonde.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtDureeRonde.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtDureeRonde.PlaceholderText = "50:30";
            this.TxtDureeRonde.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtDureeRonde.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtDureeRonde.Regex = new System.Text.RegularExpressions.Regex("[0-9]?[0-9]:[0-9]?[0-9]");
            this.TxtDureeRonde.Size = new System.Drawing.Size(142, 27);
            this.TxtDureeRonde.TabIndex = 10;
            this.TxtDureeRonde.MaxLength = 5;
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 1000;
            tt.SetToolTip(TxtDureeRonde, "20:10 = 20 minutes et 10 secondes");
            // 
            // TxtNbRondes
            // 
            this.TxtNbRondes.AllowControls = true;
            this.TxtNbRondes.AllowDecimal = false;
            this.TxtNbRondes.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtNbRondes.AllowedCharacters")));
            this.TxtNbRondes.AllowEmpty = false;
            this.TxtNbRondes.AllowEuro = false;
            this.TxtNbRondes.AllowLetters = false;
            this.TxtNbRondes.AllowNumbers = true;
            this.TxtNbRondes.AllowSpace = false;
            this.TxtNbRondes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNbRondes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNbRondes.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNbRondes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNbRondes.LastValue = null;
            this.TxtNbRondes.Location = new System.Drawing.Point(435, 75);
            this.TxtNbRondes.Name = "TxtNbRondes";
            this.TxtNbRondes.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNbRondes.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNbRondes.PlaceholderText = "2";
            this.TxtNbRondes.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNbRondes.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNbRondes.Regex = new System.Text.RegularExpressions.Regex("^[0-9]+");
            this.TxtNbRondes.Size = new System.Drawing.Size(142, 27);
            this.TxtNbRondes.TabIndex = 9;
            // 
            // TxtMaxParticipants
            // 
            this.TxtMaxParticipants.AllowControls = true;
            this.TxtMaxParticipants.AllowDecimal = false;
            this.TxtMaxParticipants.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtMaxParticipants.AllowedCharacters")));
            this.TxtMaxParticipants.AllowEmpty = false;
            this.TxtMaxParticipants.AllowEuro = false;
            this.TxtMaxParticipants.AllowLetters = false;
            this.TxtMaxParticipants.AllowNumbers = true;
            this.TxtMaxParticipants.AllowSpace = false;
            this.TxtMaxParticipants.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtMaxParticipants.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMaxParticipants.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtMaxParticipants.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtMaxParticipants.LastValue = null;
            this.TxtMaxParticipants.Location = new System.Drawing.Point(435, 42);
            this.TxtMaxParticipants.Name = "TxtMaxParticipants";
            this.TxtMaxParticipants.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtMaxParticipants.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtMaxParticipants.PlaceholderText = "100";
            this.TxtMaxParticipants.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtMaxParticipants.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtMaxParticipants.Regex = new System.Text.RegularExpressions.Regex("^[0-9]+");
            this.TxtMaxParticipants.Size = new System.Drawing.Size(142, 27);
            this.TxtMaxParticipants.TabIndex = 8;
            // 
            // TxtMajoration
            // 
            this.TxtMajoration.AllowControls = true;
            this.TxtMajoration.AllowDecimal = true;
            this.TxtMajoration.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtMajoration.AllowedCharacters")));
            this.TxtMajoration.AllowEmpty = false;
            this.TxtMajoration.AllowEuro = false;
            this.TxtMajoration.AllowLetters = false;
            this.TxtMajoration.AllowNumbers = true;
            this.TxtMajoration.AllowSpace = false;
            this.TxtMajoration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtMajoration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMajoration.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtMajoration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtMajoration.LastValue = null;
            this.TxtMajoration.Location = new System.Drawing.Point(143, 319);
            this.TxtMajoration.Name = "TxtMajoration";
            this.TxtMajoration.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtMajoration.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtMajoration.PlaceholderText = "2";
            this.TxtMajoration.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtMajoration.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtMajoration.Regex = new System.Text.RegularExpressions.Regex("^[0-9]+");
            this.TxtMajoration.Size = new System.Drawing.Size(142, 27);
            this.TxtMajoration.TabIndex = 7;
            // 
            // TxtLimiteAge
            // 
            this.TxtLimiteAge.AllowControls = true;
            this.TxtLimiteAge.AllowDecimal = false;
            this.TxtLimiteAge.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtLimiteAge.AllowedCharacters")));
            this.TxtLimiteAge.AllowEmpty = true;
            this.TxtLimiteAge.AllowEuro = false;
            this.TxtLimiteAge.AllowLetters = false;
            this.TxtLimiteAge.AllowNumbers = true;
            this.TxtLimiteAge.AllowSpace = false;
            this.TxtLimiteAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtLimiteAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLimiteAge.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtLimiteAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtLimiteAge.LastValue = null;
            this.TxtLimiteAge.Location = new System.Drawing.Point(143, 286);
            this.TxtLimiteAge.Name = "TxtLimiteAge";
            this.TxtLimiteAge.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtLimiteAge.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtLimiteAge.PlaceholderText = "16";
            this.TxtLimiteAge.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtLimiteAge.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtLimiteAge.Regex = new System.Text.RegularExpressions.Regex("^[0-9]+");
            this.TxtLimiteAge.Size = new System.Drawing.Size(142, 27);
            this.TxtLimiteAge.TabIndex = 6;
            // 
            // TxtPrixJeune
            // 
            this.TxtPrixJeune.AllowControls = true;
            this.TxtPrixJeune.AllowDecimal = true;
            this.TxtPrixJeune.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtPrixJeune.AllowedCharacters")));
            this.TxtPrixJeune.AllowEmpty = false;
            this.TxtPrixJeune.AllowEuro = false;
            this.TxtPrixJeune.AllowLetters = false;
            this.TxtPrixJeune.AllowNumbers = true;
            this.TxtPrixJeune.AllowSpace = false;
            this.TxtPrixJeune.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtPrixJeune.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrixJeune.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtPrixJeune.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtPrixJeune.LastValue = null;
            this.TxtPrixJeune.Location = new System.Drawing.Point(143, 253);
            this.TxtPrixJeune.Name = "TxtPrixJeune";
            this.TxtPrixJeune.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtPrixJeune.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtPrixJeune.PlaceholderText = "5";
            this.TxtPrixJeune.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtPrixJeune.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtPrixJeune.Regex = new System.Text.RegularExpressions.Regex("^[0-9]+");
            this.TxtPrixJeune.Size = new System.Drawing.Size(142, 27);
            this.TxtPrixJeune.TabIndex = 5;
            // 
            // TxtPrixVieux
            // 
            this.TxtPrixVieux.AllowControls = true;
            this.TxtPrixVieux.AllowDecimal = true;
            this.TxtPrixVieux.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtPrixVieux.AllowedCharacters")));
            this.TxtPrixVieux.AllowEmpty = false;
            this.TxtPrixVieux.AllowEuro = false;
            this.TxtPrixVieux.AllowLetters = false;
            this.TxtPrixVieux.AllowNumbers = true;
            this.TxtPrixVieux.AllowSpace = false;
            this.TxtPrixVieux.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtPrixVieux.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrixVieux.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtPrixVieux.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtPrixVieux.LastValue = null;
            this.TxtPrixVieux.Location = new System.Drawing.Point(143, 220);
            this.TxtPrixVieux.Name = "TxtPrixVieux";
            this.TxtPrixVieux.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtPrixVieux.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtPrixVieux.PlaceholderText = "10";
            this.TxtPrixVieux.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtPrixVieux.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtPrixVieux.Regex = new System.Text.RegularExpressions.Regex("^[0-9]+");
            this.TxtPrixVieux.Size = new System.Drawing.Size(142, 27);
            this.TxtPrixVieux.TabIndex = 4;
            // 
            // TxtDateFin
            // 
            this.TxtDateFin.AllowControls = true;
            this.TxtDateFin.AllowDecimal = false;
            this.TxtDateFin.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtDateFin.AllowedCharacters")));
            this.TxtDateFin.AllowEmpty = false;
            this.TxtDateFin.AllowEuro = false;
            this.TxtDateFin.AllowLetters = false;
            this.TxtDateFin.AllowNumbers = true;
            this.TxtDateFin.AllowSpace = false;
            this.TxtDateFin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtDateFin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDateFin.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtDateFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtDateFin.LastValue = null;
            this.TxtDateFin.Location = new System.Drawing.Point(143, 141);
            this.TxtDateFin.Name = "TxtDateFin";
            this.TxtDateFin.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtDateFin.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtDateFin.PlaceholderText = "01/01/1990";
            this.TxtDateFin.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtDateFin.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtDateFin.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtDateFin.Size = new System.Drawing.Size(142, 27);
            this.TxtDateFin.TabIndex = 3;
            // 
            // TxtDateDebut
            // 
            this.TxtDateDebut.AllowControls = true;
            this.TxtDateDebut.AllowDecimal = false;
            this.TxtDateDebut.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtDateDebut.AllowedCharacters")));
            this.TxtDateDebut.AllowEmpty = false;
            this.TxtDateDebut.AllowEuro = false;
            this.TxtDateDebut.AllowLetters = false;
            this.TxtDateDebut.AllowNumbers = true;
            this.TxtDateDebut.AllowSpace = false;
            this.TxtDateDebut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtDateDebut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDateDebut.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtDateDebut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtDateDebut.LastValue = null;
            this.TxtDateDebut.Location = new System.Drawing.Point(143, 108);
            this.TxtDateDebut.Name = "TxtDateDebut";
            this.TxtDateDebut.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtDateDebut.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtDateDebut.PlaceholderText = "01/01/1990";
            this.TxtDateDebut.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtDateDebut.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtDateDebut.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtDateDebut.Size = new System.Drawing.Size(142, 27);
            this.TxtDateDebut.TabIndex = 2;
            // 
            // TxtNom
            // 
            this.TxtNom.AllowControls = true;
            this.TxtNom.AllowDecimal = true;
            this.TxtNom.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtNom.AllowedCharacters")));
            this.TxtNom.AllowEmpty = false;
            this.TxtNom.AllowEuro = false;
            this.TxtNom.AllowLetters = true;
            this.TxtNom.AllowNumbers = true;
            this.TxtNom.AllowSpace = true;
            this.TxtNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNom.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtNom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.LastValue = null;
            this.TxtNom.Location = new System.Drawing.Point(143, 75);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtNom.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNom.PlaceholderText = "Tournoi";
            this.TxtNom.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtNom.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtNom.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtNom.Size = new System.Drawing.Size(142, 27);
            this.TxtNom.TabIndex = 1;
            // 
            // TxtRef
            // 
            this.TxtRef.AllowControls = true;
            this.TxtRef.AllowDecimal = false;
            this.TxtRef.AllowedCharacters = ((System.Collections.Generic.List<char>)(resources.GetObject("TxtRef.AllowedCharacters")));
            this.TxtRef.AllowEmpty = true;
            this.TxtRef.AllowEuro = false;
            this.TxtRef.AllowLetters = false;
            this.TxtRef.AllowNumbers = false;
            this.TxtRef.AllowSpace = false;
            this.TxtRef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtRef.ErrorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(50)))), ((int)(((byte)(46)))));
            this.TxtRef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtRef.LastValue = null;
            this.TxtRef.Location = new System.Drawing.Point(143, 42);
            this.TxtRef.Name = "TxtRef";
            this.TxtRef.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.TxtRef.NormalFont = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtRef.PlaceholderText = null;
            this.TxtRef.PlaceholdingColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(114)))), ((int)(((byte)(149)))));
            this.TxtRef.PlaceholdingFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TxtRef.ReadOnly = true;
            this.TxtRef.Regex = new System.Text.RegularExpressions.Regex(".+");
            this.TxtRef.Size = new System.Drawing.Size(142, 27);
            this.TxtRef.TabIndex = 1;
            this.TxtRef.TabStop = false;
            // 
            // DetailsJoueurDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(596, 363);
            this.Controls.Add(this.CheckConfirmMail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblTitreTournoi);
            this.Controls.Add(this.LblArbitre);
            this.Controls.Add(this.TxtArbitre);
            this.Controls.Add(this.LblDureeRonde);
            this.Controls.Add(this.TxtDureeRonde);
            this.Controls.Add(this.LblNbRondes);
            this.Controls.Add(this.TxtNbRondes);
            this.Controls.Add(this.LblMaxParticipants);
            this.Controls.Add(this.TxtMaxParticipants);
            this.Controls.Add(this.LblTitreTarif);
            this.Controls.Add(this.LblMajoration);
            this.Controls.Add(this.TxtMajoration);
            this.Controls.Add(this.LblLimiteAge);
            this.Controls.Add(this.TxtLimiteAge);
            this.Controls.Add(this.LblPrixJeune);
            this.Controls.Add(this.TxtPrixJeune);
            this.Controls.Add(this.LblPrixVieux);
            this.Controls.Add(this.TxtPrixVieux);
            this.Controls.Add(this.LblDateFin);
            this.Controls.Add(this.TxtDateFin);
            this.Controls.Add(this.LblDateDebut);
            this.Controls.Add(this.TxtDateDebut);
            this.Controls.Add(this.LblNom);
            this.Controls.Add(this.TxtNom);
            this.Controls.Add(this.LblRef);
            this.Controls.Add(this.TxtRef);
            this.Controls.Add(this.LblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TournoiProprietesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Propriétés du tournoi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.BackColor = Theme.Style.DialogBodyBackColor;
            this.ForeColor = Theme.Style.DialogBodyForeColor;
            this.Font = Theme.Style.DialogBodyFont;

            this.TxtArbitre.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtArbitre.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtArbitre.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtArbitre.Font = Theme.Style.DialogTextBoxFont;
            this.TxtArbitre.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtDateDebut.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtDateDebut.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtDateDebut.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtDateDebut.Font = Theme.Style.DialogTextBoxFont;
            this.TxtDateDebut.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtDateFin.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtDateFin.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtDateFin.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtDateFin.Font = Theme.Style.DialogTextBoxFont;
            this.TxtDateFin.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtDureeRonde.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtDureeRonde.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtDureeRonde.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtDureeRonde.Font = Theme.Style.DialogTextBoxFont;
            this.TxtDureeRonde.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtLimiteAge.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtLimiteAge.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtLimiteAge.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtLimiteAge.Font = Theme.Style.DialogTextBoxFont;
            this.TxtLimiteAge.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtMajoration.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtMajoration.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtMajoration.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtMajoration.Font = Theme.Style.DialogTextBoxFont;
            this.TxtMajoration.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtMaxParticipants.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtMaxParticipants.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtMaxParticipants.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtMaxParticipants.Font = Theme.Style.DialogTextBoxFont;
            this.TxtMaxParticipants.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtNbRondes.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNbRondes.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNbRondes.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNbRondes.Font = Theme.Style.DialogTextBoxFont;
            this.TxtNbRondes.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtNom.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtNom.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNom.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtNom.Font = Theme.Style.DialogTextBoxFont;
            this.TxtNom.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtPrixJeune.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtPrixJeune.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtPrixJeune.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtPrixJeune.Font = Theme.Style.DialogTextBoxFont;
            this.TxtPrixJeune.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtPrixVieux.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtPrixVieux.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtPrixVieux.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtPrixVieux.Font = Theme.Style.DialogTextBoxFont;
            this.TxtPrixVieux.NormalFont = Theme.Style.DialogTextBoxFont;

            this.TxtRef.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TxtRef.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtRef.NormalColor = Theme.Style.DialogTextBoxForeColor;
            this.TxtRef.Font = Theme.Style.DialogTextBoxFont;
            this.TxtRef.NormalFont = Theme.Style.DialogTextBoxFont;

            this.LblArbitre.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblArbitre.Font = Theme.Style.DialogBodyFont;

            this.LblDateDebut.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblDateDebut.Font = Theme.Style.DialogBodyFont;

            this.LblDateFin.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblDateFin.Font = Theme.Style.DialogBodyFont;

            this.LblDureeRonde.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblDureeRonde.Font = Theme.Style.DialogBodyFont;

            this.LblLimiteAge.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblLimiteAge.Font = Theme.Style.DialogBodyFont;

            this.LblMajoration.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblMajoration.Font = Theme.Style.DialogBodyFont;

            this.LblMaxParticipants.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblMaxParticipants.Font = Theme.Style.DialogBodyFont;

            this.LblNbRondes.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNbRondes.Font = Theme.Style.DialogBodyFont;

            this.LblNom.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblNom.Font = Theme.Style.DialogBodyFont;

            this.LblPrixJeune.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblPrixJeune.Font = Theme.Style.DialogBodyFont;

            this.LblPrixVieux.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblPrixVieux.Font = Theme.Style.DialogBodyFont;

            this.LblRef.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblRef.Font = Theme.Style.DialogBodyFont;

            this.LblTitle.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblTitle.Font = Theme.Style.DialogBodyFont;

            this.LblTitreTarif.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblTitreTarif.Font = Theme.Style.DialogBodyFont;

            this.LblTitreTournoi.ForeColor = Theme.Style.DialogBodyForeColor;
            this.LblTitreTournoi.Font = Theme.Style.DialogBodyFont;

            this.BtnOk.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.BtnOk.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.BtnOk.Font = Theme.Style.DialogMainButtonsFont;

            this.BtnCancel.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.BtnCancel.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.BtnCancel.Font = Theme.Style.DialogSecondaryButtonsFont;
        }
    }
}
