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

        public IQueryable<客戶資料> 資料篩選邏輯寫在repo類別內(string search_input, string dropdown_selected, string sortby)
        {
            var data = All();
            if (!string.IsNullOrEmpty(search_input))
            {
                data = data.Where(p => p.客戶名稱.Contains(search_input) || p.統一編號.Contains(search_input) ||
                p.電話.Contains(search_input) || p.傳真.Contains(search_input) ||
                p.地址.Contains(search_input) || p.Email.Contains(search_input)
                );
            }

            if (!string.IsNullOrEmpty(dropdown_selected)) data = data.Where(p => p.客戶分類Id.ToString() == dropdown_selected);

            if (!string.IsNullOrEmpty(sortby))
            {
                if ("1asc".Equals(sortby)) data = data.OrderBy(p => p.客戶名稱);
                if ("1desc".Equals(sortby)) data = data.OrderByDescending(p => p.客戶名稱);
                if ("2asc".Equals(sortby)) data = data.OrderBy(p => p.統一編號);
                if ("2desc".Equals(sortby)) data = data.OrderByDescending(p => p.統一編號);
                if ("3asc".Equals(sortby)) data = data.OrderBy(p => p.電話);
                if ("3desc".Equals(sortby)) data = data.OrderByDescending(p => p.電話);
                if ("4asc".Equals(sortby)) data = data.OrderBy(p => p.傳真);
                if ("4desc".Equals(sortby)) data = data.OrderByDescending(p => p.傳真);
                if ("5asc".Equals(sortby)) data = data.OrderBy(p => p.地址);
                if ("5desc".Equals(sortby)) data = data.OrderByDescending(p => p.地址);
                if ("6asc".Equals(sortby)) data = data.OrderBy(p => p.Email);
                if ("6desc".Equals(sortby)) data = data.OrderByDescending(p => p.Email);
                if ("7asc".Equals(sortby)) data = data.OrderBy(p => p.客戶分類.客戶分類說明);
                if ("7desc".Equals(sortby)) data = data.OrderByDescending(p => p.客戶分類.客戶分類說明);
            }

            return data;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}