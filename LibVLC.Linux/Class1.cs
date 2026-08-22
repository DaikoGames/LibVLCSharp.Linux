using CliWrap;
using System.Diagnostics;
using GnomeStack.OS.Release;
using System.Threading.Tasks;

namespace LibVLC.Linux
{
    public class LibVLCLinux
    {
        public async Task InstallVLC()
        {
            try
            {
                var OperatingSystemName = OsRelease.Current.Id?.ToLowerInvariant();

                if (OperatingSystemName == "debian" || OperatingSystemName == "ubuntu" || OperatingSystemName == "linuxmint")
                {

                    await Cli.Wrap("pkexec").WithArguments($"bash -c \"apt update && apt install -y vlc libvlc-dev\"").ExecuteAsync();
                }

                if (OperatingSystemName == "opensuse" || OperatingSystemName == "opensuse-leap" || OperatingSystemName == "opensuse-tumbleweed")
                {
                    await Cli.Wrap("pkexec").WithArguments($"bash -c \"zypper install -y vlc libvlc-devel\"").ExecuteAsync();
                }

                if (OperatingSystemName == "gentoo")
                {
                    await Cli.Wrap("pkexec").WithArguments($"bash -c \"emerge -av vlc\"").ExecuteAsync();
                }

                if (OperatingSystemName == "fedora")
                {
                    string fedoraCmd = "dnf install -y https://download1.rpmfusion.org/free/fedora/rpmfusion-free-release-$(rpm -E %fedora).noarch.rpm https://download1.rpmfusion.org/nonfree/fedora/rpmfusion-nonfree-release-$(rpm -E %fedora).noarch.rpm vlc libvlc-devel";
                    await Cli.Wrap("pkexec").WithArguments($"bash -c \"{fedoraCmd}\"").ExecuteAsync();
                }

                if (OperatingSystemName == "arch" || OperatingSystemName == "manjaro")
                {
                    await Cli.Wrap("pkexec").WithArguments($"bash -c \"pacman -S --noconfirm vlc\"").ExecuteAsync();
                }

                if (OperatingSystemName == "rhel" || OperatingSystemName == "almalinux" || OperatingSystemName == "rocky")
                {
                    string rhelCmd = "yum install -y epel-release https://download1.rpmfusion.org/free/el/rpmfusion-free-release-$(rpm -E %rhel).noarch.rpm vlc libvlc-devel";
                    await Cli.Wrap("pkexec").WithArguments($"bash -c \"{rhelCmd}\"").ExecuteAsync();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LibVLC.Linux - Error:");
                Trace.WriteLine("Either you wanted to run this on a non-Linux OS, the Linux Distro you chose does not exist for VLC, or you wrote the Distro name wrong.");
                Trace.WriteLine("Distro IDs are: debian, ubuntu, linuxmint, opensuse, gentoo, fedora, arch, rhel");
                Trace.WriteLine($"Exception Details: {ex.Message}");
                Trace.WriteLine("Another possibility is that I wrote the code wrong, if that is the case, please report the issue on Github. We can fix it :).");
            }
        }
    }
}