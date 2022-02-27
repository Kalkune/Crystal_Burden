using RoR2;
using UnityEngine;

namespace Crystal_Burden
{
    class HerCurseArtifact : Crystal_Burden
    {
        public static void Init()
        {
            HerCurse = ScriptableObject.CreateInstance<ArtifactDef>();

            HerCurse.descriptionToken = "All item drops will be turned into " + HerBurden.nameToken + " Variants";
            if (Nsfw?.Value ?? false)
            {
                HerCurse.nameToken = "Artifact of Her Curse";
                HerCurse.smallIconSelectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactBurdenEnabled");
                HerCurse.smallIconDeselectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactBurdenDisabled");
            }
            else if (!Nsfw?.Value ?? true)
            {
                HerCurse.nameToken = "Artifact of Crystal Curse";
                HerCurse.smallIconSelectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactCrystalEnabled");
                HerCurse.smallIconDeselectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactCrystalDisabled");

            }
            BetterAPI.Artifacts.Add(HerCurse);
        }
    }
}
