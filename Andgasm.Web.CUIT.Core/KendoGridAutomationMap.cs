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
    public class KendoGridAutomationMap : KendoAutomationMap
    {
        public KendoGridAutomationMap(string url, string browser, string btitle) : base(url, browser, btitle)
        {
        }

        public HtmlHyperlink ClickLinkByClassForColumnName(string parentfname, string sclass, int pause = 0)
        {
            HtmlHyperlink button = new HtmlHyperlink(BrowserWindow);
            button.SearchProperties[HtmlEdit.PropertyNames.Class] = sclass;
            button.SearchProperties[HtmlEdit.PropertyNames.TagName] = "A";
            button = button.FindMatchingControls().Where(x => x.Enabled == true)
                .First(x => x.GetParent().FriendlyName == parentfname) as HtmlHyperlink;
            Mouse.Click(button);
            Thread.Sleep(pause);
            return button;
        }

        public string FindSortStatusForColumnName(string parentfname)
        {
            // find for asc
            var ascicon = FindSpanByClassForParentName("k-icon k-i-sort-asc-sm", parentfname);
            if (ascicon != null) return "asc";
            else
            {
                // find for desc
                var descicon = FindSpanByClassForParentName("k-icon k-i-sort-asc-sm", parentfname);
                if (descicon != null) return "desc";
                else return "na";
            }
        }

        public void FilterGrid(string column, string op, string data)
        {
            ClickLinkByClassForColumnName(column, "k-grid-filter", 500);
            SelectFromDropdown("k-widget k-dropdown k-header", op);
            TypeToTextboxByTitle("Value", data);
            ClickButtonByInnerText("Filter", 1000);
        }

        // kendo grid helper
        public void OrderGrid(string column, string dir)
        {
            // 1 click = asc
            // 2 click = desc
            // 3 click = no sort
            var curorder = FindSortStatusForColumnName(column);
            if (curorder == "desc")
            {
                if (dir == "") ClickLinkByClassForColumnName(column, "k-link", 500);
                else if (dir == "asc")
                {
                    ClickLinkByClassForColumnName(column, "k-link", 500);
                    ClickLinkByClassForColumnName(column, "k-link", 500);
                }
            }
            else if (curorder == "asc")
            {
                if (dir == "")
                {
                    ClickLinkByClassForColumnName(column, "k-link", 500);
                    ClickLinkByClassForColumnName(column, "k-link", 500);
                }
                else if (dir == "desc") ClickLinkByClassForColumnName(column, "k-link", 500);
            }
            else
            {
                if (dir == "asc") ClickLinkByClassForColumnName(column, "k-link", 500);
                else if (dir == "desc")
                {
                    ClickLinkByClassForColumnName(column, "k-link", 500);
                    ClickLinkByClassForColumnName(column, "k-link", 500);
                }
            }
        }
    }
}
