using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChessTion.Controleur;
using ChessTion.Controleur.CTournoi;
using ChessTion.Utilitaires;

namespace ChessTion.Test
{
    /// <summary>
    /// Classe de tests.
    /// </summary>
    class TimotheeTest : Test
    {

        public TimotheeTest()
        {
            BaseDeDonnees Test = new BaseDeDonnees("Test", CChesstion.BasePath + @"\Ressources\Test.mdb");

            //Test.ForcerMiseAJourAsync();
            string[] s = new string[1] { "http://pastebin.com/raw/a7Ys5RB0" };
            //Convertisseur.JsonToMdb(s , CChesstion.BasePath + @"\Ressources\Test.mdb");
            //Mail.EnvoyerMail(CChesstion.BasePath + @"\Ressources\Test.mdb");

            string[] rt = new string[3]; //adresses url JSON
            //Convertisseur.MdbToJson(CChesstion.BasePath + @"\Ressources\Test.mdb", rt);

            List<string[]> l =  GJoueur.ComporteDesErreurs(1);
            MessageBox.Show(l.ElementAt(13)[0] + "   " + l.ElementAt(13)[1]);


        }
    }
}
