namespace Define
{
    public static class Constant
    {
        public const int SWORD_NUMBER = 13;

        public readonly static string[] SWORD_NAMES = new string[SWORD_NUMBER] {"베이직소드", "아이스소드", "플레어소드", "더블소드", "플레이크소드", "데빌소드", "젬스톤완드", "포레아티소드", "스컬소드",
       "헬리오소드", "호루스소드", "포이즌엑스", "홀리완드" };

        public readonly static int[] SWORD_DAMAGE = new int[SWORD_NUMBER] { 3, 5, 7, 10, 13, 15, 18, 20, 22, 25, 27, 30, 35 };
    }
}