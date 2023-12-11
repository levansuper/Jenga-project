using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JengaGame
{
    public class StackBlocksStorage
    {
        private Dictionary<string, StackData> _stackDatas;
        public StackBlocksStorage(List<StackBlock> stackBlocks)
        {
            _stackDatas = new Dictionary<string, StackData>();
            stackBlocks.ForEach((sb) =>
            {
                if (!_stackDatas.ContainsKey(sb.grade))
                {
                    _stackDatas.Add(sb.grade, new StackData(sb.grade));
                }

                StackData sd = _stackDatas[sb.grade];
                sd.AddStackBlock(sb);
            });
            
            foreach (var sd in _stackDatas)
            {
                sd.Value.SortBlocks();
            }
        }

        public List<StackData> GetSortedStacks()
        {
            List<string> stackNames = _stackDatas.Keys.OrderBy(x => x).ToList();
            List<StackData> stackDatas = new();
            stackNames.ForEach(x =>
            {
                StackData sd = _stackDatas[x];
                stackDatas.Add(sd);
            });
            return stackDatas;

        }
    }
}


