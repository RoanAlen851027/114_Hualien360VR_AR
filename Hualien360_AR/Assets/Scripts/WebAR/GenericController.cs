using UnityEngine;
using UnityEngine.Events;

namespace ARWT.Marker{
    public class GenericController : MonoBehaviour
    {
        public bool isDetected { get; protected set; } = false;
        public int ImageIndex = 0;
        public float updateSpeed = 10;
        public float positionThreshold = 0.1f;
        public float rotationThreshold = 3f;
        public UnityEvent OnVisible;
        public UnityEvent OnLost;

        bool isFirstTimeVisible = true;
        bool isFirstTimeLost = true;

        [SerializeField]
        Camera Camera;

        private void Start()
        {
            if (isDetected)
                OnVisible.Invoke();
            else
                OnLost.Invoke();

            if (Camera == default)
                Camera = Camera.main;
        }

        void OnEnable() {
            DetectionManager.onMarkerDetected += OnMarkerDetected;
            isFirstTimeVisible = true;
        }

        void OnDisable()
        {
            DetectionManager.onMarkerDetected -= OnMarkerDetected;
        }

        void OnMarkerDetected(MarkerInfo m)
        {
            if (!enabled)
                return;

            if (m.imageIndex != ImageIndex)
                return;

            isDetected = m.isVisible;
            if (m.isVisible)
            {
                UpdateTransform(m);
                if (isFirstTimeVisible)
                {
                    isFirstTimeVisible = false;
                    OnVisible.Invoke();
                }
                isFirstTimeLost = true;
            }
            else 
            {
                if (isFirstTimeLost)
                {
                    isFirstTimeLost = false;
                    OnLost.Invoke();
                }
                isFirstTimeVisible = true;
            }
        }
        
        void UpdateTransform(MarkerInfo m)
        {
            Vector3 absScale = new Vector3(
                Mathf.Abs(m.scale.x),
                Mathf.Abs(m.scale.y),
                Mathf.Abs(m.scale.z));

            var rotation = Quaternion.Euler(m.rotation.eulerAngles.x, -m.rotation.eulerAngles.z, -m.rotation.eulerAngles.y) * Quaternion.Euler(90f, 0f, 0f);
            var position = Quaternion.Euler(m.rotation.eulerAngles.x, -m.rotation.eulerAngles.z, -m.rotation.eulerAngles.y) * new Vector3(-m.position.x / m.scale.x * 7.5f, -m.position.z / m.scale.z * 7.5f, -m.position.y / m.scale.y * 7.5f);

            if (!isFirstTimeVisible)
            {

                    if (rotation.x + rotation.y + rotation.z + rotation.w != 0)
                    Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, rotation, Time.deltaTime * updateSpeed);

                    Camera.transform.position = Vector3.Lerp(Camera.transform.position, position, Time.deltaTime * updateSpeed);
            }
            else
            {
                Camera.transform.position = position;
                Camera.transform.rotation = rotation;
            }
        }
    }
}
