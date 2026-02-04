using UnityEngine;

namespace MarketShopandRetailSystem
{
    public class UIWorldAnchor : MonoBehaviour
    {
        public RectTransform uiTarget;
        public Camera worldCamera;

        void LateUpdate()
        {
            if (uiTarget == null || worldCamera == null) return;

            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, uiTarget.position);
            screenPos.z = 5f; // jarak dari kamera (boleh diubah)

            transform.position = worldCamera.ScreenToWorldPoint(screenPos);
        }
    }
}
