using System;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChessTion.Controleur;
using Newtonsoft.Json.Linq;

namespace ChessTion
{
    /// <summary>
    /// Classe gérant le thème graphique de l'application.
    /// </summary>
    static class Theme
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
        /// Style du thème.
        /// </summary>
        private static ThemeStyle style = new ThemeStyle();

        /// <summary>
        /// Nom du thème.
        /// </summary>
        private static string _themeName = "dark";










        /************************************************************
         *   ___  ____  ____    ____  ____    ___  ____  ____  ___  *
         *  / __)( ___)(_  _)  ( ___)(_  _)  / __)( ___)(_  _)/ __) *
         * ( (_-. )__)   )(     )__)   )(    \__ \ )__)   )(  \__ \ *
         *  \___/(____) (__)   (____) (__)   (___/(____) (__) (___/ *
         *                                                          *
         *       Ensemble des getters et setters de la classe.      *
         *                                                          *
         ************************************************************/

        /// <summary>
        /// Style du thème.
        /// </summary>
        public static ThemeStyle Style { get { return style; } }

        /// <summary>
        /// Nom du thème.
        /// </summary>
        public static string ThemeName { get { return _themeName; } }









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
        /// Charge un style.
        /// </summary>
        /// <param name="name"></param>
        public static void LoadStyle(string name = "")
        {
            try
            {
                if (name.Length == 0)
                {
                    string theme = (string)JObject.Parse(File.ReadAllText(CChesstion.SettingsFolder + "/settings.json"))["theme"];
                    LoadStyle(theme);
                    return;
                }

                string json = File.ReadAllText(CChesstion.ThemeFolder + "/" + name + "/" + name + ".json");
                style = JsonConvert.DeserializeObject<ThemeStyle>(json);
                _themeName = name;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue lors du chargement du thème, chargement du thème par défaut...\n\n" + ex.Message + " " + ex.StackTrace);
                SetStyleDefaultValue();
            }
        }

