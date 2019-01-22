using System.Collections.Generic;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Modele.MLieu
{
    /// <summary>
    /// Classe métier gérant les villes dans lesquelles se déroulent les <see cref="Tournoi"/>.
    /// </summary>
    class Ville
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
        /// Référence de la ville.
        /// </summary>
        public int Ref { get; set; }

        /// <summary>
        /// Nom de la ville.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Code postal de la ville.
        /// </summary>
        public string CodePostal { get; set; }

        /// <summary>
        /// Ensemble des <see cref="Lieu"/> se trouvant dans la ville.
        /// </summary>
        public List<Lieu> TsLesLieux { get; set; } = new List<Lieu>();










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
        /// <param name="reference">Référence de la ville.</param>
        /// <param name="nom">Nom de la ville.</param>
        /// <param name="codePostal">Code postal de la ville.</param>
        public Ville(int reference, string nom, string codePostal)
        {
            this.Ref = reference;
            this.Nom = nom;
            this.CodePostal = codePostal;
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
        /// Retourne un nom complet de ville, se composant du <see cref="Nom"/> et du <see cref="CodePostal"/>.
        /// </summary>
        public string NomComplet { get { return Nom + " " + CodePostal; } }

        /// <summary>
        /// Retourne l'ensemble des lieux se trouvant dans la ville.
        /// </summary>
        /// <returns>Liste de lieux.</returns>
        public List<Lieu> ListerLieux()
        {
            return this.TsLesLieux;
        }
    }
}
