using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore<IdentityRole> store)
        : base(store)
        { }
        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options,
        IOwinContext context)
        {
            return new RoleManager(new
            RoleStore<IdentityRole>(context.Get<IdentityContext>()));
        }
    }
}
