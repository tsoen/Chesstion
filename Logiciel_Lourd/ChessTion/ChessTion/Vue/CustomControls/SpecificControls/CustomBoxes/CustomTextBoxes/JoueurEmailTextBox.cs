using System.Text.RegularExpressions;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    /// <summary>
    /// Classe vue gérant une text box qui n'accèpte que les emails.
    /// </summary>
    class JoueurEmailTextBox : JoueurTextBox
    {
        /// <summary>
        /// Constructeur.
        /// </summary>
        public JoueurEmailTextBox() : base()
        {
            AllowLetters = true;
            AllowNumbers = true;
            AllowedCharacters.Add('@');
            AllowedCharacters.Add('.');
            AllowedCharacters.Add('_');
            Regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            PlaceholderText = "adresse@mail.fr";
        }
    }
}
