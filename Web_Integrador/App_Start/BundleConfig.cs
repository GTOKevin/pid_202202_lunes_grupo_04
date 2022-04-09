using System.Web;
using System.Web.Optimization;

namespace Web_Integrador
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/vendor/libs/jquery/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                "~/Assets/vendor/libs/popper/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Assets/vendor/js/bootstrap.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/scrollbar").Include(
                        "~/Assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js"));



        }
    }
}
