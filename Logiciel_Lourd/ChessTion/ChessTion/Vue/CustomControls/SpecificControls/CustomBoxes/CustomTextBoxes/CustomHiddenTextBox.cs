using ChessTion.Vue.CustomControls.GeneralControls.CustomTextBoxes;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomBoxes.CustomTextBoxes
{
    /// <summary>
    /// Classe vue gérant une text box qui gère une info et une référence.
    /// </summary>
    abstract class CustomHiddenTextBox : HiddenTextBox
    {
        /// <summary>
        /// Info gérée par la combo box.
        /// </summary>
        public string Info
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        /// <summary>
        /// Référence gérée par a combo box.
        /// </summary>
        public virtual int Ref
        {
            get
            {
                return (int)(Tag ?? -1);
            }
            set
            {
                Tag = value;
            }
        }

        

    }
}
