using System;
using System.Collections.Generic;
using ChessTion.Modele.MTournoi;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using ChessTion.Utilitaires;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using ChessTion.Test;

namespace ChessTion.Controleur.CTournoi
{
    /// <summary>
    /// Classe gérant l'ensemble des <see cref="Joueur"/>.
    /// </summary>
    static class GJoueur
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
        /// Ensemble de tous les <see cref="Joueur"/> créés.
        /// </summary>
        static List<Joueur> TsLesJoueurs { get; set; } = new List<Joueur>();

        /// <summary>
        /// Référence du prochain <see cref="Joueur"/> créé.
        /// </summary>
        public static int ProchaineRef { get; set; } = 10000000;











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
        /// Ensemble de tous les <see cref="Joueur"/> créés.
        /// </summary>
        public static List<Joueur> ListerJoueurs()
        {
            return TsLesJoueurs;
        }

        /// <summary>
        /// Ensemble de tous les <see cref="Joueur"/> créés appartenant à un <see cref="Open"/>.
        /// <param name="open">Open duquel liste les joueurs.</param>
        /// <returns></returns>
        /// </summary>
        public static List<Joueur> ListerJoueurs(Open open)
        {
            return open.TsLesJoueurs;
        }

        /// <summary>
        /// Retourne un <see cref="Joueur"/>.
        /// </summary>
        /// <param name="reference">Référence du joueur à retourner.</param>
        /// <returns></returns>
        public static Joueur GetJoueur(int reference)
        {
            Joueur gj = null;

            foreach (Joueur j in TsLesJoueurs)
            {
                if (j.Ref == reference)
                {
                    gj = j;
                }
            }

            return gj;
        }
            
        /// <summary>
        /// Crée un <see cref="Joueur"/>.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="nrFFE">Numéro FFE du joueur.</param>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="prenom">Prénom du joueur.</param>
        /// <param name="sexe">Sexe du joueur.</param>
        /// <param name="neLe">Date de naissance du joueur.</param>
        /// <param name="cat">Catégorie du joueur.</param>
        /// <param name="federation">Fédération du joueur.</param>
        /// <param name="clubRef">Référence du club du joueur.</param>
        /// <param name="elo">Elo du joueur.</param>
        /// <param name="rapide">Rapide du joueur.</param>
        /// <param name="fide">Numéro fide du joueur.</param>
        /// <param name="fidecode">Code fide du joueur.</param>
        /// <param name="fidetitre">Titre fide du joueur.</param>
        /// <param name="affType">AffType du joueur.</param>
        /// <param name="actif">Actif du joueur.</param>
        /// <param name="openRef">Référence de l'open du joueur.</param>
        /// <param name="email">Email du joueur.</param>
        /// <param name="subscribe">Vrai si le joueur souhaite recevoir des mail d'infos sur le tournoi (scores, etc.)</param>
        /// <param name="phone">Numéro de téléphone du joueur.</param>
        /// <param name="repasRef">Référence du repas du joueur.</param>
        /// <param name="confirme">Vrai si la participation du joueur est confirmé (celui-ci a payé).</param>
        /// <returns>Le joueur créé.</returns>
        public static Joueur CreerJoueur(int reference, string nrFFE, string nom, string prenom, string sexe, string neLe, 
            string cat, string federation, int clubRef, int elo, int rapide, string fide, string fidecode, string fidetitre, 
            string affType, string actif, int openRef, string email, bool subscribe, string phone, int repasRef, bool confirme = false)
        {
            Joueur j = null;
            try
            {
                GOpen.GetOpen(openRef);

                j = new Joueur(reference, nrFFE, nom, prenom, sexe, neLe, cat, federation, clubRef, elo, rapide, fide, fidecode,
                     fidetitre, affType, actif, email, subscribe, phone, repasRef, confirme);

                TsLesJoueurs.Add(j);

                GOpen.GetOpen(openRef).AjouterJoueur(j);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + j.Nom);
            }

            return j;
        }

        /// <summary>
        /// Supprime un <see cref="Joueur"/>.
        /// </summary>
        /// <param name="reference">Référence du joueur à supprimer.</param>
        public static void SupprimerJoueur(int reference)
        {
            TsLesJoueurs.Remove(GetJoueur(reference));
        }

        /// <summary>
        /// Retourne la liste des erreurs d'un <see cref="Joueur"/> (est considérée erronée une information qui diffère de la base FFE).
        /// </summary>
        /// <param name="joueurRef">Référence du joueur.</param>
        /// <returns>La liste des erreurs. En position [0] le champ erroné, en position [1] la valeur trouvée en base.</returns>
        public static List<string[]> ComporteDesErreurs(int joueurRef)
        {
            Joueur j = GJoueur.GetJoueur(joueurRef);
            List<string[]> erreurs = new List<string[]>();
            OleDbDataAdapter da;
            DataTable dt;
            DataRow row;

            try
            {
                OleDbConnection Connec =
                    new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath +
                                        @"\Ressources\DATA.MDB");
                Connec.Open();



                string rqt = "select * from JOUEUR WHERE Ref = " + j.Ref;
                da = new OleDbDataAdapter(rqt, Connec);
                Connec.Close();
                dt = new DataTable();
                da.Fill(dt);
                row = dt.Rows[0];
            }

            catch
            {
                return erreurs;
            }


            foreach (DataColumn col in dt.Columns)
            {
                string[] erreurs_array = new string[2];

                if (j.GetType().GetProperty(col.ColumnName).Name.Equals("NeLe"))
                {
                    string s = Convert.ToDateTime(row[col].ToString()).ToString("dd/MM/yyyy");

                    if (!j.GetType().GetProperty(col.ColumnName).GetValue(j).ToString().Equals(s))
                    {
                        erreurs_array[0] = (j.GetType().GetProperty(col.ColumnName).Name);
                        erreurs_array[1] = row[col].ToString();
                        erreurs.Add(erreurs_array);
                    }
                }
                else
                {
                    if (!j.GetType().GetProperty(col.ColumnName).GetValue(j).ToString().Equals(row[col].ToString()))
                    {
                        erreurs_array[0] = (j.GetType().GetProperty(col.ColumnName).Name);
                        erreurs_array[1] = row[col].ToString();

                        if (col.ColumnName.Equals("ClubRef"))
                        {
                            try
                            {
                                if (GClub.GetClub(int.Parse(row[col].ToString())) == null)
                                    CChesstion.CreerClub(int.Parse(row[col].ToString()));


                                erreurs_array[1] += ". " + GClub.GetClub(int.Parse(row[col].ToString())).Nom;
                            }
                            catch (Exception e)
                            {
                                erreurs_array[1] += ". Réf de club non trouvée en base FFE (" + e.Message + ")";
                            }
                        }

                        erreurs.Add(erreurs_array);
                    }
                }
            }

            if (GOpen.GetOpenDuJoueur(joueurRef).EloMax != -1 && j.Elo > GOpen.GetOpenDuJoueur(joueurRef).EloMax)
            {
                string[] erreurs_array = new string[2];
                erreurs_array[0] = "OpenRef";
                erreurs_array[1] = GOpen.GetOpenDuJoueur(joueurRef).EloMax.ToString();
                erreurs.Add(erreurs_array);
            }
            return erreurs;
        }

    }
}
