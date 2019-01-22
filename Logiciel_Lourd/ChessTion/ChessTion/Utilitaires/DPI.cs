using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessTion.Utilitaires
{
    /// <summary>
    /// Singleton gérant les différents DPI des écrans.
    /// </summary>
    class DPI
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
        /// Instance de la classe.
        /// </summary>
        private static DPI instance;

        /// <summary>
        /// Formulaire géré par la classe.
        /// </summary>
        private static Form form;

        /*************************************************************************************
         *   ___  _____  _  _  ___  ____  ____  __  __   ___  ____  ____  __  __  ____  ___  *
         *  / __)(  _  )( \( )/ __)(_  _)(  _ \(  )(  ) / __)(_  _)( ___)(  )(  )(  _ \/ __) *
         * ( (__  )(_)(  )  ( \__ \  )(   )   / )(__)( ( (__   )(   )__)  )(__)(  )   /\__ \ *
         *  \___)(_____)(_)\_)(___/ (__) (_)\_)(______) \___) (__) (____)(______)(_)\_)(___/ *
         *                                                                                   *
         *                      Ensemble des constructeurs de la classe.                     *
         *                                                                                   *
         *************************************************************************************/

        /// <summary>
        /// Constructeur privé.
        /// </summary>
        /// <param name="form">Formulaire géré par la classe.</param>
        private DPI(Form form)
        {
            DPI.form = form;
        }

        /// <summary>
        /// L'instance actuellement gérée.
        /// </summary>
        public static DPI Instance
        {
            get
            {
                if (instance == null)
                    CreateInstance(new Form());
                return instance;
            }
        }

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
        /// DPI sur lequel a été créé le formulaire.
        /// </summary>
        public static PointF BaseDPI
        {
            get { return new PointF(120, 120); }
        }

        /// <summary>
        /// DPI du client.
        /// </summary>
        public PointF CurrentDPI
        {
            get
            {
                Graphics graphics = form.CreateGraphics();
                return new PointF(graphics.DpiX, graphics.DpiY);
            }
        }

        /// <summary>
        /// Valeurs par lesquelles multiplié le DPI client pour obtenir le même résultat que sur le DPI de base.
        /// </summary>
        public PointF RelativeMultiplier
        {
            get
            {
                return new PointF(CurrentDPI.X / BaseDPI.X, CurrentDPI.Y / BaseDPI.Y);
            }
        }

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
        /// <see cref="RelativeMultiplier"/> appliqué à un <see cref="Point"/>.
        /// </summary>
        /// <param name="point">Point à multiplier.</param>
        /// <returns>Le Point multiplié.</returns>
        public Point MultipliedPoint(Point point)
        {
            return MultipliedPoint(new PointF(point.X, point.Y));
        }

        /// <summary>
        /// <see cref="RelativeMultiplier"/> appliqué à un <see cref="PointF"/>.
        /// </summary>
        /// <param name="point">Point à multiplier.</param>
        /// <returns>Le Point multiplié.</returns>
        public Point MultipliedPoint(PointF point)
        {
            return Point.Round(new PointF(point.X * RelativeMultiplier.X, point.Y * RelativeMultiplier.Y));
        }

        /// <summary>
        /// <see cref="RelativeMultiplier"/> appliqué à un <see cref="Point"/>.
        /// </summary>
        /// <param name="x">Coordonnée X du <see cref="Point"/>.</param>
        /// <param name="y">Coordonnée Y du <see cref="Point"/>.</param>
        /// <returns>Le Point multiplié.</returns>
        public Point MultipliedPoint(int x, int y)
        {
            return MultipliedPoint(new PointF(x, y));
        }

        /// <summary>
        /// <see cref="RelativeMultiplier"/> appliqué à un <see cref="PointF"/>.
        /// </summary>
        /// <param name="x">Coordonnée X du <see cref="PointF"/>.</param>
        /// <param name="y">Coordonnée Y du <see cref="PointF"/>.</param>
        /// <returns>Le Point multiplié.</returns>
        public Point MultipliedPoint(float x, float y)
        {
            return MultipliedPoint(new PointF(x, y));
        }

        /// <summary>
        /// <see cref="RelativeMultiplier"/> appliqué à un <see cref="Size"/>.
        /// </summary>
        /// <param name="x">Coordonnée X de la <see cref="Size"/>.</param>
        /// <param name="y">Coordonnée Y de la <see cref="Size"/>.</param>
        /// <returns>La Size multipliée.</returns>
        public Size MultipliedSize(int x, int y)
        {
            return new Size((int)(x * RelativeMultiplier.X), (int)(y * RelativeMultiplier.Y));
        }

        /// <summary>
        /// Crée une nouvelle instance.
        /// </summary>
        /// <param name="form">Le formulaire à gérer.</param>
        /// <returns></returns>
        public static DPI CreateInstance(Form form)
        {
            if (instance == null)
                instance = new DPI(form);

            return instance;
        }
    }
}
