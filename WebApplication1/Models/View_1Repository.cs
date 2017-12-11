using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebApplication1.Models
{   
	public  class View_1Repository : EFRepository<View_1>, IView_1Repository
	{

	}

	public  interface IView_1Repository : IRepository<View_1>
	{

	}
}