public static class Media
{
    public static string InfoFilePath { get; set; } = "NULL"; // Path to the info file 
    public static bool HasInfoFile { get; set; } = false; // Flag to indicate if the info file exists
    
    public static void Start()
    {
        Opical.Mount();
        Opical.Scan();
        CLIText.textBackend($"@Green:Optical Disk Type:@colen:@DarkGray: {Opical.Type} (0 = NONE, 1 = CD, 2 = DVD, 3 = Blu-ray):@return:@Green:Optical Disk is Writable:@colen:@DarkGray: {Opical.Opticalr == 1}:@return:@Green:Media Path:@colen:@DarkGray: {Opical.Path}:@return:@Green:Debug Info:@colen:@return:@Yellow:{Opical.debuginfo()}");
        //Opical.Eject();
    }
    public static void Stop()
    {
        // Add any necessary cleanup or stopping logic here
    }
    public static void LoadDir(string dir)
    {
        //Media.RunCommand($"ls {dir}"); // if thres a match to Disk.info then set the info file
    }

    public static string RunCommand(string command)
    {
        try
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = "/bin/bash";
                process.StartInfo.Arguments = $"-c \"{command}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    return $"Error: {error}";
                }

                return output;
            }
        }
        catch (Exception ex)
        {
            return $"Exception: {ex.Message}";
        }
    }
}

public static class Disk //(floppy drive)
{

}