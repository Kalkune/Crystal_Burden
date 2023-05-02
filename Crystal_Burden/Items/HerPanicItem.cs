using BepInEx;
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
    class HerPanicItem : Crystal_Burden
    {
        private static String tier = "";
        public static void Init()
        {
            ItemTier Hbtier = ItemTier.Lunar;
            if (!ToggleDebuffs.Value)
                Hbtier = ItemTier.Tier3;
            else
                tier = "Lunar";
            HerPanic = ScriptableObject.CreateInstance<ItemDef>();
            AddTokens();
            HerPanic.name = "HERPANIC";
            HerPanic.nameToken = "HERPANIC_NAME";
            HerPanic.pickupToken = "HERPANIC_PICKUP";
            HerPanic.descriptionToken = "HERPANIC_DESC";
            HerPanic.loreToken = "HERPANIC_LORE";
            HerPanic.deprecatedTier = Hbtier;
            if (Nsfw?.Value ?? false)
                HerPanic.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>(Artist.Value + "violetItemIcon");
            else if (!Nsfw?.Value ?? true)
                HerPanic.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Panic" + tier + "ItemIcon");
            if (Nsfw?.Value ?? false)
                HerPanic.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "violether_burden");
            else if (!Nsfw?.Value ?? true)
                HerPanic.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Panic");
            HerPanic.canRemove = true;
            HerPanic.hidden = false;
            if (!VariantDropCount.Value)
                HerPanic.tags = new ItemTag[] { ItemTag.WorldUnique, (ItemTag)19 };
            else
                HerPanic.tags = new ItemTag[] { (ItemTag)19 };

            var rules = new ItemDisplays.CharacterItemDisplayRuleSet();
            AddLocation(rules);
            Items.Add(HerPanic, rules);
        }
        private static void AddTokens()
        {
            if (Nsfw?.Value ?? false)
                Languages.AddTokenString("HERPANIC_NAME", "Her Panic");
            else if (!Nsfw?.Value ?? true)
                Languages.AddTokenString("HERPANIC_NAME", "Crystal Panic");
            if (ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERPANIC_PICKUP", "Increase move speed and decrease damage.\nAll item drops are now variants of: <color=#307FFF>" + NameToken + "</color>");
                Languages.AddTokenString("HERPANIC_DESC", $"Increase move speed by {Hbbv}% and decrease damage by {Hbdbv}%.\nAll item drops are now variants of: <color=#307FFF>" + NameToken + "</color>");
            }
            if (!ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERPANIC_PICKUP", "Increase move speed.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + NameToken + "</color>");
                Languages.AddTokenString("HERPANIC_DESC", $"Increase move speed by {Hbbv}%.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + NameToken + "</color>");
            }
            Languages.AddTokenString("HERPANIC_LORE", "<style=cMono>//--AUTO-TRANSCRIPTION FROM [file unavailable] --//</style>\n\n...then I have something you may find more pleasurable.\n\nHere. Take it in your hand, feel its [energy] upon your palm. Observe its [irregular] texture.\n\nNow bring it within you. Do not worry.\n\nIt is active. Feel its [panic] within you, how your body strains to [focus] it. Let it give you [initiative].\n\nDo not worry. If this does not please you...");

        }
        public static void AddLocation(ItemDisplays.CharacterItemDisplayRuleSet rules)
        {
            if (!ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "violether_burden");
                followerPrefab.AddComponent<FakePanicPrefabSizeScript>();
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                _ = new ItemDisplayRule[]
                {
                new ItemDisplayRule
                {
                     ruleType = ItemDisplayRuleType.ParentedPrefab,
                     followerPrefab = followerPrefab,
                     childName = "Pelvis",
                     localPos = new Vector3(0f, 0.1f, 0.1f),
                     localAngles = new Vector3(180f, -0.05f, 0f),
                     localScale = generalScale
                }
                };
            }
            if (!ItemVisibility.Value && (!Nsfw?.Value ?? true))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Panic");
                followerPrefab.AddComponent<FakePanicPrefabSizeScript>();
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                _ = new ItemDisplayRule[]
                {
                new ItemDisplayRule
                {
                     ruleType = ItemDisplayRuleType.ParentedPrefab,
                     followerPrefab = followerPrefab,
                     childName = "Pelvis",
                     localPos = new Vector3(0f, 0.1f, 0.1f),
                     localAngles = new Vector3(180f, -0.05f, 0f),
                     localScale = generalScale
                }
                };
            }
            if (ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "violether_burden");
                if (VariantShownOnSurvivor.Value == "Panic")
                {
                    followerPrefab.AddComponent<PrefabSizeScript>();
                    followerPrefab.transform.Find("DildoTrail").gameObject.SetActive(ParticleTrail.Value);
                }
                else
                {
                    followerPrefab.AddComponent<FakePanicPrefabSizeScript>();
                    followerPrefab.transform.Find("DildoTrail").gameObject.SetActive(false);
                }
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmR",
                    localPos = new Vector3(0f, 5.5f, 0f),
                    localAngles = new Vector3(45f, -90f, 0f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.25f, 0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "WeaponPlatform",
                    localPos = new Vector3(0.2f, 0.05f, 0.2f),
                    localAngles = new Vector3(-45f, 0f, 0f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.25f, 0.15f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Hip",
                    localPos = new Vector3(0f, 3.5f, 0f),
                    localAngles = new Vector3(135f, -0.05f, 0f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(.3f, -.15f, 0f),
                    localAngles = new Vector3(20f, -120f, -36f),
                    localScale = generalScale
                }, "mdlHeretic"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.25f, 0.1f),
                    localAngles = new Vector3(0f, 180f, 180f),
                    localScale = generalScale
                }, "mdlRailGunner"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.30f, 0f),
                    localAngles = new Vector3(45f, 90f, 180f),
                    localScale = generalScale
                }, "mdlVoidSurvivor"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "HandR",
                    localPos = new Vector3(0.005f, -0.0075f, 0f),
                    localAngles = new Vector3(45f, -4f, 0f),
                    localScale = generalScale * 0.25f
                }, "mdlMEL-T2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(-0.025f, 0f, 0f),
                    localAngles = new Vector3(-15f, 0f, 0f),
                    localScale = generalScale * 2f
                }, "mdlPaladin"
                );
            }
            if (ItemVisibility.Value && (!Nsfw?.Value ?? true))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Panic");
                Material what = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[0];
                Material what2 = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                what2.SetFloat("_Magnitude", 0.075f);
                var materials = followerPrefab.GetComponent<MeshRenderer>().materials;
                materials[2].shader = what.shader;
                materials[2].CopyPropertiesFromMaterial(what);
                materials[3].shader = what2.shader;
                materials[3].CopyPropertiesFromMaterial(what2);
                followerPrefab.GetComponent<MeshRenderer>().materials = materials;
                if (VariantShownOnSurvivor.Value == "Panic")
                    followerPrefab.AddComponent<PrefabSizeScript>();
                if (VariantShownOnSurvivor.Value != "Panic")
                    followerPrefab.AddComponent<FakePanicPrefabSizeScript>();
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.4f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.4f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0f, 3.5f, 1f),
                    localAngles = new Vector3(270f, 0f, 0f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.4f, 0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.5f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.5f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "FootBackR",
                    localPos = new Vector3(0f, 1f, 0f),
                    localAngles = new Vector3(0f, 180f, 180f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.4f, 0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0f, 4f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.5f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.5f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "KneeR",
                    localPos = new Vector3(-0.85f, -0.0225f, 0f),
                    localAngles = new Vector3(-60f, -50f, -40f),
                    localScale = generalScale
                }, "mdlHeretic"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.025f, 0.5f, 0f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlRailGunner"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0f, 0.5f, 0f),
                    localAngles = new Vector3(-40f, -170f, 155f),
                    localScale = generalScale
                }, "mdlVoidSurvivor"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "KneeR",
                    localPos = new Vector3(-0.015f, 0.04f, -0.02f),
                    localAngles = new Vector3(-90f, 180f, 0f),
                    localScale = generalScale * 0.25f
                }, "mdlMEL-T2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CalfR",
                    localPos = new Vector3(0.05f, 0.5f, 0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale * 2f
                }, "mdlPaladin"
                );
            }
        }
    }
}
