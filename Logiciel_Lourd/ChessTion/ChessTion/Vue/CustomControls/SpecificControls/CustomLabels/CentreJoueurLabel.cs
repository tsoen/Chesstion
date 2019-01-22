using System;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.SpecificControls.CustomMenus;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomLabels
{
    class CentreJoueurLabel : JoueurLabel
    {
        private bool selected = false;
        public event EventHandler JustSelected;
        public event EventHandler JustUnselected;

        public bool DisplayErrored
        {
            set
            {
                if (value)
                    ForeColor = Theme.Style.CentreBodyErroredJoueursForeColor;
                else
                    ForeColor = Theme.Style.CentreBodyJoueursForeColor;
            }
            get { return ForeColor == Theme.Style.CentreBodyErroredJoueursForeColor; }
        }
        public bool DisplayConfirmed
        {
            set
            {
                if (value)
                    this.ForeColor = Theme.Style.CentreBodyConfirmedJoueursForeColor;
            }
            get { return ForeColor == Theme.Style.CentreBodyConfirmedJoueursForeColor; }

        }
        public bool DisplayNormal
        {
            set
            {
                if (value)
                    this.ForeColor = Theme.Style.CentreBodyJoueursForeColor;
            }
            get { return ForeColor == Theme.Style.CentreBodyJoueursForeColor; }

        }

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (selected)
                    JustSelected?.Invoke(this, new EventArgs());
                else
                    JustUnselected?.Invoke(this, new EventArgs());
            }
        }

        public CentreJoueurLabel(Joueur j) : this(j.Nom.ToUpper() + " " + j.Prenom.Substring(0, 1).ToUpper() + j.Prenom.Substring(1), j.Ref, false, j.Confirme) { }
        public CentreJoueurLabel(string nom, int reference, bool errored = false, bool confirmed = false) : base(nom, reference)
        {
            if (errored)
                DisplayErrored = true;
            else if (confirmed)
                DisplayConfirmed = true;
            else
                DisplayNormal = true;

            Font = Theme.Style.CentreBodyJoueursFont;
            Cursor = Cursors.Hand;
            ContextMenu = new JoueurContextMenu(Ref);

            ToolTip tt = new ToolTip();
            tt.InitialDelay = 500;
            tt.AutoPopDelay = 3000;
            tt.SetToolTip(this, "Réf : " + this.Ref);

            MouseUp += CentreJoueurLabel_MouseUp;
            JustSelected += CentreJoueurLabel_JustSelected;
            JustUnselected += CentreJoueurLabel_JustUnselected;
        }

        private void CentreJoueurLabel_JustUnselected(object sender, EventArgs e)
        {
            Font = Theme.Style.CentreBodyJoueursFont;
        }
        private void CentreJoueurLabel_JustSelected(object sender, EventArgs e)
        {
            Font = Theme.Style.CentreBodySelectedJoueursFont;
           
            CChesstion.SelectionnerJoueur(this.Ref);
            
        }
        private void CentreJoueurLabel_MouseUp(object sender, MouseEventArgs e)
        {
            CentreJoueurLabel cjl = (CentreJoueurLabel)sender;

            if (!cjl.ClientRectangle.Contains(cjl.PointToClient(Control.MousePosition)))
                return;

            if (e.Button == MouseButtons.Left)
                Selected = true;
        }
    }
}
