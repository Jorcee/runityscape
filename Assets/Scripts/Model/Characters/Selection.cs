﻿using System;

namespace Scripts.Model.Characters {

    /// <summary>
    /// Type-safe enum used to represent the various
    /// menus of the BattlePage.
    /// </summary>
    public sealed class Selection : IComparable {
        public static readonly Selection ACT = new Selection(Type.ACTION, "Action", "ACTION");
        public static readonly Selection EQUIP = new Selection(Type.EQUIP, "Equip", "EQUIPMENT");
        public static readonly Selection FLEE = new Selection(Type.FLEE, "Flee", "FLEE");
        public static readonly Selection ITEM = new Selection(Type.ITEM, "Item", "ITEM");
        public static readonly Selection ROOT = new Selection(Type.ROOT, "FAIM", "");
        public static readonly Selection SPELL = new Selection(Type.SPELL, "Spell", "SPELL");
        public static readonly Selection SWITCH = new Selection(Type.SWITCH, "Switch", "SWITCH");
        public static readonly Selection TARGET = new Selection(Type.TARGET, "Target", "TARGET");

        private Selection(Type type, string name, string text) {
            this.SelectionType = type;
            this.Name = name;
            this.Text = text;
        }

        public enum Type {
            ROOT, SPELL, ACTION, ITEM, FLEE, TARGET, SWITCH, EQUIP
        }

        public string Name { get; private set; } //Name of selection: Example: Spell
        public Type SelectionType { get; private set; }
        public string Text { get; private set; } //Part of the tooltip text

        public int CompareTo(object obj) {
            return this.SelectionType.CompareTo(((Selection)obj).SelectionType);
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Selection return false.
            Selection p = obj as Selection;
            if ((object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return p.SelectionType == this.SelectionType;
        }

        public override int GetHashCode() {
            return SelectionType.GetHashCode();
        }
    }
}