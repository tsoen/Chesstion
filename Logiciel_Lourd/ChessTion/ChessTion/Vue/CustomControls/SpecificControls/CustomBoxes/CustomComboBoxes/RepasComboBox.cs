using System;
using System.Collections.Generic;
using ChessTion.Controleur;
using ChessTion.Modele.MRepas;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes
{
    /// <summary>
    /// Classe vue gérant une combo box qui gère des <see cref="Repas"/>.
    /// </summary>
    class RepasComboBox : CustomComboBox
    {
        /// <summary>
        /// Affecte le data source.
        /// </summary>
        /// <param name="repas">Repas à afficher dans la combo box.</param>
        /// <param name="displayMember">Attribut à afficher.</param>
        /// <param name="valueMember">Attribut de valeur.</param>
        public void SetDataSource(List<Repas> repas, string displayMember, string valueMember)
        {
            object o = SelectedValue;
            this.DataSource = null;
            this.DataSource = repas;
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            try
            {
                SelectedValue = o;
            }
            catch { }

        }

        /// <summary>
        /// Quand la valeur change, met à jour la valeur de À Payer de <see cref="JoueurPanel"/>.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);
            CChesstion.JoueurPanel.UpdateTotalAPayer();
        }
    }
}
