using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Controleur.Etats
{
    /// <summary>
    /// Classe abstraite représentant un état d'un <see cref="Tournoi"/>.
    /// </summary>
    abstract class Etat
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
        /// Thread utilisé pour les travaux opérations longues.
        /// </summary>
        protected BackgroundWorker BackgroundWorker { get; set; }

        /// <summary>
        /// Numéro de l'état.
        /// </summary>
        public abstract int Etape { get; protected set; }

        /// <summary>
        /// Nom de l'état.
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// Nom complet de l'état (incluant le <see cref="Name"/> et le numéro <see cref="Etape"/>).
        /// </summary>
        public string FullName { get { return "Etape " + Etape + " - " + Name; } }

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
        /// Méthode à appeler pour effectuer l'ensemble des actions qui mènent de l'état précédent à cet état.
        /// </summary>
        /// <param name="loadInterface">Vrai pour appeler <see cref="LoadInterface"/> à la fin de cette méthode.</param>
        public abstract void Transition(bool loadInterface = true);

        /// <summary>
        /// Charge l'interface pour coller à l'état.
        /// </summary>
        public abstract void LoadInterface();
    }
}
