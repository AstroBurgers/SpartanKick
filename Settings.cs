using System.Windows.Forms;
using Rage;

namespace SpartanKick
{
    public class Settings
    {
        internal static InitializationFile INIFile;
        internal static Keys Kickbutton = Keys.K;
        
        internal static void SetupINIFile()
        {
            INIFile = new InitializationFile(@"Plugins/SpartanKick.ini");
            INIFile.Create();
            Kickbutton = INIFile.ReadEnum("Settings", "Kick button", Kickbutton);
        }
    }
}