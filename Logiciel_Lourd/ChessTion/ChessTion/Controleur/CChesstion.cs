using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChessTion.CacheSystem;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.Etats;
using ChessTion.Controleur.ImportExport;
using ChessTion.Modele.MRepas;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.GeneralControls.CustomComboBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomButtons;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;
using ChessTion.Vue.CustomControls;
using ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using ChessTion.Vue.CustomControls.SpecificControls.CustomLabels;
using ChessTion.Vue.CustomControls.SpecificControls.CustomMenus;
using Newtonsoft.Json.Linq;
using Debug = ChessTion.Test.Debug;

namespace ChessTion.Controleur
{
    static class CChesstion
    {
        /************************************************************
         *   ___  ____  ____    ____  ____    ___  ____  ____  ___  *
         *  / __)( ___)(_  _)  ( ___)(_  _)  / __)( ___)(_  _)/ __) *
         * ( (_-. )__)   )(     )__)   )(    \__ \ )__)   )(  \__ \ *
         *  \___/(____) (__)   (____) (__)   (___/(____) (__) (___/ *
         *                                                          *
         *       Ensemble des getters et setters de la classe.      *
         *                                                          *
         ************************************************************/

        //
        // Path
        //

        /// <summary>
        /// Chemin d'accès du dossier de base.
        /// </summary>
        public static string BasePath { get; private set; } = Environment.CurrentDirectory;

        /// <summary>
        /// Chemin d'accès du dossier de thèmes.
        /// </summary>
        public static string ThemeFolder { get; private set; } = BasePath + "/Ressources/Themes";

        /// <summary>
        /// Chemin d'accès du dossier du thème courant.
        /// </summary>
        public static string CurrentThemeFolder { get { return ThemeFolder + "/" + Theme.ThemeName; } }

        /// <summary>
        /// Chemin d'accès du dossier d'icônes du thème courant.
        /// </summary>
        public static string CurrentThemeIconsFolder { get { return ThemeFolder + "/" + Theme.ThemeName + "/Images/Icons"; } }

        /// <summary>
        /// Chemin d'accès du dossier de Papi.
        /// </summary>
        public static string PapiFolder { get; set; }

        /// <summary>
        /// Chemin d'accès du dossier de sauvegardes.
        /// </summary>
        public static string SaveFolder { get { return BasePath + "/Saves"; } }

        /// <summary>
        /// Chemin d'accès du dossier comprenant les fichiers de configuration.
        /// </summary>
        public static string SettingsFolder { get { return BasePath + "/Settings"; } }

        /// <summary>
        /// URL vers le site.
        /// </summary>
        public static string BaseURL { get; private set; } = "http://www.ecole.ensicaen.fr/~soen/Chesstion/";

        //
        // Elements graphiques
        //

        /// <summary>
        /// Panneau de joueur (en haut à droite de l'interface).
        /// </summary>
        public static JoueurPanel JoueurPanel { get; set; }

        /// <summary>
        /// Panneau d'open (en bas à droite de l'interface).
        /// </summary>
        public static OpenPanel OpenPanel { get; set; }

        /// <summary>
        /// Panneau d'opens (en haut à gauche de l'interface).
        /// </summary>
        public static OpensPanel OpensPanel { get; set; }

        /// <summary>
        /// Panneau de repas (en bas à gauche de l'interface).
        /// </summary>
        public static RepasPanel RepasPanel { get; set; }

        /// <summary>
        /// Panneau central.
        /// </summary>
        public static CentrePanel CentrePanel { get; set; }

        /// <summary>
        /// Panneau d'actions (en bas de l'interface).
        /// </summary>
        public static ActionPanel ActionPanel { get; set; }

        /// <summary>
        /// Panneau de menu (en haut de l'interface).
        /// </summary>
        public static CustomMenuStrip MsMenu { get; set; }

        /// <summary>
        /// Panneau de statut.
        /// </summary>
        public static StatusPanel StatusPanel { get; set; }


        //
        // Elements métiers
        //

        /// <summary>
        /// <see cref="Joueur"/> actuellement selectionné dans l'interface.
        /// </summary>
        public static Joueur JoueurSelectionne { get; set; }

        /// <summary>
        /// <see cref="Open"/> actuellement selectionné dans l'interface.
        /// </summary>
        public static Open OpenSelectionne { get; set; }

        /// <summary>
        /// <see cref="Tournoi"/> actuellement selectionné dans l'interface.
        /// </summary>
        public static Tournoi TournoiSelectionne { get; set; }



        //
        // Autre
        //

        /// <summary>
        /// Cache de joueurs.
        /// </summary>
        public static JoueurErrorsCache JoueurErrorsCache { get; } = new JoueurErrorsCache();


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
        /// Initialise l'ensemble des panels et débute l'application.
        /// </summary>
        public static void Init()
        {
            MsMenu.Init();
            OpensPanel.Init();
            RepasPanel.Init();
            JoueurPanel.Init();
            OpenPanel.Init();
            ActionPanel.Init();
            CentrePanel.Init();
            StatusPanel.Init();

            Demarrage.Done += (object sender, EventArgs e) => {
                CChesstion.FillComboBoxes();
                CChesstion.SelectionnerOpen(CChesstion.TournoiSelectionne.TsLesOpens.First().Ref);
                CChesstion.UpdateTournoiEtat(true);
            };
            Demarrage.Start();
        }


