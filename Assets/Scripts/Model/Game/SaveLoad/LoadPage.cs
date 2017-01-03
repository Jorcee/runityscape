﻿using Scripts.Model.World.Pages;
using Scripts.Model.Interfaces;
using Scripts.Model.Pages;
using Scripts.Model.Processes;
using Scripts.View.ActionGrid;
using Scripts.Presenter;

namespace Scripts.Model.World.Serialization {

    /// <summary>
    /// Page that allows users to load their saved games.
    /// </summary>
    public class LoadPage : ReadPage {

        public LoadPage() : base("Select a file to load.", "", "Load", false) {
            OnEnterAction += () => {
                string[] filePaths = SaveLoad.GetSavePaths();
                for (int i = 0; i < filePaths.Length; i++) {
                    Camp camp = SaveLoad.Load(filePaths[i]);
                    string saveName = SaveLoad.SaveFileDisplay(filePaths[i], camp.Party.Leader.Level);

                    ActionGrid[i] = new Process(saveName, "Load this file.", () =>
                    Game.Instance.CurrentPage = new ReadPage(
                        camp.Party,
                        "",
                        "",
                        "Load this save?",
                        "",
                        new IButtonable[] {
                        new Process("Yes", "", () => Game.Instance.CurrentPage = camp),
                        new Process("No", "", () => Game.Instance.CurrentPage = this) }
                        )
                    );
                }
                ActionGrid[ActionGridView.TOTAL_BUTTON_COUNT - 1] = new Process("Back", "Return to the Main Menu", () => Game.Instance.CurrentPage = Game.Instance.StartMenu);
            };
        }
    }
}