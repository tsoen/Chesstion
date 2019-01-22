using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.ImportExport;
using ChessTion.Test;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ChessTion.Utilitaires
{
    class BaseDeDonnees
    {

        /**
         * Attributs
         * */

        public string Nom { get; set; }
        public string UrlSource { get; set; }

        public static event EventHandler MiseAJour;

        static OleDbConnection Connec;

        /**
         * Constructeurs
         * */

        public BaseDeDonnees() { }

        public BaseDeDonnees(string nom, string urlSource)
        {
            this.Nom = nom;
            this.UrlSource = urlSource;
            MiseAJour += MajElo;
        }

        /**
         * Méthodes
         * */

        public void MiseAJourAuto()
        {
            try
            {

                JObject dateJo = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"))["dateMaj"].Value<JObject>();
                DateTime dateMaj = JsonConvert.DeserializeObject<DateTime>(dateJo.ToString());

                Debug.WriteLine("Ancienne date : " + dateJo + "(" + dateMaj.ToShortDateString() + ")");

                if (!dateMaj.Month.Equals(DateTime.Now.Month))
                {
                    WebClient webClient = new WebClient();
                    //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFinished());
                    webClient.DownloadFile(new Uri("http://www.echecs.asso.fr/Papi/PapiData.zip"), CChesstion.BasePath + @"\Ressources\PapiData.zip");

                    if (File.Exists(CChesstion.BasePath + @"\Ressources\DATA.mdb"))
                    {
                        File.Delete(CChesstion.BasePath + @"\Ressources\DATA.mdb");
                    }

                    System.IO.Compression.ZipFile.ExtractToDirectory(CChesstion.BasePath + @"\Ressources\PapiData.zip", CChesstion.BasePath + @"\Ressources\");

                    if (File.Exists(CChesstion.BasePath + @"\Ressources\PapiData.zip"))
                    {
                        File.Delete(CChesstion.BasePath + @"\Ressources\PapiData.zip");
                    }

                    dateJo = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"));
                    dateJo["dateMaj"] = JsonConvert.SerializeObject(DateTime.Today);
                    string json = dateJo.ToString();
                    File.WriteAllText(CChesstion.SettingsFolder + "/settings.json", json);

                    Debug.WriteLine("Nouvelle date : " + dateJo + "(" + DateTime.Today.ToShortDateString() + ")");

                    
                    MiseAJour?.Invoke(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void MajElo(object sender, EventArgs e)
        {
            MessageBox.Show("Mise à jour des elos");

            Connec = new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath + @"\Ressources\DATA.mdb");
            Connec.Open();
            foreach (Joueur t in GJoueur.ListerJoueurs())
            {

                string rqt = "select * from JOUEUR WHERE Ref = " + t.Ref;
                OleDbDataAdapter da = new OleDbDataAdapter(rqt, Connec);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    t.Elo = int.Parse(row["Elo"].ToString());
                }
                else
                {
                    //La Base ne contient pas de joueur avec cette Ref
                }
            }
        }

        public static void ForcerMiseAJourAsync(DownloadProgressChangedEventHandler progressChanged, bool force = true)
        {
            JObject dateJo = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"));
            DateTime dateMaj = JsonConvert.DeserializeObject<DateTime>(dateJo["dateMaj"].ToString());

            Debug.WriteLine("Ancienne date : " + dateJo + "(" + dateMaj.ToShortDateString() + ")");

            if (!dateMaj.Month.Equals(DateTime.Today.Month) || force)
            {
                string path = CChesstion.SettingsFolder + "/settings.json";
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += progressChanged;
                webClient.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                {
                    if (File.Exists(CChesstion.BasePath + @"\Ressources\DATA.mdb"))
                    {
                        File.Delete(CChesstion.BasePath + @"\Ressources\DATA.mdb");
                    }

                    System.IO.Compression.ZipFile.ExtractToDirectory(CChesstion.BasePath + @"\Ressources\PapiData.zip", CChesstion.BasePath + @"\Ressources\");

                    while (IsFileLocked(new FileInfo(CChesstion.BasePath + @"\Ressources\PapiData.zip")))
                        Thread.Sleep(100);

                    if (File.Exists(CChesstion.BasePath + @"\Ressources\PapiData.zip"))
                    {
                        File.Delete(CChesstion.BasePath + @"\Ressources\PapiData.zip");
                    }


                    //inscrit le mois de la dernière mise à jour des Elos dans un fichier
                    //tableau json en prévention de stockage de plusieurs informations
                    dateJo = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"));
                    dateJo["dateMaj"] = JsonConvert.SerializeObject(DateTime.Today);
                    string json = dateJo.ToString();
                    File.WriteAllText(CChesstion.SettingsFolder + "/settings.json", json);

                    Debug.WriteLine("Nouvelle date : " + dateJo + "(" + DateTime.Today.ToShortDateString() + ")");

                    Action action = () =>
                    {
                        MiseAJour?.Invoke(new BaseDeDonnees(), new EventArgs());
                    };

                    FTPAdapter.UploadFileAsync(CChesstion.BasePath + "/Ressources/DATA.mdb", "DATA.MDB", action,
                            new Action<ulong>((ulong percent) =>
                            {
                                CChesstion.StatusPanel.Tip = "Upload sur le site : " + percent + " %";
                            }));

                    /*
                    FTPAdapter.UploadFileAsync(CChesstion.BasePath + "/Ressources/DATA.mdb", "DATA.MDB",
                        new UploadProgressChangedEventHandler((object s, UploadProgressChangedEventArgs args) =>
                        {
                            CChesstion.StatusPanel.Tip = "Upload sur le site : " + args.ProgressPercentage + " %";
                        }),
                    new UploadFileCompletedEventHandler((object s, UploadFileCompletedEventArgs args) =>
                    {
                        MiseAJour?.Invoke(new BaseDeDonnees(), new EventArgs());
                    }));
                    */
                };
                webClient.DownloadFileAsync(new Uri("http://www.echecs.asso.fr/Papi/PapiData.zip"), CChesstion.BasePath + @"\Ressources\PapiData.zip");
            }
            else
                MiseAJour?.Invoke(new BaseDeDonnees(), new EventArgs());

        }

        /// <summary>
        /// Renvoie vrai si un fichier est bloqué en écriture et lecture (en cours de création).
        /// </summary>
        /// <param name="file">Fichier à vérifier.</param>
        /// <returns></returns>
        private static bool IsFileLocked(FileInfo file)
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
