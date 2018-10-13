using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Andgasm.Web.CUIT.Core
{
    public class KendoAutomationMap : WebAutomationMap
    {
        public KendoAutomationMap(string url, string browser, string btitle) : base(url, browser, btitle)
        {
        }

        public HtmlControl SelectFromDropdown(string sclass, string data, string ddname = "Operator")
        {
            // TODO: Hack just now - kendo combo boxes are weird!!
            // find dropdown and clck
            HtmlSpan input = new HtmlSpan(BrowserWindow);
            input.SearchProperties[HtmlEdit.PropertyNames.Class] = sclass;
            input.SearchProperties[HtmlEdit.PropertyNames.TagName] = "SPAN";
            input = input.FindMatchingControls().First(x => x.FriendlyName == ddname) as HtmlSpan; // defaults for filter dd

            var n = input.GetChildren();
            var k = n[1].GetChildren(); // options
            foreach (var v in k)
            {
                var tv = v as HtmlControl;
                if (tv.ValueAttribute == data)
                {
                    Keyboard.SendKeys("{ENTER}");
                    return tv;
                }
                else Keyboard.SendKeys("{DOWN}");
            }
            return input;
        }
    }
}
