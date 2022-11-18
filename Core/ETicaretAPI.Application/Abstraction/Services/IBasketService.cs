using ETicaretAPI.Application.ViewModels.Baskets;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(VmCreateBasketItem basketItem);
        public Task UpdateQuantityAsync(VmUpdateBasketItem basketItem);
        public Task RemoveBasketItemAsync(string basketId);
    }
}
