using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
//using System.Web.Script.Serialization;
using WardenCore.Models;

namespace WardenCore.Controllers
{
    public class DeckController : ApiController
    {
        public string objective = "{0}Back/1.png";
        public string ploy = "{0}Back/2.png";

        public string preurl = "{2}{1}/{0}.png";



        // GET: api/Deck/getDeck
        [HttpGet]
        [ActionName("getDeck")]
        public HttpResponseMessage Get(string ObjectiveCard, string GambitCard, string UpgradeCard, string lang, string filename = "")
        {
            var prefix = ConfigurationManager.AppSettings["PreUrl"];


            DeckViewModel deck = new DeckViewModel();
            string[] objectivesID = null;
            string[] gambitIds = null;
            string[] upgradeIds = null;
            int[] objectivesIDForTTS = null;
            int[] gambitIDForTTS = null;
            int[] upgradeIDForTTS = null;
            try
            {
                objectivesID = ObjectiveCard.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                objectivesIDForTTS = Array.ConvertAll(Array.ConvertAll(objectivesID, x => x + "00"), x => int.Parse(x));
            }catch(Exception ex)
            { }
            try
            {
                gambitIds = GambitCard.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                gambitIDForTTS = Array.ConvertAll(Array.ConvertAll(gambitIds, x => x + "00"), x => int.Parse(x));
            }
            catch (Exception ex)
            { }
            try
            {
                upgradeIds = UpgradeCard.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                upgradeIDForTTS = Array.ConvertAll(Array.ConvertAll(upgradeIds, x => x + "00"), x => int.Parse(x));
            }catch(Exception ex)
            { }




            deck.ObjectStates[0].DeckIDs = objectivesIDForTTS != null ? objectivesIDForTTS : new int[0];
            deck.ObjectStates[1].DeckIDs = gambitIDForTTS != null && upgradeIDForTTS != null ? gambitIDForTTS.Concat(upgradeIDForTTS).ToArray() : new int[0];

            List<_containedObject> objCards = new List<_containedObject>();
            List<_containedObject> ployCards = new List<_containedObject>();


            // objectives
            if(objectivesID != null)
             fillObject(objectivesID, objectivesIDForTTS, objCards, true, lang, prefix);

            // gambit
            if(gambitIDForTTS != null)
            fillObject(gambitIds, gambitIDForTTS, ployCards, false, lang, prefix);

            // upgrade
            if(upgradeIDForTTS != null)
            fillObject(upgradeIds, upgradeIDForTTS, ployCards, false, lang, prefix);


            // deck for obj
            foreach (_containedObject c in objCards)
            {
                deck.ObjectStates[0].CustomDeck.Add(c.CustomDeck.First().Key, c.CustomDeck.First().Value);

            }

            // deck for ploys
             foreach (_containedObject c in ployCards)
            {
                deck.ObjectStates[1].CustomDeck.Add(c.CustomDeck.First().Key, c.CustomDeck.First().Value);

            }


            deck.ObjectStates[0].ContainedObject = objCards;
            deck.ObjectStates[1].ContainedObject = ployCards;

            var stream = new MemoryStream();
            // processing the stream.





            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(deck);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = filename == string.Empty ? "MyDeck.json" : filename + ".json"
                    };
            //result.Headers.Add("Access-Control-Allow-Origin", "*");
            return result;


            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            //return response;


        }


        private void fillObject(string[] stringIds, int[] Ids, List<_containedObject> cardList, bool isObjective, string lang, string prefix)
        {


            _containedObject toAdd = new _containedObject();
            for (int i = 0; i < stringIds.Length; i++)
            {

                toAdd.CustomDeck = new Dictionary<string, _customCard>();
                toAdd.CustomDeck.Add(stringIds[i], new _customCard
                {
                    BackURL = isObjective ? string.Format(objective, prefix): string.Format(ploy, prefix),
                    FaceURL = string.Format(preurl, stringIds[i], lang, prefix)
                });

                toAdd.CardID = Ids[i];


                cardList.Add(toAdd);
                toAdd = new _containedObject();
            }




        }
    }
}
