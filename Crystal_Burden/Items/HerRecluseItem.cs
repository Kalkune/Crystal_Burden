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
    class HerRecluseItem : Crystal_Burden
    {
        private static String tier = "";
        public static void Init()
        {
            ItemTier Hbtier = ItemTier.Lunar;
            if (!ToggleDebuffs.Value)
                Hbtier = ItemTier.Tier3;
            else
                tier = "Lunar";
            HerRecluse = ScriptableObject.CreateInstance<ItemDef>();
            AddTokens();
            HerRecluse.name = "HERRECLUSE";
            HerRecluse.nameToken = "HERRECLUSE_NAME";
            HerRecluse.pickupToken = "HERRECLUSE_PICKUP";
            HerRecluse.descriptionToken = "HERRECLUSE_DESC";
            HerRecluse.loreToken = "HERRECLUSE_LORE";
            HerRecluse.deprecatedTier = Hbtier;
            if (Nsfw?.Value ?? false)
                HerRecluse.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>(Artist.Value + "lightishblueItemIcon");
            else if (!Nsfw?.Value ?? true)
                HerRecluse.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Recluse" + tier + "ItemIcon");
            if (Nsfw?.Value ?? false)
                HerRecluse.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "lightishblueher_burden");
            else if (!Nsfw?.Value ?? true)
                HerRecluse.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Recluse");
            HerRecluse.canRemove = true;
            HerRecluse.hidden = false;
            if (!VariantDropCount.Value)
                HerRecluse.tags = new ItemTag[] { ItemTag.WorldUnique, (ItemTag)19 };
            else
                HerRecluse.tags = new ItemTag[] { (ItemTag)19 };

            var rules = new ItemDisplays.CharacterItemDisplayRuleSet();
            AddLocation(rules);
            Items.Add(HerRecluse, rules);
        }
        private static void AddTokens()
        {
            if (Nsfw?.Value ?? false)
                Languages.AddTokenString("HERRECLUSE_NAME", "Her Recluse");
            else if (!Nsfw?.Value ?? true)
                Languages.AddTokenString("HERRECLUSE_NAME", "Crystal Recluse");
            if (ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERRECLUSE_PICKUP", "Increase armor and decrease regen.\nAll item drops are now: <color=#307FFF>" + NameToken + "</color>");
                Languages.AddTokenString("HERRECLUSE_DESC", $"Increase armor by {Hbbv}% and decrease regen by {Hbdbv}%.\nAll item drops are now: <color=#307FFF>" + NameToken + "</color>");
            }
            if (!ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERRECLUSE_PICKUP", "Increase armor.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + NameToken + "</color>");
                Languages.AddTokenString("HERRECLUSE_DESC", $"Increase armor by {Hbbv}%.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + NameToken + "</color>");
            }
            Languages.AddTokenString("HERRECLUSE_LORE", "<style=cMono>//--AUTO-TRANSCRIPTION FROM [file unavailable] --//</style>\n\n...then I have something you may find more pleasurable.\n\nHere. Take it in your hand, feel its [softness] upon your palm. Observe its [blocky] texture.\n\nNow bring it within you. Do not worry.\n\nIt is active. Feel its [recluse] within you, how your body strains to [sustain] it. Let it give you [safety].\n\nDo not worry. If this does not please you...");

        }
        public static void AddLocation(ItemDisplays.CharacterItemDisplayRuleSet rules)
        {
            if (!ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "lightishblueher_burden");
                followerPrefab.AddComponent<FakeReclusePrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Recluse");
                followerPrefab.AddComponent<FakeReclusePrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "lightishblueher_burden");
                if (VariantShownOnSurvivor.Value == "Recluse")
                {
                    followerPrefab.AddComponent<PrefabSizeScript>();
                    followerPrefab.transform.Find("DildoTrail").gameObject.SetActive(ParticleTrail.Value);
                }
                else
                {
                    followerPrefab.AddComponent<FakeReclusePrefabSizeScript>();
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
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.125f, 0.075f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlDeputy"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.125f, -0.075f),
                    localAngles = new Vector3(180f, 180f, 0f),
                    localScale = generalScale
                }, "mdlDriver(Clone)"
                );
            }
            if (ItemVisibility.Value && (!Nsfw?.Value ?? true))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Recluse");
                Material what = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[0];
                Material what2 = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                what2.SetFloat("_Magnitude", 0.075f);
                var materials = followerPrefab.GetComponent<MeshRenderer>().materials;
                materials[2].shader = what.shader;
                materials[2].CopyPropertiesFromMaterial(what);
                materials[3].shader = what2.shader;
                materials[3].CopyPropertiesFromMaterial(what2);
                followerPrefab.GetComponent<MeshRenderer>().materials = materials;
                followerPrefab.AddComponent<FakeReclusePrefabSizeScript>();
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.25f, 0.35f, -0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.2f, 0.3f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(2.5f, 2.5f, -1f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "CannonHeadR",
                    localPos = new Vector3(-0.2f, 0.2f, 0.1f),
                    localAngles = new Vector3(-45f, -90f, -90f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.15f, 0.3f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.225f, 0.3f, -0.025f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "FlowerBase",
                    localPos = new Vector3(0.8f, 0.75f, -0.4f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.25f, 0.3f, -0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(-3f, 2f, 2f),
                    localAngles = new Vector3(-30f, -160f, 10f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.25f, 0.45f, -0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.25f, 0.35f, -0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(-0.6f, -0.6f, -0.6f),
                    localAngles = new Vector3(-90f, 90f, 0f),
                    localScale = generalScale
                }, "mdlHeretic"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.24f, 0.12f, -0.035f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlRailGunner"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.22f, 0.225f, -0.25f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                }, "mdlVoidSurvivor"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Body",
                    localPos = new Vector3(0.055f, 0.06f, -0.035f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.25f
                }, "mdlMEL-T2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.3f, 0.45f, -0.175f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2f
                }, "mdlPaladin"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.175f, 0.225f, 0.02f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                }, "mdlDeputy"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0.3f, 0.385f, -0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                }, "mdlDriver(Clone)"
                );
            }
        }
    }
}
