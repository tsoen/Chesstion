using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    class ActionPanel : HeadedPanel, IChesstionPanel
    {
        private Image toDo;
        private Image done;

        protected Button OuvrirInscriptionsButton { get; } = new Button();
        protected Button FermerInscriptionsButton { get; } = new Button();
        protected Button DebuterTournoiButton { get; } = new Button();
        protected Button TerminerTournoiButton { get; } = new Button();

        protected Label ProgressArrow1 { get; } = new Label();
        protected Label ProgressArrow2 { get; } = new Label();
        protected Label ProgressArrow3 { get; } = new Label();

        public bool EnableOuvrirInscriptions { get { return OuvrirInscriptionsButton.Enabled; } set
        {
            OuvrirInscriptionsButton.Enabled = value;
        } }
        public bool EnableFermerInscriptions
        {
            get { return FermerInscriptionsButton.Enabled; }
            set
            {
                FermerInscriptionsButton.Enabled = value;
            }
        }
        public bool EnableDebuterTournoi
        {
            get { return DebuterTournoiButton.Enabled; }
            set
            {
                DebuterTournoiButton.Enabled = value;
            }
        }
        public bool EnableTerminerTournoi
        {
            get { return TerminerTournoiButton.Enabled; }
            set
            {
                TerminerTournoiButton.Enabled = value;
            }
        }

        public void SetProgressed(int arrow, bool progressed = true)
        {
            Image i = progressed ? done : toDo;
            switch (arrow)
            {
                case -1:
                    SetProgressed(1, progressed);
                    SetProgressed(2, progressed);
                    SetProgressed(3, progressed);
                    break;
                case 1:
                    ProgressArrow1.Image = i;
                    break;
                case 2:
                    ProgressArrow2.Image = i;
                    break;
                case 3:
                    ProgressArrow3.Image = i;
                    break;
            }
        }


        public void Init()
        {
            CreateArrows();
            CreateButtons();
            CreatePanel();
        }

        private void CreateArrows()
        {
            toDo = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/ProgressRightArrow.png");
            done = Image.FromFile(CChesstion.CurrentThemeIconsFolder + "/ProgressedRightArrow.png");
        }

        private void CreateButtons()
        {
            Controls.Add(OuvrirInscriptionsButton);
            Controls.Add(FermerInscriptionsButton);
            Controls.Add(DebuterTournoiButton);
            Controls.Add(TerminerTournoiButton);

            Controls.Add(ProgressArrow1);
            Controls.Add(ProgressArrow2);
            Controls.Add(ProgressArrow3);

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button b = (Button) c;
                    b.BackColor = Theme.Style.ActionsButtonsBackColor;
                    b.ForeColor = Theme.Style.ActionsButtonsForeColor;
                    b.Font = Theme.Style.ActionsButtonsFont;
                    b.Enabled = false;
                    b.FlatStyle = FlatStyle.Flat;
                    b.Size = DPI.Instance.MultipliedSize(170, 45);
                    b.Click += (object sender, EventArgs args) =>
                    {
                        CChesstion.TournoiProchainEtat();
                    };
                } else if (c is Label && !c.Equals(header))
                {
                    Label l = (Label)c;
                    l.AutoSize = false;
                    l.Image = toDo;
                    l.Size = l.Image.Size;
                    l.BackColor = Color.Transparent;
                }

            }

            OuvrirInscriptionsButton.Text = "Ouvrir les inscriptions";
            FermerInscriptionsButton.Text = "Fermer les inscriptions";
            DebuterTournoiButton.Text = "Débuter le tournoi";
            TerminerTournoiButton.Text = "Terminer le tournoi";
        }

        private void CreatePanel()
        {
            BackColor = Theme.Style.ActionsBodyBackColor;
            HeaderBackColor = Theme.Style.ActionsHeaderBackColor;
            HeaderForeColor = Theme.Style.ActionsHeaderForeColor;
            HeaderFont = Theme.Style.ActionsHeaderFont;
            HeaderHeight = (int)(DPI.Instance.RelativeMultiplier.Y * Theme.Style.ActionsHeaderHeight);

            RelocateAndResize();

        }

        public void RelocateAndResize()
        {
            int margin = (int)(DPI.Instance.RelativeMultiplier.X * 6);

            Location = new Point(CChesstion.RepasPanel.Right + 2, Parent.ClientSize.Height - Size.Height);
            Size = new Size(CChesstion.OpenPanel.Location.X - Location.X - 2, (int)(DPI.Instance.RelativeMultiplier.Y*68));

            int totalSize = 4*OuvrirInscriptionsButton.Width + 3*ProgressArrow1.Width + 6*margin;
            int startPoint = (Width - totalSize)/2;

            OuvrirInscriptionsButton.Location = new Point(startPoint, HeaderHeight + (Height - OuvrirInscriptionsButton.Height)/2);
            ProgressArrow1.Location = new Point(OuvrirInscriptionsButton.Right + margin, HeaderHeight + (Height - ProgressArrow1.Height) / 2);
            FermerInscriptionsButton.Location = new Point(ProgressArrow1.Right + margin, HeaderHeight + (Height - FermerInscriptionsButton.Height) / 2);
            ProgressArrow2.Location = new Point(FermerInscriptionsButton.Right + margin, HeaderHeight + (Height - ProgressArrow2.Height) / 2);
            DebuterTournoiButton.Location = new Point(ProgressArrow2.Right + margin, HeaderHeight + (Height - DebuterTournoiButton.Height) / 2);
            ProgressArrow3.Location = new Point(DebuterTournoiButton.Right + margin, HeaderHeight + (Height - ProgressArrow3.Height) / 2);
            TerminerTournoiButton.Location = new Point(ProgressArrow3.Right + margin, HeaderHeight + (Height - TerminerTournoiButton.Height) / 2);
        }

        public void EnableAll(bool value = true)
        {
            EnableOuvrirInscriptions = value;
            EnableFermerInscriptions = value;
            EnableDebuterTournoi = value;
            EnableTerminerTournoi = value;
        }
    }
}
