namespace Common
{   
    public static class Constants
    {
        // const : 상수로 선언된 변수로, 프로그램 전체에서 변경되지 않는 값을 저장합니다.
        private const int MAX_HEIGHT = 20; // Maximum height for the pyramid
        public static int MaxHeight
        {
            get 
            { 
                return MAX_HEIGHT; 
            }
        }
    }

    public enum ETypePyamid
    {
        None = 0,
        Increase = 1,
        Decrease = 2,
        ReverseIncrease = 3,
        ReverseDecrease = 4,

        Max,
    }
}