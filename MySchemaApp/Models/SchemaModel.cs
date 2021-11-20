using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp.Models
{
    public class SchemaModel 
    {
       



      
       
       
        public string Schema { get; set; }
        public string[] Schemas = new string[] { "Kek", "Organizyon Şeması",
            "F89 PROSES ETKİLEŞİM TABLOSU",
            "Kalite Politikası","Kalite Risk Analiz Tablosu",
            "F97 KALİTE HEDEFLERİ İZLEME TABLOSU","GÖREV TANIMLARI","F50 YILLIK KALİBRASYON PLANI",
            "F52 DOĞRULAMA RAPORU","-YILLIK EĞİTİM PLANI-EĞİTİM ETKİNLİĞİ DEĞERLENDİRME...",
           "YENİ FORM","REVİZE FORM","İPTAL FORM","DIŞ KAYNAKLI DOKÜMAN LİSTESİ","F47 TEDARİKÇİ DEĞERLENDİRME FORMU",
           };
}
}
