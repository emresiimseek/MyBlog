﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Mvc.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterRoutes(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControlerFactory(new BusinessModule()));
        }
        //protected void Application_BeginRequest()
        //{
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        //    Response.Cache.SetNoStore();
        //}
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;


            base.Init();
        }
        protected void Session_Start(object sender, EventArgs e)
        {
        }


        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }
                var encTicket = authCookie.Value;
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }
                var ticket = FormsAuthentication.Decrypt(encTicket);
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormAuthTicketToIdentity(ticket);
                var principal = new GenericPrincipal(identity, identity.Roles);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;

            }
            catch (Exception)
            {


            }

        }
    }
}
