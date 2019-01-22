using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MRepas;
using ChessTion.Modele.MTournoi;
using ChessTion.Test;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe gérant l'importation d'un <see cref="Tournoi"/> dans le logiciel.
    /// </summary>
    static class ImportTournoi
    {
        /// <summary>
        /// Importe un <see cref="Tournoi"/> depuis un fichier json.
        /// </summary>
        /// <param name="pathToJson">Chemin d'accès du fichier json contenant les infos du tournoi.</param>
        /// <returns></returns>
        public static Tournoi FromJson(string pathToJson)
        {
            if (!File.Exists(pathToJson))
                throw new ArgumentException("Le fichier n'existe pas.");

            string json = File.ReadAllText(pathToJson);

            Tournoi t = GTournoi.CreerTournoi(JsonConvert.DeserializeObject<Tournoi>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
            CChesstion.TournoiSelectionne = t;

            int lieuRef = (int)JObject.Parse(json)["LieuRef"];
            Debug.WriteLine(lieuRef.ToString());

            if (GLieu.GetLieu(lieuRef) != null)
                t.Lieu = GLieu.GetLieu(lieuRef);

            // Opens
            string opensString = JObject.Parse(json)["Opens"].ToString();
            JArray opensArray = JArray.Parse(opensString);
            List<Open> opens = JsonConvert.DeserializeObject<List<Open>>(opensArray.ToString());
            foreach (Open o in opens)
                CChesstion.CreerOpen(o);

            // Repas
            string repasString = JObject.Parse(json)["Repas"].ToString();
            JArray repasArray = JArray.Parse(repasString);
            List<Repas> repas = JsonConvert.DeserializeObject<List<Repas>>(repasArray.ToString());
            foreach (Repas r in repas)
                if (r.Ref != GRepas.AUCUN_REPAS)
                    CChesstion.CreerRepas(r);


            return t;
        }
    }
}
