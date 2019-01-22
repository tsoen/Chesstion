using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.ImportExport;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;

namespace ChessTion.Controleur.Etats
{
    /// <summary>
    /// Classe gérant l'étape de vérification des inscriptions et d'accueil des joueurs.
    /// </summary>
    class Accueil : Etat
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
        /// Numéro de l'état.
        /// </summary>
        public override int Etape { get; protected set; } = 3;

        /// <summary>
        /// Nom de l'état.
        /// </summary>
        public override string Name { get; protected set; } = "Contrôle & accueil des joueurs";

        /// <summary>
        /// Chemin d'accès du fichier local.
        /// </summary>
        private string FilePath { get; set; } = "";

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
        /// Méthode à appeler pour effectuer l'ensemble des actions qui mènent de l'état précédent à cet état.
        /// </summary>
        /// <param name="loadInterface">Vrai pour appeler <see cref="LoadInterface"/> à la fin de cette méthode.</param>
        public override void Transition(bool loadInterface = true)
        {
            CChesstion.EnableAll(false);
            CChesstion.ShowStatusPanel(false, false);
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);

            int tournoiRef = CChesstion.TournoiSelectionne.Ref;

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                BackgroundWorker.ReportProgress(0);
                FTPAdapter.DownloadFile(CChesstion.BasePath + @"\Ressources\json\Inscrits\" + tournoiRef + ".json",
                    @"json/Inscrits/" + tournoiRef + ".json");

                ImportJoueurs.FromJson(tournoiRef, CChesstion.BasePath + "/Ressources/json/Inscrits/" + tournoiRef + ".json", false, BackgroundWorker);

            };
            BackgroundWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                CChesstion.StatusPanel.Invoke(new MethodInvoker(delegate
                {
                    CChesstion.StatusPanel.Tip = "Progression : " + e.ProgressPercentage + " %";
                }));
            };
            BackgroundWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                BackgroundWorker = new BackgroundWorker();

                // S'occupe d'exporter en Json et de l'écrire dans un fichier
                BackgroundWorker.DoWork += DoWork_ExportJsonToFile;
                BackgroundWorker.ProgressChanged += (object ss, ProgressChangedEventArgs ee) =>
                {
                    // NOPE
                    CChesstion.StatusPanel.Invoke(new MethodInvoker(delegate
                    {
                        CChesstion.StatusPanel.Tip = "Export vers fichier .json : " + ee.ProgressPercentage + " %";
                    }));
                };
                BackgroundWorker.RunWorkerCompleted += DoWork_UploadFile;
                BackgroundWorker.WorkerReportsProgress = true;

                BackgroundWorker.RunWorkerAsync(loadInterface);     
            };
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.RunWorkerAsync(loadInterface);

            CChesstion.StatusPanel.Title = "Importation des inscrits...";
            CChesstion.StatusPanel.Message = "Chesstion est en train de télécharger le liste des joueurs inscrits depuis le formulaire Web.\nVeuillez patienter...";
            CChesstion.StatusPanel.Closable = false;
            CChesstion.StatusPanel.Visible = true;
            CChesstion.ActionPanel.EnableFermerInscriptions = false;

        }

        /// <summary>
        /// Upload le fichier json généré.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_UploadFile(object sender, RunWorkerCompletedEventArgs e)
        {
            Action action = () =>
            {
                LoadInterface();
            };

            FTPAdapter.UploadFileAsync(FilePath, "json/Tournois/" + CChesstion.TournoiSelectionne.Ref + ".json", action,
                    new Action<ulong>((ulong percent) =>
                    {
                        CChesstion.StatusPanel.Invoke(new MethodInvoker(delegate
                        {
                            CChesstion.StatusPanel.Tip = "Upload sur le site : " + (percent * 60 / 100 + 40) + " %";
                        }));
                    }));

            /*
            FTPAdapter.UploadFileAsync(FilePath, "json/Tournois/" + CChesstion.TournoiSelectionne.Ref + ".json",
                    new UploadProgressChangedEventHandler((object s, UploadProgressChangedEventArgs args) =>
                    {
                        CChesstion.StatusPanel.Tip = "Upload sur le site : " + (args.ProgressPercentage * 60 / 100 + 40) + " %";
                    }),
                    new UploadFileCompletedEventHandler((object s, UploadFileCompletedEventArgs args) =>
                    {
                        if ((bool)e.Result)
                            LoadInterface();
                    }));
                    */
        }


        private void DoWork_ExportJsonToFile(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker.ReportProgress(0);
            string text = ExportTournoi.ToJson(CChesstion.TournoiSelectionne.Ref);
            FilePath = CChesstion.BasePath + @"\Ressources\json\Tournois\" + CChesstion.TournoiSelectionne.Ref + ".json";
            BackgroundWorker.ReportProgress(20);
            File.WriteAllText(FilePath, text);
            BackgroundWorker.ReportProgress(40);
            e.Result = e.Argument;    
        }


        /// <summary>
        /// Charge l'interface pour coller à l'état.
        /// </summary>
        public override void LoadInterface()
        {
            CChesstion.CentrePanel.Load();
            CChesstion.StatusPanel.Title = FullName;
            CChesstion.StatusPanel.Message = "Vous venez de récupérer la liste des inscriptions. Les joueurs sont maintenant classés par open. "
                + "Cliquez sur le nom de l'un d'entre eux pour accéder à toutes ses infos (panel en haut à droite). Vous pouvez modifier ces informations.\n\n"
                + "Un joueur apparaissant en rouge indique qu'une erreur lui est associée. Cliquez sur celui-ci pour connaître le champ précis qui pose problème.\n\n"
                + "Au besoin, on peut ajouter ou supprimer (définitivement !) un joueur à un open : utilisez les boutons en bas du panneau central. "
                + "Le jour du tournoi, lorsqu'un joueur se sera présenté et aura payé, vous pourrez confirmer sa venue à l'aide du bouton Confirmer (panel en haut à droite). "
                + "Attention, seuls les joueurs confirmés seront ensuite exportés vers Papi !\n"
                + "Une fois l'accueil des joueurs terminé, cliquez sur Débuter le tournoi.";
            CChesstion.StatusPanel.Tip = "TIP : Vous pouvez trier les joueurs par référence, nom, masquer les joueurs confirmés ou encore faire une recherche à l'aide des "
                                               + "différents boutons et champs du panneau central !";
            CChesstion.ShowStatusPanel(true, true);
            CChesstion.FillComboBoxes();
            CChesstion.EnableAll();
            CChesstion.OpenPanel.ReadOnly = true;
            CChesstion.OpensPanel.ReadOnly = true;
            CChesstion.RepasPanel.ReadOnly = true;
            CChesstion.CentrePanel.AllowAdd = true;
            CChesstion.CentrePanel.AllowDelete = true;
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.LEFT_BUTTON);
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);
            CChesstion.ActionPanel.EnableAll(false);
            CChesstion.ActionPanel.SetProgressed(-1, false);
            CChesstion.ActionPanel.EnableDebuterTournoi = true;
            CChesstion.ActionPanel.SetProgressed(1);
            CChesstion.ActionPanel.SetProgressed(2);
            CChesstion.JoueurPanel.EnabledButtons(true);
            CChesstion.RepasPanel.AllowAdd = false;
            CChesstion.RepasPanel.AllowDelete = true;
            CChesstion.MsMenu.EnableUpdateInscrits = true;


            Save.PerformSave();

        }
    }
}
