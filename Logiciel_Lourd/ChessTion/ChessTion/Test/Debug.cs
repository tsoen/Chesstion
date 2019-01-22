using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTion.Controleur;

namespace ChessTion.Test
{
    /// <summary>
    /// Classe gérant le log de débug.
    /// </summary>
    static class Debug
    {


        /*************************************************************
         *    __    ____  ____  ____  ____  ____  __  __  ____  ___  *
         *   /__\  (_  _)(_  _)(  _ \(_  _)(  _ \(  )(  )(_  _)/ __) *
         *  /(__)\   )(    )(   )   / _)(_  ) _ < )(__)(   )(  \__ \ *
         * (__)(__) (__)  (__) (_)\_)(____)(____/(______) (__) (___/ *
         *                                                           *
         *      Ensemble des attributs utilisés dans la classe.      *
         *                                                           *
         *************************************************************/

        /// <summary>
        /// Chemin d'accès du fichier servant de logs.
        /// </summary>
        public static string FilePath { get; set; } = CChesstion.BasePath + @"/logs.txt";

        /// <summary>
        /// Vrai pour écrire dans la console en plus du fichier de logs.
        /// </summary>
        public static bool WriteToConsole { get; set; } = true;









        /********************************************************
         *  __  __  ____  ____  _   _  _____  ____   ____  ___  *
         * (  \/  )( ___)(_  _)( )_( )(  _  )(  _ \ ( ___)/ __) *
         *  )    (  )__)   )(   ) _ (  )(_)(  )(_) ) )__) \__ \ *
         * (_/\/\_)(____) (__) (_) (_)(_____)(____/ (____)(___/ *
         *                                                      *
         *      Ensemble des méthodes autres de la classe.      *
         *                                                      *
         ********************************************************/

        /// <summary>
        /// Ecrit un texte.
        /// </summary>
        /// <param name="text">Texte à écrire.</param>
        public static void Write(string text)
        {
            if (WriteToConsole)
                System.Diagnostics.Debug.Write(text);

            File.AppendAllText(FilePath, text);
        }

        /// <summary>
        /// Ecrit un texte suivi d'un retour à la ligne.
        /// </summary>
        /// <param name="text">Texte à écrire.</param>
        public static void WriteLine(string text)
        {
            Write(text + Environment.NewLine);
        }

        /// <summary>
        /// Vide le fichier de logs.
        /// </summary>
        public static void EmptyLogFile()
        {
            File.WriteAllText(FilePath, "");
        }
    }
}
