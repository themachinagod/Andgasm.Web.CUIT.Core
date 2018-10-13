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
    public class WebAutomationMap
    {
        public string TargetBrowser { get; set; }
        public string TargetUrl { get; set; }
        public BrowserWindow BrowserWindow { get; set; }
        public string BrowserTitle { get; set; }

        public WebAutomationMap(string url, string browser, string browsertitle)
        {
            TargetBrowser = browser;
            TargetUrl = url;
            BrowserTitle = browsertitle;
        }

        public void LaunchBroswerAndNavigate()
        {
            BrowserWindow = new BrowserWindow();
            BrowserWindow.CurrentBrowser = "chrome";
            BrowserWindow = BrowserWindow.Launch(TargetUrl);
            BrowserWindow.Maximized = true;
            BrowserWindow.SearchProperties[UITestControl.PropertyNames.Name] = BrowserTitle;
            BrowserWindow.SearchProperties[UITestControl.PropertyNames.ControlType] = "Window";
            BrowserWindow.SearchProperties[UITestControl.PropertyNames.TechnologyName] = "MSAA";
            Thread.Sleep(1000);
        }

        public void CloseBrowser()
        {
            BrowserWindow.Close();
        }

        public HtmlEdit ClickLinkButtonByInnerText(string innerText, int pause = 0)
        {
            HtmlEdit button = new HtmlEdit(BrowserWindow);
            button.SearchProperties[HtmlEdit.PropertyNames.InnerText] = innerText;
            button.SearchProperties[HtmlEdit.PropertyNames.TagName] = "A";
            button.TryFind();
            Mouse.Click(button);
            Thread.Sleep(pause);
            return button;
        }

        public HtmlButton ClickButtonByInnerText(string innerText, int pause = 0)
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.SearchProperties[HtmlEdit.PropertyNames.InnerText] = innerText;
            button.SearchProperties[HtmlEdit.PropertyNames.TagName] = "BUTTON";
            button.TryFind();
            Mouse.Click(button);
            Thread.Sleep(pause);
            return button;
        }

        public HtmlSpan FindSpanByClassForParentName(string sclass, string parentfname)
        {
            HtmlSpan ascsorticon = new HtmlSpan(BrowserWindow);
            ascsorticon.SearchProperties[HtmlEdit.PropertyNames.Class] = sclass;
            var d = ascsorticon.FindMatchingControls().Where(x => x.Enabled == true)
                .FirstOrDefault(x => x.GetParent().FriendlyName == parentfname);
            return (d != null) ? d as HtmlSpan : null;
        }

        public HtmlEdit TypeToTextboxByName(string name, string data, bool doclear = true, int pause = 0)
        {
            // find host and put int data
            HtmlEdit input = new HtmlEdit(BrowserWindow);
            input.SearchProperties[HtmlEdit.PropertyNames.Name] = name;
            input.SearchProperties[HtmlEdit.PropertyNames.TagName] = "INPUT";
            input.TryFind();
            Mouse.Click(input);
            if (doclear) SelectAllAndClear();
            Keyboard.SendKeys(input, data);
            Thread.Sleep(pause);
            return input;
        }

        public HtmlEdit TypeToTextboxByTitle(string title, string data, bool doclear = true, int pause = 0)
        {
            // find host and put int data
            HtmlEdit input = new HtmlEdit(BrowserWindow);
            input.SearchProperties[HtmlEdit.PropertyNames.Title] = title;
            input.SearchProperties[HtmlEdit.PropertyNames.TagName] = "INPUT";
            input.TryFind();
            Mouse.Click(input);
            if (doclear) SelectAllAndClear();
            Keyboard.SendKeys(input, data);
            Thread.Sleep(pause);
            return input;
        }

        public static void SelectAllAndClear()
        {
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys("{DELETE}");
        }
    }
}
