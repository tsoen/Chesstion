using System.Collections.Generic;
using System.Linq;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomComboBoxes
{
    /// <summary>
    /// Classe vue gérant une combo box qui gère des <see cref="Club"/>.
    /// </summary>
    class ClubComboBox : CustomComboBox
    {
        /// <summary>
        /// Affecte le data source.
        /// </summary>
        /// <param name="club">Clubs à afficher dans la combo box.</param>
        /// <param name="displayMember">Attribut à afficher.</param>
        /// <param name="valueMember">Attribut de valeur.</param>
        public void SetDataSource(List<Modele.MTournoi.Club> club, string displayMember, string valueMember)
        {
            Loading = true;
            object selectedValue = SelectedValue;
            this.DataSource = null;
            this.DataSource = club.OrderBy(o => o.Nom).ToList(); ;
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            try
            {
                SelectedValue = selectedValue;
            }
            catch { }

            Loading = false;
        }
    }
}
