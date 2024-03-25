using IrrKlang;
using Rage;

namespace SpartanKick
{
    internal class SoundPlayer
    {
        internal string FileName;
        private ISoundEngine soundEngine = new ISoundEngine();
        
        internal SoundPlayer(string FileName)
        {
            this.FileName = FileName;
        }

        internal void PlaySound2D()
        {
            soundEngine.Play2D(FileName);
        }
    }
}