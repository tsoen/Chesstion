using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs
{
    /// <summary>
    /// Classe vue gérant une pop-up d'ajout de <see cref="Club"/> depuis son nom.
    /// </summary>
    class AddClubFromNameDialog : SingleInputWithComboAnswerDialog
    {

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
        public AddClubFromNameDialog() : base("Ajouter un club", "Entrez au moins trois lettres du nom du club")
        {
            OKButton.Click += OKButton_Click;
            CancelButton.Click += CancelButton_Click;
            TextBox.TextChanged += TextBox_TextChanged;

            AnswerLabel.Text = "Clubs trouvés";
            AnswerComboBox.Enabled = false;
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
        /// Si le nombre de caractères entré est supérieur à 3, recherche dans la base FFE les clubs correspondants.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (TextBox.Text.Length < 3)
            {
                AnswerComboBox.Enabled = false;
                AnswerComboBox.DataSource = null;
                AnswerComboBox.Items.Clear();
                AnswerComboBox.Items.Add("Entrez votre recherche...");
                AnswerComboBox.SelectedIndex = 0;
                OKButton.Enabled = false;
                return;
            }
            TextBox.ReadOnly = true;

            try
            {
                OleDbConnection connec =
                    new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath +
                                        @"\Ressources\DATA.mdb");
                OleDbCommand command =
                    new OleDbCommand(
                        "SELECT Ref, Nom FROM CLUB WHERE LCase(Nom) LIKE '%" + TextBox.Text.ToLower().Replace("'", "''") +
                        "%'", connec);
                //MessageBox.Show(command.CommandText);
                Dictionary<int, string> data = new Dictionary<int, string>();
                connec.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(reader.GetInt32(0), reader.GetString(1));
                }
                connec.Close();

                if (data.Count == 0)
                {
                    AnswerComboBox.DataSource = null;
                    AnswerComboBox.Enabled = false;
                    AnswerComboBox.Items.Add("Aucun résultat");
                    AnswerComboBox.SelectedIndex = 0;
                    OKButton.Enabled = false;
                    TextBox.ReadOnly = false;
                    OKButton.Enabled = false;
                    return;
                }

                AnswerComboBox.DataSource = new BindingSource(data, null);
                AnswerComboBox.DisplayMember = "Value";
                AnswerComboBox.ValueMember = "Key";

                AnswerComboBox.Enabled = true;
                TextBox.ReadOnly = false;
                OKButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : \n\n" + ex.Message);
                Close();
            }
        }

        /// <summary>
        /// Ferme la pop-up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Ajoute le club choisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (!(int.Parse(AnswerComboBox.SelectedValue.ToString()) > 0 && AnswerComboBox.Enabled))
                return;

            try
            {
                CChesstion.CreerClub(int.Parse(AnswerComboBox.SelectedValue.ToString()), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            DialogResult = DialogResult.OK;
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

            this.AnswerComboBox.BackColor = Theme.Style.DialogTextBoxBackColor;
            this.AnswerComboBox.ForeColor = Theme.Style.DialogTextBoxForeColor;
            this.AnswerComboBox.Font = Theme.Style.DialogTextBoxFont;

            this.OKButton.BackColor = Theme.Style.DialogMainButtonsBackColor;
            this.OKButton.ForeColor = Theme.Style.DialogMainButtonsForeColor;
            this.OKButton.Font = Theme.Style.DialogMainButtonsFont;

            this.CancelButton.BackColor = Theme.Style.DialogSecondaryButtonsBackColor;
            this.CancelButton.ForeColor = Theme.Style.DialogSecondaryButtonsForeColor;
            this.CancelButton.Font = Theme.Style.DialogSecondaryButtonsFont;
        }
    }
}
