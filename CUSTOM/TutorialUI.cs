using UnityEngine;
using TMPro;
using System.Collections;

namespace MarketShopandRetailSystem
{
    public class TutorialUI : MonoBehaviour
    {
        public static TutorialUI Instance;

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float typingSpeed = 0.04f;

        private Coroutine typingRoutine;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            gameObject.SetActive(false);
            text.text = "";
        }

        public void Show(string message)
        {
            gameObject.SetActive(true);

            if (typingRoutine != null)
                StopCoroutine(typingRoutine);

            typingRoutine = StartCoroutine(TypeText(message));
        }

        IEnumerator TypeText(string message)
        {
            text.text = "";
            foreach (char c in message)
            {
                text.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public void Hide()
        {
            if (typingRoutine != null)
                StopCoroutine(typingRoutine);

            gameObject.SetActive(false);
        }

        // 🔥 DIPANGGIL BUTTON
        public void OnNextButton()
        {
            TutorialManager.Instance.NextStep();
        }
    }
}
