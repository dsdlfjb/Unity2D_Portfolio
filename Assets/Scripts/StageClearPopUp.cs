using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageClearPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _clearTime;

    private void Awake()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnEnemyDie, this.OnEnemyDie);

            this.transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
            Managers.Event.RemoveEvent(Enum.EEventKey.OnEnemyDie, this.OnEnemyDie);
    }

    void OnEnemyDie(object enemyType)
    {
        if (EEnemyType.Boss == (EEnemyType)(enemyType))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Stage1":
                    if (DataManager.Instance.nowPlayer.maxStage < 1)
                        DataManager.Instance.nowPlayer.maxStage = 1;
                    break;

                case "Stage2":
                    if (DataManager.Instance.nowPlayer.maxStage < 2)
                        DataManager.Instance.nowPlayer.maxStage = 2;
                    break;

                case "Stage3":
                    if (DataManager.Instance.nowPlayer.maxStage < 3)
                        DataManager.Instance.nowPlayer.maxStage = 3;
                    break;

                case "Stage4":
                    if (DataManager.Instance.nowPlayer.maxStage < 4)
                        DataManager.Instance.nowPlayer.maxStage = 4;
                    break;

                case "Stage5":
                    if (DataManager.Instance.nowPlayer.maxStage < 5)
                        DataManager.Instance.nowPlayer.maxStage = 5;
                    break;
            }

            DataManager.Instance.Save();

            gameObject.SetActive(true);

            float clearTime = GameManager.Instance._gameTime;

            // N0 ~ N6응ㄴ 소수점 지정
            // N1 이면 소수점 첫째자리까지만 보여줌
            _clearTime.text = "Clear Time" + (Mathf.FloorToInt(clearTime / 60)).ToString("N0") + ":" + (clearTime % 60).ToString("N0");
        }
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(1);
    }
}
