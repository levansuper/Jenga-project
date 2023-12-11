using System;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;


namespace JengaGame
{
  
    public struct CubeData
    {
        public CubeType Type;
        public string JsonData;
    }

    public struct JengaData
    {
        public WorldUI WorldUI;
        public Vector3 Position;
        public string Name;
        public List<CubeData> Cubes;
        public Vector3 CubeSizes;
    }

    public class Jenga : MonoBehaviour
    {
        
        [SerializeField] private Cube _cube;
        [SerializeField] private Transform _cubeParent;
        [SerializeField] private TMP_Text _jengaName;
        [SerializeField] private  CinemachineVirtualCamera _camera;
        private List<Cube> _initializedCubes = new (); 
        private bool _simulationStarted = false;
        private JengaData _jengaData;
        private bool _isSelected = false;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
        }
        
        public void Initialize(JengaData jengaData)
        {
            _jengaData = jengaData;
            ReloadJenga();
        }

        public void RunSimulationOrReload()
        {
            if (_simulationStarted)
            {
                ReloadJenga();
            }
            else
            {
                RunSimulation();
            }
        }

        public void RunSimulation()
        {
            _simulationStarted = true;
            _initializedCubes.ForEach(c => c.RunSimulation());
        }

        public void ReloadJenga()
        {

            _simulationStarted = false;
            transform.position = _jengaData.Position;
            _jengaName.text = _jengaData.Name;
            
            
            GameObject newCubeParent = new GameObject();
            newCubeParent.transform.parent = _cubeParent.parent;
            newCubeParent.transform.position = _cubeParent.position;
            newCubeParent.transform.rotation = _cubeParent.rotation;
            newCubeParent.transform.localScale = _cubeParent.localScale;
            newCubeParent.name = "Cubes";
            Destroy(_cubeParent.gameObject);
            _cubeParent = newCubeParent.transform;

            GenerateCubes();

        }

        public void Select()
        {
            _isSelected = true;
            _camera.Priority = 100;
            _jengaName.alpha = 1;
        }
        
        public void Deselect()
        {
            _isSelected = false;
            _camera.Priority = 0;
            _jengaName.alpha = 0;
        }


        private void GenerateCubes()
        {
            _initializedCubes = new();
            int level = -1;
            for(int i = 0; i<_jengaData.Cubes.Count; i++)
            {
                if (i % 3 == 0)
                {
                    level++;
                }

                CubeData cubeData = _jengaData.Cubes[i];
                
                Cube newCube = Instantiate<Cube>(_cube, _cubeParent);
                newCube.Initialize(cubeData.Type, cubeData.JsonData, _jengaData.WorldUI);
                newCube.transform.localScale = _jengaData.CubeSizes;

                float positionX = 0;
                float positionZ = 0;
                float positionY = level * _jengaData.CubeSizes.y;
                float horizontalPosition = 0;
                if (i % 3 == 0)
                {
                    horizontalPosition = -_jengaData.CubeSizes.x - (_jengaData.CubeSizes.x * 0.1f);
                }else if (i % 3 == 2)
                {
                    horizontalPosition = _jengaData.CubeSizes.x + (_jengaData.CubeSizes.x * 0.1f);
                }

                float rotationY = 0;
                
                if (level % 2 == 1)
                {
                    rotationY = 90;
                    positionZ = horizontalPosition;
                }
                else
                {
                    positionX = horizontalPosition;
                }

                newCube.transform.localPosition = new Vector3(positionX, positionY, positionZ);
                newCube.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
                _initializedCubes.Add(newCube);
            }
        }

    }
}