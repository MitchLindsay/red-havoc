﻿using Assets.Code.UI.Static;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class ShowSplashScreenEvent : Event
    {
        private SplashScreen splashScreen;
        private float showDuration;

        public ShowSplashScreenEvent(EventID eventID, object sender, EventArgs<SplashScreen, float> e) : base(eventID, sender, e)
        {
            this.splashScreen = e.Value;
            this.showDuration = e.Value2;
        }

        public override IEnumerator Execute()
        {
            splashScreen.Show();

            yield return new WaitForSeconds(showDuration);

            splashScreen.Hide();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}