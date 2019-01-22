namespace ChessTion.CacheSystem
{
    /// <summary>
    /// Interface de cache.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Retourne la valeur associée à la clé.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Clé.</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Ajoute une valeur au cache.
        /// </summary>
        /// <param name="key">Clé.</param>
        /// <param name="data">Valeur.</param>
        /// <param name="cacheTime">Durée de mise en cache.</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Renvoie vrai si la clé existe.
        /// </summary>
        /// <param name="key">Clé.</param>
        /// <returns></returns>
        bool IsSet(string key);

        /// <summary>
        /// Enlève une valeur du cache.
        /// </summary>
        /// <param name="key">Clé de la valeur à enlever.</param>
        void Remove(string key);

        /// <summary>
        /// Enlève toutes les valeus du cache.
        /// </summary>
        void Clear();
    }
}
