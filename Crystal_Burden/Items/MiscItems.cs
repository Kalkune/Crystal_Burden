﻿using BetterAPI;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Crystal_Burden
{
    class MiscItems : Crystal_Burden
    {
        public static void Init()
        {
            HBItemPicker = ScriptableObject.CreateInstance<ItemDef>();
            HBItemPicker.name = "HBITEMPICKER";
            HBItemPicker.AutoPopulateTokens();
            HBItemPicker.deprecatedTier = ItemTier.NoTier;
            HBItemPicker.hidden = true;
            Items.Add(HBItemPicker, null);
        }
    }
}
