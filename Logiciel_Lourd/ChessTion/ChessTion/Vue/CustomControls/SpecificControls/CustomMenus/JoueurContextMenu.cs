using System;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MTournoi;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomMenus
{
    class JoueurContextMenu : ContextMenu
    {
        private readonly int reference;
        private readonly bool displayRenommer;
        private readonly bool displayConfirmer;

        public JoueurContextMenu(int reference, bool displayRenommer = true, bool displayConfirmer = true)
        {
            this.reference = reference;
            this.displayRenommer = displayRenommer;
            this.displayConfirmer = displayConfirmer;
            Popup += JoueurContextMenu_Popup;

            MenuItem renameItem = new MenuItem();
            renameItem.Name = "rename";
            renameItem.Text = "Renommer";
            renameItem.Click += RenommerEvent;

            MenuItem confirmItem = new MenuItem();
            confirmItem.Name = "confirm";
            confirmItem.Text = "Confirmer";
            confirmItem.Click += ConfirmerEvent;

            MenuItems.Add(renameItem);
            MenuItems.Add(confirmItem);
        }

        private void ConfirmerEvent(object sender, EventArgs e)
        {
            try
            {
                CChesstion.ConfimerJoueur(reference, !GJoueur.GetJoueur(reference).Confirme);
            }
            catch (ArgumentException ae)
            {
                CustomQuickDialog cqd = new CustomQuickDialog(
                    ae.Message,
                    GeneralControls.CustomDialogs.QuickDialogType.Error,
                    SourceControl,
                    GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterBelowParent);
            }
        }
        private void RenommerEvent(object sender, EventArgs e)
        {
            RenamePlayerDialog renamePlayerDialog = new RenamePlayerDialog(reference);
            renamePlayerDialog.ShowDialog();
        }


        private void JoueurContextMenu_Popup(object sender, EventArgs e)
        {
            Joueur j = GJoueur.GetJoueur(reference);
            MenuItems["confirm"].Text = j.Confirme ? "Annuler la confirmation" : "Confirmer";

            foreach (MenuItem mi in MenuItems)
                mi.Visible = true;

            if (!displayRenommer || j.Confirme)
                MenuItems["rename"].Visible = false;

            if (!displayConfirmer)
                MenuItems["confirm"].Visible = false;
        }
    }
}
