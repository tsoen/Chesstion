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
    /// Classe gérant l'importation de <see cref="Club"/> dans le logiciel.
    /// </summary>
    static class ImportClub
    {
        /// <summary>
        /// Importe des clubs depuis un fichier json.
        /// </summary>
        /// <param name="jsonPath">Chemin d'accès du fichier json.</param>
        public static void FromJson(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new ArgumentException("Le fichier " + jsonPath + " n'existe pas.");

            List<Club> clubs = JsonConvert.DeserializeObject<List<Club>>(File.ReadAllText(jsonPath));

            foreach (Club c in clubs)
                CChesstion.CreerClub(c.Ref, c.NrFFE, c.Nom, c.Ligue, c.Commune, c.Actif, false);
        }
    }
}
