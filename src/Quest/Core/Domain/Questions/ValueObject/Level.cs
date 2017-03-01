namespace Domain.Questions.ValueObject
{
    public class Level
    {
        public static readonly Level Basic = new Level(100, "Basic");
        public static readonly Level Amateur = new Level(200, "Amateur");
        public static readonly Level Professional = new Level(300, "Professional");
        public static readonly Level Expert = new Level(400, "Expert");

        public int Index { get; private set; }
        public string DisplayName { get; private set; }
        private Level(int index, string display)
        {
            Index = index;
            DisplayName = display;
        }
        public override string ToString()
        {
            return DisplayName;
        }
    }
}