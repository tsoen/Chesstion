using System;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    class TournoiTextBox : CustomHiddenTextBox
    {
        public override int Ref
        {
            get
            {
                return base.Ref;
            }

            set
            {
                base.Ref = value;
                this.NewValue = GTournoi.GetTournoi(Ref).GetType().GetProperty(Info).GetValue(GTournoi.GetTournoi(Ref)).ToString();
            }
        }

        public TournoiTextBox()
        {
            PlaceholdingColor = Theme.Style.OpenBoxesPlaceHolderForeColor;
            BackColor = Theme.Style.OpenBoxesBackColor;
            ForeColor = Theme.Style.OpenBoxesForeColor;
            NormalColor = ForeColor;
            ErrorBackColor = Theme.Style.OpenBoxesErrorBackColor;
            Font = Theme.Style.OpenBoxesFont;
            NormalFont = Font;
            PlaceholdingFont = Theme.Style.OpenBoxesPlaceHolderFont;
            TabStop = false;
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            object value = LastValue;

            if (CChesstion.TournoiSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.TournoiSelectionne) is int)
                value = int.Parse(LastValue);
            else if (CChesstion.TournoiSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.TournoiSelectionne) is float)
                value = float.Parse(LastValue);
            else if (CChesstion.TournoiSelectionne.GetType().GetProperty(Info).GetValue(CChesstion.TournoiSelectionne) is DateTime)
                value = Convert.ToDateTime(LastValue);

            CChesstion.TournoiSelectionne.GetType().GetProperty(Info).SetValue(CChesstion.TournoiSelectionne, value);
        }
    }
}
