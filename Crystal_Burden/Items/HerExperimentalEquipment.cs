﻿using BepInEx;
using BepInEx.Configuration;
using BetterAPI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Crystal_Burden
{
    class HerGambleEquipment : Crystal_Burden
    {
        private static String tier = "";
        public static void Init()
        {
            var Hgcolor = ColorCatalog.ColorIndex.LunarItem;
            if (!ToggleDebuffs.Value)
                Hgcolor = ColorCatalog.ColorIndex.Equipment;
            else
                tier = "Lunar";
            HerGamble = ScriptableObject.CreateInstance<EquipmentDef>();
            AddTokens();
            HerGamble.name = "HERGAMBLE";
            HerGamble.nameToken = "HERGAMBLE_NAME";
            HerGamble.pickupToken = "HERGAMBLE_PICKUP";
            HerGamble.descriptionToken = "HERGAMBLE_DESC";
            HerGamble.loreToken = "HERGAMBLE_LORE";
            HerGamble.isLunar = ToggleDebuffs.Value;
            HerGamble.colorIndex = Hgcolor;
            if (Nsfw?.Value ?? false)
                HerGamble.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Gamble" + tier + "EquipmentIcon");
            else if (!Nsfw?.Value ?? true)
                HerGamble.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Gamble" + tier + "EquipmentIcon");
            if (Nsfw?.Value ?? false)
                HerGamble.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("her_gamble");
            else if (!Nsfw?.Value ?? true)
                HerGamble.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Gamble");
            HerGamble.canDrop = true;
            HerGamble.cooldown = 60f;
            HerGambleBuff = ScriptableObject.CreateInstance<BuffDef>();
            HerGambleBuff.name = "HerGambleBuff";
            if (Nsfw?.Value ?? false)
                HerGambleBuff.iconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("GambleEquipmentIcon");
            else if (!Nsfw?.Value ?? true)
                HerGambleBuff.iconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_GambleEquipmentIcon");
            Buffs.Add(HerGambleBuff);
            HerGambleDeBuff = ScriptableObject.CreateInstance<BuffDef>();
            HerGambleDeBuff.name = "HerGambleDeBuff";
            HerGambleDeBuff.isDebuff = true;
            if (Nsfw?.Value ?? false)
                HerGambleDeBuff.iconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Gamble" + tier + "EquipmentIcon");
            else if (!Nsfw?.Value ?? true)
                HerGambleDeBuff.iconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Gamble" + tier + "EquipmentIcon");
            Buffs.Add(HerGambleDeBuff);
            var rules = new ItemDisplays.CharacterItemDisplayRuleSet();
            AddLocation(rules);
            Equipments.Add(HerGamble, rules);
        }
        private static void AddTokens()
        {
            if (Nsfw?.Value ?? false)
                Languages.AddTokenString("HERGAMBLE_NAME", "Her Gamble");
            else if (!Nsfw?.Value ?? true)
                Languages.AddTokenString("HERGAMBLE_NAME", "Crystal Gamble");
            if (ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERGAMBLE_PICKUP", "An equipment that gambles your stats");
                Languages.AddTokenString("HERGAMBLE_DESC", "An equipment that gambles your stats that come from <color=#307FFF>" + NameToken + "</color> Variants");
            }
            if (!ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERGAMBLE_PICKUP", "Has a chance to double your stats");
                Languages.AddTokenString("HERGAMBLE_DESC", "Has a chance to double your stats that come from <color=#e7553b>" + NameToken + "</color> Variants");
            }
            Languages.AddTokenString("HERGAMBLE_LORE", "<style=cMono>//--AUTO-TRANSCRIPTION FROM [file unavailable] --//</style>\n\n...then I have something for you, now that you have found pleasure.\n\nHere. Take it in your hand, feel its [TRANSCRIPTION ERROR]. Observe how its texture changes.\n\nNow bring it within you. Do not worry.\n\nDo not activate it yet. Allow it to [observe]. Feel your [pleasure] empower it.\n\nDo not worry. If ever you wish to be pleased...");

        }
        public static void AddLocation(ItemDisplays.CharacterItemDisplayRuleSet rules)
        {
            if (!ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("her_gamble");
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                _ = new ItemDisplayRule[]
                {
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 0f, 0.1f),
                    localAngles = new Vector3(40f, 180f, 180f),
                    localScale = generalScale
                }
                };
            }
            if (!ItemVisibility.Value && (!Nsfw?.Value ?? true))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Gamble");
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                _ = new ItemDisplayRule[]
                {
                new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }
                };
            }
            if (ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("her_gamble");
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 0f, 0.1f),
                    localAngles = new Vector3(40f, 180f, 180f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.025f, 0.025f, 0.1875f),
                    localAngles = new Vector3(40f, 150f, 180f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.15f, 0.5f, 0.8f),
                    localAngles = new Vector3(45f, -170f, -170f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.15f, 0.04f, -0.16f),
                    localAngles = new Vector3(35f, -25f, 180f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.025f, 0.325f, 0.17f),
                    localAngles = new Vector3(60f, 165f, 175f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.06f, 0.03f, 0.16f),
                    localAngles = new Vector3(40f, 155f, 180f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "FlowerBase",
                    localPos = new Vector3(-0.27f, 0.4f, 0.3f),
                    localAngles = new Vector3(30f, 80f, 10f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "MechBase",
                    localPos = new Vector3(0.2025f, 0.2f, 0.467f),
                    localAngles = new Vector3(-46.5f, 180f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 1.5f, 1.35f),
                    localAngles = new Vector3(45f, -10f, 175f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.01f, 0.32f, 0.15f),
                    localAngles = new Vector3(55f, 160f, 170f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 0.225f, 0.115f),
                    localAngles = new Vector3(50f, 150f, 170f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.15f, 0.2f, 0.13f),
                    localAngles = new Vector3(40f, 130f, 60f),
                    localScale = generalScale * 2
                }, "mdlHeretic"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.015f, -0.02f, 0.11f),
                    localAngles = new Vector3(47.5f, 170f, 180f),
                    localScale = generalScale
                }, "mdlRailGunner"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.05f, 0.1f, -0.115f),
                    localAngles = new Vector3(40f, 10f, 180f),
                    localScale = generalScale
                }, "mdlVoidSurvivor"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.15f, 0.5f, 0.8f),
                    localAngles = new Vector3(45f, -170f, -170f),
                    localScale = generalScale * 0.25f
                }, "mdlMEL-T2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.055f, 0.1f, -0.325f),
                    localAngles = new Vector3(37.5f, 32.5f, 155f),
                    localScale = generalScale
                }, "mdlPaladin"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.1f, 0.225f, 0.05f),
                    localAngles = new Vector3(40f, 150f, 180f),
                    localScale = generalScale * 0.8f
                }, "mdlDeputy"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.06f, 0.055f, 0.08f),
                    localAngles = new Vector3(40f, 150f, 180f),
                    localScale = generalScale * 0.8f
                }, "mdlDriver(Clone)"
                );
            }
            if (ItemVisibility.Value && (!Nsfw?.Value ?? true))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Gamble");
                Material what = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[0];
                Material what2 = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                what2.SetFloat("_Magnitude", 0.075f);
                var materials = followerPrefab.GetComponent<MeshRenderer>().materials;
                materials[2].shader = what.shader;
                materials[2].CopyPropertiesFromMaterial(what);
                materials[3].shader = what2.shader;
                materials[3].CopyPropertiesFromMaterial(what2);
                followerPrefab.GetComponent<MeshRenderer>().materials = materials;
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.1f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 1.5f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.25f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.1f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.15f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.4f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.2f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(-0.2f, 0f, 0f),
                    localAngles = new Vector3(90f, 0f, 0f),
                    localScale = generalScale * 2
                }, "mdlHeretic"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.075f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlRailGunner"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.075f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlVoidSurvivor"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Body",
                    localPos = new Vector3(0f, 0.03f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.25f
                }, "mdlMEL-T2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.15f, 0.1f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2f
                }, "mdlPaladin"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.1f, 0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                }, "mdlDeputy"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                }, "mdlDriver(Clone)"
                );
            }
        }
    }
}
