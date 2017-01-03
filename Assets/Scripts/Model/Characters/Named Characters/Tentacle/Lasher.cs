﻿using Script.Model.Characters.Named;
using Scripts.Model.Spells.Named;
using Scripts.Model.Stats.Resources;
using UnityEngine;

namespace Scripts.Model.Characters.Named {

    public class Lasher : Mimic {

        public Lasher()
            : base(
                new Displays {
                    Loc = "Icons/spiked-tentacle",
                    Name = "Lasher",
                    Color = Color.white,
                    Check = "A tentacle optimized for combat. Many have tried to weaponize these creatures, only to realize that they are at best, abysmal whips."
                },
                new Displays {
                    Loc = "Icons/gladius",
                    Name = "Knight",
                    Color = Color.white,
                    Check = "A knight in shining white armor, sans horse. Rumored to have an eccentric and suboptimal walking style."
                },
                new StartStats {
                    Lvl = 1,
                    Str = 1,
                    Int = 1,
                    Agi = 1,
                    Vit = 1
                }
                ) {
            this.AddResource(new NamedResource.DeathExperience(3));
        }

        protected override void DecideSpell() {
            if (isTransformed) {
                QuickCast(new Lash());
            } else {
                QuickCast(new Attack());
            }
        }

        public override Mimic Summon() {
            return new Lasher();
        }
    }
}