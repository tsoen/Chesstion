using ChessTion.Controleur.CTournoi;
using System.Collections.Generic;

namespace ChessTion.Modele.MTournoi
{
    /// <summary>
    /// Classe gérant les opens.
    /// </summary>
    class Open
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
        /// Référence de l'open.
        /// </summary>
        public int Ref { get; set; }

        /// <summary>
        /// Nom de l'open.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Elo maximum autorisé pour s'inscrire à l'open.
        /// </summary>
        public int EloMax { get; set; }

        /// <summary>
        /// Tous les joueurs de l'open.
        /// </summary>
        public List<Joueur> TsLesJoueurs { get; set; } = new List<Joueur>();









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
        /// <param name="reference">Référence de l'open.</param>
        /// <param name="nom">Nom de l'open.</param>
        /// <param name="eloMax">Elo maximum autorisé pour s'inscrire à l'open.</param>
        public Open(int reference, string nom, int eloMax)
        {
            this.Ref = reference;
            this.Nom = nom;
            this.EloMax = eloMax;
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
        /// Ajouter un <see cref="Joueur"/> à l'open.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        public void AjouterJoueur(int reference)
        {
            Joueur j = GJoueur.GetJoueur(reference);
            if (j != null)
                TsLesJoueurs.Add(j);
        }

        /// <summary>
        /// Ajoute un <see cref="Joueur"/> à l'open.
        /// </summary>
        /// <param name="joueur">Le joueur à ajouter.</param>
        public void AjouterJoueur(Joueur joueur)
        {
            TsLesJoueurs.Add(joueur);
        }

        /// <summary>
        /// Supprime un <see cref="Joueur"/> de l'open.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        public void SupprimerJoueur(int reference)
        {
            TsLesJoueurs.Remove(GJoueur.GetJoueur(reference));
        }

        /// <summary>
        /// Supprime un <see cref="Joueur"/> de l'open.
        /// </summary>
        /// <param name="joueur">Joueur à supprimer.</param>
        public void SupprimerJoueur(Joueur joueur)
        {
            TsLesJoueurs.Remove(joueur);
        }

        /// <summary>
        /// Rajouter le mot 'Open' au nom de l'open si celui-ci n'y est pas encore.
        /// </summary>
        /// <returns>Le titre formatté</returns>
        public string TitreFormatte()
        {
            if (!this.Nom.ToLower().Contains("open"))
            {
                return "Open " + this.Nom;
            }
            else
            {
                return this.Nom;
            }
        }

        /// <summary>
        /// Retourne un texte représentant l'objet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Ref + " " + this.Nom + ". Elomax: " + this.EloMax + " Tournoi: " +
                   GTournoi.GetTournoiDeOpen(this.Ref).Ref;
        }
    }
}
