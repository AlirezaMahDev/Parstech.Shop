﻿using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface ICateguryRepository:IGenericRepository<Categury>
{
    Task<List<Categury>> GetCateguryByParentId(int parentId, int? Row);
    Task<List<Categury>> GetAllParentCategury();
        
    Task<List<Categury>> GetShowCateguryByParentId(int parentId);

    Task<Categury?> GetCateguryByLatinName(string latinName);
    Task<Categury?> GetCateguryByName(string name);
       
}