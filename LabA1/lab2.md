Sprawdzenie formularza

Po wyłączeniu JavaScript błędy przestały się pojawiać natychmiastowo przy wpisywaniu. Zamiast tego cała strona musiała się przeładować, a walidację wykonał serwer 

Po wysłaniu poprawnego formularza i wciśnięciu Odśwież, książka NIE dodała się drugi raz. To zasługa wzorca PRG i przekierowania RedirectToAction. Gdybyśmy użyli View("Index"), wciśnięcie F5 spowodowałoby ponowne przesłanie formularza i zduplikowanie książki w bazie.

Atrybuty data-val-* w kodzie HTML to specjalne znaczniki generowane przez ASP.NET. Służą one bibliotece jQuery Validation do tego, by wiedziała, jakie reguły nałożyć na konkretne pole, zanim jeszcze klikniemy wyślij.


Pytania kontrolne

1. Domyślnie Razor automatycznie koduje znaki HTML. Jeśli ktoś wpisze w tytule złośliwy kod w JavaScript, wyświetli się on jako zwykły tekst. Użycie Html.Raw() wyłącza to zabezpieczenie, co pozwala przeglądarce wykonać ten skrypt.

2. ViewBag i ViewData żyją tylko przez jedno bieżące żądanie. TempData z kolei wykorzystuje mechanizm sesji/ciasteczek, dzięki czemu potrafi przeżyć jedno dodatkowe przekierowanie. Używamy go np. do pokazania komunikatu o sukcesie po dodaniu obiektu.


3. *Bezpieczeństwo: ukrywamy pola, których użytkownik nie powinien modyfikować, zapobiegając atakowi Overposting.

*Inne reguły walidacji: formularz często wymaga np. potwierdzenia hasła czy zaznaczenia checkboxa Akceptuję regulamin, czego w ogóle nie trzymamy w bazie.

*Formatowanie/łączenie danych: ViewModel może zawierać dane z kilku różnych tabel złączone w jeden wygodny obiekt, gotowy do wyświetlenia na konkretnej stronie.

4. ModelState.IsValid sprawdza, czy dane przesłane w formularzu spełniają wszystkie reguły walidacji określone przez atrybuty w klasie. Sprawdzenie to odbywa się automatycznie podczas tzw. Model Bindingu, tuż po tym jak dane trafią na serwer, ale zanim zostanie wykonany kod wewnątrz metody w kontrolerze.