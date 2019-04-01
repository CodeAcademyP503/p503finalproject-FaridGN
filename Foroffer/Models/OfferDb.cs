using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class OfferDb
    {
        public static void Seed(OfferDbContext offerDbContext, RoleManager<IdentityRole> roleManager)
        {
            if (!offerDbContext.Roles.Any())
            {
                string[] roleNames = Enum.GetNames(typeof(RoleType));
                foreach (string role in roleNames)
                {
                    IdentityRole IdentityRole = new IdentityRole(role);
                    roleManager.CreateAsync(IdentityRole).GetAwaiter().GetResult();
                   
                }
            }

            if (!offerDbContext.Categories.Any())
            {
                Category avto = new Category()
                {
                    Name = "Avto"
                };

                Category home = new Category()
                {
                    Name = "Ev"
                };

                Category digital = new Category()
                {
                    Name = "Digital"
                };

                Category household = new Category()
                {
                    Name = "Məişət"
                };

                Category entertainment = new Category()
                {
                    Name = "Əyləncə"
                };

                Category furniture = new Category()
                {
                    Name = "Mebel"
                };

                Category clothes = new Category()
                {
                    Name = "Geyim"
                };

                Category cafe = new Category()
                {
                    Name = "Kafe"
                };

                Category food = new Category()
                {
                    Name = "Qida"
                };

                Category edu = new Category()
                {
                    Name = "Təhsil"
                };

                Category medical = new Category()
                {
                    Name = "Tibb"
                };

                Category tourism = new Category()
                {
                    Name = "turizm"
                };

                offerDbContext.Categories.AddRange(avto, home, digital, household, entertainment, furniture, clothes, cafe, food, edu, medical, tourism);
                offerDbContext.SaveChanges();

                if (!offerDbContext.Subcategories.Any())
                {
                    #region Avtosub
                    Subcategory newcar = new Subcategory()
                    {
                        CategoryId = avto.Id,
                        Name = "Yeni avto",
                    };

                    Subcategory usedcar = new Subcategory()
                    {
                        CategoryId = avto.Id,
                        Name = "İşlənmiş avto"
                    };

                    Subcategory parts = new Subcategory()
                    {
                        CategoryId = avto.Id,
                        Name = "Ehtiyat hissə"
                    };

                    Subcategory carrent = new Subcategory()
                    {
                        CategoryId = avto.Id,
                        Name = "Avto icarə"
                    };
                    #endregion

                    #region Subhome
                    Subcategory apartment = new Subcategory()
                    {
                        CategoryId = home.Id,
                        Name = "Mənzil"
                    };

                    Subcategory villas = new Subcategory()
                    {
                        CategoryId = home.Id,
                        Name = "Villa"
                    };

                    Subcategory house = new Subcategory()
                    {
                        CategoryId = home.Id,
                        Name = "Həyət evi"
                    };

                    Subcategory office = new Subcategory()
                    {
                        CategoryId = home.Id,
                        Name = "Obyekt"
                    };
                    #endregion

                    #region Subdigit
                    Subcategory tv = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "TV",
                    };

                    Subcategory pc = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "PC kompüter",
                    };

                    Subcategory notebook = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "notbuk",
                    };

                    Subcategory netbuk = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "netbuk",
                    };

                    Subcategory tablet = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "Planşet",
                    };

                    Subcategory smartphone = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "Smartfon",
                    };

                    Subcategory photocamera = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "Foto-kamera",
                    };

                    Subcategory audio = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "Audio",
                    };

                    Subcategory other = new Subcategory()
                    {
                        CategoryId = digital.Id,
                        Name = "Digər",
                    };
                    #endregion

                    #region Subelect
                    Subcategory fridge = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Soyuducu",
                    };

                    Subcategory laundry = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Paltaryuyan",
                    };

                    Subcategory wm = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Qabyuyan",
                    };

                    Subcategory gas = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Qaz",
                    };

                    Subcategory cleaner = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Tozsoran",
                    };

                    Subcategory juicier = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Şirəçəkən",
                    };

                    Subcategory oven = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Oven",
                    };

                    Subcategory others = new Subcategory()
                    {
                        CategoryId = household.Id,
                        Name = "Digər",
                    };
                    #endregion

                    #region Subentertainment
                    Subcategory cinema = new Subcategory()
                    {
                        CategoryId = entertainment.Id,
                        Name = "Kino"
                    };

                    Subcategory consert = new Subcategory()
                    {
                        CategoryId = entertainment.Id,
                        Name = "Konsert"
                    };

                    Subcategory karaoke = new Subcategory()
                    {
                        CategoryId = entertainment.Id,
                        Name = "Karaoke"
                    };

                    Subcategory rest = new Subcategory()
                    {
                        CategoryId = entertainment.Id,
                        Name = "Digər əyləncə"
                    };
                    #endregion

                    #region Subfur
                    Subcategory bedroom = new Subcategory()
                    {
                        CategoryId = furniture.Id,
                        Name = "Yataq dəsti"
                    };

                    Subcategory kitchen = new Subcategory()
                    {
                        CategoryId = furniture.Id,
                        Name = "Mətbəx"
                    };

                    Subcategory livingroom = new Subcategory()
                    {
                        CategoryId = furniture.Id,
                        Name = "Qonaq dəsti"
                    };

                    Subcategory forkid = new Subcategory()
                    {
                        CategoryId = furniture.Id,
                        Name = "Uşaq mebelləri"
                    };
                    #endregion

                    #region Subcafe
                    Subcategory soup = new Subcategory()
                    {
                        CategoryId = cafe.Id,
                        Name = "Duru",
                    };

                    Subcategory meatfish = new Subcategory()
                    {
                        CategoryId = cafe.Id,
                        Name = "Ət və baliq",
                    };

                    Subcategory salad = new Subcategory()
                    {
                        CategoryId = cafe.Id,
                        Name = "Salat",
                    };

                    Subcategory national = new Subcategory()
                    {
                        CategoryId = cafe.Id,
                        Name = "Milli xörəklər",
                    };

                    Subcategory pizza = new Subcategory()
                    {
                        CategoryId = cafe.Id,
                        Name = "Pizza/lahmacun",
                    };

                    Subcategory tea = new Subcategory()
                    {
                        CategoryId = cafe.Id,
                        Name = "Kofe/çay",
                    };
                    #endregion

                    #region Subfood

                    Subcategory fruit = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Meyvə-tərəvəz",
                    };

                    Subcategory meat_poultry = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Ət və toyuq",
                    };

                    Subcategory fish = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Baliq",
                    };

                    Subcategory drinks = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "İçəcəklər",
                    };

                    Subcategory bakery = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Un məmulatları",
                    };

                    Subcategory sweet = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Şirniyyat",
                    };

                    Subcategory dairy = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Süd məhsulları",
                    };

                    Subcategory frozen = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Dondurulmuş məhsullar",
                    };

                    Subcategory etc = new Subcategory()
                    {
                        CategoryId = food.Id,
                        Name = "Digər məhsullar",
                    };
                    #endregion

                    #region Subedu
                    Subcategory lan = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Xarici dil",
                    };

                    Subcategory comp = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Komputer və İT",
                    };

                    Subcategory abituriyent = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Abituriyent kursları",
                    };

                    Subcategory master = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Magistr kursları",
                    };

                    Subcategory foredu = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Xaricdə təhsil",
                    };

                    Subcategory dm = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Digital marketing",
                    };

                    Subcategory finance = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Maliyyə",
                    };

                    Subcategory business = new Subcategory()
                    {
                        CategoryId = edu.Id,
                        Name = "Digər biznes",
                    };
                    #endregion

                    #region SubMed
                    Subcategory neurology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Nevroloji",
                    };

                    Subcategory endocronology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Endokronoloji",
                    };

                    Subcategory dental = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Stomotoloji",
                    };

                    Subcategory cardiology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Kardioloji",
                    };

                    Subcategory urology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Uroloji",
                    };

                    Subcategory oftalmology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Oftalmoloji",
                    };

                    Subcategory oncology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Onkoloji",
                    };

                    Subcategory pediatric = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Pediatrik",
                    };

                    Subcategory gynecology = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Ginekoloji",
                    };

                    Subcategory terapy = new Subcategory()
                    {
                        CategoryId = medical.Id,
                        Name = "Digər terapiya",
                    };
                    #endregion

                    #region Subtur
                    Subcategory travel = new Subcategory()
                    {
                        CategoryId = tourism.Id,
                        Name = "Xaricə səyahət",
                    };

                    Subcategory ourtours = new Subcategory()
                    {
                        CategoryId = tourism.Id,
                        Name = "Ölkə turları",
                    };

                    Subcategory tickets = new Subcategory()
                    {
                        CategoryId = tourism.Id,
                        Name = "Avia biletler",
                    };
                    #endregion

                    #region SubCloth
                    Subcategory men = new Subcategory()
                    {
                        CategoryId = clothes.Id,
                        Name = "Kişi",
                    };

                    Subcategory women = new Subcategory()
                    {
                        CategoryId = clothes.Id,
                        Name = "Qadın",
                    };

                    Subcategory kids = new Subcategory()
                    {
                        CategoryId = clothes.Id,
                        Name = "Uşaq",
                    };
                    #endregion

                    offerDbContext.Subcategories.AddRange(newcar, usedcar, parts, carrent, apartment, villas, house, office, tv, pc, notebook, netbuk, tablet, smartphone, photocamera,
                      audio, other, fridge, laundry, wm, gas, cleaner, juicier, oven, others, cinema, consert, karaoke, rest, bedroom, kitchen, livingroom, forkid, soup, meatfish, salad, national, pizza, tea, fruit, meat_poultry, fish, drinks, bakery,
                      sweet, dairy, frozen, etc, lan, comp, abituriyent, master, foredu, dm, finance, business, neurology, dental, cardiology, urology, oftalmology, oncology, pediatric, gynecology, terapy, travel, ourtours, tickets, men, women, kids);
                    offerDbContext.SaveChanges();
                }
            }
        }
    }
}
