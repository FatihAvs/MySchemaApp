using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp.Models.Entities
{
    public class Schema
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MySchemaName { get; set; }
        public string SchemaName { get; set; }
        public string ListeElemanları { get; set; }
    }
}
