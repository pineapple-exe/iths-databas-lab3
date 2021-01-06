using SaintNicholas.Data;
using SaintNicholas.Data.Entities;

namespace SaintNicholas.ConsoleApp
{
    class DataSeeding
    {
        public static void CreateTestData(SaintNicholasDbContext context)
        {
            Child child1 = new Child
            {
                Name = "Lisa Nilsson",
                PostalCode = "414 74",
                StreetAddress = "Orustgatan 14",
                City = "Göteborg",
                Country = "Sverige",
                Gender = "girl"
            };
            context.Children.Add(child1);

            BehavioralRecord record1 = new BehavioralRecord
            {
                Child = child1,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record1);

            ChristmasPresent p1 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Coal",
                Receiver = child1
            };
            context.ChristmasPresents.Add(p1);




            Child child2 = new Child
            {
                Name = "Patrik Bolgert",
                PostalCode = "42137",
                StreetAddress = "Tunnlandsgatan 5",
                City = "Västra Frölunda",
                Country = "Sverige",
                Gender = "boy"
            };
            context.Children.Add(child2);

            BehavioralRecord record2 = new BehavioralRecord
            {
                Child = child2,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record2);

            ChristmasPresent p2 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "boy",
                ForNaughtyChild = false,
                Contents = "Spider-Man bed sheets",
                Receiver = child2
            };
            context.ChristmasPresents.Add(p2);




            Child child3 = new Child
            {
                Name = "Mårten Everbrand",
                PostalCode = "972 34",
                StreetAddress = "Grodvägen 1 A",
                City = "Luleå",
                Country = "Sverige",
                Gender = "boy"
            };
            context.Children.Add(child3);

            BehavioralRecord record3 = new BehavioralRecord
            {
                Child = child3,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record3);

