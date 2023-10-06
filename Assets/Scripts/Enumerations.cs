namespace Enum
    {
        // 변수명을 선언할 때에는 해당 변수가 어떤 타입인지,
        // 어떤 역할을 하는지 명확하게 하는것이 좋습니다.
        // 대부분 열거형의 이름 맨앞에는 E 라는 대문자를 붙이기도 합니다.

        // 그리고 열거형 제일 마지막에 Max를 두는 이유는, 마우스를 올려보시면 정수로 몇인지 나와있습니다.
        // 배열을 선언하거나, for문등을 사용할 때 3으로 쓰지않고 (int)ESpriteKey.Max로 사용하게 된다면 추후에 열거형이 추가되어도
        // 수정하기가 매우 편합니다.
        //
        // ex) public int[] a = new int[(int)ESpriteKey.max];
        //
        // ex2) 아래 for문은 Max - 1 까지 반복합니다.
        // for(int i = 0; i < (int)ESpriteKey.Max; i++)
        // {
        //      
        // }

        public enum ESpriteKey 
        { 
            Sword_1, Sword_2, Sword_3, Sword_4,
            Sword_5, Sword_6, Sword_7, Sword_8,
            Sword_9, Sword_10, Sword_11, Sword_12,
            Sword_13, 
            Max
        }

        public enum EEventKey
        {
            // 이렇게 열거형 위에 <summary>로 파라미터가 뭐가 필요한지 설명해놓는다면,
            // 나중에도 마우스를 올리는것만으로 파라미터가 어떤것이 사용되는지 알기 쉬워집니다.

            /// <summary> [int] index </summary>
            OnClickInventorySlot,
            /// <summary> [int] index </summary>
            OnPurcharseSword,
        }
    }