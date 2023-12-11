using System.Collections.Generic;
using UnityEngine;

namespace JengaGame
{
    

    public class World : MonoBehaviour
    {
        private Transform _jengasParent;
        [SerializeField] private WorldUI _worldUI;
        [SerializeField] private Jenga _jengaPrefab;
        [SerializeField] private float _distanceBetweenJengas; 
        [SerializeField] private Vector3 _cubeSizes = new Vector3 { x = 0.25f, y = 0.15f, z = 0.75f };
        private List<Jenga> _jengas = new List<Jenga>();
        private int _currentlyActiveJengaIndex = 0;
        
        public void Initialize(List<StackData> stacks)
        {
            if (_jengasParent != null)
            {
                Destroy(_jengasParent.gameObject);    
            }

            _jengasParent = new GameObject().transform;
            _jengasParent.parent = transform;
            _jengasParent.name = "Jengas";

            _jengas = new List<Jenga>();
            for(int i = 0; i<stacks.Count; i++){
                
                Jenga newJenga = Instantiate<Jenga>(_jengaPrefab, _jengasParent);
                _jengas.Add(newJenga);
                List<CubeData> cubes = new ();

                stacks[i].GetStackBlocks().ForEach(stackBlock =>
                {

                    CubeData cubeData = new CubeData();
                    cubeData.JsonData = JsonUtility.ToJson(stackBlock, true);
                    switch (stackBlock.mastery)
                    {
                        case 2:
                            cubeData.Type = CubeType.Stone;
                            break;
                        case 1:
                            cubeData.Type = CubeType.Wood;
                            break;
                        default:
                            cubeData.Type = CubeType.Glass;
                            break;
                    }
                    cubes.Add(cubeData);
                });

                newJenga.Initialize(new JengaData
                {
                    Name = stacks[i].StackName,
                    Position = new Vector3(i * _distanceBetweenJengas, 0, 0),
                    Cubes = cubes,
                    CubeSizes = _cubeSizes,
                    WorldUI = _worldUI,
                });
            }

            MoveCameraToCurrent();
        }

        public void MoveCameraToIndex(int index)
        {
            if (index < 0)
            {
                index = _jengas.Count - 1;
            }

            if (index > _jengas.Count - 1)
            {
                index = 0;
            }
            _jengas.ForEach(j => j.Deselect());
            _jengas[index].Select();
            _currentlyActiveJengaIndex = index;
        }        
        
        public void MoveCameraToCurrent()
        {
            MoveCameraToIndex(_currentlyActiveJengaIndex);
        }        
        
        public void MoveToPrev()
        {
            MoveCameraToIndex(_currentlyActiveJengaIndex - 1);
        }        
        
        public void MoveToNext()
        {
            MoveCameraToIndex(_currentlyActiveJengaIndex + 1);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveToPrev();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveToNext();
            }            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                RunSimulationOrReload();
            }
        }

        private void RunSimulationOrReload()
        {
            _jengas[_currentlyActiveJengaIndex].RunSimulationOrReload();
        }
    }
    
    

}