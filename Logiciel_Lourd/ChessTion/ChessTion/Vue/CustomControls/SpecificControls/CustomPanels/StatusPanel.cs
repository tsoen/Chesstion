using System;
using System.Drawing;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomButtons;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    class StatusPanel : Panel
    {
        protected Action LeftButtonAction { get; set; } = new Action(() => { });
        protected Action MiddleButtonAction { get; set; } = new Action(() => { });
        protected Action RightButtonAction { get; set; } = new Action(() => { });

        protected Button LeftButton { get; } = new Button();
        protected Button MiddleButton { get; } = new Button();
        protected Button RightButton { get; } = new Button();

        protected Panel MessagePanel { get; } = new Panel();
        protected Panel ButtonsPanel { get; } = new Panel();

        protected Label TitleLabel { get; } = new Label();
        protected Label MessageLabel { get; } = new Label();
        protected Label TipLabel { get; } = new Label();

        protected NoBorderButton CloseButton { get; } = new NoBorderButton();

        public static readonly int LEFT_BUTTON = 1;
        public static readonly int MIDDLE_BUTTON = 2;
        public static readonly int RIGHT_BUTTON = 3;

        public string Title { get { return TitleLabel.Text; } set { TitleLabel.Text = value; } }
        public string Message { get { return MessageLabel.Text; } set { MessageLabel.Text = value; } }
        public string Tip { get { return TipLabel.Text; } set { TipLabel.Text = value; } }
        public bool Closable { get { return CloseButton.Enabled; } set { CloseButton.Enabled = value; } }

        public void Init()
        {
            this.Visible = false;
            this.BackColor = Theme.Style.StatusBackColor;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(CloseButton);
            this.Controls.Add(TitleLabel);
            this.Controls.Add(MessagePanel);
            this.Controls.Add(ButtonsPanel);
            this.Controls.Add(TipLabel);

            // Title
            TitleLabel.AutoSize = false;
            TitleLabel.ForeColor = Theme.Style.StatusTitleForeColor;
            TitleLabel.BackColor = Color.Transparent;
            TitleLabel.Font = Theme.Style.StatusTitleFont;
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Message
            MessagePanel.AutoSize = false;
            MessagePanel.AutoScroll = true;
            MessagePanel.BackColor = Color.Transparent;
            MessagePanel.Controls.Add(MessageLabel);

            MessageLabel.AutoSize = true;
            MessageLabel.ForeColor = Theme.Style.StatusMessageForeColor;
            MessageLabel.BackColor = Color.Transparent;
            MessageLabel.Font = Theme.Style.StatusMessageFont;
            MessageLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Tip
            TipLabel.AutoSize = false;
            TipLabel.ForeColor = Theme.Style.StatusTipForeColor;
            TipLabel.BackColor = Color.Transparent;
            TipLabel.Font = Theme.Style.StatusTipFont;
            TipLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Buttons
            ButtonsPanel.AutoSize = false;
            ButtonsPanel.AutoScroll = false;
            ButtonsPanel.BackColor = Color.Transparent;
            ButtonsPanel.Controls.Add(LeftButton);
            ButtonsPanel.Controls.Add(MiddleButton);
            ButtonsPanel.Controls.Add(RightButton);

            LeftButton.BackColor = MiddleButton.BackColor = RightButton.BackColor = Theme.Style.StatusButtonBackColor;
            LeftButton.ForeColor = MiddleButton.ForeColor = RightButton.ForeColor = Theme.Style.StatusButtonForeColor;
            LeftButton.Font = MiddleButton.Font = RightButton.Font = Theme.Style.StatusButtonFont;
            LeftButton.FlatStyle = MiddleButton.FlatStyle = RightButton.FlatStyle = FlatStyle.Flat;
            LeftButton.Visible = MiddleButton.Visible = RightButton.Visible = false;

            LeftButton.Click += (object sender, EventArgs e) => { LeftButtonAction(); };
            MiddleButton.Click += (object sender, EventArgs e) => { MiddleButtonAction(); };
            RightButton.Click += (object sender, EventArgs e) => { RightButtonAction(); };


            // Close Button
            CloseButton.Text = '\u00D7'.ToString();
            CloseButton.TextAlign = ContentAlignment.MiddleCenter;
            CloseButton.BackColor = Color.Transparent;
            CloseButton.ForeColor = Theme.Style.QuickDialogSuccessForeColor;
            CloseButton.Font = new Font(Theme.Style.QuickDialogFont, FontStyle.Bold);
            CloseButton.Size = new Size(20, 20);
            CloseButton.Click += StatusCloseButton_Click;

            /*TitleLabel.BackColor = Color.Red;
            MessagePanel.BackColor = Color.Green;
            MessageLabel.BackColor = Color.DarkGreen;
            TipLabel.BackColor = Color.Blue;
            ButtonsPanel.BackColor = Color.Violet;*/

            RelocateAndResize();

        }

        public void ActivateButton(int button, string text, Action action)
        {
            switch (button)
            {
                case 1:
                    LeftButtonAction = action;
                    LeftButton.Text = text;
                    LeftButton.Visible = true;
                    break;
                case 2:
                    MiddleButtonAction = action;
                    MiddleButton.Text = text;
                    MiddleButton.Visible = true;
                    break;
                case 3:
                    RightButtonAction = action;
                    RightButton.Text = text;
                    RightButton.Visible = true;
                    break;
                default:
                    throw new ArgumentException("Button not found !");
            }
        }
        public void DeactivateButton(int button)
        {
            switch (button)
            {
                case 1:
                    LeftButton.Visible = false;
                    break;
                case 2:
                    MiddleButton.Visible = false;
                    break;
                case 3:
                    RightButton.Visible = false;
                    break;
                default:
                    throw new ArgumentException("Button not found !");
            }
        }

        public void RelocateAndResize()
        {
            this.Location = new Point(CChesstion.CentrePanel.Location.X + (int)(CChesstion.CentrePanel.Size.Width * 0.1), CChesstion.CentrePanel.Location.Y + (int)((CChesstion.CentrePanel.Size.Height - CChesstion.CentrePanel.HeaderHeight) * 0.2));
            this.Size = new Size((int)(CChesstion.CentrePanel.Size.Width * 0.8), (int)((CChesstion.CentrePanel.Size.Height - CChesstion.CentrePanel.HeaderHeight) * 0.8));

            // Title
            TitleLabel.Location = new Point((int)(this.Size.Width * 0.05), 0);
            TitleLabel.Size = new Size((int)(this.Size.Width * 0.90), (int)(this.Size.Height * 0.1));

            // Message
            MessagePanel.Location = new Point(TitleLabel.Location.X, TitleLabel.Bottom);
            MessagePanel.Size = new Size(TitleLabel.Width, (int)(this.Size.Height * 0.65));

            MessageLabel.MaximumSize = new Size((int)(MessagePanel.Width * 0.92), 0);
            MessageLabel.MinimumSize = new Size((int)(MessagePanel.Width * 0.92), MessagePanel.Height);
            MessageLabel.Location = new Point((int)(MessagePanel.Width * 0.04), 0);

            // Label
            TipLabel.Location = new Point(MessagePanel.Location.X, MessagePanel.Bottom);
            TipLabel.Size = new Size(MessagePanel.Width, (int)(this.Size.Height * 0.10));

            // Buttons
            ButtonsPanel.Location = new Point(TipLabel.Location.X, TipLabel.Bottom);
            ButtonsPanel.Size = new Size(TipLabel.Width, this.Height - ButtonsPanel.Location.Y);

            LeftButton.Size = DPI.Instance.MultipliedSize(170, 55);
            MiddleButton.Size = DPI.Instance.MultipliedSize(170, 55);
            RightButton.Size = DPI.Instance.MultipliedSize(170, 55);

            int margin = 6;
            int totalSize = 3 * LeftButton.Width + 2 * margin;

            LeftButton.Location = new Point((ButtonsPanel.Width - totalSize)/2, (ButtonsPanel.Height - LeftButton.Height)/2);
            MiddleButton.Location = new Point(LeftButton.Right + margin, LeftButton.Location.Y);
            RightButton.Location = new Point(MiddleButton.Right + margin, LeftButton.Location.Y);





            CloseButton.Location = new Point(Size.Width - CloseButton.Width, 0);

        }

        private void StatusCloseButton_Click(object sender, EventArgs e)
        {
            CChesstion.ShowStatusPanel(false, true);
        }


    }
}
