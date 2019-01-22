using System.Collections.Generic;
using ChessTion.Modele.MRepas;

namespace ChessTion.Controleur.CRepas
{
    /// <summary>
    /// Classe gérant l'ensemble des <see cref="Repas"/>.
    /// </summary>
    class GRepas
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
        /// Constante représentant le choix d'aucun repas.
        /// </summary>
        public static readonly int AUCUN_REPAS = -1;

        /// <summary>
        /// Référence du prochain repas créé.
        /// </summary>
        private static int ProchaineRef { get; set; } = 1;

        /// <summary>
        /// Ensemble de tous les repas créés.
        /// </summary>
        private static List<Repas> TsLesRepas = new List<Repas>() { new Repas(-1, "Aucun", 0) };









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
        /// Ensemble de tous les <see cref="Repas"/> créés.
        /// </summary>
        public static List<Repas> ListerRepas()
        {
            return TsLesRepas;
        }

        /// <summary>
        /// Retourne un <see cref="Repas"/>.
        /// </summary>
        /// <param name="reference">Référence du repas.</param>
        /// <returns></returns>
        public static Repas GetRepas(int reference)
        {
            Repas gr = null;

            foreach (Repas r in TsLesRepas)
            {
                if (r.Ref == reference)
                {
                    gr = r;
                }
            }

            return gr;
        }

        /// <summary>
        /// Crée un <see cref="Repas"/>.
        /// </summary>
        /// <param name="nom">Nom du repas.</param>
        /// <param name="prix">Prix du repas.</param>
        /// <returns>Le repas créé.</returns>
        public static Repas CreerRepas(string nom, float prix)
        {
            int reference;

            do
            {
                reference = ProchaineRef++;
            } while (GRepas.GetRepas(reference) != null);

            Repas r = new Repas(reference, nom, prix);

            TsLesRepas.Add(r);
            return r;
        }

        /// <summary>
        /// Crée un <see cref="Repas"/>.
        /// </summary>
        /// <param name="reference">Référence du repas.</param>
        /// <param name="nom">Nom du repas.</param>
        /// <param name="prix">Prix du repas.</param>
        /// <returns>Le repas créé.</returns>
        public static Repas CreerRepas(int reference, string nom, float prix)
        {
            if (reference == AUCUN_REPAS)
                return GRepas.GetRepas(AUCUN_REPAS);
            if (GRepas.GetRepas(reference) != null)
                throw new System.ArgumentException("Un repas avec la ref " + reference + " existe déjà.");

            Repas r = new Repas(reference, nom, prix);

            TsLesRepas.Add(r);
            return r;
        }

        /// <summary>
        /// Supprime un <see cref="Repas"/>.
        /// </summary>
        /// <param name="reference">Référence du repas à supprimer.</param>
        public static void SupprimerRepas(int reference)
        {
            TsLesRepas.Remove(GetRepas(reference));
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
