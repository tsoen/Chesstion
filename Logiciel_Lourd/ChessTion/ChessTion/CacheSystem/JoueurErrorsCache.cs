using System.Collections.Generic;
using ChessTion.Controleur.CTournoi;

namespace ChessTion.CacheSystem
{
    class JoueurErrorsCache : CacheManager
    {
        public virtual List<string[]> Get(int reference)
        {
            if (!IsSet(reference))
                Set(reference, GJoueur.ComporteDesErreurs(reference));

            return Get<List<string[]>>(reference.ToString());
        }
        public virtual void Set(int reference, List<string[]> errors, int cacheTime = 60)
        {
            Set(reference.ToString(), errors, cacheTime);
        }
        public virtual bool IsSet(int reference)
        {
            return IsSet(reference.ToString());
        }
        public virtual void Remove(int reference)
        {
            Remove(reference.ToString());
        }
    }
}
