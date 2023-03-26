using BepInEx;
using HarmonyLib;
using SpaceWarp;
using SpaceWarp.API.Mods;
using KSP.Sim.impl;

namespace EvaAnarchy;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class EvaAnarchyPlugin : BaseSpaceWarpPlugin
{
    public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    public const string ModName = MyPluginInfo.PLUGIN_NAME;
    public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    private static EvaAnarchyPlugin instance;

    public override void OnInitialized()
    {
        base.OnInitialized();
        Logger.LogInfo($"OnInitialized called");
        instance = this;
        Harmony.CreateAndPatchAll(typeof(EvaAnarchyPlugin));
    }

    [HarmonyPatch(typeof(IVAPortraitEVAObstacleDetector), nameof(IVAPortraitEVAObstacleDetector.IsEVADisabledByObstacle))]
    [HarmonyPrefix]
    public static bool IVAPortraitEVAObstacleDetector_IsEVADisabledByObstacle(ref bool __result) {
        instance.Logger.LogInfo($"IsEVADisabledByObstacle called");
        __result = true;
        return false;
    }
}