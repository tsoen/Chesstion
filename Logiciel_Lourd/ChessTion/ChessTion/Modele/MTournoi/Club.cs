namespace ChessTion.Modele.MTournoi
{
    /// <summary>
    /// Classe métier gérant les clubs des joueurs.
    /// </summary>
    class Club
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
        /// Référence du club.
        /// </summary>
        public int Ref { get; set; }

        /// <summary>
        /// Numéro FFE du club.
        /// </summary>
        public string NrFFE { get; set; }

        /// <summary>
        /// Nom du club.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Ligue du club.
        /// </summary>
        public string Ligue { get; set; }

        /// <summary>
        /// Cummune du club.
        /// </summary>
        public string Commune { get; set; }

        /// <summary>
        /// Actif du club.
        /// </summary>
        public string Actif { get; set; }










        /*************************************************************************************
         *   ___  _____  _  _  ___  ____  ____  __  __   ___  ____  ____  __  __  ____  ___  *
         *  / __)(  _  )( \( )/ __)(_  _)(  _ \(  )(  ) / __)(_  _)( ___)(  )(  )(  _ \/ __) *
         * ( (__  )(_)(  )  ( \__ \  )(   )   / )(__)( ( (__   )(   )__)  )(__)(  )   /\__ \ *
         *  \___)(_____)(_)\_)(___/ (__) (_)\_)(______) \___) (__) (____)(______)(_)\_)(___/ *
         *                                                                                   *
         *                      Ensemble des constructeurs de la classe.                     *
         *                                                                                   *
         *************************************************************************************/

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="reference">Référence du club.</param>
        /// <param name="nrFFE">Numéro FFE du club.</param>
        /// <param name="nom">Nom du club.</param>
        /// <param name="ligue">Ligue du club.</param>
        /// <param name="commune">Cummune du club.</param>
        /// <param name="actif">Actif du club.</param>
        public Club(int reference, string nrFFE, string nom, string ligue, string commune, string actif)
        {
            this.Ref = reference;
            this.NrFFE = nrFFE;
            this.Nom = nom;
            this.Ligue = ligue;
            this.Commune = commune;
            this.Actif = actif;
        }










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
        /// Retourne un texte décrivant l'objet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Ref + " " + this.Nom + " " + this.Commune + " " + this.Ligue + " " + this.NrFFE + " " + this.Actif;
         }
    }
}
