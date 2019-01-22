using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur.CTournoi;
using ChessTion.Test;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe gérant la sauvegarde et le chargement d'un tournoi en local.
    /// </summary>
    static class Save
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
        /// Dossier de sauvegarde.
        /// </summary>
        private static string SaveFolder { get { return CChesstion.SaveFolder; } }

        /// <summary>
        /// Liste des chemins d'accès de toutes les sauvegardes.
        /// </summary>
        private static List<string> SavesFolders { get { return GetSavesFolders(); } }

        /// <summary>
        /// Liste toutes les sauvegardes par leur nom.
        /// </summary>
        /// <returns></returns>
        public static List<string> ListSavesByName()
        {
            if (!Directory.Exists(SaveFolder))
                throw new Exception("Le répertoire Saves n'existe pas.");

            List<string> names = new List<string>();

            foreach (string save in SavesFolders)
            {
                string name = (string)JObject.Parse(File.ReadAllText(save + "/tournoi.json"))["Nom"];
                names.Add(name);
            }

            return names;
        }

        /// <summary>
        /// Liste toutes les sauvegardes par leur référence.
        /// </summary>
        /// <returns></returns>
        public static List<int> ListSavesByRef()
        {
            if (!Directory.Exists(SaveFolder))
                throw new Exception("Le répertoire Saves n'existe pas.");

            List<int> references = new List<int>();

            foreach (string save in SavesFolders)
                references.Add((int)JObject.Parse(File.ReadAllText(save + "/tournoi.json"))["Ref"]);

            return references;
        }


        /// <summary>
        /// Sauvegarde le tournoi ouvert.
        /// </summary>
        public static void PerformSave()
        {
            string dirPath = SaveFolder + "/" + CChesstion.TournoiSelectionne.Ref;

            if (Directory.Exists(dirPath))
                Directory.Delete(dirPath, true);

            Directory.CreateDirectory(dirPath);
            File.WriteAllText(dirPath + "/tournoi.json", ExportTournoi.ToJson(CChesstion.TournoiSelectionne.Ref));
            File.WriteAllText(dirPath + "/joueurs.json", ExportJoueurs.ToJson(GJoueur.ListerJoueurs()));
            File.WriteAllText(dirPath + "/clubs.json", ExportClubs.ToJson(GClub.ListerClubs()));

            CChesstion.MsMenu.UpdateSavesList();
            CustomQuickDialog d = new CustomQuickDialog("Le tournoi a été enregistré !",
                Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogType.Success, CChesstion.CentrePanel,
                Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOverParent);
            d.DisplayDelay = 5000;
            d.Show();
        }

        /// <summary>
        /// Charge le tournoi.
        /// </summary>
        /// <param name="reference">Référence du tournoi à charger.</param>
        public static void LoadSave(int reference)
        {
            if (!ListSavesByRef().Contains(reference))
                throw new ArgumentException("Cette sauvegarde n'existe pas !");

            CChesstion.DeleteAll();

            ImportTournoi.FromJson(SaveFolder + "/" + reference + "/tournoi.json");
            ImportClub.FromJson(SaveFolder + "/" + reference + "/clubs.json");
            ImportJoueurs.FromJson(CChesstion.TournoiSelectionne.Ref, SaveFolder + "/" + reference + "/joueurs.json");

            CChesstion.SelectionnerOpen(GOpen.TsLesOpens.First().Ref);

            CChesstion.FillComboBoxes();
            CChesstion.UpdateTournoiEtat(true);

            CChesstion.MsMenu.UpdateSavesList();
            CustomQuickDialog d = new CustomQuickDialog("Le tournoi a été chargé !",
                Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogType.Success, CChesstion.CentrePanel,
                Vue.CustomControls.GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOverParent);
            d.DisplayDelay = 5000;
            d.Show();

        }

        /// <summary>
        /// Retourne la liste des chemins d'accès de toutes les sauvegardes.
        /// </summary>
        /// <returns></returns>
        private static List<string> GetSavesFolders()
        {
            if (!Directory.Exists(SaveFolder))
                throw new Exception("Le répertoire Saves n'existe pas.");

            List<string> folders = new List<string>();

            foreach (string s in Directory.GetDirectories(SaveFolder))
            {
                //Debug.WriteLine("In SaveFolder : " + s);

                int i = 0;
                foreach (string f in Directory.GetFiles(s))
                {
                    if (Path.GetFileName(f) == null)
                        continue;

                    if (Path.GetFileName(f).Equals("tournoi.json"))
                        i++;
                    else if (Path.GetFileName(f).Equals("joueurs.json"))
                        i++;
                    else if (Path.GetFileName(f).Equals("clubs.json"))
                        i++;
                }

                if (i == 3)
                    folders.Add(s);
            }

            return folders;
        }
    }
}
