using CliWrap;
using System.Diagnostics;

namespace LibVLC.Linux
{
    public class LibVLCLinux()
    {
        public async Task InstallVLC(string OperatingSystemName)
        {
            try
            {
                if (OperatingSystemName == "Debian")
                {
                    await Cli.Wrap("sudo").WithArguments(args => args.Add("apt").Add("install").Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "Ubuntu")
                {
                    await Cli.Wrap("sudo").WithArguments(args => args.Add("snap").Add("install").Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "Mint")
                {
                    await Cli.Wrap("sudo").WithArguments(args => args.Add("apt").Add("install").Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "openSUSE")
                {
                    await Cli.Wrap("sudo").WithArguments(args => args.Add("zypper").Add("install").Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "Gentoo")
                {
                    await Cli.Wrap("emerge").WithArguments(args => args.Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "Fedora")
                {
                    await Cli.Wrap("dnf").WithArguments(args => args.Add("install").Add("https://download1.rpmfusion.org/free/fedora/rpmfusion-free-release").Add("-$").Add("(").Add("rpm").Add("-E").Add("%fedora").Add(")").Add(".noarch.rpm")).ExecuteAsync();
                    await Cli.Wrap("dnf").WithArguments(args => args.Add("install").Add("https://download1.rpmfusion.org/free/fedora/rpmfusion-nonfree-release").Add("-$").Add("(").Add("rpm").Add("-E").Add("%fedora").Add(")").Add(".noarch.rpm")).ExecuteAsync();
                    await Cli.Wrap("dnf").WithArguments(args => args.Add("install").Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "Arch")
                {
                    await Cli.Wrap("pacman").WithArguments(args => args.Add("-S").Add("vlc")).ExecuteAsync();
                }

                if (OperatingSystemName == "Red_Hat_Enterprise")
                {
                    await Cli.Wrap("yum").WithArguments(args => args.Add("install").Add("https://dl.fedoraproject.org/pub/epel/epel-release-latest-8.noarch.rpm")).ExecuteAsync();
                    await Cli.Wrap("yum").WithArguments(args => args.Add("install").Add("https://download1.rpmfusion.org/free/el/rpmfusion-free-release-8.noarch.rpm")).ExecuteAsync();
                    await Cli.Wrap("yum").WithArguments(args => args.Add("install").Add("vlc")).ExecuteAsync();
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine("LibVLCSharp.Linux - Error:");
                Trace.WriteLine("Either you wanted to run this on a non-Linux OS, the Linux Distro you chose does not exist for VLC, or you wrote the Distro name wrong.");
                Trace.WriteLine("Distro Names are: Debian, Ubuntu, Mint, openSUSE, Gentoo, Fedora, Arch, and Red_Hat_Enterprise");
                Trace.WriteLine("Another possibility is that i wrote the code wrong, if that is the case, please report the issue on Github. We can fix it :).");
            }
        }
    }
}
