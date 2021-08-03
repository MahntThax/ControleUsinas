using Microsoft.AspNetCore.Mvc;

namespace ControleUsinas.Helpers
{
    public static class RouteHelper
    {
        public static string NameofController<T>() where T: Controller => typeof(T).Name.Replace(nameof(Controller), string.Empty);
    }
}
