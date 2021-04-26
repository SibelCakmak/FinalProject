using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Bussiness
{
    public class BusinessRules
    {
        // istediğimiz kadar parametre ekleyebiliriz.
        //IResult türü dönecek
        public static IResult Run(params IResult[] logics)
        {
            
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;//başarılı ise bişey döndürmesin
        }
    }
}
