using UnityEngine;

public class Properties_Sample : MonoBehaviour
{
    #region Case 1 - ������Ƽ �⺻
    // �ܺ� ��ũ��Ʈ���� interger ������ ������ �� ������(�б�), ����(����)�� �� �� �ֽ��ϴ�.
    // ������Ƽ�� get���̳� set�� ����ص� �˴ϴ�.
    // �� set�� ����� ������ �ܺο��� ����� �� �����Ұ��� ����������մϴ�.
    public int integer_Public { get; set; }

    public int integer_OnlyGet { get; }

    // ����� ���ÿ� �ʱ�ȭ�� �����մϴ�.
    public float float_WithInitialize { get; set; } = 0f;


    #region Set�� ��� ����
    public int integer_OnlySet 
    {
        set
        {
            // integer_OnlySet�̶� ������ ���ԵǴ� ���� �־��ݴϴ�.
            // Set������Ƽ�� 'value'��� ���� �ܺο��� �־��ִ� ���� �˴ϴ�.
            integer_OnlySet = value;
        }
    }

    public double double_OnlySet
    {
        set
        {
            // double�̾ value�Դϴ�.
            // Ŭ�����̰ų� ����ü���� value�Դϴ�.
            double_OnlySet = value;
        }
    }

    public class SampleClass
    {
        public int a;
        public bool b;
    }

    public SampleClass SampleClassExample
    {
        set
        {
            SampleClassExample = value;
        }
    }
    #endregion 
    #endregion

    #region Case 2 - ���� ������ ���1
    // get�� set�տ��� ���� �ϳ��� ���������ڸ� ����� �� �ֽ��ϴ�.
    // �̷��ԵǸ� integer_Public���� �ٸ���
    // �ܺ� ��ũ��Ʈ���� �б�� ����������, integer_Public2�� ������ ���� ���� �˴ϴ�.
    // �� ��ũ��Ʈ �������� ������ �ؾ��ϰ�, �ܺο����� �б⸸ �����ϵ��� �� �� �����մϴ�.
    //
    // ex) Player_MoveSpeed, Player_Position ��
    public int integer_Public2 { get; private set; }
    #endregion

    #region Case 3 - ���� ������ ��� ����
    // protected(�� ��ũ��Ʈ + �� ��ũ��Ʈ�� ����� Ŭ���������� ��� ����)
    // ���������ڴ� public ���� ������ �۱� ������
    // set�̶�� ������Ƽ�� public���� ����� �� �����ϴ�.
    //
    // �Ʒ����� �ּ��� Ǯ�� ������ ��Ÿ���ϴ�.
    //protected int integer_Protected { get; public set; }

    // �Ʒ� ��Ȳ�� �� ��Ȳ�� �����մϴ�.
    // private�� protected���� ������ �۱� �����Դϴ�.
    //
    // �Ʒ����� �ּ��� Ǯ�� ������ ��Ÿ���ϴ�.
    //private int integer_Private { get; protected set; }
    #endregion

    #region Case 4 - �̹� �ִ� ���� Ȱ��
    // �ν�����â���� Sprite Renderer�� �巡�׾� ������� ����ͼ� �������ѳ��ٰ� �����غ��ô�.
    [SerializeField] private SpriteRenderer spRenderer;

    // �Լ��� ������ �ʹ� ���� ���µ�, spRenderer�� �ܺο��� �����Ǹ� �ȵ� ��
    // �̷������� �ۼ��ϸ� �����ϴ�.
    //
    // ��ũ��Ʈ ���� �̹� ����Ǿ��ִ� Ŭ������ ����ü ���� �����鵵 �̷������� 
    // ������Ƽ�� ���� �� �ֽ��ϴ�.
    // �߰�ȣ��  get�� �����Ϸ��� return�� ��� � ���� ��ȯ�ϴ���
    // ���������մϴ�.
    public bool FlipX_Property 
    {
        get
        {
            return spRenderer.flipX;
        }
    }


    // �� �Լ��� ���� ������Ƽ�� ��ó�ߴٰ� �����ϸ� �˴ϴ�.
    public bool FlipX_Method()
    {
        return spRenderer.flipX;
    }
    #endregion
}
