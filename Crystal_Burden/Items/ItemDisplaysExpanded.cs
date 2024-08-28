using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Crystal_Burden
{
    class ItemDisplaysExpanded : Crystal_Burden
    {
        public static void HerItemDisplay(ItemDisplayRuleDict rules, String interalName, String itemName)
        {
            if (ItemVisibility.Value && (Nsfw?.Value ?? false))
            {
                GameObject followerPrefab = Crystal_Burden.bundle.LoadAsset<GameObject>(Artist.Value + interalName);
                if (VariantShownOnSurvivor.Value == itemName)
                {
                    followerPrefab.AddComponent<PrefabSizeScript>();
                    followerPrefab.transform.Find("DildoTrail").gameObject.SetActive(ParticleTrail.Value);
                }
                else
                {
                    if (itemName == "Burden")
                        followerPrefab.AddComponent<FakeBurdenPrefabSizeScript>();
                    else if (itemName == "Recluse")
                        followerPrefab.AddComponent<FakeReclusePrefabSizeScript>();
                    else if (itemName == "Fury")
                        followerPrefab.AddComponent<FakeFuryPrefabSizeScript>();
                    else if (itemName == "Torpor")
                        followerPrefab.AddComponent<FakeTorporPrefabSizeScript>();
                    else if (itemName == "Rancor")
                        followerPrefab.AddComponent<FakeRancorPrefabSizeScript>();
                    else if (itemName == "Panic")
                        followerPrefab.AddComponent<FakePanicPrefabSizeScript>();
                    followerPrefab.transform.Find("DildoTrail").gameObject.SetActive(false);
                }
                Vector3 generalScale = new Vector3(.0125f, .0125f, .0125f);
                rules.Add("mdlCommandoDualies", new ItemDisplayRule[] { new ItemDisplayRule
                    {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlHuntress", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlToolbot", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "LowerArmR",
                    localPos = new Vector3(0f, 5.5f, 0f),
                    localAngles = new Vector3(45f, -90f, 0f),
                    localScale = generalScale * 10
                }
                });
                rules.Add("mdlEngi", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlMage", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlMerc", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.25f, 0.05f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlTreebot", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "WeaponPlatform",
                    localPos = new Vector3(0.2f, 0.05f, 0.2f),
                    localAngles = new Vector3(-45f, 0f, 0f),
                    localScale = generalScale * 2
                }
                });
                rules.Add("mdlLoader", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.25f, 0.15f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlCroco", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Hip",
                    localPos = new Vector3(0f, 3.5f, 0f),
                    localAngles = new Vector3(135f, -0.05f, 0f),
                    localScale = generalScale * 10
                }
                });
                rules.Add("mdlCaptain", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlBandit2", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.1f, 0.1f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlHeretic", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(.3f, -.15f, 0f),
                    localAngles = new Vector3(20f, -120f, -36f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlRailGunner", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.25f, 0.1f),
                    localAngles = new Vector3(0f, 180f, 180f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlVoidSurvivor", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.30f, 0f),
                    localAngles = new Vector3(45f, 90f, 180f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlMEL-T2", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "HandR",
                    localPos = new Vector3(0.005f, -0.0075f, 0f),
                    localAngles = new Vector3(45f, -4f, 0f),
                    localScale = generalScale * 0.25f
                }
                });
                rules.Add("mdlPaladin", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(-0.025f, 0f, 0f),
                    localAngles = new Vector3(-15f, 0f, 0f),
                    localScale = generalScale * 2f
                }
                });
                rules.Add("mdlDeputy", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.125f, 0.075f),
                    localAngles = new Vector3(180f, -0.05f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlDriver(Clone)", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.125f, -0.075f),
                    localAngles = new Vector3(180f, 180f, 0f),
                    localScale = generalScale
                }
                });
                rules.Add("mdlHouse(Clone)", new ItemDisplayRule[] { new ItemDisplayRule
                {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = followerPrefab,
                    childName = "Pelvis",
                    localPos = new Vector3(0f, 0.15f, -0.025f),
                    localAngles = new Vector3(15f, 180f, 180f),
                    localScale = generalScale
                }
                });
            }
        }
    }
}
