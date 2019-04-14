using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DAL
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
                : base(store)
        {
        }
        public static UserManager Create(IdentityFactoryOptions<UserManager> options,
                                                IOwinContext context)
        {
            IdentityContext db = context.Get<IdentityContext>();
            UserManager manager = new UserManager(new UserStore<User>(db));
            return manager;
        }
    }
}
