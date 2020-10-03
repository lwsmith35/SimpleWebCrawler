using Microsoft.Extensions.Logging;
using swc.Function.FindLinks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace swc.Function.FindLinks.Tests
{
    public class FindLinksServiceTests
    {
        [Fact]
        public void ParseLinksReturnsLinks()
        {
            ILogger<FindLinksService> logger = null;
            IHttpClientFactory httpClientFactory = null;
            string htmlContent =
@"<!DOCTYPE html><!-- This site was created in Webflow. http://www.webflow.com --><!-- Last Published: Mon May 27 2019 03:43:16 GMT+0000 (UTC) --><html data-wf-domain='fivesecondtest.com' data-wf-page='5b5554ec45c6a379b44b3a76' data-wf-site='5b5554ec45c6a346364b3a75'><head><meta charset='utf-8'/><title>Five Second Test</title><meta content='The five second test is a simple usability test to optimize the clarity of your designs and improve conversion rates by measuring people&#x27;s first impressions.' name='description'/><meta content='Five Second Test' property='og:title'/><meta content='The five second test is a simple usability test to optimize the clarity of your designs and improve conversion rates by measuring people&#x27;s first impressions.' property='og:description'/><meta content='summary' name='twitter:card'/><meta content='width=device-width, initial-scale=1' name='viewport'/><meta content='Webflow' name='generator'/><link href='https://assets.website-files.com/5b5554ec45c6a346364b3a75/css/fivesecondtest-com.webflow.d6d25518b.min.css' rel='stylesheet' type='text/css'/><script src='https://ajax.googleapis.com/ajax/libs/webfont/1.6.26/webfont.js' type='text/javascript'></script><script type='text/javascript'>WebFont.load({  google: {    families: ['Open Sans:300,300italic,400,400italic,600,600italic,700,700italic,800,800italic']  }});</script><!--[if lt IE 9]><script src='https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.min.js' type='text/javascript'></script><![endif]--><script type='text/javascript'>!function(o,c){var n=c.documentElement,t=' w-mod-';n.className+=t+'js',('ontouchstart'in o||o.DocumentTouch&&c instanceof DocumentTouch)&&(n.className+=t+'touch')}(window,document);</script><link href='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b63d0a6cd934cde8a631a45_favicon.png' rel='shortcut icon' type='image/x-icon'/><link href='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b63d0b9f125747f3bb91392_webclip.png' rel='apple-touch-icon'/><script type='text/javascript'>(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','https://www.google-analytics.com/analytics.js','ga');ga('create', 'UA-8268727-7', 'auto');ga('send', 'pageview');</script><style>
  /* Default fonts */
  body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol' !important; }
</style></head><body class='body'><div class='pagecontainer pagecontainer--centered'><img src='https://assets.website-files.com/5af97a9c84ec1bc79d81b5f4/5af99c764d23b2277ac5227e_five-second-test.svg' alt='' class='headericon'/><h1 class='heading-2 h1'><strong class='bold-text'>Five second tests</strong></h1><div class='text text--center'>Optimize the clarity of your designs by measuring first impressions and recall.</div></div><div class='pagesection'><div class='pagecontainer'><div class='featuresummary'><div class='featuredguide'><img src='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6155306d61a022359d830f_5b44330a05b1ce09edcc052e_home_designer.png' width='550' srcset='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6155306d61a022359d830f_5b44330a05b1ce09edcc052e_home_designer-p-1080.png 1080w, https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6155306d61a022359d830f_5b44330a05b1ce09edcc052e_home_designer.png 1100w' sizes='(max-width: 479px) 85vw, (max-width: 767px) 72vw, (max-width: 991px) 34vw, 38vw' alt='' class='featureimage'/></div><div class='featuresummary-contentcolumn'><div class='featuresummary-content'><h3 class='heading-3'>What are they?</h3><div class='text'>Five second tests are a method of user research that help you measure what information users take away and what impression they get within the first five seconds of viewing a design. They&#x27;re commonly used to test whether web pages are effectively communicating their intended message.<br/></div></div></div></div><div class='featuresummary featuresummary--reversed'><div class='featuresummary-imagecolumn'><div class='featuredguide'><img src='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6154d1989723ffc817b31e_5b44330a6c5dba54a973c215_home_marketer.png' width='551' srcset='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6154d1989723ffc817b31e_5b44330a6c5dba54a973c215_home_marketer-p-500.png 500w, https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6154d1989723ffc817b31e_5b44330a6c5dba54a973c215_home_marketer-p-800.png 800w, https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b6154d1989723ffc817b31e_5b44330a6c5dba54a973c215_home_marketer.png 1102w' sizes='(max-width: 479px) 85vw, (max-width: 767px) 72vw, (max-width: 991px) 310px, 440px' alt='' class='featureimage'/></div></div><div class='featuresummary-contentcolumn'><div class='featuresummary-content'><h3 class='heading-3'>How do they work?</h3><div class='text'>Participants are given five seconds to view a design, after which they answer some simple questions. Before the test starts participants are given a primer on the format and reminded to pay close attention. Depending on the goal of the test, they may also be given some context for what to look out for.</div></div></div></div><div class='featuresummary'><div class='featuredguide'><img src='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b625ad36855d65e139da406_5b44330a049e86cae8ae66e5_home_manager-p-800.png' width='551' alt='' class='featureimage'/></div><div class='featuresummary-contentcolumn'><div class='featuresummary-content'><h3 class='heading-3'>What can I use them for?</h3><div class='text w-richtext'><p>Five second tests are suitable for determining if users&#x27; <em>first impressions</em> of your page are on point.  This includes answering questions like:</p><ul><li>What is the purpose of the page?</li><li>What are the main elements you can recall?</li><li>Who do you think the intended audience is?</li><li>Did the design/brand appear trustworthy?</li><li>What was your impression of the design?</li></ul><p>‍</p></div></div></div></div><div class='featuresummary featuresummary--reversed'><div class='featuredguide'><img src='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b615af517697fd32a5c8292_5b44341e6c5dba702173c24d_panel_fast.png' width='551' srcset='https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b615af517697fd32a5c8292_5b44341e6c5dba702173c24d_panel_fast-p-500.png 500w, https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b615af517697fd32a5c8292_5b44341e6c5dba702173c24d_panel_fast-p-800.png 800w, https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b615af517697fd32a5c8292_5b44341e6c5dba702173c24d_panel_fast-p-1080.png 1080w, https://assets.website-files.com/5b5554ec45c6a346364b3a75/5b615af517697fd32a5c8292_5b44341e6c5dba702173c24d_panel_fast.png 1282w' sizes='(max-width: 479px) 85vw, (max-width: 767px) 72vw, (max-width: 991px) 34vw, 38vw' alt='' class='featureimage'/></div><div class='featuresummary-contentcolumn'><div class='featuresummary-content'><h3 class='heading-3'>Why just five seconds?</h3><div class='text w-richtext'><p>Five seconds is long enough for a good design to communicate its primary message. Furthermore there is a growing trend for website visitors to open many sites at once, reducing the attention each receives and increasing the importance of effective design and messaging.</p><p>‍</p></div></div></div></div></div></div><div><div class='centered w-container'><div><h3 class='heading-3'>Create your own five second tests and start improving your designs today!<br/></h3></div><a href='https://usabilityhub.com/product/five-second-tests' class='button w-button'>Create a five second test here</a></div></div><div class='footer'><div class='footer-subfooter'><div class='pagecontainer'><div class='row-6 w-row'><div class='page-subfooter-legals w-col w-col-10'><a href='https://usabilityhub.com' class='footer-subfooter-link w-inline-block'><img src='https://assets.website-files.com/5af97a9c84ec1bc79d81b5f4/5af980b24d23b2a395c4cfe2_logo.svg' alt='UsabilityHub logo' class='footer-logo'/></a><a href='https://app.usabilityhub.com/privacy' class='footer-subfooter-link'>Privacy policy</a><a href='https://app.usabilityhub.com/terms' class='footer-subfooter-link'>Terms &amp; conditions</a><a href='https://app.usabilityhub.com/security' class='footer-subfooter-link'>Security information</a></div><div class='footer-subfooter-social w-col w-col-2'><a href='https://www.facebook.com/usabilityhubapp/' target='_blank' class='socialicon w-inline-block'><img src='https://assets.website-files.com/5af97a9c84ec1bc79d81b5f4/5afaa86099826ef7882d7c69_facebook.svg' alt='Facebook'/></a><a href='https://twitter.com/usabilityhub' target='_blank' class='socialicon w-inline-block'><img src='https://assets.website-files.com/5af97a9c84ec1bc79d81b5f4/5afaa8a0d0d7b7713d4188df_twitter.svg' alt='Twitter' class='image-6'/></a></div></div></div></div></div><script src='https://d1tdp7z6w94jbb.cloudfront.net/js/jquery-3.3.1.min.js' type='text/javascript' integrity='sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=' crossorigin='anonymous'></script><script src='https://assets.website-files.com/5b5554ec45c6a346364b3a75/js/webflow.48a94c17e.js' type='text/javascript'></script><!--[if lte IE 9]><script src='//cdnjs.cloudflare.com/ajax/libs/placeholders/3.0.2/placeholders.min.js'></script><![endif]--></body></html> ";

            Type type = typeof(FindLinksService);
            var findLinksService = Activator.CreateInstance(type, logger, httpClientFactory);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "ParseLinksFromContent" && x.IsPrivate)
                .First();

            var links = (IEnumerable<(string href, string label)>)method.Invoke(findLinksService, new object[] { htmlContent });

            Assert.True(links.Any());
        }

        [Fact]
        public void ParseLinksHandlesEmptyNull()
        {
            //var findLinksService = new FindLinksService(null, null);
            //findLinksService.ParseLinksFromPage

            ILogger<FindLinksService> logger = null;
            IHttpClientFactory httpClientFactory = null;

            Type type = typeof(FindLinksService);
            var findLinksService = Activator.CreateInstance(type, logger, httpClientFactory);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "ParseLinksFromContent" && x.IsPrivate)
                .First();

            // Test for empty
            string htmlContent = "";
            var links = (IEnumerable<(string href, string label)>)method.Invoke(findLinksService, new object[] { htmlContent });
            Assert.False(links.Any());

            // Test for null
            htmlContent = null;
            links = (IEnumerable<(string href, string label)>)method.Invoke(findLinksService, new object[] { htmlContent });
            Assert.False(links.Any());
        }
    }
}