        /// <summary>
        /// Charge un style par défaut.
        /// </summary>
        public static void SetStyleDefaultValue()
        {
            style = new ThemeStyle();
            _themeName = "default";

            // General
            style.GeneralBackColor = Color.FromArgb(63, 63, 70);


            // Menu
            style.MenuBackColor = Color.FromArgb(37, 37, 38);
            style.MenuForeColor = Color.FromArgb(224, 221, 240);
            style.MenuSeparatorColor = Color.FromArgb(63, 63, 70);
            style.MenuFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;


            // Opens
            style.OpensHeaderBackColor = Color.FromArgb(28, 28, 28);
            style.OpensHeaderForeColor = Color.FromArgb(224, 221, 240);
            style.OpensHeaderFont = (new FontConverter()).ConvertFromString("Segoe UI; 11pt; style=Bold") as Font;
            style.OpensHeaderHeight = 32;

            style.OpensBodyBackColor = Color.FromArgb(37, 37, 38);

            style.OpensButtonsBackColor = Color.FromArgb(45, 45, 48);
            style.OpensButtonsForeColor = Color.FromArgb(224, 221, 240);
            style.OpensButtonsFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            style.OpensSelectedButtonBackColor = Color.FromArgb(63, 63, 70);
            style.OpensSelectedButtonForeColor = Color.FromArgb(224, 221, 240);
            style.OpensSelectedButtonFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Bold") as Font;


            // Repas
            style.RepasHeaderBackColor = Color.FromArgb(28, 28, 28);
            style.RepasHeaderForeColor = Color.FromArgb(224, 221, 240);
            style.RepasHeaderFont = (new FontConverter()).ConvertFromString("Segoe UI; 11pt; style=Bold") as Font;
            style.RepasHeaderHeight = 32;

            style.RepasBodyBackColor = Color.FromArgb(37, 37, 38);

            style.RepasTextBoxesBackColor = Color.FromArgb(37, 37, 38);
            style.RepasTextBoxesForeColor = Color.FromArgb(224, 221, 240);
            style.RepasTextBoxesFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;


            // Joueur
            style.JoueurHeaderBackColor = Color.FromArgb(28, 28, 28);
            style.JoueurHeaderForeColor = Color.FromArgb(224, 221, 240);
            style.JoueurHeaderFont = (new FontConverter()).ConvertFromString("Segoe UI; 11pt; style=Bold") as Font;
            style.JoueurHeaderHeight = 32;
            style.JoueurHeaderConfirmedForeColor = Color.FromArgb(39, 174, 96);

            style.JoueurBodyBackColor = Color.FromArgb(37, 37, 38);

            style.JoueurBoxesBackColor = Color.FromArgb(37, 37, 38);
            style.JoueurBoxesForeColor = Color.FromArgb(224, 221, 240);
            style.JoueurBoxesFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Regular") as Font;

            style.JoueurBoxesPlaceHolderForeColor = Color.FromArgb(113, 114, 149);
            style.JoueurBoxesPlaceHolderFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Italic") as Font;

            style.JoueurBoxesErrorBackColor = Color.FromArgb(145, 50, 46);

            style.JoueurLabelsForeColor = Color.FromArgb(224, 221, 240);
            style.JoueurLabelsFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Bold") as Font;
            style.JoueurLabelsConfirmedForeColor = Color.FromArgb(39, 174, 96);

            style.JoueurConfirmButtonBackColor = Color.FromArgb(63, 63, 70);
            style.JoueurConfirmButtonForeColor = Color.FromArgb(224, 221, 240);
            style.JoueurConfirmButtonFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            style.JoueurDetailsButtonBackColor = Color.FromArgb(45, 45, 48);
            style.JoueurDetailsButtonForeColor = Color.FromArgb(224, 221, 240);
            style.JoueurDetailsButtonFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;


            // Open
            style.OpenHeaderBackColor = Color.FromArgb(28, 28, 28);
            style.OpenHeaderForeColor = Color.FromArgb(224, 221, 240);
            style.OpenHeaderFont = (new FontConverter()).ConvertFromString("Segoe UI; 11pt; style=Bold") as Font;
            style.OpenHeaderHeight = 32;

            style.OpenBodyBackColor = Color.FromArgb(37, 37, 38);

            style.OpenBoxesBackColor = Color.FromArgb(37, 37, 38);
            style.OpenBoxesForeColor = Color.FromArgb(224, 221, 240);
            style.OpenBoxesFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            style.OpenBoxesPlaceHolderForeColor = Color.FromArgb(113, 114, 149);
            style.OpenBoxesPlaceHolderFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Italic") as Font;

            style.OpenBoxesErrorBackColor = Color.FromArgb(145, 50, 46);

            style.OpenLabelsForeColor = Color.FromArgb(224, 221, 240);
            style.OpenLabelsFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Bold") as Font;


            // Centre
            style.CentreHeaderBackColor = Color.FromArgb(0, 122, 204);
            style.CentreHeaderForeColor = Color.FromArgb(224, 221, 240);
            style.CentreHeaderFont = (new FontConverter()).ConvertFromString("Segoe UI; 18pt; style=Bold") as Font;
            style.CentreHeaderHeight = 48;

            style.CentreBodyBackColor = Color.FromArgb(28, 28, 28);

            style.CentreBodyJoueursForeColor = Color.FromArgb(224, 221, 240);
            style.CentreBodyJoueursFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Regular") as Font;
            style.CentreBodySelectedJoueursFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Bold") as Font;
            style.CentreBodyConfirmedJoueursForeColor = Color.FromArgb(39, 174, 96);
            style.CentreBodyErroredJoueursForeColor = Color.FromArgb(145, 50, 46);

            style.CentreBodyTextBoxBackColor = Color.FromArgb(28, 28, 28);
            style.CentreBodyTextBoxForeColor = Color.FromArgb(224, 221, 240);
            style.CentreBodyTextBoxFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Regular") as Font;
            style.CentreBodyTextPlaceHolderForeColor = Color.FromArgb(113, 114, 149);
            style.CentreBodyTextPlaceHolderFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Italic") as Font;

            // Action
            style.ActionsHeaderBackColor = Color.FromArgb(0, 122, 204);
            style.ActionsHeaderForeColor = Color.FromArgb(224, 221, 240);
            style.ActionsHeaderFont = (new FontConverter()).ConvertFromString("Segoe UI; 11pt; style=Bold") as Font;
            style.ActionsHeaderHeight = 2;

            style.ActionsBodyBackColor = Color.FromArgb(37, 37, 38);

            style.ActionsButtonsBackColor = Color.FromArgb(45, 45, 48);
            style.ActionsButtonsForeColor = Color.FromArgb(224, 221, 240);
            style.ActionsButtonsFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            // Dialog
            style.DialogBodyBackColor = Color.FromArgb(37, 37, 38);
            style.DialogBodyForeColor = Color.FromArgb(224, 221, 240);
            style.DialogBodyFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            style.DialogMainButtonsBackColor = Color.FromArgb(63, 63, 70);
            style.DialogMainButtonsForeColor = Color.FromArgb(224, 221, 240);
            style.DialogMainButtonsFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            style.DialogSecondaryButtonsBackColor = Color.FromArgb(45, 45, 48);
            style.DialogSecondaryButtonsForeColor = Color.FromArgb(224, 221, 240);
            style.DialogSecondaryButtonsFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            style.DialogTextBoxBackColor = Color.FromArgb(28, 28, 28);
            style.DialogTextBoxForeColor = Color.FromArgb(224, 221, 240);
            style.DialogTextBoxFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;
            style.DialogTextBoxPlaceHolderForeColor = Color.FromArgb(113, 114, 149);
            style.DialogTextBoxPlaceHolderFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Italic") as Font;
            style.DialogTextBoxErrorBackColor = Color.FromArgb(145, 50, 46);

            style.CentreBodyLabelForeColor = Color.FromArgb(224, 221, 240);
            style.CentreBodyLabelFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;

            // Quick Dialog
            style.QuickDialogFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt") as Font;
            
            style.QuickDialogInfoBackColor = Color.FromArgb(0, 122, 204);
            style.QuickDialogInfoForeColor = Color.FromArgb(224, 221, 240);

            style.QuickDialogSuccessBackColor = Color.FromArgb(27, 120, 66); // 39, 174, 96
            style.QuickDialogSuccessForeColor = Color.FromArgb(224, 221, 240);

            style.QuickDialogWarningBackColor = Color.FromArgb(239, 242, 132);
            style.QuickDialogWarningForeColor = Color.FromArgb(37, 37, 38);

            style.QuickDialogErrorBackColor = Color.FromArgb(145, 50, 46);
            style.QuickDialogErrorForeColor = Color.FromArgb(224, 221, 240);

            // Status Panel
            style.StatusBackColor = Color.FromArgb(27, 120, 66);

            style.StatusTitleForeColor = Color.FromArgb(224, 221, 240);
            style.StatusTitleFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Bold") as Font;

            style.StatusMessageForeColor = Color.FromArgb(224, 221, 240);
            style.StatusMessageFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt;") as Font;

            style.StatusTipForeColor = Color.FromArgb(224, 221, 240);
            style.StatusTipFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt; style=Italic") as Font;

            style.StatusButtonBackColor = Color.FromArgb(19, 84, 45); // 63, 63, 70
            style.StatusButtonForeColor = Color.FromArgb(224, 221, 240);
            style.StatusButtonFont = (new FontConverter()).ConvertFromString("Segoe UI; 9pt;") as Font;


            string json = JsonConvert.SerializeObject(style);
            File.WriteAllText(CChesstion.BasePath + "/txt.json", json);
        }


    }
}
