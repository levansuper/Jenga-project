using TMPro;
using UnityEngine;

namespace JengaGame
{

    public class WorldUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cubeInfo;


        public void SetCubeInfo(string text)
        {
            _cubeInfo.text = text;
        }
        
    }
}