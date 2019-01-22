using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.ImportExport;
using ChessTion.Modele.MTournoi;
using ChessTion.Test;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.Etats
{
    /// <summary>
    /// Classe gérant l'étape durant laquelle le tournoi est en cours de déroulement.
    /// </summary>
    class DurantTournoi : Etat
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
        /// Liste d'extensions de fichiers surveillées par Chesstion.
        /// </summary>
        private static readonly List<string> filenameEndings = new List<string>() {".html"};

        /// <summary>
        /// Liste des chemin d'accès des fichiers générés par open.
        /// </summary>
        private Dictionary<Open, string> opensPath;

        /// <summary>
        /// Compteur.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Liste des chemins d'accès des fichiers générés par Papi.
        /// </summary>
        private List<string> PapiChildren = new List<string>();

        /// <summary>
        /// Numéro de l'état.
        /// </summary>
        public override int Etape { get; protected set; } = 4;

        /// <summary>
        /// Nom de l'état.
        /// </summary>
        public override string Name { get; protected set; } = "Tournoi";

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

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += DoWork_SupprimerJoueurs;
            BackgroundWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                CChesstion.StatusPanel.Tip = "(1/3) Suppression des joueurs non confirmés : " + e.ProgressPercentage +
                                             " %";
            };
            BackgroundWorker.RunWorkerCompleted += Completed_SupprimerJoueurs;
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.RunWorkerAsync();

            CChesstion.StatusPanel.Title = "Exportation des joueurs confirmés...";
            CChesstion.StatusPanel.Message = "Chesstion est en train d'exporter l'ensemble des joueurs dont la participation a été validée. Vous pourrez ensuite importer le fichier généré dans Papi.\nVeuillez patienter...";
            CChesstion.ShowStatusPanel(true, false);       
        }

        /// <summary>
        /// Supprime les joueurs non confirmés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_SupprimerJoueurs(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker.ReportProgress(0);
            int i = 0, count = CChesstion.TournoiSelectionne.TsLesJoueurs.Count;
            foreach (Joueur j in CChesstion.TournoiSelectionne.TsLesJoueurs)
            {
                if (!j.Confirme)
                    CChesstion.SupprimerJoueur(j.Ref, false, false);
                BackgroundWorker.ReportProgress((100*i++)/count);
            }
            BackgroundWorker.ReportProgress(100);
        }

        /// <summary>
        /// Demande le chemin d'accès de Papi à l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Completed_SupprimerJoueurs(object sender, RunWorkerCompletedEventArgs e)
        {
            // Directory select
            DialogResult dialogResult = DialogResult.Cancel;
            CChesstion.JoueurPanel.Reset();

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = CChesstion.PapiFolder;
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Sélectionnez le dossier dans lequel Papi est installé (dossier contenant Papi.exe)";

            while (dialogResult != DialogResult.OK || !Directory.Exists(fbd.SelectedPath) || !ContainsPapiExe(Directory.GetFiles(fbd.SelectedPath)))
                dialogResult = fbd.ShowDialog();

            JObject o = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"));
            o["papiPath"] = fbd.SelectedPath;
            File.WriteAllText(CChesstion.SettingsFolder + "/settings.json", o.ToString());

            // Do work
            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += DoWork_CreateMdbFiles;
            BackgroundWorker.ProgressChanged += (object senderr, ProgressChangedEventArgs ee) =>
            {
                CChesstion.StatusPanel.Tip = "(2/3) Génération des fichiers mdb : " + ee.ProgressPercentage +
                                             " %";
            };
            BackgroundWorker.RunWorkerCompleted += Completed_CreateMdbFiles;
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.RunWorkerAsync(fbd.SelectedPath);

            CChesstion.CentrePanel.Load();
        }

        private bool ContainsPapiExe(string[] files)
        {
            foreach (string g in files)
                if (g.Contains("Papi.exe") || g.Contains("papi.exe"))
                    return true;
            return false;
        }

        /// <summary>
        /// Crée les fichiers MDB associés aux opens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_CreateMdbFiles(object sender, DoWorkEventArgs e)
        {
            Dictionary<Open, string> opens = new Dictionary<Open, string>();

            int i = 0, count = GOpen.ListerOpens().Count;

            foreach (Open open in GOpen.ListerOpens())
            {
                if (open.TsLesJoueurs.Count == 0) continue;

                string name = open.Ref + "_" + open.Nom + "_DATA";
                foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    name = name.Replace(c, '-');

                while (File.Exists(e.Argument + @"\" + name + ".MDB"))
                    name += "_";

                string path = e.Argument + @"\" + name + ".MDB";
                File.Copy(CChesstion.BasePath + @"\Ressources\DATA_empty.MDB", path);

                opens.Add(open, path);

                BackgroundWorker.ReportProgress((100*i++)/count);
            }
            BackgroundWorker.ReportProgress(100);
            e.Result = opens;
        }

        /// <summary>
        /// Enregistre les chemins d'accès à ces fichiers créés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Completed_CreateMdbFiles(object sender, RunWorkerCompletedEventArgs e)
        {
            opensPath = (Dictionary<Open, string>) e.Result;
            Completed_FillMdbFiles(null, null);
        }

        /// <summary>
        /// Exporte les données dans les fichiers MDB créés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_FillMdbFiles(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker.ReportProgress(0);

            KeyValuePair<Open, string> open = (KeyValuePair<Open, string>) e.Argument;

            ImportExport.ExportJoueurs.ToDataMdb(open.Key.TsLesJoueurs, GClub.ListerClubs(),
                open.Value, BackgroundWorker);

            BackgroundWorker.ReportProgress(100);
        }
        private void Completed_FillMdbFiles(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e != null) counter++;

            if (counter == opensPath.Count)
            {
                Save.PerformSave();
                LoadInterface();
                return;
            }

            KeyValuePair<Open, string> open = opensPath.ElementAt(counter);

            string message = "(3/3) Remplissage des fichiers mdb " + open.Key.TitreFormatte() + " (" + (counter+1) + "/" + opensPath.Count + ") : ";

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.ProgressChanged += (object senderr, ProgressChangedEventArgs ee) =>
            {
                CChesstion.StatusPanel.Tip = message + ee.ProgressPercentage + " %";
            };
            BackgroundWorker.RunWorkerCompleted += Completed_FillMdbFiles;
            BackgroundWorker.DoWork += DoWork_FillMdbFiles;
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.RunWorkerAsync(open);
        }

        private void UploadInscrits()
        {
            


        }

        /// <summary>
        /// Charge l'interface pour coller à l'état.
        /// </summary>
        public override void LoadInterface()
        {
            CChesstion.CentrePanel.AllowAdd = false;
            CChesstion.CentrePanel.AllowDelete = false;
            CChesstion.StatusPanel.Title = FullName;
            CChesstion.ActionPanel.EnableAll(false);
            CChesstion.ActionPanel.EnableTerminerTournoi = true;
            CChesstion.ActionPanel.SetProgressed(-1, true);
            CChesstion.MsMenu.EnableUpdateInscrits = true;
            CChesstion.JoueurPanel.EnabledButtons(true);

            if (PapiChildren.Count == 0)
            {
                CChesstion.EnableAll(true, true);
                CChesstion.OpensPanel.ReadOnly = true;
                CChesstion.RepasPanel.ReadOnly = true;
                CChesstion.OpenPanel.ReadOnly = true;


                CChesstion.StatusPanel.Message = "Il est temps de débuter le tournoi !\n";

                if (opensPath != null && opensPath.Count > 0)
                {
                    int count = opensPath.Count;
                    string message;
                    if (count == 1)
                        message = count + " fichier a été généré à cet emplacement :\n\n" + opensPath.First().Value +
                                  "\n\nUtilisez ce fichier ";
                    else
                    {
                        message = count + " fichiers ont été générés à ces emplacements :\n\n";
                        foreach (KeyValuePair<Open, string> open in opensPath)
                        {
                            message += open.Value + "\n";
                        }
                        message += "\nUtilisez ces fichiers ";
                    }

                    CChesstion.StatusPanel.Message += message;
                }
                else
                {
                    CChesstion.StatusPanel.Message = "Utilisez les fichiers générés dans " + CChesstion.PapiFolder + " ";
                }

                CChesstion.StatusPanel.Message += "pour importer les joueurs dans Papi.";
                CChesstion.StatusPanel.Tip =
                    "Vous serrez prévenus lorsque Papi génèrera des fichiers, vous pourrez ainsi les envoyer d'un clic aux joueurs ou les publier sur le site !";
                CChesstion.ShowStatusPanel(true, true);
                WatchForNewFiles();
            }
            else
            {
                CChesstion.StatusPanel.Message = "Des fichiers ont été générés !\n\n";

                foreach (string s in PapiChildren)
                    CChesstion.StatusPanel.Message += "- " + GetFileTitle(s) + " (" + Path.GetFileName(s) + ")\n";

                if (PapiChildren.Count > 0)
                {
                    CChesstion.StatusPanel.ActivateButton(StatusPanel.LEFT_BUTTON, "Publier ces fichiers", () =>
                    {
                        CChesstion.StatusPanel.Enabled = false;
                        foreach (string s in PapiChildren)
                        {
                            string title = GetFileTitle(s);
                            if (title.Equals(string.Empty))
                                title = Path.GetFileNameWithoutExtension(s);

                            string filenameWithoutPath = Path.GetFileName(title);

                            title = new string(title.Where(m => !System.IO.Path.GetInvalidFileNameChars().Contains(m)).ToArray<char>());

                            FTPAdapter.UploadFileAsync(s, "html/" + CChesstion.TournoiSelectionne.Ref + "/" + filenameWithoutPath + ".html");
                        }
                        CChesstion.StatusPanel.Enabled = true;
                    CChesstion.StatusPanel.DeactivateButton(StatusPanel.LEFT_BUTTON);
                    });

                    CChesstion.StatusPanel.ActivateButton(StatusPanel.MIDDLE_BUTTON, "Envoyer par mail", () =>
                    {
                        CChesstion.StatusPanel.Enabled = false;
                        SendMessages(PapiChildren);
                        CChesstion.StatusPanel.Enabled = true;
                        CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);
                    });

                    CChesstion.StatusPanel.ActivateButton(StatusPanel.RIGHT_BUTTON, "Supprimer ces fichiers", () =>
                    {
                        CChesstion.StatusPanel.Enabled = false;
                        foreach (string s in PapiChildren)
                        {
                            File.Delete(s);
                        }
                        PapiChildren.Clear();
                        LoadInterface();
                        CChesstion.StatusPanel.Enabled = true;
                        CChesstion.StatusPanel.DeactivateButton(StatusPanel.LEFT_BUTTON);
                        CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);
                        CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);
                    });
                }
                else
                {
                    CChesstion.StatusPanel.DeactivateButton(StatusPanel.LEFT_BUTTON);
                    CChesstion.StatusPanel.DeactivateButton(StatusPanel.MIDDLE_BUTTON);
                    CChesstion.StatusPanel.DeactivateButton(StatusPanel.RIGHT_BUTTON);
                }
            }
        }

        /// <summary>
        /// Surveille le dossier et trigger <see cref="FileSystemWatcher_Created(object, FileSystemEventArgs)"/> pour chaque fichier généré.
        /// </summary>
        public void WatchForNewFiles()
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = CChesstion.PapiFolder;
            fileSystemWatcher.Filter = "*.html";
            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName;
            fileSystemWatcher.SynchronizingObject = CChesstion.StatusPanel;

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Created;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Enregistre et affiche le nom du fichier généré à l'écran.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            bool validName = false;
            foreach (string s in filenameEndings)
                if (e.Name.EndsWith(s))
                {
                    validName = true;
                    break;
                }

            if (!validName || PapiChildren.Contains(e.FullPath))
                return;
            while (IsFileLocked(new FileInfo(e.FullPath)))
            {
                Thread.Sleep(100);
            }
            GetFileTitle(e.FullPath);
            PapiChildren.Add(e.FullPath);
            LoadInterface();
            
        }

        /// <summary>
        /// Retourne le nom du fichier (contenu dans le code html).
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetFileTitle(string path)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(path);

                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@class='" + "papi_titre" + "']");

                if (nodes == null || nodes.Count == 0)
                    throw new ArgumentException("File does not contain any.");

                HtmlNode node = nodes.First();

                while (node.HasChildNodes)
                {
                    if (node.FirstChild.NodeType == HtmlNodeType.Text)
                        break;
                    node = node.FirstChild;
                }

                string result = string.Empty;
                string delim = string.Empty;

                if (node.InnerHtml.Contains("<br>"))
                    delim = "<br>";
                else if (node.InnerHtml.Contains("<br/>"))
                    delim = "<br/>";
                else if (node.InnerHtml.Contains("<br />"))
                    delim = "<br />";

                try
                {
                    if (delim.Equals(string.Empty))
                        result = node.InnerHtml;
                    else
                        result = node.InnerHtml.Substring(node.InnerHtml.LastIndexOf(delim) + delim.Length);
                }
                catch
                {
                    result = string.Empty;
                }

                return Regex.Replace(result, "<.*?>", String.Empty).Trim();
                ;

            }
            catch
            {
                return path; // Si le doc ne contient pas de titre
            }
        }

        /// <summary>
        /// Envoie les fichiers par email.
        /// </summary>
        /// <param name="files"></param>
        private void SendMessages(List<string> files)
        {
            string subject = "Documents liés au tournoi " + CChesstion.TournoiSelectionne.Nom;
            string body;

            foreach (Joueur j in CChesstion.TournoiSelectionne.TsLesJoueurs)
                if (j.Subscribe && j.Email.Length > 0)
                {
                    body = "Bonjour, " + j.Prenom + " " + j.Nom + ",\n\n";
                    body += "Vous avez souhaité recevoir les documents liés au tournoi " +
                            CChesstion.TournoiSelectionne.Nom +
                            " auquel vous participez. Vous trouverez donc ci-joint : \n";

                    foreach (string f in files)
                        body += "- " + GetFileTitle(f) + "\n";

                    body += "\nCordialement.";

                    Mail.Send(new MailAddress(j.Email), subject, body, files);
                }
                
        }

        /// <summary>
        /// Renvoie vrai si un fichier est bloqué en écriture et lecture (en cours de création).
        /// </summary>
        /// <param name="file">Fichier à vérifier.</param>
        /// <returns></returns>
        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
