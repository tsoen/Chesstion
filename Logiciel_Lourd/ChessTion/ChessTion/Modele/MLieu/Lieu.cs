using ChessTion.Controleur.CLieu;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Modele.MLieu
{
    /// <summary>
    /// Classe métier gérant les lieux des <see cref="Tournoi"/>.
    /// </summary>
    class Lieu
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
        /// Référence du lieu.
        /// </summary>
        public int Ref { get; set; }

        /// <summary>
        /// Nom du lieu.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Numéro de rue du lieu.
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Rue du lieu.
        /// </summary>
        public string Rue { get; set; }










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
        /// <param name="reference">Référence du lieu.</param>
        /// <param name="nom">Nom du lieu.</param>
        /// <param name="numero">Numéro de rue du lieu.</param>
        /// <param name="rue">Rue du lieu.</param>
        public Lieu(int reference, string nom, string numero, string rue)
        {
            this.Ref = reference;
            this.Nom = nom;
            this.Numero = numero;
            this.Rue = rue;
        }










        /************************************************************
         *   ___  ____  ____    ____  ____    ___  ____  ____  ___  *
         *  / __)( ___)(_  _)  ( ___)(_  _)  / __)( ___)(_  _)/ __) *
         * ( (_-. )__)   )(     )__)   )(    \__ \ )__)   )(  \__ \ *
         *  \___/(____) (__)   (____) (__)   (___/(____) (__) (___/ *
         *                                                          *
         *       Ensemble des getters et setters de la classe.      *
         *                                                          *
         ************************************************************/

        /// <summary>
        /// Retrouve la <see cref="Ville"/> associée au lieu.
        /// </summary>
        /// <returns>La ville associée au lieu.</returns>
        public Ville GetVille()
        {
            Ville res = null;

            foreach(Ville v in GLieu.ListerVilles())
            {
                foreach(Lieu l in v.ListerLieux())
                {
                    if(l.Ref == this.Ref)
                    {
                        res = v;
                    }
                }
            }

            return res;
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
            return this.Nom + ", " + this.Numero + " " + this.Rue + " " + this.GetVille().CodePostal + " " + this.GetVille().Nom;
        }
    }
}
