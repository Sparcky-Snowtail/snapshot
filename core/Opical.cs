public static class Opical //(CD, DVD, Blu-ray)
{
    public static int Type { get; set; } = 0; // 0 = NONE, 1 = CD, 2 = DVD, 3 = Blu-ray
    public static int Opticalr { get; set; } = 0; // if current optical disk is ritable or not, 0 = no, 1 = yes
    public static string Path { get; set; } = "NULL"; //
    public static void Scan()
    {
        try
        {
            switch (Media.RunCommand($"udevadm info --query=property --name={Config.opticalDrivePath} | grep 'CDROM_MEDIA'"))
            {
                case string s when s.Contains("ID_CDROM_MEDIA_BD=1"):
                    Type = 3; // Blu-ray
                    Opticalr = 0;
                    break;
                case string s when s.Contains("ID_CDROM_MEDIA_BD_R=1"):
                    Type = 3; // Blu-ray-R
                    Opticalr = 1;
                    break;
                case string s when s.Contains("ID_CDROM_MEDIA_DVD=1"):
                    Type = 2; // DVD
                    Opticalr = 0;
                    break;
                case string s when s.Contains("ID_CDROM_MEDIA_DVD_R=1"):
                    Type = 2; // DVD-R
                    Opticalr = 1;
                    break;
                case string s when s.Contains("ID_CDROM_MEDIA_CD=1"):
                    Type = 1; // CD
                    Opticalr = 0;
                    break;
                case string s when s.Contains("ID_CDROM_MEDIA_CD_R=1"):
                    Type = 1; // CD-R
                    Opticalr = 1;
                    break;
                default:
                    Type = 0; // NONE
                    Opticalr = 0;
                    break;
                    //return !string.IsNullOrWhiteSpace(output);
            }
            Path = Media.RunCommand($"lsblk -o NAME,MOUNTPOINT | grep sr0") 
                .Replace("sr0         ", "")
                .Trim();
        }
        catch
        {
            //return false;
            Opticalr = 0;
            Type = 0;
            Path = "NULL";
        }
    }
    public static string debuginfo()
    {
        try
        {
            return Media.RunCommand($"udevadm info --query=property --name={Config.opticalDrivePath} | grep 'CDROM_MEDIA'") + Media.RunCommand("lsblk -o NAME,MOUNTPOINT | grep sr0");
        }
        catch
        {
            return "Error: Unable to retrieve debug information.";
        }
    }
    public static void Mount()
    {
        Media.RunCommand($"udisksctl mount -b {Config.opticalDrivePath}");
    }
    public static void Eject()
    {
        Media.RunCommand($"eject {Config.opticalDrivePath}");
    }
}