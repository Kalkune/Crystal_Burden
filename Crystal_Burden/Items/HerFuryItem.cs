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
    class HerFuryItem : Crystal_Burden
    {
        private static String tier = "";
        public static void Init()
        {
            ItemTier Hbtier = ItemTier.Lunar;
            if (!ToggleDebuffs.Value)
                Hbtier = ItemTier.Tier3;
            else
                tier = "Lunar";
            HerFury = ScriptableObject.CreateInstance<ItemDef>();
            AddTokens();
            HerFury.name = "HERFURY";
            HerFury.nameToken = "HERFURY_NAME";
            HerFury.pickupToken = "HERFURY_PICKUP";
            HerFury.descriptionToken = "HERFURY_DESC";
            HerFury.loreToken = "HERFURY_LORE";
            HerFury.deprecatedTier = Hbtier;
            if (Nsfw?.Value ?? false)
                HerFury.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>(Artist.Value + "reallyredItemIcon");
            else if (!Nsfw?.Value ?? true)
                HerFury.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Fury" + tier + "ItemIcon");
            if (Nsfw?.Value ?? false)
                HerFury.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "reallyredher_burden");
            else if (!Nsfw?.Value ?? true)
                HerFury.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Fury");
            HerFury.canRemove = true;
            HerFury.hidden = false;
            if (!VariantDropCount.Value)
                HerFury.tags = new ItemTag[] { ItemTag.WorldUnique, (ItemTag)19 };
            else
                HerFury.tags = new ItemTag[] { (ItemTag)19 };

            var rules = new ItemDisplays.CharacterItemDisplayRuleSet();
            AddLocation(rules);
            Items.Add(HerFury, rules);
        }
        private static void AddTokens()
        {
            if (Nsfw?.Value ?? false)
                Languages.AddTokenString("HERFURY_NAME", "Her Fury");
            else if (!Nsfw?.Value ?? true)
                Languages.AddTokenString("HERFURY_NAME", "Crystal Fury");
            if (ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERFURY_PICKUP", "Increase attack speed and decrease HP.\nAll item drops are now variants of: <color=#307FFF>" + NameToken + "</color>");
                Languages.AddTokenString("HERFURY_DESC", $"Increase attack speed by {Hbbv}% and decrease HP by {Hbdbv}%.\nAll item drops are now variants of: <color=#307FFF>" + NameToken + "</color>");
            }
            if (!ToggleDebuffs.Value)
            {
                Languages.AddTokenString("HERFURY_PICKUP", "Increase attack speed.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + NameToken + "</color>");
                Languages.AddTokenString("HERFURY_DESC", $"Increase attack speed by {Hbbv}%.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + NameToken + "</color>");
            }
            Languages.AddTokenString("HERFURY_LORE", "<style=cMono>//--AUTO-TRANSCRIPTION FROM [file unavailable] --//</style>\n\n...then I have something you may find more pleasurable.\n\nHere. Take it in your hand, feel its [heat] upon your palm. Observe its [rough] texture.\n\nNow bring it within you. Do not worry.\n\nIt is active. Feel its [fury] within you, how your body strains to [sustain] it. Let it give you [passion].\n\nDo not worry. If this does not please you...");

        }
        public static void AddLocation(ItemDisplays.CharacterItemDisplayRuleSet rules)
        {
            if (!ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "reallyredher_burden");
                followerPrefab.AddComponent<FakeFuryPrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Fury");
                followerPrefab.AddComponent<FakeFuryPrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + "reallyredher_burden");
                if (VariantShownOnSurvivor.Value == "Fury")
                {
                    followerPrefab.AddComponent<PrefabSizeScript>();
                    followerPrefab.transform.Find("DildoTrail").gameObject.SetActive(ParticleTrail.Value);
                }
                else
                {
                    followerPrefab.AddComponent<FakeFuryPrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Fury");
                Material what = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[0];
                Material what2 = LegacyResourcesAPI.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                what2.SetFloat("_Magnitude", 0.075f);
                var materials = followerPrefab.GetComponent<MeshRenderer>().materials;
                materials[2].shader = what.shader;
                materials[2].CopyPropertiesFromMaterial(what);
                materials[3].shader = what2.shader;
                materials[3].CopyPropertiesFromMaterial(what2);
                followerPrefab.GetComponent<MeshRenderer>().materials = materials;
                if (VariantShownOnSurvivor.Value == "Fury")
                    followerPrefab.AddComponent<PrefabSizeScript>();
                if (VariantShownOnSurvivor.Value != "Fury")
                    followerPrefab.AddComponent<FakeFuryPrefabSizeScript>();
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.2f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.15f, -0.1f, 0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Hip",
                    localPos = new Vector3(0.9f, 0.75f, 1.15f),
                    localAngles = new Vector3(0f, 90f, 180f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.25f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.15f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.2f, 0.05f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.65f, 0.25f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.15f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(1.75f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.2f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.2f, -0.1f, -0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(-0.45f, 0f, -0.35f),
                    localAngles = new Vector3(-90f, 90f, 0f),
                    localScale = generalScale
                }, "mdlHeretic"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.14f, 0.42f, 0.015f),
                    localAngles = new Vector3(-40f, -130f, 130f),
                    localScale = generalScale
                }, "mdlRailGunner"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(-0.09f, 0.025f, 0.12f),
                    localAngles = new Vector3(0f, 70f, 175f),
                    localScale = generalScale
                }, "mdlVoidSurvivor"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Waist",
                    localPos = new Vector3(0.02f, 0.032f, 0.043f),
                    localAngles = new Vector3(0f, 90f, 180f),
                    localScale = generalScale * 0.25f
                }, "mdlMEL-T2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.325f, 0.15f, 0.04f),
                    localAngles = new Vector3(0f, 20f, 0f),
                    localScale = generalScale * 2f
                }, "mdlPaladin"
                );
            }
        }
    }
}
