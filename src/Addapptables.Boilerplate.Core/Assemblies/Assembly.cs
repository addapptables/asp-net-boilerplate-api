using System;
using System.Collections.Generic;
using System.Linq;

namespace Addapptables.Boilerplate.Assemblies
{
    public class Assembly : IAssembly
    {
        public IList<Type> GetAssembliesByType(Type type)
        {
            return AppDomain.CurrentDomain
            .GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();
        }
    }
}
