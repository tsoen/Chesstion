using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CRepas;
using ChessTion.Controleur.CTournoi;
using ChessTion.Controleur.Etats;
using ChessTion.Controleur.ImportExport;
using ChessTion.Modele.MLieu;
using ChessTion.Modele.MTournoi;
using ChessTion.Utilitaires;
using ChessTion.Vue.CustomControls.GeneralControls.CustomDialogs;
using ChessTion.Vue.CustomControls.SpecificControls.CustomDialogs;
using Newtonsoft.Json.Linq;

namespace ChessTion.Test
{
    /// <summary>
    /// Classe de tests.
    /// </summary>
    class GaetanTest : Test
    {
        public GaetanTest() : base(false)
        {
            CChesstion.OpensPanel.ContextMenu = new ContextMenu();

            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Show DPI", ShowDPIEvent);
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Generate 100 players", Generate100PlayersEvent);
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Prochain état", ProchainEtat);
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Show BasePath", ShowBasePath);
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Empty log file", (object sender, EventArgs e) => { Debug.EmptyLogFile(); });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Ajouter joueur sans FFE", AjouterJoueurSansFFE);
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Faire apparaitre boutons sur statuspanel", BoutonStatusPanel);
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Enable CenterPanel", (object sender, EventArgs e) =>
            {
                CChesstion.CentrePanel.Enabled = !CChesstion.CentrePanel.Enabled;
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Show open.tournoi.ref", (object sender, EventArgs e) =>
                {
                    MessageBox.Show(GTournoi.GetTournoiDeOpen(CChesstion.OpenSelectionne.Ref).Ref.ToString());
                });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Send mail to nateags@mail.com", (object sender, EventArgs e) =>
            {
                Mail.Send(new System.Net.Mail.MailAddress("nateags@gmail.com"), "test", "le corps");
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Export joueurs dans json", (object sender, EventArgs e) =>
            {
                ExportJoueurs.ToJson(CChesstion.TournoiSelectionne.TsLesJoueurs, CChesstion.BasePath + "/tmp/ex.json");
                MessageBox.Show("Exporté dans " + CChesstion.BasePath + "/tmp/ex.json");
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Export tournoi vers /tmp/ex.json", (object sender, EventArgs e) =>
            {
                File.WriteAllText(CChesstion.BasePath + "/tmp/ex.json",
                    ExportTournoi.ToJson(CChesstion.TournoiSelectionne.Ref));
                MessageBox.Show("Exporté dans " + CChesstion.BasePath + "/tmp/ex.json");
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Import tournoi vers /tmp/ex.json", (object sender, EventArgs e) =>
            {
                Tournoi t = ImportTournoi.FromJson(CChesstion.BasePath + "/tmp/ex.json");
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Lister saves by ref", (object sender, EventArgs e) =>
            {
                MessageBox.Show(string.Join("\n", Save.ListSavesByRef()));
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Lister saves by names", (object sender, EventArgs e) =>
            {
                MessageBox.Show(string.Join("\n", Save.ListSavesByName()));
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("DeleteAll", (object sender, EventArgs e) =>
            {
                CChesstion.DeleteAll();
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("Load save 1", (object sender, EventArgs e) =>
            {
                Save.LoadSave(1);
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("EnregistrerLieux", (object sender, EventArgs e) =>
            {
                GLieu.EnregistrerLieux();
            });
            CChesstion.OpensPanel.ContextMenu.MenuItems.Add("LoadLieux", (object sender, EventArgs e) =>
            {
                GLieu.ChargerLieux();
            });
        }

        private void BoutonStatusPanel(object sender, EventArgs e)
        {
            CChesstion.StatusPanel.ActivateButton(1, "Bouton de gauche", () => { });
            CChesstion.StatusPanel.ActivateButton(2, "Bouton du milieu", () => { });
            CChesstion.StatusPanel.ActivateButton(3, "Bouton de droite", () => { });
        }

        private void AjouterJoueurSansFFE(object sender, EventArgs e)
        {
            CChesstion.CreerJoueur(2, "", "ROBERT", "Jean", "M", "24/04/1965", "", "", GClub.ListerClubs().First().Ref, 1500, 1500, "", "",
                "", "", "", GOpen.ListerOpens().First().Ref, "bfds@fd.fr", false, "", GRepas.ListerRepas().First().Ref);
        }

        private void ShowBasePath(object sender, EventArgs e)
        {
            CustomQuickDialog cqd = new CustomQuickDialog(
                "Base Path : " + CChesstion.BasePath,
                QuickDialogType.Info,
                CChesstion.CentrePanel,
                QuickDialogRelativeStartPosition.CenterOnParent);

            cqd.DisplayDelay = 10000;
            cqd.Show();

            //Debug.WriteLine(CChesstion.BasePath);
        }

        private void ProchainEtat(object sender, EventArgs e)
        {
            CChesstion.TournoiProchainEtat();
        }
        private void Generate100PlayersEvent(object sender, EventArgs e)
        {
            CustomQuickDialog d2 = new CustomQuickDialog(
                "Starting...",
                QuickDialogType.Warning,
                CChesstion.CentrePanel,
                new Point(0, 0));
            d2.RelativeStartPosition = QuickDialogRelativeStartPosition.CenterOnParent;
            d2.Show();

            Thread t = new Thread(new ThreadStart(() =>
            {
                int id = 1;
                foreach (Joueur j in GJoueur.ListerJoueurs())
                    if (j.Ref >= id)
                        id = j.Ref + 1;

                for (int i = 0; i < 100; ++i)
                    GJoueur.CreerJoueur(
                    id,
                    "X12345",
                    "NOM",
                    "Prenom " + id++,
                    "M",
                    "25/10/1985",
                    "SenM",
                    "FRA",
                    1,
                    1234,
                    1234,
                    "f",
                    "12345678",
                    "m",
                    "4",
                    "o",
                    1,
                    "ghjkl@hds.fr",
                    true,
                    "0654253504",
                    1
                );
            }));

            t.Start();
            t.Join();

            d2.Close();

            CustomQuickDialog d1 = new CustomQuickDialog(
                "Done !",
                QuickDialogType.Warning,
                CChesstion.CentrePanel,
                new Point(0, 0));
            d1.RelativeStartPosition = QuickDialogRelativeStartPosition.CenterOnParent;
            d1.Show();
        }
        private void ShowDPIEvent(object sender, EventArgs e)
        {
            CustomQuickDialog d1 = new CustomQuickDialog(
                "DPI : " + DPI.Instance.CurrentDPI,
                QuickDialogType.Warning,
                CChesstion.CentrePanel,
                new Point(0, 0));
            d1.RelativeStartPosition = QuickDialogRelativeStartPosition.CenterOnParent;
            d1.Show();
        }

        public new void Test1()
        {
            File.WriteAllText(CChesstion.BasePath + "/tmp/" + CChesstion.TournoiSelectionne.Ref + ".json", ExportTournoi.ToJson(CChesstion.TournoiSelectionne.Ref));
        }

        public new void Test2()
        {
            CChesstion.CentrePanel.Panel.Size = new Size(CChesstion.CentrePanel.Panel.Size.Width + 100,
                CChesstion.CentrePanel.Panel.Size.Height);
        }

        public new void Test3()
        {
            OleDbConnection Connec = new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + CChesstion.BasePath + @"\Ressources\DATA.MDB");
            Connec.Open();

            string rqt = "SELECT TOP 125 * FROM JOUEUR WHERE NrFFE IS NOT NULL AND FideCode IS NOT NULL AND Federation = 'FRA' ORDER BY rnd(Ref)";
            OleDbDataAdapter da = new OleDbDataAdapter(rqt, Connec);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //MessageBox.Show(dt.Rows.Count.ToString());

            Random r = new Random();
            JArray array = new JArray();

            foreach (DataRow dr in dt.Rows)
            {
                JObject infos = new JObject
                {
                    {"Ref", int.Parse(dr["Ref"].ToString())},
                    {"NrFFE", dr["NrFFE"].ToString()},
                    {"Nom", dr["Nom"].ToString()},
                    {"Prenom", dr["Prenom"].ToString()},
                    {"Sexe", dr["Sexe"].ToString()},
                    {"NeLe", DateTime.Parse(dr["NeLe"].ToString()).ToShortDateString()},
                    {"Cat", dr["Cat"].ToString()},
                    {"Federation", dr["Federation"].ToString()},
                    {"ClubRef", int.Parse(dr["ClubRef"].ToString())},
                    {"Elo", int.Parse(dr["Elo"].ToString())},
                    {"Rapide", int.Parse(dr["Rapide"].ToString())},
                    {"Fide", dr["Fide"].ToString()},
                    {"FideCode", dr["FideCode"].ToString()},
                    {"FideTitre", dr["FideTitre"].ToString()},
                    {"AffType", dr["AffType"].ToString()},
                    {"Actif", dr["Actif"].ToString()},
                    {"OpenRef", r.Next(1, 4)},
                    {"Email", "mail@email.com"},
                    {"Phone", "0685263514"},
                    {"RepasRef", r.Next(1, 3)},
                };
                array.Add(infos);
            }

            File.WriteAllText(CChesstion.BasePath + "/tmp/inscrits.json", array.ToString());
        }

        public new void Test4()
        {
        }
    }
}
