using System;
using System.Collections.Generic;
using System.IO;
using ChessTion.Modele.MTournoi;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.CTournoi
{
    /// <summary>
    /// Classe gérant l'ensemble des <see cref="Tournoi"/>.
    /// </summary>
    static class GTournoi
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
        /// Référence du prochain <see cref="Tournoi"/> créé.
        /// </summary>
        private static int ProchaineRef
        {
            get { return (int)JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"))["prochaineRefTournoi"]; }
            set
            {
                JObject o = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"));
                o["prochaineRefTournoi"] = value;
                File.WriteAllText(CChesstion.SettingsFolder + "/settings.json", o.ToString());
            }
        }

        /// <summary>
        /// Ensemble de tous les <see cref="Tournoi"/> créés.
        /// </summary>
        static List<Tournoi> TsLesTournois = new List<Tournoi>();



        //
        // Constantes
        //

        /// <summary>
        /// Représente un tournoi en cours de création.
        /// </summary>
        public const int ETAT__CREATION = 1;

        /// <summary>
        /// Représente un tournoi dont les inscriptions ont été ouvertes.
        /// </summary>
        public const int ETAT__INSCRIPTIONS_OUVERTES = 2;

        /// <summary>
        /// Représente un tournoi dont les inscriptions ont été fermées et qui est en cours d'accueil des joueurs.
        /// </summary>
        public const int ETAT__ACCUEIL_JOUEURS = 3;

        /// <summary>
        /// Représente un tournoi en cours de déroulement.
        /// </summary>
        public const int ETAT__TOURNOI_EN_COURS = 4;

        /// <summary>
        /// Représente un tournoi terminé.
        /// </summary>
        public const int ETAT__TOURNOI_TERMINE = 5;










        /********************************************************
         *  __  __  ____  ____  _   _  _____  ____   ____  ___  *
         * (  \/  )( ___)(_  _)( )_( )(  _  )(  _ \ ( ___)/ __) *
         *  )    (  )__)   )(   ) _ (  )(_)(  )(_) ) )__) \__ \ *
         * (_/\/\_)(____) (__) (_) (_)(_____)(____/ (____)(___/ *
         *                                                      *
         *      Ensemble des méthodes autres de la classe.      *
         *                                                      *
         ********************************************************/

        /// <summary>
        /// Liste l'ensemble des <see cref="Tournoi"/> créés.
        /// </summary>
        /// <returns></returns>
        public static List<Tournoi> ListerTournois()
        {
            return TsLesTournois;
        }

        /// <summary>
        /// Retourne un <see cref="Tournoi"/>.
        /// </summary>
        /// <param name="reference">Référence du tournoi à retourner.</param>
        /// <returns></returns>
        public static Tournoi GetTournoi(int reference)
        {
            Tournoi gt = null;

            foreach (Tournoi t in TsLesTournois)
            {
                if (t.Ref == reference)
                {
                    gt = t;
                }
            }

            return gt;
        }

        /// <summary>
        /// Crée un <see cref="Tournoi"/>.
        /// </summary>
        /// <param name="nom">Nom du tournoi.</param>
        /// <param name="dateDebut">Date de début du tournoi.</param>
        /// <param name="dateFin">Date de fin du tournoi.</param>
        /// <param name="lieuRef">Référence du lieu du tournoi.</param>
        /// <returns>Le tournoi créé.</returns>
        public static Tournoi CreerTournoi(string nom, DateTime dateDebut, DateTime dateFin, int lieuRef)
        {
            Tournoi t = new Tournoi(ProchaineRef, nom, dateDebut, dateFin, lieuRef);

            TsLesTournois.Add(t);
            ProchaineRef++;

            return t;
        }

        /// <summary>
        /// Crée un <see cref="Tournoi"/>.
        /// </summary>
        /// <param name="reference">Référence du tournoi.</param>
        /// <param name="nom">Nom du tournoi.</param>
        /// <param name="dateDebut">Date de début du tournoi.</param>
        /// <param name="dateFin">Date de fin du tournoi.</param>
        /// <param name="lieuRef">Référence du lieu du tournoi.</param>
        /// <returns>Le tournoi créé.</returns>
        public static Tournoi CreerTournoi(int reference, string nom, DateTime dateDebut, DateTime dateFin, int lieuRef)
        {
            if (GTournoi.GetTournoi(reference) != null)
                throw new ArgumentException("Un tournoi avec la référence " + reference + " existe déjà.");

            Tournoi t = new Tournoi(reference, nom, dateDebut, dateFin, lieuRef);

            TsLesTournois.Add(t);

            return t;
        }

        /// <summary>
        /// Crée un <see cref="Tournoi"/> donné en paramètre.
        /// </summary>
        /// <param name="t">Le tournoi à créer.</param>
        /// <returns>Le tournoi créé.</returns>
        public static Tournoi CreerTournoi(Tournoi t)
        {
            TsLesTournois.Add(t);
            return t;
        }

        /// <summary>
        /// Supprime un <see cref="Tournoi"/>.
        /// </summary>
        /// <param name="reference">Référence du tournoi à supprimer.</param>
        public static void SupprimerTournoi(int reference)
        {
            foreach (Open o in GetTournoi(reference).TsLesOpens)
            {
                GOpen.SupprimerOpen(o.Ref);
            }
                    
            TsLesTournois.Remove(GetTournoi(reference));
        }

        /// <summary>
        /// Retourne le <see cref="Tournoi"/> associé à un <see cref="Open"/>.
        /// </summary>
        /// <param name="openRef">La référence de l'open.</param>
        /// <returns>Le tournoi associé à l'open.</returns>
        public static Tournoi GetTournoiDeOpen(int openRef)
        {
            Tournoi res = null;

            foreach (Tournoi t in TsLesTournois)
            {
                foreach (Open o in t.TsLesOpens)
                {
                    if (o.Ref == openRef)
                    {
                        res = t;
                    }
                }
            }

            return res;
        }
    }
}
