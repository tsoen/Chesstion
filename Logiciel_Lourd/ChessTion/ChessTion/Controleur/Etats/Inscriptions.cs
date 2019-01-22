using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.ImportExport;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Clipboard = System.Windows.Clipboard;

namespace ChessTion.Controleur.Etats
{
    /// <summary>
    /// Classe gérant l'étape durant laquelle les inscriptions sont ouvertes.
    /// </summary>
    class Inscriptions : Etat
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
        /// Chemin d'accès du fichier local.
        /// </summary>
        private string FilePath { get; set; } = "";

        /// <summary>
        /// Chemin d'accès du dossier distant.
        /// </summary>
        private string Url { get; set; } = "";

        /// <summary>
        /// Vrai pour charger l'interface à la fin de l'éape de transition.
        /// </summary>
        private bool loadInterface = true;

        /// <summary>
        /// Numéro de l'état.
        /// </summary>
        public override int Etape { get; protected set; } = 2;

        /// <summary>
        /// Nom de l'état.
        /// </summary>
        public override string Name { get; protected set; } = "Inscription des joueurs";

        private Timer timer = new Timer();

        private SFTP asynchFtp;


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
            this.loadInterface = loadInterface;
            CChesstion.EnableAll(false);
            CChesstion.ShowStatusPanel(false, false);

            BackgroundWorker = new BackgroundWorker();

