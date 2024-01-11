
using Core.Utilities.ElasticSearch.Models;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Core.Utilities.ElasticSearch;


public class ElasticSearchManager : IElasticSearch
{
    private readonly ConnectionSettings _connectionSettings;

    public ElasticSearchManager(IConfiguration configuration)
    {
        var settings = configuration.GetSection("ElasticSearchConfig").Get<ElasticSearchConfig>();
        var uri = new System.Uri(settings.ConnectionString);
        _connectionSettings = new ConnectionSettings(uri);
    }

    public async Task<Dictionary<bool,string>> CreateNewIndexAsync(IndexModel indexModel)
    {
        Dictionary<bool, string> openWith =
    new Dictionary<bool, string>();
        var elasticClient = GetElasticClient(indexModel.IndexName);
        if (elasticClient.Indices.Exists(indexModel.IndexName).Exists)
        {
            openWith.Add(false, "Index already exists");
            return openWith;
        }

        var response = await elasticClient.Indices.CreateAsync(indexModel.IndexName, se =>
            se.Settings(a => a.NumberOfReplicas(indexModel.NumberOfReplicas)
                    .NumberOfShards(indexModel.NumberOfShards))
                .Aliases(x => x.Alias(indexModel.AliasName)));

        openWith.Add(response.IsValid, response.IsValid ? "Success" : response.ServerError.Error.Reason);
        return openWith;
    }

    public async Task<Dictionary<bool, string>> DeleteByElasticIdAsync(ElasticSearchModel model)
    {
        Dictionary<bool, string> openWith =
   new Dictionary<bool, string>();
        var elasticClient = GetElasticClient(model.IndexName);
        var response = await elasticClient.DeleteAsync<object>(model.ElasticId, x => x.Index(model.IndexName));
        openWith.Add(response.IsValid, response.IsValid ? "Success" : response.ServerError.Error.Reason);
        return openWith;
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetAllSearch<T>(SearchParameters parameters)
        where T : class
    {
        var type = typeof(T);

        var elasticClient = GetElasticClient(parameters.IndexName);
        var searchResponse = await elasticClient.SearchAsync<T>(s => s
            .Index(Indices.Index(parameters.IndexName))
            .From(parameters.From)
            .Size(parameters.Size));


        var list = searchResponse.Hits.Select(x => new ElasticSearchGetModel<T>()
        {
            ElasticId = x.Id,
            Item = x.Source
        }).ToList();

        return list;
    }

    public IReadOnlyDictionary<IndexName, IndexState> GetIndexList()
    {
        var elasticClient = new ElasticClient(_connectionSettings);
        return elasticClient.Indices.Get(new GetIndexRequest(Indices.All)).Indices;
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetSearchByField<T>(SearchByFieldParameters fieldParameters)
        where T : class
    {
        var elasticClient = GetElasticClient(fieldParameters.IndexName);
        var searchResponse = await elasticClient.SearchAsync<T>(s => s
            .AllIndices()
            .From(fieldParameters.From)
            .Size(fieldParameters.Size)
            .Query(q => q.Match(
                m => m.Field(fieldParameters.FieldName)
                    .Query(fieldParameters.Value)
                    .Operator(Operator.And))));

        var list = searchResponse.Hits.Select(x => new ElasticSearchGetModel<T>()
        {
            ElasticId = x.Id,
            Item = x.Source
        }).ToList();

        return list;
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetSearchBySimpleQueryString<T>(
        SearchByQueryParameters queryParameters)
        where T : class
    {
        var elasticClient = GetElasticClient(queryParameters.IndexName);
        var searchResponse = await elasticClient.SearchAsync<T>(s => s
            .Index(queryParameters.IndexName)
            .From(queryParameters.From)
            .Size(queryParameters.Size)
            .MatchAll()
            .Query(a => a.SimpleQueryString(c => c
                .Name(queryParameters.QueryName)
                .Boost(1.1)
                .Fields(queryParameters.Fields)
                .Query(queryParameters.Query)
                .Analyzer("standard")
                .DefaultOperator(Operator.Or)
                .Flags(SimpleQueryStringFlags.And | SimpleQueryStringFlags.Near)
                .Lenient()
                .AnalyzeWildcard(false)
                .MinimumShouldMatch("30%")
                .FuzzyPrefixLength(0)
                .FuzzyMaxExpansions(50)
                .FuzzyTranspositions()
                .AutoGenerateSynonymsPhraseQuery(false))));

        var list = searchResponse.Hits.Select(x => new ElasticSearchGetModel<T>()
        {
            ElasticId = x.Id,
            Item = x.Source
        }).ToList();

        return list;
    }

    public async Task<Dictionary<bool, string>> InsertAsync(ElasticSearchInsertUpdateModel model)
    {
        Dictionary<bool, string> openWith =
new Dictionary<bool, string>();
        var elasticClient = GetElasticClient(model.IndexName);

        var response = await elasticClient.IndexAsync(model.Item, i => i.Index(model.IndexName)
            .Id(model.ElasticId)
            .Refresh(Refresh.True));
        openWith.Add(response.IsValid, response.IsValid ? "Success" : response.ServerError.Error.Reason);
        return openWith;
    }

    public async Task<Dictionary<bool, string>> InsertManyAsync(string indexName, object[] items)
    {
        Dictionary<bool, string> openWith =
new Dictionary<bool, string>();
        var elasticClient = GetElasticClient(indexName);
        var response = await elasticClient.BulkAsync(a =>
            a.Index(indexName)
                .IndexMany(items));

         openWith.Add(response.IsValid, response.IsValid ? "Success" : response.ServerError.Error.Reason);
        return openWith;
    }

    public async Task<Dictionary<bool, string>> UpdateByElasticIdAsync(ElasticSearchInsertUpdateModel model)
    {
        Dictionary<bool, string> openWith =
new Dictionary<bool, string>();
        var elasticClient = GetElasticClient(model.IndexName);
        var response =
            await elasticClient.UpdateAsync<object>(model.ElasticId, u => u.Index(model.IndexName).Doc(model.Item));
        openWith.Add(response.IsValid, response.IsValid ? "Success" : response.ServerError.Error.Reason);
        return openWith;
    }

    private ElasticClient GetElasticClient(string indexName)
    {
        if (string.IsNullOrEmpty(indexName))
        {
            throw new ArgumentNullException(indexName, "Index name cannot be null or empty ");
        }

        return new ElasticClient(_connectionSettings);
    }
}