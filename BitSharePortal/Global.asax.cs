﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BitSharePortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "FileDownload", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Torrents", action = "Download", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "TorrentDetail", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Torrents", action = "Detail", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
               "UserProfile", // Route name
               "{controller}/{action}/{id}", // URL with parameters
               new { controller = "Users", action = "Profile", id = UrlParameter.Optional } // Parameter defaults
           );

            routes.MapRoute(
              "ListaTorrentsFilme", // Route name
              "{controller}/{action}/{id}", // URL with parameters
              new { controller = "Torrents", action = "ListaTorrentsFilme", id = UrlParameter.Optional } // Parameter defaults
          );

          routes.MapRoute(
             "SearchDirector", // Route name
             "{controller}/{action}/{id}", // URL with parameters
             new { controller = "Search", action = "Director", id = UrlParameter.Optional } // Parameter defaults
         );
            routes.MapRoute(
             "SearchActor", // Route name
             "{controller}/{action}/{id}", // URL with parameters
             new { controller = "Search", action = "Actor", id = UrlParameter.Optional } // Parameter defaults
         );
            routes.MapRoute(
             "SearchGenre", // Route name
             "{controller}/{action}/{id}", // URL with parameters
             new { controller = "Search", action = "Genre", id = UrlParameter.Optional } // Parameter defaults
         );
            routes.MapRoute(
             "SearchYear", // Route name
             "{controller}/{action}/{id}", // URL with parameters
             new { controller = "Search", action = "Year", id = UrlParameter.Optional } // Parameter defaults
         );
            routes.MapRoute(
             "SearchMovie", // Route name
             "{controller}/{action}/{id}", // URL with parameters
             new { controller = "Search", action = "Movie", id = UrlParameter.Optional } // Parameter defaults
         );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}