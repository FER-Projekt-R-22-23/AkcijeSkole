using AkcijeSkole.Repositories;

/// <summary>
/// Summary description for Class1
/// </summary>
public interface IMaterijalnaPotrebaRepository<TKey, TModel> : IRepository<TKey, TModel>, IAggregateRepository<TKey, TModel>
{
	
}
