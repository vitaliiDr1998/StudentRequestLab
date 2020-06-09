using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation
{
    public interface IContainer
    {
        void Register<TTypeToResolve, TConcrete>();
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle);
        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type typeToResolve);
    }
}