        //
        // Gestion des panneaux Joueur et Open
        //

        /// <summary>
        /// Récupère les infos du <see cref="JoueurPanel"/> pour les mettre dans l'objet métier (la plupart des champs mettent directement à jour l'objet métier).
        /// </summary>
        /// <param name="resetPanel">Vrai si on veut reset la panneau après la récupération des infos.</param>
        /// <returns>Le joueur selectionné.</returns>
        public static Joueur LoadJoueurFromPanel(bool resetPanel = false)
        {
            JoueurPanel.LoadInfoFromPanel(resetPanel);

            return JoueurSelectionne;
        }

        /// <summary>
        /// Remplit le <see cref="JoueurPanel"/> avec les valeurs de <see cref="JoueurSelectionne"/>.
        /// </summary>
        public static void LoadJoueurToPanel()
        {
            JoueurPanel.LoadInfoToPanel();
        }

        /// <summary>
        /// Récupère les infos du <see cref="OpenPanel"/> pour les mettre dans l'objet métier (la plupart des champs mettent directement à jour l'objet métier).
        /// </summary>
        /// <param name="resetPanel">Vrai si on veut reset la panneau après la récupération des infos.</param>
        /// <returns>L'open selectionné.</returns>
        public static Open LoadOpenFromPanel(bool resetPanel = false)
        {
            OpenPanel.LoadInfoFromPanel(resetPanel);

            return OpenSelectionne;
        }

        /// <summary>
        /// Remplit le <see cref="OpenPanel"/> avec les valeurs de <see cref="OpenSelectionne"/>.
        /// </summary>
        public static void LoadOpenToPanels()
        {
            OpenPanel.LoadInfoToPanel();
            OpensPanel.SelectMenuButton(OpenSelectionne.Ref);
            CentrePanel.Load();
        }

        /// <summary>
        /// Shorcut vers <see cref="LoadJoueurToPanel"/> et <see cref="LoadOpenToPanels"/>.
        /// </summary>
        public static void LoadInfoToPanel()
        {
            LoadOpenToPanels();
            LoadJoueurToPanel();
        }

        /// <summary>
        /// Shortcut vers <see cref="LoadJoueurFromPanel"/> et <see cref="LoadOpenFromPanel"/>.
        /// </summary>
        /// <param name="resetPanel">Vrai si on veut reset lzs panneaux après la récupération des infos.</param>
        public static void LoadInfoFromPanel(bool resetPanel = false)
        {
            LoadJoueurFromPanel(resetPanel);
            LoadOpenFromPanel(resetPanel);
        }



        //
        // Gestion d'élements graphiques divers
        //
        
        /// <summary>
        /// Remplit les différentes combo box depuis les valeurs des objets métiers.
        /// </summary>
        public static void FillComboBoxes()
        {
            foreach (HiddenComboBox cb in JoueurPanel.ComboBoxes)
            {
                if (cb is RepasComboBox)
                    ((RepasComboBox)cb).SetDataSource(GRepas.ListerRepas(), "NomEtPrix", "Ref");
                else if (cb is OpenComboBox)
                    ((OpenComboBox)cb).SetDataSource(GOpen.ListerOpens(), "Nom", "Ref");
                else if (cb is LieuComboBox)
                    ((LieuComboBox)cb).SetDataSource(GLieu.ListerLieux(), "Nom", "Ref");
                else if (cb is ClubComboBox)
                    ((ClubComboBox)cb).SetDataSource(GClub.ListerClubs(), "Nom", "Ref");
            }

            foreach (HiddenComboBox cb in OpenPanel.ComboBoxes)
            {
                if (cb is RepasComboBox)
                    ((RepasComboBox)cb).SetDataSource(GRepas.ListerRepas(), "NomEtPrix", "Ref");
                else if (cb is OpenComboBox)
                    ((OpenComboBox)cb).SetDataSource(GOpen.ListerOpens(), "Nom", "Ref");
                else if (cb is LieuComboBox)
                    ((LieuComboBox)cb).SetDataSource(GLieu.ListerLieux(), "Nom", "Ref");
                else if (cb is ClubComboBox)
                    ((ClubComboBox)cb).SetDataSource(GClub.ListerClubs(), "Nom", "Ref");
            }
        }

        /// <summary>
        /// Enable ou disable l'ensemble des panneaux de l'interface.
        /// </summary>
        /// <param name="value">Vrai pour enable, faux pour disable.</param>
        /// <param name="affectMenu">Vrai enable ou disable aussi <see cref="MsMenu"/>.</param>
        /// <param name="affectStatus">Vrai enable ou disable aussi <see cref="StatusPanel"/>.</param>
        public static void EnableAll(bool value = true, bool affectMenu = true, bool affectStatus = true)
        {
            CChesstion.JoueurPanel.Enabled = value;
            CChesstion.OpenPanel.Enabled = value;
            CChesstion.OpensPanel.Enabled = value;
            CChesstion.CentrePanel.Enabled = value;
            CChesstion.RepasPanel.Enabled = value;
            CChesstion.ActionPanel.Enabled = value;
            if (affectMenu) CChesstion.MsMenu.Enabled = value;
            if (affectStatus) CChesstion.StatusPanel.Enabled = value;
        }

