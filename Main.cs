using System;
using System.Collections.Generic;
using System.Linq;
using Rage;
using Rage.Native;

[assembly: Rage.Attributes.Plugin("Spartan Kick", Description = "THIS IS SPARTAAAAA", Author = "Astro")]

namespace SpartanKick
{
    internal static class EntryPoint
    {
        internal static Ped MainPlayer => Game.LocalPlayer.Character;
        internal static SoundPlayer SoundPlayer = new SoundPlayer(@"plugins\SpartanKick\STOMP_KICK_5.wav");
        internal static Ped Suspect;
        
        internal static void Main()
        {
            while (true)
            {
                GameFiber.Yield();

                if (Game.IsKeyDown(Settings.Kickbutton))
                {
                    Game.LogTrivial("Button pressed");
                    if (GetNearestPedPlayerIsFacing(out Suspect))
                    {
                        // Huge thanks to Khorio for the Vector math code

                        Game.LogTrivial("Knocking ped over");
                        float startDistance = 15f;

                        float distance = MainPlayer.DistanceTo(Suspect);
                        if (distance < startDistance)
                        {
                            NativeFunction.Natives.SET_PED_TO_RAGDOLL(Suspect, 5000, 6000, 0, false, true);

                            if (MainPlayer.IsInMeleeCombat)
                            {
                                MainPlayer.Tasks.Clear();
                            }
                            GameFiber.StartNew(SoundPlayer.PlaySound2D);
                            MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("melee@large_wpn@streamed_core_fps"),
                                "kick_far_a", 2500, 5f, 5f, 0.25f, AnimationFlags.None).WaitForCompletion();

                            if (MainPlayer.DistanceTo(Suspect) > 3f)
                            {
                                NativeFunction.Natives.APPLY_FORCE_TO_ENTITY(Suspect, 3,
                                    (Suspect.Position.X - MainPlayer.Position.X) + 2f,
                                    (Suspect.Position.Y - MainPlayer.Position.Y) + 2f,
                                    Suspect.Position.Z - MainPlayer.Position.Z, 0f, 0f, 0f, 0, false, true, true, false,
                                    true);
                                
                                
                                switch (Suspect.Health <= 10)
                                {
                                    case true:
                                        Suspect.Kill();
                                        break;
                                    default:
                                        Suspect.Health -= 10;
                                        break;
                                }
                            }

                        }
                    }
                }
            }
        }

        internal static bool GetNearestPedPlayerIsFacing(out Ped ped, float radius = 5f)
        {
            ped = null;

            var nearbyPeds = GetNearbyPeds(radius, p => MainPlayer.IsFacingPed(p, 10f));
            if (nearbyPeds.Length == 0) return false;

            ped = (Ped)nearbyPeds.GetByShortestDistanceTo(MainPlayer);
            return true;
        }

        internal static bool IsFacingPed(this Ped ped, Ped otherPed, float angle)
            => NativeFunction.Natives.xD71649DB0A545AA3<bool>(ped, otherPed, angle); // IS_PED_FACING_PED

        private static Ped[] GetNearbyPeds(float radius, Func<Ped, bool> predicate)
            => MainPlayer.GetNearbyPeds(16)
                .Where(ped =>
                    // base checks
                    ped.Exists() && ped.DistanceTo(MainPlayer) < radius && ped.IsHuman && !ped.IsRagdoll
                    // police checks
                    // extra
                    && predicate(ped)
                )
                .ToArray();

        internal static ISpatial GetByShortestDistanceTo(this IEnumerable<ISpatial> spatialList, ISpatial spatial)
        {
            var array = spatialList.ToArray();
            if (array.Length == 0)
                return null;

            var result = array[0];
            var distance = result.DistanceTo(spatial);

            foreach (var position in array)
            {
                var dist = position.DistanceTo(spatial);
                if (dist > distance)
                    continue;

                result = position;
                distance = dist;
            }

            return result;
        }
    }
}