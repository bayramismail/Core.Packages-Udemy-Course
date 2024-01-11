using Core.Utilities.ElasticSearch.Models;
using Nest;

public interface IElasticSearch
{
    /// <summary>
    /// Creating a new IndexModel
    /// </summary>
    /// <param name="indexModel"></param>
    /// <returns>
    /// The return value is a dictionary indicating the success of the operation. 
    /// The dictionary has a boolean key (true if successful, false otherwise) and 
    /// a string value providing additional information. Example: true, "Success".
    /// </returns>
    Task<Dictionary<bool, string>> CreateNewIndexAsync(IndexModel indexModel);
    /// <summary>
    /// Adding ElasticSearchInsertUpdateModel
    /// </summary>
    /// <param name="model"></param>
    /// <returns>
    /// The return value is a dictionary indicating the success of the operation. 
    /// The dictionary has a boolean key (true if successful, false otherwise) and 
    /// a string value providing additional information. Example: true, "Success".
    /// </returns>
    Task<Dictionary<bool, string>> InsertAsync(ElasticSearchInsertUpdateModel model);
    /// <summary>
    /// Adding list item by indexName
    /// </summary>
    /// <param name="indexName"></param>
    /// <param name="items"></param>
    /// <returns>
    /// The return value is a dictionary indicating the success of the operation. 
    /// The dictionary has a boolean key (true if successful, false otherwise) and 
    /// a string value providing additional information. Example: true, "Success".
    /// </returns>
    Task<Dictionary<bool, string>> InsertManyAsync(string indexName, object[] items);
    IReadOnlyDictionary<IndexName, IndexState> GetIndexList();

    Task<List<ElasticSearchGetModel<T>>> GetAllSearch<T>(SearchParameters parameters)
        where T : class;

    Task<List<ElasticSearchGetModel<T>>> GetSearchByField<T>(SearchByFieldParameters fieldParameters)
        where T : class;

    Task<List<ElasticSearchGetModel<T>>> GetSearchBySimpleQueryString<T>(SearchByQueryParameters queryParameters)
        where T : class;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns>
    /// The return value is a dictionary indicating the success of the operation. 
    /// The dictionary has a boolean key (true if successful, false otherwise) and 
    /// a string value providing additional information. Example: true, "Success".
    /// </returns>
    Task<Dictionary<bool, string>> UpdateByElasticIdAsync(ElasticSearchInsertUpdateModel model);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns>
    /// The return value is a dictionary indicating the success of the operation. 
    /// The dictionary has a boolean key (true if successful, false otherwise) and 
    /// a string value providing additional information. Example: true, "Success".
    /// </returns>
    Task<Dictionary<bool, string>> DeleteByElasticIdAsync(ElasticSearchModel model);
}