using Abp.Dependency;
using System;
using System.Collections.Generic;

namespace Addapptables.Boilerplate.Assemblies
{
    public interface IAssembly : ITransientDependency
    {
        IList<Type> GetAssembliesByType(Type type);
    }
}
