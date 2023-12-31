using Rage;
using static Rage.Native.NativeFunction;

namespace SpartanKick
{
    public class Sound
    {
        public int Id { get; set; }

        public Sound(int id)
        {
            this.Id = id;
        }
        public Sound() : this(GetId()) { }

        public void Play(string soundName, string setName, bool bOverNetwork = false, int nNetworkRange = 0, bool enableInRockstarEditor = true)
        {
            if (setName != null) Natives.PLAY_SOUND(Id, soundName, setName, bOverNetwork, nNetworkRange, enableInRockstarEditor);
            else Natives.PLAY_SOUND(Id, soundName, 0, bOverNetwork, nNetworkRange, enableInRockstarEditor);
        }

        /// <summary>
        /// Plays back a sound "frontend" - at full volume, panned centrally. Optionally can specify a sound set which contains the sound.
        /// </summary>
        public void PlayFrontend(string soundName, string setName, bool enableInRockstarEditor = false)
        {
            if (setName != null) Natives.PLAY_SOUND_FRONTEND(Id, soundName, setName, enableInRockstarEditor);
            else Natives.PLAY_SOUND_FRONTEND(Id, soundName, 0, enableInRockstarEditor);
        }

        /// <summary>
        /// The sound's position will track the entity's position as it moves.
        /// </summary>
        public void PlayFromEntity(string soundName, string setName, Entity entity, bool bOverNetwork = false, int nNetworkRange = 0)
        {
            if (setName != null) Natives.PLAY_SOUND_FROM_ENTITY(Id, soundName, entity, setName, bOverNetwork, nNetworkRange);
            else Natives.PLAY_SOUND_FROM_ENTITY(Id, soundName, entity, 0, bOverNetwork, nNetworkRange);
        }

        /// <summary>
        /// The sound's position will track the entity's position as it moves.
        /// </summary>
        public void PlayFromEntityHash(int soundNameHash, int setNameHash, Entity entity, bool bOverNetwork = false, int nNetworkRange = 0)
        {
            Natives.PLAY_SOUND_FROM_ENTITY_HASH(Id, soundNameHash, entity, setNameHash, bOverNetwork, nNetworkRange);
        }

        /// <summary>
        /// Plays back a sound from an absolute position. Optionally can specify a sound set which contains the sound.
        /// </summary>
        /// <param name="soundName"></param>
        /// <param name="setName"></param>
        /// <param name="position"></param>
        /// <param name="bOverNetwork"></param>
        /// <param name="nNetworkRange"></param>
        /// <param name="isExteriorLoc">If isExteriorLoc is set to TRUE, then it will use a portal occlusion environmentGroup.  Only use this if the sound is playing outside and needs occlusion.</param>
        public void PlayFromPosition(string soundName, string setName, Vector3 position, bool bOverNetwork = false, int nNetworkRange = 0, bool isExteriorLoc = false)
        {
            if (setName != null) Natives.PLAY_SOUND_FROM_COORD(Id, soundName, position.X, position.Y, position.Z, setName, bOverNetwork, nNetworkRange, isExteriorLoc);
            else Natives.PLAY_SOUND_FROM_COORD(Id, soundName, position.X, position.Y, position.Z, 0, bOverNetwork, nNetworkRange, isExteriorLoc);
        }

        /// <summary>
        /// Updates a playing sounds absolute position. 
        /// </summary>
        public void UpdateCoords(Vector3 position)
        {
            Natives.UPDATE_SOUND_COORDS(Id, position);
        }

        public void PlayFireSoundFromCoords(Vector3 position) 
        {
            Natives.PLAY_FIRE_SOUND_FROM_COORDS(Id, position);
        }

        public void SetVariable(string variableName, float value)
        {
            Natives.SET_VARIABLE_ON_SOUND(Id, variableName, value);
        }

        public void Stop()
        {
            if (Id != -1) Natives.STOP_SOUND(Id);
        }

        /// <summary>
        /// Checks that a sound has finished playing.
        /// </summary>
        /// <returns>Whether the sound has finished playing or not.</returns>
        public bool HasFinished()
        {
            return Natives.HAS_SOUND_FINISHED<bool>(Id);
        }

        public void ReleaseId()
        {
            Natives.RELEASE_SOUND_ID(Id);
            Id = -1;
        }

        public static int GetId()
        {
            return Natives.GET_SOUND_ID<int>();
        }

        public static void PlayAmbientSpeechFromPositionNative(string voiceLine, string voiceName, Vector3 position, string speechParams)
        {
            Natives.PLAY_AMBIENT_SPEECH_FROM_POSITION_NATIVE(voiceLine, voiceName, position, speechParams);
        }

        public static bool RequestMissionAudioBank(string audioBankName, bool p1 = true)
        {
            return Natives.REQUEST_MISSION_AUDIO_BANK<bool>(audioBankName, p1);
        }
        public static bool RequestAmbientAudioBank(string audioBankName, bool p1 = true)
        {
            return Natives.REQUEST_AMBIENT_AUDIO_BANK<bool>(audioBankName, p1);
        }
        public static bool RequestScriptAudioBank(string audioBankName, bool p1 = true)
        {
            return Natives.REQUEST_SCRIPT_AUDIO_BANK<bool>(audioBankName, p1, -1);
        }
    }
}
