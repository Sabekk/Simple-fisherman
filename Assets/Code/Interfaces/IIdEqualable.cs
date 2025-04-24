public interface IIdEqualable
{
    public int Id { get; }
    public bool IdEquals(int id);
}