        /// <summary>
        /// Trigger les fonctions de redimensionnage de tous les panneaux.
        /// </summary>
        public static void ResizePanels()
        {
            //Debug.WriteLine("=== Resize ===");

            CChesstion.MsMenu.RelocateAndResize();
            CChesstion.OpensPanel.RelocateAndResize();
            CChesstion.RepasPanel.RelocateAndResize();
            CChesstion.JoueurPanel.RelocateAndResize();
            CChesstion.OpenPanel.RelocateAndResize();
            CChesstion.ActionPanel.RelocateAndResize();
            CChesstion.CentrePanel.RelocateAndResize();
            CChesstion.StatusPanel.RelocateAndResize();
        }

        /// <summary>
        /// Supprime toutes les données gérées par le logiciel (Joueurs, Opens...).
        /// </summary>
        public static void DeleteAll()
        {
            List<int> joueursRef = new List<int>();
            List<int> clubsRef = new List<int>();
            List<int> repasRef = new List<int>();
            List<int> opensRef = new List<int>();

            foreach (Joueur j in GJoueur.ListerJoueurs())
                joueursRef.Add(j.Ref);
            foreach (Club c in GClub.ListerClubs())
                clubsRef.Add(c.Ref);
            foreach (Repas r in GRepas.ListerRepas())
                if (r.Ref != GRepas.AUCUN_REPAS)
                    repasRef.Add(r.Ref);
            foreach (Open o in GOpen.ListerOpens())
                opensRef.Add(o.Ref);

            foreach (int r in joueursRef)
                CChesstion.SupprimerJoueur(r);
            foreach (int r in clubsRef)
                CChesstion.SupprimerClub(r, false);
            foreach (int r in repasRef)
                CChesstion.SupprimerRepas(r);
            foreach (int r in opensRef)
                CChesstion.SupprimerOpen(r, true);

            FillComboBoxes();
            CentrePanel.Load();
            JoueurPanel.Reset();
            JoueurSelectionne = null;

            var i = CChesstion.OpensPanel.Controls;
        }



        //
        // Gestion des joueurs
        //

        /// <summary>
        /// Confirme la participation au tournoi d'un joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="value">Vrai pour confirmer, faux pour annuler sa participation.</param>
        public static void ConfimerJoueur(int reference, bool value = true)
        {
            GJoueur.GetJoueur(reference).Confirme = value;
            CentrePanel.ConfirmerJoueur(reference, value);
            if (CChesstion.JoueurSelectionne != null && CChesstion.JoueurSelectionne.Ref == reference)
                JoueurPanel.ConfirmeJoueur(value);
        }

        /// <summary>
        /// Crée un joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="nrFFE">Numéro FFE du joueur.</param>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="prenom">Prénom du joueur.</param>
        /// <param name="sexe">Sexe du joueur.</param>
        /// <param name="neLe">Date de naissance du joueur.</param>
        /// <param name="cat">Catégorie du joueur.</param>
        /// <param name="federation">Fédération du joueur.</param>
        /// <param name="clubRef">Référence du <see cref="Club"/> du joueur.</param>
        /// <param name="elo">Elo du joueur.</param>
        /// <param name="rapide">Rapide du joueur.</param>
        /// <param name="fide">Fide du joueur.</param>
        /// <param name="fideCode">Code fide du joueur.</param>
        /// <param name="fideTitre">Titre fide du joueur.</param>
        /// <param name="affType">AffType du joueur.</param>
        /// <param name="actif">Actif du joueur.</param>
        /// <param name="openRef">Référence de l'open du joueur.</param>
        /// <param name="email">Eamil du joueur.</param>
        /// <param name="subscribe">Vrai si le joueur veut recevoir les mails.</param>
        /// <param name="phone">Numéro de téléphone du joueur.</param>
        /// <param name="refRepas">Référence du <see cref="Repas"/> du joueur.</param>
        /// <param name="confirme">Vrai si la participation du joueur est confirmée.</param>
        /// <param name="reloadPanel">Vrai pour recharger <see cref="CentrePanel"/> après l'ajout du joueur.</param>
        /// <returns>Le joueur ajouté.</returns>
        public static Joueur CreerJoueur(int reference, string nrFFE, string nom, string prenom, string sexe,
            string neLe,
            string cat, string federation, int clubRef, int elo, int rapide, string fide, string fideCode,
            string fideTitre,
            string affType, string actif, int openRef, string email, bool subscribe, string phone, int refRepas, bool confirme = false, bool reloadPanel = true)
        {
            if (CChesstion.TournoiSelectionne == null)
                throw new NullReferenceException("Aucun tournoi sélectionné !");

            /*if (CChesstion.OpenSelectionne == null)
                throw new NullReferenceException("Aucun Open sélectionné !");*/

            Joueur joueur = GJoueur.CreerJoueur(reference, nrFFE, nom, prenom, sexe, neLe, cat, federation, clubRef, elo,
                rapide, fide, fideCode, fideTitre, affType, actif, openRef, email, subscribe, phone, refRepas, confirme);

            JoueurErrorsCache.Set(reference.ToString(), GJoueur.ComporteDesErreurs(reference), 60);

            if (reloadPanel)
                CChesstion.CentrePanel.Load();

            return joueur;
        }

