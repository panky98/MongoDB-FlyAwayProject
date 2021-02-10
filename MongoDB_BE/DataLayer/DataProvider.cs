using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text;
using MongoDB.Driver.Core.Servers;
using DataLayer.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Linq;

namespace DataLayer
{
    public static class DataProvider
    {
        #region Rezervacija

        public static void KreirajKolekcijuRezervacija()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("rezervacije");
        }

        public static void KreirajRezervaciju(Rezervacija r)
        {
                IMongoDatabase db = Session.MongoDatabase;
                var collection = db.GetCollection<Rezervacija>("rezervacije");
                collection.InsertOne(r);
        }

        public static Rezervacija VratiRezervacijuPrekoKoda(string kod)
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => x.KodRezervacije==kod).FirstOrDefault();
        }

        public static IList<Rezervacija> VratiRezervacije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Rezervacija>("rezervacije").Find(x => true).ToList<Rezervacija>();
        }

        public static void ObrisiRezervaciju(String kod)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Rezervacija>("rezervacije").DeleteOne(x => x.KodRezervacije == kod);
        }

        public static void AzurirajRezervaciju(String kod,String newStatus)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Rezervacija>.Filter.Eq(x => x.KodRezervacije,kod);
            var update = Builders<Rezervacija>.Update.Set(x => x.Status, newStatus);


            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter, update);
        }

        #endregion

        #region Putnik
        public static void KreirajKolekcijuPutnika()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("putnici");
        }

        public static List<PutnikDTO> VratiSvePutnike()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            var rezervacijeCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<PutnikDTO> putnici = new List<PutnikDTO>();

            foreach (Putnik putnik in collection.Find(x => true).ToList())
            {
                List<Rezervacija> rezervacije = new List<Rezervacija>();
                if (putnik.rezervacije != null)
                {
                    foreach (ObjectId rezervacijaId in putnik.rezervacije)
                    {
                        rezervacije.Add(rezervacijeCollection.Find(x => x.Id == rezervacijaId).First());
                    }
                }
                PutnikDTO newPutnik = new PutnikDTO
                {
                    Id = putnik.Id,
                    jmbg = putnik.jmbg,
                    ime = putnik.ime,
                    prezime = putnik.prezime,
                    pol = putnik.pol,
                    godinaRodjenja = putnik.godinaRodjenja,
                    rezervacije = rezervacije
                };
                putnici.Add(newPutnik);
            }
            return putnici;
        }

        public static List<Putnik> VratiPutnikeRodjenePosle(int godina)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");

            List<Putnik> putnici = new List<Putnik>();

            foreach (Putnik p in collection.Find(x => x.godinaRodjenja > godina).ToList())
            {
                putnici.Add(p);
            }

            return putnici;
        }

        public static List<Putnik> VratiPutnikeSaLetom(String let)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            var collectionRezervacije = db.GetCollection<Rezervacija>("rezervacije");


            List<Putnik> putnici = new List<Putnik>();
            Rezervacija rezervacija = collectionRezervacije.Find(x => x.KodRezervacije == let).First();
            foreach(Putnik p in collection.Find(x => x.rezervacije.Contains(rezervacija.Id)).ToList())
            {
                putnici.Add(p);
            }
            return putnici;
        }

        public static void KreirajPutnika(Putnik putnik)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            collection.InsertOne(putnik);
        }

        public static void DodajRezervacijuPutniku(String kodRezervacije, String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var putniciColleciton = db.GetCollection<Putnik>("putnici");
            var rezervacijaCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<ObjectId> rezervacije = new List<ObjectId>();
            ObjectId putnikId = new ObjectId();
            foreach (Putnik p in putniciColleciton.Find(x => x.jmbg == jmbg).ToList())
            {
                putnikId = p.Id;
                rezervacije = p.rezervacije;
                if (rezervacije == null)
                {
                    rezervacije = new List<ObjectId>();
                }
            }
            Rezervacija rezervacija = rezervacijaCollection.Find(x => x.KodRezervacije == kodRezervacije).First();
            rezervacije.Add(rezervacija.Id);

            var filter = Builders<Putnik>.Filter.Eq(x => x.jmbg, jmbg);
            var update = Builders<Putnik>.Update.Set(x => x.rezervacije, rezervacije);

            db.GetCollection<Putnik>("putnici").UpdateOne(filter, update);

            var filter1 = Builders<Rezervacija>.Filter.Eq(x => x.KodRezervacije, kodRezervacije);
            var update1 = Builders<Rezervacija>.Update.Set(x => x.Putnik, putnikId);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter1, update1);
        }

        public static void AzurirajPutnika(String jmbg, String prezime)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Putnik>.Filter.Eq(x => x.jmbg, jmbg);
            var update = Builders<Putnik>.Update.Set(x => x.prezime, prezime);

            db.GetCollection<Putnik>("putnici").UpdateOne(filter, update);
        }

        public static void ObrisiPutnika(String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Putnik>("putnici").DeleteOne(x => x.jmbg == jmbg);
        }

        #endregion

        #region Komentar

        public static List<Komentar> VratiSveKomentare()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");

            List<Komentar> komentari = new List<Komentar>();

            foreach (Komentar komentar in collection.Find(x => true).ToList())
            {
                komentari.Add(komentar);
            }

            return komentari;
        }

        public static void KreirajKomentar(Komentar komentar)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");
            collection.InsertOne(komentar);
        }

        public static void AzurirajKomentar(ObjectId komentarId, String text)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Komentar>.Filter.Eq(x => x.Id, komentarId);
            var update = Builders<Komentar>.Update.Set(x => x.tekstKomentara, text);


            db.GetCollection<Komentar>("komentari").UpdateOne(filter, update);
        }

        public static void ObrisiKomentar(ObjectId komentarId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Komentar>("komentari").DeleteOne(x => x.Id == komentarId);
        }
        #endregion

        #region Kofer
        public static void KreirajKolekcijuKofera()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Kofer>("koferi");
        }

        public static List<Kofer> VratiSveKofere()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Kofer>("koferi");

            List<Kofer> koferi = new List<Kofer>();

            foreach (Kofer kofer in collection.Find(x => true).ToList())
            {
                koferi.Add(kofer);
            }

            return koferi;
        }

        public static void KreirajKofer(Kofer kofer)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Kofer>("koferi");
            collection.InsertOne(kofer);
        }

        //azuriraj
        public static void AzurirajTipKofera(ObjectId idPro, string newTip)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Kofer>.Filter.Eq(x => x.Id, idPro);
            var update = Builders<Kofer>.Update.Set(x => x.tip, newTip);

            db.GetCollection<Kofer>("koferi").UpdateOne(filter, update);
        }

        public static void ObrisiKofer(ObjectId koferId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Kofer>("koferi").DeleteOne(x => x.Id == koferId);
        }


        #endregion

        #region Proizvod
        public static void KreirajKolekcijuProizvoda()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Proizvod>("proizvodi");
        }

        public static void KreirajProizvod(Proizvod p)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Proizvod>("proizvodi");
            collection.InsertOne(p);
        }

        public static IList<Proizvod> VratiProizvode()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Proizvod>("proizvodi").Find(x => true).ToList<Proizvod>();
        }
        public static void ObrisiProizvod(ObjectId proizvodId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Komentar>("proizvodi").DeleteOne(x => x.Id == proizvodId);
        }
        public static void AzurirajKolicinuProizvoda(ObjectId idPro, int newKol)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Proizvod>.Filter.Eq(x => x.Id, idPro);
            var update = Builders<Proizvod>.Update.Set(x => x.kolicina, newKol);

            db.GetCollection<Proizvod>("proizvodi").UpdateOne(filter, update);
        }
        #endregion

        #region Let
        public static void KreirajLet(Let let)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var letovi = db.GetCollection<Let>("let");
            letovi.InsertOne(let);
        }

        public static IList<LetDTO> VratiSveLetove()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Let> letovi= db.GetCollection<Let>("let").Find(x => true).ToList<Let>();
            IList<LetDTO> letoviDTO = new List<LetDTO>();

            foreach(var l in letovi)
            {
                LetDTO pom = new LetDTO();
                pom.Id = l.Id.ToString();
                pom.PolazniAerodrom = l.PolazniAerodrom;
                pom.DolazniAerodrom = l.DolazniAerodrom;
                pom.DatumLeta = l.DatumLeta;
                pom.BrojSedista = l.BrojSedista;

                pom.ListaRezervacija = new List<string>();
                foreach(var rez in l.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                if(l.AvioKompanija.CompareTo(ObjectId.Empty)==0)
                {
                    pom.AvioKompanija = "";
                }
                else
                {
                    AvioKompanija mojaAvioKompanija = db.GetCollection<AvioKompanija>("avioKompanija").Find(x => x.Id == l.AvioKompanija).FirstOrDefault();
                    if(mojaAvioKompanija!=null)
                    {
                        pom.AvioKompanija = mojaAvioKompanija.Id.ToString();
                    }
                }

                letoviDTO.Add(pom);
            }

            return letoviDTO;
        }

        public static IList<LetDTO> VratiGotoveLetove()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Let> letovi= db.GetCollection<Let>("let").Find(x => !(x.DatumLeta > DateTime.Now)).ToList<Let>();

            IList<LetDTO> letoviDTO = new List<LetDTO>();

            foreach (var l in letovi)
            {
                LetDTO pom = new LetDTO();
                pom.Id = l.Id.ToString();
                pom.PolazniAerodrom = l.PolazniAerodrom;
                pom.DolazniAerodrom = l.DolazniAerodrom;
                pom.DatumLeta = l.DatumLeta;
                pom.BrojSedista = l.BrojSedista;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in l.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                if (l.AvioKompanija.CompareTo(ObjectId.Empty) == 0)
                {
                    pom.AvioKompanija = "";
                }
                else
                {
                    AvioKompanija mojaAvioKompanija = db.GetCollection<AvioKompanija>("avioKompanija").Find(x => x.Id == l.AvioKompanija).FirstOrDefault();
                    if (mojaAvioKompanija != null)
                    {
                        pom.AvioKompanija = mojaAvioKompanija.Id.ToString();
                    }
                }
                letoviDTO.Add(pom);
            }

            return letoviDTO;
        }

        public static IList<LetDTO> VratiTrenutneLetove()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<Let> letovi=db.GetCollection<Let>("let").Find(x => x.DatumLeta > DateTime.Now).ToList<Let>();


            IList<LetDTO> letoviDTO = new List<LetDTO>();

            foreach (var l in letovi)
            {
                LetDTO pom = new LetDTO();
                pom.Id = l.Id.ToString();
                pom.PolazniAerodrom = l.PolazniAerodrom;
                pom.DolazniAerodrom = l.DolazniAerodrom;
                pom.DatumLeta = l.DatumLeta;
                pom.BrojSedista = l.BrojSedista;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in l.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                if (l.AvioKompanija.CompareTo(ObjectId.Empty) == 0)
                {
                    pom.AvioKompanija = "";
                }
                else
                {
                    AvioKompanija mojaAvioKompanija = db.GetCollection<AvioKompanija>("avioKompanija").Find(x => x.Id == l.AvioKompanija).FirstOrDefault();
                    if (mojaAvioKompanija != null)
                    {
                        pom.AvioKompanija = mojaAvioKompanija.Id.ToString();
                    }
                }
                letoviDTO.Add(pom);
            }

            return letoviDTO;
        }

        public static LetDTO VratiLet(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            Let l= db.GetCollection<Let>("let").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            LetDTO pom = new LetDTO();

            if(l!=null)
            {

                pom.Id = l.Id.ToString();
                pom.PolazniAerodrom = l.PolazniAerodrom;
                pom.DolazniAerodrom = l.DolazniAerodrom;
                pom.DatumLeta = l.DatumLeta;
                pom.BrojSedista = l.BrojSedista;

                pom.ListaRezervacija = new List<string>();
                foreach (var rez in l.ListaRezervacija)
                {
                    pom.ListaRezervacija.Add(rez.ToString());
                }
                if (l.AvioKompanija.CompareTo(ObjectId.Empty) == 0)
                {
                    pom.AvioKompanija = "";
                }
                else
                {
                    AvioKompanija mojaAvioKompanija = db.GetCollection<AvioKompanija>("avioKompanija").Find(x => x.Id == l.AvioKompanija).FirstOrDefault();
                    if (mojaAvioKompanija != null)
                    {
                        pom.AvioKompanija = mojaAvioKompanija.Id.ToString();
                    }
                }

            }
            return pom;
        }

        public static void AzurirajLet(String id,LetDTO letDTO)
        {
            IMongoDatabase db = Session.MongoDatabase;

            Let pom = new Let();
            pom.Id = new ObjectId(id);
            pom.PolazniAerodrom = letDTO.PolazniAerodrom;
            pom.DolazniAerodrom = letDTO.DolazniAerodrom;
            pom.DolazniAerodrom = letDTO.DolazniAerodrom;
            pom.DatumLeta = letDTO.DatumLeta;
            pom.BrojSedista = letDTO.BrojSedista;

            pom.ListaRezervacija = new List<ObjectId>();
            foreach(var rez in letDTO.ListaRezervacija)
            {
                pom.ListaRezervacija.Add(new ObjectId(rez));
            }
            if (letDTO.AvioKompanija.Equals(""))
                pom.AvioKompanija = ObjectId.Empty;
            else
            {
                IMongoCollection<AvioKompanija> collectionAvioKompanija = db.GetCollection<AvioKompanija>("avioKompanija");

                AvioKompanija a = collectionAvioKompanija.Find(kompanija => kompanija.Id == new ObjectId(letDTO.AvioKompanija)).FirstOrDefault();

                if (a != null)
                {

                    if (!a.Letovi.Contains(pom.Id))
                    {
                        a.Letovi.Add(pom.Id);
                        collectionAvioKompanija.ReplaceOne(x => x.Id == a.Id, a);

                    }
                    pom.AvioKompanija = new ObjectId(letDTO.AvioKompanija);
                }


            }


            db.GetCollection<Let>("let").ReplaceOne(x => x.Id == new ObjectId(id), pom);
        }

        public static void ObrisiLet(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            /* Let let = db.GetCollection<Let>("let").Find(x => x.Id.ToString() == id).FirstOrDefault();
             db.GetCollection<Let>("let").DeleteOne(let => let.Id == id);
             var avioKompanije = db.GetCollection<AvioKompanija>("avioKompanija");

             AvioKompanija a = avioKompanije.Find(x => x.Id == let.Id).FirstOrDefault();
             if(a!=null)
             {
                 a.Letovi.Remove(a.Letovi.Where(x => x ==new ObjectId(id)).FirstOrDefault());
                 avioKompanije.ReplaceOne(x => x.Id == a.Id, a);
             }*/

           

            db.GetCollection<Let>("let").DeleteOne(x => x.Id == new ObjectId(id));
        }

        #endregion

        #region AvioKompanija

        public static void KreirajAvioKompaniju(AvioKompanija avioKompanija)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<AvioKompanija>("avioKompanija");
            collection.InsertOne(avioKompanija);
        }

        public static IList<AvioKompanijaDTO> VratiAvioKompanije()
        {
            IMongoDatabase db = Session.MongoDatabase;
            IList<AvioKompanija> AvioKompanije = db.GetCollection<AvioKompanija>("avioKompanija").Find(x => true).ToList<AvioKompanija>();

            IList<AvioKompanijaDTO> AvioKompanijeDTO = new List<AvioKompanijaDTO>();
            foreach(var a in AvioKompanije)
            {
                AvioKompanijaDTO pom = new AvioKompanijaDTO();
                pom.Id = a.Id.ToString();
                pom.Naziv = a.Naziv;
                pom.GodinaOsnivanja = a.GodinaOsnivanja;
                pom.GradPredstavnistva = a.GradPredstavnistva;

                foreach(var let in a.Letovi)
                {
                    pom.Letovi.Add(let.ToString());
                }

                foreach(var k in a.Komentari)
                {
                    pom.Komentari.Add(k.ToString());
                }

                AvioKompanijeDTO.Add(pom);
            }

            return AvioKompanijeDTO;
        }

        public static AvioKompanijaDTO VratiAvioKompaniju(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;
            AvioKompanija a= db.GetCollection<AvioKompanija>("avioKompanija").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();


            AvioKompanijaDTO pom = new AvioKompanijaDTO();

            if (a != null)
            {

                pom.Id = a.Id.ToString();
                pom.GodinaOsnivanja = a.GodinaOsnivanja;
                pom.GradPredstavnistva = a.GradPredstavnistva;
                pom.Naziv = a.Naziv;
                foreach (var let in a.Letovi)
                {
                    pom.Letovi.Add(let.ToString());
                }

                foreach (var k in a.Komentari)
                {
                    pom.Komentari.Add(k.ToString());
                }
            }
            return pom;

        }

        public static void AzurirajAvioKompaniju(string id, AvioKompanijaDTOUpdate avioKompanijaDTOUpdate)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<AvioKompanija> avioKompanijaCollection = db.GetCollection<AvioKompanija>("avioKompanija");

            AvioKompanija avioKompanija = avioKompanijaCollection.Find(a => a.Id == new ObjectId(id)).FirstOrDefault();

            if(avioKompanija!=null)
            {
                avioKompanija.Naziv = avioKompanijaDTOUpdate.Naziv;
                avioKompanija.GodinaOsnivanja = avioKompanijaDTOUpdate.GodinaOsnivanja;
                avioKompanija.GradPredstavnistva = avioKompanijaDTOUpdate.GradPredstavnistva;

                avioKompanijaCollection.ReplaceOne(x => x.Id == new ObjectId(id), avioKompanija);
            }
        }
        public static void ObrisiAvioKompanijuId(string id)
        {
            IMongoDatabase db = Session.MongoDatabase;

            IMongoCollection<AvioKompanija> avioKompanijaCollection = db.GetCollection<AvioKompanija>("avioKompanija");

            AvioKompanija avioKompanijaZaBrisanje = avioKompanijaCollection.Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            if(avioKompanijaZaBrisanje!=null)
            {
                foreach(var let in avioKompanijaZaBrisanje.Letovi)
                {
                    DataProvider.ObrisiLet(let.ToString());
                }

                avioKompanijaCollection.DeleteOne(a => a.Id == new ObjectId(id));
            }
        }
        #endregion

        #region Putnik
        public static void KreirajKolekcijuPutnika()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Rezervacija>("putnici");
        }

        public static List<PutnikDTO> VratiSvePutnike()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            var rezervacijeCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<PutnikDTO> putnici = new List<PutnikDTO>();

            foreach (Putnik putnik in collection.Find(x => true).ToList())
            {
                List<Rezervacija> rezervacije = new List<Rezervacija>();
                if (putnik.rezervacije != null)
                {
                    foreach (ObjectId rezervacijaId in putnik.rezervacije)
                    {
                        rezervacije.Add(rezervacijeCollection.Find(x => x.Id == rezervacijaId).First());
                    }
                }
                PutnikDTO newPutnik = new PutnikDTO
                {
                    Id = putnik.Id,
                    jmbg = putnik.jmbg,
                    ime = putnik.ime,
                    prezime = putnik.prezime,
                    pol = putnik.pol,
                    godinaRodjenja = putnik.godinaRodjenja,
                    rezervacije = rezervacije
                };
                putnici.Add(newPutnik);
            }
            return putnici;
        }

        public static List<Putnik> VratiPutnikeRodjenePosle(int godina)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");

            List<Putnik> putnici = new List<Putnik>();

            foreach (Putnik p in collection.Find(x => x.godinaRodjenja > godina).ToList())
            {
                putnici.Add(p);
            }

            return putnici;
        }

        public static List<Putnik> VratiPutnikeSaLetom(String let)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            var collectionRezervacije = db.GetCollection<Rezervacija>("rezervacije");


            List<Putnik> putnici = new List<Putnik>();
            Rezervacija rezervacija = collectionRezervacije.Find(x => x.KodRezervacije == let).First();
            foreach(Putnik p in collection.Find(x => x.rezervacije.Contains(rezervacija.Id)).ToList())
            {
                putnici.Add(p);
            }
            return putnici;
        }

        public static void KreirajPutnika(Putnik putnik)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Putnik>("putnici");
            collection.InsertOne(putnik);
        }

        public static void DodajRezervacijuPutniku(String kodRezervacije, String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var putniciColleciton = db.GetCollection<Putnik>("putnici");
            var rezervacijaCollection = db.GetCollection<Rezervacija>("rezervacije");

            List<ObjectId> rezervacije = new List<ObjectId>();
            ObjectId putnikId = new ObjectId();
            foreach (Putnik p in putniciColleciton.Find(x => x.jmbg == jmbg).ToList())
            {
                putnikId = p.Id;
                rezervacije = p.rezervacije;
                if (rezervacije == null)
                {
                    rezervacije = new List<ObjectId>();
                }
            }
            Rezervacija rezervacija = rezervacijaCollection.Find(x => x.KodRezervacije == kodRezervacije).First();
            rezervacije.Add(rezervacija.Id);

            var filter = Builders<Putnik>.Filter.Eq(x => x.jmbg, jmbg);
            var update = Builders<Putnik>.Update.Set(x => x.rezervacije, rezervacije);

            db.GetCollection<Putnik>("putnici").UpdateOne(filter, update);

            var filter1 = Builders<Rezervacija>.Filter.Eq(x => x.KodRezervacije, kodRezervacije);
            var update1 = Builders<Rezervacija>.Update.Set(x => x.Putnik, putnikId);
            db.GetCollection<Rezervacija>("rezervacije").UpdateOne(filter1, update1);
        }

        public static void AzurirajPutnika(String jmbg, String prezime)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Putnik>.Filter.Eq(x => x.jmbg, jmbg);
            var update = Builders<Putnik>.Update.Set(x => x.prezime, prezime);

            db.GetCollection<Putnik>("putnici").UpdateOne(filter, update);
        }

        public static void ObrisiPutnika(String jmbg)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Putnik>("putnici").DeleteOne(x => x.jmbg == jmbg);
        }

        #endregion

        #region Komentar

        public static List<Komentar> VratiSveKomentare()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");

            List<Komentar> komentari = new List<Komentar>();

            foreach (Komentar komentar in collection.Find(x => true).ToList())
            {
                komentari.Add(komentar);
            }

            return komentari;
        }

        public static void KreirajKomentar(Komentar komentar)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Komentar>("komentari");
            collection.InsertOne(komentar);
        }

        public static void AzurirajKomentar(ObjectId komentarId, String text)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Komentar>.Filter.Eq(x => x.Id, komentarId);
            var update = Builders<Komentar>.Update.Set(x => x.tekstKomentara, text);


            db.GetCollection<Komentar>("komentari").UpdateOne(filter, update);
        }

        public static void ObrisiKomentar(ObjectId komentarId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Komentar>("komentari").DeleteOne(x => x.Id == komentarId);
        }
        #endregion

        #region Kofer
        public static void KreirajKolekcijuKofera()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Kofer>("koferi");
        }

        public static List<Kofer> VratiSveKofere()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Kofer>("koferi");

            List<Kofer> koferi = new List<Kofer>();

            foreach (Kofer kofer in collection.Find(x => true).ToList())
            {
                koferi.Add(kofer);
            }

            return koferi;
        }

        public static void KreirajKofer(Kofer kofer)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Kofer>("koferi");
            collection.InsertOne(kofer);
        }

        //azuriraj
        public static void AzurirajTipKofera(ObjectId idPro, string newTip)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Kofer>.Filter.Eq(x => x.Id, idPro);
            var update = Builders<Kofer>.Update.Set(x => x.tip, newTip);

            db.GetCollection<Kofer>("koferi").UpdateOne(filter, update);
        }

        public static void ObrisiKofer(ObjectId koferId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Kofer>("koferi").DeleteOne(x => x.Id == koferId);
        }


        #endregion

        #region Proizvod
        public static void KreirajKolekcijuProizvoda()
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Proizvod>("proizvodi");
        }

        public static void KreirajProizvod(Proizvod p)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var collection = db.GetCollection<Proizvod>("proizvodi");
            collection.InsertOne(p);
        }

        public static IList<Proizvod> VratiProizvode()
        {
            IMongoDatabase db = Session.MongoDatabase;
            return db.GetCollection<Proizvod>("proizvodi").Find(x => true).ToList<Proizvod>();
        }
        public static void ObrisiProizvod(ObjectId proizvodId)
        {
            IMongoDatabase db = Session.MongoDatabase;
            db.GetCollection<Komentar>("proizvodi").DeleteOne(x => x.Id == proizvodId);
        }
        public static void AzurirajKolicinuProizvoda(ObjectId idPro, int newKol)
        {
            IMongoDatabase db = Session.MongoDatabase;
            var filter = Builders<Proizvod>.Filter.Eq(x => x.Id, idPro);
            var update = Builders<Proizvod>.Update.Set(x => x.kolicina, newKol);

            db.GetCollection<Proizvod>("proizvodi").UpdateOne(filter, update);
        }
        #endregion
    }
}
