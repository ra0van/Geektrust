using geektrust.Family.Enums;

namespace geektrust.Family.Extention
{
    public static class Ext
    {
        public static Gender ToGender(this string input)
        {
            return input == "Male" ? Gender.Male : Gender.Female;
        }
        public static Type ToRelation(this string input)
        {
            return input == "Parent" ? Type.Parent : Type.Spouse;
        }

    }
}
