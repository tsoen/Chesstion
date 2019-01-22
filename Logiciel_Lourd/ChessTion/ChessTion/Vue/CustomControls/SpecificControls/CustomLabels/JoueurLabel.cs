using System.Windows.Forms;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomLabels
{
    class JoueurLabel : Label
    {
        public int Ref
        {
            get { return (int) this.Tag; }
            set { this.Tag = value; }
        }
        public string Nom
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public JoueurLabel(string nom, int reference) : base()
        {
            this.AutoSize = true;

            this.Nom = nom;
            this.Ref = reference;
        }

        public JoueurLabel(Joueur j) : this(
            j.Nom.ToUpper() + " " + j.Prenom.Substring(0, 1).ToUpper() + j.Prenom.Substring(1),
            j.Ref
        ) { }
    }
}