            // S'occupe d'exporter en Json et de l'écrire dans un fichier
            BackgroundWorker.DoWork += DoWork_ExportJsonToFile;
            BackgroundWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                CChesstion.StatusPanel.Tip = "Export vers fichier .json : " + e.ProgressPercentage + " %";
            };
            BackgroundWorker.RunWorkerCompleted += DoWork_UploadFile;
            BackgroundWorker.WorkerReportsProgress = true;

            BackgroundWorker.RunWorkerAsync();
            CChesstion.StatusPanel.Title = "Ouverture des inscriptions";
            CChesstion.StatusPanel.Message = "Chesstion est en train d'ouvrir les inscriptions aux joueurs.\nVeuillez patienter...";
            CChesstion.ShowStatusPanel(true, false);
            CChesstion.ActionPanel.EnableOuvrirInscriptions = false;
            CChesstion.ActionPanel.EnableFermerInscriptions = true;
            CChesstion.ActionPanel.SetProgressed(1);

            Save.PerformSave();
        }

        /// <summary>
        /// Export le tournoi en fichier json.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_ExportJsonToFile(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker.ReportProgress(0);
            string text = ExportTournoi.ToJson(CChesstion.TournoiSelectionne.Ref);
            FilePath = CChesstion.BasePath + @"\Ressources\json\Tournois\" + CChesstion.TournoiSelectionne.Ref + ".json";
            BackgroundWorker.ReportProgress(20);
            File.WriteAllText(FilePath, text);
            BackgroundWorker.ReportProgress(40);
        }

        /// <summary>
        /// Upload le fichier json généré.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_UploadFile(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Start();

            long fileSize = new FileInfo(FilePath).Length;

            asynchFtp = FTPAdapter.UploadFileAsync(FilePath, "json/Tournois/" + CChesstion.TournoiSelectionne.Ref + ".json", 
                delegate {
                    Url = CChesstion.BaseURL + "/index.php/home/inscription/" + CChesstion.TournoiSelectionne.Ref;
                    Clipboard.SetText(Url);

                    if (loadInterface)
                    {
                        LoadInterface();
                    }
                }, 
                new Action<ulong>((ulong percent) =>
                {
                    CChesstion.StatusPanel.Invoke(new MethodInvoker(delegate
                    {
                        CChesstion.StatusPanel.Tip = "Upload sur le site : " + (int)percent / fileSize * 100 + " %";
                    }));
                }));

            /*
            asynchFtp = FTPAdapter.UploadFileAsync(FilePath, "json/Tournois/" + CChesstion.TournoiSelectionne.Ref + ".json",
                    new UploadProgressChangedEventHandler((object s, UploadProgressChangedEventArgs args) =>
                    {
                        CChesstion.StatusPanel.Tip = "Upload sur le site : " + (args.ProgressPercentage * 60 / 100 + 40) + " %";
                    }),
                    new UploadFileCompletedEventHandler((object s, UploadFileCompletedEventArgs args) =>
                    {
                        Url = CChesstion.BaseURL + "/index.php/home/inscription/" + CChesstion.TournoiSelectionne.Ref;
                        Clipboard.SetText(Url);

                        if (this.loadInterface)
                            LoadInterface();
                    }));
                    */
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            CChesstion.StatusPanel.Enabled = true;
            CChesstion.StatusPanel.ActivateButton(StatusPanel.MIDDLE_BUTTON, "Annuler", () =>
            {
                asynchFtp.CancelUploadAsync();
                CChesstion.TournoiSelectionne.Etat--;
                CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);
                CChesstion.UpdateTournoiEtat(true);
            });
            CustomQuickDialog q =
                new CustomQuickDialog(
                    "Cela semble prendre plus de temps que prévu...\nSi cela s'éternise, n'hésitez pas à annuler\net à relancer l'opération !",
                    Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogType.Warning, CChesstion.StatusPanel,
                    Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOnParent);
            q.DisplayDelay = 5000;
            q.Show();

        }

        /// <summary>
        /// Charge l'interface pour coller à l'état.
        /// </summary>
        public override void LoadInterface()
        {
            timer.Stop();
            CChesstion.StatusPanel.Title = FullName;
            CChesstion.StatusPanel.Message =
                "L'inscription au tournoi a été ouverte ! Les joueurs peuvent maintenant aller s'inscrire à l'adresse suivante :\n\n" + Url
                + "\n\n\nPour fermer les inscriptions et importer la liste des joueurs, utilisez le bouton plus bas. Attention, la fermeture est définitive.";
            CChesstion.StatusPanel.Tip = "TIP : L'adresse vient d'être copiée dans votre presse-papier !";
            CChesstion.ShowStatusPanel(true, true);
            CChesstion.EnableAll();
            CChesstion.OpensPanel.ReadOnly = true;
            CChesstion.RepasPanel.AllowAdd = false;
            CChesstion.RepasPanel.AllowDelete = false;
            CChesstion.CentrePanel.Enabled = false;
            CChesstion.RepasPanel.ReadOnly = true;
            CChesstion.RepasPanel.AllowAdd = false;
            CChesstion.RepasPanel.AllowDelete = true;
            CChesstion.OpenPanel.ReadOnly = true;
            CChesstion.JoueurPanel.EnabledButtons(false);
            CChesstion.MsMenu.EnableUpdateInscrits = false;


            CChesstion.ActionPanel.EnableAll(false);
            CChesstion.ActionPanel.SetProgressed(-1, false);
            CChesstion.ActionPanel.EnableFermerInscriptions = true;
            CChesstion.ActionPanel.SetProgressed(1);

            CChesstion.StatusPanel.ActivateButton(StatusPanel.RIGHT_BUTTON, "Compter les inscrits",
                new Action(CompterInscrits));
            CChesstion.StatusPanel.ActivateButton(StatusPanel.LEFT_BUTTON, "Copier le lien",
                () =>
                {
                    Clipboard.SetText(Url);
                    CustomQuickDialog q = new CustomQuickDialog("Lien copié !",
                        Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogType.Info, CChesstion.StatusPanel,
                        Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition
                            .CenterBelowParent);
                    q.DisplayDelay = 2000;
                    q.Show();
                });
        }

        /// <summary>
        /// Compte les inscrits au tournoi.
        /// </summary>
        private void CompterInscrits()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted += (object ss, RunWorkerCompletedEventArgs eee) =>
            {
                string sss = (string) eee.Result;
                if (!sss.Equals(string.Empty))
                {
                    CChesstion.StatusPanel.Tip = "Erreur : " + sss;
                    CChesstion.StatusPanel.ActivateButton(StatusPanel.RIGHT_BUTTON, "Compter les inscrits",
                        new Action(CompterInscrits));
                    return;
                }

                BackgroundWorker = new BackgroundWorker();
                BackgroundWorker.WorkerReportsProgress = true;
                BackgroundWorker.WorkerSupportsCancellation = true;
                BackgroundWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
                {
                    CChesstion.StatusPanel.Tip = "Comptage : " + e.ProgressPercentage + " %";
                };
                BackgroundWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    CChesstion.StatusPanel.ActivateButton(StatusPanel.RIGHT_BUTTON, "Compter les inscrits",
                        new Action(CompterInscrits));
                    CChesstion.StatusPanel.Tip = "Nombre d'inscrits à ce jour : " + e.Result;
                };
                BackgroundWorker.DoWork += BackgroundWorker_CompterInscrits;
                BackgroundWorker.RunWorkerAsync();
                CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);
            };

            bw.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                e.Result = CChesstion.NoInternetIssues();
            };

            bw.RunWorkerAsync();

            CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);
            CChesstion.StatusPanel.Tip = "Vérification de la connexion Internet...";
        }

        /// <summary>
        /// Thread dédié au comptage d'inscrits.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_CompterInscrits(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker.ReportProgress(0);
            try
            {
                FTPAdapter.DownloadFile(CChesstion.BasePath + "/tmp/tmp.json",
                    @"json/Inscrits/" + CChesstion.TournoiSelectionne.Ref + ".json");
            }
            catch
            {
                e.Result = 0;
                BackgroundWorker.ReportProgress(100);
                return;
            }

            BackgroundWorker.ReportProgress(50);
            if (BackgroundWorker.CancellationPending)
                return;
            try
            {
                JArray jArr =
                    (JArray) JsonConvert.DeserializeObject(File.ReadAllText(CChesstion.BasePath + "/tmp/tmp.json"));
                e.Result = jArr.Count;
            }
            catch
            {
                e.Result = 0;
            }

            BackgroundWorker.ReportProgress(100);
        }
    }
}
