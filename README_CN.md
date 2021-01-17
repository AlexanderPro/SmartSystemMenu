![SmartSystemMenu](https://user-images.githubusercontent.com/8102586/68280906-8e86b800-0087-11ea-9762-f9eb028bb8fe.png) SmartSystemMenu
=============

- [English](/README.md)
- [Русский](/README_RU.md)
- 中文版

---

SmartSystemMenu 扩展了系统中所有窗口的系统菜单。 它会将下面的自定义项目追加到菜单:

* **信息.** 显示一个对话框，其中包含当前窗口和进程的信息：窗口句柄、窗口标题、窗口风格、窗口类、进程名称、进程ID、进程路径。
* **卷起.** 允许将当前窗口向上卷起。
* **毛玻璃效果.** 允许将 "毛玻璃效果" 模糊添加到当前窗口。(Windows Vista 及更高版本。主要用于控制台窗口。)
* **窗口置顶.** 允许当前窗口位于所有其他窗口之上。
* **移至最底层.** 允许将当前窗口发送到底部。
* **保存窗口截图.** 允许将当前窗口的屏幕截图保存到文件中。
* **在资源管理器中打开文件.** 允许在文件资源管理器中打开进程文件。
* **将文本复制到剪贴板.** 允许复制所有窗口文本 (包括控制台、MS Office 产品等) 到剪贴板中。
* **通过鼠标拖动.** 允许通过鼠标拖动当前窗口。
* **调整窗口大小.** 允许更改当前窗口的大小。
* **移动到.** 允许将当前窗口移动到另一个显示器。
* **对齐.** 允许当前窗口与桌面上的9个位置中的任何一个对齐。
* **透明度.** 允许更改当前窗口的透明度。
* **优先级.** 允许更改当前窗口的程序优先级。
* **系统托盘.** 允许将当前窗口最小化到系统托盘。
* **其他窗口.** 允许关闭和最小化系统中除当前窗口之外的所有窗口。
* **启动程序.** 允许启动设置中的程序。

截图
------------------

![Resize](https://cdn.jsdelivr.net/gh/LightAPIs/PicGoImg@master/img/20201229214044.png)

![Alignment](https://cdn.jsdelivr.net/gh/LightAPIs/PicGoImg@master/img/20201229214127.png)

![Transparency](https://cdn.jsdelivr.net/gh/LightAPIs/PicGoImg@master/img/20201229214204.png)

要求
--------------------

* OS Windows XP SP3 及更高版本。 支持 x86 和 x64 系统。
* .NET Framework 4.0

程序文件
--------------------

* SmartSystemMenu.exe
* SmartSystemMenu64.exe (位于 SmartSystemMenu.exe 模块的资源中)
* SmartSystemMenuHook.dll
* SmartSystemMenuHook64.dll
* SmartSystemMenu.xml
* Language.xml

此程序具有用于 x86 进程的 SmartSystemMenu.exe 和 SmartSystemMenuHook.dll 模块，用于 x64 进程的 SmartSystemMenu64.exe 和 SmartSystemMenuHook64.dll 模块。当您运行 SmartSystemMenu.exe 时，它还会运行 SmartSystemMenu64.exe。 这两个可执行模块将挂钩 (SmartSystemMenuHook.dll 和 SmartSystemMenuHook64.dll) 加载到所有进程。 当您在系统菜单中选择一项时，挂钩会向可执行模块发送一条消息。之后，模块执行选定的操作：更改窗口的透明度、更改窗口的大小等等。

局限性
--------------------

此工具无法在系统菜单由其自己的进程管理的窗口上正常工作，例如 IE9 及更高版本、Chrome 29 及更高版本等。此外，该工具不能与 Delphi 进程的窗口一起正常工作，因为 Delphi 窗口有一个父 TApplication 窗口。

提示
--------------------

运行 SmartSystemMenu.exe 进程。如果您的操作系统启用了 UAC，系统将显示 UAC 对话框。您不必担心，因为程序需要提升权限。程序执行后，在所有窗口的所有系统菜单中都可以看到自定义项目。

## 关于中文语言

在第一次运行 SmartSystemMenu.exe 时，会自动检测系统语言环境，若为简体中文或繁体中文，自动应用对应的简体中文或繁体中文语言，后续亦可进入设置中修改显示语言，重新启动应用后生效。

