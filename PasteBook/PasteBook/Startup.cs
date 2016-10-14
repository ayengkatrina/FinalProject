using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PasteBook.Startup))]
namespace PasteBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
