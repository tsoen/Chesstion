using System;
using System.Runtime.Caching;
using ChessTion.Modele.MTournoi;

namespace ChessTion.CacheSystem
{
    /// <summary>
    /// Classe gérant un cache contenant les erreurs des <see cref="Joueur"/> .
    /// </summary>
    public class CacheManager : ICacheManager
    {
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
        /// Retourn le cache.
        /// </summary>
        private ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
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
        /// Retourne la valeur associée à la clé.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Clé.</param>
        /// <returns></returns>
        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// Ajoute une nouvelle valeur dans le cache.
        /// </summary>
        /// <param name="key">Clé.</param>
        /// <param name="data">Valeur.</param>
        /// <param name="cacheTime">Durée de mise en cache.</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Renvoie vrai si la clé existe.
        /// </summary>
        /// <param name="key">Clé.</param>
        /// <returns></returns>
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// Enlève une valeur du cache.
        /// </summary>
        /// <param name="key">Clé de la valeur à enlever.</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Enlève toutes les valeurs du cache.
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}
