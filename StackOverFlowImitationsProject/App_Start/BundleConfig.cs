using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace StackOverFlowImitationsProject
{
    public class BundleConfig
    {
        /// <summary>
        /// this method needs to be invoked in the global.asax file with " App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);"
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundle names are user defined

            //creating a bundle called "bootstrap" in scripts, comprising of 3 js files that are sufficient to run bootstrap
            bundles.Add(new Bundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.6.0.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
        
            //creating a bundle for styles
            bundles.Add(new Bundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css"));

            //you are free to add styles or scrips as you wish to the bundles, but the above are the base ones for bootstrap
            //for example, add a Web file in "Content" of the main application names Styles.css, then create a bundle for it:
            bundles.Add(new Bundle("~/Styles/site").Include("~/Content/Styles.css"));

            //after creation of the bundles, we need to enable optimization:
            BundleTable.EnableOptimizations = true;

        }
    }
}