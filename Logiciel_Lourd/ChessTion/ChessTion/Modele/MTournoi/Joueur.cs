using System;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.CRepas;
using ChessTion.Modele.MRepas;

namespace ChessTion.Modele.MTournoi
{
    /// <summary>
    /// Classe métier gérant les joueurs.
    /// </summary>
    class Joueur
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
        /// Référence du joueur.
        /// </summary>
        public int Ref { get; set; }

        /// <summary>
        /// Numéro FFE du joueur.
        /// </summary>
        public string NrFFE { get; set; }

        /// <summary>
        /// Nom du joueur.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Prénom du joueur.
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// Sexe du joueur.
        /// </summary>
        public string Sexe { get; set; }

        /// <summary>
        /// Date de naissance du joueur.
        /// </summary>
        public string NeLe { get { return this._NeLe; } set { this._NeLe = value; if (!this.FraisOverridden) { CalcFraisInscription(); } } }

        /// <summary>
        /// Catégorie du joueur.
        /// </summary>
        public string Cat { get; set; }

        /// <summary>
        /// Fédération du joueur.
        /// </summary>
        public string Federation { get; set; }

        /// <summary>
        /// Référence du <see cref="Club"/> du joueur.
        /// </summary>
        public int ClubRef { get; set; }

        /// <summary>
        /// Elo du joueur.
        /// </summary>
        public int Elo { get; set; }

        /// <summary>
        /// Rapide du joueur.
        /// </summary>
        public int Rapide { get; set; }

        /// <summary>
        /// Fide du joueur.
        /// </summary>
        public string Fide { get; set; }

        /// <summary>
        /// Code fide du joueur.
        /// </summary>
        public string FideCode { get; set; }

        /// <summary>
        /// Titre fide du joueur.
        /// </summary>
        public string FideTitre { get; set; }

        /// <summary>
        /// AffType du joueur.
        /// </summary>
        public string AffType { get; set; }

        /// <summary>
        /// Actif du joueur.
        /// </summary>
        public string Actif { get; set; }

        /// <summary>
        /// Référence de l'<see cref="Open"/> du joueur.
        /// </summary>
        public int OpenRef{
            get
            {
                return GOpen.GetOpenDuJoueur(this.Ref).Ref;
            } set
            {
                if (GOpen.GetOpenDuJoueur(this.Ref) != null)
                    GOpen.GetOpenDuJoueur(this.Ref).SupprimerJoueur(this.Ref);
                GOpen.GetOpen(value).AjouterJoueur(this);
            }
        }

        /// <summary>
        /// Email du joueur.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Vrai si le joueur veut recevoir les mails.
        /// </summary>
        public bool Subscribe { get; set; }

        /// <summary>
        /// Numéro de téléphone du joueur.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Référence du <see cref="Repas"/> du joueur.
        /// </summary>
        public int RepasRef { get; set; }

        /// <summary>
        /// Vrai si la participation du joueur est confirmée.
        /// </summary>
        public bool Confirme { get; set; }

        /// <summary>
        /// Frais d'inscriptions du joueur.
        /// </summary>
        public float FraisInscription { get; set; }

        /// <summary>
        /// Date de naissance du joueur.
        /// </summary>
        private string _NeLe;

        /// <summary>
        /// Vrai si les frais d'inscriptions du joueur 
        /// </summary>
        private bool FraisOverridden { get; set; } = false; //set par setFraisManually();










        /*************************************************************************************
         *   ___  _____  _  _  ___  ____  ____  __  __   ___  ____  ____  __  __  ____  ___  *
         *  / __)(  _  )( \( )/ __)(_  _)(  _ \(  )(  ) / __)(_  _)( ___)(  )(  )(  _ \/ __) *
         * ( (__  )(_)(  )  ( \__ \  )(   )   / )(__)( ( (__   )(   )__)  )(__)(  )   /\__ \ *
         *  \___)(_____)(_)\_)(___/ (__) (_)\_)(______) \___) (__) (____)(______)(_)\_)(___/ *
         *                                                                                   *
         *                      Ensemble des constructeurs de la classe.                     *
         *                                                                                   *
         *************************************************************************************/

