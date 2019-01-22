using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ChessTion.Modele.MLieu;
using ChessTion.Test;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChessTion.Controleur.CLieu
{
    /// <summary>
    /// Classe gérant l'ensemble des <see cref="Lieu"/> et des <see cref="Ville"/>.
    /// </summary>
    class GLieu
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
        /// Référence du prochain <see cref="Lieu"/> créé.
        /// </summary>
        private static int ProchaineRefLieu {
            get
            {
                int reference = 0;
                JArray villes = JArray.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/lieux.json"));
                foreach (JObject ville in villes.Children<JObject>())
                {
                    JArray lieux = ville["TsLesLieux"].Value<JArray>();

                    foreach (JObject lieu in lieux.Children<JObject>())
                    {
                        int r = (int)lieu["Ref"];
                        if (r > reference)
                            reference = r;
                    }
                }

                return reference + 1;
            }
        }

        /// <summary>
        /// Référence de la prochaine <see cref="Ville"/> créé.
        /// </summary>
        private static int ProchaineRefVille {
            get
            {
                int reference = 0;
                JArray o = JArray.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/lieux.json"));
                foreach (JObject item in o.Children<JObject>())
                {
                    int r = (int) item["Ref"];
                    if (r > reference)
                        reference = r;
                }

                return reference + 1;
            }
        }



        /// <summary>
        /// Ensemble des <see cref="Lieu"/> créés.
        /// </summary>
        static List<Lieu> TsLesLieux { get; set; } = new List<Lieu>();

        /// <summary>
        /// Ensemble des <see cref="Ville"/> créés.
        /// </summary>
        static List<Ville> TtesLesVilles { get; set; } = new List<Ville>();










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
        // Lieux
        //

        /// <summary>
        /// Retourne la liste des <see cref="Lieu"/> créés.
        /// </summary>
        /// <returns></returns>
        public static List<Lieu> ListerLieux()
        {
            return TsLesLieux;
        }

        /// <summary>
        /// Retourne un <see cref="Lieu"/>.
        /// </summary>
        /// <param name="reference">Référence du lieu à retourner.</param>
        /// <returns></returns>
        public static Lieu GetLieu(int reference)
        {
            Lieu gl = null;

            foreach (Lieu l in TsLesLieux)
            {
                if (l.Ref == reference)
                {
                    gl = l;
                }
            }

            return gl;
        }

        /// <summary>
        /// Crée un <see cref="Lieu"/>.
        /// </summary>
        /// <param name="nom">Nom du lieu.</param>
        /// <param name="numero">Numéro de rue du lieu.</param>
        /// <param name="rue">Rue du lieu.</param>
        /// <returns>Lieu créé.</returns>
        public static Lieu CreerLieu(string nom, string numero, string rue)
        {
            Lieu l = new Lieu(ProchaineRefLieu, nom, numero, rue);

            TsLesLieux.Add(l);

            EnregistrerLieux();

            return l;
        }

        /// <summary>
        /// Supprime un <see cref="Lieu"/>.
        /// </summary>
        /// <param name="reference">Référence du lieu à supprimer.</param>
        public static void SupprimerLieu(int reference)
        {
            TsLesLieux.Remove(GetLieu(reference));

            EnregistrerLieux();
        }



        //
        // Villes
        //

        /// <summary>
        /// Retourne la liste des <see cref="Ville"/> créées.
        /// </summary>
        /// <returns></returns>
        public static List<Ville> ListerVilles()
        {
            return TtesLesVilles;
        }

        /// <summary>
        /// Retourne une <see cref="Ville"/>.
        /// </summary>
        /// <param name="reference">Référence de la ville à retourner.</param>
        /// <returns></returns>
        public static Ville GetVille(int reference)
        {
            Ville gv = null;

            foreach (Ville v in TtesLesVilles)
            {
                if (v.Ref == reference)
                {
                    gv = v;
                }
            }

            return gv;
        }

        /// <summary>
        /// Crée une <see cref="Ville"/>.
        /// </summary>
        /// <param name="nom">Nom de la ville à créer.</param>
        /// <param name="codePostal">Code postal de la ville à créer.</param>
        /// <returns>La ville créée.</returns>
        public static Ville CreerVille(string nom, string codePostal)
        {
            Ville v = new Ville(ProchaineRefVille, nom, codePostal);

            TtesLesVilles.Add(v);

            EnregistrerLieux();

            return v;
        }

        /// <summary>
        /// Supprime une <see cref="Ville"/>.
        /// </summary>
        /// <param name="reference">Référence de la ville à supprimer.</param>
        public static void SupprimerVille(int reference)
        {
            TtesLesVilles.Remove(GetVille(reference));

            EnregistrerLieux();
        }

        






        /// <summary>
        /// Enregistre l'ensemble des villes dans un fichier.
        /// </summary>
        public static void EnregistrerLieux()
        {
            string json = JsonConvert.SerializeObject(TtesLesVilles, Formatting.Indented);
            JArray a = JArray.Parse(json);

            foreach (JObject content in a.Children<JObject>())
            {
                json = JsonConvert.SerializeObject(GetVille((int)content["Ref"]).TsLesLieux);
                JArray aa = JArray.Parse(json);
                content["TsLesLieux"] = aa;
            }

            File.WriteAllText(CChesstion.SettingsFolder + "/lieux.json", a.ToString());

            Debug.WriteLine("Enregistré ");
        }

        /// <summary>
        /// Charge toutes les <see cref="Ville"/> depuis un fichier.
        /// </summary>
        /// <param name="emptyFirst">Supprime toutes les villes créées d'abord.</param>
        /// <param name="overwrite">Excrase les anciennes villes si deux réf entrent en conflit.</param>
        public static void ChargerLieux(bool emptyFirst = false, bool overwrite = false)
        {
            string json = File.ReadAllText(CChesstion.SettingsFolder + "/lieux.json");
            List<Ville> villes = JsonConvert.DeserializeObject<List<Ville>>(json);

            int maxref = 1;

            if (emptyFirst)
                TtesLesVilles.Clear();

            foreach (Ville v in villes)
            {
                if (GetVille(v.Ref) == null)
                {
                    TtesLesVilles.Add(v);
                }
                else if (overwrite)
                {
                    TtesLesVilles.Remove(GetVille(v.Ref));
                    TtesLesVilles.Add(v);
                    
                }

            }

            foreach (Ville v in TtesLesVilles)
            {
                foreach (Lieu l in v.TsLesLieux)
                    if (GLieu.GetLieu(l.Ref) == null)
                    {
                        TsLesLieux.Add(l);
                        if (l.Ref > maxref) maxref = l.Ref + 1;
                    }
            }



        }

    }
}
