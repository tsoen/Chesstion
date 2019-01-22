using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up d'ajout de <see cref="Joueur"/> depuis son numéro FFE.
    /// </summary>
    class AddPlayerFromFFEDialog : SingleInputWithAnswerDialog
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
        /// Numéro FFE du joueur trouvé.
        /// </summary>
        private string nrFFEjoueurTrouve = string.Empty;










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
        public AddPlayerFromFFEDialog() : base("Ajouter un joueur", "Veuillez entrer le numéro FFE du joueur")
        {
            OKButton.Click += BtnValider_Click;
            CancelButton.Click += BtnAnnuler_Click;
            TextBox.TextChanged += TxtInput_Validated;

            OKButton.Enabled = false;
            TextBox.MaxLength = 6;
            TextBox.Regex = new Regex("[a-zA-Z][0-9]{5}");
        }











        /*****************************************************************
         *  ____  _  _  ____  _  _  ____  __  __  ____  _  _  ____  ___  *
         * ( ___)( \/ )( ___)( \( )( ___)(  \/  )( ___)( \( )(_  _)/ __) *
         *  )__)  \  /  )__)  )  (  )__)  )    (  )__)  )  (   )(  \__ \ *
         * (____)  \/  (____)(_)\_)(____)(_/\/\_)(____)(_)\_) (__) (___/ *
         *                                                               *
         *        Ensemble des évènements gérés par la classe.           *
         *                                                               *
         *****************************************************************/

        /// <summary>
        /// Cherche le joueur en base depuis le numéro FFE entré.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtInput_Validated(object sender, EventArgs e)
        {
            if (!TextBox.Regex.IsMatch(TextBox.Text))
            {
                AnswerTextBox.Text = string.Empty;                
                return;
            }

            AnswerTextBox.Text = "Recherche...";

            TextBox.ReadOnly = true;
            OKButton.Enabled = false;

            string FFE = TextBox.Text.ToUpper();

            try
            {
                OleDbConnection connec =
                    new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath +
                                        @"\Ressources\DATA.mdb");
                OleDbCommand command =
                    new OleDbCommand("SELECT Nom, Prenom, NeLe FROM JOUEUR WHERE NrFFE = '" + FFE + "'", connec);
                List<object> data = new List<object>();
                connec.Open();
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                data.Add(reader.GetString(0));
                data.Add(reader.GetString(1));
                data.Add(reader.GetDateTime(2));
                connec.Close();

                AnswerTextBox.Text = data[0] + " " + data[1] + " (" + ((DateTime) data[2]).ToShortDateString() + ")";
                nrFFEjoueurTrouve = FFE;
                OKButton.Enabled = true;
                TextBox.ReadOnly = false;
                this.ActiveControl = OKButton;
            }
            catch 
            {
                AnswerTextBox.Text = "Joueur non trouvé";
                OKButton.Enabled = false;
                nrFFEjoueurTrouve = string.Empty;
                TextBox.ReadOnly = false;
            }
        }

        /// <summary>
        /// Ajoute le joueur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Ferme la pop-up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (nrFFEjoueurTrouve.Equals(string.Empty))
                return;

            DialogResult = DialogResult.OK;

            int reference = CChesstion.CreerJoueur(nrFFEjoueurTrouve, true).Ref;
            CChesstion.JoueurErrorsCache.Remove(reference);
            //CChesstion.UpdateTournoiEtat(true);
            Close();
            
        }

        /// <summary>
        /// Applique le thème aux controls.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Theme.Style.DialogBodyBackColor;
            this.ForeColor = Theme.Style.DialogBodyForeColor;
            this.Font = Theme.Style.DialogBodyFont;

            this.TextBox.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.TextBox.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.TextBox.Font = Theme.Style.DialogTextBoxFont;

            this.Label.ForeColor = Theme.Style.DialogBodyForeColor;
            this.Label.Font = Theme.Style.DialogBodyFont;

            this.AnswerLabel.ForeColor = Theme.Style.DialogBodyForeColor;
            this.AnswerLabel.Font = Theme.Style.DialogBodyFont;

            this.AnswerTextBox.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.AnswerTextBox.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.AnswerTextBox.Font = Theme.Style.DialogTextBoxFont;

            this.OKButton.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.OKButton.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.OKButton.Font = Theme.Style.DialogMainButtonsFont;

            this.CancelButton.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.CancelButton.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.CancelButton.Font = Theme.Style.DialogSecondaryButtonsFont;

        }

    }
}
