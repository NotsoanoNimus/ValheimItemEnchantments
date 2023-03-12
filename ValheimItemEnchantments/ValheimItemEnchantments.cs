using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ValheimItemEnchantments
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ValheimItemEnchantments : BaseUnityPlugin
    {
        public const string PluginGUID = "xyz.xmit.ValheimItemEnchantments";
        public const string PluginName = "ValheimItemEnchantments";
        public const string PluginVersion = "0.0.1";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            AddLocalizations();
            RegisterCopperDoorTestAsset();
            
            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
        }

        private void AddLocalizations()
        {
            var localization = new CustomLocalization();
            LocalizationManager.Instance.AddLocalization(localization);

            localization.AddTranslation("English", new Dictionary<string, string>
            {
                { "piece_copperdoor", "Copper Door" }, { "piece_copperdoor_description", "Oooo, shiny O_O" },
            });
        }

        private void RegisterCopperDoorTestAsset()
        {
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("ValheimItemEnchantments has landed");
            Jotunn.Logger.LogInfo($"Current working directory: {Environment.CurrentDirectory}");

            var testAsset = AssetBundle.LoadFromFile(@"BepInEx\plugins\ValheimItemEnchantments\Assets\copper_door");
            Jotunn.Logger.LogInfo(testAsset);

            var copperDoorConfig = new PieceConfig()
            {
                Name = "$piece_copperdoor",
                Description = "$piece_copperdoor_description",
                PieceTable = "Hammer",
                CraftingStation = "forge",
            };
            copperDoorConfig.AddRequirement(new RequirementConfig("copper", 6, 0, true));

            PieceManager.Instance.AddPiece(new CustomPiece("custom_copper_door", "copper_door", copperDoorConfig));
        }
    }
}

