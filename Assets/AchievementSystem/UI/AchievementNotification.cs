using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace LetiArts.Systems.Achievements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AchievementNotification : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI txt_title;
        [SerializeField] private Image img_Icon;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            GameEvents.OnAchievementUnlocked += ShowAchievement;
        }

        private void OnDisable()
        {
            GameEvents.OnAchievementUnlocked -= ShowAchievement;
        }

        public void ShowAchievement(AchievementEntry data)
        {
            txt_title.text = data.title;
            img_Icon.sprite = data.icon;

            canvasGroup.alpha = 1;
            StopAllCoroutines();  // good practice 
            StartCoroutine(AnimateNotification());

        }

        private IEnumerator AnimateNotification() 
        {
            Vector2 settledPosition = new Vector2(0, -100); // final position of the notification panel
            Vector2 hiddenPosition = new Vector2(0, 100); // off screen position of the panel

            rectTransform.anchoredPosition = hiddenPosition;  // putting our panel in the initial position we want it to be

            // Slide down & Fade in
            float elapsed = 0;
            float duration = 0.6f;

            // Area of concern
            while (elapsed < duration) // loop that runs until the animation duration is reached
            {
                elapsed += Time.deltaTime;  // making elapsed time use accurate time, independent of frame rate
                float t = elapsed / duration;  // t is our location navigation guy that guides our lerp mover
                float smoothT = Mathf.SmoothStep(0, 1, t);  // for smoother animation 

                rectTransform.anchoredPosition = Vector2.Lerp(hiddenPosition, settledPosition, smoothT);  // panel position mover 
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);  // panel fade in
                yield return null;   // wait for the next frame before continuing the loop
            }

            // Pause
            yield return new WaitForSeconds(2.5f);

            // Fade out
            elapsed = 0;  // set elapsed timer back to 0 after first half of animation

            while (elapsed < duration) 
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                rectTransform.anchoredPosition = Vector2.Lerp(settledPosition, hiddenPosition, t);
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
                yield return null;

            }
            canvasGroup.alpha = 0;
        }
    }

}