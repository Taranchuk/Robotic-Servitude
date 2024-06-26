﻿using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;
namespace RoboticServitude
{
    [HarmonyPatch(typeof(Building_MechGestator), "Notify_HauledTo")]
    public static class Building_MechGestator_Notify_HauledTo_Patch
    {
        public static void Postfix(Building_MechGestator __instance)
        {
            var allThings = __instance.innerContainer.ToList();
            __instance.innerContainer.Clear();
            var pawnRace = __instance.ActiveBill.recipe.ProducedThingDef;
            var kind = DefDatabase<PawnKindDef>.AllDefs.FirstOrDefault(x => x.race == pawnRace);
            var pawn = PawnGenerator.GeneratePawn(kind);
            pawn.Kill(null);
            __instance.innerContainer.TryAdd(pawn.Corpse);
            foreach (var thing in allThings)
            {
                __instance.innerContainer.TryAdd(thing);
            }
        }
    }
}
