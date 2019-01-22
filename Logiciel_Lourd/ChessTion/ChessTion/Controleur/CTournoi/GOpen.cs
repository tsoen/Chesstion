using System.Collections.Generic;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Controleur.CTournoi
{
    /// <summary>
    /// Classe gérant l'ensemble des <see cref="Open"/>.
    /// </summary>
    static class GOpen
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
        /// Référence du prochain <see cref="Open"/>* créé.
        /// </summary>
        private static int ProchaineRef { get; set; } = 1;

        /// <summary>
        /// Ensemble de tous les <see cref="Open"/> créés.
        /// </summary>
        public static List<Open> TsLesOpens { get; private set; }  = new List<Open>();










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
        /// Liste l'ensemble des <see cref="Open"/>.
        /// </summary>
        /// <returns></returns>
        public static List<Open> ListerOpens()
        {
            return TsLesOpens;
        }

        /// <summary>
        /// Liste l'ensemble des <see cref="Open"/> d'un <see cref="Tournoi"/>.
        /// </summary>
        /// <param name="tournoi"></param>
        /// <returns></returns>
        public static List<Open> ListerOpens(Tournoi tournoi)
        {
            return tournoi.TsLesOpens;
        }

        /// <summary>
        /// Retourne un <see cref="Open"/> correspondant à une référence.
        /// </summary>
        /// <param name="reference">Référence de l'<see cref="Open"/> à sélectionner.</param>
        /// <returns></returns>
        public static Open GetOpen(int reference)
        {
            Open go = null;

            foreach (Open o in TsLesOpens)
            {
                if (o.Ref == reference)
                {
                    go = o;
                }
            }

            return go;
        }

        /// <summary>
        /// Crée un <see cref="Open"/>.
        /// </summary>
        /// <param name="nom">Nom de l'open.</param>
        /// <param name="eloMax">Elo maximum de l'open.</param>
        /// <returns>L'open créé.</returns>
        public static Open CreerOpen(string nom, int eloMax)
        {
            return CreerOpen(ProchaineRef++, nom, eloMax);
        }

        /// <summary>
        /// Crée un <see cref="Open"/>.
        /// </summary>
        /// <param name="reference">Référence de l'open.</param>
        /// <param name="nom">Nom de l'open.</param>
        /// <param name="eloMax">Elo maximum de l'open.</param>
        /// <returns>L'open créé.</returns>
        public static Open CreerOpen(int reference, string nom, int eloMax)
        {
            if (GOpen.GetOpen(reference) != null)
                throw new System.ArgumentException("Un open avec la réf " + reference + " existe déjà.");

            Open o = new Open(reference, nom, eloMax);
            TsLesOpens.Add(o);
            return o;
        }

        /// <summary>
        /// Supprime un <see cref="Open"/>.
        /// </summary>
        /// <param name="reference">Référence de l'open à supprimer.</param>
        public static void SupprimerOpen(int reference)
        {
            GetOpen(reference).TsLesJoueurs.Clear();
            //GTournoi.GetTournoiDeOpen(reference).SupprimerOpen(reference);
            TsLesOpens.Remove(GetOpen(reference));
        }

        /// <summary>
        /// Retourne l'<see cref="Open"/> d'un <see cref="Joueur"/>.
        /// </summary>
        /// <param name="joueurRef">Référence du joueur.</param>
        /// <returns></returns>
        public static Open GetOpenDuJoueur(int joueurRef)
        {
            Open res = null;
            foreach(Open o in TsLesOpens)
            {
                foreach (Joueur j in o.TsLesJoueurs)
                {
                    if(j.Ref == joueurRef)
                    {
                        res = o;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Remet le compteur de références à 1.
        /// </summary>
        public static void RecommencerProchaineRef()
        {
            ProchaineRef = 1;
        }

    }
}
