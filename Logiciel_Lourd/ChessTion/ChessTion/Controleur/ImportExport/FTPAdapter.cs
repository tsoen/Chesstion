using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using ChessTion.Test;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using ChessTion.Utilitaires;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe servant d'intermédiaire avec <see cref="SFTP"/>.
    /// </summary>
    static class FTPAdapter
    {
        /// <summary>
        /// URL host du serveur FTP.
        /// </summary>
        //public static string URL { get; set; } = @"ftp://91.121.83.225:21";
        public static string URL { get; set; } = @"cybele.ecole.ensicaen.fr";

        /// <summary>
        /// Nom d'utilisateur du serveur FTP.
        /// </summary>
        public static string Username { get; set; } = @"soen";

        /// <summary>
        /// Mot de passe du serveur FTP.
        /// </summary>
        public static string Password { get; set; } = @"T1m0s03n-1";

        /// <summary>
        /// Retourne une nouvelle instance de FTP avec les credentials donnés en attributs.
        /// </summary>
        public static SFTP Ftp
        {
            get { return new SFTP(URL, Username, Password); }
        }

        /// <summary>
        /// Retourne vrai si o arrive à se connecter au FTP.
        /// </summary>
        /// <returns></returns>
        public static bool IsFtpConnectionOK()
        {
            try
            {
                FTPAdapter.Ls("/");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne vrai si on arrive à se connecter à Internet.
        /// </summary>
        /// <returns></returns>
        public static bool IsInternetConnectionOK()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Upload un fichier sur le serveur FTP.
        /// </summary>
        /// <param name="localFilePath">Chemin d'accès local du fichier.</param>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        public static void UploadFile(string localFilePath, string remoteFilePath)
        {
            if (!File.Exists(localFilePath))
                throw new ArgumentException("Le fichier source n'existe pas !");

            Ftp.UploadFile(localFilePath, FormatRemotePath(remoteFilePath));
        }

        /// <summary>
        /// Upload un fichier sur le serveur FTP de manière asynchrone.
        /// </summary>
        /// <param name="localFilePath">Chemin d'accès local du fichier.</param>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        /// <param name="progressChanged">Fonction à exécuter lorsque le progress a changé.</param>
        /// <param name="completedEvent">Fonction à exécuter lorsque l'upload est fini</param>
        public static SFTP UploadFileAsync(string localFilePath, string remoteFilePath,
             Action uploadCompletedEvent = null, Action<ulong> uploadOnProgressEvent = null)
        {
            if (!File.Exists(localFilePath))
                throw new ArgumentException("Le fichier source n'existe pas !");

            SFTP ftp = Ftp;

            ftp.UploadFileAsync(localFilePath, remoteFilePath, uploadCompletedEvent, uploadOnProgressEvent);

            return ftp;
        }

        public static void CancelUploadAsynch(SFTP ftp)
        {
            ftp.CancelUploadAsync();
        }

        /// <summary>
        /// Télécharge un fichier depuis le serveur FTP.
        /// </summary>
        /// <param name="localFilePath">Chemin d'accès local du fichier.</param>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        public static void DownloadFile(string localFilePath, string remoteFilePath)
        {
            Ftp.DownloadFile(localFilePath, FormatRemotePath(remoteFilePath));
        }

        /// <summary>
        /// Supprime un fichier du serveur FTP.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        public static void DeleteFile(string remoteFilePath)
        {
            Ftp.DeleteFile(FormatRemotePath(remoteFilePath));
        }

        /// <summary>
        /// Renomme un fichier du serveur FTP.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        /// <param name="newFileName">Nouveau nom du fichier.</param>
        public static void RenameFile(string remoteFilePath, string newFileName)
        {
            Ftp.RenameFile(FormatRemotePath(remoteFilePath), newFileName);
        }

        /// <summary>
        /// Crée un dossier sur le serveur FTP.
        /// </summary>
        /// <param name="remoteDirectoryPath">Chemin d'accès distant du dossier.</param>
        public static void CreateDirectory(string remoteDirectoryPath)
        {
            Ftp.CreateDirectory(FormatRemotePath(remoteDirectoryPath));
        }

        /// <summary>
        /// Supprime un dossier du serveur FTP.
        /// </summary>
        /// <param name="remoteDirectoryPath">Chemin d'accès distant du dossier.</param>
        public static void DeleteDirectory(string remoteDirectoryPath)
        {
            Ftp.DeleteDirectory(FormatRemotePath(remoteDirectoryPath));
        }

        /// <summary>
        /// Liste l'ensemble des fichiers d'un dossier.
        /// </summary>
        /// <param name="remoteDirectoryPath">Chemin d'accès distant du dossier.</param>
        /// <returns></returns>
        public static List<string> Ls(string remoteDirectoryPath)
        {
            return Ftp.Ls(FormatRemotePath(remoteDirectoryPath));
        }

        /// <summary>
        /// Liste de manière détaillée l'ensemble des fichiers d'un dossier.
        /// </summary>
        /// <param name="remoteDirectoryPath">Chemin d'accès distant du dossier.</param>
        /// <returns></returns>
        public static List<string> Lsl(string remoteDirectoryPath)
        {
            return Ftp.Lsl(FormatRemotePath(remoteDirectoryPath));
        }

        /// <summary>
        /// Retourne la date de dernière modification d'un fichier.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        /// <returns></returns>
        public static DateTime GetFileLastModifiedDatetime(string remoteFilePath)
        {
            return Ftp.GetFileLastModifiedDatetime(FormatRemotePath(remoteFilePath));
        }

        /// <summary>
        /// Retourne la taille d'un fichier.
        /// </summary>
        /// <param name="remoteFilePath">Chemin d'accès distant du fichier.</param>
        /// <returns></returns>
        public static long GetFileSize(string remoteFilePath)
        {
            return Ftp.GetFileSize(FormatRemotePath(remoteFilePath));
        }

        private static string FormatRemotePath(string destinationPath)
        {
            destinationPath = destinationPath.Trim();
            if (destinationPath.ToCharArray()[0] == '/')
                destinationPath = destinationPath.Substring(1);
            return destinationPath;
        }
    }
}
