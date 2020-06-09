using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation
{
    public static class BootStrapper
    {
        public static void Configure(IContainer container)
        {
            container.Register<HomeController, HomeController>();
        }
    }
}