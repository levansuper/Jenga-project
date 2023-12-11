using System.Collections.Generic;
using System.Linq;

namespace JengaGame
{
    

    public class StackData
    {
        private List<StackBlock> _stackBlocks;
        public string StackName;

        public StackData(string name)
        {
            StackName = name;
            _stackBlocks = new List<StackBlock>();
        }

        public void AddStackBlock(StackBlock value)
        {
            _stackBlocks.Add(value);
        }

        public void SortBlocks()
        {
            StackBlock[] blockArray = _stackBlocks.ToArray();
            var sorted = blockArray.OrderBy(x => x.domain).ThenBy(x => x.cluster ).ThenBy(x => x.id); 
            List<StackBlock> list = new();
            _stackBlocks.Clear();
            _stackBlocks.AddRange(sorted);
        }       
        
        public List<StackBlock> GetStackBlocks()
        {
            return _stackBlocks;
        }
    }



}