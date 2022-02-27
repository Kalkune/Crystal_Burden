using BodyBlend;
using RoR2;
using UnityEngine;

namespace Crystal_Burden
{
    static class BodyBlendCompatibility
    {

        public static void SetBlendValue(this CharacterBody body, string name, float value, string source)
        {
            var controller = body.GetBodyBlendController();
            if (controller)
                controller.SetBlendTargetWeight(name, value, source);
        }

        public static void RemoveBlend(this CharacterBody body, string name, string source)
        {
            var controller = body.GetBodyBlendController();
            if (controller)
                controller.RemoveBlendTargetWeight(name, source);
        }

        private static BodyBlendController GetBodyBlendController(this CharacterBody body)
        {
            return body.modelLocator.modelTransform.gameObject.GetComponent<BodyBlendController>();
        }
    }
}