using Board_Game_Stranica_N_.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Board_Game_Stranica_N_.Startup))]
namespace Board_Game_Stranica_N_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //stvaramo defaultne uloge za Registriranog Korisnika i Administratora
        private void NapraviUloge()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Aministrator uloga i defaultni Administrator    
            if (!roleManager.RoleExists("Admin"))
            {

                // Uloga
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Defaultni Administrator                

                var user = new ApplicationUser();
                user.UserName = "jcilar6@gmail.com";
                user.Email = "jcilar6@gmail.com";

                string lozinka = "Lisica12!";

                var napraviKor = UserManager.Create(user, lozinka);

                //Add defaultnog korisnika kao Administrator 
                if (napraviKor.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // stvaramo ulogu Registrirani korisnik  
            //if (!roleManager.RoleExists("RegKorisnik"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "RegKorisnik";
            //    roleManager.Create(role);

            //}

        }
    }


}
