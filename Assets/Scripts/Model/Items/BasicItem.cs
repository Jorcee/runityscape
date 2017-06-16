﻿using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Model.Spells;
using UnityEngine;

namespace Scripts.Model.Items {

    public class BasicItem : Item {

        public BasicItem(Sprite icon, int basePrice, int count, int maxCount, string name, string description)
            : base(icon, basePrice, count, maxCount, TargetType.NONE, name, description) { }

        protected sealed override string DescriptionHelper {
            get {
                return string.Empty;
            }
        }

        protected sealed override bool IsMeetOtherRequirements(SpellParams caster, SpellParams target) {
            return true;
        }
    }
}