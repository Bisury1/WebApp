using Microsoft.EntityFrameworkCore;
using WebApp.Application.Common;
using WebApp.Application.DtoRequest.AuthorDtoRequest;
using WebApp.Application.DtoResponse.AuthorDtoResponse;
using WebApp.Application.Mappers.Interfaces;
using WebApp.Domain.DbAdapters;

namespace WebApp.Application.ApiService.AuthorService;

public class AuthorService(
    IAuthorDbContext authorDbContext, 
    IAuthorMapper authorMapper,
    ISaveDbContext saveDbContext) : IAuthorService
{
    public async Task<DtoGetAuthorsResponse> GetAuthors()
    {
        var authors = await authorDbContext.Authors.AsNoTracking().ToListAsync();
        return new DtoGetAuthorsResponse
        {
            Authors = authorMapper.MapToGetAuthorResponses(authors)
        };
    }

    public async Task<DtoGetAuthorsResponse> GetAuthors(DtoGetAuthorsByAlias getAuthorsByAliasRequest)
    {
         var authors = await authorDbContext.Authors.AsNoTracking()
             .Where(a => a.Alias.Contains(getAuthorsByAliasRequest.Alias)).ToListAsync();
         return new DtoGetAuthorsResponse
         {
             Authors = authorMapper.MapToGetAuthorResponses(authors)
         };
    }

    public async Task<DtoGetAuthorResponse> GetAuthor(int id)
    {
        var author = await authorDbContext.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if (author is null)
            ThrowHelper.ThrowRecordNotFoundException("Автор не найден");

        return authorMapper.MapToGetAuthorResponse(author!);
    }

    public async Task<int> CreateAuthor(DtoCreateAuthorRequest createAuthorRequest)
    {
        var author = authorMapper.MapToAuthor(createAuthorRequest);
        var addedAuthor = await authorDbContext.Authors.AddAsync(author);
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return addedAuthor.Entity.Id;
    }

    public async Task<bool> ChangeAuthorAlias(DtoAuthorUpdateAuthorAliasRequest dtoAuthorAliasRequest)
    {
        var author = await authorDbContext.Authors.FirstOrDefaultAsync(a => a.Id == dtoAuthorAliasRequest.Id);
        
        if(author is null)
            ThrowHelper.ThrowRecordNotFoundException("Автор не найден");

        author!.Alias = dtoAuthorAliasRequest.Alias;
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }

    public async Task<bool> DeleteAuthor(DtoDeleteAuthorRequest deleteAuthorRequest)
    {
        var author = await authorDbContext.Authors.FirstOrDefaultAsync(a => a.Id == deleteAuthorRequest.Id);
        if (author is null)
            return false;

        authorDbContext.Authors.Remove(author);
        await saveDbContext.SaveChangesAsync(CancellationToken.None);
        return true;
    }
}