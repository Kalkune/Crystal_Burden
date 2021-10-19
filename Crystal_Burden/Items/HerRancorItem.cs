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
    class HerRancorItem : Crystal_Burden
    {
        private static String HERRANCOR_PICKUP;
        private static String HERRANCOR_DESC;
        private static String HERRANCOR_LORE;
        private static String tier = "";
        public static void Init()
        {
            ItemTier Hbtier = ItemTier.Lunar;
            if (Hbdbt.Value == false)
                Hbtier = ItemTier.Tier3;
            else
                tier = "Lunar";
            HerRancor = ScriptableObject.CreateInstance<ItemDef>();
            if (Hbnsfw?.Value ?? false)
            {
                HerRancor.name = "HERRANCOR";
                HerRancor.nameToken = "Her Rancor";
            }
            else if (!Hbnsfw?.Value ?? true)
            {
                HerRancor.name = "HERRANCOR";
                HerRancor.nameToken = "Crystal Rancor";
            }
            AddTokens();
            HerRancor.pickupToken = HERRANCOR_PICKUP;
            HerRancor.descriptionToken = HERRANCOR_DESC;
            HerRancor.loreToken = HERRANCOR_LORE;
            HerRancor.tier = Hbtier;
            if (Hbnsfw?.Value ?? false)
                HerRancor.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>(Hbiiv.Value + "orangeItemIcon");
            else if (!Hbnsfw?.Value ?? true)
                HerRancor.pickupIconSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("Brdn_Crystal_Rancor" + tier + "ItemIcon");
            if (Hbnsfw?.Value ?? false)
                HerRancor.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Hbiiv.Value + "orangeher_burden");
            else if (!Hbnsfw?.Value ?? true)
                HerRancor.pickupModelPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Rancor");
            HerRancor.canRemove = true;
            HerRancor.hidden = false;
            if (!Hbvst.Value)
                HerRancor.tags = new ItemTag[] { ItemTag.WorldUnique, (ItemTag)19 };
            else
                HerRancor.tags = new ItemTag[] { (ItemTag)19 };

            var rules = new Items.CharacterItemDisplayRuleSet();
            AddLocation(rules);
            Items.Add(HerRancor, rules);
        }
        private static void AddTokens()
        {
            if (Hbdbt.Value)
            {
                HERRANCOR_PICKUP = "Increase damage and decrease armor.\nAll item drops are now variants of: <color=#307FFF>" + HerBurden.nameToken + "</color>";
                HERRANCOR_DESC = $"Increase damage by {Hbbv}5% and decrease armor by {Hbdbv}%.\nAll item drops are now variants of: <color=#307FFF>" + HerBurden.nameToken + "</color>";
            }
            if (!Hbdbt.Value)
            {
                HERRANCOR_PICKUP = "Increase damage.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + HerBurden.nameToken + "</color>";
                HERRANCOR_DESC = $"Increase damage by {Hbbv}%.\nMonsters now have a chance to drop variants of: <color=#e7553b>" + HerBurden.nameToken + "</color>";
            }
            HERRANCOR_LORE = "None";

        }
        public static void AddLocation(Items.CharacterItemDisplayRuleSet rules)
        {
            if (!Hbisos.Value && (Hbnsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Hbiiv.Value + "orangeher_burden");
                followerPrefab.AddComponent<FakeRancorPrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Rancor");
                followerPrefab.AddComponent<FakeRancorPrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Hbiiv.Value + "orangeher_burden");
                if (Hbvos.Value == "Rancor")
                    followerPrefab.AddComponent<PrefabSizeScript>();
                if (Hbvos.Value != "Rancor")
                    followerPrefab.AddComponent<FakeRancorPrefabSizeScript>();
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
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>("Brdn_Crystal_Rancor");
                Material what = Resources.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[0];
                Material what2 = Resources.Load<GameObject>("prefabs/networkedobjects/LockedMage").transform.Find("ModelBase/IceMesh").GetComponent<MeshRenderer>().materials[1];
                what2.SetFloat("_Magnitude", 0.075f);
                var materials = followerPrefab.GetComponent<MeshRenderer>().materials;
                materials[2].shader = what.shader;
                materials[2].CopyPropertiesFromMaterial(what);
                materials[3].shader = what2.shader;
                materials[3].CopyPropertiesFromMaterial(what2);
                followerPrefab.GetComponent<MeshRenderer>().materials = materials;
                if (Hbvos.Value == "Rancor")
                    followerPrefab.AddComponent<PrefabSizeScript>();
                if (Hbvos.Value != "Rancor")
                    followerPrefab.AddComponent<FakeRancorPrefabSizeScript>();
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(0.05f, 0.3f, -0.05f),
                    localAngles = new Vector3(180f, 0f, 0f),
                    localScale = generalScale
                }, "mdlCommandoDualies"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(-0.05f, 0.2f, 0.025f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlHuntress"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(0.35f, 3f, 1f),
                    localAngles = new Vector3(0f, 0f, 140f),
                    localScale = generalScale * 10
                }, "mdlToolbot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(-0.02f, 0.25f, 0f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlEngi"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(0f, 0.3f, -0.05f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlMage"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(-0.02f, 0.2f, 0.01f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlMerc"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "WeaponPlatform",
                    localPos = new Vector3(0.1f, 0.3f, 0.25f),
                    localAngles = new Vector3(0f, 0f, 155f),
                    localScale = generalScale * 2
                }, "mdlTreebot"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "MechLowerArmL",
                    localPos = new Vector3(0.05f, 0.4f, -0.05f),
                    localAngles = new Vector3(180f, 0f, 0f),
                    localScale = generalScale
                }, "mdlLoader"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(-0.75f, 1.5f, 0.5f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale * 10
                }, "mdlCroco"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(-0.05f, 0.45f, -0.015f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlCaptain"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmL",
                    localPos = new Vector3(-0.025f, 0.2f, 0f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlBandit2"
                );
                rules.AddCharacterModelRule(new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Chest",
                    localPos = new Vector3(-0.05f, 0.2f, 0.025f),
                    localAngles = new Vector3(-20f, -160f, 135f),
                    localScale = generalScale
                }, "mdlHeretic"
                );
            }
        }
    }
}
