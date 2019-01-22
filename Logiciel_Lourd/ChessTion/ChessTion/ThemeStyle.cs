using System.Drawing;

namespace ChessTion
{
    class ThemeStyle
    {
        //
        // Général
        //

        public Color GeneralBackColor { get; set; }





        //
        // Menu
        //

        public Color MenuBackColor { get; set; }
        public Color MenuForeColor { get; set; }
        public Color MenuSeparatorColor { get; set; }
        public Font MenuFont { get; set; }





        //
        // Opens
        //

        // Header
        public Color OpensHeaderBackColor { get; set; }
        public Color OpensHeaderForeColor { get; set; }
        public Font OpensHeaderFont { get; set; }
        public int OpensHeaderHeight { get; set; }

        // Body
        public Color OpensBodyBackColor { get; set; }

        // Buttons
        public Color OpensButtonsBackColor { get; set; }
        public Color OpensButtonsForeColor { get; set; }
        public Font OpensButtonsFont { get; set; }

        // Selected button
        public Color OpensSelectedButtonBackColor { get; set; }
        public Color OpensSelectedButtonForeColor { get; set; }
        public Font OpensSelectedButtonFont { get; set; }





        //
        // Repas
        //

        // Header
        public Color RepasHeaderBackColor { get; set; }
        public Color RepasHeaderForeColor { get; set; }
        public Font RepasHeaderFont { get; set; }
        public int RepasHeaderHeight { get; set; }

        // Body
        public Color RepasBodyBackColor { get; set; }

        // Text Boxes
        public Color RepasTextBoxesBackColor { get; set; }
        public Color RepasTextBoxesForeColor { get; set; }
        public Font RepasTextBoxesFont { get; set; }





        //
        // Joueur
        //

        // Header
        public Color JoueurHeaderBackColor { get; set; }
        public Color JoueurHeaderForeColor { get; set; }
        public Font JoueurHeaderFont { get; set; }
        public int JoueurHeaderHeight { get; set; }
        public Color JoueurHeaderConfirmedForeColor { get; set; }

        // Body
        public Color JoueurBodyBackColor { get; set; }

        //  Boxes
        public Color JoueurBoxesBackColor { get; set; }
        public Color JoueurBoxesForeColor { get; set; }
        public Font JoueurBoxesFont { get; set; }

        //  Boxes Place Holder
        public Color JoueurBoxesPlaceHolderForeColor { get; set; }
        public Font JoueurBoxesPlaceHolderFont { get; set; }

        //  Boxes Error
        public Color JoueurBoxesErrorBackColor { get; set; }

        // Labels
        public Color JoueurLabelsForeColor { get; set; }
        public Font JoueurLabelsFont { get; set; }
        public Color JoueurLabelsConfirmedForeColor { get; set; }

        // Confirm Button
        public Color JoueurConfirmButtonBackColor { get; set; }
        public Color JoueurConfirmButtonForeColor { get; set; }
        public Font JoueurConfirmButtonFont { get; set; }

        // Details button
        public Color JoueurDetailsButtonBackColor { get; set; }
        public Color JoueurDetailsButtonForeColor { get; set; }
        public Font JoueurDetailsButtonFont { get; set; }





        //
        // Open
        //

        // Header
        public Color OpenHeaderBackColor { get; set; }
        public Color OpenHeaderForeColor { get; set; }
        public Font OpenHeaderFont { get; set; }
        public int OpenHeaderHeight { get; set; }

        // Body
        public Color OpenBodyBackColor { get; set; }

        // Boxes
        public Color OpenBoxesBackColor { get; set; }
        public Color OpenBoxesForeColor { get; set; }
        public Font OpenBoxesFont { get; set; }

        // Boxes Place Holder
        public Color OpenBoxesPlaceHolderForeColor { get; set; }
        public Font OpenBoxesPlaceHolderFont { get; set; }

        //  Boxes Error
        public Color OpenBoxesErrorBackColor { get; set; }

        // Labels
        public Color OpenLabelsForeColor { get; set; }
        public Font OpenLabelsFont { get; set; }





