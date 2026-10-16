Odpowiedzi - Lab 1

Zadanie A1.

Program.cs - punkt startowy aplikacji. Tutaj konfiguruje się cały serwer 

Controllers/ - kontrolery, czyli klasy z akcjami, które odbierają żądania HTTP od użytkownika i decydują, co z nimi zrobić.

Models/ - zwykłe klasy z danymi, często odzwierciedlają to, co leży w bazie albo jest przesyłane do klienta.

Views/ - widoki  odpowiadają za to, co widzi użytkownik 

wwwroot/ - jedyny folder w projekcie, do którego jest publiczny dostęp. Trzyma się tu pliki statyczne typu CSS, JavaScript i obrazki.

appsettings.json - główna konfiguracja apki 

Properties/launchSettings.json - ustawienia do uruchamiania projektu u nas lokalnie w Riderze/Visual Studio

Część B. 

Metoda żądania: GET

Kod statusu odpowiedzi: 200 OK

Content-Type odpowiedzi: text/html; charset=utf-8

Nagłówek User-Agent: Mozilla/5.0(Windows NT 10.0;Win64;x64)ApplewebKit/537.36 (KHTML, like Gecko)Chrome/155.0.0.0 Safari/537.36 Edg/155.0.0.0

Ile żądań wykonała strona łącznie i dlaczego?:  więcej niż jedno. Najpierw przeglądarka pobiera sam szkielet HTML, a potem doczytuje sobie resztę plików, żeby strona zadziałała 

Co się dzieje po wejściu na /abc: Wyrzuca kod 404 (Not Found).

Pytania kontrolne

Różnice GET i POST: GET służy tylko do pobierania danych z serwera, jest idempotentny  a parametry idą jawnie w adresie URL. POST służy do wysyłania/zapisywania danych , a dane lecą ukryte w ciele żądania .

Kody statusu:
200 - OK 
302 - Found przekierowanie pod inny adres
400 - Bad Request wysłaliśmy złe dane
401 - Unauthorized nie jesteśmy zalogowani
403 - Forbidden brak uprawnień
404 - Not Found strona/zasób nie istnieje
405 - Method Not Allowed dobry adres, ale np. próbujemy wejść GET tam, gdzie można tylko POST
415 - Unsupported Media Type zły format wysyłanych danych, np. czysty tekst zamiast JSON
500 - Internal Server Error wywaliło błąd w naszym kodzie na serwerze)

Skąd model binding wie, skąd wziąć id?: Framework ma wbudowaną kolejność szukania. Najpierw patrzy na ścieżkę z routingu ,a jeśli tam nic nie znajdzie, to dopiero szuka tych parametrów po znaku zapytania.

Dlaczego wwwroot jest dostępne, a Controllers nie?: Bo w Program.cs celowo udostępnia się tylko folder wwwroot (linijka app.UseStaticFiles()). Cała reszta, łącznie z Controllers, to kod źródłowy C#, który serwer tylko kompiluje i uruchamia u siebie w pamięci, a nie wystawia na zewnątrz jako pliki do pobrania. Wpisanie adresu w przeglądarce odpala metodę w kontrolerze, a nie zwraca nam pliku .cs.