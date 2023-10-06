using UnityEngine;

public class Properties_Sample : MonoBehaviour
{
    #region Case 1 - 프로퍼티 기본
    // 외부 스크립트에서 interger 변수를 가져갈 수 있으며(읽기), 수정(쓰기)도 할 수 있습니다.
    // 프로퍼티는 get만이나 set만 사용해도 됩니다.
    // 단 set만 사용할 때에는 외부에서 사용할 때 대입할곳을 지정해줘야합니다.
    public int integer_Public { get; set; }

    public int integer_OnlyGet { get; }

    // 선언과 동시에 초기화도 가능합니다.
    public float float_WithInitialize { get; set; } = 0f;


    #region Set만 사용 예시
    public int integer_OnlySet 
    {
        set
        {
            // integer_OnlySet이란 변수에 대입되는 값을 넣어줍니다.
            // Set프로퍼티는 'value'라는 값이 외부에서 넣어주는 값이 됩니다.
            integer_OnlySet = value;
        }
    }

    public double double_OnlySet
    {
        set
        {
            // double이어도 value입니다.
            // 클래스이거나 구조체여도 value입니다.
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

    #region Case 2 - 범위 한정자 사용1
    // get과 set앞에는 둘중 하나에 범위한정자를 명시할 수 있습니다.
    // 이렇게되면 integer_Public과는 다르게
    // 외부 스크립트에서 읽기는 가능하지만, integer_Public2를 변경할 수는 없게 됩니다.
    // 이 스크립트 내에서만 변경을 해야하고, 외부에서는 읽기만 가능하도록 할 때 유용합니다.
    //
    // ex) Player_MoveSpeed, Player_Position 등
    public int integer_Public2 { get; private set; }
    #endregion

    #region Case 3 - 범위 한정자 사용 에러
    // protected(이 스크립트 + 이 스크립트를 상속한 클래스에서만 사용 가능)
    // 범위한정자는 public 보다 범위가 작기 때문에
    // set이라는 프로퍼티를 public으로 사용할 수 없습니다.
    //
    // 아래줄의 주석을 풀면 에러가 나타납니다.
    //protected int integer_Protected { get; public set; }

    // 아래 상황도 위 상황과 동일합니다.
    // private는 protected보다 범위가 작기 때문입니다.
    //
    // 아래줄의 주석을 풀면 에러가 나타납니다.
    //private int integer_Private { get; protected set; }
    #endregion

    #region Case 4 - 이미 있는 변수 활용
    // 인스펙터창에서 Sprite Renderer를 드래그앤 드랍으로 끌어와서 참조시켜놨다고 가정해봅시다.
    [SerializeField] private SpriteRenderer spRenderer;

    // 함수를 만들기는 너무 별거 없는데, spRenderer가 외부에서 수정되면 안될 때
    // 이런식으로 작성하면 좋습니다.
    //
    // 스크립트 내에 이미 선언되어있는 클래스나 구조체 등의 변수들도 이런식으로 
    // 프로퍼티를 만들 수 있습니다.
    // 중괄호로  get을 구현하려면 return을 적어서 어떤 값을 반환하는지
    // 명시해줘야합니다.
    public bool FlipX_Property 
    {
        get
        {
            return spRenderer.flipX;
        }
    }


    // 이 함수를 위에 프로퍼티로 대처했다고 생각하면 됩니다.
    public bool FlipX_Method()
    {
        return spRenderer.flipX;
    }
    #endregion
}
