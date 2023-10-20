// 코인 아이템 구현
using UnityEngine;

public class Coin : MonoBehaviour
{
    int _score = 10;

    public void UseItem()
    {
        DataManager.Instance.AddCoin(_score);
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UseItem();
            Destroy(this.gameObject);
        }
    }
}