        public Joueur() {  }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="reference">Référence du joueur.</param>
        /// <param name="nrFFE">Numéro FFE du joueur.</param>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="prenom">Prénom du joueur.</param>
        /// <param name="sexe">Sexe du joueur.</param>
        /// <param name="neLe">Date de naissance du joueur.</param>
        /// <param name="cat">Catégorie du joueur.</param>
        /// <param name="federation">Fédération du joueur.</param>
        /// <param name="clubRef">Référence du <see cref="Club"/> du joueur.</param>
        /// <param name="elo">Elo du joueur.</param>
        /// <param name="rapide">Rapide du joueur.</param>
        /// <param name="fide">Fide du joueur.</param>
        /// <param name="fideCode">Code fide du joueur.</param>
        /// <param name="fideTitre">Titre fide du joueur.</param>
        /// <param name="affType">AffType du joueur.</param>
        /// <param name="actif">Actif du joueur.</param>
        /// <param name="email">Eamil du joueur.</param>
        /// <param name="subscribe">Vrai si le joueur veut recevoir les mails.</param>
        /// <param name="phone">Numéro de téléphone du joueur.</param>
        /// <param name="refRepas">Référence du <see cref="Repas"/> du joueur.</param>
        /// <param name="confirme">Vrai si la participation du joueur est confirmée.</param>
        public Joueur(int reference, string nrFFE, string nom, string prenom, string sexe, string neLe, string cat, string federation,
            int clubRef, int elo, int rapide, string fide, string fideCode, string fideTitre, string affType,
            string actif, string email, bool subscribe, string phone, int refRepas, bool confirme = false)
        {
            this.Ref = reference;
            this.NrFFE = nrFFE;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Sexe = sexe;
            this.NeLe = neLe;
            this.Cat = cat;
            this.Federation = federation;
            this.ClubRef = clubRef;
            this.Elo = elo;
            this.Rapide = rapide;
            this.Fide = fide;
            this.FideCode = fideCode;
            this.FideTitre = fideTitre;
            this.AffType = affType;
            this.Actif = actif;
            this.Email = email;
            this.Subscribe = subscribe;
            this.Phone = phone;
            this.RepasRef = refRepas;
            this.Confirme = confirme;
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
        /// Retourne la somme que le joueur doit payer.
        /// </summary>
        /// <returns>Somme que le joueur doit payer</returns>
        public float TotalAPayer()
        {
            float repas;

            CalcFraisInscription();
                    
            repas = GRepas.GetRepas(this.RepasRef).Prix;

            return this.FraisInscription + repas;
        }

        /// <summary>
        /// Modifie les frais d'inscription en overidant le calcul automatique.
        /// </summary>
        /// <param name="frais">Nouveaux frais d'inscriptions</param>
        public void SetFraisManually(float frais)
        {
            this.FraisInscription = frais;
            this.FraisOverridden = true;
        }

        /// <summary>
        /// Calcul les frais d'inscriptions, basé sur l'âge, le tournoi, etc.
        /// </summary>
        private void CalcFraisInscription()
        {
            if (this.FraisOverridden)
                return;

            Tournoi t;
            try
            {
                t = GTournoi.GetTournoiDeOpen(GOpen.GetOpenDuJoueur(this.Ref).Ref);
            }
            catch(Exception)
            {
                t = new Tournoi();
                // ignored
            }        

            if (this.FideTitre != "MF" && this.FideTitre != "MI" && this.FideTitre != "GMI")
            {
                if ((DateTime.Now.Subtract(Convert.ToDateTime(this.NeLe)).Days) / 365.25 < t.LimiteAge)
                {
                    this.FraisInscription = t.PrixJeune;
                }
                else
                {
                    this.FraisInscription = t.PrixVieux;
                }
            }
            else
            {
                this.FraisInscription = 0;
            }
        }

        /// <summary>
        /// Retourne un texte représentant l'objet.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Ref + " (open " + this.OpenRef + " club " + this.ClubRef + ") " + this.Nom + " " + this.Prenom + " " + this.Elo + " " + this.NrFFE
                + " " + this.Fide + " " + this.Email;
        }

        /// <summary>
        /// Compare deux objets.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Vrai si les objets sont égaux.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Joueur && this.Ref == ((Joueur)obj).Ref)
                return true;
            return false;
        }

    }
}
