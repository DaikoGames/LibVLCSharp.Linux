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
                var operatingSystemName = OsRelease.Current.Id?.ToLowerInvariant() ?? string.Empty;
                var idLike = OsRelease.Current.IdLike?.ToLowerInvariant() ?? string.Empty;

                Trace.WriteLine($"LibVLC.Linux - ID={operatingSystemName} ID_LIKE={idLike}");

                string command = null;

                if (operatingSystemName == "debian" || operatingSystemName == "ubuntu" || operatingSystemName == "linuxmint"
                    || idLike.Contains("debian") || idLike.Contains("ubuntu"))
                {
                    command = "apt-get update && apt-get install -y vlc libvlc-dev";
                }
                else if (operatingSystemName == "opensuse" || operatingSystemName == "opensuse-leap" || operatingSystemName == "opensuse-tumbleweed"
                         || idLike.Contains("suse"))
                {
                    command = "zypper --non-interactive install vlc libvlc-devel";
                }
                else if (operatingSystemName == "gentoo")
                {
                    command = "emerge -av vlc";
                }
                else if (operatingSystemName == "fedora" || idLike.Contains("fedora"))
                {
                    command = "dnf install -y https://download1.rpmfusion.org/free/fedora/rpmfusion-free-release-$(rpm -E %fedora).noarch.rpm https://download1.rpmfusion.org/nonfree/fedora/rpmfusion-nonfree-release-$(rpm -E %fedora).noarch.rpm vlc libvlc-devel";
                }
                else if (operatingSystemName == "arch" || operatingSystemName == "manjaro" || idLike.Contains("arch"))
                {
                    command = "pacman -Sy --noconfirm vlc";
                }
                else if (operatingSystemName == "rhel" || operatingSystemName == "almalinux" || operatingSystemName == "rocky"
                         || idLike.Contains("rhel"))
                {
                    command = "yum install -y epel-release https://download1.rpmfusion.org/free/el/rpmfusion-free-release-$(rpm -E %rhel).noarch.rpm vlc libvlc-devel";
                }

                if (string.IsNullOrWhiteSpace(command))
                {
                    throw new Exception($"No install command for distro ID='{operatingSystemName}' ID_LIKE='{idLike}'.");
                }

                await Cli.Wrap("/usr/bin/pkexec")
                    .WithArguments(new[]
                    {
                        "/usr/bin/bash",
                        "-c",
                        command
                    })
                    .ExecuteAsync();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LibVLC.Linux - Error:");
                Trace.WriteLine("Either you wanted to run this on a non-Linux OS, the Linux Distro you chose does not exist for VLC, or you wrote the Distro name wrong.");
                Trace.WriteLine("Distro IDs are: debian, ubuntu, linuxmint, opensuse, gentoo, fedora, arch, rhel");
                Trace.WriteLine($"Exception Details: {ex.Message}");
                Trace.WriteLine(ex.ToString());
                Trace.WriteLine("Another possibility is that I wrote the code wrong, if that is the case, please report the issue on Github. We can fix it :).");
            }
        }
    }
}
