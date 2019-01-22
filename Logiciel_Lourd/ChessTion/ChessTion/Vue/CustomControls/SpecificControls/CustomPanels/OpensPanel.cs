using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;
using ChessTion.Vue.CustomControls.GeneralControls.CustomPanels;
using ChessTion.Vue.CustomControls.SpecificControls.CustomButtons;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    class OpensPanel : AddDeletePanel, IChesstionPanel
    {
        private static readonly int startingY = 49;
        private static readonly int spaceY = 3;
        private CustomQuickDialog confirmDeleteDialog;
        private CustomQuickDialog errorDeleteDialog;
        private bool readOnly = false;

        public List<OpensMenuButton> MenuButtons
        {
            get
            {
                List<OpensMenuButton> buttons = new List<OpensMenuButton>();

                foreach (Control c in Controls)
                    if (c is OpensMenuButton)
                        buttons.Add((OpensMenuButton) c);

                return buttons;
            }
        }

        public OpensMenuButton SelectedButton
        {
            get
            {
                foreach (OpensMenuButton omb in MenuButtons)
                    if (omb.Selected)
                        return omb;

                return null;
            }
            set
            {
                foreach (OpensMenuButton omb in MenuButtons)
                {
                    if (omb != value)
                        omb.Selected = false;
                    else
                        omb.Selected = true;
                }
                    

            }
        }

        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                if (ReadOnly)
                    AllowAdd = AllowDelete = false;
            }
        }

        public OpensPanel()
        {
            AddButtonClicked += OpensPanel_AddButtonClicked;
            DeleteButtonClicked += OpensPanel_DeleteButtonClicked;
        }



        public void Init()
        {
            CreatePanel();
        }

        private void CreatePanel()
        {
            //Location = DPI.Instance.MultipliedPoint(0, 30);
            //Size = DPI.Instance.MultipliedSize(200, 350);
            HeaderHeight = (int)(DPI.Instance.RelativeMultiplier.Y * Theme.Style.OpensHeaderHeight);
            BackColor = Theme.Style.OpensBodyBackColor;
            HeaderBackColor = Theme.Style.OpensHeaderBackColor;
            HeaderForeColor = Theme.Style.OpensHeaderForeColor;
            HeaderFont = Theme.Style.OpensHeaderFont;
            Title = "Opens";

            RelocateAndResize();
        }

        public void RelocateAndResize()
        {
            Location = new Point(0, CChesstion.MsMenu.Height + 2);
            Size = new Size((int)(DPI.Instance.RelativeMultiplier.X * 200),
            (Parent.ClientSize.Height - (CChesstion.MsMenu.Height + 2))/2);

            RelocateButtons();
        }

        public void AddOpenMenuButton(string openNom, int reference)
        {
            OpensMenuButton newButton = new OpensMenuButton();
            if (MenuButtons.Count > 0)
                newButton.Location = new Point(0, MenuButtons.Last().Location.Y + newButton.Size.Height + spaceY);
            else
                newButton.Location = new Point(0, startingY);

            newButton.Text = openNom;
            newButton.Ref = reference;

            newButton.MouseUp += MenuButton_MouseUp;

            Controls.Add(newButton);
            if (MenuButtons.Count == 1)
                newButton.Selected = true;
        }



        public void DeleteMenuButton(int reference)
        {
            foreach (OpensMenuButton omb in MenuButtons)
                if (omb.Tag.Equals(reference))
                {
                    Controls.Remove(omb);

                    if (omb.Selected && MenuButtons.Count > 0)
                        CChesstion.SelectionnerOpen(MenuButtons[0].Ref);
                    else
                        CChesstion.OpenPanel.Reset();

                    foreach (OpensMenuButton ombb in MenuButtons)
                        if (ombb.Ref > reference)
                            ombb.Location = new Point(ombb.Location.X, ombb.Location.Y - ombb.Size.Height - spaceY);

                        return;
                }

            throw new ArgumentException("Aucun bouton ne correspond à ce reference.");
        }
        public void SelectMenuButton(int reference)
        {
            foreach (OpensMenuButton omb in MenuButtons)
                if (omb.Ref == reference && !omb.Selected)
                    SelectedButton = omb;
        }


        private void OpensPanel_AddButtonClicked(object sender, EventArgs e)
        {
            OpenNameSingleInputDialog id = new OpenNameSingleInputDialog();

            if (id.ShowDialog() == DialogResult.Cancel)
                return;

            CChesstion.CreerOpen(id.Input, -1);
        }
        private void OpensPanel_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (MenuButtons.Count == 0)
                return;

            try
            {
                if (confirmDeleteDialog == null || !confirmDeleteDialog.Visible)
                {
                    confirmDeleteDialog = new CustomQuickDialog(
                        "Êtes-vous sûr ?\nCliquez à nouveau pour confirmer !",
                        QuickDialogType.Info,
                        DeleteButton,
                        QuickDialogRelativeStartPosition.CenterBelowParent);
                    confirmDeleteDialog.DisplayDelay = 3000;
                    confirmDeleteDialog.Show();
                }
                else
                {
                    confirmDeleteDialog.Close();
                    CChesstion.SupprimerOpen((int)SelectedButton.Tag);
                }
            }
            catch (ArgumentException ae)
            {

                errorDeleteDialog = new CustomQuickDialog(
                        ae.Message,
                        QuickDialogType.Error,
                        DeleteButton,
                        QuickDialogRelativeStartPosition.CenterBelowParent);
                errorDeleteDialog.DisplayDelay = 3000;
                errorDeleteDialog.Show();
            }
        }

        private void MenuButton_MouseUp(object sender, EventArgs e)
        {
            
            OpensMenuButton omb = (OpensMenuButton)sender;
            MouseEventArgs me = (MouseEventArgs)e;

            if (!omb.ClientRectangle.Contains(omb.PointToClient(MousePosition)))
                return;

            if (me.Button == MouseButtons.Left)
                CChesstion.SelectionnerOpen(omb.Ref);
            else if (me.Button == MouseButtons.Right && !ReadOnly)
            {
                OpenNameSingleInputDialog id = new OpenNameSingleInputDialog(omb.Text);

                if (id.ShowDialog() == DialogResult.Cancel)
                    return;

                CChesstion.ChangerNomOpen((int) omb.Tag, id.Input);

            }

        }


    }
}
