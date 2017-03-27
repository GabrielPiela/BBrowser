using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;
using CefSharp.WinForms.Internals;
using System.IO;

namespace B
{
    public partial class okno : Form
    {
        private StringBuilder _pressedKeys = new StringBuilder();
        
        CefSettings cs = new CefSettings();
        ChromiumWebBrowser name;

        public okno()
        {
            InitializeComponent();
        }


        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BBrowser\\Cache");
            Cef.Initialize(settings);
            cs.CefCommandLineArgs.Add("enable-automatic-password-saving", "enable-automatic-password-saving");
            cs.CefCommandLineArgs.Add("enable-password-save-in-page-navigation", "enable-password-save-in-page-navigation");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
            Application.Exit();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("1");
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            tabControl1.SelectedTab.Text = "       New tab       ";
            InitializeChromium();
            name = new ChromiumWebBrowser("google.com");
            name.Parent = tabControl1.SelectedTab;
            name.Dock = DockStyle.Fill;
            name.TitleChanged += OnBrowserTitleChanged;
            name.AddressChanged += OnBrowserAddressChanged;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (name != null)
            {
                if (textBox1.Text != "")
                {
                    if (textBox1.Text.IndexOf("//") != -1)
                    {
                        name.Load(textBox1.Text);
                    }
                    else
                    {
                        name.Load("http://" + textBox1.Text);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (name != null)
                name.Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (name!=null)
                name.Forward();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tabControl1.Controls.Add(tab);
            tab.Text = "       New tab       ";
            tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
            name = new ChromiumWebBrowser("google.com");
            name.Parent = tab;
            name.Dock = DockStyle.Fill;
            name.TitleChanged += OnBrowserTitleChanged;
            name.AddressChanged += OnBrowserAddressChanged;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (name != null)
            {
                name.Load("https://www.google.pl/search?q=" + textBox2.Text);
                textBox2.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (name != null && tabControl1.TabPages.Count > 1)
            {
                tabControl1.SelectedIndex = tabControl1.SelectedIndex - 1;
                tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count - 1);
                name.Dispose();
            }
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            int x = tabControl1.SelectedIndex;
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
            tabControl1.SelectedTab.Text = "       " + this.Text + "       ";
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() =>textBox1.Text = args.Address);
        }

        private void textbox1_KeyUp(object sender, KeyEventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (e.KeyCode == Keys.Enter)
            {        
                if (name != null)
                {
                    if (textBox1.Text != "")
                    {
                        if (textBox1.Text.IndexOf("//") != -1)
                        {
                            name.Load(textBox1.Text);
                        }
                        else
                        {
                            name.Load("http://" + textBox1.Text);
                        }
                    }
                }
            }
        }

        private void textbox2_KeyUp(object sender, KeyEventArgs e)
        {
            ChromiumWebBrowser name = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (e.KeyCode == Keys.Enter)
            {
                if (name != null)
                {
                    name.Load("https://www.google.pl/search?q=" + textBox2.Text);
                    textBox2.Text = "";
                }
            }           
        }
    }
}