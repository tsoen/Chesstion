using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.ImportExport;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;
using Clipboard = System.Windows.Clipboard;

namespace ChessTion.Controleur.Etats
{
    class TournoiTermine : Etat
    {

        /// <summary>
        /// Chemin d'accès du fichier local.
        /// </summary>
        private string FilePath { get; set; } = "";

        public override int Etape { get; protected set; } = 5;
        public override string Name { get; protected set; } = "Fin du tournoi";
        public override void Transition(bool loadInterface = true)
        {
            BackgroundWorker = new BackgroundWorker();

            // S'occupe d'exporter en Json et de l'écrire dans un fichier
            BackgroundWorker.DoWork += DoWork_ExportJsonToFile;
            BackgroundWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                CChesstion.StatusPanel.Tip = "Export vers fichier .json : " + e.ProgressPercentage + " %";
            };
            BackgroundWorker.RunWorkerCompleted += DoWork_UploadFile;
            BackgroundWorker.WorkerReportsProgress = true;

            BackgroundWorker.RunWorkerAsync(loadInterface);

            CChesstion.EnableAll(false);
            Directory.Delete(CChesstion.SaveFolder + "/" + CChesstion.TournoiSelectionne.Ref, true);

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
            e.Result = e.Argument;
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
                        CChesstion.StatusPanel.Tip = "Upload sur le site : " + (percent * 60 / 100 + 40) + " %";
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

        public override void LoadInterface()
        {
            CChesstion.EnableAll(false);
            CChesstion.StatusPanel.Enabled = true;
            CChesstion.StatusPanel.Title = FullName;
            CChesstion.StatusPanel.Message =
                "Le tournoi est terminé !\nTous les fichiers y étant liés ont été supprimés ; cependant, les résultats sont encore disponibles en ligne.\n\nÀ bientôt !";
            CChesstion.ShowStatusPanel(true, false);
            CChesstion.StatusPanel.ActivateButton(StatusPanel.LEFT_BUTTON, "Nouveau tournoi", () =>
            {
                CChesstion.TournoiSelectionne = GTournoi.CreerTournoi("Nouveau tournoi", DateTime.Today, DateTime.Today,
                GLieu.ListerLieux().First().Ref);


                CChesstion.UpdateTournoiEtat();
                CChesstion.SelectionnerOpen(GOpen.ListerOpens().First().Ref);
            });
            CChesstion.StatusPanel.ActivateButton(StatusPanel.RIGHT_BUTTON, "Fermer Chesstion", Application.Exit);
        }
    }
}
