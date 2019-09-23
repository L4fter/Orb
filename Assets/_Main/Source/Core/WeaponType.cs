namespace Core
{
    public struct WeaponType
    {
        public string Name;

        public bool Equals(WeaponType other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is WeaponType other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public static bool operator ==(WeaponType left, WeaponType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WeaponType left, WeaponType right)
        {
            return !left.Equals(right);
        }
    }
}