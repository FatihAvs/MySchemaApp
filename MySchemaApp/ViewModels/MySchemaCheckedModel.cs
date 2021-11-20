using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp.ViewModels
{
    public class MySchemaCheckedModel

    {
        public string mySchemaName { get; set; }
        public string schemaName { get; set; }

        [BindProperty]
        public List<string> areChecked { get; set; }
       


    }
}
