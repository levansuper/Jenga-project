using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace JengaGame
{
    public class GameManager : MonoBehaviour
    {
        private StackBlocksStorage _stackBlocksStorage;
        [SerializeField]
        private World world;

        async void Start()
        {
            await LoadStacks();
            StartGame();
        }


        async Task LoadStacks()
        {
            {
                try
                {
                    List<StackBlock> stackBlocks = await ApiManager.Instance.getStacks();
                    _stackBlocksStorage = new StackBlocksStorage(stackBlocks);
                }
                catch (Exception e)
                {
                    Debug.Log("AN ERROR WITH THE API");
                }
            }
        }


        void StartGame()
        {
            var stacks = _stackBlocksStorage.GetSortedStacks();
            world.Initialize(stacks);
        }
    }
    
}
