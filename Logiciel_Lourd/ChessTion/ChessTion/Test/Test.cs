using System;
using System.Linq;
using ChessTion.Controleur;
using ChessTion.Controleur.CLieu;
using ChessTion.Controleur.CTournoi;
using ChessTion.Modele.MLieu;
using ChessTion.Modele.MTournoi;

namespace ChessTion.Test
{
    /// <summary>
    /// Classe de tests.
    /// </summary>
    class Test
    {
        public Test(bool lancerJeuDessai = true)
        {
            if (lancerJeuDessai)
                LancerJeuDessai();
        }

        public void LancerJeuDessai()
        {
            if (GTournoi.ListerTournois().Count > 0)
                return;

            /*Ville ville = GLieu.CreerVille("Illkirch", "67100");
            Lieu lieu1 = GLieu.CreerLieu("Association des bg", "15A", "Rue des Boloss");
            Lieu lieu2 = GLieu.CreerLieu("Ici les bails", "1", "Rue des geeks");
            ville.TsLesLieux.Add(lieu1);
            ville.TsLesLieux.Add(lieu2);*/

            Tournoi tournoi = GTournoi.CreerTournoi("Tournoi des vainqueurs", new DateTime(2016, 12, 5), new DateTime(2016, 12, 7), 1); // ID = 1
            CChesstion.TournoiSelectionne = tournoi;
            CChesstion.UpdateTournoiEtat();

            CChesstion.CreerRepas("Poulet", 3);
            CChesstion.CreerRepas("Steak", 5);
            CChesstion.CreerRepas("Frites", 2);



            //GClub.CreerClub(1, "Ffe", "Club oui", "guhio", "ghjokl", "ghjk");
            //GClub.CreerClub(2, "Ffe", "Club Win", "guhio", "ghjokl", "ghjk");

            CChesstion.CreerOpen("A", 1900); // ID = 1
            CChesstion.CreerOpen("B", 2100); // ID = 2
            CChesstion.CreerOpen("C", 2400); // ID = 3

            CChesstion.OpenSelectionne = GOpen.ListerOpens().First();

        }

        public void Test1()
        {

        }

        public void Test2()
        {

        }

        public void Test3()
        {

        }

        public void Test4()
        {

        }
    }
}
