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
        private static String HERFURY_PICKUP;
        private static String HERFURY_DESC;
        private static String HERFURY_LORE;
        private static String tier = "";
        public static void Init()
        {
            ItemTier Hbtier = ItemTier.Lunar;
            if (Hbdbt.Value == false)
                Hbtier = ItemTier.Tier3;
            else
                tier = "Lunar";
            HerFury = ScriptableObject.CreateInstance<ItemDef>();
            if (Hbnsfw?.Value ?? false)
            {
                HerFury.name = "HERFURY";
                HerFury.nameToken = "Her Fury";
            }
            else if (!Hbnsfw?.Value ?? true)
            {
                HerFury.name = "HERFURY";
                HerFury.nameToken = "Crystal Fury";
            }
            AddTokens();
            HerFury.pickupToken = HERFURY_PICKUP;
            HerFury.descriptionToken = HERFURY_DESC;
            HerFury.loreToken = HERFURY_LORE;
            HerFury.tier = Hbtier;
            if (Hbnsfw?.Value ?? false)
                HerFury.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>(Hbiiv.Value + "reallyredItemIcon");
            else if (!Hbnsfw?.Value ?? true)
                HerFury.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Fury" + tier + "ItemIcon");
            if (Hbnsfw?.Value ?? false)
                HerFury.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Hbiiv.Value + "reallyredher_burden");
            else if (!Hbnsfw?.Value ?? true)
                HerFury.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Fury");
            HerFury.canRemove = true;
            HerFury.hidden = false;
            if (!Hbvst.Value)
                HerFury.tags = new ItemTag[] { ItemTag.WorldUnique, (ItemTag)19 };
            else
                HerFury.tags = new ItemTag[] { (ItemTag)19 };

            var rules = new Items.CharacterItemDisplayRuleSet();
            AddLocation(rules);
            Items.Add(HerFury, rules);
        }
        private static void AddTokens()
        {
            if (Hbdbt.Value)
            {
                HERFURY_PICKUP = "Increase attack speed and decrease HP.\nAll item drops are now variants of: <color=#307FFF>" + HerBurden.nameToken + "</color>";
                HERFURY_DESC = $"Increase attack speed by {Hbbv}% and decrease HP by {Hbdbv}%.\nAll item drops are now variants of: <color=#307FFF>" + HerBurden.nameToken + "</color>";
            }
            if (!Hbdbt.Value)
            {
                HERFURY_PICKUP = "Increase attack speed.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + HerBurden.nameToken + "</color>";
                HERFURY_DESC = $"Increase attack speed by {Hbbv}%.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + HerBurden.nameToken + "</color>";
            }
            HERFURY_LORE = "None";

        }
        public static void AddLocation(Items.CharacterItemDisplayRuleSet rules)
        {
            if (!Hbisos.Value && (Hbnsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Hbiiv.Value + "reallyredher_burden");
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
            if (!Hbisos.Value && (!Hbnsfw?.Value ?? true))
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
            if (Hbisos.Value && (Hbnsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Hbiiv.Value + "reallyredher_burden");
                if (Hbvos.Value == "Fury")
                    followerPrefab.AddComponent<PrefabSizeScript>();
                if (Hbvos.Value != "Fury")
                    followerPrefab.AddComponent<FakeFuryPrefabSizeScript>();
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
                    localPos = new Vector3(0f, 0.2f, 0.2f),
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
            }
            if (Hbisos.Value && (!Hbnsfw?.Value ?? true))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Fury");
                Material what = Resources.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[0];
                Material what2 = Resources.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                what2.SetFloat("_Magnitude", 0.075f);
                var materials = followerPrefab.GetComponent<MeshRenderer>().materials;
                materials[2].shader = what.shader;
                materials[2].CopyPropertiesFromMaterial(what);
                materials[3].shader = what2.shader;
                materials[3].CopyPropertiesFromMaterial(what2);
                followerPrefab.GetComponent<MeshRenderer>().materials = materials;
                if (Hbvos.Value == "Fury")
                    followerPrefab.AddComponent<PrefabSizeScript>();
                if (Hbvos.Value != "Fury")
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
            }
        }
    }
}
