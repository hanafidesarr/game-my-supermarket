using UnityEngine;

namespace MarketShopandRetailSystem
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance;

        [SerializeField] private bool forceTutorial = false;

        [Header("Tutorial Targets")]
        public GameObject step1_Target;
        public GameObject step2_Target;
        public GameObject step3_Target;
        public GameObject step4_Target;
        public GameObject step5_Target;
        public GameObject step6_Target;

        private int step = 0;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            if (forceTutorial)
            {
                PlayerPrefs.DeleteKey("TutorialDone");
                PlayerPrefs.Save();
            }

            if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
            {
                StartTutorial();
            }
        }

        void StartTutorial()
        {
            step = 1;
            LockPlayer(false);

            SetStep(step1_Target,
                "Rent the store and clean it to start your business.");
        }

        // 🔥 SATU NEXT STEP SAJA
        public void NextStep()
        {
            step++;

            switch (step)
            {
                case 2:
                    SetStep(step2_Target,
                        "Check the mailbox to receive your first bill.");
                    break;

                case 3:
                    SetStep(step3_Target,
                        "Buy furniture, food, drinks, and discount posters for your store.");
                    break;

                case 4:
                    SetStep(step4_Target,
                        "Throw away unused items in the trash bin.");
                    break;

                case 5:
                    SetStep(step5_Target,
                        "Use the open/close board to manage your store.");
                    break;

                case 6:
                    SetStep(step6_Target,
                        "Rest on the bed to continue to the next day.");
                    break;

                default:
                    FinishTutorial();
                    break;
            }
        }

        void SetStep(GameObject target, string message)
        {
            TargetPointer.Instance.PointedTarget = target;
            TutorialUI.Instance.Show(message);
        }

        void FinishTutorial()
        {
            TargetPointer.Instance.PointedTarget = null;
            TutorialUI.Instance.Hide();
            PlayerPrefs.SetInt("TutorialDone", 1);
        }

        void LockPlayer(bool value)
        {
            FirstPersonController.Instance.enabled = !value;
        }
    }
}
