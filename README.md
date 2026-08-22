A project that downloads you everything necessary for LibVLCSharp to work on your Linux Distro :)

Still in early WIP

| Dependency | License |
|:---|:---|
| [CliWrap](https://github.com/Tyrrrz/CliWrap) | [MIT-License](https://github.com/Tyrrrz/CliWrap?tab=MIT-1-ov-file) |
| [GnomeStack.Os.Release](https://www.nuget.org/packages/GnomeStack.Os.Release) | [MIT-License](https://licenses.nuget.org/MIT) |

this is an example on how to use my project in Avalonia projects: 

App.axaml.cs: 

```
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Shared;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LibVLC.Linux;

namespace sb1_sb2_sb3_xml_to_Csharp_converter;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            if (OperatingSystem.IsLinux())
            {
                var installer = new LibVLCLinux();
                installer.InstallVLC();
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}
```

and in the .csproj just add this line: 

```
<PackageReference Include="LibVLC.Linux" Version="1.0.0.15"/>
```
