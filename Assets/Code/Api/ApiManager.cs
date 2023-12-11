using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace JengaGame
{
    [Serializable]
    class ArrayWrapper<T>
    {
        public T[] Items;
    }
    public class ApiManager
    {
        private static string BLOCKS_ENDPOINT =
            "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";


        private static ApiManager _instance;

        public static ApiManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiManager();
                }

                return _instance;
            }
        }


        public string fixJSON(string value)
        {
            return "{\"Items\":" + value + "}";
        }
        

        public async Task<string> MakeRawRequestGet(string endpoint)
        {
            var request = UnityWebRequest.Get(endpoint);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
                Debug.LogError(request.downloadHandler.text);
                return null;
            }

            return request.downloadHandler.text;
        }
        
        
        

        public async Task<T> GetData<T>(string endpoint)
        {
                string serverResponse = await MakeRawRequestGet(endpoint);
                return JsonUtility.FromJson<T>(serverResponse);
          
        }       
        
        public async Task<List<T>> GetDataList<T>(string endpoint)
        {
             string serverResponse = await MakeRawRequestGet(endpoint);

            var resp = JsonUtility.FromJson<ArrayWrapper<T>>(fixJSON(serverResponse));
            
            List<T> list = new();
            list.AddRange(resp.Items);
            return list;
  
        }
        
        
        public async Task<List<StackBlock>> getStacks()
        
        {
            return await GetDataList<StackBlock>(BLOCKS_ENDPOINT);
        }
    }
}