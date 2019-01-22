using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomPanels
{
    interface IChesstionPanel
    {
        /// <summary>
        /// Lance la création du <see cref="Panel"/>, de ses <see cref="Control"/> et des évènements associés.
        /// </summary>
        void Init();

        /// <summary>
        /// Redimensionne et replace le panel sur le forme dynamiquement.
        /// </summary>
        void RelocateAndResize();
    }
}
