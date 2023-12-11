using System.Collections.Generic;
using UnityEngine;


namespace JengaGame
{
    
    public enum CubeType
    {
        Wood,
        Stone,
        Glass
    }

    [RequireComponent(
        typeof(BoxCollider), 
        typeof(Rigidbody), 
        typeof(MeshRenderer))]
    public class Cube : MonoBehaviour
    {
        public CubeType Type = CubeType.Glass;
        public string _text = "";
        [SerializeField]
        public Material _glassMaterial;
        [SerializeField]
        public Material _woodMaterial;
        [SerializeField]
        public Material _stoneMaterial;
        [SerializeField]
        public Material _selectedMaterial;

        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private WorldUI _worldUI;

        public void Initialize(CubeType type, string text, WorldUI worldUI)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _text = text;
            Type = type;
            _worldUI = worldUI;
            SetMaterial();
        }

        private void SetMaterial()
        {
            List<Material> materials = new List<Material>();
            switch (Type)
            {
                case CubeType.Wood:
                    materials.Add(_woodMaterial);
                    break;
                case CubeType.Stone:
                    materials.Add(_stoneMaterial);
                    break;
                default:
                    materials.Add(_glassMaterial);
                    break;
            };
            
            _meshRenderer.SetMaterials(materials);
        }

        public void RunSimulation()
        {
            _rigidbody.isKinematic = false;
            if (Type == CubeType.Glass)
            {
                Destroy(this.gameObject);
            }
        }
        
        void OnMouseDown()
        {
            _worldUI.SetCubeInfo(_text);
            _meshRenderer.material = _selectedMaterial;
        }      
        void OnMouseUp()
        {
            _worldUI.SetCubeInfo("");
            SetMaterial();
        }


    }

  
}
