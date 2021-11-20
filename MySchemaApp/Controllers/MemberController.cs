using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySchemaApp.Models;
using MySchemaApp.Models.Entities;
using MySchemaApp.Models.Identity;

using MySchemaApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchemaApp.Controllers
{

    [Authorize]
    public class MemberController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        SchemaModel _schemaModel;
        AppIdentityDbContext _schemaDbContext;
        List<string> kek;
        List<string> OrganizyonSemasi;
        List<string> F89ProsesEtkilesimTablosu;
        List<string> KaliteTablosu;
        List<string> KaliteRiskAnalizTablosu;
        List<string> F97KaliteHedefleriİzlemeTablosu;
        Schema _schema;
        List<string> GorevTanimlari;
        List<string> F50YillikKalibrasyonPlani;
        List<string> YillikEgitimPlani;
        List<string> F52DogrulamaPlani;
        List<string> YeniForm;
        List<string> RevizeForm;
        List<string> IptalForm;
        List<string> DısKaynakliDokumanListesi;
        List<string> F47TedarikciDegerlendirmeFormu;
        MySchemaCheckedModel _mySchemaCheckedModel;
        List<string> myListt;
        List<string> viewList;
        public MemberController(AppIdentityDbContext schemaDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _schemaDbContext = schemaDbContext;
        }
        [HttpGet]

        public IActionResult SchemaChecked()
        {

            _mySchemaCheckedModel = new MySchemaCheckedModel();


            return View(_mySchemaCheckedModel);
        }

        [HttpPost]
        public async Task<IActionResult> SchemaChecked(List<string> areChecked, string schemaName, string mySchemaName)
        {



            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                string UserId = user.Id;
                string combindedString = string.Join("+", areChecked);
                _schema = new Schema();
                _schema.ListeElemanları = combindedString;
                _schema.MySchemaName = mySchemaName;
                _schema.SchemaName = schemaName;
                _schema.UserId = UserId;
                var addedEntity = _schemaDbContext.Entry(_schema);
                addedEntity.State = EntityState.Added;
                _schemaDbContext.SaveChanges();

                return RedirectToAction("GetSchemas", "Member");


            }
            else
            {
                return View(myListt);
            }

        }

        [HttpGet]

        public IActionResult Sil(int id)
        {
            _schema= _schemaDbContext.Schemas.FirstOrDefault(i => i.Id == id);
            var deletedEntity = _schemaDbContext.Entry(_schema);
            deletedEntity.State = EntityState.Deleted;
            _schemaDbContext.SaveChanges();
            TempData["islem"] = "Silme işlemi başarıyla gerçekleşmiştir.";
            return RedirectToAction("GetSchemas","Member");
        }


        [HttpGet]

        public IActionResult HangiSchema()
        {
            _schemaModel = new SchemaModel();
            return View(_schemaModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,string mySchemaName,string schemaName,List<string> areChecked)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                string UserId = user.Id;
                string combindedString = string.Join("+", areChecked);
                _schema = _schemaDbContext.Schemas.FirstOrDefault(i => i.Id == id);
                _schema.ListeElemanları = combindedString;
                _schema.MySchemaName = mySchemaName;
                _schema.SchemaName = schemaName;
                _schema.UserId = UserId;
                var ModifidedEntity = _schemaDbContext.Entry(_schema);
                ModifidedEntity.State = EntityState.Modified;
                _schemaDbContext.SaveChanges();
                TempData["islem"] = "Güncelleme işlemi başarıyla gerçekleşmiştir.";
                return RedirectToAction("GetSchemas", "Member");


            }
            else
            {
                return View();
            }

     
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Schema schema = _schemaDbContext.Schemas.FirstOrDefault(i=>i.Id==id);
            List<string> listeElemanları = schema.ListeElemanları.Split('+').ToList();
            TempData["id"] = schema.Id;
            TempData["schemaName"] = schema.SchemaName;
            TempData["mySchemaName"] = schema.MySchemaName;
            TempData["isaretliOlanlar"] = listeElemanları;
            kek = new List<string>() {"KEK İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
            "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.","F06 DOKÜMAN TESLİM FORMU İLE GENEL MÜDÜRE VERİLİR.",
            "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.","ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.","YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR."
};
            OrganizyonSemasi = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.","F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.","F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR","YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
                "YENİ ORGANİZASYON ŞEMASINA GÖRE  GÖREV TANIMLARI EKLENİR VEYA ÇIKARILIR.",
                "KEK İÇİNDEKİ ORGANİZASYON ŞEMASI YENİSİ İLE DEĞİŞTİRİLİR."};
            F89ProsesEtkilesimTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.",
               "DOKÜMANTE EDİLMİŞ BİLGİ DOSYASINDA  ESKİ DOKÜMAN  'ESKİ FORMLAR' İÇİNE ATILIR VE YENİSİ 'FORMLAR' DOSYASINA EKLENİR.",
               "YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
              " KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR"
};
            KaliteTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.",
                "YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
                "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."
 };
            KaliteRiskAnalizTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
                 "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
                 "RİSK VE FIRSATLAR PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
                 "KALİTE RİSK ANALİZ TALİMATI KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR",
                 "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR"

 };

            F97KaliteHedefleriİzlemeTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
                "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
               " KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."

 };
            GorevTanimlari = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
               " ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.",
               "YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
            "DEĞİŞİKLİKTEN SONRAKİ İLGİLİ FORMLARDA YENİ TANIM KULLANILIR",
                "ORGANİZASYON ŞEMASI KONTROL EDİLİR VE GEREKİRSE DEĞİŞİKLİK YAPILIR."
};

            F50YillikKalibrasyonPlani = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
               " ESKİ DOKÜMAN FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR",
               "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
              " P9 KALİBRASYON PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
              "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."
};
            F52DogrulamaPlani = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
               " F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
               "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
               "ESKİ DOKÜMAN FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
               "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
               "P9 KALİBRASYON PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
               "T 7.1.5.4 DOĞRULAMA TALİMATI KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
               "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR.",

          };
            YillikEgitimPlani = new List<string>()
            {
                    "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR",
                    "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                    "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                    "ESKİ DOKÜMAN FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
                    "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
                  " P10 EĞİTİM PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR",

                    "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."


};
            YeniForm = new List<string>()
            {
                "YENİ FORM OLUŞTURULACAĞI ZAMAN F04 DOKÜMAN YAZIM FORMATINA GÖRE HAZIRLANIR.",
               " F01 DOKÜMAN LİSTESİNE YENİ FORM KAYDEDİLİR",
               "YENİ FORMUN MASTAR HALİ 7.5.1 DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR İÇİNDE TUTULUR.",
              " YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR."
};


            RevizeForm = new List<string>()
            {
               " GEREKLİ DEĞİŞİKLİKLERDEN SONRA FORMUN REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
               "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
              " F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
             " F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
            " YENİ FORMUN MASTAR HALİ 7.5.1 DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR İÇİNDE TUTULUR.",
            "YENİ DOKÜMAN SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR."

 };
            IptalForm = new List<string>()
            {
                "F01 DOKÜMAN LİSTESİNDEN KAYDI SİLİNİR.",
            "İPTAL EDİLECEK FORM 7.5.1>DOKÜMANTE EDİLMİŞ BİLGİ>FORMAR>İPTAL EDİLEN FORMLAR KLASÖRÜNDE TUTULUR.",

            "İPTAL EDİLEN FORMLAR>LİSTE KLASÖRÜ İÇİNDEKİ  F07 YÜRÜRLÜKTEN KALDIRILAN DOKÜMAN LİSTESİNE KAYDEDİLİR."

};
            DısKaynakliDokumanListesi = new List<string>()
            {
               " LİSTEDEKİ DOKÜMANLARIN GÜNCELLİĞİ KONTROL EDİLİR. REVİZE DEĞİŞİKLİKLERİ KAYDEDİLİR.",
              " YENİ KULLANILACAK DOKÜMANLAR LİSTEYE EKLENDİR. ARTIK KULLANILMAYAN DOKÜMANLAR LİSTEDEN ÇIKARILIR.",
              "YENİ DOKÜMAN SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR."



            };

            F47TedarikciDegerlendirmeFormu = new List<string>()
            {
                "4 AYDA BİR YURTİÇİ VE YURTDIŞI OLARAK TEDARİKÇİ DEĞERLENDİRMESİ YAPILIR.",
                "HER DEĞERLENDİRME SONUNDA YURTİÇİ VE YURTDIŞI OLARAK ONAYLI TEDARİÇİ LİSTESİ OLUŞTURULUR.",
              " HER DEĞERLENDİRME SONUNDA TEDARİKÇİLERE PUANLARI BİLDİRİLİR.",

                "ONAYLI TEDARİKÇİ LİSTESİ ONAYLI PDF ŞEKLİNDE SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR."




            };



            viewList = new List<string>();

            if (schema.SchemaName == "Kek")
            {
                viewList = kek;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "Organizyon Şeması")
            {
                viewList = OrganizyonSemasi;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "F89 PROSES ETKİLEŞİM TABLOSU")
            {
                viewList = F89ProsesEtkilesimTablosu;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "Kalite Politikası")
            {
                viewList = KaliteTablosu;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "Kalite Risk Analiz Tablosu")
            {
                viewList = KaliteRiskAnalizTablosu;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "F97 KALİTE HEDEFLERİ İZLEME TABLOSU")
            {
                viewList = F97KaliteHedefleriİzlemeTablosu;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "GÖREV TANIMLARI")
            {
                viewList = GorevTanimlari;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }

            else if (schema.SchemaName == "F50 YILLIK KALİBRASYON PLANI")
            {
                viewList = F50YillikKalibrasyonPlani;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }

            else if (schema.SchemaName == "F52 DOĞRULAMA RAPORU")
            {
                viewList = F52DogrulamaPlani;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }

                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "-YILLIK EĞİTİM PLANI-EĞİTİM ETKİNLİĞİ DEĞERLENDİRME...")
            {
                viewList = YillikEgitimPlani;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                    TempData["isaretliOlmayanlar"] = viewList;

            }
            else if (schema.SchemaName == "YENİ FORM")
            {
                viewList = YeniForm;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "REVİZE FORM")
            {
                viewList = RevizeForm;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "İPTAL FORM")
            {
                viewList = IptalForm;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "DIŞ KAYNAKLI DOKÜMAN LİSTESİ")
            {
                viewList = DısKaynakliDokumanListesi;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);
                }
                TempData["isaretliOlmayanlar"] = viewList;
            }
            else if (schema.SchemaName == "F47 TEDARİKÇİ DEĞERLENDİRME FORMU")
            {
                viewList = F47TedarikciDegerlendirmeFormu;
                foreach (var item in listeElemanları)
                {
                    viewList.Remove(item);

                }
                TempData["isaretliOlmayanlar"] = viewList;
            }



            return View();
        }

        public async Task<IActionResult> GetSchemas()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
           List<Schema> viewSchemas = _schemaDbContext.Schemas.Where(i => i.UserId == user.Id).ToList();
            TempData["userName"] = user.UserName;
            return View(viewSchemas);
        }


        public void LogOut()
        {
            _signInManager.SignOutAsync();
        }

        [HttpPost]

        public IActionResult HangiSchema(SchemaModel schemaa)
        {


            kek = new List<string>() {"KEK İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
            "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.","F06 DOKÜMAN TESLİM FORMU İLE GENEL MÜDÜRE VERİLİR.",
            "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.","ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.","YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR."
};
            OrganizyonSemasi = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.","F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.","F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR","YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
                "YENİ ORGANİZASYON ŞEMASINA GÖRE  GÖREV TANIMLARI EKLENİR VEYA ÇIKARILIR.",
                "KEK İÇİNDEKİ ORGANİZASYON ŞEMASI YENİSİ İLE DEĞİŞTİRİLİR."};
            F89ProsesEtkilesimTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.",
               "DOKÜMANTE EDİLMİŞ BİLGİ DOSYASINDA  ESKİ DOKÜMAN  'ESKİ FORMLAR' İÇİNE ATILIR VE YENİSİ 'FORMLAR' DOSYASINA EKLENİR.",
               "YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
              " KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR"
};
            KaliteTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.",
                "YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
                "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."
 };
            KaliteRiskAnalizTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
                 "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
                 "RİSK VE FIRSATLAR PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
                 "KALİTE RİSK ANALİZ TALİMATI KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR",
                 "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR"

 };

            F97KaliteHedefleriİzlemeTablosu = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                "ESKİ DOKÜMAN DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
                "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
               " KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."

 };
            GorevTanimlari = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
               " ESKİ DOKÜMAN ARŞİV DOSYASINA ATILIR.",
               "YENİ DOKÜMANIN ONAYLI PDF FORMATI SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR VE MAİL İLE BİLGİ VERİLİR.",
            "DEĞİŞİKLİKTEN SONRAKİ İLGİLİ FORMLARDA YENİ TANIM KULLANILIR",
                "ORGANİZASYON ŞEMASI KONTROL EDİLİR VE GEREKİRSE DEĞİŞİKLİK YAPILIR."
};

            F50YillikKalibrasyonPlani = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
                "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
               " ESKİ DOKÜMAN FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR",
               "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
              " P9 KALİBRASYON PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
              "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."
};
            F52DogrulamaPlani = new List<string>()
            {
                "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
               " F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
               "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
               "ESKİ DOKÜMAN FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
               "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
               "P9 KALİBRASYON PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
               "T 7.1.5.4 DOĞRULAMA TALİMATI KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR.",
               "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR.",

          };
            YillikEgitimPlani = new List<string>()
            {
                    "DOKÜMAN İÇİNDEKİ REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR",
                    "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
                    "F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
                    "ESKİ DOKÜMAN FORMLAR>ESKİ FORMLAR DOSYASINA ATILIR.",
                    "YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR.",
                  " P10 EĞİTİM PROSEDÜRÜ KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRİLİR",

                    "KEK İÇİNDEKİ İLGİLİ MADDE KONTROL EDİLİR VE GEREKİRSE DEĞİŞTİRLİR."


};
            YeniForm = new List<string>()
            {
                "YENİ FORM OLUŞTURULACAĞI ZAMAN F04 DOKÜMAN YAZIM FORMATINA GÖRE HAZIRLANIR.",
               " F01 DOKÜMAN LİSTESİNE YENİ FORM KAYDEDİLİR",
               "YENİ FORMUN MASTAR HALİ 7.5.1 DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR İÇİNDE TUTULUR.",
              " YENİ DOKÜMAN SERVER ÜZERİNDEN FORMLAR KLASÖRÜNDE PERSONELLERLE PAYLAŞILIR."
};


            RevizeForm = new List<string>()
            {
               " GEREKLİ DEĞİŞİKLİKLERDEN SONRA FORMUN REVİZYON NUMARASI VE TARİHİ DEĞİŞTİRİLİR.",
               "F01 DOKÜMAN LİSTESİNE YENİ REVİZYON NUMARASI VE TARİHİ YAZILIR.",
              " F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
             " F84 DOKÜMAN REVİZYONU İZLEME FORMUNA YAPILAN DEĞİŞİKLİK YAZILIR.",
            " YENİ FORMUN MASTAR HALİ 7.5.1 DOKÜMANTE EDİLMİŞ BİLGİ>FORMLAR İÇİNDE TUTULUR.",
            "YENİ DOKÜMAN SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR."

 };
            IptalForm = new List<string>()
            {
                "F01 DOKÜMAN LİSTESİNDEN KAYDI SİLİNİR.",
            "İPTAL EDİLECEK FORM 7.5.1>DOKÜMANTE EDİLMİŞ BİLGİ>FORMAR>İPTAL EDİLEN FORMLAR KLASÖRÜNDE TUTULUR.",

            "İPTAL EDİLEN FORMLAR>LİSTE KLASÖRÜ İÇİNDEKİ  F07 YÜRÜRLÜKTEN KALDIRILAN DOKÜMAN LİSTESİNE KAYDEDİLİR."

};
            DısKaynakliDokumanListesi = new List<string>()
            {
               " LİSTEDEKİ DOKÜMANLARIN GÜNCELLİĞİ KONTROL EDİLİR. REVİZE DEĞİŞİKLİKLERİ KAYDEDİLİR.",
              " YENİ KULLANILACAK DOKÜMANLAR LİSTEYE EKLENDİR. ARTIK KULLANILMAYAN DOKÜMANLAR LİSTEDEN ÇIKARILIR.",
              "YENİ DOKÜMAN SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR."



            };

            F47TedarikciDegerlendirmeFormu = new List<string>()
            {
                "4 AYDA BİR YURTİÇİ VE YURTDIŞI OLARAK TEDARİKÇİ DEĞERLENDİRMESİ YAPILIR.",
                "HER DEĞERLENDİRME SONUNDA YURTİÇİ VE YURTDIŞI OLARAK ONAYLI TEDARİÇİ LİSTESİ OLUŞTURULUR.",
              " HER DEĞERLENDİRME SONUNDA TEDARİKÇİLERE PUANLARI BİLDİRİLİR.",

                "ONAYLI TEDARİKÇİ LİSTESİ ONAYLI PDF ŞEKLİNDE SERVER ÜZERİNDEN PERSONELLERLE PAYLAŞILIR."




            };




            _schemaModel = schemaa;

            var hey = _schemaModel.Schema;

            if (hey == "Kek")
            {
                TempData["b"] = "Kek";
                TempData["a"] = kek;
            }
            else if (hey == "Organizyon Şeması")
            {
                TempData["b"] = "Organizyon Şeması";
                TempData["a"] = OrganizyonSemasi;
            }
            else if (hey == "F89 PROSES ETKİLEŞİM TABLOSU")
            {
                TempData["b"] = "F89 PROSES ETKİLEŞİM TABLOSU";
                TempData["a"] = F89ProsesEtkilesimTablosu;
            }
            else if (hey == "Kalite Politikası")
            {
                TempData["b"] = "Kalite Politikası";
                TempData["a"] = KaliteTablosu;
            }
            else if (hey == "Kalite Risk Analiz Tablosu")
            {
                TempData["b"] = "Kalite Risk Analiz Tablosu";
                TempData["a"] = KaliteRiskAnalizTablosu;
            }
            else if (hey == "F97 KALİTE HEDEFLERİ İZLEME TABLOSU")
            {
                TempData["b"] = "F97 KALİTE HEDEFLERİ İZLEME TABLOSU";
                TempData["a"] = F97KaliteHedefleriİzlemeTablosu;
            }
            else if (hey == "GÖREV TANIMLARI")
            {
                TempData["b"] = "GÖREV TANIMLARI";
                TempData["a"] = GorevTanimlari;
            }

            else if (hey == "F50 YILLIK KALİBRASYON PLANI")
            {
                TempData["b"] = "F50 YILLIK KALİBRASYON PLANI";
                TempData["a"] = F50YillikKalibrasyonPlani;
            }

            else if (hey == "F52 DOĞRULAMA RAPORU")
            {
                TempData["b"] = "F52 DOĞRULAMA RAPORU";
                TempData["a"] = F52DogrulamaPlani;
            }
            else if (hey == "-YILLIK EĞİTİM PLANI-EĞİTİM ETKİNLİĞİ DEĞERLENDİRME...")
            {
                TempData["b"] = "-YILLIK EĞİTİM PLANI-EĞİTİM ETKİNLİĞİ DEĞERLENDİRME...";
                TempData["a"] = YillikEgitimPlani;
            }
            else if (hey == "YENİ FORM")
            {
                TempData["b"] = "YENİ FORM";
                TempData["a"] = YeniForm;
            }
            else if (hey == "REVİZE FORM")
            {
                TempData["b"] = "REVİZE FORM";
                TempData["a"] = RevizeForm;
            }
            else if (hey == "İPTAL FORM")
            {
                TempData["b"] = "İPTAL FORM";
                TempData["a"] = IptalForm;
            }
            else if (hey == "DIŞ KAYNAKLI DOKÜMAN LİSTESİ")
            {
                TempData["b"] = "DIŞ KAYNAKLI DOKÜMAN LİSTESİ";
                TempData["a"] = DısKaynakliDokumanListesi;
            }
            else if (hey == "F47 TEDARİKÇİ DEĞERLENDİRME FORMU")
            {
                TempData["b"] = "F47 TEDARİKÇİ DEĞERLENDİRME FORMU";
                TempData["a"] = F47TedarikciDegerlendirmeFormu;

            }


            if (ModelState.IsValid)
            {



                return RedirectToAction("SchemaChecked");

            }



            else
            {

                return View(_schemaModel);
            };





        }
    }
}
