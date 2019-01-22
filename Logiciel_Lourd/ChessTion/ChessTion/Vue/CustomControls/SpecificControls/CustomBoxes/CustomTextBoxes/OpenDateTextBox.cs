namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    /// <summary>
    /// Classe vue gérant une text box qui n'accèpte que les dates.
    /// </summary>
    class OpenDateTextBox : OpenTextBox
    {
        /// <summary>
        /// Constructeur.
        /// </summary>
        public OpenDateTextBox()
        {
            Regex = new System.Text.RegularExpressions.Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            MaxLength = 10;
            AllowNumbers = true;
            AllowedCharacters.Add('/');
            AllowedCharacters.Add('.');
            AllowedCharacters.Add('-');
            PlaceholderText = "01/01/1990";
        }
    }
}
