using BepInEx;
using BepInEx.Configuration;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
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
            ContentAddition.AddBuffDef(HerGambleBuff);
            HerGambleDeBuff = ScriptableObject.CreateInstance<BuffDef>();
            HerGambleDeBuff.name = "HerGambleDeBuff";
            HerGambleDeBuff.isDebuff = true;
            if (Nsfw?.Value ?? false)
                HerGambleDeBuff.iconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Gamble" + tier + "EquipmentIcon");
            else if (!Nsfw?.Value ?? true)
                HerGambleDeBuff.iconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Gamble" + tier + "EquipmentIcon");
            ContentAddition.AddBuffDef(HerGambleDeBuff);
            var rules = new ItemDisplayRuleDict();
            AddLocation(rules);
            CustomEquipment CustomEquipment = new CustomEquipment(HerGamble, rules);
            ItemAPI.Add(CustomEquipment);
        }
        private static void AddTokens()
        {
            if (Nsfw?.Value ?? false)
                LanguageAPI.Add("HERGAMBLE_NAME", "Her Gamble");
            else if (!Nsfw?.Value ?? true)
                LanguageAPI.Add("HERGAMBLE_NAME", "Crystal Gamble");
            if (ToggleDebuffs.Value)
            {
                LanguageAPI.Add("HERGAMBLE_PICKUP", "An equipment that gambles your stats");
                LanguageAPI.Add("HERGAMBLE_DESC", "An equipment that gambles your stats that come from <color=#307FFF>" + NameToken + "</color> Variants");
            }
            if (!ToggleDebuffs.Value)
            {
                LanguageAPI.Add("HERGAMBLE_PICKUP", "Has a chance to double your stats");
                LanguageAPI.Add("HERGAMBLE_DESC", "Has a chance to double your stats that come from <color=#e7553b>" + NameToken + "</color> Variants");
            }
            LanguageAPI.Add("HERGAMBLE_LORE", "<style=cMono>//--AUTO-TRANSCRIPTION FROM [file unavailable] --//</style>\n\n...then I have something for you, now that you have found pleasure.\n\nHere. Take it in your hand, feel its [TRANSCRIPTION ERROR]. Observe how its texture changes.\n\nNow bring it within you. Do not worry.\n\nDo not activate it yet. Allow it to [observe]. Feel your [pleasure] empower it.\n\nDo not worry. If ever you wish to be pleased...");

        }
        public static void AddLocation(ItemDisplayRuleDict rules)
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
                rules.Add("mdlCommandoDualies", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 0f, 0.1f),
                    localAngles = new Vector3(40f, 180f, 180f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlHuntress", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.025f, 0.025f, 0.1875f),
                    localAngles = new Vector3(40f, 150f, 180f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlToolbot", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.15f, 0.5f, 0.8f),
                    localAngles = new Vector3(45f, -170f, -170f),
                    localScale = generalScale * 10
                },
                });
                rules.Add("mdlEngi", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0.15f, 0.04f, -0.16f),
                    localAngles = new Vector3(35f, -25f, 180f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlMage", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.025f, 0.325f, 0.17f),
                    localAngles = new Vector3(60f, 165f, 175f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlMerc", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.06f, 0.03f, 0.16f),
                    localAngles = new Vector3(40f, 155f, 180f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlTreebot", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "FlowerBase",
                    localPos = new Vector3(-0.27f, 0.4f, 0.3f),
                    localAngles = new Vector3(30f, 80f, 10f),
                    localScale = generalScale * 2
                },
                });
                rules.Add("mdlLoader", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "MechBase",
                    localPos = new Vector3(0.2025f, 0.2f, 0.467f),
                    localAngles = new Vector3(-46.5f, 180f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlCroco", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 1.5f, 1.35f),
                    localAngles = new Vector3(45f, -10f, 175f),
                    localScale = generalScale * 10
                },
                });
                rules.Add("mdlCaptain", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.01f, 0.32f, 0.15f),
                    localAngles = new Vector3(55f, 160f, 170f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlBandit2", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0f, 0.225f, 0.115f),
                    localAngles = new Vector3(50f, 150f, 170f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlHeretic", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.15f, 0.2f, 0.13f),
                    localAngles = new Vector3(40f, 130f, 60f),
                    localScale = generalScale * 2
                },
                });
                rules.Add("mdlRailGunner", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.015f, -0.02f, 0.11f),
                    localAngles = new Vector3(47.5f, 170f, 180f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlVoidSurvivor", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.05f, 0.1f, -0.115f),
                    localAngles = new Vector3(40f, 10f, 180f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlMEL-T2", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.15f, 0.5f, 0.8f),
                    localAngles = new Vector3(45f, -170f, -170f),
                    localScale = generalScale * 0.25f
                },
                });
                rules.Add("mdlPaladin", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(0.055f, 0.1f, -0.325f),
                    localAngles = new Vector3(37.5f, 32.5f, 155f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlDeputy", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.1f, 0.225f, 0.05f),
                    localAngles = new Vector3(40f, 150f, 180f),
                    localScale = generalScale * 0.8f
                },
                });
                rules.Add("mdlDriver(Clone)", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "ThighR",
                    localPos = new Vector3(-0.06f, 0.055f, 0.08f),
                    localAngles = new Vector3(40f, 150f, 180f),
                    localScale = generalScale * 0.8f
                },
                });
                rules.Add("mdlHouse(Clone)", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "RightThigh",
                    localPos = new Vector3(0.05f, 0.1f, -0.18f),
                    localAngles = new Vector3(45f, 300f, 175f),
                    localScale = generalScale * 0.8f
                },
                });
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
                rules.Add("mdlCommandoDualies", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlHuntress", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.1f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlToolbot", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 1.5f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 10
                },
                });
                rules.Add("mdlEngi", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.25f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlMage", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.1f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlMerc", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.15f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlTreebot", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.4f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2
                },
                });
                rules.Add("mdlLoader", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlCroco", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 10
                },
                });
                rules.Add("mdlCaptain", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.2f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlBandit2", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlHeretic", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(-0.2f, 0f, 0f),
                    localAngles = new Vector3(90f, 0f, 0f),
                    localScale = generalScale * 2
                },
                });
                rules.Add("mdlRailGunner", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.075f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlVoidSurvivor", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.075f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale
                },
                });
                rules.Add("mdlMEL-T2", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Body",
                    localPos = new Vector3(0f, 0.03f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.25f
                },
                });
                rules.Add("mdlPaladin", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.15f, 0.1f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 2f
                },
                });
                rules.Add("mdlDeputy", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.1f, 0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                },
                });
                rules.Add("mdlDriver(Clone)", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0.175f, 0.05f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                },
                });
                rules.Add("mdlHouse(Clone)", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0f, 0f, 0f),
                    localAngles = new Vector3(0f, 0f, 0f),
                    localScale = generalScale * 0.8f
                },
                });
            }
        }
    }
}
