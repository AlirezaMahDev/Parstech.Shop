﻿using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ISectionRepository : IGenericRepository<Section>
{
    Task<bool> CheckSectionDetailExist(int SectionId);
    Task<Section> GetByOlaviat(int Olaviat);
    Task<Section> GetByStore(int storeId);
    Task<List<Section>> GetSectionsOfStore(int? storeId);
}