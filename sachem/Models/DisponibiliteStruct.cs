namespace sachem.Models
{
    public struct DisponibiliteStruct
    {
        public string Jour;
        public int Minute;
        public DisponibiliteStruct(string s, int m)
        {
            Jour = s;
            Minute = m;
        }
    }
}