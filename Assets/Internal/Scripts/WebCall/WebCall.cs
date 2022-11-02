using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
namespace Web
{

    public class WebCall : MonoBehaviour
    {
        ///////////////////////////////
        //  Protected methods        //
        ///////////////////////////////

        protected async Task<string> ApiGet(string url)
        {
            string returnValue = null;

            using var request = UnityWebRequest.Get(url);

            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                returnValue = request.downloadHandler.text;

            }
            else
            {

                Debug.LogError("Could Not Retrieve Word");
            }


            return returnValue;
        }
    }
}