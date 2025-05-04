namespace LibraryAPI.Application.Contracts.ClientContracts
{
    public interface IBaseClient<T> where T: class
    {
        Task<bool> EntityExists(int entityId, T Entity);
    }
}
