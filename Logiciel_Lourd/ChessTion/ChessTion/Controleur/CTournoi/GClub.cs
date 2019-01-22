using ChessTion.Modele.MTournoi;
using System.Collections.Generic;

namespace ChessTion.Controleur.CTournoi
{
    /// <summary>
    /// Classe gérant l'ensemble des <see cref="Club"/>.
    /// </summary>
    class GClub
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
        /// Ensemble de tous les <see cref="Club"/> créés.
        /// </summary>
        private static List<Club> TsLesClubs { get; set; } = new List<Club>();










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
        /// Ensemble de tous les <see cref="Club"/> créés.
        /// </summary>
        public static List<Club> ListerClubs()
        {
            return TsLesClubs;
        }

        /// <summary>
        /// Retourne un <see cref="Club"/>.
        /// </summary>
        /// <param name="reference">Référence du club à retourner.</param>
        /// <returns></returns>
        public static Club GetClub(int reference)
        {
            Club cres = null;

            foreach (Club c in TsLesClubs)
            {
                if (c.Ref == reference)
                {
                    cres = c;
                }
            }

            return cres;
        }

        /// <summary>
        /// Crée un <see cref="Club"/>.
        /// </summary>
        /// <param name="reference">Référence du club.</param>
        /// <param name="nrFFE">Numéro FFE du club.</param>
        /// <param name="nom">Nom du club.</param>
        /// <param name="ligue">Ligue du club.</param>
        /// <param name="commune">Commune du club.</param>
        /// <param name="actif">Actif du club.</param>
        /// <returns>Le club créé.</returns>
        public static Club CreerClub(int reference, string nrFFE, string nom, string ligue, string commune, string actif)
        {
            Club c = new Club(reference, nrFFE, nom, ligue, commune, actif);
            TsLesClubs.Add(c);
            return c;
        }

        /// <summary>
        /// Supprime un <see cref="Club"/>.
        /// </summary>
        /// <param name="reference">Référence du club à supprimer.</param>
        public static void SupprimerClub(int reference)
        {
            TsLesClubs.Remove(GetClub(reference));
        }
    }
}