        //
        // Centre
        //

        // Header
        public Color CentreHeaderBackColor { get; set; }
        public Color CentreHeaderForeColor { get; set; }
        public Font CentreHeaderFont { get; set; }
        public int CentreHeaderHeight { get; set; }

        // Body
        public Color CentreBodyBackColor { get; set; }

        // Joueurs
        public Color CentreBodyJoueursForeColor { get; set; }
        public Font CentreBodyJoueursFont { get; set; }
        public Font CentreBodySelectedJoueursFont { get; set; }
        public Color CentreBodyConfirmedJoueursForeColor { get; set; }
        public Color CentreBodyErroredJoueursForeColor { get; set; }

        // TextBox
        public Color CentreBodyTextBoxBackColor { get; set; }
        public Color CentreBodyTextBoxForeColor { get; set; }
        public Font CentreBodyTextBoxFont { get; set; }
        public Color CentreBodyTextPlaceHolderForeColor { get; set; }
        public Font CentreBodyTextPlaceHolderFont { get; set; }

        // Labels
        public Color CentreBodyLabelForeColor { get; set; }
        public Font CentreBodyLabelFont { get; set; }






        //
        // Actions
        //

        // Header
        public Color ActionsHeaderBackColor { get; set; }
        public Color ActionsHeaderForeColor { get; set; }
        public Font ActionsHeaderFont { get; set; }
        public int ActionsHeaderHeight { get; set; }

        // Body
        public Color ActionsBodyBackColor { get; set; }

        // Buttons
        public Color ActionsButtonsBackColor { get; set; }
        public Color ActionsButtonsForeColor { get; set; }
        public Font ActionsButtonsFont { get; set; }


        //
        // Dialogs
        //

        // Body
        public Color DialogBodyBackColor { get; set; }
        public Color DialogBodyForeColor { get; set; }
        public Font DialogBodyFont { get; set; }

        // Main Buttons
        public Color DialogMainButtonsBackColor { get; set; }
        public Color DialogMainButtonsForeColor { get; set; }
        public Font DialogMainButtonsFont { get; set; }

        // Secondary Buttons
        public Color DialogSecondaryButtonsBackColor { get; set; }
        public Color DialogSecondaryButtonsForeColor { get; set; }
        public Font DialogSecondaryButtonsFont { get; set; }

        // TextBox
        public Color DialogTextBoxBackColor { get; set; }
        public Color DialogTextBoxForeColor { get; set; }
        public Font DialogTextBoxFont { get; set; }
        public Color DialogTextBoxPlaceHolderForeColor { get; set; }
        public Font DialogTextBoxPlaceHolderFont { get; set; }
        public Color DialogTextBoxErrorBackColor { get; set; }


        //
        // Quick Dialog
        //

        public Font QuickDialogFont { get; set; }
        
        // Info
        public Color QuickDialogInfoBackColor { get; set; }
        public Color QuickDialogInfoForeColor { get; set; }

        // Success
        public Color QuickDialogSuccessBackColor { get; set; }
        public Color QuickDialogSuccessForeColor { get; set; }

        // Warning
        public Color QuickDialogWarningBackColor { get; set; }
        public Color QuickDialogWarningForeColor { get; set; }

        // Error
        public Color QuickDialogErrorBackColor { get; set; }
        public Color QuickDialogErrorForeColor { get; set; }


        //
        // Status Panel
        //

        public Color StatusBackColor { get; set; }

        // Title
        public Color StatusTitleForeColor { get; set; }
        public Font StatusTitleFont { get; set; }

        // Message
        public Color StatusMessageForeColor { get; set; }
        public Font StatusMessageFont { get; set; }

        // Tip
        public Color StatusTipForeColor { get; set; }
        public Font StatusTipFont { get; set; }

        // Buttons
        public Color StatusButtonBackColor { get; set; }
        public Color StatusButtonForeColor { get; set; }
        public Font StatusButtonFont { get; set; }




        public ThemeStyle() { }
    }
}
