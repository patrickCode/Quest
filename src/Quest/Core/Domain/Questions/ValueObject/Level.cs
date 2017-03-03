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

        public override bool Equals(object obj)
        {
            var otherLevel = obj as Level;
            if (otherLevel == null)
                return false;

            return (Index == otherLevel.Index && DisplayName == otherLevel.DisplayName);
        }

        public static Level Get(int level)
        {
            switch(level)
            {
                case 100: return Basic;
                case 200: return Amateur;
                case 300: return Professional;
                case 400: return Expert;
                default: return null;
            };
        }
    }
}