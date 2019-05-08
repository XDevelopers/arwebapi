namespace AltaRail.Domain
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Name} {Lastname}";
        }
    }
}
