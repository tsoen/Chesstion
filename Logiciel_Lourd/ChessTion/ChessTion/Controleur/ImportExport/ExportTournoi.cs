using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MRepas;
using ChessTion.Modele.MTournoi;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.ImportExport
{
    /// <summary>
    /// Classe gérant l'exportation de <see cref="Tournoi"/>.
    /// </summary>
    static class ExportTournoi
    {
        /// <summary>
        /// Exporte un tournoi en json.
        /// </summary>
        /// <param name="reference">Référence du tournoi à exporter.</param>
        /// <returns>Le code json associé.</returns>
        public static string ToJson(int reference)
        {
            Tournoi tournoi = GTournoi.GetTournoi(reference);

            JArray opens = new JArray();
            JArray repas = new JArray();

            foreach (Open open in tournoi.TsLesOpens)
            {
                JObject o = new JObject
                {
                    {"Ref", open.Ref},
                    {"Nom", open.Nom},
                    {"EloMax", open.EloMax}
                };

                opens.Add(o);
            }

            foreach (Repas r in GRepas.ListerRepas())
            {
                JObject o = new JObject
                {
                    {"Ref", r.Ref},
                    {"Nom", r.Nom},
                    {"Prix", r.Prix}
                };

                repas.Add(o);
            }

            JObject infos = new JObject
            {
                {"Ref", tournoi.Ref},
                {"Nom", tournoi.Nom},
                {"DateDebut", tournoi.DateDebut.ToShortDateString()},
                {"DateFin", tournoi.DateFin.ToShortDateString()},
                {"NomLieu", tournoi.Lieu.Nom},
                {"Ville", tournoi.Lieu.GetVille().Nom}, // <-- glitch
                {"LieuRef", tournoi.Lieu.Ref },
                {"AdresseNum", tournoi.Lieu.Numero},
                {"AdresseRue", tournoi.Lieu.Rue},
                {"PrixJeune", tournoi.PrixJeune},
                {"PrixVieux", tournoi.PrixVieux},
                {"Majoration", tournoi.Majoration},
                {"LimiteAge", tournoi.LimiteAge },
                {"NbParticipants", tournoi.MaxParticipants},
                {"NbOpens", tournoi.TsLesOpens.Count},
                {"Opens", opens },
                {"Repas", repas },
                {"Arbitre", tournoi.Arbitre },
                {"Ouvert", tournoi.Etat == 2 },
                {"Etat", tournoi.Etat },
                {"NbRondes", tournoi.NbRondes },
                {"DureeRonde", tournoi.DureeRonde },
                {"ConfirmMail", tournoi.ConfirmMail }
            };
            return infos.ToString();
        }
    }
}
