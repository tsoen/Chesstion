using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.IO;
using ChessTion.Modele.MTournoi;
using ChessTion.Test;
using Newtonsoft.Json;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe gérant l'exportation de <see cref="Joueur"/>.
    /// </summary>
    static class ExportJoueurs
    {
        /// <summary>
        /// Exporte des joueurs dans un fichier mdb.
        /// </summary>
        /// <param name="joueurs">Liste des joueurs à exporter.</param>
        /// <param name="clubs">Liste des clubs associés aux joueurs.</param>
        /// <param name="pathToEmptyMdb">Chemin d'accès vers le fichier mdb à remplir.</param>
        /// <param name="backgroundWorker">BackgroundWorker à qui reporter le progress</param>
        public static void ToDataMdb(List<Joueur> joueurs, List<Club> clubs, string pathToEmptyMdb, BackgroundWorker backgroundWorker = null)
        {
            OleDbConnection connec = new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + pathToEmptyMdb);
            connec.Close();
            OleDbCommand command = new OleDbCommand("", connec);

            int i = 0, count = joueurs.Count + clubs.Count;
            foreach (Joueur joueur in joueurs)
            {
                string query = "INSERT INTO JOUEUR VALUES (";
                query += joueur.Ref + ", ";
                query += "'" + joueur.NrFFE + "', ";
                query += "'" + joueur.Nom.ToUpper().Replace("'", "''") + "', ";
                query += "'" + joueur.Prenom.Replace("'", "''") + "', ";
                query += "'" + joueur.Sexe + "', ";
                query += "FORMAT('" + joueur.NeLe + "', 'dd/mm/yyyy'), ";
                query += "'" + joueur.Cat.Replace("'", "''") + "', ";
                query += "'" + joueur.Federation.Replace("'", "''") + "', ";
                query += "" + joueur.ClubRef + ", ";
                query += "" + joueur.Elo + ", ";
                query += "" + joueur.Rapide + ", ";
                query += "'" + joueur.Fide + "', ";
                query += "'" + joueur.FideCode + "', ";
                query += "'" + joueur.FideTitre + "', ";
                query += "'" + joueur.AffType + "', ";
                query += "'" + joueur.Actif + "'";
                query += ");";

                Debug.WriteLine(query);

                command.CommandText = query;
                connec.Open();
                command.ExecuteNonQuery();
                connec.Close();

                backgroundWorker?.ReportProgress((100*i++)/count);
            }

            foreach (Club club in clubs)
            {
                string query = "INSERT INTO CLUB VALUES (";
                query += club.Ref + ", ";
                query += "'" + club.NrFFE + "', ";
                query += "'" + club.Nom.Replace("'", "''") + "', ";
                query += "'" + club.Ligue.Replace("'", "''") + "', ";
                query += "'" + club.Commune.Replace("'", "''") + "', ";
                query += "'" + club.Actif + "'";
                query += ")";

                Debug.WriteLine(query);

                command.CommandText = query;
                connec.Open();
                command.ExecuteNonQuery();
                connec.Close();

                backgroundWorker?.ReportProgress((100*i++)/count);
            }
        }

        /// <summary>
        /// Exporte les joueurs dans un fichier json.
        /// </summary>
        /// <param name="joueurs">Liste des joueurs à exporter.</param>
        /// <param name="pathToJson">Chemin d'accès du fichier json.</param>
        /// <returns>Le code json.</returns>
        public static string ToJson(List<Joueur> joueurs, string pathToJson = null)
        {
            string json = JsonConvert.SerializeObject(joueurs, Formatting.Indented);
            if (pathToJson != null)
                File.WriteAllText(pathToJson, json);

            return json;
        }
    }
}
