# CheatCodeLite
Cheat code keystrokes<br>

<img src="resources/logo.png" width="100">

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

Once it's done you can then use this code to start :


<img src="resources/sample1.PNG"><br>
<img src="resources/sample2.PNG"><br>

Pay attention that the value passed to the CheatCodeLite.CheatCodeLite(int keystrokesInterval) is the time allowed to keep chained cheat code, if this time is overwhelmed then the cheat code in progress is reseted.<br>
To desable this timer just put a very long value like 100000000 :)<br>

Pay attention to some scenarios that will raise some error.
When a cheat code is registred like "HELLO" in the first hand, and another cheat code is registred like "HELL", in that case the second cheat code will hide the first one because cheat code will be reset as soon as it trigger the "HELL" event, so to avoid that an exception will be thrown to prevent and warn you about that.
<img src="resources/throwError1.PNG"><br>

Same scenarion when the revers happen, when you first add a cheat code like "HELL" and that you just another one like "HELLO", then the second one will be hidden by the first one.
<img src="resources/throwError2.PNG"><br>

Same thing when a duplication is found.<br>
<img src="resources/throwError3.PNG"><br>

Hope it helps someone
