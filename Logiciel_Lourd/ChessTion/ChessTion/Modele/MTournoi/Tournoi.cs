using System;
using System.Collections.Generic;
using ChessTion.Controleur.CLieu;
using ChessTion.Modele.MLieu;
using ChessTion.Modele.MRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.CRepas;
using Newtonsoft.Json.Linq;

namespace ChessTion.Modele.MTournoi
{
    /// <summary>
    /// Classe gérant les tournois.
    /// </summary>
    class Tournoi
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
        /// Etat du tournoi.
        /// </summary>
        public int Etat { get; set; } = 1;



        /// <summary>
        /// Référence du tournoi.
        /// </summary>
        public int Ref { get; set; }

        /// <summary>
        /// Nom du tournoi.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Date de début du tournoi.
        /// </summary>
        public DateTime DateDebut { get; set; }

        /// <summary>
        /// Date de fin du tournoi.
        /// </summary>
        public DateTime DateFin { get; set; }

        /// <summary>
        /// <see cref="Lieu"/> du tournoi.
        /// </summary>
        public Lieu Lieu { get; set; }



        /// <summary>
        /// Prix à payer par les joueurs en-dessous de la <see cref="LimiteAge"/>.
        /// </summary>
        public float PrixJeune { get; set; } = 10;

        /// <summary>
        /// Prix à payer par les joueurs au-dessus de la <see cref="LimiteAge"/>.
        /// </summary>
        public float PrixVieux { get; set; } = 20;

        /// <summary>
        /// Majoration si le joueur s'inscrit le jour-même.
        /// </summary>
        public float Majoration { get; set; } = 0;

        /// <summary>
        /// Limite d'âge séparant le <see cref="PrixJeune"/> et le <see cref="PrixVieux"/>.
        /// </summary>
        public int LimiteAge { get; set; } = 20;

        /// <summary>
        /// Nombre maximum de participants au tournoi.
        /// </summary>
        public int MaxParticipants { get; set; } = 500;



        /// <summary>
        /// Email de l'arbitre ou de l'organisateur du tournoi.
        /// </summary>
        public string Arbitre { get; set; } = "arbitre@arbitre.fr";

        /// <summary>
        /// Nombre de rondes du tournoi.
        /// </summary>
        public int NbRondes { get; set; } = 6;

        /// <summary>
        /// Durée d'une ronde du tournoi.
        /// </summary>
        public string DureeRonde { get { return DureeRondeMinutes + ":" + DureeRondeSecondes; } }

        /// <summary>
        /// Minutes d'une ronde du tournoi.
        /// </summary>
        public int DureeRondeMinutes { get; set; } = 50;

        /// <summary>
        /// Secondes d'une ronde du tournoi.
        /// </summary>
        public int DureeRondeSecondes { get; set; } = 10;



        /// <summary>
        /// Opens du tournoi.
        /// </summary>
        public List<Open> TsLesOpens { get; } = new List<Open>();

        /// <summary>
        /// Repas proposés lors du tournoi.
        /// </summary>
        public List<Repas> TsLesRepas { get; } = new List<Repas>();

        /// <summary>
        /// Joueurs participant au tournoi.
        /// </summary>
        public List<Joueur> TsLesJoueurs
        {
            get
            {
                List<Joueur> joueurs = new List<Joueur>();

                foreach (Open o in TsLesOpens)
                    joueurs.AddRange(o.TsLesJoueurs);

                return joueurs;
            }
        }



        /// <summary>
        /// Vrai si on envoie une confirmation d'inscription par mail à l'arbitre.
        /// </summary>
        public bool ConfirmMail { get; set; } = false;










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
        public Tournoi() { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="reference">Référence du tournoi.</param>
        /// <param name="nom">Nom du tournoi.</param>
        /// <param name="dateDebut">Date de début du tournoi.</param>
        /// <param name="dateFin">Date de fin du tournoi.</param>
        /// <param name="lieuRef">Référence du lieu dans lequel se déroule le tournoi.</param>
        public Tournoi(int reference, string nom, DateTime dateDebut, DateTime dateFin, int lieuRef)
        {
            this.Ref = reference;
            this.Nom = nom;
            this.DateDebut = dateDebut;
            this.DateFin = dateFin;
            this.Lieu = GLieu.GetLieu(lieuRef);
            this.Etat = GTournoi.ETAT__CREATION;
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
        /// Ajoute un open au tournoi.
        /// </summary>
        /// <param name="reference">Référence de l'open à ajouter.</param>
        public void AjouterOpen(int reference)
        {
            TsLesOpens.Add(GOpen.GetOpen(reference));
        }

        /// <summary>
        /// Ajoute un open au tournoi.
        /// </summary>
        /// <param name="open">Open à ajouter.</param>
        public void AjouterOpen(Open open)
        {
            TsLesOpens.Add(open);
        }

        /// <summary>
        /// Supprime un open du tournoi.
        /// </summary>
        /// <param name="reference">Référence de l'open à supprimer.</param>
        public void SupprimerOpen(int reference)
        {
            TsLesOpens.Remove(GOpen.GetOpen(reference));
        }

        /// <summary>
        /// Supprime un open du tournoi.
        /// </summary>
        /// <param name="open">Open à supprimer.</param>
        public void SupprimerOpen(Open open)
        {
            TsLesOpens.Remove(open);
        }

        /// <summary>
        /// Ajoute un repas au tournoi.
        /// </summary>
        /// <param name="reference">Référence du repas à ajouter.</param>
        public void AjouterRepas(int reference)
        {
            TsLesRepas.Add(GRepas.GetRepas(reference));
        }

        /// <summary>
        /// Ajoute un repas au tournoi.
        /// </summary>
        /// <param name="repas">Repas à ajouter.</param>
        public void AjouterRepas(Repas repas)
        {
            TsLesRepas.Add(repas);
        }

        /// <summary>
        /// Supprime un repas du tournoi.
        /// </summary>
        /// <param name="reference">Référence du repas à supprimer.</param>
        public void SupprimerRepas(int reference)
        {
            TsLesRepas.Remove(GRepas.GetRepas(reference));
        }

        /// <summary>
        /// Supprime un repas du tournoi.
        /// </summary>
        /// <param name="repas">Repas à supprimer.</param>
        public void SupprimerRepas(Repas repas)
        {
            TsLesRepas.Remove(repas);
        }


        /// <summary>
        /// Retourne un texte représentant l'objet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Ref + " : " + Nom + " (" + DateDebut.ToShortDateString() + " à " + DateFin.ToShortDateString() + ") à " + Lieu.Nom;
        }
    }
}
