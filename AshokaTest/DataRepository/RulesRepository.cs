using AshokaTest.Database;
using AshokaTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AshokaTest.DataRepository
{
    public class RulesRepository
    {
        AshokaTestEntities entities = null;
        public RulesRepository()
        {
            entities = new AshokaTestEntities();
        }


        public List<SelectListItem> GetAllRules()
        {
            List<SelectListItem> rulesViewModels = (from rule in entities.Rules
                                                      select new SelectListItem
                                                      {
                                                          Text = rule.Category,
                                                          Value = rule.Category
                                                      }).ToList();
            return rulesViewModels;
        }

        public RuleViewModel GetRuleDetailsByCategory(string Category)
        {
            RuleViewModel rulesViewModels = (from rule in entities.Rules
                                                    select new RuleViewModel
                                                    {Category=rule.Category,
                                                        Min = rule.Min,
                                                        Max = rule.Max
                                                    }).Where(x=>x.Category== Category).FirstOrDefault();
            return rulesViewModels;
        }
    }
}