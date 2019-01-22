using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.ImportExport;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using ChessTion.Vue.CustomControls.SpecificControls.CustomPanels;
using Newtonsoft.Json.Linq;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomMenus
{
    class CustomMenuStrip : MenuStrip, IChesstionPanel
    {
        // Tournoi
        private CustomToolStripMenuItem L1Tournoi = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Nouveau = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Ouvrir = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Enregistrer = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Quitter = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Proprietes = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2UpdateInscrits = new CustomToolStripMenuItem();

        // Edition
        private CustomToolStripMenuItem L1Edition = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Lieu = new CustomToolStripMenuItem();

        // Màj
        private CustomToolStripMenuItem L1MiseAJour = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2MajAuto = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2MajImporter = new CustomToolStripMenuItem();

        // Options
        private CustomToolStripMenuItem L1Options = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Themes = new CustomToolStripMenuItem();

        // Aide
        private CustomToolStripMenuItem L1Aide = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2APropos = new CustomToolStripMenuItem();
        private CustomToolStripMenuItem L2Documentation = new CustomToolStripMenuItem();

        // Status
        public CustomToolStripMenuItem L1Status { get; private set; } = new CustomToolStripMenuItem();


        // Autres
        public bool EnableUpdateInscrits
        {
            get { return L2UpdateInscrits.Enabled; }
            set { L2UpdateInscrits.Enabled = value; }
        }


        public void Init()
        {
            // Panel
            BackColor = Theme.Style.MenuBackColor;
            ForeColor = Theme.Style.MenuForeColor;
            Font = Theme.Style.MenuFont;

            Items.Add(L1Tournoi);
            Items.Add(L1Edition);
            Items.Add(L1MiseAJour);
            Items.Add(L1Options);
            Items.Add(L1Aide);
            Items.Add(L1Status);



            // L1 Tournoi
            L1Tournoi.Text = "Tournoi";

            L1Tournoi.DropDownItems.Add(L2Nouveau);
            L1Tournoi.DropDownItems.Add(L2Ouvrir);
            L1Tournoi.DropDownItems.Add(L2Enregistrer);
            L1Tournoi.DropDownItems.Add(L2Quitter);
            L1Tournoi.DropDownItems.Add(new CustomToolStripSeparator());
            L1Tournoi.DropDownItems.Add(L2Proprietes);
            L1Tournoi.DropDownItems.Add(L2UpdateInscrits);

            L2Nouveau.Text = "Nouveau";
            L2Ouvrir.Text = "Ouvrir";
            L2Enregistrer.Text = "Enregistrer";
            L2Quitter.Text = "Fermer";
            L2Proprietes.Text = "Propriétés";
            L2Lieu.Text = "Ajouter un lieu...";
            L2UpdateInscrits.Text = "Mettre à jour les inscrits";
            L2UpdateInscrits.Enabled = false;



            // L1 Edition
            L1Edition.Text = "Edition";

            L1Edition.DropDownItems.Add(L2Lieu);



            // L1 Mise à jour
            L1MiseAJour.Text = "Mises à jour";

            L1MiseAJour.DropDownItems.Add(L2MajAuto);
            L1MiseAJour.DropDownItems.Add(L2MajImporter);

            L2MajAuto.Text = "Mettre à jour la base depuis le site FFE";
            L2MajImporter.Text = "Mettre à jour la base depuis un fichier .mdb";


            // L1 Options
            L1Options.Text = "Options";





            // Adds available themes to options
            string[] themes = Directory.GetDirectories(CChesstion.ThemeFolder);
            if (themes.Length != 0)
            {
                L1Options.DropDownItems.Add(L2Themes);
                L2Themes.Text = "Changer le thème";
                foreach (string s in themes)
                {
                    if (Path.GetFileName(s).Equals("default") || !File.Exists(s + "/" + Path.GetFileName(s) + ".json"))
                        continue;

                    CustomToolStripMenuItem theme = new CustomToolStripMenuItem();
                    theme.Text = Path.GetFileName(s);
                    theme.ForeColor = L1Options.ForeColor;
                    theme.BackColor = L1Options.BackColor;
                    theme.Font = L1Options.Font;
                    theme.Click += (object sender, EventArgs e) =>
                    {
                        JObject o = JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"));
                        o["theme"] = ((CustomToolStripMenuItem) sender).Text;
                        File.WriteAllText(CChesstion.SettingsFolder + "/settings.json", o.ToString());

                        MessageBox.Show("Le thème sera appliqué au prochain démarrage !");
                    };
                    if (theme.Text.Equals(Theme.ThemeName))
                        theme.Checked = true;

                    L2Themes.DropDownItems.Add(theme);
                }
            }



            // L1 Aide
            L1Aide.Text = "Aide";

            L1Aide.DropDownItems.Add(L2APropos);
            L1Aide.DropDownItems.Add(L2Documentation);

            L2APropos.Text = "À propos";
            L2Documentation.Text = "Documentation en ligne";


            // L1 Status
            L1Status.Text = "Afficher le panneau d'information/de statut";
            L1Status.Alignment = ToolStripItemAlignment.Right;
            L1Status.AutoSize = true;

            

            // Couleurs
            foreach (ToolStripItem tsi in Items)
            {
                if (!(tsi is ToolStripMenuItem)) continue;
                ToolStripMenuItem tsmi = (ToolStripMenuItem) tsi;

                tsmi.BackColor = BackColor;
                tsmi.ForeColor = ForeColor;
                tsmi.Font = Font;

                foreach (ToolStripItem tsii in tsmi.DropDownItems)
                {
                    if (!(tsii is ToolStripMenuItem)) continue;
                    ToolStripMenuItem tsmii = (ToolStripMenuItem)tsii;

                    tsmii.BackColor = BackColor;
                    tsmii.ForeColor = ForeColor;
                    tsmii.Font = Font;
                }
            }

            // Custom colors
            L1Status.BackColor = Theme.Style.StatusBackColor;
            L1Status.ForeColor = Theme.Style.StatusMessageForeColor;


            UpdateSavesList();
            AddEvents();

        }

        private void AddEvents()
        {
            L1Status.Click += L1Status_Click;


            L2Nouveau.Click += L2Nouveau_Click;
            L2Enregistrer.Click += (object sender, EventArgs e) => { Save.PerformSave(); };
            L2Quitter.Click += (object sender, EventArgs e) => { Application.Exit(); };
            L2MajImporter.Click += ChoisirBasejoueurs;
            L2MajAuto.Click += L2MajAuto_Click;
            L2Proprietes.Click += L2Proprietes_Click;
            L2Lieu.Click += L2Lieu_Click;
            L2APropos.Click += (object sender, EventArgs e) => { MessageBox.Show("S comme... Espoir."); };
            L2Documentation.Click += (object sender, EventArgs e) => { System.Diagnostics.Process.Start("https://docs.google.com/document/d/1RsiZ_NhFIg-0NWA82ftXvB1Ocx4CcE4E4_80EkRZ1dA/edit?usp=sharing"); };
            L2UpdateInscrits.Click += (object sender, EventArgs e) => { UpdateInscritsServeur(); };
        }

        private void L2MajAuto_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Cette opération peut prendre plusieurs minutes, êtes-vous sûr ?", "Mise à jour",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                BaseDeDonnees.MiseAJour += (object s, EventArgs ee) =>
                {
                    CChesstion.UpdateTournoiEtat(true);
                };
                CChesstion.EnableAll(false, true, false);
                CChesstion.StatusPanel.Title = "Mise à jour en cours...";
                CChesstion.StatusPanel.Message = "Veuillez patienter.";
                CChesstion.StatusPanel.Tip = "Mise à jour : 0 %";

                

                BackgroundWorker bw = new BackgroundWorker();
                bw.RunWorkerCompleted += (object s, RunWorkerCompletedEventArgs ee) =>
                {
                    if (((string) ee.Result).Equals(string.Empty))
                    {
                        if (File.Exists(CChesstion.BasePath + @"\Ressources\DATA.mdb"))
                            File.Delete(CChesstion.BasePath + @"\Ressources\DATA.mdb");

                        BaseDeDonnees.ForcerMiseAJourAsync((object ss, DownloadProgressChangedEventArgs args) =>
                        {
                            CChesstion.StatusPanel.Tip = "Mise à jour : " + args.ProgressPercentage + " %";
                        });

                    }
                    else
                    {
                        CChesstion.UpdateTournoiEtat(true);
                        CChesstion.StatusPanel.Tip = (string) ee.Result;
                    }
                };
                bw.DoWork += (object ss, DoWorkEventArgs ee) =>
                {
                    ee.Result = CChesstion.NoInternetIssues();
                };
                bw.RunWorkerAsync();


            }
        }

        private void L2Nouveau_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Voulez-vous enregistrer le tournoi actuellement ouvert (il sera fermé) ?", "Enregistrer le tournoi", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
                Save.PerformSave();
            else if (dialogResult == DialogResult.Cancel)
                return;

            CChesstion.TournoiSelectionne = GTournoi.CreerTournoi("Nouveau tournoi", DateTime.Today, DateTime.Today,
                GLieu.ListerLieux().First().Ref);


            CChesstion.UpdateTournoiEtat();
            CChesstion.SelectionnerOpen(GOpen.ListerOpens().First().Ref);

        }

        private void L2Lieu_Click(object sender, EventArgs e)
        {
            AjouterLieuDialog a = new AjouterLieuDialog();
            a.ShowDialog();
        }

        private void L2Proprietes_Click(object sender, EventArgs e)
        {
            TournoiProprietesDialog d = new TournoiProprietesDialog(CChesstion.TournoiSelectionne.Ref);
            d.ShowDialog();
        }

        private void L1Status_Click(object sender, EventArgs e)
        {
            CChesstion.ShowStatusPanel(!CChesstion.StatusPanel.Visible, true);
        }

        public void RelocateAndResize()
        {
            Location = new System.Drawing.Point(0, 0);
            Size = new System.Drawing.Size(Parent.ClientSize.Width, (int) (DPI.Instance.RelativeMultiplier.Y*28));
        }

        public void UpdateSavesList()
        {
            List<string> saves = Save.ListSavesByName();
            List<int> savesRef = Save.ListSavesByRef();
            int C = 0;

            L2Ouvrir.DropDownItems.Clear();
            L2Ouvrir.Enabled = saves.Count > 0;

            foreach (string s in saves)
            {
                CustomToolStripMenuItem save = new CustomToolStripMenuItem
                {
                    Text = s + " (réf. " + savesRef[C] + ")",
                    Tag = savesRef[C]
                };
                save.BackColor = this.BackColor;
                save.ForeColor = ForeColor;
                save.Font = Font;

                save.Click += (object sender, EventArgs e) => {
                    DialogResult dialogResult = MessageBox.Show("Voulez-vous enregistrer le tournoi actuellement ouvert (il sera fermé) ?", "Enregistrer le tournoi", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                        Save.PerformSave();
                    else if (dialogResult == DialogResult.Cancel)
                        return;

                    CustomToolStripMenuItem c = (CustomToolStripMenuItem)sender;

                    Save.LoadSave((int)c.Tag);
                };

                L2Ouvrir.DropDownItems.Add(save);


                C++; //     <-- Enorme Blague d'IUT Info.
            }
        }

        public void ChoisirBasejoueurs(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Access Files (MDB)| *.MDB";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                CChesstion.EnableAll(false);
                CChesstion.StatusPanel.Title = "Importation en cours...";
                CChesstion.StatusPanel.Message = "";
                CChesstion.StatusPanel.Tip = "";
                CChesstion.ShowStatusPanel(true, false);

                string sourceFile = ofd.FileName;
                File.Copy(sourceFile, CChesstion.BasePath + @"\Ressources\DATA.MDB", true);

                CChesstion.UpdateTournoiEtat(true);
                CustomQuickDialog d = new CustomQuickDialog("La nouvelle base FFE a été importée !",
                    GeneralControls.CustomDialogs.QuickDialogType.Success, this,
                    GeneralControls.CustomDialogs.QuickDialogRelativeStartPosition.CenterOnParent);
                d.DisplayDelay = 3000;
                d.Show();


            }
        }

        public void UpdateInscritsServeur()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                e.Result = CChesstion.NoInternetIssues();
            };
            bw.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                if (!((string) e.Result).Equals(string.Empty))
                {
                    //CChesstion.UpdateTournoiEtat(true);
                    CChesstion.StatusPanel.Tip = (string) e.Result;
                    return;
                }

                Save.PerformSave();

                Action action = () =>
                {
                    CChesstion.StatusPanel.Tip = "Upload des inscrits terminé.";
                };

                FTPAdapter.UploadFileAsync(CChesstion.SaveFolder + "/" + CChesstion.TournoiSelectionne.Ref + "/joueurs.json",
                    "/json/Inscrits/" + CChesstion.TournoiSelectionne.Ref + ".json", action,
                        new Action<ulong>((ulong percent) =>
                        {
                            CChesstion.StatusPanel.Tip = "Upload sur le site : " + percent + " %";
                        }));

                /*
                FTPAdapter.UploadFileAsync(
                    CChesstion.SaveFolder + "/" + CChesstion.TournoiSelectionne.Ref + "/joueurs.json",
                    "/json/Inscrits/" + CChesstion.TournoiSelectionne.Ref + ".json",
                    new UploadProgressChangedEventHandler((object s, UploadProgressChangedEventArgs args) =>
                    {
                        CChesstion.StatusPanel.Tip = "Upload des inscrits : " + args.ProgressPercentage + " %";
                    }),
                    new UploadFileCompletedEventHandler((object s, UploadFileCompletedEventArgs args) =>
                        {
                            CChesstion.StatusPanel.Tip = "Upload des inscrits terminé.";
                        }
                    ));
                    */
            };
            bw.RunWorkerAsync();
            CChesstion.StatusPanel.Tip = "Vérification de la connexion Internet...";



        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
