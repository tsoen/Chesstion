using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up regroupant toutes les infos <see cref="Joueur"/>.
    /// </summary>
    class DetailsJoueurDialog : Form
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
        /// Label titre de la référence du joueur.
        /// </summary>
        private Label LblTRef;

        /// <summary>
        /// Label de référence du joueur.
        /// </summary>
        private Label LblRef;

        /// <summary>
        /// Label de numéro FFE du joueur.
        /// </summary>
        private Label LblNrFFE;

        /// <summary>
        /// Label de titre de numéro FFE du joueur.
        /// </summary>
        private Label LblTNrFFE;

        /// <summary>
        /// Label du sexe du joueur.
        /// </summary>
        private Label LblSexe;

        /// <summary>
        /// Label titre du sexe du joueur.
        /// </summary>
        private Label LblTSexe;

        /// <summary>
        /// Label de date de naissance du joueur.
        /// </summary>
        private Label LblNeLe;

        /// <summary>
        /// Label titre de date de naissance du joueur.
        /// </summary>
        private Label LblTNeLe;

        /// <summary>
        /// Label de catégorie du joueur.
        /// </summary>
        private Label LblCat;

        /// <summary>
        /// Label titre de catégorie du joueur.
        /// </summary>
        private Label LblTCat;

        /// <summary>
        /// Label de fédération du joueur.
        /// </summary>
        private Label LblFederation;

        /// <summary>
        /// Label titre de fédération du joueur.
        /// </summary>
        private Label LblTFederation;

        /// <summary>
        /// Label de référence du club du joueur.
        /// </summary>
        private Label LblClubRef;

        /// <summary>
        /// Label titre de référence du club du joueur.
        /// </summary>
        private Label LblTClubRef;

        /// <summary>
        /// Label d'elo du joueur.
        /// </summary>
        private Label LblElo;

        /// <summary>
        /// Label titre d'elo du joueur.
        /// </summary>
        private Label LblTElo;

        /// <summary>
        /// Label de rapide du joueur.
        /// </summary>
        private Label LblRapide;

        /// <summary>
        /// Label titre de rapide du joueur.
        /// </summary>
        private Label LblTRapide;

        /// <summary>
        /// Label de fide du joueur.
        /// </summary>
        private Label LblFide;

        /// <summary>
        /// Label titre de fide du joueur.
        /// </summary>
        private Label LblTFide;

        /// <summary>
        /// Label du code fide du joueur.
        /// </summary>
        private Label LblFideCode;

        /// <summary>
        /// Label titre du code fide du joueur.
        /// </summary>
        private Label LblTFideCode;

        /// <summary>
        /// Label de titre fide du joueur.
        /// </summary>
        private Label LblFideTitre;

        /// <summary>
        /// Label titre de titre fide du joueur.
        /// </summary>
        private Label LblTFideTitre;

        /// <summary>
        /// Label d'affType du joueur.
        /// </summary>
        private Label LblAffType;

        /// <summary>
        /// Label de titre d'affType du joueur.
        /// </summary>
        private Label LblTAffType;

        /// <summary>
        /// Label d'actif du joueur.
        /// </summary>
        private Label LblActif;

        /// <summary>
        /// Label de titre d'actif du joueur.
        /// </summary>
        private Label LblTActif;

        /// <summary>
        /// Label de référence d'open du joueur.
        /// </summary>
        private Label LblOpenRef;

        /// <summary>
        /// Label titre de référnce de l'open du joueur.
        /// </summary>
        private Label LblTOpenRef;

        /// <summary>
        /// Label d'email du joueur.
        /// </summary>
        private Label LblEmail;

        /// <summary>
        /// Label titre d'email du joueur.
        /// </summary>
        private Label LblTEmail;

        /// <summary>
        /// Label de titre du subscribe du joueur.
        /// </summary>
        private Label LblTSubscribe;

        /// <summary>
        /// Label de numéro de téléphone du joueur.
        /// </summary>
        private Label LblPhone;

        /// <summary>
        /// Label titre de numéro de téléphone du joueur.
        /// </summary>
        private Label LblTPhone;

        /// <summary>
        /// Label de référence de repas du joueur.
        /// </summary>
        private Label LblRepasRef;

        /// <summary>
        /// Label titre de référence de repas du joueur.
        /// </summary>
        private Label LblTRepasRef;

        /// <summary>
        /// Label titre de confirmation.
        /// </summary>
        private Label LblTConfirme;

        /// <summary>
        /// Label de frais d'inscription.
        /// </summary>
        private Label LblFraisInscription;

        /// <summary>
        /// Label de titre de frais d'inscription.
        /// </summary>
        private Label LblTFraisInscription;

        /// <summary>
        /// Label de subscribe du joueur.
        /// </summary>
        private Label LblSubscribe;

        /// <summary>
        /// Label de confirmation du joueur.
        /// </summary>
        private Label LblConfirme;



        /// <summary>
        /// Label de titre.
        /// </summary>
        private Label LblTitle;

        /// <summary>
        /// Bouton de fermeture du pop-up.
        /// </summary>
        private Button BtnClose;










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
        /// <param name="refJoueur">Référence du joueur à détailler.</param>
        public DetailsJoueurDialog(int refJoueur)
        {
            InitializeComponent();

            BtnClose.Click += (object sender, EventArgs e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            if (GJoueur.GetJoueur(refJoueur) == null)
                return;

            Joueur j = GJoueur.GetJoueur(refJoueur);

            LblTitle.Text = j.Nom.ToUpper() + " " + j.Prenom;
            LblRef.Text = j.Ref.ToString();
            LblNrFFE.Text = j.NrFFE;
            LblSexe.Text = j.Sexe;
            LblNeLe.Text = j.NeLe;
            LblCat.Text = j.Cat;
            LblFederation.Text = j.Federation;
            LblClubRef.Text = j.ClubRef.ToString();
            LblElo.Text = j.Elo.ToString();
            LblRapide.Text = j.Rapide.ToString();
            LblFide.Text = j.Fide;
            LblFideCode.Text = j.FideCode;
            LblFideTitre.Text = j.FideTitre;
            LblAffType.Text = j.AffType;
            LblActif.Text = j.Actif;
            LblOpenRef.Text = j.OpenRef.ToString();
            LblEmail.Text = j.Email;
            LblSubscribe.Text = j.Subscribe ? "oui" : "non";
            LblPhone.Text = j.Phone;
            LblRepasRef.Text = j.RepasRef.ToString();
            LblConfirme.Text = j.Confirme ? "oui" : "non";
            LblFraisInscription.Text = j.FraisInscription + " €";



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
        /// Crée les controls.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblTRef = new System.Windows.Forms.Label();
            this.LblRef = new System.Windows.Forms.Label();
            this.LblNrFFE = new System.Windows.Forms.Label();
            this.LblTNrFFE = new System.Windows.Forms.Label();
            this.LblSexe = new System.Windows.Forms.Label();
            this.LblTSexe = new System.Windows.Forms.Label();
            this.LblNeLe = new System.Windows.Forms.Label();
            this.LblTNeLe = new System.Windows.Forms.Label();
            this.LblCat = new System.Windows.Forms.Label();
            this.LblTCat = new System.Windows.Forms.Label();
            this.LblFederation = new System.Windows.Forms.Label();
            this.LblTFederation = new System.Windows.Forms.Label();
            this.LblClubRef = new System.Windows.Forms.Label();
            this.LblTClubRef = new System.Windows.Forms.Label();
            this.LblElo = new System.Windows.Forms.Label();
            this.LblTElo = new System.Windows.Forms.Label();
            this.LblRapide = new System.Windows.Forms.Label();
            this.LblTRapide = new System.Windows.Forms.Label();
            this.LblFide = new System.Windows.Forms.Label();
            this.LblTFide = new System.Windows.Forms.Label();
            this.LblFideCode = new System.Windows.Forms.Label();
            this.LblTFideCode = new System.Windows.Forms.Label();
            this.LblFideTitre = new System.Windows.Forms.Label();
            this.LblTFideTitre = new System.Windows.Forms.Label();
            this.LblAffType = new System.Windows.Forms.Label();
            this.LblTAffType = new System.Windows.Forms.Label();
            this.LblActif = new System.Windows.Forms.Label();
            this.LblTActif = new System.Windows.Forms.Label();
            this.LblOpenRef = new System.Windows.Forms.Label();
            this.LblTOpenRef = new System.Windows.Forms.Label();
            this.LblEmail = new System.Windows.Forms.Label();
            this.LblTEmail = new System.Windows.Forms.Label();
            this.LblTSubscribe = new System.Windows.Forms.Label();
            this.LblPhone = new System.Windows.Forms.Label();
            this.LblTPhone = new System.Windows.Forms.Label();
            this.LblRepasRef = new System.Windows.Forms.Label();
            this.LblTRepasRef = new System.Windows.Forms.Label();
            this.LblTConfirme = new System.Windows.Forms.Label();
            this.LblFraisInscription = new System.Windows.Forms.Label();
            this.LblTFraisInscription = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.LblSubscribe = new System.Windows.Forms.Label();
            this.LblConfirme = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTitle.Location = new System.Drawing.Point(12, 10);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(758, 22);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "CHAMPION Philippe";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTRef
            // 
            this.LblTRef.AutoSize = true;
            this.LblTRef.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTRef.Location = new System.Drawing.Point(12, 50);
            this.LblTRef.Name = "LblTRef";
            this.LblTRef.Size = new System.Drawing.Size(87, 20);
            this.LblTRef.TabIndex = 1;
            this.LblTRef.Text = "Référence :";
            // 
            // LblRef
            // 
            this.LblRef.AutoSize = true;
            this.LblRef.Location = new System.Drawing.Point(105, 50);
            this.LblRef.Name = "LblRef";
            this.LblRef.Size = new System.Drawing.Size(65, 20);
            this.LblRef.TabIndex = 2;
            this.LblRef.Text = "1236458";
            // 
            // LblNrFFE
            // 
            this.LblNrFFE.AutoSize = true;
            this.LblNrFFE.Location = new System.Drawing.Point(121, 83);
            this.LblNrFFE.Name = "LblNrFFE";
            this.LblNrFFE.Size = new System.Drawing.Size(58, 20);
            this.LblNrFFE.TabIndex = 4;
            this.LblNrFFE.Text = "X01234";
            // 
            // LblTNrFFE
            // 
            this.LblTNrFFE.AutoSize = true;
            this.LblTNrFFE.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTNrFFE.Location = new System.Drawing.Point(12, 83);
            this.LblTNrFFE.Name = "LblTNrFFE";
            this.LblTNrFFE.Size = new System.Drawing.Size(103, 20);
            this.LblTNrFFE.TabIndex = 3;
            this.LblTNrFFE.Text = "Numéro FFE :";
            // 
            // LblSexe
            // 
            this.LblSexe.AutoSize = true;
            this.LblSexe.Location = new System.Drawing.Point(67, 116);
            this.LblSexe.Name = "LblSexe";
            this.LblSexe.Size = new System.Drawing.Size(22, 20);
            this.LblSexe.TabIndex = 6;
            this.LblSexe.Text = "M";
            // 
            // LblTSexe
            // 
            this.LblTSexe.AutoSize = true;
            this.LblTSexe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTSexe.Location = new System.Drawing.Point(12, 116);
            this.LblTSexe.Name = "LblTSexe";
            this.LblTSexe.Size = new System.Drawing.Size(49, 20);
            this.LblTSexe.TabIndex = 5;
            this.LblTSexe.Text = "Sexe :";
            // 
            // LblNeLe
            // 
            this.LblNeLe.AutoSize = true;
            this.LblNeLe.Location = new System.Drawing.Point(71, 149);
            this.LblNeLe.Name = "LblNeLe";
            this.LblNeLe.Size = new System.Drawing.Size(85, 20);
            this.LblNeLe.TabIndex = 8;
            this.LblNeLe.Text = "12/08/1990";
            // 
            // LblTNeLe
            // 
            this.LblTNeLe.AutoSize = true;
            this.LblTNeLe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTNeLe.Location = new System.Drawing.Point(12, 149);
            this.LblTNeLe.Name = "LblTNeLe";
            this.LblTNeLe.Size = new System.Drawing.Size(53, 20);
            this.LblTNeLe.TabIndex = 7;
            this.LblTNeLe.Text = "Né le :";
            // 
            // LblCat
            // 
            this.LblCat.AutoSize = true;
            this.LblCat.Location = new System.Drawing.Point(102, 182);
            this.LblCat.Name = "LblCat";
            this.LblCat.Size = new System.Drawing.Size(46, 20);
            this.LblCat.TabIndex = 10;
            this.LblCat.Text = "SenM";
            // 
            // LblTCat
            // 
            this.LblTCat.AutoSize = true;
            this.LblTCat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTCat.Location = new System.Drawing.Point(12, 182);
            this.LblTCat.Name = "LblTCat";
            this.LblTCat.Size = new System.Drawing.Size(84, 20);
            this.LblTCat.TabIndex = 9;
            this.LblTCat.Text = "Catégorie :";
            // 
            // LblFederation
            // 
            this.LblFederation.AutoSize = true;
            this.LblFederation.Location = new System.Drawing.Point(110, 215);
            this.LblFederation.Name = "LblFederation";
            this.LblFederation.Size = new System.Drawing.Size(35, 20);
            this.LblFederation.TabIndex = 12;
            this.LblFederation.Text = "FRA";
            // 
            // LblTFederation
            // 
            this.LblTFederation.AutoSize = true;
            this.LblTFederation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTFederation.Location = new System.Drawing.Point(12, 215);
            this.LblTFederation.Name = "LblTFederation";
            this.LblTFederation.Size = new System.Drawing.Size(92, 20);
            this.LblTFederation.TabIndex = 11;
            this.LblTFederation.Text = "Fédération :";
            // 
            // LblClubRef
            // 
            this.LblClubRef.AutoSize = true;
            this.LblClubRef.Location = new System.Drawing.Point(114, 248);
            this.LblClubRef.Name = "LblClubRef";
            this.LblClubRef.Size = new System.Drawing.Size(65, 20);
            this.LblClubRef.TabIndex = 14;
            this.LblClubRef.Text = "1234567";
            // 
            // LblTClubRef
            // 
            this.LblTClubRef.AutoSize = true;
            this.LblTClubRef.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTClubRef.Location = new System.Drawing.Point(12, 248);
            this.LblTClubRef.Name = "LblTClubRef";
            this.LblTClubRef.Size = new System.Drawing.Size(96, 20);
            this.LblTClubRef.TabIndex = 13;
            this.LblTClubRef.Text = "Réf du club :";
            // 
            // LblElo
            // 
            this.LblElo.AutoSize = true;
            this.LblElo.Location = new System.Drawing.Point(301, 50);
            this.LblElo.Name = "LblElo";
            this.LblElo.Size = new System.Drawing.Size(41, 20);
            this.LblElo.TabIndex = 16;
            this.LblElo.Text = "1500";
            // 
            // LblTElo
            // 
            this.LblTElo.AutoSize = true;
            this.LblTElo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTElo.Location = new System.Drawing.Point(257, 50);
            this.LblTElo.Name = "LblTElo";
            this.LblTElo.Size = new System.Drawing.Size(38, 20);
            this.LblTElo.TabIndex = 15;
            this.LblTElo.Text = "Elo :";
            // 
            // LblRapide
            // 
            this.LblRapide.AutoSize = true;
            this.LblRapide.Location = new System.Drawing.Point(328, 83);
            this.LblRapide.Name = "LblRapide";
            this.LblRapide.Size = new System.Drawing.Size(41, 20);
            this.LblRapide.TabIndex = 18;
            this.LblRapide.Text = "1500";
            // 
            // LblTRapide
            // 
            this.LblTRapide.AutoSize = true;
            this.LblTRapide.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTRapide.Location = new System.Drawing.Point(257, 83);
            this.LblTRapide.Name = "LblTRapide";
            this.LblTRapide.Size = new System.Drawing.Size(65, 20);
            this.LblTRapide.TabIndex = 17;
            this.LblTRapide.Text = "Rapide :";
            // 
            // LblFide
            // 
            this.LblFide.AutoSize = true;
            this.LblFide.Location = new System.Drawing.Point(309, 116);
            this.LblFide.Name = "LblFide";
            this.LblFide.Size = new System.Drawing.Size(16, 20);
            this.LblFide.TabIndex = 20;
            this.LblFide.Text = "F";
            // 
            // LblTFide
            // 
            this.LblTFide.AutoSize = true;
            this.LblTFide.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTFide.Location = new System.Drawing.Point(257, 116);
            this.LblTFide.Name = "LblTFide";
            this.LblTFide.Size = new System.Drawing.Size(46, 20);
            this.LblTFide.TabIndex = 19;
            this.LblTFide.Text = "Fide :";
            // 
            // LblFideCode
            // 
            this.LblFideCode.AutoSize = true;
            this.LblFideCode.Location = new System.Drawing.Point(348, 149);
            this.LblFideCode.Name = "LblFideCode";
            this.LblFideCode.Size = new System.Drawing.Size(73, 20);
            this.LblFideCode.TabIndex = 22;
            this.LblFideCode.Text = "00628840";
            // 
            // LblTFideCode
            // 
            this.LblTFideCode.AutoSize = true;
            this.LblTFideCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTFideCode.Location = new System.Drawing.Point(257, 149);
            this.LblTFideCode.Name = "LblTFideCode";
            this.LblTFideCode.Size = new System.Drawing.Size(85, 20);
            this.LblTFideCode.TabIndex = 21;
            this.LblTFideCode.Text = "Code Fide :";
            // 
            // LblFideTitre
            // 
            this.LblFideTitre.AutoSize = true;
            this.LblFideTitre.Location = new System.Drawing.Point(346, 182);
            this.LblFideTitre.Name = "LblFideTitre";
            this.LblFideTitre.Size = new System.Drawing.Size(31, 20);
            this.LblFideTitre.TabIndex = 24;
            this.LblFideTitre.Text = "gm";
            // 
            // LblTFideTitre
            // 
            this.LblTFideTitre.AutoSize = true;
            this.LblTFideTitre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTFideTitre.Location = new System.Drawing.Point(257, 182);
            this.LblTFideTitre.Name = "LblTFideTitre";
            this.LblTFideTitre.Size = new System.Drawing.Size(83, 20);
            this.LblTFideTitre.TabIndex = 23;
            this.LblTFideTitre.Text = "Titre Fide :";
            // 
            // LblAffType
            // 
            this.LblAffType.AutoSize = true;
            this.LblAffType.Location = new System.Drawing.Point(336, 215);
            this.LblAffType.Name = "LblAffType";
            this.LblAffType.Size = new System.Drawing.Size(20, 20);
            this.LblAffType.TabIndex = 26;
            this.LblAffType.Text = "N";
            // 
            // LblTAffType
            // 
            this.LblTAffType.AutoSize = true;
            this.LblTAffType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTAffType.Location = new System.Drawing.Point(257, 215);
            this.LblTAffType.Name = "LblTAffType";
            this.LblTAffType.Size = new System.Drawing.Size(73, 20);
            this.LblTAffType.TabIndex = 25;
            this.LblTAffType.Text = "AffType :";
            // 
            // LblActif
            // 
            this.LblActif.AutoSize = true;
            this.LblActif.Location = new System.Drawing.Point(314, 248);
            this.LblActif.Name = "LblActif";
            this.LblActif.Size = new System.Drawing.Size(41, 20);
            this.LblActif.TabIndex = 28;
            this.LblActif.Text = "2016";
            // 
            // LblTActif
            // 
            this.LblTActif.AutoSize = true;
            this.LblTActif.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTActif.Location = new System.Drawing.Point(257, 248);
            this.LblTActif.Name = "LblTActif";
            this.LblTActif.Size = new System.Drawing.Size(51, 20);
            this.LblTActif.TabIndex = 27;
            this.LblTActif.Text = "Actif :";
            // 
            // LblOpenRef
            // 
            this.LblOpenRef.AutoSize = true;
            this.LblOpenRef.Location = new System.Drawing.Point(586, 50);
            this.LblOpenRef.Name = "LblOpenRef";
            this.LblOpenRef.Size = new System.Drawing.Size(41, 20);
            this.LblOpenRef.TabIndex = 30;
            this.LblOpenRef.Text = "1234";
            // 
            // LblTOpenRef
            // 
            this.LblTOpenRef.AutoSize = true;
            this.LblTOpenRef.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTOpenRef.Location = new System.Drawing.Point(502, 50);
            this.LblTOpenRef.Name = "LblTOpenRef";
            this.LblTOpenRef.Size = new System.Drawing.Size(78, 20);
            this.LblTOpenRef.TabIndex = 29;
            this.LblTOpenRef.Text = "OpenRef :";
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.Location = new System.Drawing.Point(563, 83);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(162, 20);
            this.LblEmail.TabIndex = 32;
            this.LblEmail.Text = "jean.michel@hotmail.fr";
            // 
            // LblTEmail
            // 
            this.LblTEmail.AutoSize = true;
            this.LblTEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTEmail.Location = new System.Drawing.Point(502, 83);
            this.LblTEmail.Name = "LblTEmail";
            this.LblTEmail.Size = new System.Drawing.Size(55, 20);
            this.LblTEmail.TabIndex = 31;
            this.LblTEmail.Text = "Email :";
            // 
            // LblTSubscribe
            // 
            this.LblTSubscribe.AutoSize = true;
            this.LblTSubscribe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTSubscribe.Location = new System.Drawing.Point(502, 116);
            this.LblTSubscribe.Name = "LblTSubscribe";
            this.LblTSubscribe.Size = new System.Drawing.Size(102, 20);
            this.LblTSubscribe.TabIndex = 33;
            this.LblTSubscribe.Text = "Reçoit mails :";
            // 
            // LblPhone
            // 
            this.LblPhone.AutoSize = true;
            this.LblPhone.Location = new System.Drawing.Point(597, 149);
            this.LblPhone.Name = "LblPhone";
            this.LblPhone.Size = new System.Drawing.Size(89, 20);
            this.LblPhone.TabIndex = 36;
            this.LblPhone.Text = "0685969685";
            // 
            // LblTPhone
            // 
            this.LblTPhone.AutoSize = true;
            this.LblTPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTPhone.Location = new System.Drawing.Point(502, 149);
            this.LblTPhone.Name = "LblTPhone";
            this.LblTPhone.Size = new System.Drawing.Size(89, 20);
            this.LblTPhone.TabIndex = 35;
            this.LblTPhone.Text = "Téléphone :";
            // 
            // LblRepasRef
            // 
            this.LblRepasRef.AutoSize = true;
            this.LblRepasRef.Location = new System.Drawing.Point(613, 182);
            this.LblRepasRef.Name = "LblRepasRef";
            this.LblRepasRef.Size = new System.Drawing.Size(57, 20);
            this.LblRepasRef.TabIndex = 38;
            this.LblRepasRef.Text = "123456";
            // 
            // LblTRepasRef
            // 
            this.LblTRepasRef.AutoSize = true;
            this.LblTRepasRef.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTRepasRef.Location = new System.Drawing.Point(502, 182);
            this.LblTRepasRef.Name = "LblTRepasRef";
            this.LblTRepasRef.Size = new System.Drawing.Size(105, 20);
            this.LblTRepasRef.TabIndex = 37;
            this.LblTRepasRef.Text = "Réf du repas :";
            // 
            // LblTConfirme
            // 
            this.LblTConfirme.AutoSize = true;
            this.LblTConfirme.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTConfirme.Location = new System.Drawing.Point(502, 215);
            this.LblTConfirme.Name = "LblTConfirme";
            this.LblTConfirme.Size = new System.Drawing.Size(82, 20);
            this.LblTConfirme.TabIndex = 39;
            this.LblTConfirme.Text = "Confirmé :";
            // 
            // LblFraisInscription
            // 
            this.LblFraisInscription.AutoSize = true;
            this.LblFraisInscription.Location = new System.Drawing.Point(607, 248);
            this.LblFraisInscription.Name = "LblFraisInscription";
            this.LblFraisInscription.Size = new System.Drawing.Size(37, 20);
            this.LblFraisInscription.TabIndex = 42;
            this.LblFraisInscription.Text = "25 €";
            // 
            // LblTFraisInscription
            // 
            this.LblTFraisInscription.AutoSize = true;
            this.LblTFraisInscription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblTFraisInscription.Location = new System.Drawing.Point(502, 248);
            this.LblTFraisInscription.Name = "LblTFraisInscription";
            this.LblTFraisInscription.Size = new System.Drawing.Size(99, 20);
            this.LblTFraisInscription.TabIndex = 41;
            this.LblTFraisInscription.Text = "Inscriptions :";
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Location = new System.Drawing.Point(327, 298);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(109, 45);
            this.BtnClose.TabIndex = 43;
            this.BtnClose.Text = "Fermer";
            this.BtnClose.UseVisualStyleBackColor = false;
            // 
            // LblSubscribe
            // 
            this.LblSubscribe.AutoSize = true;
            this.LblSubscribe.Location = new System.Drawing.Point(610, 116);
            this.LblSubscribe.Name = "LblSubscribe";
            this.LblSubscribe.Size = new System.Drawing.Size(34, 20);
            this.LblSubscribe.TabIndex = 44;
            this.LblSubscribe.Text = "non";
            // 
            // LblConfirme
            // 
            this.LblConfirme.AutoSize = true;
            this.LblConfirme.Location = new System.Drawing.Point(590, 215);
            this.LblConfirme.Name = "LblConfirme";
            this.LblConfirme.Size = new System.Drawing.Size(30, 20);
            this.LblConfirme.TabIndex = 45;
            this.LblConfirme.Text = "oui";
            // 
            // DetailsJoueurDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(773, 355);
            this.Controls.Add(this.LblConfirme);
            this.Controls.Add(this.LblSubscribe);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LblFraisInscription);
            this.Controls.Add(this.LblTFraisInscription);
            this.Controls.Add(this.LblTConfirme);
            this.Controls.Add(this.LblRepasRef);
            this.Controls.Add(this.LblTRepasRef);
            this.Controls.Add(this.LblPhone);
            this.Controls.Add(this.LblTPhone);
            this.Controls.Add(this.LblTSubscribe);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.LblTEmail);
            this.Controls.Add(this.LblOpenRef);
            this.Controls.Add(this.LblTOpenRef);
            this.Controls.Add(this.LblActif);
            this.Controls.Add(this.LblTActif);
            this.Controls.Add(this.LblAffType);
            this.Controls.Add(this.LblTAffType);
            this.Controls.Add(this.LblFideTitre);
            this.Controls.Add(this.LblTFideTitre);
            this.Controls.Add(this.LblFideCode);
            this.Controls.Add(this.LblTFideCode);
            this.Controls.Add(this.LblFide);
            this.Controls.Add(this.LblTFide);
            this.Controls.Add(this.LblRapide);
            this.Controls.Add(this.LblTRapide);
            this.Controls.Add(this.LblElo);
            this.Controls.Add(this.LblTElo);
            this.Controls.Add(this.LblClubRef);
            this.Controls.Add(this.LblTClubRef);
            this.Controls.Add(this.LblFederation);
            this.Controls.Add(this.LblTFederation);
            this.Controls.Add(this.LblCat);
            this.Controls.Add(this.LblTCat);
            this.Controls.Add(this.LblNeLe);
            this.Controls.Add(this.LblTNeLe);
            this.Controls.Add(this.LblSexe);
            this.Controls.Add(this.LblTSexe);
            this.Controls.Add(this.LblNrFFE);
            this.Controls.Add(this.LblTNrFFE);
            this.Controls.Add(this.LblRef);
            this.Controls.Add(this.LblTRef);
            this.Controls.Add(this.LblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(221)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailsJoueurDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Détails du joueur";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
