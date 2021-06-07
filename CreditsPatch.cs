using TMPro;
using UnityEngine;
using HarmonyLib;

namespace MitchClient
{
    [HarmonyPatch(typeof(ButtonSfx), "OnPointerClick")]
    class ButtonSfxPatch
    {
        static void Postfix(ButtonSfx __instance)
        {
            if (Object.FindObjectOfType<Credits>() != null)
            {
                Object.FindObjectsOfType<TextMeshProUGUI>()[0].text = "Thank you for downloading Mitch!";
                Object.FindObjectsOfType<TextMeshProUGUI>()[1].fontSize = 24f;
                Object.FindObjectsOfType<TextMeshProUGUI>()[1].text = "Mitch client made by ugackMiner.\nELFOURLY as emotional support and idea man";
                Object.FindObjectsOfType<TextMeshProUGUI>()[3].text = "Mitch client " + MitchClientPlugin.Version;
            }
        }
    }
}
