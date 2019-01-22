using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ChessTion.Utilitaires
{
    /// <summary>
    /// Classe gérant l'accès FTP à un serveur : upload, download, gestion des répertoires, etc.
    /// </summary>
    class SFTP
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
        /// Url de base du serveur FTP.
        /// </summary>
        private string url;

        /// <summary>
        /// Nom d'utilisateur utilisé lors de l'autentification sur le serveur FTP.
        /// </summary>
        private string username;

        /// <summary>
        /// Mot de passe utilisé lors de l'autentification sur le serveur FTP.
        /// </summary>
        private string password;

        private BackgroundWorker bgWorker;

        private static string _basePath { get; set; } = @"/home/eleves/promo20/info/soen/public_html/Chesstion/";

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
        /// Constructeur
        /// </summary>
        /// <param name="url">Url de base du serveur FTP.</param>
        /// <param name="username">Nom d'utilisateur utilisé lors de l'autentification sur le serveur FTP.</param>
        /// <param name="password">Mot de passe utilisé lors de l'autentification sur le serveur FTP.</param>
        public SFTP(string url, string username, string password)
        {
            this.url = url;
            this.username = username;
            this.password = password;
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
        // Upload
        //

        /// <summary>
        /// Upload un fichier sur le serveur FTP.
        /// </summary>
        /// <param name="localFilePath">Chemin d'accès du fichier à transférer.</param>
        /// <param name="remoteFilePath">Chemin d'accès de l'emplacement où uploader le fichier.</param>
        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            using (SftpClient sftp = new SftpClient(url, username, password))
            {
                sftp.Connect();
                sftp.ChangeDirectory(url + "/" + remoteFilePath);

                using (FileStream fs = new FileStream(localFilePath, FileMode.Open))
                {
                    sftp.BufferSize = 4 * 1024;
                    sftp.UploadFile(fs, Path.GetFileName(localFilePath));
                }

            }
        }

        /// <summary>
        /// Upload un fichier sur le serveur FTP de manière asynchrone.
        /// </summary>
        /// <param name="localFilePath">Chemin d'accès du fichier à transférer.</param>
        /// <param name="remoteFilePath">Chemin d'accès de l'emplacement où uploader le fichier.</param>
        /// <param name="completedEvent">Evenement appelé lorsque l'upload est terminé.</param>
        /// <param name="progressChanged">Evènement appelé lorque que le progress a changé.</param>
        public void UploadFileAsync(string localFilePath, string remoteFilePath, Action uploadCompletedEvent = null, Action<ulong> uploadOnProgressEvent = null)
        {
            bgWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
            };

            bgWorker.DoWork += delegate
            {
                using (SftpClient sftp = new SftpClient(url, username, password))
                {
                    sftp.Connect();

                    string[] tokens = remoteFilePath.Split('/');

                    if (tokens[0] == "json")
                    {
                        sftp.ChangeDirectory(_basePath + "json/Tournois/");
                    }
                    else if (tokens[0] == "html")
                    {
                        try
                        {
                            sftp.CreateDirectory(_basePath + tokens[0] + "/" + tokens[1]);
                        }
                        catch
                        {
                            //directory exists
                        }
                    }

                    sftp.ChangeDirectory(_basePath);

                    using (FileStream stream = new FileStream(localFilePath, FileMode.Open))
                    {
                        sftp.UploadFile(stream, remoteFilePath, uploadOnProgressEvent);
                    }
                }
            };

            bgWorker.RunWorkerCompleted += delegate {
                uploadCompletedEvent?.Invoke();
            };

            bgWorker.RunWorkerAsync();

            /*
            clientAsynch = new WebClient();
            clientAsynch.Credentials = new NetworkCredential(username, password);
            clientAsynch.UploadFileCompleted += completedEvent;
            clientAsynch.UploadProgressChanged += progressChanged;
            clientAsynch.UploadFileAsync(new Uri("ftp://" + url + "/" + remoteFilePath), WebRequestMethods.Ftp.UploadFile, localFilePath);
            */
        }

        /// <summary>
        /// Annule l'upload asynchrone.
        /// </summary>
        public void CancelUploadAsync()
        {
            bgWorker.CancelAsync();
            /*
            if (clientAsynch.IsBusy)
                clientAsynch.CancelAsync();
                */
        }

        // 
        // Download
        //

        /// <summary>
        /// Télécharge un fichier depuis le serveur FTP.
        /// </summary>
        /// <param name="localFilePath">Chemin d'accès du fichier à transférer.</param>
        /// <param name="remoteFilePath">Chemin d'accès de l'emplacement où uploader le fichier.</param>
        public void DownloadFile(string localFilePath, string remoteFilePath)
        {
            using (SftpClient sftp = new SftpClient(url, username, password))
            {
                try
                {
                    sftp.Connect();
                    sftp.ChangeDirectory(_basePath);

                    Console.WriteLine(_basePath);
                    Console.WriteLine(localFilePath);
                    Console.WriteLine(remoteFilePath);


                    using (Stream fileStream = File.OpenWrite(localFilePath))
                    {
                        sftp.DownloadFile(remoteFilePath, fileStream);
                    }

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }

            /*
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(username, password);
                client.DownloadFile(url + "/" + remoteFilePath, localFilePath);
            }
            */
        }

        /// <summary>
        /// Supprime un fichier sur le serveur FTP.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès du fichier à supprimer.</param>
        public void DeleteFile(string remoteFilePath)
        {
            using (SftpClient sftp = new SftpClient(url, username, password))
            {
                try
                {
                    sftp.Connect();
                    sftp.ChangeDirectory(_basePath);

                    sftp.DeleteFile(remoteFilePath);

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }
            /*
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + remoteFilePath);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
            */
        }

        /// <summary>
        /// Renomme un fichier du serveur FTP.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès du fichier à renommer.</param>
        /// <param name="newFileName">Nouveau nom.</param>
        public void RenameFile(string remoteFilePath, string newFileName)
        {
            using (SftpClient sftp = new SftpClient(url, username, password))
            {
                try
                {
                    sftp.Connect();
                    sftp.ChangeDirectory(_basePath);

                    using (Stream fileStream = File.OpenWrite(remoteFilePath))
                    {
                        sftp.RenameFile(remoteFilePath, newFileName);
                    }

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }

            /*
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + remoteFilePath);
            request.RenameTo = newFileName;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
            */
        }

        //
        // Répertoires
        //

        /// <summary>
        /// Crée un répertoire sur le serveur FTP.
        /// </summary>
        /// <param name="remoteDirectoryPath">Chemin d'accès du nouveau répertoire (nom inclus).</param>
        public void CreateDirectory(string remoteDirectoryPath)
        {
            using (SftpClient sftp = new SftpClient(url, username, password))
            {
                try
                {
                    sftp.Connect();
                    sftp.ChangeDirectory(_basePath);

                    sftp.CreateDirectory(remoteDirectoryPath);

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }

            /*
            WebRequest request = WebRequest.Create(url + "/" + remoteDirectoryPath);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(username, password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
            */
        }

        /// <summary>
        /// Supprime un répertoire du serveur FTP.
        /// </summary>
        /// <param name="remoteDirectoryPath">Chemin d'accès du nouveau répertoire (nom inclus).</param>
        public void DeleteDirectory(string remoteDirectoryPath)
        {
            WebRequest request = WebRequest.Create(url + "/" + remoteDirectoryPath);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential(username, password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
        }

        //
        // Liste de fichier
        //

        /// <summary>
        /// Liste les fichiers du serveur FTP.
        /// </summary>
        /// <param name="remoteDirectoryPath">Dossier duquel lister les fichiers.</param>
        /// <returns>Liste des noms de fichier.</returns>
        public List<string> Ls(string remoteDirectoryPath)
        {
            using (SftpClient sftp = new SftpClient(url, username, password))
            {
                try
                {                 
                    sftp.Connect();

                    var files = sftp.ListDirectory("/");

                    foreach (var file in files)
                    {
                        //Console.WriteLine(file.Name);
                    }

                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception has been caught " + e.ToString());
                    throw;
                }
            }
            return null;
            /*
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(url + "/" + remoteDirectoryPath);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            Stream ftpStream = ftpResponse.GetResponseStream();
            StreamReader ftpReader = new StreamReader(ftpStream);
            string directoryRaw = null;
            try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); throw; }
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            if (directoryRaw == null)
                return new List<string>();
            string[] directoryList = directoryRaw.Split("|".ToCharArray());

            List<string> list = new List<string>();
            foreach (string s in directoryList)
                if (!string.IsNullOrWhiteSpace(s))
                    list.Add(s);

            return list;*/
        }

        /// <summary>
        /// Liste de manière détaillée les fichiers du serveur FTP.
        /// </summary>
        /// <param name="remoteDirectoryPath">Dossier duquel lister les fichiers.</param>
        /// <returns>La lsite des noms de fichier avec leurs détails.</returns>
        public List<string> Lsl(string remoteDirectoryPath)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(url + "/" + remoteDirectoryPath);
            ftpRequest.Credentials = new NetworkCredential(username, password);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            Stream ftpStream = ftpResponse.GetResponseStream();
            StreamReader ftpReader = new StreamReader(ftpStream);
            string directoryRaw = null;
            try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); throw; }
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            if (directoryRaw == null)
                return new List<string>();
            string[] directoryList = directoryRaw.Split("|".ToCharArray());

            List<string> list = new List<string>();
            foreach (string s in directoryList)
                if (!string.IsNullOrWhiteSpace(s))
                    list.Add(s);

            return list;
        }

        //
        // Informations sur les fichiers
        //

        /// <summary>
        /// Retourne la date de dernière modificiation d'un fichier.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès du fichier (nom inclus).</param>
        /// <returns>Date de dernière modificiation d'un fichier.</returns>
        public DateTime GetFileLastModifiedDatetime(string remoteFilePath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + remoteFilePath);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            DateTime dt = response.LastModified;
            response.Close();
            return dt;
        }

        /// <summary>
        /// Retourne la taille d'un fichier.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès du fichier (nom inclus).</param>
        /// <returns>Taille du fichier.</returns>
        public long GetFileSize(string remoteFilePath)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + url + " / " + remoteFilePath));
            request.Proxy = null;
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            long r = response.ContentLength;
            response.Close();
            return r;
        }
    }
}
