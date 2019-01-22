using System;
using System.Linq;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    class RepasTextBox : HiddenTextBox
    {
        public event EventHandler Entered;

        public int Ref
        {
            get
            {
                return (int)Tag;
            }
            set { Tag = value; }
        }

        public float Prix
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.LastValue) || this.LastValue == PlaceholderText)
                    return -1f;

                string[] splits = this.LastValue.Trim().Split(' ');
                string prix = splits[splits.Count() - 2];
                return float.Parse(prix.Replace(".", ","));
            }
        }
        public string Nom
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.LastValue) || this.LastValue == PlaceholderText)
                    return string.Empty;

                string[] splits = this.LastValue.Trim().Split(' ');
                string nom = "";
                for (int i = 0; i < splits.Count() - 2; i++)
                    nom += splits[i] + " ";

                return nom.Trim();
            }
        }

        public RepasTextBox() : base()
        {
            PlaceholderText = "Repas 0 €";
            Regex = new System.Text.RegularExpressions.Regex(@"(([a-zA-Z0-9àâêéëèïüöçÀÂÊÉËÈÏÜÖÇ]| ))+ [0-9](,|.)?([0-9]*( €)?)$");
            MaxLength = 45;

            AllowedCharacters = new System.Collections.Generic.List<char>() {'\''};
            AllowControls = true;
            AllowDecimal = true;
            AllowEmpty = false;
            AllowEuro = true;
            AllowLetters = true;
            AllowNumbers = true;
            AllowSpace = true;
            

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = Theme.Style.RepasTextBoxesBackColor;
            ForeColor = Theme.Style.RepasTextBoxesForeColor;
            Font = Theme.Style.RepasTextBoxesFont;
            ToolTip t = new ToolTip();
            t.SetToolTip(this, this.Text);
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            if (!LastValue.EndsWith(" €"))
            {
                this.LastValue = this.Text = this.LastValue.Replace("€", "");
                this.LastValue += " €";
                this.Text += " €";
            }

            CChesstion.ChangerInfoRepas(Ref, Nom, Prix);
            ToolTip t = new ToolTip();
            t.SetToolTip(this, this.Text);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Entered?.Invoke(this, new EventArgs());
        }
    }
}
