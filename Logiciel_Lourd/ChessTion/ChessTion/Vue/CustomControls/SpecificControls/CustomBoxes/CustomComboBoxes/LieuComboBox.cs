using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChessTion.Controleur.CLieu;
using ChessTion.Modele.MLieu;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes
{
    /// <summary>
    /// Classe vue gérant une combo box qui gère des <see cref="Lieu"/>.
    /// </summary>
    class LieuComboBox : CustomComboBox
    {
        /// <summary>
        /// Affecte le data source.
        /// </summary>
        /// <param name="lieu">Lieux à afficher dans la combo box.</param>
        /// <param name="displayMember">Attribut à afficher.</param>
        /// <param name="valueMember">Attribut de valeur.</param>
        public void SetDataSource(List<Modele.MLieu.Lieu> lieu, string displayMember, string valueMember)
        {
            object selected = SelectedValue;
            this.DataSource = null;
            this.DataSource = lieu.OrderBy(o => o.Nom).ToList(); ;
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            try
            {
                SelectedValue = selected;
            }
            catch { }
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            try
            {
                base.OnSelectedValueChanged(e);

                if (this.SelectedValue != null && GLieu.GetLieu(int.Parse(this.SelectedValue.ToString())) != null)
                {
                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(this.label, GLieu.GetLieu(int.Parse(this.SelectedValue.ToString())).ToString());
                }
            } catch { }

        }
    }
}
