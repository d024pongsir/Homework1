using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebApplication1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.IsDeleted = true;
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.IsDeleted);
        }

        public IQueryable<客戶資料> 資料篩選邏輯寫在repo類別內(string search_input, string dropdown_selected)
        {
            var data = All().Where(p => p.客戶名稱.Contains(search_input) || p.統一編號.Contains(search_input) ||
                p.電話.Contains(search_input) || p.傳真.Contains(search_input) ||
                p.地址.Contains(search_input) || p.Email.Contains(search_input)
            );

            if (string.IsNullOrEmpty(dropdown_selected)) return data;

            return data.Where(p => p.客戶分類Id.ToString() == dropdown_selected);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}