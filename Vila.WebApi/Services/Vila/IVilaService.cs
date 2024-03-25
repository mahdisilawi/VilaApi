namespace Vila.WebApi.Services.Vila
{
    public interface IVilaService
    {
        List<Models.Vila> GetAll();
        Models.Vila GetById(int id);
        bool Create(Models.Vila model);
        bool Update(Models.Vila model);
        bool Delete(Models.Vila model);
        bool Save();
    }
}
