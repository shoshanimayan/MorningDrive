using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace Web
{

    
    public class RandomWordCall : WebCall
    {
        private class RandomWord
        {
            public string word { get; set; }
        }

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////
        public async Task<string> GetRandomWord()
        {
            string json= await ApiGet(WebCallConstants.RandomWordAddress);
            json=json.Replace("[", "");
            json=json.Replace("]", "");

            string word = null;
            if (json != null) {
                try
                {
                    RandomWord random = JsonConvert.DeserializeObject<RandomWord>(json);
                    word = random.word;
                }
                catch (Exception e)
                {
                    Debug.LogError("could not parse random word json: "+e.Message);
                }

            }

            return word;
        }

    }
}