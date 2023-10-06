namespace Enum
    {
        // �������� ������ ������ �ش� ������ � Ÿ������,
        // � ������ �ϴ��� ��Ȯ�ϰ� �ϴ°��� �����ϴ�.
        // ��κ� �������� �̸� �Ǿտ��� E ��� �빮�ڸ� ���̱⵵ �մϴ�.

        // �׸��� ������ ���� �������� Max�� �δ� ������, ���콺�� �÷����ø� ������ ������ �����ֽ��ϴ�.
        // �迭�� �����ϰų�, for������ ����� �� 3���� �����ʰ� (int)ESpriteKey.Max�� ����ϰ� �ȴٸ� ���Ŀ� �������� �߰��Ǿ
        // �����ϱⰡ �ſ� ���մϴ�.
        //
        // ex) public int[] a = new int[(int)ESpriteKey.max];
        //
        // ex2) �Ʒ� for���� Max - 1 ���� �ݺ��մϴ�.
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
            // �̷��� ������ ���� <summary>�� �Ķ���Ͱ� ���� �ʿ����� �����س��´ٸ�,
            // ���߿��� ���콺�� �ø��°͸����� �Ķ���Ͱ� ����� ���Ǵ��� �˱� �������ϴ�.

            /// <summary> [int] index </summary>
            OnClickInventorySlot,
            /// <summary> [int] index </summary>
            OnPurcharseSword,
        }
    }