        /// <summary>
        /// Crée un joueur depuis sont numéro FFE depuis la base FFE (DATA.MDB).
        /// </summary>
        /// <param name="nrFFEjoueur">Numéro FFE du joueur.</param>
        /// <param name="fillComboBox">Vrai pour appeler <see cref="FillComboBoxes"/> après l'ajout du joueur.</param>
        /// <returns>Le joueur ajouté.</returns>
        public static Joueur CreerJoueur(string nrFFEjoueur, bool fillComboBox = false)
        {
            OleDbConnection connec =
                new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath +
                                    @"\Ressources\DATA.mdb");
            OleDbCommand command = new OleDbCommand("SELECT * FROM JOUEUR WHERE NrFFE = '" + nrFFEjoueur.ToUpper() + "'", connec);
            List<object> data = new List<object>();
            connec.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            data.Add(reader.GetInt32(0));   // Ref
            data.Add(reader.GetString(1));  // NrFFE
            data.Add(reader.GetString(2));  // Nom
            data.Add(reader.GetString(3));  // Prenom
            data.Add(reader.GetString(4));  // Sexe
            data.Add(reader.GetDateTime(5).ToShortDateString()); // NeLe
            data.Add(reader.GetString(6)); // Cat
            data.Add(reader.GetString(7)); // Federation
            data.Add(reader.GetInt32(8)); // ClubRef
            data.Add((int)reader.GetInt16(9)); // Elo
            data.Add((int)reader.GetInt16(10)); // Rapide
            data.Add(reader.GetString(11)); // Fide
            data.Add(reader.GetString(12)); // Fidecode
            data.Add(reader.GetString(13)); // Fidetitre
            data.Add(reader.GetString(14)); // AffType
            data.Add(reader.GetString(15)); // Actif
            connec.Close();
            Joueur j = CChesstion.CreerJoueur(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(),
                data[3].ToString(), data[4].ToString(), data[5].ToString(), data[6].ToString(), data[7].ToString(),
                int.Parse(data[8].ToString()), int.Parse(data[9].ToString()), int.Parse(data[10].ToString()),
                data[11].ToString(), data[12].ToString(), data[13].ToString(), data[14].ToString(), data[15].ToString(), CChesstion.OpenSelectionne.Ref, "", false, "", GRepas.AUCUN_REPAS);

            if (GClub.GetClub(int.Parse(data[8].ToString())) == null)
                CChesstion.CreerClub(int.Parse(data[8].ToString()), fillComboBox);

            return j;
        }

        /// <summary>
        /// Crée un joueur avec le stricte minimum des informations essentielles.
        /// </summary>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="prenom">Prénom du joueur.</param>
        /// <param name="sexe">Sexe du joueur.</param>
        /// <param name="nationalite">Nationalité du joueur.</param>
        /// <param name="elo">Elo du joueur.</param>
        /// <param name="neLe">Date de naissance du joueur.</param>
        /// <param name="clubRef">Référence du club du joueur.</param>
        /// <returns>MLe joueur ajouté.</returns>
        public static Joueur CreerJoueur(string nom, string prenom, string sexe, string nationalite, int elo, string neLe, int clubRef)
        {
            return CChesstion.CreerJoueur(GJoueur.ProchaineRef++, "", nom, prenom, sexe, neLe, "", nationalite, clubRef, elo, 0,
                "", "", "", "", "", CChesstion.OpenSelectionne.Ref, "", false, "", GRepas.AUCUN_REPAS);
        }

        /// <summary>
        /// Changer l'open du joueur.
        /// </summary>
        /// <param name="referenceJoueur">Référence du joueur.</param>
        /// <param name="referenceOpen">Référence du nouvel open.</param>
        public static void ChangerOpenDuJoueur(int referenceJoueur, int referenceOpen)
        {
            if (!CChesstion.JoueurPanel.IsHandling)
                return;

            Joueur joueur = GJoueur.GetJoueur(referenceJoueur);
            Open open = GOpen.GetOpen(referenceOpen);

            if (joueur.Elo > open.EloMax)
                throw new ArgumentException("L'elo du joueur dépasse elo max de l'open.");

            if (!string.IsNullOrEmpty(CChesstion.OpenPanel.TextBoxes[0].LastValue))
            {
                CChesstion.LoadOpenFromPanel();
            }
            GJoueur.GetJoueur(referenceJoueur).OpenRef = referenceOpen;
            CChesstion.OpenSelectionne = GOpen.GetOpen(referenceOpen);
            CChesstion.LoadOpenToPanels();
        }

        /// <summary>
        /// Sélectionner un joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur à sélectionner.</param>
        public static void SelectionnerJoueur(int reference)
        {
            /* Debug */
            Stopwatch sw = new Stopwatch();
            string bef = "SelectionnerJoueur(int) - ";
            sw.Start();
            /* END Debug */

            CChesstion.LoadJoueurFromPanel();

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "LoadJoueurFromPanel() takes " + sw.ElapsedMilliseconds + " ms");
            sw.Restart();
            /* END Debug */

