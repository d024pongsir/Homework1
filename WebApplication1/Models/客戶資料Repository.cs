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
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}