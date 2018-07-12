using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AnjaliMIS
{
    public static class HtmlHelpers
    {
        //Extensibl method MenuLink
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            //Create a tag, that will be an action link inside a "li" element
            TagBuilder builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
            };
            //Get current routes from the context of the view.
            RouteData route = htmlHelper.ViewContext.RouteData;
            //Gets the controller and the action from the route data
            string action = route.GetRequiredString("action");
            string controller = route.GetRequiredString("controller");
            //If the current controller and action are the same as the element on the navbar menu,
            //then we need to add the class "active" on the tag.
            if (controllerName == controller && actionName == action)
            {
                builder.AddCssClass("active");
            }
            return new MvcHtmlString(builder.ToString());
        }
    }
}