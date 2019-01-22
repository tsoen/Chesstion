using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes
{
    /// <summary>
    /// Classe vue gérant une combo box qui gère des <see cref="Open"/>.
    /// </summary>
    class OpenComboBox : CustomComboBox
    {
        /// <summary>
        /// Tool tip de la combo box.
        /// </summary>
        protected override ToolTip ToolTip
        {
            get { return null; }
        }

        /// <summary>
        /// Affecte le data source.
        /// </summary>
        /// <param name="open">Opens à afficher dans la combo box.</param>
        /// <param name="displayMember">Attribut à afficher.</param>
        /// <param name="valueMember">Attribut de valeur.</param>
        public void SetDataSource(List<Open> open, string displayMember, string valueMember)
        {
            object o = null;
            try
            {
                o = SelectedValue;
            } catch (IndexOutOfRangeException) { }
            this.DataSource = null;
            this.DataSource = open;
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            try
            {
                SelectedValue = o;
            } catch { }
        }

        /// <summary>
        /// Quand la valeur change, vérifie si l'elo max de l'open est bien supérieur à l'élo du joueur.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedValueChanged(EventArgs e)
        {
            if (CChesstion.JoueurSelectionne == null || SelectedValue == null || CChesstion.JoueurSelectionne.OpenRef == int.Parse(SelectedValue?.ToString() ?? "-1"))
                return;

            try
            {
                CChesstion.ChangerOpenDuJoueur(CChesstion.JoueurSelectionne.Ref, int.Parse(SelectedValue.ToString()));
            }
            catch (ArgumentException ae) // Elo > EloMax
            {
                CustomQuickDialog cqd = new CustomQuickDialog(
                    ae.Message,
                    GeneralControls.CustomDialogs.QuickDialogType.Error,
                    this,
                    GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOnRightOfParent
                );

                cqd.DisplayDelay = 2000;
                cqd.Show();

                SelectedValue = CChesstion.JoueurSelectionne.OpenRef;
            }
        }
    }
}