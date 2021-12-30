using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugalyzer
{
    public class Drugalyzer : BaseScript
    {
        private bool beingDrugalyzed = false;
        private int currentnetidofficer;
        public Drugalyzer()
        {
            TriggerEvent("chat:addSuggestion", "/druga", "Starts drug test procedure on specified player", new[]
            {
                new { name = "ID", help = "ID of the player to drugalyze" },                
            });
            TriggerEvent("chat:addSuggestion", "/saliva", "Provide a specimen of saliva", new[]
            {
                new { name = "cannabis", help = "Either true or false depending on if there is cannabis present." },
                new { name = "cocaine", help = "Either true or false depending on if there is cocaine present." }
            });
            EventHandlers["DGZ:ReceiveTest"] += new Action<int, string>(HandleBreathalyzerReceived);
            EventHandlers["DGZ:ProvideSample"] += new Action<string, string, string>(provideSample);
            EventHandlers["DGZ:FailProvide"] += new Action<string>(failProvide);
        }

        private async void HandleBreathalyzerReceived(int netidofficer, string officername)
        {
            if (beingDrugalyzed) { return; }
            beingDrugalyzed = true;
            int count = -1;
            currentnetidofficer = netidofficer;
            while (beingDrugalyzed)
            {
                count++;
                if (count % 20 == 0)
                {
                    TriggerEvent("chatMessage", "DRUGALYZER", new int[] { 255, 255, 0 }, "You're being drugalyzed by " + officername + ". Type '/saliva cannabis cocaine' or '/failprovide'.");
                }
                Ped officer = Players[netidofficer].Character;
                if (count > 120 || Vector3.Distance(officer.Position, Game.PlayerPed.Position) > 20)
                {
                    TriggerEvent("chatMessage", "DRUGALYZER", new int[] { 255, 255, 0 }, "You failed to provide a valid sample of saliva.");
                    TriggerServerEvent("DGZ:SendMessage", currentnetidofficer, "The suspect failed to provide a valid sample of saliva.");
                    beingDrugalyzed = false;
                }
                await Delay(500);
            }
        }

        private void failProvide(string SuspectName)
        {
            if (beingDrugalyzed)
            {
                TriggerEvent("chatMessage", "DRUGALYZER", new int[] { 255, 255, 0 }, "You failed to provide a valid sample of saliva.");
                TriggerServerEvent("DGZ:SendMessage", currentnetidofficer, SuspectName + " failed to provide a valid sample of saliva.");
                beingDrugalyzed = false;
            }
        }

        private void provideSample(string SuspectName, string cannabisString, string cocaineString)
        {
            if (beingDrugalyzed)
            {
                bool cannabis, cocaine;
                if (bool.TryParse(cannabisString, out cannabis) && bool.TryParse(cocaineString, out cocaine))
                {

                    TriggerEvent("chatMessage", "DRUGALYZER", new int[] { 255, 255, 0 }, "You provided a saliva sample. Cannabis: " + (cannabis ? "POSITIVE" : "NEGATIVE") + ". Cocaine: " 
                        + (cocaine ? "POSITIVE" : "NEGATIVE") + ".");
                    TriggerServerEvent("DGZ:SendMessage", currentnetidofficer, SuspectName + " provided a saliva sample. Cannabis: " + (cannabis ? "POSITIVE" : "NEGATIVE") + ". Cocaine: "
                        + (cocaine ? "POSITIVE" : "NEGATIVE") + ".");
                    beingDrugalyzed = false;
                }
                else
                {
                    TriggerEvent("chatMessage", "DRUGALYZER", new int[] { 255, 255, 0 }, "Type '/saliva cannabis cocaine' (replace each with true or false) or '/failprovide'.");
                }
            }               
        }
    }
}
