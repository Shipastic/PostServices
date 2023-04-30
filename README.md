# PostServices
Веб-сервис, взаимодействующий с АПИ компаний по доставке посылок
Сервис реализован на .asmx и, представляет собой веб-службу, которая возвращает результат в json-формате.
- Результатом является местонахождение посылки у службы доставки.
- Входными данными являются трек-номер для остлеживания:
```c#
   [WebMethod]
        public List<Checkpoint> GetPostMagicTrans(string slugName, string trackNumber)
        {
            if (slugName == "magictrans")
            {
                Client client = new Client(MagicTranceUrlApi);

                return  magicTranceResponse.GetResponceMagictarnceAsync(trackNumber, client);
            }
            return null;
        }
```
