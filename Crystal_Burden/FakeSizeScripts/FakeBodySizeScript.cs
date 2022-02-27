using RoR2;
using System.Linq;
using UnityEngine;

namespace Crystal_Burden
{
    internal class FakeBodySizeScript : MonoBehaviour
    {
        private static int globalIndexTick = 0;
        private int personalIndex;

        private void OnEnable()
        {
            personalIndex = globalIndexTick;
            globalIndexTick++;
        }

        private void OnDisable()
        {
            globalIndexTick--;
        }

        //This is passed the stack count from Inventory, and hands it to the PrefabSizeScript
        internal void UpdateStacks(string item, int newStacks, CharacterBody body)
        {
            if (item == "burden")
                FakeSizeHandoffManager.burdenprefabSizeScripts.ElementAt(personalIndex).UpdateStacks(newStacks);
            if (item == "recluse")
                FakeSizeHandoffManager.recluseprefabSizeScripts.ElementAt(personalIndex).UpdateStacks(newStacks);
            if (item == "fury")
                FakeSizeHandoffManager.furyprefabSizeScripts.ElementAt(personalIndex).UpdateStacks(newStacks);
            if (item == "torpor")
                FakeSizeHandoffManager.torporprefabSizeScripts.ElementAt(personalIndex).UpdateStacks(newStacks);
            if (item == "rancor")
                FakeSizeHandoffManager.rancorprefabSizeScripts.ElementAt(personalIndex).UpdateStacks(newStacks);
            if (item == "panic")
                FakeSizeHandoffManager.panicprefabSizeScripts.ElementAt(personalIndex).UpdateStacks(newStacks);
        }

        //This will override the display rules, since scale is now dynamically set
        internal void SetBodyMultiplier(string nameToken, CharacterBody body)
        {
            float bodySizeMultiplier;
            //If you need to add rules, model it after the existing ones below
            switch (nameToken)
            {
                case "CROCO_BODY_NAME":
                    bodySizeMultiplier = 10;
                    break;
                case "TOOLBOT_BODY_NAME":
                    bodySizeMultiplier = 10;
                    break;
                case "TREEBOT_BODY_NAME":
                    bodySizeMultiplier = 2;
                    break;
                case "HERETIC_BODY_NAME":
                    bodySizeMultiplier = 2;
                    break;
                case "MELT_NAME":
                    bodySizeMultiplier = 0.25f;
                    break;
                //If there isn't a special rule, default to default
                default:
                    bodySizeMultiplier = 1;
                    break;
            }
            if (body.inventory.GetItemCount(Crystal_Burden.HerBurden.itemIndex) > 0 && Crystal_Burden.HerBurden.itemIndex != Crystal_Burden.VariantOnSurvivor.itemIndex)
                FakeSizeHandoffManager.burdenprefabSizeScripts.ElementAt(personalIndex).characterSizeMultiplier = bodySizeMultiplier;
            if (body.inventory.GetItemCount(Crystal_Burden.HerRecluse.itemIndex) > 0 && Crystal_Burden.HerRecluse.itemIndex != Crystal_Burden.VariantOnSurvivor.itemIndex)
                FakeSizeHandoffManager.recluseprefabSizeScripts.ElementAt(personalIndex).characterSizeMultiplier = bodySizeMultiplier;
            if (body.inventory.GetItemCount(Crystal_Burden.HerFury.itemIndex) > 0 && Crystal_Burden.HerFury.itemIndex != Crystal_Burden.VariantOnSurvivor.itemIndex)
                FakeSizeHandoffManager.furyprefabSizeScripts.ElementAt(personalIndex).characterSizeMultiplier = bodySizeMultiplier;
            if (body.inventory.GetItemCount(Crystal_Burden.HerTorpor.itemIndex) > 0 && Crystal_Burden.HerTorpor.itemIndex != Crystal_Burden.VariantOnSurvivor.itemIndex)
                FakeSizeHandoffManager.torporprefabSizeScripts.ElementAt(personalIndex).characterSizeMultiplier = bodySizeMultiplier;
            if (body.inventory.GetItemCount(Crystal_Burden.HerRancor.itemIndex) > 0 && Crystal_Burden.HerRancor.itemIndex != Crystal_Burden.VariantOnSurvivor.itemIndex)
                FakeSizeHandoffManager.rancorprefabSizeScripts.ElementAt(personalIndex).characterSizeMultiplier = bodySizeMultiplier;
            if (body.inventory.GetItemCount(Crystal_Burden.HerPanic.itemIndex) > 0 && Crystal_Burden.HerPanic.itemIndex != Crystal_Burden.VariantOnSurvivor.itemIndex)
                FakeSizeHandoffManager.panicprefabSizeScripts.ElementAt(personalIndex).characterSizeMultiplier = bodySizeMultiplier;
        }
    }
}