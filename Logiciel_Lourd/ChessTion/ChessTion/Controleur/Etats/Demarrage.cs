using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.ImportExport;
using ChessTion.Modele.MLieu;
using ChessTion.Modele.MTournoi;
using ChessTion.Test;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.Etats
{
    /// <summary>
    /// Classe executée au démarrage de l'application. Elle vérifie l'ensemble des fichiers néecessaires au bon fonctionnement du logiciel.
    /// </summary>
    static class Demarrage
    {
        /// <summary>
        /// Evènement appelé lorsque toutes les opérations de démarrage ont été exécutée avec succès.
        /// </summary>
        public static event EventHandler Done;


        /// <summary>
        /// Méthode principale appelée pour lancer Demarrage.
        /// </summary>
        public static void Start()
        {
            Done += Demarrage_Done;
            CreateLocalDirectories();
            LoadLieux();
            CChesstion.LoadFtpLogin();
            CChesstion.LoadPapiPath();
            Init();
            CheckFirstStart();
            CheckMissingMDB();
        }

        /// <summary>
        /// Crée un <see cref="Tournoi"/> vide.
        /// </summary>
        private static void Init()
        {
            CChesstion.TournoiSelectionne = GTournoi.CreerTournoi("Nouveau tournoi", DateTime.Today, DateTime.Today, GLieu.ListerLieux().First().Ref);
            CChesstion.UpdateTournoiEtat();
        }

        /// <summary>
        /// Charge les <see cref="Lieu"/> et <see cref="Ville"/> enregistrés.
        /// </summary>
        private static void LoadLieux()
        {
            GLieu.ChargerLieux();
        }

        /// <summary>
        /// Crée tous les répertoires dont l'application à besoin.
        /// </summary>
        private static void CreateLocalDirectories()
        {
            Directory.CreateDirectory(CChesstion.BasePath + @"\Ressources\json\Inscrits");
            Directory.CreateDirectory(CChesstion.BasePath + @"\Ressources\json\Tournois");
            Directory.CreateDirectory(CChesstion.BasePath + @"\Logs");
        }

        /// <summary>
        /// Vérifie que DATA.MDB est présent, sinon propose à l'utilisateur de la télécharger ou de l'importer.
        /// </summary>
        public static void CheckMissingMDB()
        {
            if (File.Exists(CChesstion.BasePath + @"\Ressources\DATA.MDB"))
            {
                Done?.Invoke(new object(), new EventArgs());
                return;
            }
            CChesstion.EnableAll(false, true, false);
            CChesstion.StatusPanel.Title = "DATA.MDB non trouvé !";
            CChesstion.StatusPanel.Message =
                "CChesstion ne trouve pas le fichier DATA.MDB. " +
                "Ce fichier comprend l'ensemble des joueurs et des clubs de la FFE, " +
                "et est nécessaire au bon fonctionnement de Chesstion.\n\n" +
                "Deux options s'offrent à vous : laisser Chesstion télécharger la base, " +
                "ou importer une base que vous avez déjà téléchargée.";
            CChesstion.StatusPanel.Tip = "Attention, cette opération peut prendre quelques minutes.";
            CChesstion.StatusPanel.ActivateButton(StatusPanel.LEFT_BUTTON, "Télécharger la base", () =>
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += (object ss, DoWorkEventArgs ee) =>
                {
                    ee.Result = CChesstion.NoInternetIssues();
                };
                bw.RunWorkerCompleted += (object s, RunWorkerCompletedEventArgs ee) =>
                {
                    if (!((string) ee.Result).Equals(string.Empty))
                    {
                        CChesstion.StatusPanel.Tip = (string) ee.Result;
                        CChesstion.StatusPanel.Enabled = true;
                        return;
                    }

                    CChesstion.StatusPanel.Tip = "Téléchargement : 0 %";
                    BaseDeDonnees.MiseAJour += (object sender, EventArgs e) =>
                    {
                        CChesstion.EnableAll(true);
                        Done?.Invoke(new object(), new EventArgs());
                    };
                    BaseDeDonnees.ForcerMiseAJourAsync(
                        (object sender, DownloadProgressChangedEventArgs args) =>
                        {
                            CChesstion.StatusPanel.Tip = "Téléchargement : " + args.ProgressPercentage + " %";
                        });

                };
                bw.RunWorkerAsync();
                CChesstion.EnableAll(false);
                CChesstion.StatusPanel.Tip = "Vérification de la connexion Internet...";

            });
            CChesstion.StatusPanel.ActivateButton(StatusPanel.RIGHT_BUTTON, "Importer la base", () => {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Access Files (MDB)| *.MDB";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sourceFile = ofd.FileName;
                    CChesstion.EnableAll(false);
                    CChesstion.StatusPanel.Tip = "Importation en cours...";
                    File.Copy(sourceFile, CChesstion.BasePath + @"\Ressources\DATA.MDB", true);
                    CChesstion.EnableAll(true);
                    Done?.Invoke(new object(), new EventArgs());
                }
            });
            CChesstion.ShowStatusPanel(true, false);

        }

        public static void CheckFirstStart()
        {
            // TODO
        }

        /// <summary>
        /// Désactive les boutons potentiellements créés par <see cref="CheckMissingMDB"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Demarrage_Done(object sender, EventArgs e)
        {
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.LEFT_BUTTON);
            CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);
        }
    }
}
