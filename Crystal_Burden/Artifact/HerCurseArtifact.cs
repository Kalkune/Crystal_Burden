using BetterAPI;
using RoR2;
using UnityEngine;

namespace Crystal_Burden
{
    class HerCurseArtifact : Crystal_Burden
    {
        public static void Init()
        {
            HerCurse = ScriptableObject.CreateInstance<ArtifactDef>();
            Languages.AddTokenString("HERCURSE_DESC", "All item drops will be turned into " + HerBurden.nameToken + " Variants");
            HerCurse.descriptionToken = "HERCURSE_DESC";
            if (Nsfw?.Value ?? false)
            {
                Languages.AddTokenString("HERCURSE_NAME", "Artifact of Her Curse");
                HerCurse.nameToken = "HERCURSE_NAME";
                HerCurse.smallIconSelectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactBurdenEnabled");
                HerCurse.smallIconDeselectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactBurdenDisabled");
            }
            else if (!Nsfw?.Value ?? true)
            {
                Languages.AddTokenString("HERCURSE_NAME", "Artifact of Crystal Curse");
                HerCurse.nameToken = "HERCURSE_NAME";
                HerCurse.smallIconSelectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactCrystalEnabled");
                HerCurse.smallIconDeselectedSprite = Crystal_Burden.bundle.LoadAsset<Sprite>("HerCurseArtifactCrystalDisabled");

            }
            Artifacts.Add(HerCurse);
        }
    }
}