            ChristmasPresent p3 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "boy",
                ForNaughtyChild = true,
                Contents = "Socks",
                Receiver = child3
            };
            context.ChristmasPresents.Add(p3);





            Child child4 = new Child
            {
                Name = "Torill Iversen",
                PostalCode = "9513",
                StreetAddress = "Skoleåsen 27",
                City = "Alta",
                Country = "Norge",
                Gender = "girl"
            };
            context.Children.Add(child4);

            BehavioralRecord record4 = new BehavioralRecord
            {
                Child = child4,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record4);

            ChristmasPresent p4 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "girl",
                ForNaughtyChild = false,
                Contents = "Dollhouse",
                Receiver = child4
            };
            context.ChristmasPresents.Add(p4);




            Child child5 = new Child
            {
                Name = "Alisha Aziz",
                PostalCode = "560011",
                StreetAddress = "Park Street",
                City = "Kolkata",
                Country = "India",
                Gender = "girl"
            };
            context.Children.Add(child5);

            BehavioralRecord record5 = new BehavioralRecord
            {
                Child = child5,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record5);

            ChristmasPresent p5 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Coal",
                Receiver = child5
            };
            context.ChristmasPresents.Add(p5);




            Child child6 = new Child
            {
                Name = "Fatima Gulnaz",
                PostalCode = "75500",
                StreetAddress = "I. I. Chundrigar Road",
                City = "Karachi",
                Country = "Islamic Republic of Pakistan",
                Gender = "girl"
            };
            context.Children.Add(child6);

            BehavioralRecord record6 = new BehavioralRecord
            {
                Child = child6,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record6);

            ChristmasPresent p6 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Coal",
                Receiver = child6
            };
            context.ChristmasPresents.Add(p6);




            Child child7 = new Child
            {
                Name = "Gaimu Hanako",
                PostalCode = "100-8994",
                StreetAddress = "Tōkyō-to Chūō-ku Yaesu 1-Chōme 5-ban 3-gō",
                City = "Tokyo",
                Country = "Japan",
                Gender = "girl"
            };
            context.Children.Add(child7);

            BehavioralRecord record7 = new BehavioralRecord
            {
                Child = child7,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record7);

            ChristmasPresent p7 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = false,
                Contents = "Toniebox",
                Receiver = child7
            };
            context.ChristmasPresents.Add(p7);





            Child child8 = new Child
            {
                Name = "Sophia Friis",
                PostalCode = "411 03",
                StreetAddress = "Burggrevegatan 23B",
                City = "Gothenburg",
                Country = "Sweden",
                Gender = "girl"
            };
            context.Children.Add(child8);

            BehavioralRecord record8 = new BehavioralRecord
            {
                Child = child8,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record8);

            ChristmasPresent p8 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Coal",
                Receiver = child8
            };
            context.ChristmasPresents.Add(p8);




            Child child9 = new Child
            {
                Name = "Anna Siintola",
                PostalCode = "02100",
                StreetAddress = "Revontulenkuuja 1",
                City = "Espoo",
                Country = "Finland",
                Gender = "girl"
            };
            context.Children.Add(child9);

            BehavioralRecord record9 = new BehavioralRecord
            {
                Child = child9,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record9);

            ChristmasPresent p9 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "girl",
                ForNaughtyChild = false,
                Contents = "Diary",
                Receiver = child9
            };
            context.ChristmasPresents.Add(p9);





            Child child10 = new Child
            {
                Name = "Sonja Ketterer",
                PostalCode = "02100",
                StreetAddress = "Männi 30",
                City = "Kernu, Harjumaa",
                Country = "Estonia",
                Gender = "girl"
            };
            context.Children.Add(child10);

            BehavioralRecord record10 = new BehavioralRecord
            {
                Child = child10,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record10);

            ChristmasPresent p10 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "girl",
                ForNaughtyChild = false,
                Contents = "Dollhouse",
                Receiver = child10
            };
            context.ChristmasPresents.Add(p10);





            Child child11 = new Child
            {
                Name = "Tegan Holland",
                PostalCode = "RH16 6XP",
                StreetAddress = "30  Newmarket Road",
                City = "Haywards Heath",
                Country = "United Kingdom",
                Gender = "u"
            };
            context.Children.Add(child11);

            BehavioralRecord record11 = new BehavioralRecord
            {
                Child = child11,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record11);

            ChristmasPresent p11 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Socks",
                Receiver = child11
            };
            context.ChristmasPresents.Add(p11);






            Child child12 = new Child
            {
                Name = "Thomas Rhodes",
                PostalCode = "16407",
                StreetAddress = "2413 Harley Brook Lane",
                City = "Pennsylvania",
                Country = "Unites States",
                Gender = "boy"
            };
            context.Children.Add(child12);

            BehavioralRecord record12 = new BehavioralRecord
            {
                Child = child12,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record12);

            ChristmasPresent p12 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "boy",
                ForNaughtyChild = false,
                Contents = "Lego",
                Receiver = child12
            };
            context.ChristmasPresents.Add(p12);




            Child child13 = new Child
            {
                Name = "Moses Bakabulindi",
                PostalCode = "7077",
                StreetAddress = "Plot 60 Bombo Road",
                City = "Kampala",
                Country = "Uganda",
                Gender = "boy"
            };
            context.Children.Add(child13);

            BehavioralRecord record13 = new BehavioralRecord
            {
                Child = child13,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record13);

            ChristmasPresent p13 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = false,
                Contents = "Augmented Reality Globe",
                Receiver = child13
            };
            context.ChristmasPresents.Add(p13);




            Child child14 = new Child
            {
                Name = "Don Kopec",
                PostalCode = "53233",
                StreetAddress = "2832  Larry Street",
                City = "Wisconsin",
                Country = "US",
                Gender = "boy"
            };
            context.Children.Add(child14);

            BehavioralRecord record14 = new BehavioralRecord
            {
                Child = child14,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record14);

            ChristmasPresent p14 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "boy",
                ForNaughtyChild = false,
                Contents = "Model train set",
                Receiver = child14
            };
            context.ChristmasPresents.Add(p14);





            Child child15 = new Child
            {
                Name = "Otilija Gaudesiute",
                PostalCode = "N/A",
                StreetAddress = "Sodų g. 5",
                City = "Vilnius",
                Country = "Lietuva",
                Gender = "girl"
            };
            context.Children.Add(child15);
            BehavioralRecord record15 = new BehavioralRecord
            {
                Child = child15,
                Year = 2020,
                Naughty = true
            };
            context.BehavioralRecords.Add(record15);

            ChristmasPresent p15 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "girl",
                ForNaughtyChild = true,
                Contents = "Socks",
                Receiver = child15
            };
            context.ChristmasPresents.Add(p15);





            Child child16 = new Child
            {
                Name = "Haryati binti Bagus",
                PostalCode = "50400",
                StreetAddress = "A 3 Jln Setiawan",
                City = "Kuala Lumpur",
                Country = "Malaysia",
                Gender = "u"
            };
            context.Children.Add(child16);

            BehavioralRecord record16 = new BehavioralRecord
            {
                Child = child16,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record16);

            ChristmasPresent p16 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = false,
                Contents = "Slime",
                Receiver = child16
            };
            context.ChristmasPresents.Add(p16);





            Child child17 = new Child
            {
                Name = "Janie Garbutt",
                PostalCode = "4430-327",
                StreetAddress = "R Pádua Correia 32",
                City = "Vila Nova de Gaia",
                Country = "Portugal",
                Gender = "u"
            };
            context.Children.Add(child17);

            BehavioralRecord record17 = new BehavioralRecord
            {
                Child = child17,
                Year = 2020,
                Naughty = false
            };
            context.BehavioralRecords.Add(record17);

            ChristmasPresent p17 = new ChristmasPresent
            {
                HandOutYear = 2020,
                ForGender = "u",
                ForNaughtyChild = false,
                Contents = "Super Mario 3D All-Stars",
                Receiver = child17
            };
            context.ChristmasPresents.Add(p17);





            ChristmasPresent p18 = new ChristmasPresent
            {
                HandOutYear = 2021,
                ForGender = "u",
                ForNaughtyChild = false,
                Contents = "Augmented Reality Globe"
            };
            context.ChristmasPresents.Add(p18);

            ChristmasPresent p19 = new ChristmasPresent
            {
                HandOutYear = 2021,
                ForGender = "boy",
                ForNaughtyChild = false,
                Contents = "Coding Robot"
            };
            context.ChristmasPresents.Add(p19);

            ChristmasPresent p20 = new ChristmasPresent
            {
                HandOutYear = 2021,
                ForGender = "boy",
                ForNaughtyChild = true,
                Contents = "Socks"
            };
            context.ChristmasPresents.Add(p20);

            ChristmasPresent p21 = new ChristmasPresent
            {
                HandOutYear = 2021,
                ForGender = "girl",
                ForNaughtyChild = false,
                Contents = "Fairy Light Garden"
            };
            context.ChristmasPresents.Add(p21);

            ChristmasPresent p22 = new ChristmasPresent
            {
                HandOutYear = 2021,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Coal"
            };
            context.ChristmasPresents.Add(p22);

            ChristmasPresent p23 = new ChristmasPresent
            {
                HandOutYear = 2021,
                ForGender = "u",
                ForNaughtyChild = true,
                Contents = "Coal"
            };
            context.ChristmasPresents.Add(p23);





            BehavioralRecord oldRecord1 = new BehavioralRecord
            {
                Child = child1,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord2 = new BehavioralRecord
            {
                Child = child1,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord3 = new BehavioralRecord
            {
                Child = child1,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord4 = new BehavioralRecord
            {
                Child = child2,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord5 = new BehavioralRecord
            {
                Child = child2,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord6 = new BehavioralRecord
            {
                Child = child2,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord7 = new BehavioralRecord
            {
                Child = child3,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord8 = new BehavioralRecord
            {
                Child = child3,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord9 = new BehavioralRecord
            {
                Child = child3,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord10 = new BehavioralRecord
            {
                Child = child4,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord11 = new BehavioralRecord
            {
                Child = child4,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord12 = new BehavioralRecord
            {
                Child = child4,
                Year = 2017,
                Naughty = true
            };




            BehavioralRecord oldRecord13 = new BehavioralRecord
            {
                Child = child5,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord14 = new BehavioralRecord
            {
                Child = child5,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord15 = new BehavioralRecord
            {
                Child = child5,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord16 = new BehavioralRecord
            {
                Child = child6,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord17 = new BehavioralRecord
            {
                Child = child6,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord18 = new BehavioralRecord
            {
                Child = child6,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord19 = new BehavioralRecord
            {
                Child = child7,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord20 = new BehavioralRecord
            {
                Child = child7,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord21 = new BehavioralRecord
            {
                Child = child7,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord22 = new BehavioralRecord
            {
                Child = child8,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord23 = new BehavioralRecord
            {
                Child = child8,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord24 = new BehavioralRecord
            {
                Child = child8,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord25 = new BehavioralRecord
            {
                Child = child9,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord26 = new BehavioralRecord
            {
                Child = child9,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord27 = new BehavioralRecord
            {
                Child = child9,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord28 = new BehavioralRecord
            {
                Child = child10,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord29 = new BehavioralRecord
            {
                Child = child10,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord30 = new BehavioralRecord
            {
                Child = child10,
                Year = 2017,
                Naughty = true
            };




            BehavioralRecord oldRecord31 = new BehavioralRecord
            {
                Child = child11,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord32 = new BehavioralRecord
            {
                Child = child11,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord33 = new BehavioralRecord
            {
                Child = child11,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord34 = new BehavioralRecord
            {
                Child = child12,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord35 = new BehavioralRecord
            {
                Child = child12,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord36 = new BehavioralRecord
            {
                Child = child12,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord37 = new BehavioralRecord
            {
                Child = child13,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord38 = new BehavioralRecord
            {
                Child = child13,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord39 = new BehavioralRecord
            {
                Child = child13,
                Year = 2017,
                Naughty = true
            };




            BehavioralRecord oldRecord40 = new BehavioralRecord
            {
                Child = child14,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord41 = new BehavioralRecord
            {
                Child = child14,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord42 = new BehavioralRecord
            {
                Child = child14,
                Year = 2017,
                Naughty = true
            };




            BehavioralRecord oldRecord43 = new BehavioralRecord
            {
                Child = child15,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord44 = new BehavioralRecord
            {
                Child = child15,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord45 = new BehavioralRecord
            {
                Child = child15,
                Year = 2017,
                Naughty = false
            };




            BehavioralRecord oldRecord46 = new BehavioralRecord
            {
                Child = child16,
                Year = 2019,
                Naughty = false
            };

            BehavioralRecord oldRecord47 = new BehavioralRecord
            {
                Child = child16,
                Year = 2018,
                Naughty = false
            };

            BehavioralRecord oldRecord48 = new BehavioralRecord
            {
                Child = child16,
                Year = 2017,
                Naughty = true
            };



            BehavioralRecord oldRecord49 = new BehavioralRecord
            {
                Child = child17,
                Year = 2019,
                Naughty = true
            };

            BehavioralRecord oldRecord50 = new BehavioralRecord
            {
                Child = child17,
                Year = 2018,
                Naughty = true
            };

            BehavioralRecord oldRecord51 = new BehavioralRecord
            {
                Child = child17,
                Year = 2017,
                Naughty = true
            };



            BehavioralRecord newRecord1 = new BehavioralRecord
            {
                Child = child11,
                Year = 2021,
                Naughty = true
            };
            BehavioralRecord newRecord2 = new BehavioralRecord
            {
                Child = child12,
                Year = 2021,
                Naughty = true
            };
            BehavioralRecord newRecord3 = new BehavioralRecord
            {
                Child = child13,
                Year = 2021,
                Naughty = false
            };
            BehavioralRecord newRecord4 = new BehavioralRecord
            {
                Child = child14,
                Year = 2021,
                Naughty = false
            };

            context.BehavioralRecords.Add(oldRecord1);
            context.BehavioralRecords.Add(oldRecord2);
            context.BehavioralRecords.Add(oldRecord3);
            context.BehavioralRecords.Add(oldRecord4);
            context.BehavioralRecords.Add(oldRecord5);
            context.BehavioralRecords.Add(oldRecord6);
            context.BehavioralRecords.Add(oldRecord7);
            context.BehavioralRecords.Add(oldRecord8);
            context.BehavioralRecords.Add(oldRecord9);
            context.BehavioralRecords.Add(oldRecord10);
            context.BehavioralRecords.Add(oldRecord11);
            context.BehavioralRecords.Add(oldRecord12);
            context.BehavioralRecords.Add(oldRecord13);
            context.BehavioralRecords.Add(oldRecord14);
            context.BehavioralRecords.Add(oldRecord15);
            context.BehavioralRecords.Add(oldRecord16);
            context.BehavioralRecords.Add(oldRecord17);
            context.BehavioralRecords.Add(oldRecord18);
            context.BehavioralRecords.Add(oldRecord19);
            context.BehavioralRecords.Add(oldRecord20);
            context.BehavioralRecords.Add(oldRecord21);
            context.BehavioralRecords.Add(oldRecord22);
            context.BehavioralRecords.Add(oldRecord23);
            context.BehavioralRecords.Add(oldRecord24);
            context.BehavioralRecords.Add(oldRecord25);
            context.BehavioralRecords.Add(oldRecord26);
            context.BehavioralRecords.Add(oldRecord27);
            context.BehavioralRecords.Add(oldRecord28);
            context.BehavioralRecords.Add(oldRecord29);
            context.BehavioralRecords.Add(oldRecord30);
            context.BehavioralRecords.Add(oldRecord31);
            context.BehavioralRecords.Add(oldRecord32);
            context.BehavioralRecords.Add(oldRecord33);
            context.BehavioralRecords.Add(oldRecord34);
            context.BehavioralRecords.Add(oldRecord35);
            context.BehavioralRecords.Add(oldRecord36);
            context.BehavioralRecords.Add(oldRecord37);
            context.BehavioralRecords.Add(oldRecord38);
            context.BehavioralRecords.Add(oldRecord39);
            context.BehavioralRecords.Add(oldRecord40);
            context.BehavioralRecords.Add(oldRecord41);
            context.BehavioralRecords.Add(oldRecord42);
            context.BehavioralRecords.Add(oldRecord43);
            context.BehavioralRecords.Add(oldRecord44);
            context.BehavioralRecords.Add(oldRecord45);
            context.BehavioralRecords.Add(oldRecord46);
            context.BehavioralRecords.Add(oldRecord47);
            context.BehavioralRecords.Add(oldRecord48);
            context.BehavioralRecords.Add(oldRecord49);
            context.BehavioralRecords.Add(oldRecord50);
            context.BehavioralRecords.Add(oldRecord51);

            context.BehavioralRecords.Add(newRecord1);
            context.BehavioralRecords.Add(newRecord2);
            context.BehavioralRecords.Add(newRecord3);
            context.BehavioralRecords.Add(newRecord4);

            context.SaveChanges();
        }
    }
}