            CChesstion.JoueurSelectionne = GJoueur.GetJoueur(reference);

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "CChesstion.JoueurSelectionne = GJoueur.GetJoueur(reference); takes " + sw.ElapsedMilliseconds + " ms");
            sw.Restart();
            /* END Debug */

            CChesstion.LoadJoueurToPanel();

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "LoadJoueurToPanel() takes " + sw.ElapsedMilliseconds + " ms");
            sw.Restart();
            /* END Debug */

            //DisplayJoueurErrored(CChesstion.JoueurSelectionne.Ref, IsJoueurErrored(reference));

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "DisplayJoueurErrored takes " + sw.ElapsedMilliseconds + " ms");
            /* END Debug */
        }

        /// <summary>
        /// Affiche un joueur comme errored (ou non).
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="errored">Vrai pour qu'il apparaisse errored, faux pour qu'il apparaisse normal.</param>
        public static void DisplayJoueurErrored(int reference, bool errored = true)
        {
            
            if (GJoueur.GetJoueur(reference).Confirme)
                return;

            CentreJoueurLabel cjl = CChesstion.CentrePanel.GetLabel(reference);
            if (cjl != null)
                cjl.DisplayErrored = errored;

            if (CChesstion.JoueurSelectionne != null && CChesstion.JoueurSelectionne.Ref == reference)
            {
                CChesstion.JoueurPanel.Errored = errored;
            }
        }

        /// <summary>
        /// Renomme un joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="nom">Nouveau nom du joueur.</param>
        /// <param name="prenom">Nouveau prénom du joueur.</param>
        public static void RenommerJoueur(int reference, string nom, string prenom)
        {
            GJoueur.GetJoueur(reference).Nom = nom.ToUpper();
            GJoueur.GetJoueur(reference).Prenom = prenom;

            if (GJoueur.GetJoueur(reference).OpenRef == CChesstion.OpenSelectionne.Ref)
                CChesstion.CentrePanel.GetLabel(reference).Nom = nom.ToUpper() + " " + prenom;

            if (CChesstion.JoueurSelectionne.Ref == reference) { }
                CChesstion.JoueurPanel.Title = nom.ToUpper() + " " + prenom;
        }

        /// <summary>
        /// Retourne les erreurs d'un joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <returns>Liste d'erreurs du joueur. En [0] se trouve le nom du champ, en [1] la valeur trouvée en base FFE.</returns>
        public static List<string[]> GetJoueurErrors(int reference)
        {
            /* Avec cache */
            List<string[]> liste = JoueurErrorsCache.Get(reference); 
            /* */

            /* Sans cache *
            List<string> liste = GJoueur.ComporteDesErreurs(reference);
            /* */
            return liste;

        }

        /// <summary>
        /// Vérifie si un joueur est errored (si les valeurs de l'objet ne correspondent pas avoir les valeurs en base FFE).
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <returns>Vrai s'il est errored, faux sinon.</returns>
        public static bool IsJoueurErrored(int reference)
        {
            return GetJoueurErrors(reference).Count > 0;
        }

        /// <summary>
        /// Supprime un joueur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="reload">Vrai pour reload le <see cref="CentrePanel"/>.</param>
        /// <param name="resetJoueurPanel">Vrai pour reset le <see cref="JoueurPanel"/>.</param>
        public static void SupprimerJoueur(int reference, bool reload = false, bool resetJoueurPanel = true)
        {
            if (CChesstion.JoueurSelectionne != null && CChesstion.JoueurSelectionne.Ref == reference)
            {
                CChesstion.JoueurSelectionne = null;
                if (resetJoueurPanel)
                    CChesstion.JoueurPanel.Reset();
            }

            if (GOpen.GetOpenDuJoueur(reference) == null)
                return;

            GOpen.GetOpenDuJoueur(reference).SupprimerJoueur(reference);
            GJoueur.ListerJoueurs().Remove(GJoueur.GetJoueur(reference));
            if (reload)
                CChesstion.CentrePanel.Load();

        }



        //
        // Open
        //

        /// <summary>
        /// Crée un <see cref="Open"/>.
        /// </summary>
        /// <param name="nom">Nom de l'open.</param>
        /// <param name="eloMax">Elo maximum pour s'inscrire à l'open.</param>
        /// <returns>L'open créé.</returns>
        public static Open CreerOpen(string nom, int eloMax)
        {
            if (CChesstion.TournoiSelectionne == null)
                throw new NullReferenceException("Aucun tournoi sélectionné !");

            Open open = GOpen.CreerOpen(nom, eloMax);
            return AjouterOpen(open);
        }

        /// <summary>
        /// Crée un <see cref="Open"/> en copiant un objet de référence.
        /// </summary>
        /// <param name="open">Open à copier.</param>
        /// <returns>L'open créé.</returns>
        public static Open CreerOpen(Open open)
        {
            if (CChesstion.TournoiSelectionne == null)
                throw new NullReferenceException("Aucun tournoi sélectionné !");

            open = GOpen.CreerOpen(open.Ref, open.Nom, open.EloMax);
            return AjouterOpen(open);
        }

        /// <summary>
        /// Ajoute un <see cref="Open"/>.
        /// </summary>
        /// <param name="open">Open à ajouter.</param>
        /// <returns>L'open ajouté.</returns>
        private static Open AjouterOpen(Open open)
        {
            TournoiSelectionne.AjouterOpen(open);
            FillComboBoxes();
            OpensPanel.AddOpenMenuButton(open.Nom, open.Ref);
            return open;
        }

        /// <summary>
        /// Supprime un <see cref="Open"/>.
        /// </summary>
        /// <param name="reference">Référence de l'open à supprimer.</param>
        /// <param name="limitIgnore">Faux empêchera de supprimer l'open si c'est le dernier ; vrai supprimera de toute façon.</param>
        public static void SupprimerOpen(int reference, bool limitIgnore = false)
        {
            if (GOpen.GetOpen(reference).TsLesJoueurs.Count > 0)
                throw new ArgumentException("Il existe encore des joueurs dans cet open !");
            if (GOpen.ListerOpens().Count == 1 && !limitIgnore)
                throw new ArgumentException("Il doit exister au moins un open !");

            try
            {
                OpensPanel.DeleteMenuButton(reference);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            GTournoi.GetTournoiDeOpen(reference).TsLesOpens.Remove(GOpen.GetOpen(reference));
            GOpen.SupprimerOpen(reference);
            FillComboBoxes();

        }

        /// <summary>
        /// Renomme un <see cref="Open"/>.
        /// </summary>
        /// <param name="reference">Référence de l'open à renommer.</param>
        /// <param name="nom">Nouveau nom de l'open.</param>
        public static void ChangerNomOpen(int reference, string nom)
        {
            GOpen.GetOpen(reference).Nom = nom;
            FillComboBoxes();

            foreach (OpensMenuButton omb in OpensPanel.MenuButtons)
                if ((int) omb.Tag == reference)
                {
                    omb.Text = nom;
                    if (omb.Selected)
                    {
                        OpenPanel.FormattedTitle = omb.Text;
                        JoueurPanel.OpenComboBox.SelectedValue = reference;
                    }

                }

        }

        /// <summary>
        /// Sélectionne un <see cref="Open"/>.
        /// </summary>
        /// <param name="reference">Référence de l'open à sélectionner.</param>
        public static void SelectionnerOpen(int reference)
        {
            if (GOpen.GetOpen(reference) == null)
                return;

            /* Debug */
            Stopwatch sw = new Stopwatch();
            string bef = "SelectionnerOpen(int) - ";
            sw.Start();
            /* END Debug */

            CChesstion.LoadInfoFromPanel();

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "LoadInfoFromPanel() takes " + sw.ElapsedMilliseconds + " ms");
            sw.Restart();
            /* END Debug */

            CChesstion.OpenSelectionne = GOpen.GetOpen(reference);

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "CChesstion.OpenSelectionne = GOpen.GetOpen(reference); takes " + sw.ElapsedMilliseconds + " ms");
            sw.Restart();
            /* END Debug */

            CChesstion.LoadOpenToPanels();

            /* Debug */
            sw.Stop();
            Debug.WriteLine(bef + "LoadOpenToPanels() takes " + sw.ElapsedMilliseconds + " ms");
            /* END Debug */
        }



        //
        // Repas
        //

        /// <summary>
        /// Crée un <see cref="Repas"/>.
        /// </summary>
        /// <param name="nom">Nom du repas à créer.</param>
        /// <param name="prix">Prix du repas à créer.</param>
        public static int CreerRepas(string nom, float prix)
        {
            int reference = GRepas.CreerRepas(nom, prix).Ref;
            CChesstion.TournoiSelectionne.AjouterRepas(reference);
            CChesstion.RepasPanel.AddRepas(reference);

            FillComboBoxes();

            return reference;
        }

        /// <summary>
        /// Crée un <see cref="Repas"/> à partir d'un autre <see cref="Repas"/>.
        /// </summary>
        /// <param name="r"></param>
        public static void CreerRepas(Repas r)
        {
            int reference = GRepas.CreerRepas(r.Ref, r.Nom, r.Prix).Ref;
            CChesstion.TournoiSelectionne.AjouterRepas(reference);
            CChesstion.RepasPanel.AddRepas(reference);

            FillComboBoxes();

        }

        /// <summary>
        /// Change les infos d'un <see cref="Repas"/>.
        /// </summary>
        /// <param name="reference">Référence du repas.</param>
        /// <param name="nom">Nouveau nom du repas.</param>
        /// <param name="prix">Nouveau prix du repas.</param>
        public static void ChangerInfoRepas(int reference, string nom, float prix)
        {
            if (GRepas.GetRepas(reference) == null)
                return;

            GRepas.GetRepas(reference).Nom = nom;
            GRepas.GetRepas(reference).Prix = prix;

            FillComboBoxes();
        }

        /// <summary>
        /// Supprime un <see cref="Repas"/>.
        /// </summary>
        /// <param name="reference">Référence du repas à supprimer.</param>
        public static void SupprimerRepas(int reference)
        {
            foreach (Open o in CChesstion.TournoiSelectionne.TsLesOpens)
            {
                Debug.WriteLine("TEsting open " + o.Nom);
                foreach (Joueur j in o.TsLesJoueurs)
                {
                    Debug.WriteLine("TEsting joueur " + j.Nom + "(repas : " + j.RepasRef + ")");
                    if (j.RepasRef == reference)
                        throw new ArgumentException("Un joueur a commandé ce repas : " + j.Prenom + " " +
                                                    j.Nom.ToUpper() + " (" + GOpen.GetOpen(j.OpenRef).TitreFormatte() + ")");
                }
            }

            CChesstion.RepasPanel.DeleteRepas(reference);
            CChesstion.TournoiSelectionne.SupprimerRepas(reference);
            GRepas.SupprimerRepas(reference);
            FillComboBoxes();

        }



        //
        // Club
        //

        /// <summary>
        /// Crée un <see cref="Club"/>.
        /// </summary>
        /// <param name="reference">Référence du club.</param>
        /// <param name="nrFFE">Numéro FFE du club.</param>
        /// <param name="nom">Nom du club.</param>
        /// <param name="ligue">Ligue du club.</param>
        /// <param name="commune">Commune du club.</param>
        /// <param name="actif">Actif du club.</param>
        /// <param name="fillComboBoxes">Vrai pour appeler <see cref="FillComboBoxes"/> après la création.</param>
        /// <returns>Le club créé.</returns>
        public static Club CreerClub(int reference, string nrFFE, string nom, string ligue, string commune, string actif, bool fillComboBoxes = true)
        {
            foreach (Club c in GClub.ListerClubs())
                if (c.Ref == reference)
                    throw new ArgumentException("Un club avec cette référence existe déjà.");

            Club club = GClub.CreerClub(reference, nrFFE, nom, ligue, commune, actif);

            if (fillComboBoxes) CChesstion.FillComboBoxes();

            return club;
        }

        /// <summary>
        /// Crée un <see cref="Club"/> depuis sa référence et des valeurs trouvées en base FFE (DATA.MDB).
        /// </summary>
        /// <param name="reference">Référence du club à créer.</param>
        /// <param name="fillComboBox">Vrai pour appeler <see cref="FillComboBoxes"/> après la création.</param>
        /// <returns>Le club créé.</returns>
        public static Club CreerClub(int reference, bool fillComboBox = false)
        {
            OleDbConnection connec =
                new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath +
                                    @"\Ressources\DATA.mdb");
            OleDbCommand command = new OleDbCommand("SELECT * FROM CLUB WHERE Ref = " + reference, connec);
            List<object> data = new List<object>();
                connec.Open();
                OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            data.Add(reader.GetInt32(0));
            data.Add(reader.GetString(1));
            data.Add(reader.GetString(2));
            data.Add(reader.GetString(3));
            data.Add(reader.GetString(4));
            data.Add(reader.GetString(5));
            connec.Close();
            return CChesstion.CreerClub(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(),
                data[3].ToString(), data[4].ToString(), data[5].ToString(), fillComboBox);
        }

        /// <summary>
        /// Supprime un <see cref="Club"/>.
        /// </summary>
        /// <param name="clubRef">Référence du club à supprimer.</param>
        /// <param name="fillComboBoxes">Vrai pour appeler <see cref="FillComboBoxes"/> après la suppression.</param>
        private static void SupprimerClub(int clubRef, bool fillComboBoxes = true)
        {
            GClub.SupprimerClub(clubRef);

            if (fillComboBoxes)
                FillComboBoxes();
        }




        //
        // Tournoi
        //

        /// <summary>
        /// Passe le <see cref="TournoiSelectionne"/> à l'état suivant.
        /// </summary>
        public static void TournoiProchainEtat()
        {
            if (CChesstion.TournoiSelectionne.Etat == GTournoi.ETAT__TOURNOI_TERMINE)
                return;

            CChesstion.TournoiSelectionne.Etat++;

            try
            {
                if (MessageBox.Show("Êtes-vous sûr ? Cette opération est irréversible (et peut prendre plusieurs minutes).", "Confirmer",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    UpdateTournoiEtat();
                else
                {
                    CChesstion.TournoiSelectionne.Etat--;
                    UpdateTournoiEtat(true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                CChesstion.TournoiSelectionne.Etat--;
                UpdateTournoiEtat(true);
            }

        }

        

        //
        // Etats tournois
        //

        /// <summary>
        /// Appele les <see cref="Etat"/> associées à l'état du <see cref="TournoiSelectionne"/>.
        /// </summary>
        /// <param name="interfaceOnly">Vrai pour n'afficher que l'interface associée à l'état sans exécuter tout le processus de transition.</param>
        public static void UpdateTournoiEtat(bool interfaceOnly = false)
        {
            EnableAll(false, true, true);
            switch (CChesstion.TournoiSelectionne.Etat)
            {
                case GTournoi.ETAT__CREATION:
                    CChesstion.ChangerEtat_Creation(interfaceOnly);
                    break;

                case GTournoi.ETAT__INSCRIPTIONS_OUVERTES:
                    CChesstion.ChangerEtat_Inscriptions(interfaceOnly);
                    break;

                case GTournoi.ETAT__ACCUEIL_JOUEURS:
                    CChesstion.ChangerEtat_Accueil(interfaceOnly);
                    break;

                case GTournoi.ETAT__TOURNOI_EN_COURS:
                    CChesstion.ChangerEtat_DurantTournoi(interfaceOnly);
                    break;

                case GTournoi.ETAT__TOURNOI_TERMINE:
                    CChesstion.ChangerEtat_TournoiTermine(interfaceOnly);
                    break;
            }
        }

        /// <summary>
        /// Interface vers <see cref="TournoiTermine"/>.
        /// </summary>
        /// <param name="interfaceOnly">Vrai pour n'afficher que l'interface associée à l'état sans exécuter tout le processus de transition.</param>
        private static void ChangerEtat_TournoiTermine(bool interfaceOnly = false)
        {
            TournoiTermine tt = new TournoiTermine();
            if (interfaceOnly)
            {
                tt.LoadInterface();
                return;
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                

                if (((string)e.Result).Equals(string.Empty))
                {
                    
                    tt.Transition();
                }
                else
                {
                    MessageBox.Show(((string)e.Result));
                    CChesstion.TournoiSelectionne.Etat--;
                    UpdateTournoiEtat(true);
                }
            };
            bw.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                e.Result = NoInternetIssues();
            };
            bw.RunWorkerAsync();
            CChesstion.StatusPanel.Tip = "Vérification de la connexion internet...";
        }

        /// <summary>
        /// Interface vers <see cref="DurantTournoi"/>.
        /// </summary>
        /// <param name="interfaceOnly">Vrai pour n'afficher que l'interface associée à l'état sans exécuter tout le processus de transition.</param>
        private static void ChangerEtat_DurantTournoi(bool interfaceOnly = false)
        {
            bool auMoinsUnConfirme = false;
            foreach(Joueur joueur in GJoueur.ListerJoueurs())
                if (joueur.Confirme)
                {
                    auMoinsUnConfirme = true;
                    break;
                }

            if (!auMoinsUnConfirme)
                throw new Exception("Aucun joueur confirmé.");

            DurantTournoi dt = new DurantTournoi();
            if (interfaceOnly) dt.LoadInterface();
            else dt.Transition();
        }

        /// <summary>
        /// Interface vers <see cref="Accueil"/>.
        /// </summary>
        /// <param name="interfaceOnly">Vrai pour n'afficher que l'interface associée à l'état sans exécuter tout le processus de transition.</param>
        private static void ChangerEtat_Accueil(bool interfaceOnly = false)
        {
            Accueil a = new Accueil();
            if (interfaceOnly)
            {
                a.LoadInterface();
                return;
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                if (((string)e.Result).Equals(string.Empty))
                {
                    
                    a.Transition();
                }
                else
                {
                    MessageBox.Show(((string)e.Result));
                    CChesstion.TournoiSelectionne.Etat--;
                    UpdateTournoiEtat(true);
                }
            };
            bw.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                e.Result = NoInternetIssues();
            };
            bw.RunWorkerAsync();
            CChesstion.StatusPanel.Tip = "Vérification de la connexion internet...";
        }

        /// <summary>
        /// Interface vers <see cref="Inscriptions"/>.
        /// </summary>
        /// <param name="interfaceOnly">Vrai pour n'afficher que l'interface associée à l'état sans exécuter tout le processus de transition.</param>
        private static void ChangerEtat_Inscriptions(bool interfaceOnly = false)
        {
            Inscriptions i = new Inscriptions();
            if (interfaceOnly)
            {
                i.LoadInterface();
                return;
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                if (((string) e.Result).Equals(string.Empty))
                {
                    i.Transition();
                }
                else
                {
                    MessageBox.Show(((string) e.Result));
                    CChesstion.TournoiSelectionne.Etat--;
                    UpdateTournoiEtat(true);
                }
            };
            bw.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                e.Result = NoInternetIssues();
            };
            bw.RunWorkerAsync();
            CChesstion.StatusPanel.Tip = "Vérification de la connexion internet...";
        }

        /// <summary>
        /// Interface vers <see cref="Creation"/>.
        /// </summary>
        /// <param name="interfaceOnly">Vrai pour n'afficher que l'interface associée à l'état sans exécuter tout le processus de transition.</param>
        private static void ChangerEtat_Creation(bool interfaceOnly = false)
        {
            Creation c = new Creation();
            if (interfaceOnly) c.LoadInterface();
            else c.Transition();
        }

        /// <summary>
        /// Vérifie qu'il n'y a aucun problème lié à la connexion Internet.
        /// </summary>
        /// <returns>Une chaîne vide si aucun problème, sinon un message d'erreur associé au problème.</returns>
        public static string NoInternetIssues()
        {
            if (!FTPAdapter.IsInternetConnectionOK())
                return "Vous devez être connecté à Internet !";

            LoadFtpLogin();
            if (!FTPAdapter.IsFtpConnectionOK())
                return
                    "Impossible de se connecter, les informations de connexion FTP fournis sont erronées : vérifiez les informations entrées dans *dossier d'installation*/Settings/settings.json";

            return string.Empty;
        }

        /// <summary>
        /// Récupère les identifiants FTP depuis le fichier de config.
        /// </summary>
        public static void LoadFtpLogin()
        {
            if (File.Exists(CChesstion.SettingsFolder + "/settings.json"))
            {
                JObject o = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"))["ftp"].Value<JObject>();
                FTPAdapter.Username = (string)o["username"];
                FTPAdapter.Password = (string)o["password"];
                FTPAdapter.URL = (string)o["host"];
            }
        }

        public static void LoadPapiPath()
        {
            if (File.Exists(CChesstion.SettingsFolder + "/settings.json"))
            {
                CChesstion.PapiFolder = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"))["papiPath"].ToString();
            }
        }



        //
        // Status
        //

        /// <summary>
        /// Affiche ou masque le <see cref="StatusPanel"/>.
        /// </summary>
        /// <param name="visible">Vrai pour afficher, faux pour masquer.</param>
        /// <param name="closable">Vrai pour laisser à l'utilisateur la possibilité de masque le panneau, faux pour l'en empêcher.</param>
        public static void ShowStatusPanel(bool visible = true, bool closable = true)
        {
            CChesstion.StatusPanel.Visible = visible;
            CChesstion.StatusPanel.Closable = closable;
            CChesstion.MsMenu.L1Status.Enabled = closable;
            CChesstion.MsMenu.L1Status.Text = visible
                ? "Masquer le panneau d'information/de statut"
                : "Afficher le panneau d'information/de statut";
        }


        
    }
}
