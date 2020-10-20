# CheatCodeLite
Cheat code keystrokes<br>

<img src="logo.png" width="100">

[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/melharfi/MCheatCodeLite/blob/master/LICENSE)
[![Version](https://badge.fury.io/gh/tterb%2FHyde.svg)](https://github.com/melharfi/MCheatCodeLite)
![GitHub Release Date](https://img.shields.io/github/release-date/melharfi/CheatCodeLite?color=black)
[![GitHub Release](https://img.shields.io/github/v/release/melharfi/CheatCodeLite)](https://github.com/melharfi/CheatCodeLite/releases) 
[![PayPal](https://img.shields.io/badge/paypal-donate-yellow.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VN92ND2CDMX92)
[![Nuget](https://img.shields.io/nuget/v/melharfi?color=red)](https://www.nuget.org/packages/MELHARFI/)
![GitHub language count](https://img.shields.io/github/languages/count/melharfi/CheatCodeLite?color=red)

Hello, this is a library that allow you to trigger some cheat codes in your game/application.<br>
The keystrokes handler is not included in this library cause it really depend on the context where this library is used, most of time games use a third party to catch keystrokes not the one in the .Net Framework Class Library.<br>

Just add a reference to the library in the release https://github.com/melharfi/CheatCodeLite/releases
or using nuget

one it's done you can then use this code to start :

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheatCodeLite;

namespace winform_test
{
    public partial class Form1 : Form
    {
        readonly CheatCodeLite.CheatCodeLite cch;
        public Form1()
        {
            InitializeComponent();
            cch = new CheatCodeLite.CheatCodeLite(10000000);

            ChainePattern cp1 = new ChainePattern(new List<int> { (int)Keys.D, (int)Keys.O, (int)Keys.O, (int)Keys.M }, "DOOM");
            cch.AddChainePattern(cp1);

            ChainePattern cp2 = new ChainePattern(new List<int> { (int)Keys.H, (int)Keys.E, (int)Keys.L, (int)Keys.L }, "HELLO");
            cch.AddChainePattern(cp2);

            cch.AddedPattern += CheatCodeHandler_AddedPattern;

            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            cch.AddKeystroke(e.KeyValue);
        }


        private void CheatCodeHandler_AddedPattern(object sender, PatternEventHandler e)
        {
            MessageBox.Show(e.ChainePattern.Alias + " cheat code has been triggered", ":-)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


