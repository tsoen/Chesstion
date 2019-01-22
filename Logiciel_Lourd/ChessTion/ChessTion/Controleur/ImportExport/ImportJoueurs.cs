using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using Newtonsoft.Json;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe gérant l'importation de <see cref="Joueur"/> dans le logiciel.
    /// </summary>
    static class ImportJoueurs
    {
        /// <summary>
        /// Importe des joueurs depuis un fichier json.
        /// </summary>
        /// <param name="referenceTournoi">Référence du <see cref="Tournoi"/> auquel ajouter les joueurs.</param>
        /// <param name="jsonOrPath">Chemin d'accès du fichier json, ou code json lui-même.</param>
        /// <param name="reloadPanel">Vrai pour recharger le panneau central à la fin de l'import.</param>
        /// <param name="backgroundWorker">BackgroundWorker à qui reporter des progress.</param>
        /// <returns>Nombre de joueurs importés.</returns>
        public static int FromJson(int referenceTournoi, string jsonOrPath, bool reloadPanel = false, BackgroundWorker backgroundWorker = null)
        {
            string json = jsonOrPath.EndsWith(".json", true, CultureInfo.CurrentCulture) ? File.ReadAllText(jsonOrPath) : jsonOrPath;
            
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);

            string errored = string.Empty;
            int count = dataTable.Rows.Count, current = -1;

            foreach (DataRow row in dataTable.Rows)
            {
                ++current;
                //Debug.WriteLine("Progress (" + current + "*100/" + count + ") :" + (current * 100)/count);
                backgroundWorker?.ReportProgress((current * 100) / count);
                bool clubNonTrouve = false;

                if (GClub.GetClub(int.Parse(row["ClubRef"].ToString())) == null)
                {
                    OleDbConnection Connec = new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath + @"\Ressources\DATA.MDB");
                    Connec.Open();

                    string rqt = "SELECT * FROM CLUB WHERE Ref = " + row["ClubRef"].ToString();
                    OleDbDataAdapter da = new OleDbDataAdapter(rqt, Connec);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Connec.Close();
                    if (dt.Rows.Count == 0)
                    {
                        clubNonTrouve = true;
                    }
                    else
                    {
                        DataRow club = dt.Rows[0];

                        CChesstion.CreerClub(int.Parse(club["Ref"].ToString()), club["NrFFE"].ToString(),
                            club["Nom"].ToString(), club["Ligue"].ToString(), club["Commune"].ToString(),
                            club["Actif"].ToString(), false);
                    }
                }



                int repas = int.Parse(row["RepasRef"].ToString());
                if (GOpen.GetOpen(int.Parse(row["OpenRef"].ToString())) == null 
                    || clubNonTrouve
                    || GRepas.GetRepas(repas) == null)
                {


                    if (errored == string.Empty)
                    {
                        errored = CChesstion.BasePath + @"\Logs\error" + DateTime.Now.ToString("dd-MM-yyy_HH-mm") + ".txt";
                        File.WriteAllText(errored, string.Format(
                            "{0,6} {1,15} {2,15} {3,10} {4,30} {5, 10} {6,50}" + Environment.NewLine + Environment.NewLine,
                            "Nr FFE",
                            "Nom",
                            "Prenom",
                            "Phone",
                            "Email",
                            "Importé ?",
                            "Error"
                        ));
                    }

                    if (GOpen.GetOpen(int.Parse(row["OpenRef"].ToString())) == null)
                    {
                        File.AppendAllText(errored, string.Format(
                            "{0,6} {1,15} {2,15} {3,10} {4,30} {5, 10} {6,50}" + Environment.NewLine,
                            row["NrFFE"].ToString(),
                            row["Nom"].ToString(),
                            row["Prenom"].ToString(),
                            row["Phone"].ToString(),
                            row["Email"].ToString(),
                            "Non",
                            "open non trouvé (ref " + row["OpenRef"].ToString() + ")"
                        ));
                        continue;
                    } else if (clubNonTrouve)
                    {

                        File.AppendAllText(errored, string.Format(
                            "{0,6} {1,15} {2,15} {3,10} {4,30} {5, 10} {6,50}" + Environment.NewLine,
                            row["NrFFE"].ToString(),
                            row["Nom"].ToString(),
                            row["Prenom"].ToString(),
                            row["Phone"].ToString(),
                            row["Email"].ToString(),
                            "Non",
                            "club non trouvé (ref " + row["ClubRef"].ToString() + ")"
                        ));
                        continue;

                    }
                    if (GRepas.GetRepas(repas) == null)
                    {
                        File.AppendAllText(errored, string.Format(
                            "{0,6} {1,15} {2,15} {3,10} {4,30}  {5, 10} {6,50}" + Environment.NewLine,
                            row["NrFFE"].ToString(),
                            row["Nom"].ToString(),
                            row["Prenom"].ToString(),
                            row["Phone"].ToString(),
                            row["Email"].ToString(),
                            "Oui",
                            "repas non trouvé (ref " + row["RepasRef"].ToString() + ", -1 attribué)"
                        ));
                        repas = -1;
                        //Debug.WriteLine(" déclanché");
                    }

                    //if (repas == 0 && GRepas.GetRepas(repas) != null)
                        //Debug.WriteLine("So ? " + GRepas.GetRepas(repas).Nom);

                }


                CChesstion.CreerJoueur(
                    int.Parse(row["Ref"].ToString()),
                    row["NrFFE"].ToString(),
                    row["Nom"].ToString(),
                    row["Prenom"].ToString(),
                    row["Sexe"].ToString(),
                    row["NeLe"].ToString(),
                    row["Cat"].ToString(),
                    row["Federation"].ToString(),
                    int.Parse(row["ClubRef"].ToString()),
                    int.Parse(row["Elo"].ToString()),
                    int.Parse(row["Rapide"].ToString()),
                    row["Fide"].ToString(),
                    row["FideCode"].ToString(),
                    row["FideTitre"].ToString(),
                    row["AffType"].ToString(),
                    row["Actif"].ToString(),
                    int.Parse(row["OpenRef"].ToString()),
                    row["Email"].ToString(),
                    bool.Parse(row["Subscribe"].ToString()),
                    row["Phone"].ToString(),
                    repas,
                    !row.Table.Columns.Contains("Confirme") ? false : bool.Parse(row["Confirme"].ToString()),
                    false
                );
            }

            backgroundWorker?.ReportProgress(100);

            if (errored != string.Empty)
                MessageBox.Show("Des joueurs n'ont pas pu être importés. Un fichier a été créé pour décrire ces erreurs :\n\n" + errored);

            if (reloadPanel) CChesstion.CentrePanel.Load();

            return dataTable.Rows.Count;
        }
    }
}
