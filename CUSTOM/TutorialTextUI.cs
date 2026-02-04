using UnityEngine;
using TMPro;

namespace MarketShopandRetailSystem
{
    public class TutorialTextUI : MonoBehaviour
    {
        public static TutorialTextUI Instance;
        public TextMeshProUGUI tutorialText;

        void Awake()
        {
            Instance = this;
            Hide();
        }

        public void Show(string text)
        {
            tutorialText.text = text;
            tutorialText.gameObject.SetActive(true);
        }

        public void Hide()
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}
