using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamMvc.Web.Startup))]
namespace ExamMvc.Web
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
