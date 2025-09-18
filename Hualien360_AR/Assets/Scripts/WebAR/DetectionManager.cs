using ARWT.Core;
using UnityEngine;
using UnityEngine.Events;

namespace ARWT.Marker
{
    [System.Serializable]
    public class MarkerInfo{
        public int imageIndex;
        public bool isVisible;
        public Vector3 position;
#if UNITY_EDITOR
        [EulerAngles]
#endif
        public Quaternion rotation;
        public Vector3 scale;

        public MarkerInfo(int imageIndex, bool isVisible, Vector3 position, Quaternion rotation, Vector3 scale){
            this.imageIndex = imageIndex;
            this.isVisible = isVisible;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }

    public class DetectionManager : Singleton<DetectionManager>
    {

#if UNITY_EDITOR
        public MarkerInfo markerInfo;
#endif
        public delegate void MarkerDetection(MarkerInfo m);
        public static event MarkerDetection onMarkerDetected;
        public static UnityEvent onArReady = new UnityEvent();
        public static UnityEvent onArError = new UnityEvent();

        public static bool isArReady { get; protected set; } = false;
        public static bool isArError { get; protected set; } = false;

        [System.Obsolete]
        protected virtual void Start()
        {
#if UNITY_EDITOR
            arReady();
#else
            Application.ExternalCall("detectionManagerReady");
#endif
        }
#if UNITY_EDITOR
        public void Update()
        {
            var m = new MarkerInfo(markerInfo.imageIndex, markerInfo.isVisible, markerInfo.position, markerInfo.rotation, markerInfo.scale);
            onMarkerDetected?.Invoke(m);
        }
#endif
        public virtual void arReady()
        {
            isArReady = true;
            onArReady?.Invoke();
        }

        public virtual void arError()
        {
            isArError = true;
            onArError?.Invoke();
        }

        public virtual void markerInfos(string infos)
        {
            if (!isActiveAndEnabled)
                return;

            string[] datas =  infos.Split(","[0]);

            int imageIndex = int.Parse(datas[0]);
            bool isVisible = bool.Parse(datas[1]);
            float posX = float.Parse(datas[2].ToString());
            float posY = float.Parse(datas[3].ToString());
            float posZ = float.Parse(datas[4].ToString());
            float rotX = float.Parse(datas[5].ToString());
            float rotY = float.Parse(datas[6].ToString());
            float rotZ = float.Parse(datas[7].ToString());
            float rotW = float.Parse(datas[8].ToString());
            float scaX = float.Parse(datas[9].ToString());
            float scaY = float.Parse(datas[10].ToString());
            float scaZ = float.Parse(datas[11].ToString());

            MarkerInfo m = new MarkerInfo(
                imageIndex,
                isVisible,
                new Vector3(posX, posY, posZ),
                new Quaternion(rotX, rotY, rotZ, rotW),
                new Vector3(scaX, scaY, scaZ)
            );

            if(onMarkerDetected != default){
                onMarkerDetected(m);
            }
        }
    }
}
