using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTion.Modele.MTournoi;
using Newtonsoft.Json;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe gérant l'exportation des <see cref="Club"/>.
    /// </summary>
    static class ExportClubs
    {
        /// <summary>
        /// Export les clubs en json.
        /// </summary>
        /// <param name="clubs">Liste des clubs à exporter.</param>
        /// <param name="pathToJson">Chemin d'accès du fichier json.</param>
        /// <returns>Le code json.</returns>
        public static string ToJson(List<Club> clubs, string pathToJson = null)
        {
            string json = JsonConvert.SerializeObject(clubs, Formatting.Indented);
            if (pathToJson != null)
                File.WriteAllText(pathToJson, json);

            return json;
        }
    }
}
