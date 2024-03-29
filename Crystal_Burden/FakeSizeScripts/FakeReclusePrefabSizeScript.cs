﻿using UnityEngine;

namespace Crystal_Burden
{
    internal class FakeReclusePrefabSizeScript : MonoBehaviour
    {
        //This Vector will need to match the generalScale used in AddLocation
        private Vector3 originalScale;
        private Vector3 newScale;
        internal float characterSizeMultiplier;
        //These could be set in a config if you wanted to
        internal static float maxSizeMultiplier = Crystal_Burden.MaxSize.Value, stackSizeMultiplier = (Crystal_Burden.MaxSize.Value - 1) / Crystal_Burden.SizeMultiplier.Value;

        private void OnEnable()
        {
            originalScale = new Vector3(.0125f, .0125f, .0125f);
            newScale = new Vector3(0.05f, 0.05f, 0.05f);
            characterSizeMultiplier = 1f;
            FakeSizeHandoffManager.recluseprefabSizeScripts.Add(this);
        }

        private void Update()
        {
            transform.localScale = newScale;
        }

        private void OnDisable()
        {
            FakeSizeHandoffManager.recluseprefabSizeScripts.Remove(this);
        }

        //This handles all of the item size changes, and is called by BodySizeScript
        internal void UpdateStacks(int newStacks)
        {
            float testSizeMultiplier = 1 + (newStacks * stackSizeMultiplier);
            if (testSizeMultiplier <= maxSizeMultiplier)
            {
                newScale = originalScale * characterSizeMultiplier * testSizeMultiplier;
            }
            else
            {
                newScale = originalScale * characterSizeMultiplier * maxSizeMultiplier;
            }
        }
    }
}