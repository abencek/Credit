using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;

using Credit.Web.Models;



namespace Credit.Web.TagHelpers{

    /// <summary>
    /// Pagination for customer and agreement table in Customer and Agreement view
    /// </summary>

    [HtmlTargetElement ("nav",Attributes="page-model")]
    public class PageNavigationTagHelper:TagHelper {

        private readonly IUrlHelperFactory urlHelperFactory;
    
        public PageNavigationTagHelper (IUrlHelperFactory helperFactory){
            urlHelperFactory=helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext{get;set;}

        public PagingInfoViewModel PageModel {get;set;}
  
        public object PageFilter {get;set;}

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int currentPage = PageModel.CurrentPage==0 ? 1 : PageModel.CurrentPage;
            int firstPage = 1;
            int lastPage = PageModel.TotalPages;

            firstPage = Math.Max(currentPage - (currentPage==lastPage? 2 : 1), firstPage);
            lastPage = Math.Min(Math.Max(currentPage + 1, 3), lastPage);  //show max 3 links
            
            Dictionary<string,string> filter = SerializeFilterProperties();

            IUrlHelper urlHelper=urlHelperFactory.GetUrlHelper(ViewContext);

            //Create HTML elements
            TagBuilder ul = new("ul");
            ul.AddCssClass("pagination justify-content-end");

            for (int i = firstPage; i <= lastPage; i++)
            {
                TagBuilder li = new("li");
                li.AddCssClass("page-item");
                if (currentPage == i) {
                     li.AddCssClass("active");
                }
                TagBuilder a = new("a");
                a.AddCssClass("page-link");
                filter["page"]=i.ToString();
                a.Attributes["href"]=urlHelper.Action(
                    ViewContext.RouteData.Values["action"].ToString(),
                    ViewContext.RouteData.Values["controller"].ToString(), 
                    filter);
                a.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
              
            output.Content.AppendHtml(ul);
        }


        private Dictionary<string,string> SerializeFilterProperties(){

            Dictionary<string,string> dictionary = PageFilter.GetType().GetProperties()
                .Where(x => (x.GetValue(PageFilter)??null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(PageFilter).ToString());
            dictionary.Add("page","1");

            return dictionary;
            
        }

    }

}
