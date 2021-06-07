using UnityEngine;
using BepInEx;
using HarmonyLib;

namespace MitchClient
{
    [BepInPlugin(Id, "Mitch Client", Version)]
    [BepInProcess("Muck.exe")]
    public class MitchClientPlugin : BaseUnityPlugin
    {
        public const string Id = "com.ugackminer.mitchclient";
        public const string Version = "2.0.0";
        public Harmony Harmony { get; } = new Harmony(Id);

        public void Awake()
        {
            Harmony.PatchAll();
            Debug.Log("[Mitch] Loaded!");
        }

    }
}
